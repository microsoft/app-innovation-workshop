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
        // Create an instance of the Refit RestService for the job interface.
        readonly IJobServiceAPI contosoMaintenanceApi = RestService.For<IJobServiceAPI>(Constants.BaseUrl);

        public async Task<(ResponseType type, List<Job> result)> GetJobsAsync(bool force = false)
        {
            var key = "Jobs";

            // Handle online/offline scenario
            if (!CrossConnectivity.Current.IsConnected && Barrel.Current.Exists(key))
            {
                // If no connectivity, we'll return the cached jobs list.
                return (ResponseType.NotConnected, Barrel.Current.Get<List<Job>>(key));
            }

            // If the data isn't too old, we'll go ahead and return it rather than call the backend again.
            if (!force && !Barrel.Current.IsExpired(key) && Barrel.Current.Exists(key))
            {
                var jobs = Barrel.Current.Get<IEnumerable<Job>>(key);
                return (ResponseType.Success, jobs.ToList());
            }

            // Use Polly to handle retrying
            var pollyResult = await Policy.ExecuteAndCaptureAsync(async () => await contosoMaintenanceApi.GetJobs(Constants.ApiManagementKey));
            if (pollyResult.Result != null)
            {
                // Save jobs into the cache
                Barrel.Current.Add(key, pollyResult.Result, TimeSpan.FromMinutes(5));
                return (ResponseType.Success, pollyResult.Result);
            }

            return (ResponseType.Error, null);
        }

        public async Task<Job> GetJobByIdAsync(string id)
        {
            var pollyResult = await Policy.ExecuteAndCaptureAsync(async () => await contosoMaintenanceApi.GetJobById(id, Constants.ApiManagementKey));
            if (pollyResult.Result != null)
            {
                return pollyResult.Result;
            }

            return null;
        }

        public async Task<List<Job>> SearchJobsAsync(string keyword)
        {
            var pollyResult = await Policy.ExecuteAndCaptureAsync(async () => await contosoMaintenanceApi.SearchJobs(keyword, Constants.ApiManagementKey));
            if (pollyResult.Result != null)
            {
                return pollyResult.Result;
            }

            return null;

        }

        public async Task<Job> CreateJobAsync(Job job)
        {
            var pollyResult = await Policy.ExecuteAndCaptureAsync(async () => await contosoMaintenanceApi.CreateJob(job, Constants.ApiManagementKey));
            if (pollyResult.Result != null)
            {
                return pollyResult.Result;
            }

            return null;
        }

        public async Task<Job> DeleteJobByIdAsync(string id)
        {
            var pollyResult = await Policy.ExecuteAndCaptureAsync(async () => await contosoMaintenanceApi.DeleteJob(id, "Bearer " + AuthenticationService.AccessToken, Constants.ApiManagementKey));
            if (pollyResult.Result != null)
            {
                return pollyResult.Result;
            }

            return null;
        }

        public async Task<Job> UpdateJob(Job job)
        {
            return await contosoMaintenanceApi.UpdateJob(job.Id, job, Constants.ApiManagementKey);
        }
    }
}
