using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Identity.Client;

namespace ContosoFieldService.Services
{
    public interface IAuthenticationService
    {
        Task LoginAsync();
        Task LoginSilentAsync();
        Task LogoutAsync();
    }

    public class AzureADB2CAuthenticationService : IAuthenticationService
    {  
        public static IAccount CurrentAccount { get; private set; }
        public static string AccessToken { get; private set; }
        public static bool IsLoggedIn { get; private set; }
        public static string CurrentUserEmail { get; private set; }

        readonly IPublicClientApplication client;
        readonly string[] scopes;
        readonly object parentUI;

        public AzureADB2CAuthenticationService(
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

        public async Task<AuthenticationResult> LoginAsync()
        {
            try
            {
                var result = await client
                    .AcquireTokenInteractive(scopes)
                    .WithParentActivityOrWindow(parentUI)
                    .ExecuteAsync();

                SetUserData(result);
                return result;
            }
            catch (Exception ex)
            {
                // User cancelled authentication
                return null;
            }
        }

        public async Task<AuthenticationResult> LoginSilentAsync()
        {
            try
            {
                var accounts = await client.GetAccountsAsync();
                var result = await client
                    .AcquireTokenSilent(scopes, accounts.FirstOrDefault())
                    .ExecuteAsync();

                SetUserData(result);                    
                return result;
            } 
            catch (MsalUiRequiredException)
            {
                // No user logged in or silent refresh not possible
                return null;
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
    }
}
