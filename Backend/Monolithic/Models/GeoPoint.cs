using System;
using Newtonsoft.Json;

namespace ContosoMaintenance.WebAPI.Models
{
    public class GeoPoint
    {
        [JsonProperty("longitude")]
        public double Longitude { get; set; }

        [JsonProperty("latitude")]
        public double Latitude { get; set; }

        /// <summary>
        /// This is here as a small hack due to ensure compatibilty between old data in our DB and new data. 
        /// </summary>
        /// <value>The coordinates.</value>
        double[] coordinates;
        [Obsolete("Coordinates is deprecated, please use the Longitude and Latitude properties instead.")]
        [JsonProperty("coordinates")]
        public double[] Coordinates
        {
            get
            {
                if (coordinates == null)
                    return new double[] { Longitude, Latitude };
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
