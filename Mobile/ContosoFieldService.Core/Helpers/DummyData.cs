using System;
using System.Collections.ObjectModel;
using ContosoFieldService.Models;
using MvvmHelpers;
using ContosoFieldService.ViewModels;
using System.Linq;
using System.Collections.Generic;

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
                    Status = JobStatus.Waiting,
                    Photos = new List<Photo>
                    {
                        new Photo { IconUrl = "myface.jpg" },
                        new Photo { IconUrl = "myface.jpg" },
                        new Photo { IconUrl = "myface.jpg" }
                    }
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

        public static ObservableRangeCollection<Part> GetDummyParts()
        {
            var parts = new ObservableRangeCollection<Part>
            {
                new Part
                {
                    Name = "Crankshaft F337",
                    Manufacturer = "Bonus Materials",
                    ModelNumber = "CE3874902726",
                    SerialNumber = "456732037463823902826",
                    PartNumber = "6262939036",
                    PriceInUSD = 599.12M,
                    ImageSource = "https://contosomaintenance.blob.core.windows.net/images-large/67a04a57-b51e-45d8-b75c-c343a273b6f7.png"
                },
                new Part
                {
                    Name = "Airplane Engine RFE-747",
                    Manufacturer = "Lolz Roice",
                    ModelNumber = "CE3874902728",
                    SerialNumber = "456732037463823902828",
                    PartNumber = "6262939038",
                    PriceInUSD = 35000000M,
                    ImageSource = "https://contosomaintenance.blob.core.windows.net/images-large/67a24a57-b51r-45d8-b75c-c343a273b6f8.jpg"
                }
            };

            return parts;
        }
    }
}
