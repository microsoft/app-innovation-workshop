using System.Collections.Generic;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Azure.WebJobs.Extensions.CosmosDB;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System;
using System.Linq;
using Newtonsoft.Json;
using Microsoft.Azure.Documents;

namespace ContosoMaintenance.Functions
{
    public static class ResizeImage
    {
        static readonly HttpClient httpClient = new HttpClient();

        [FunctionName("ProcessPhotosQueue")]
        public static async Task Run(
            // Trigger
            [QueueTrigger("processphotos")] PhotoProcess queueItem,

            // Inputs
            [CosmosDB("contosomaintenance", "jobs", Id = "{jobId}", ConnectionStringSetting = "CosmosDb")] Job job,
            [Blob("images-large/{blobName}", FileAccess.Read)] byte[] imageLarge,

            // Outputs
            [Blob("images-medium/{blobName}", FileAccess.Write)] Stream imageMedium,
            [Blob("images-icon/{blobName}", FileAccess.Write)] Stream imageIcon,

            // Logger
            TraceWriter log)
        {
            log.Info($"New photo upload '{queueItem.PhotoId}' detected for job '{job.Id}'");

            // Crop photos to medium and icon sizes using Microsoft Cognitive Services
            await CropImageSmartAsync(imageLarge, imageMedium, 300, 300);
            await CropImageSmartAsync(imageLarge, imageIcon, 150, 150);
            log.Info("Images cropped");

            // Update Cosmos DB entry
            var photo = job.Photos.FirstOrDefault(p => p.Id.Equals(queueItem.PhotoId));
            if (photo != null)
            {
                photo.MediumUrl = photo.LargeUrl?.Replace("large", "medium");
                photo.IconUrl = photo.LargeUrl?.Replace("large", "icon");
                log.Info("Cosmos DB entry updated");
            }
        }

        /// <summary>
        /// Crops an image to a specific size using Microsoft Cognitive Services
        /// </summary>
        /// <returns>The image smart async.</returns>
        /// <param name="inputImage">Input image.</param>
        /// <param name="outputImage">Output image stream.</param>
        /// <param name="width">Targeted image width.</param>
        /// <param name="height">Targeted image height.</param>
        private static async Task CropImageSmartAsync(byte[] inputImage, Stream outputImage, int width, int height)
        {
            // Add Microsoft Azure Cognitive Service Token to HttpClient header
            httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", Environment.GetEnvironmentVariable("CognitiveServicesKey"));

            // Create Cognitive Service request url with parameters
            var url = $"{Environment.GetEnvironmentVariable("CognitiveServicesEndpoint")}vision/v1.0/generateThumbnail?width={width}&height={height}&smartCropping=true";

            using (ByteArrayContent content = new ByteArrayContent(inputImage))
            {
                // Send request
                content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                var response = await httpClient.PostAsync(url, content);

                // Write cropped image to output stream
                var resizedImage = await response.Content.ReadAsStreamAsync();
                resizedImage.CopyTo(outputImage);
            }
        }
    }

    public class PhotoProcess
    {
        [JsonProperty("jobId")]
        public string JobId { get; set; }
        [JsonProperty("photoId")]
        public string PhotoId { get; set; }
        [JsonProperty("blobName")]
        public string BlobName { get; set; }
    }

    public class Job
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("createdAt")]
        public DateTime CreatedAt { get; set; }
        public string Name { get; set; }
        [JsonProperty("isDeleted")]
        public bool IsDeleted { get; set; }
        public string Details { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public string[] Attachements { get; set; }
        public dynamic Address { get; set; }
        public dynamic AssignedTo { get; set; }
        [JsonProperty("photos")]
        public List<Photo> Photos { get; set; }
    }

    public class Photo
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("largeUrl")]
        public string LargeUrl { get; set; }
        [JsonProperty("mediumUrl")]
        public string MediumUrl { get; set; }
        [JsonProperty("iconUrl")]
        public string IconUrl { get; set; }
    }
}
