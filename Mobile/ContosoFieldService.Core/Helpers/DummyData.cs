using System;
using System.Collections.ObjectModel;
using ContosoFieldService.Models;
using MvvmHelpers;
using ContosoFieldService.PageModels;
using System.Linq;

namespace ContosoFieldService.Helpers
{
    public static class DummyData
    {
        public static ObservableRangeCollection<Job> GetDummyJobs()
        {
            var jobs = new ObservableRangeCollection<Job>
            {
                new Job
                {
                    Name = "Lorem Ipsum dolor 1",
                    Details="Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.",
                    Status = JobStatus.Waiting
                },
                new Job
                {
                    Name = "Lorem Ipsum dolor 2",
                    Details="Nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.",
                    Status = JobStatus.Complete
                },
                new Job
                {
                    Name = "Lorem Ipsum dolor 3",
                    Details="Minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.",
                    Status = JobStatus.Complete
                }
            };

            return jobs;
        }

        internal static ObservableRangeCollection<GroupedJobs> GetGroupedDummyJobs()
        {
            var jobs = GetDummyJobs();

            return new ObservableRangeCollection<GroupedJobs>
            {
                new GroupedJobs("Waiting", jobs.Where(x => x.Status == JobStatus.Waiting)),
                new GroupedJobs("Waiting", jobs.Where(x => x.Status == JobStatus.Waiting)),
                new GroupedJobs("Waiting", jobs.Where(x => x.Status == JobStatus.Waiting)),
            };
        }
    }
}
