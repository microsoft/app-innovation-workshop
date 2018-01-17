using System.Collections.Generic;
using System.Threading.Tasks;
using ContosoFieldService.Models;
using Refit;

namespace ContosoFieldService.Services
{
    [Headers(Helpers.Constants.ApiManagementKey)]
    public interface IJobServiceAPI
    {
        [Get("/job/")]
        Task<List<Job>> GetJobs();

        [Get("/job/{id}/")]
        Task<Job> GetJobById(string id);

        [Get("/search/jobs/?keyword={keyword}")]
        Task<List<Job>> SearchJobs(string keyword);

        [Post("/job/")]
        Task<Job> CreateJob([Body] Job job);

        [Post("/job/{id}/")]
        Task<Job> DeleteJob(string id);

        [Patch("/job/")]
        Task<Job> UpdateJob([Body] Job job);
    }

    public class JobsAPIService
    {
        public async Task<Job> CreateJobAsync(Job job)
        {
            var contosoMaintenanceApi = RestService.For<IJobServiceAPI>(Helpers.Constants.BaseUrl);
            return await contosoMaintenanceApi.CreateJob(job);
        }

        public async Task<List<Job>> GetJobsAsync()
        {
            var contosoMaintenanceApi = RestService.For<IJobServiceAPI>(Helpers.Constants.BaseUrl);
            return await contosoMaintenanceApi.GetJobs();
        }

        public async Task<Job> GetJobByIdAsync(string id)
        {
            var contosoMaintenanceApi = RestService.For<IJobServiceAPI>(Helpers.Constants.BaseUrl);
            return await contosoMaintenanceApi.GetJobById(id);
        }

        public async Task<Job> DeleteJobByIdAsync(string id)
        {
            var contosoMaintenanceApi = RestService.For<IJobServiceAPI>(Helpers.Constants.BaseUrl);
            return await contosoMaintenanceApi.DeleteJob(id);
        }

        public async Task<Job> UpdateJob(Job job)
        {
            var contosoMaintenanceApi = RestService.For<IJobServiceAPI>(Helpers.Constants.BaseUrl);
            return await contosoMaintenanceApi.UpdateJob(job);
        }

        public async Task<List<Job>> SearchJobsAsync(string keyword)
        {
            var contosoMaintenanceApi = RestService.For<IJobServiceAPI>(Helpers.Constants.BaseUrl);
            return await contosoMaintenanceApi.SearchJobs(keyword);
        }
    }
}
