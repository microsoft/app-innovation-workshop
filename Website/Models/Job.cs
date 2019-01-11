using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContosoMaintenance.Web.Models
{
    public partial class Job
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
        public object Attachements { get; set; }

        [JsonProperty("address")]
        public Address Address { get; set; }

        [JsonProperty("assignedTo")]
        public Engineer AssignedTo { get; set; }

        [JsonProperty("dueDate")]
        public DateTimeOffset DueDate { get; set; }

        [JsonProperty("photos")]
        public object Photos { get; set; }

        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("createdAt")]
        public DateTimeOffset CreatedAt { get; set; }

        [JsonProperty("isDeleted")]
        public bool IsDeleted { get; set; }
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
