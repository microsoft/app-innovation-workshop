using System.Collections.Generic;
using System.Threading.Tasks;
using ContosoFieldService.Models;
using Refit;

namespace ContosoFieldService.Services
{
    [Headers(Helpers.Constants.ApiManagementKey)]
    public interface IPartsService
    {
        [Get("/api/part/")]
        Task<List<Job>> GetParts();

        [Get("/api/part/{id}/")]
        Task<Job> GetPartById(string id);

        [Get("/api/search/parts/?keyword={keyword}")]
        Task<List<Job>> SearchParts(string keyword);

        [Post("/api/part/")]
        Task<Job> CreatePart([Body] Job job);

        [Post("/api/part/{id}/")]
        Task<Job> DeletePart(string id);
    }

    public class PartsAPIService
    {
        public async Task<Job> CreatePartAsync(Job job)
        {
            var contosoMaintenanceApi = RestService.For<IJobServiceAPI>(Helpers.Constants.BaseUrl);
            return await contosoMaintenanceApi.CreateJob(job);
        }

        public async Task<List<Job>> GetPartsAsync()
        {
            var contosoMaintenanceApi = RestService.For<IJobServiceAPI>(Helpers.Constants.BaseUrl);
            return await contosoMaintenanceApi.GetJobs();
        }

        public async Task<Job> GetPartByIdAsync(string id)
        {
            var contosoMaintenanceApi = RestService.For<IJobServiceAPI>(Helpers.Constants.BaseUrl);
            return await contosoMaintenanceApi.GetJobById(id);
        }

        public async Task<Job> DeletePartByIdAsync(string id)
        {
            var contosoMaintenanceApi = RestService.For<IJobServiceAPI>(Helpers.Constants.BaseUrl);
            return await contosoMaintenanceApi.DeleteJob(id);
        }

        public async Task<List<Job>> SearchPartsAsync(string keyword)
        {
            var contosoMaintenanceApi = RestService.For<IJobServiceAPI>(Helpers.Constants.BaseUrl);
            return await contosoMaintenanceApi.SearchJobs(keyword);
        }
    }
}
