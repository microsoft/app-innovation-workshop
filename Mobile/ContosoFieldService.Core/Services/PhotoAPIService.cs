using System.Threading.Tasks;
using Plugin.Media.Abstractions;
using Refit;

namespace ContosoFieldService.Services
{
    [Headers(Helpers.Constants.ApiManagementKey)]
    public interface IPhotoServiceAPI
    {
        [Multipart]
        [Post("/photo/{jobId}/")]
        Task UploadPhoto(string jobId, [AliasAs("file")] StreamPart stream);
    }

    public class PhotoAPIService
    {
        public async Task<MediaFile> UploadPhotoAsync(string jobId, MediaFile file)
        {
            var restService = RestService.For<IPhotoServiceAPI>(Helpers.Constants.BaseUrl);

            var streamPart = new StreamPart(file.GetStream(), $"{System.Guid.NewGuid().ToString()}.jpg", "image/jpeg");
            await restService.UploadPhoto(jobId, streamPart);

            return file;
        }
    }
}
