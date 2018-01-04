using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ContosoMaintenance.WebAPI.Models
{
    public class Job
    {

        [JsonProperty ("name")]
        public string Name { get; set; }

        [JsonProperty ("details")]
        public string Details { get; set; }

        [JsonProperty("type")]
        public JobType Type { get; set; }


    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum JobType
    {
        Installation,
        Repair,
        Service
    }
}
