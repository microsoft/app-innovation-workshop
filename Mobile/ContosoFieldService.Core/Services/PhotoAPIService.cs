using System.Collections.Generic;
using System.Threading.Tasks;
using ContosoFieldService.Models;
using Plugin.Media.Abstractions;
using Refit;

namespace ContosoFieldService.Services
{
    [Headers(Helpers.Constants.ApiManagementKey)]
    public interface IPhotoServiceAPI
    {
        [Get("/photo/")]
        Task<List<Job>> GetJobs();

        [Get("/photo/{id}/")]
        Task<Job> GetPhotoById(string id);

        [Post("/photo/")]
        Task<Job> CreatePhoto([Body] Job job);

        [Post("/photo/{id}/")]
        Task<Job> DeletePhoto(string id);

        [Patch("/photo/")]
        Task<Job> UpdatePhoto([Body] Job job);
    }

    public class PhotoAPIService
    {
        public async Task<Job> CreatePhotoAsync(MediaFile file)
        {
            //TODO COme back and continue coding 
            var contosoMaintenanceApi = RestService.For<IPhotoServiceAPI>(Helpers.Constants.BaseUrl);
            return null;
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
