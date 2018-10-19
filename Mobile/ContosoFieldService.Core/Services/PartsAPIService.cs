using System.Collections.Generic;
using System.Threading.Tasks;
using ContosoFieldService.Models;
using Refit;
using ContosoFieldService.Helpers;
using MonkeyCache.FileStore;
using System.Linq;
using System;
using Xamarin.Essentials;
using Microsoft.AppCenter.Crashes;

namespace ContosoFieldService.Services
{
    public interface IPartsServiceAPI
    {
        [Get("/part/")]
        Task<List<Part>> GetParts();

        [Get("/part/{id}/")]
        Task<Part> GetPartById(string id);

        [Get("/search/parts/?keyword={keyword}")]
        Task<List<Part>> SearchParts(string keyword);

        [Post("/part/")]
        Task<Part> CreatePart([Body] Part part);

        [Post("/part/{id}/")]
        Task<Part> DeletePart(string id, [Header("Authorization")] string authorization);
    }

    public class PartsAPIService : BaseAPIService
    {
        // Note: Usually, we would create only one instance of the IPartsServiceAPI here and
        // Re-use it for every operation. For this demo, we can chance the BaseUrl at runtime, so we
        // Need a way to create the api for every single call
        // readonly IPartsServiceAPI api = GetManagedApiService<IPartsServiceAPI>();

        public PartsAPIService()
        {
            CacheKey = "Parts";
        }

        public async Task<(ResponseCode code, List<Part> result)> GetPartsAsync(bool force = false)
        {
            // Handle online/offline scenario
            if (Connectivity.NetworkAccess != NetworkAccess.Internet && Barrel.Current.Exists(CacheKey))
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
                IPartsServiceAPI api = GetManagedApiService<IPartsServiceAPI>();

                // Use Polly to handle retrying
                var pollyResult = await Policy.ExecuteAndCaptureAsync(async () => await api.GetParts());
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
            IPartsServiceAPI api = GetManagedApiService<IPartsServiceAPI>();

            var pollyResult = await Policy.ExecuteAndCaptureAsync(async () => await api.SearchParts(keyword));
            if (pollyResult.Result != null)
            {
                return (ResponseCode.Success, pollyResult.Result);
            }

            return (ResponseCode.Error, null);
        }
    }
}
