using System.Collections.Generic;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
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
        [FunctionName("ProcessPhotosQueue")]
        public static async Task Run(
            // Trigger
            [QueueTrigger("processphotos")] PhotoProcess queueItem,

            // Inputs
            [DocumentDB("contosomaintenance", "jobs", Id = "{jobId}", ConnectionStringSetting = "CosmosDb")] Job job,
            [Blob("images-large/{photoId}.jpg", FileAccess.Read)] byte[] imageLarge,

            // Outputs
            [Blob("images-medium/{photoId}.jpg", FileAccess.Write)] Stream imageMedium,
            [Blob("images-icon/{photoId}.jpg", FileAccess.Write)] Stream imageIcon,
            
            // Logger
            TraceWriter log)
        {
            log.Info($"New photo upload detected for job {job.Id}");

            // Crop photos smart using Microsoft Cognitive Services
            await CropImageSmartAsync(imageLarge, imageMedium, 300, 300);
            await CropImageSmartAsync(imageLarge, imageIcon, 150, 150);
            log.Info("Images cropped");

            // Update Cosmos DB entry
            var photo = (job.Photos).FirstOrDefault(p => p.Id.Equals(queueItem.PhotoId));
            if (photo != null)
            {
                photo.MediumUrl = photo.LargeUrl?.Replace("large", "medium");
                photo.IconUrl = photo.LargeUrl?.Replace("large", "icon");
                log.Info("Cosmos DB entry updated");
            }            
        }

        private static async Task CropImageSmartAsync(byte[] inputImage, Stream outputImage, int width, int height)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", Environment.GetEnvironmentVariable("Ocp-Apim-Subscription-Key"));
            var url =$"https://northeurope.api.cognitive.microsoft.com/vision/v1.0/generateThumbnail?width={width}&height={height}&smartCropping=true";
            using (ByteArrayContent content = new ByteArrayContent(inputImage))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                HttpResponseMessage response = await client.PostAsync(url, content);
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
    }

    public class Job
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("createdAt")]
        public DateTime CreatedAt { get; set; }
        public string Name { get; set; }

        public string Details { get; set; }
        public string Type { get; set; }

        public string Status { get; set; }

        public dynamic Customer { get; set; }

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
