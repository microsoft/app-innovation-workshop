using System.Collections.Generic;
using System.Threading.Tasks;
using ContosoFieldService.Models;
using Refit;
using ContosoFieldService.Helpers;
using Plugin.Connectivity;
using MonkeyCache.FileStore;
using System.Linq;
using System;

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
        public async Task<Part> CreatePartAsync(Part part)
        {
            var contosoMaintenanceApi = RestService.For<IPartsServiceAPI>(Helpers.Constants.BaseUrl);
            return await contosoMaintenanceApi.CreatePart(part, Constants.ApiManagementKey);
        }

        public async Task<List<Part>> GetPartsAsync(bool force = false)
        {
            var key = "Parts";

            // Handle online/offline scenario
            if (!CrossConnectivity.Current.IsConnected && Barrel.Current.Exists(key))
            {
                // If no connectivity, we'll return the cached list.
                return Barrel.Current.Get<List<Part>>(key);
            }

            // If the data isn't too old, we'll go ahead and return it rather than call the backend again.
            if (!force && !Barrel.Current.IsExpired(key) && Barrel.Current.Exists(key))
            {
                var parts = Barrel.Current.Get<IEnumerable<Part>>(key);
                return parts.ToList();
            }

            // Create an instance of the Refit RestService for the part interface.
            var contosoMaintenanceApi = RestService.For<IPartsServiceAPI>(Helpers.Constants.BaseUrl);

            // Use Polly to handle retrying
            var pollyResult = await Policy.ExecuteAndCaptureAsync(async () => await contosoMaintenanceApi.GetParts(Constants.ApiManagementKey));
            if (pollyResult.Result != null)
            {
                // Save parts into the cache
                Barrel.Current.Add(key, pollyResult.Result, TimeSpan.FromMinutes(5));
                return pollyResult.Result;
            }

            return null;
        }

        public async Task<Part> GetPartByIdAsync(string id)
        {
            var contosoMaintenanceApi = RestService.For<IPartsServiceAPI>(Helpers.Constants.BaseUrl);
            return await contosoMaintenanceApi.GetPartById(id, Constants.ApiManagementKey);
        }

        public async Task<Part> DeletePartByIdAsync(string id)
        {
            var contosoMaintenanceApi = RestService.For<IPartsServiceAPI>(Helpers.Constants.BaseUrl);
            return await contosoMaintenanceApi.DeletePart(id, "Bearer " + AuthenticationService.AccessToken, Constants.ApiManagementKey);
        }

        public async Task<List<Part>> SearchPartsAsync(string keyword)
        {
            var contosoMaintenanceApi = RestService.For<IPartsServiceAPI>(Helpers.Constants.BaseUrl);
            return await contosoMaintenanceApi.SearchParts(keyword, Constants.ApiManagementKey);
        }
    }
}
