using System;
using Microsoft.Azure.Documents.Spatial;
using Newtonsoft.Json;

namespace ContosoFieldService.Models
{
    public class Location
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
