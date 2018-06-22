using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Xamarin.Forms;
using System.Text.RegularExpressions;
using System.Linq;
using ContosoFieldService.Helpers;

namespace ContosoFieldService.Models
{
    public class Job : BaseModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("details")]
        public string Details { get; set; }

        [JsonProperty("dueDate")]
        public DateTime DueDate { get; set; }

        [JsonProperty("type")]
        public JobType Type { get; set; }

        [JsonProperty("status")]
        public JobStatus Status { get; set; }

        [JsonProperty("address")]
        public Location Address { get; set; }

        [JsonProperty("photos")]
        public List<Photo> Photos { get; set; }

        [JsonIgnore]
        public FormattedString NameAsFormattedString
        {
            get
            {
                return this.ConvertNameToFormattedString("[", "]");
            }
        }

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

