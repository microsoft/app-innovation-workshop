using System.Collections.Generic;
using System.Threading.Tasks;
using ContosoFieldService.Models;
using Refit;

namespace ContosoFieldService.Services
{
    [Headers(Helpers.Constants.ApiManagementKey)]
    public interface IPartsServiceAPI
    {
        [Get("/part/")]
        Task<List<Part>> GetParts();

        [Get("/part/{id}/")]
        Task<Part> GetPartById(string id);

        [Get("/search/parts/?keyword={keyword}")]
        Task<List<Part>> SearchParts(string keyword);

        [Post("/part/")]
        Task<Part> CreatePart([Body] Part job);

        [Post("/part/{id}/")]
        Task<Part> DeletePart(string id);
    }

    public class PartsAPIService
    {
        public async Task<Part> CreatePartAsync(Part part)
        {
            var contosoMaintenanceApi = RestService.For<IPartsServiceAPI>(Helpers.Constants.BaseUrl);
            return await contosoMaintenanceApi.CreatePart(part);
        }

        public async Task<List<Part>> GetPartsAsync()
        {
            var contosoMaintenanceApi = RestService.For<IPartsServiceAPI>(Helpers.Constants.BaseUrl);
            return await contosoMaintenanceApi.GetParts();
        }

        public async Task<Part> GetPartByIdAsync(string id)
        {
            var contosoMaintenanceApi = RestService.For<IPartsServiceAPI>(Helpers.Constants.BaseUrl);
            return await contosoMaintenanceApi.GetPartById(id);
        }

        public async Task<Part> DeletePartByIdAsync(string id)
        {
            var contosoMaintenanceApi = RestService.For<IPartsServiceAPI>(Helpers.Constants.BaseUrl);
            return await contosoMaintenanceApi.DeletePart(id);
        }

        public async Task<List<Part>> SearchPartsAsync(string keyword)
        {
            var contosoMaintenanceApi = RestService.For<IPartsServiceAPI>(Helpers.Constants.BaseUrl);
            return await contosoMaintenanceApi.SearchParts(keyword);
        }
    }
}
