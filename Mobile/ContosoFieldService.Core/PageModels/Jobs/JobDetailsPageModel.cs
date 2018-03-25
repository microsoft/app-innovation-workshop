using System;
using ContosoFieldService.Models;
using FreshMvvm;
using Humanizer;
using Plugin.Share;
using Plugin.Share.Abstractions;
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
        public GeoPoint Point { get; set; }

        Job selectedJob;
        public override void Init(object initData)
        {
            if (initData != null)
            {
                selectedJob = (Job)initData;
                Name = selectedJob.Name;
                Details = selectedJob.Details;
                DueDate = DateTime.Now.Humanize();

                Age = selectedJob.CreatedAt.Humanize();
                Details = string.IsNullOrEmpty(selectedJob.Details) ? "Not Supplied" : selectedJob.Details;
                Point = selectedJob?.Address?.GeoPosition;

            }
            else
            {
                selectedJob = new Job();
            }
        }

        public Command StartJobClicked
        {
            get
            {
                return new Command(async () =>
                {
                    await CoreMethods.PushPageModel<WorkingJobPageModel>(selectedJob, true, true);
                });
            }
        }

        public Command ShareJobClicked
        {
            get
            {
                return new Command(async () =>
                {
                    if (!CrossShare.IsSupported)
                        return;

                    await CrossShare.Current.Share(new ShareMessage
                    {
                        Title = selectedJob.Name,
                        Text = selectedJob.Details,
                        Url = $"{Helpers.Constants.BaseUrl}job/{selectedJob.Id}"
                    });
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
                    if ("Delete" == await CoreMethods.DisplayActionSheet("You're going to delete this job and it'll be embrassing for all if we need to restore it...", "Canel", "Delete"))
                    {
                        var jobsService = new Services.JobsAPIService();
                        var deletedJob = await jobsService.DeleteJobByIdAsync(selectedJob.Id);
                        if (deletedJob != null)
                        {
                            await CoreMethods.PopPageModel(deletedJob);
                        }
                        else
                        {
                            await CoreMethods.DisplayAlert("Deletion failed", "The job could not be deleted. This most likely mean authentication issues.", "Ok");
                        }
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
