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
        [Multipart]
        [Post("/photo/")]
        Task UploadPhoto([AliasAs("file")] StreamPart stream);
    }

    public class PhotoAPIService
    {
        public async Task<MediaFile> CreatePhotoAsync(MediaFile file)
        {
            var restService = RestService.For<IPhotoServiceAPI>(Helpers.Constants.BaseUrl);

            var streamPart = new StreamPart(file.GetStream(), $"{System.Guid.NewGuid().ToString()}.jpg", "image/jpeg");
            await restService.UploadPhoto(streamPart);

            return file;
        }
    }
}
