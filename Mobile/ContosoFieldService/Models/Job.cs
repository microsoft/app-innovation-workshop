using System;
namespace ContosoFieldService.Models
{
    public class Job
    {
        public string Name { get; set; }
        public string Details { get; set; }
        public JobType Type { get; set; }
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

