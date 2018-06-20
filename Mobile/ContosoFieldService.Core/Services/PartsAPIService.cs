using System.Collections.Generic;
using System.Threading.Tasks;
using ContosoFieldService.Models;
using Refit;
using ContosoFieldService.Helpers;
using Plugin.Connectivity;
using MonkeyCache.FileStore;
using System.Linq;
using System;
using Microsoft.AppCenter.Crashes;

namespace ContosoFieldService.Services
{
    public interface IPartsServiceAPI
    {
        [Get("/part/")]
        Task<List<Part>> GetParts([Header("Ocp-Apim-Subscription-Key")] string apiManagementKey);

        [Get("/part/{id}/")]
        Task<Part> GetPartById(string id, [Header("Ocp-Apim-Subscription-Key")] string apiManagementKey);

        [Get("/search/parts/?keyword={keyword}")]
        Task<List<Part>> SearchParts(string keyword, [Header("Ocp-Apim-Subscription-Key")] string apiManagementKey);

        [Post("/part/")]
        Task<Part> CreatePart([Body] Part part, [Header("Ocp-Apim-Subscription-Key")] string apiManagementKey);

        [Post("/part/{id}/")]
        Task<Part> DeletePart(string id, [Header("Authorization")] string authorization, [Header("Ocp-Apim-Subscription-Key")] string apiManagementKey);
    }

    public class PartsAPIService : BaseAPIService
    {
        // Note: Usually, we would create only one instance of the IPartsServiceAPI here and
        // Re-use it for every operation. For this demo, we can chance the BaseUrl at runtime, so we
        // Need a way to create the api for every single call
        // readonly IPartsServiceAPI api = RestService.For<IPartsServiceAPI>(Helpers.Constants.BaseUrl);

        public PartsAPIService()
        {
            CacheKey = "Parts";
        }

        public async Task<(ResponseCode code, List<Part> result)> GetPartsAsync(bool force = false)
        {
            // Handle online/offline scenario
            if (!CrossConnectivity.Current.IsConnected && Barrel.Current.Exists(CacheKey))
            {
                // If no connectivity, we'll return the cached list.
                return (ResponseCode.NotConnected, Barrel.Current.Get<List<Part>>(CacheKey));
            }

            // If the data isn't too old, we'll go ahead and return it rather than call the backend again.
            if (!force && !Barrel.Current.IsExpired(CacheKey) && Barrel.Current.Exists(CacheKey))
            {
                var parts = Barrel.Current.Get<IEnumerable<Part>>(CacheKey);
                return (ResponseCode.Success, parts.ToList());
            }

            try
            {
                IPartsServiceAPI api = RestService.For<IPartsServiceAPI>(Helpers.Constants.BaseUrl);

                // Use Polly to handle retrying
                var pollyResult = await Policy.ExecuteAndCaptureAsync(async () => await api.GetParts(Constants.ApiManagementKey));
                if (pollyResult.Result != null)
                {
                    // Save parts into the cache
                    Barrel.Current.Add(CacheKey, pollyResult.Result, TimeSpan.FromMinutes(5));
                    return (ResponseCode.Success, pollyResult.Result);
                }
            }
            catch (UriFormatException)
            {
                // No or invalid BaseUrl set in Constants.cs
                return (ResponseCode.ConfigurationError, null);
            }
            catch (ArgumentException ex)
            {
                //Lets report this exception to App Center 
                Crashes.TrackError(ex);

                // Backend not found at specified BaseUrl in Constants.cs or call limit reached
                return (ResponseCode.BackendNotFound, null);
            }
            catch (Exception)
            {
                // Everything else
                return (ResponseCode.Error, null);
            }

            return (ResponseCode.Error, null);
        }

        public async Task<(ResponseCode code, List<Part> result)> SearchPartsAsync(string keyword)
        {
            IPartsServiceAPI api = RestService.For<IPartsServiceAPI>(Helpers.Constants.BaseUrl);

            var pollyResult = await Policy.ExecuteAndCaptureAsync(async () => await api.SearchParts(keyword, Constants.ApiManagementKey));
            if (pollyResult.Result != null)
            {
                return (ResponseCode.Success, pollyResult.Result);
            }

            return (ResponseCode.Error, null);
        }
    }
}
