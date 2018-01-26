using System;
using ContosoFieldService.Models;
using FreshMvvm;
using Humanizer;
using Microsoft.Azure.Documents.Spatial;
using Xamarin.Forms;

namespace ContosoFieldService.PageModels
{
    public class JobDetailsPageModel : FreshBasePageModel
    {
        public string Name { get; set; }
        public string Details { get; set; }
        public string Age { get; set; }
        public string DueDate { get; set; }
        public string ContactName { get; set; }
        public string CompanyName { get; set; }
        public Microsoft.Azure.Documents.Spatial.Point Point { get; set; }

        Job CurrentJob;
        public override void Init(object initData)
        {
            if (initData != null)
            {
                CurrentJob = (Job)initData;
                Name = CurrentJob.Name;
                Details = CurrentJob.Details;
                DueDate = DateTime.Now.Humanize();

                Age = CurrentJob.CreatedAt.Humanize();
                Details = string.IsNullOrEmpty(CurrentJob.Details) ? "Not Supplied" : CurrentJob.Details;
                Point = CurrentJob?.Address?.GeoPosition;
            }
            else
            {
                CurrentJob = new Job();
            }
        }

        public Command StartJobClicked
        {
            get
            {
                return new Command(async () =>
                {
                    await CoreMethods.PushPageModel<WorkingJobPageModel>(CurrentJob, true, true);
                });
            }
        }

        public Command EditJobClicked
        {
            get
            {
                return new Command(async () =>
                {
                    //Todo 
                });
            }
        }

        public Command DeleteJobClicked
        {
            get
            {
                return new Command(async () =>
                {
                    // Confirm deletion
                    if (await CoreMethods.DisplayAlert("Delete", "Do you really want to permanently delete this job?", "Delete", "Cancel"))
                    {
                        //TODO: Show Loading indicator
                        var jobsService = new Services.JobsAPIService();
                        var updatedJob = await jobsService.DeleteJobByIdAsync(CurrentJob.Id);
                        await CoreMethods.PopPageModel(updatedJob);
                    }
                });
            }
        }

        public Command OrderPartsClicked
        {
            get
            {
                return new Command(async () =>
                {
                    await CoreMethods.PushPageModel<PartsPageModel>();
                });
            }
        }

    }

}
