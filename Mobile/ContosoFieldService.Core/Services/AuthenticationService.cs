using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContosoFieldService.Helpers;
using Microsoft.Identity.Client;
using Newtonsoft.Json.Linq;

namespace ContosoFieldService.Services
{
    public class AuthenticationService
    {
        public static UIParent UIParent;
        public static IUser CurrentUser { get; set; }
        public static string CurrentUserEmail { get; set; }
        public static bool IsLoggedIn { get; set; }
        public static string AccessToken { get; set; }

        PublicClientApplication authClient;
        string[] scopes;
        string signUpAndInPolicy;
        string authority;

        public Action ShowLoginUi { get; set; }

        public AuthenticationService()
        {
            var authorityBase = $"https://login.microsoftonline.com/tfp/{Constants.Tenant}/";
            authority = $"{authorityBase}{Constants.SignUpAndInPolicy}";
            scopes = Constants.Scopes;
            signUpAndInPolicy = Constants.SignUpAndInPolicy;

            try
            {
                authClient = new PublicClientApplication(Constants.ApplicationId, authority); ;
                authClient.ValidateAuthority = false;
                authClient.RedirectUri = $"msalcontosomaintenance://auth";
            }
            catch (ArgumentException)
            {
                // That usually only happens, when ADB2C is not configured correctly or is not
                // configured at all. Should not happen in real-world scenarios but for the
                // matter of this workshop we have to catch it
                authClient = null;
            }
        }

        /// <summary>
        /// Opens a Web Browser to display the Login Website to the user and returns to the app after the process has been
        /// completed or cancelled by the user
        /// </summary>
        /// <returns>The Authentication Result.</returns>
        public async Task<AuthenticationResult> LoginAsync()
        {
            try
            {
                var user = GetUserByPolicy(authClient.Users, signUpAndInPolicy);

                // Open the login web form
                var result = await authClient?.AcquireTokenAsync(scopes, user, UIParent);
                if (result != null)
                {
                    // Login successful, set properties
                    CurrentUser = result.User;
                    AccessToken = result.AccessToken;
                    IsLoggedIn = true;

                    // Get claims
                    var handler = new JwtSecurityTokenHandler();
                    var token = handler.ReadJwtToken(result.AccessToken);
                    CurrentUserEmail = token.Claims.FirstOrDefault(x => x.Type == "emails")?.Value;
                }

                return result;
            }
            catch (MsalServiceException ex)
            {
                if (ex.ErrorCode == MsalClientException.AuthenticationCanceledError)
                {
                    // User cancelled authentication
                    return null;
                }
            }

            return null;
        }

        /// <summary>
        /// Tries to refresh the user's Access Token in the background without any user innteraction
        /// </summary>
        /// <returns>The Authentication Result.</returns>
        public async Task<AuthenticationResult> LoginSilentAsync()
        {
            var user = GetUserByPolicy(authClient?.Users, signUpAndInPolicy);
            if (user == null)
                return null;

            // Try to refresh the token in the background
            var result = await authClient?.AcquireTokenSilentAsync(scopes, user, authority, false);
            if (result != null)
            {
                // Restore successful, set properties
                CurrentUser = result.User;
                AccessToken = result.AccessToken;
                IsLoggedIn = true;

                // Get claims
                var handler = new JwtSecurityTokenHandler();
                var token = handler.ReadJwtToken(result.AccessToken);
                CurrentUserEmail = token.Claims.FirstOrDefault(x => x.Type == "emails")?.Value;
            }

            return result;
        }

        public void Logout()
        {
            foreach (var user in authClient?.Users)
            {
                authClient?.Remove(user);
            }

            // Reset properties
            CurrentUser = null;
            AccessToken = null;
            IsLoggedIn = false;
            CurrentUserEmail = null;
        }

        IUser GetUserByPolicy(IEnumerable<IUser> users, string policy)
        {
            foreach (var user in users)
            {
                string userIdentifier = Base64UrlDecode(user.Identifier.Split('.')[0]);
                if (userIdentifier.EndsWith(policy.ToLower(), StringComparison.OrdinalIgnoreCase)) return user;
            }

            return null;
        }

        string Base64UrlDecode(string s)
        {
            s = s.Replace('-', '+').Replace('_', '/');
            s = s.PadRight(s.Length + (4 - s.Length % 4) % 4, '=');
            var byteArray = Convert.FromBase64String(s);
            var decoded = Encoding.UTF8.GetString(byteArray, 0, byteArray.Count());
            return decoded;
        }
    }
}