using Newtonsoft.Json;

namespace ContosoFieldService.Models
{
    public class Job
    {
        [JsonProperty ("name")]
        public string Name { get; set; }

        [JsonProperty("details")]
        public string Details { get; set; }



        [JsonProperty("type")]
        public JobType Type { get; set; }

        [JsonProperty("status")]
        public JobStatus Status { get; set; }
    }

    public enum JobType
    {
        Installation,
        Repair,
        Service
    }

    public enum JobStatus
    {
        Waiting,
        InProgress,
        Complete
    }
}

