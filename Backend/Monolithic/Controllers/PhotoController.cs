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
                var blobName = Guid.NewGuid().ToString();
                var fileStream = file.OpenReadStream();
                blobName = string.Format($"{blobName}");
                await blobStorage.UploadAsync(blobName, fileStream);

                //Create a message on our queue for the Azure Function to process the image.
                try
                {
                    string json = JsonConvert.SerializeObject(new Models.PhotoProcess() { PhotoId = blobName, JobId = jobId }, Formatting.Indented);
                    await queue.AddMessage(json);
                }
                catch (ArgumentException)
                {
                    // Appears if Azure Storage Queue is not configured correctly which happens during the workshop,
                    // as Storage Queues appear at a later point.
                }

                var photo = new Photo
                {
                    Id = blobName,
                    LargeUrl = ""
                };

                // SHOULD RETURN PATH TO UPLOADED IMAGE
                return new ObjectResult(true);
            }
            catch
            {
                return new ObjectResult(false);
            }
        }
    }
}
