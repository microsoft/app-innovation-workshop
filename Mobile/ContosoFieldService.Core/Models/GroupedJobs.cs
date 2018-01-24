using System.Collections.Generic;
using MvvmHelpers;
using System.Linq;

namespace ContosoFieldService.Models
{
    public class GroupedJobs : ObservableRangeCollection<Job>
    {
        public string Heading { get; set; }
        public ObservableRangeCollection<Job> Jobs => this;

        public GroupedJobs(string heading, IEnumerable<Job> collection) : base(collection)
        {
            Heading = heading;
        }
    }
}
