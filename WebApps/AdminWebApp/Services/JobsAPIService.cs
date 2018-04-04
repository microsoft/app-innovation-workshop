using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;
using Refit;

using ContosoMaintenance.AdminWebApp.Models;

namespace ContosoMaintenance.AdminWebApp.Services
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
        private IConfiguration configuration;

        public JobsAPIService(IConfiguration configuration) 
        {
            this.configuration = configuration;
        }

        public async Task<List<Job>> GetJobsAsync()
        {
            // Create an instance of the Refit RestService for the job interface.
            var contosoMaintenanceApi = RestService.For<IJobServiceAPI>(configuration["Api:BaseUrl"]);

            // Use Polly to handle retrying
            var result = await contosoMaintenanceApi.GetJobs(configuration["Api:ApiManagementKey"]);
            if (result != null)
                return result;
            else
                return null;
        }

        public async Task<Job> GetJobByIdAsync(string id)
        {
            var contosoMaintenanceApi = RestService.For<IJobServiceAPI>(configuration["Api:BaseUrl"]);
            var result = await contosoMaintenanceApi.GetJobById(id, configuration["Api:ApiManagementKey"]);
            if (result != null)
                return result;
            else
                return null;
        }

        public async Task<List<Job>> SearchJobsAsync(string keyword)
        {
            var contosoMaintenanceApi = RestService.For<IJobServiceAPI>(configuration["Api:BaseUrl"]);
            var result = await contosoMaintenanceApi.SearchJobs(keyword, configuration["Api:ApiManagementKey"]);
            if (result != null)
                return result;
            else
                return null;

        }

        public async Task<Job> CreateJobAsync(Job job)
        {
            var contosoMaintenanceApi = RestService.For<IJobServiceAPI>(configuration["Api:BaseUrl"]);
            var result = await contosoMaintenanceApi.CreateJob(job, configuration["Api:ApiManagementKey"]);
            if (result != null)
                return result;
            else
                return null;
        }

        public async Task<Job> DeleteJobByIdAsync(string id)
        {
            var contosoMaintenanceApi = RestService.For<IJobServiceAPI>(configuration["Api:BaseUrl"]);
            var result = await contosoMaintenanceApi.DeleteJob(id, configuration["Api:ApiManagementKey"]);
            if (result != null)
                return result;
            else
                return null;
        }

        public async Task<Job> UpdateJob(Job job)
        {
            var contosoMaintenanceApi = RestService.For<IJobServiceAPI>(configuration["Api:BaseUrl"]);
            return await contosoMaintenanceApi.UpdateJob(job.Id, job, configuration["Api:ApiManagementKey"]);
        }
    }
}
