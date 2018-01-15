using System.Collections.Generic;
using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ContosoMaintenance.WebAPI.Models
{
    [SerializePropertyNamesAsCamelCase]
    public class Job : BaseModel
    {
        [IsSearchable]
        public string Name { get; set; }

        public string Details { get; set; }

        [IsFilterable, IsFacetable]
        public JobType Type { get; set; }

        [IsFilterable, IsSortable]
        public JobStatus Status { get; set; }

        public Customer Customer { get; set; }

        public string[] Attachements { get; set; }

        public Location Address { get; set; }

        public Employee AssignedTo { get; set; }

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
