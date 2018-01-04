using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ContosoMaintenance.WebAPI.Models
{
    public class Job : BaseModel
    {
        [JsonProperty ("name")]
        public string Name { get; set; }

        [JsonProperty ("details")]
        public string Details { get; set; }

        [JsonProperty("type")]
        public JobType Type { get; set; }

        [JsonProperty("status")]
        public JobStatus Status { get; set; }

        [JsonProperty("customer")]
        public User Customer { get; set; }

    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum JobType
    {
        Installation,
        Repair,
        Service
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum JobStatus
    {
        Waiting,
        InProgress,
        Complete
    }
}
