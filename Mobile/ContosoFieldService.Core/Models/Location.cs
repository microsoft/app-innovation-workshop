using System;
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
        public GeographyPoint Point { get; set; }
    }

    public class GeographyPoint 
    {
        [JsonProperty("latitude")]
        public double Latitude { get; set; }

        [JsonProperty("longitude")]
        public double Longitude { get; set; }

    }
}
