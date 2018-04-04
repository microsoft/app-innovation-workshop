using System;
using Newtonsoft.Json;

namespace ContosoMaintenance.WebAPI.Models
{
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
