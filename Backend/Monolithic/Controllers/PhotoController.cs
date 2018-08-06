using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ContosoMaintenance.WebAPI.Services.BlobStorage;
using ContosoMaintenance.WebAPI.Services.StorageQueue;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.WindowsAzure.Storage;
using Newtonsoft.Json;
using ContosoMaintenance.WebAPI.Models;
using ContosoMaintenance.WebAPI.Services;
using Microsoft.Extensions.Configuration;
using ContosoMaintenance.WebAPI.Helpers;

namespace ContosoMaintenance.WebAPI.Controllers
{
    [Route("/api/photo")]
    public class PhotoController : Controller
    {
        readonly IAzureBlobStorage blobStorage;
        readonly IAzureStorageQueue queue;
        readonly DocumentDBRepositoryBase<Job> jobRepository;

        public PhotoController(IConfiguration configuration, IAzureBlobStorage blobStorage, IAzureStorageQueue queue)
        {
            this.blobStorage = blobStorage;
            this.queue = queue;

            jobRepository = new DocumentDBRepositoryBase<Job>();
            jobRepository.Initialize(
                configuration["AzureCosmosDb:Endpoint"],
                configuration["AzureCosmosDb:Key"],
                Constants.DatabaseId);
        }

        /// <summary>
        /// Uploads a photo and adds it to a Job
        /// </summary>
        /// <returns>The updated Job with the photo attached to it</returns>
        /// <param name="jobId">Job ID.</param>
        /// <param name="file">File</param>
        [HttpPost("{jobId}")]
        public async Task<IActionResult> UploadPhoto(string jobId, IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Invalid file");

            var job = await jobRepository.GetItemAsync(jobId);
            if (job == null)
                return BadRequest("Can't find the job to attach the photo to");

            // Create Blob Name
            var photoId = Guid.NewGuid().ToString();
            var fileEnding = file.FileName.Substring(file.FileName.LastIndexOf('.'));
            var blobName = photoId + fileEnding;

            try
            {
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

                // Add the photo to Job
                if (job.Photos == null)
                    job.Photos = new List<Photo>();

                job.Photos.Add(photo);
                var updatedJob = await jobRepository.UpdateItemAsync(jobId, job);

                // Create a message on our queue for the Azure Function to process the image.
                string json = JsonConvert.SerializeObject(new Models.PhotoProcess() { PhotoId = photoId, BlobName = blobName, JobId = jobId }, Formatting.Indented);
                await queue.AddMessage(json);
            }
            catch (StorageException)
            {
                return StatusCode(500, "Uploading the file failed. Please check your Storage Configuration in the App Settings");
            }
            catch (ArgumentException)
            {
                // Appears if Azure Storage Queue is not configured correctly which happens during the workshop,
                // as Storage Queues appear at a later point.
            }

            // Return the updated Job
            return new ObjectResult(job);
        }
    }
}
