using System;
using Newtonsoft.Json;

namespace ContosoFieldService.Models
{
    public class GeoPoint
    {
        [JsonProperty("longitude")]
        public double Longitude { get; set; }

        [JsonProperty("latitude")]
        public double Latitude { get; set; }


        double[] coordinates;

        /// <summary>
        /// This is here as a small hack due to ensure compatibilty between old data in our DB and new data. 
        /// </summary>
        /// <value>The coordinates.</value>
        [Obsolete("Coordinates is deprecated, please use the Longitude and Latitude properties instead.")]
        [JsonProperty("coordinates")] 
        public double[] Coordinates
        {
            get
            {
                return coordinates;
            }
            set
            {
                coordinates = value;
                Latitude = coordinates[0];
                Longitude = coordinates[1];
            }
        }

    }
}
