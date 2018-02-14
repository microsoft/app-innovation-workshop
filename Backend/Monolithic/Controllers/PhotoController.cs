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

        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest();

            try
            {
                //The FileName is actually going to be the JobID as we set this on the mobile device.
                var jobID = file.FileName.Split(".")[0]; //Not going to lie. This whole queue code is horrible, but it works, so although I'm saying I'll fix it. I probably wont.

                var blobName = Guid.NewGuid().ToString();
                var fileStream = file.OpenReadStream();
                blobName = string.Format($"{blobName}");
                await blobStorage.UploadAsync(blobName, fileStream);

                //Create a message on our queue for the Azure Function to process the image.
                try
                {
                    string json = JsonConvert.SerializeObject(new Models.PhotoProcess() { PhotoId = blobName, JobId = jobID }, Formatting.Indented);
                    await queue.AddMessage(json);
                }
                catch (ArgumentException)
                {
                    // Appears if Azure Storage Queue is not configured correctly which happens during the workshop,
                    // as Storage Queues appear at a later point.
                }

                return new ObjectResult(true);
            }
            catch
            {
                return new ObjectResult(false);
            }
        }
    }
}
