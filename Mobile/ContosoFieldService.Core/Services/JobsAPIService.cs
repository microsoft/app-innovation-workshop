using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContosoFieldService.Models;
using Refit;
using ContosoFieldService.Helpers;
using MonkeyCache.FileStore;
using Xamarin.Essentials;
using Microsoft.AppCenter.Crashes;

namespace ContosoFieldService.Services
{
    public interface IJobServiceAPI
    {
        [Get("/job/")]
        Task<List<Job>> GetJobs();

        [Get("/job/{id}/")]
        Task<Job> GetJobById(string id);

        [Get("/jobs?keyword={keyword}&suggestions={enableSuggestions}")]
        Task<List<Job>> SearchJobs(string keyword, bool enableSuggestions);

        [Post("/job/")]
        Task<Job> CreateJob([Body] Job job);

        [Delete("/job/{id}/")]
        Task<Job> DeleteJob(string id, [Header("Authorization")] string authorization);

        [Put("/job/{id}/")]
        Task<Job> UpdateJob(string id, [Body] Job job);
    }

    public class JobsAPIService : BaseAPIService
    {
        // Note: Usually, we would create only one instance of the IJobServiceAPI here and
        // Re-use it for every operation. For this demo, we can change the BaseUrl at runtime, so we
        // Need a way to create the api for every single call
        // IJobServiceAPI api = GetManagedApiService<IJobServiceAPI>();

        public JobsAPIService()
        {
            CacheKey = "Jobs";
        }

        public async Task<(ResponseCode code, List<Job> result)> GetJobsAsync(bool force = false)
        {
            // Handle online/offline scenario
            if (Connectivity.NetworkAccess != NetworkAccess.Internet && Barrel.Current.Exists(CacheKey))
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
                IJobServiceAPI api = GetManagedApiService<IJobServiceAPI>();

                // Use Polly to handle retrying
                var pollyResult = await Policy.ExecuteAndCaptureAsync(async () => await api.GetJobs());
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
            IJobServiceAPI api = GetManagedApiService<IJobServiceAPI>();

            var pollyResult = await Policy.ExecuteAndCaptureAsync(async () => await api.GetJobById(id));
            if (pollyResult.Result != null)
            {
                return (ResponseCode.Success, pollyResult.Result);
            }

            return (ResponseCode.Error, null);
        }

        public async Task<(ResponseCode code, List<Job> result)> SearchJobsAsync(string keyword, bool enableSuggestions = false)
        {
            // Create an instance of the Refit RestService for the job interface.
            IJobServiceAPI api = GetManagedApiService<IJobServiceAPI>();

            var pollyResult = await Policy.ExecuteAndCaptureAsync(async () => await api.SearchJobs(keyword, enableSuggestions));
            if (pollyResult.Result != null)
            {
                return (ResponseCode.Success, pollyResult.Result);
            }

            return (ResponseCode.Error, null);
        }

        public async Task<(ResponseCode code, Job result)> CreateJobAsync(Job job)
        {
            // Create an instance of the Refit RestService for the job interface.
            IJobServiceAPI api = GetManagedApiService<IJobServiceAPI>();

            var pollyResult = await Policy.ExecuteAndCaptureAsync(async () => await api.CreateJob(job));
            if (pollyResult.Result != null)
            {
                return (ResponseCode.Success, pollyResult.Result);
            }

            return (ResponseCode.Error, null);
        }

        public async Task<(ResponseCode code, Job result)> DeleteJobByIdAsync(string id)
        {
            // Create an instance of the Refit RestService for the job interface.
            IJobServiceAPI api = GetManagedApiService<IJobServiceAPI>();

            var pollyResult = await Policy.ExecuteAndCaptureAsync(async () => await api.DeleteJob(id, "Bearer " + AuthenticationService.AccessToken));
            if (pollyResult.Result != null)
            {
                return (ResponseCode.Success, pollyResult.Result);
            }

            return (ResponseCode.Error, null);
        }

        public async Task<(ResponseCode code, Job result)> UpdateJob(Job job)
        {
            // Create an instance of the Refit RestService for the job interface.
            IJobServiceAPI api = GetManagedApiService<IJobServiceAPI>();

            var results = await api.UpdateJob(job.Id, job);
            if (results != null)
            {
                return (ResponseCode.Success, results);
            }

            return (ResponseCode.Error, null);
        }
    }
}
