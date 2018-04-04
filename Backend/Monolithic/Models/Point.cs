using System;
using Newtonsoft.Json;

namespace ContosoMaintenance.WebAPI.Models
{
    /// <summary>
    /// Point class to represent Geo Coordinates in the GeoJASON format (https://tools.ietf.org/html/rfc7946)
    /// Cosmos DB needs this format for geospacial requests (https://docs.microsoft.com/en-us/azure/cosmos-db/geospatial)
    /// </summary>
    public class Point
    {
        [JsonIgnore]
        public double Latitude => Coordinates[0];

        [JsonIgnore]
        public double Longitude => Coordinates[1];

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("coordinates")]
        public double[] Coordinates { get; set; }

        public Point(double latitude, double longitude)
        {
            Type = "Point";
            Coordinates = new double[] { latitude, longitude };
        }
    }
}
