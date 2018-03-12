using System;
using Newtonsoft.Json;
using Microsoft.Azure.Documents.Spatial;

namespace ContosoMaintenance.WebAPI.Models
{
    public class Location : BaseModel
    {
        [JsonProperty("firstLineAddress")]
        public string FirstLineAddress { get; set; }

        [JsonProperty("secondLineAddress")]
        public string SecondLineAddress { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("zipCode")]
        public string ZipCode { get; set; }

        [JsonProperty("point")]
        public Point Point { get; set; }
    }
}
