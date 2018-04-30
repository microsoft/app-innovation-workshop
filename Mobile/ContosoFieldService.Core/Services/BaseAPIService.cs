using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using Polly;
using Refit;
using MonkeyCache.FileStore;

namespace ContosoFieldService.Services
{
    public abstract class BaseAPIService
    {
        protected AuthenticationService authenticationService;
        protected Policy Policy;
        protected string CacheKey;

        protected BaseAPIService()
        {
            authenticationService = new AuthenticationService();

            // Define Policy
            Policy = Policy
                .Handle<ApiException>()
                .Or<HttpRequestException>()
                .Or<TimeoutException>()
                .RetryAsync(3, async (exception, retry) =>
                {
                    // Handle Unauthorized
                    if (exception is ApiException apiException && apiException.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        if (retry == 1)
                        {
                            // The first time an Unauthorized exception occurs, 
                            // try to aquire an Access Token silently and try again
                            await authenticationService.LoginSilentAsync();
                        }
                        else if (retry == 2)
                        {
                            // If refreshing the access token silently failed, show the login UI 
                            // and let the user enter his credentials again
                            await authenticationService.LoginAsync();
                        }
                    }
                    // Handle everything else
                    else
                    {
                        // Wait a bit and try again
                        Thread.Sleep(TimeSpan.FromSeconds(retry));
                    }
                });
        }

        public void InvalidateCache(string key = "")
        {
            if (string.IsNullOrEmpty(key))
                key = CacheKey;

            Barrel.Current.Empty(key);
        }
    }
}