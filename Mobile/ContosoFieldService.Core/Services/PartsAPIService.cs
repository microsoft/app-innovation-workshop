using System.Collections.Generic;
using System.Threading.Tasks;
using ContosoFieldService.Models;
using Refit;
using ContosoFieldService.Helpers;

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
        Task<Part> CreatePart([Body] Part job, [Header("Ocp-Apim-Subscription-Key")] string apiManagementKey);

        [Post("/part/{id}/")]
        Task<Part> DeletePart(string id, [Header("Ocp-Apim-Subscription-Key")] string apiManagementKey);
    }

    public class PartsAPIService
    {
        public async Task<Part> CreatePartAsync(Part part)
        {
            var contosoMaintenanceApi = RestService.For<IPartsServiceAPI>(Helpers.Constants.BaseUrl);
            return await contosoMaintenanceApi.CreatePart(part, Constants.ApiManagementKey);
        }

        public async Task<List<Part>> GetPartsAsync()
        {
            var contosoMaintenanceApi = RestService.For<IPartsServiceAPI>(Helpers.Constants.BaseUrl);
            return await contosoMaintenanceApi.GetParts(Constants.ApiManagementKey);
        }

        public async Task<Part> GetPartByIdAsync(string id)
        {
            var contosoMaintenanceApi = RestService.For<IPartsServiceAPI>(Helpers.Constants.BaseUrl);
            return await contosoMaintenanceApi.GetPartById(id, Constants.ApiManagementKey);
        }

        public async Task<Part> DeletePartByIdAsync(string id)
        {
            var contosoMaintenanceApi = RestService.For<IPartsServiceAPI>(Helpers.Constants.BaseUrl);
            return await contosoMaintenanceApi.DeletePart(id, Constants.ApiManagementKey);
        }

        public async Task<List<Part>> SearchPartsAsync(string keyword)
        {
            var contosoMaintenanceApi = RestService.For<IPartsServiceAPI>(Helpers.Constants.BaseUrl);
            return await contosoMaintenanceApi.SearchParts(keyword, Constants.ApiManagementKey);
        }
    }
}
