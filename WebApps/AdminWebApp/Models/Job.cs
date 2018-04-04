using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ContosoMaintenance.AdminWebApp.Models
{
    public class Job : BaseModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("details")]
        public string Details { get; set; }

        [JsonProperty("type")]
        public JobType Type { get; set; }

        [JsonProperty("status")]
        public JobStatus Status { get; set; }

        [JsonProperty("attachements")]
        public string[] Attachements { get; set; }

        [JsonProperty("assignedTo")]
        public Employee AssignedTo { get; set; }

        [JsonProperty("dueDate")]
        public DateTime DueDate { get; set; }

        [JsonProperty("photos")]
        public List<Photo> Photos { get; set; }
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
