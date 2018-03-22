using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using ContosoFieldService.Models;
using Plugin.Connectivity;
using Polly;
using Refit;
using ContosoFieldService.Helpers;
using MonkeyCache.FileStore;

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
        Task<Job> DeleteJob(string id, [Header("Authorization")] string authorization, [Header("Ocp-Apim-Subscription-Key")] string apiManagementKey);

        [Put("/job/{id}/")]
        Task<Job> UpdateJob(string id, [Body] Job job, [Header("Ocp-Apim-Subscription-Key")] string apiManagementKey);
    }

    public class JobsAPIService : BaseAPIService
    {
        public async Task<List<Job>> GetJobsAsync()
        {
            var key = "Jobs";

            // Handle online/offline scenario
            if (!CrossConnectivity.Current.IsConnected && Barrel.Current.Exists(key))
            {
                // If no connectivity, we'll return the cached jobs list.
                return Barrel.Current.Get<List<Job>>(key);
            }

            // ----
            // TODO: THERE IS A BUG WITH GEOSPARTIAL DATA AT THE MOMENT
            // ----
            // If the data isn't too old, we'll go ahead and return it rather than call the backend again.
            if (!Barrel.Current.IsExpired(key) && Barrel.Current.Exists(key))
            {
                var jobs = Barrel.Current.Get<IEnumerable<Job>>(key);
                return jobs.ToList();
            }

            // Create an instance of the Refit RestService for the job interface.
            var contosoMaintenanceApi = RestService.For<IJobServiceAPI>(Helpers.Constants.BaseUrl);

            // Use Polly to handle retrying
            var pollyResult = await Policy.ExecuteAndCaptureAsync(async () => await contosoMaintenanceApi.GetJobs(Constants.ApiManagementKey));
            if (pollyResult.Result != null)
            {
                // Save jobs into the cache
                Barrel.Current.Add(key, pollyResult.Result, TimeSpan.FromMinutes(5));
                return pollyResult.Result;
            }

            return null;
        }

        public async Task<Job> GetJobByIdAsync(string id)
        {
            var contosoMaintenanceApi = RestService.For<IJobServiceAPI>(Constants.BaseUrl);
            var pollyResult = await Policy.ExecuteAndCaptureAsync(async () => await contosoMaintenanceApi.GetJobById(id, Constants.ApiManagementKey));
            if (pollyResult.Result != null)
            {
                return pollyResult.Result;
            }

            return null;
        }

        public async Task<List<Job>> SearchJobsAsync(string keyword)
        {
            var contosoMaintenanceApi = RestService.For<IJobServiceAPI>(Constants.BaseUrl);
            var pollyResult = await Policy.ExecuteAndCaptureAsync(async () => await contosoMaintenanceApi.SearchJobs(keyword, Constants.ApiManagementKey));
            if (pollyResult.Result != null)
            {
                return pollyResult.Result;
            }

            return null;
        }

        public async Task<Job> CreateJobAsync(Job job)
        {
            var contosoMaintenanceApi = RestService.For<IJobServiceAPI>(Constants.BaseUrl);
            var pollyResult = await Policy.ExecuteAndCaptureAsync(async () => await contosoMaintenanceApi.CreateJob(job, Constants.ApiManagementKey));
            if (pollyResult.Result != null)
            {
                return pollyResult.Result;
            }

            return null;
        }

        public async Task<Job> DeleteJobByIdAsync(string id)
        {
            var contosoMaintenanceApi = RestService.For<IJobServiceAPI>(Constants.BaseUrl);
            var pollyResult = await Policy.ExecuteAndCaptureAsync(async () => await contosoMaintenanceApi.DeleteJob(id, "Bearer " + AuthenticationService.AccessToken, Constants.ApiManagementKey));
            if (pollyResult.Result != null)
            {
                return pollyResult.Result;
            }

            return null;
        }

        public async Task<Job> UpdateJob(Job job)
        {
            var contosoMaintenanceApi = RestService.For<IJobServiceAPI>(Helpers.Constants.BaseUrl);
            return await contosoMaintenanceApi.UpdateJob(job.Id, job, Constants.ApiManagementKey);
        }
    }
}
