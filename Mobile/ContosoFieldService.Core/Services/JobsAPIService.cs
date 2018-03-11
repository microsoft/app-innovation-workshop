using System.Collections.Generic;
using System.Threading.Tasks;
using ContosoFieldService.Models;
using Refit;
using ContosoFieldService.Helpers;

namespace ContosoFieldService.Services
{
    public interface IJobServiceAPI
    {
        [Get("/job/")]
        Task<List<Job>> GetJobs([Header("Ocp-Apim-Subscription-Key")] string apiManagementKey);

        [Get("/job/{id}/")]
        Task<Job> GetJobById(string id, [Header("Ocp-Apim-Subscription-Key")] string apiManagementKey);

        [Get("/search/jobs/?keyword={keyword}")]
        Task<List<Job>> SearchJobs(string keyword, [Header("Ocp-Apim-Subscription-Key")] string apiManagementKey);

        [Post("/job/")]
        Task<Job> CreateJob([Body] Job job, [Header("Ocp-Apim-Subscription-Key")] string apiManagementKey);

        [Delete("/job/{id}/")]
        Task<Job> DeleteJob(string id, [Header("Ocp-Apim-Subscription-Key")] string apiManagementKey);

        [Put("/job/{id}/")]
        Task<Job> UpdateJob(string id, [Body] Job job, [Header("Ocp-Apim-Subscription-Key")] string apiManagementKey);
    }

    public class JobsAPIService
    {
        public async Task<Job> CreateJobAsync(Job job)
        {
            var contosoMaintenanceApi = RestService.For<IJobServiceAPI>(Helpers.Constants.BaseUrl);
            return await contosoMaintenanceApi.CreateJob(job, Constants.ApiManagementKey);
        }

        public async Task<List<Job>> GetJobsAsync()
        {
            var contosoMaintenanceApi = RestService.For<IJobServiceAPI>(Helpers.Constants.BaseUrl);
            return await contosoMaintenanceApi.GetJobs(Constants.ApiManagementKey);
        }

        public async Task<Job> GetJobByIdAsync(string id)
        {
            var contosoMaintenanceApi = RestService.For<IJobServiceAPI>(Helpers.Constants.BaseUrl);
            return await contosoMaintenanceApi.GetJobById(id, Constants.ApiManagementKey);
        }

        public async Task<Job> DeleteJobByIdAsync(string id)
        {
            var contosoMaintenanceApi = RestService.For<IJobServiceAPI>(Helpers.Constants.BaseUrl);
            return await contosoMaintenanceApi.DeleteJob(id, Constants.ApiManagementKey);
        }

        public async Task<Job> UpdateJob(Job job)
        {
            var contosoMaintenanceApi = RestService.For<IJobServiceAPI>(Helpers.Constants.BaseUrl);
            return await contosoMaintenanceApi.UpdateJob(job.Id, job, Constants.ApiManagementKey);
        }

        public async Task<List<Job>> SearchJobsAsync(string keyword)
        {
            var contosoMaintenanceApi = RestService.For<IJobServiceAPI>(Helpers.Constants.BaseUrl);
            return await contosoMaintenanceApi.SearchJobs(keyword, Constants.ApiManagementKey);
        }
    }
}
