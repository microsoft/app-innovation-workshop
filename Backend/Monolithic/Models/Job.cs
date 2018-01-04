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
        public Customer Customer { get; set; }

        [JsonProperty("attachementUrls")]
        public string[] Attachements { get; set; }

        [JsonProperty("address")]
        public Location Address { get; set; }
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
