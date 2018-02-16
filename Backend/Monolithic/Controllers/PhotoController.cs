using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using ContosoMaintenance.WebAPI.Services.BlobStorage;
using ContosoMaintenance.WebAPI.Services.StorageQueue;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Queue;
using Microsoft.Azure;
using Newtonsoft.Json;
using ContosoMaintenance.WebAPI.Models;

namespace ContosoMaintenance.WebAPI.Controllers
{
    [Route("/api/photo")]
    public class PhotoController : Controller
    {
        readonly IAzureBlobStorage blobStorage;
        readonly IAzureStorageQueue queue;

        public PhotoController(IAzureBlobStorage blobStorage, IAzureStorageQueue queue)
        {
            this.blobStorage = blobStorage;
            this.queue = queue;
        }

        [HttpPost("{jobId}")]
        public async Task<IActionResult> UploadPhoto(string jobId, IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Invalid file");

            try
            {
                // Create Blob
                var photoId = Guid.NewGuid().ToString();
                var fileEnding = file.FileName.Substring(file.FileName.LastIndexOf('.'));
                var blobName = photoId + fileEnding;

                // Upload photo to blob
                var uri = await blobStorage.UploadAsync(string.Format($"{blobName}"), file.OpenReadStream());

                // Generate photo object
                var photo = new Photo
                {
                    Id = photoId,
                    LargeUrl = uri.ToString(),
                    MediumUrl = uri.ToString(),
                    IconUrl = uri.ToString(),
                };

                try
                {
                    // Create a message on our queue for the Azure Function to process the image.
                    string json = JsonConvert.SerializeObject(new Models.PhotoProcess() { PhotoId = blobName, JobId = jobId }, Formatting.Indented);
                    await queue.AddMessage(json);
                }
                catch (ArgumentException)
                {
                    // Appears if Azure Storage Queue is not configured correctly which happens during the workshop,
                    // as Storage Queues appear at a later point.
                }

                // Return photo object
                return new ObjectResult(photo);
            }
            catch
            {
                return new ObjectResult(false);
            }
        }
    }
}
