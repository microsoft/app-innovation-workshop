using System.Threading.Tasks;
using ContosoFieldService.Models;
using Plugin.Media.Abstractions;
using Refit;
using ContosoFieldService.Helpers;

namespace ContosoFieldService.Services
{
    public interface IPhotoServiceAPI
    {
        [Multipart]
        [Post("/photo/{jobId}/")]
        Task<Job> UploadPhoto(string jobId, [AliasAs("file")] StreamPart stream, [Header("Ocp-Apim-Subscription-Key")] string apiManagementKey);
    }

    public class PhotoAPIService
    {
        public async Task<Job> UploadPhotoAsync(string jobId, MediaFile file)
        {
            IPhotoServiceAPI api = RestService.For<IPhotoServiceAPI>(Constants.BaseUrl);

            var streamPart = new StreamPart(file.GetStream(), "photo.jpg", "image/jpeg");
            var updatedJob = await api.UploadPhoto(jobId, streamPart, Constants.ApiManagementKey);
            return updatedJob;
        }
    }
}
