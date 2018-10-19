using System.Threading.Tasks;
using ContosoFieldService.Models;
using Plugin.Media.Abstractions;
using Refit;

namespace ContosoFieldService.Services
{
    public interface IPhotoServiceAPI
    {
        [Multipart]
        [Post("/photo/{jobId}/")]
        Task<Job> UploadPhoto(string jobId, [AliasAs("file")] StreamPart stream);
    }

    public class PhotoAPIService : BaseAPIService
    {
        public async Task<Job> UploadPhotoAsync(string jobId, MediaFile file)
        {
            IPhotoServiceAPI api = GetManagedApiService<IPhotoServiceAPI>();

            var streamPart = new StreamPart(file.GetStream(), "photo.jpg", "image/jpeg");
            var updatedJob = await api.UploadPhoto(jobId, streamPart);
            return updatedJob;
        }
    }
}
