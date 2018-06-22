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
using Microsoft.AppCenter.Crashes;

namespace ContosoFieldService.Services
{
    public interface IJobServiceAPI
    {
        [Get("/job/")]
        Task<List<Job>> GetJobs([Header("Ocp-Apim-Subscription-Key")] string apiManagementKey);

        [Get("/job/{id}/")]
        Task<Job> GetJobById(string id, [Header("Ocp-Apim-Subscription-Key")] string apiManagementKey);

        [Get("/jobs?keyword={keyword}&suggestions={enableSuggestions}")]
        Task<List<Job>> SearchJobs(string keyword, bool enableSuggestions, [Header("Ocp-Apim-Subscription-Key")] string apiManagementKey);

        [Post("/job/")]
        Task<Job> CreateJob([Body] Job job, [Header("Ocp-Apim-Subscription-Key")] string apiManagementKey);

        [Delete("/job/{id}/")]
        Task<Job> DeleteJob(string id, [Header("Authorization")] string authorization, [Header("Ocp-Apim-Subscription-Key")] string apiManagementKey);

        [Put("/job/{id}/")]
        Task<Job> UpdateJob(string id, [Body] Job job, [Header("Ocp-Apim-Subscription-Key")] string apiManagementKey);
    }

    public class JobsAPIService : BaseAPIService
    {
        // Note: Usually, we would create only one instance of the IJobServiceAPI here and
        // Re-use it for every operation. For this demo, we can change the BaseUrl at runtime, so we
        // Need a way to create the api for every single call
        // IJobServiceAPI api = RestService.For<IJobServiceAPI>(Constants.BaseUrl);

        public JobsAPIService()
        {
            CacheKey = "Jobs";
        }

        public async Task<(ResponseCode code, List<Job> result)> GetJobsAsync(bool force = false)
        {
            // Handle online/offline scenario
            if (!CrossConnectivity.Current.IsConnected && Barrel.Current.Exists(CacheKey))
            {
                // If no connectivity, we'll return the cached jobs list.
                return (ResponseCode.NotConnected, Barrel.Current.Get<List<Job>>(CacheKey));
            }

            // If the data isn't too old, we'll go ahead and return it rather than call the backend again.
            if (!force && !Barrel.Current.IsExpired(CacheKey) && Barrel.Current.Exists(CacheKey))
            {
                var jobs = Barrel.Current.Get<IEnumerable<Job>>(CacheKey);
                return (ResponseCode.Success, jobs.ToList());
            }

            try
            {
                // Create an instance of the Refit RestService for the job interface.
                IJobServiceAPI api = RestService.For<IJobServiceAPI>(Constants.BaseUrl);

                // Use Polly to handle retrying
                var pollyResult = await Policy.ExecuteAndCaptureAsync(async () => await api.GetJobs(Constants.ApiManagementKey));
                if (pollyResult.Result != null)
                {
                    // Save jobs into the cache
                    Barrel.Current.Add(CacheKey, pollyResult.Result, TimeSpan.FromMinutes(5));
                    return (ResponseCode.Success, pollyResult.Result);
                }
            }
            catch (UriFormatException ex)
            {
                //Lets report this exception to App Center 
                Crashes.TrackError(ex);

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
            catch (Exception ex)
            {
                //Lets report this exception to App Center 
                Crashes.TrackError(ex);

                // Everything else
                return (ResponseCode.Error, null);
            }

            return (ResponseCode.Error, null);
        }

        public async Task<(ResponseCode code, Job result)> GetJobByIdAsync(string id)
        {
            // Create an instance of the Refit RestService for the job interface.
            IJobServiceAPI api = RestService.For<IJobServiceAPI>(Constants.BaseUrl);

            var pollyResult = await Policy.ExecuteAndCaptureAsync(async () => await api.GetJobById(id, Constants.ApiManagementKey));
            if (pollyResult.Result != null)
            {
                return (ResponseCode.Success, pollyResult.Result);
            }

            return (ResponseCode.Error, null);
        }

        public async Task<(ResponseCode code, List<Job> result)> SearchJobsAsync(string keyword, bool enableSuggestions = false)
        {
            // Create an instance of the Refit RestService for the job interface.
            IJobServiceAPI api = RestService.For<IJobServiceAPI>(Constants.BaseUrl);

            var pollyResult = await Policy.ExecuteAndCaptureAsync(async () => await api.SearchJobs(keyword, enableSuggestions, Constants.ApiManagementKey));
            if (pollyResult.Result != null)
            {
                return (ResponseCode.Success, pollyResult.Result);
            }

            return (ResponseCode.Error, null);
        }

        public async Task<(ResponseCode code, Job result)> CreateJobAsync(Job job)
        {
            // Create an instance of the Refit RestService for the job interface.
            IJobServiceAPI api = RestService.For<IJobServiceAPI>(Constants.BaseUrl);

            var pollyResult = await Policy.ExecuteAndCaptureAsync(async () => await api.CreateJob(job, Constants.ApiManagementKey));
            if (pollyResult.Result != null)
            {
                return (ResponseCode.Success, pollyResult.Result);
            }

            return (ResponseCode.Error, null);
        }

        public async Task<(ResponseCode code, Job result)> DeleteJobByIdAsync(string id)
        {
            // Create an instance of the Refit RestService for the job interface.
            IJobServiceAPI api = RestService.For<IJobServiceAPI>(Constants.BaseUrl);

            var pollyResult = await Policy.ExecuteAndCaptureAsync(async () => await api.DeleteJob(id, "Bearer " + AuthenticationService.AccessToken, Constants.ApiManagementKey));
            if (pollyResult.Result != null)
            {
                return (ResponseCode.Success, pollyResult.Result);
            }

            return (ResponseCode.Error, null);
        }

        public async Task<(ResponseCode code, Job result)> UpdateJob(Job job)
        {
            // Create an instance of the Refit RestService for the job interface.
            IJobServiceAPI api = RestService.For<IJobServiceAPI>(Constants.BaseUrl);

            var results = await api.UpdateJob(job.Id, job, Constants.ApiManagementKey);
            if (results != null)
            {
                return (ResponseCode.Success, results);
            }

            return (ResponseCode.Error, null);
        }
    }
}
