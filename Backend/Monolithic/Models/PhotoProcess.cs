using System;
using Newtonsoft.Json;

namespace ContosoMaintenance.WebAPI.Models
{
    public class PhotoProcess
    {
        [JsonProperty("jobId")]
        public string JobId { get; set; }

        [JsonProperty("photoId")]
        public string PhotoId { get; set; }
    }
}
