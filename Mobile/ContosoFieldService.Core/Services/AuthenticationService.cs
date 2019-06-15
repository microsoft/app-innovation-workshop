using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Identity.Client;
using Newtonsoft.Json.Linq;

namespace ContosoFieldService.Services
{
    public class User
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string AccessToken { get; set; }
    }

    public interface IAuthenticationService
    {        
        Task LoginAsync();
        Task LoginSilentAsync();
        Task LogoutAsync();
        User GetCurrentUser();
        bool IsLoggedIn();
    }

    public class AzureADB2CAuthenticationService : IAuthenticationService
    {
        static bool isLoggedIn;
        static IAccount currentAccount;
        static User user;

        readonly IPublicClientApplication client;

        readonly string[] scopes;
        readonly object parentUI;
        readonly string authority;
        readonly string singInSignUpPolicy;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tenant">ADB2C Tenant's Domain Name</param>
        /// <param name="clientId">Application ID</param>
        /// <param name="redirectUri">Application's native custom redirect URI</param>
        /// <param name="singInSignUpPolicy">Name of the Sign Up and Sign In Policy</param>
        /// <param name="scopes">URL of the published scope</param>
        /// <param name="parentUI"></param>
        /// <param name="iOSKeyChainGroup"></param>
        /// <param name="hostname">Custom hostname if it differs from 'login.microsoftonline.com'. See here for more info: https://docs.microsoft.com/en-us/azure/active-directory-b2c/b2clogin</param>
        public AzureADB2CAuthenticationService(
            string tenant,
            string clientId,
            string redirectUri,
            string singInSignUpPolicy,
            string[] scopes,
            object parentUI,
            string iOSKeyChainGroup,
            string hostname = "login.microsoftonline.com"
        )
        {
            var authorityBase = $"https://{hostname}/tfp/{tenant}/";
            this.authority = $"{authorityBase}{singInSignUpPolicy}";            
            this.scopes = scopes;
            this.parentUI = parentUI;
            this.singInSignUpPolicy = singInSignUpPolicy;

            client = PublicClientApplicationBuilder
                .Create(clientId)
                .WithB2CAuthority(authority)
                .WithIosKeychainSecurityGroup(iOSKeyChainGroup)
                .WithRedirectUri(redirectUri)
                .Build();
        }

        public async Task LoginAsync()
        {
            try
            {
                var accounts = await client.GetAccountsAsync();
                var account = GetAccountByPolicy(accounts, singInSignUpPolicy);
                var result = await client
                    .AcquireTokenInteractive(scopes)
                    .WithAccount(account)
                    .WithParentActivityOrWindow(parentUI)
                    .ExecuteAsync();

                SetUserData(result);
            }
            catch (MsalClientException ex)
            {
                // User cancelled authentication
            }
        }

        public async Task LoginSilentAsync()
        {
            try
            {
                var accounts = await client.GetAccountsAsync();
                var account = GetAccountByPolicy(accounts, singInSignUpPolicy);
                var result = await client
                    .AcquireTokenSilent(scopes, account)
                    .WithB2CAuthority(authority)
                    .ExecuteAsync();

                SetUserData(result);
            }
            catch (MsalUiRequiredException)
            {
                // No user logged in or silent refresh not possible
            }
        }

        public async Task LogoutAsync()
        {
            await client.RemoveAsync(currentAccount);
            currentAccount = null;            
            isLoggedIn = false;
        }

        private void SetUserData(AuthenticationResult result)
        {
            currentAccount = result.Account;

            var userInformation = ParseIdToken(result.IdToken);
            user = new User
            {
                Id = userInformation["name"]?.ToString(),
                Name = userInformation["name"]?.ToString(),
                AccessToken = result.AccessToken
            };

            var emails = userInformation["emails"] as JArray;
            user.Email = emails?[0].ToString();

            isLoggedIn = true;
        }

        private IAccount GetAccountByPolicy(IEnumerable<IAccount> accounts, string policy)
        {
            foreach (var account in accounts)
            {
                string userIdentifier = account.HomeAccountId.ObjectId.Split('.')[0];
                if (userIdentifier.EndsWith(policy.ToLower())) return account;
            }

            return null;
        }

        private JObject ParseIdToken(string idToken)
        {
            // Get the piece with actual user info
            idToken = idToken.Split('.')[1];
            idToken = Base64UrlDecode(idToken);
            return JObject.Parse(idToken);
        }

        private string Base64UrlDecode(string s)
        {
            s = s.Replace('-', '+').Replace('_', '/');
            s = s.PadRight(s.Length + (4 - s.Length % 4) % 4, '=');
            var byteArray = Convert.FromBase64String(s);
            var decoded = Encoding.UTF8.GetString(byteArray, 0, byteArray.Count());
            return decoded;
        }

        public User GetCurrentUser()
        {
            return user;
        }

        public bool IsLoggedIn()
        {
            return isLoggedIn;
        }
    }

    public class AzureActiveDirectoryAuthenticationService : IAuthenticationService
    {  
        public static IAccount CurrentAccount { get; private set; }
        public static string AccessToken { get; private set; }
        public static bool IsLoggedIn { get; private set; }
        public static string CurrentUserEmail { get; private set; }

        readonly IPublicClientApplication client;
        readonly string[] scopes;
        readonly object parentUI;

        public AzureActiveDirectoryAuthenticationService(
            string clientId,
            string redirectUri,
            string[] scopes,
            object parentUI
        )
        {
            client = PublicClientApplicationBuilder
                .Create(clientId)
                .WithRedirectUri(redirectUri)
                .Build();

            this.scopes = scopes;
            this.parentUI = parentUI;
        }

        public async Task LoginAsync()
        {
            try
            {
                var result = await client
                    .AcquireTokenInteractive(scopes)
                    .WithParentActivityOrWindow(parentUI)
                    .ExecuteAsync();

                SetUserData(result);
            }
            catch (Exception ex)
            {
                // User cancelled authentication
            }
        }

        public async Task LoginSilentAsync()
        {
            try
            {
                var accounts = await client.GetAccountsAsync();
                var result = await client
                    .AcquireTokenSilent(scopes, accounts.FirstOrDefault())
                    .ExecuteAsync();

                SetUserData(result);                    
            } 
            catch (MsalUiRequiredException)
            {
                // No user logged in or silent refresh not possible
            }
        }

        public async Task LogoutAsync()
        {
            await client.RemoveAsync(CurrentAccount);

            // Reset properties
            CurrentAccount = null;
            AccessToken = null;
            IsLoggedIn = false;
            CurrentUserEmail = null;
        }

        private void SetUserData(AuthenticationResult result)
        {
            if (result == null)
                return;

            CurrentAccount = result.Account;
            AccessToken = result.AccessToken;
            IsLoggedIn = true;

            // Get claims
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(result.AccessToken);
            CurrentUserEmail = token.Claims.FirstOrDefault(x => x.Type == "emails")?.Value;
        }

        public User GetCurrentUser()
        {
            throw new NotImplementedException();
        }

        bool IAuthenticationService.IsLoggedIn()
        {
            throw new NotImplementedException();
        }
    }
}
