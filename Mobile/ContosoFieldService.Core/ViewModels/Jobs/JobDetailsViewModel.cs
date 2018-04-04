using System;
using System.Collections.Generic;
using ContosoFieldService.Models;
using FreshMvvm;
using Humanizer;
using Plugin.Share;
using Plugin.Share.Abstractions;
using Xamarin.Forms;
using System.Windows.Input;
using ContosoFieldService.ViewModels.Jobs;

namespace ContosoFieldService.ViewModels
{
    public class JobDetailsViewModel : FreshBasePageModel
    {
        Photo selectedPhoto;
        public Photo SelectedPhoto
        {
            get { return selectedPhoto; }
            set
            {
                selectedPhoto = value;
                RaisePropertyChanged();
            }
        }

        List<Photo> photos;
        public List<Photo> Photos
        {
            get { return photos; }
            set { photos = value; RaisePropertyChanged(); }
        }

        public string Name { get; set; }
        public string Details { get; set; }
        public string Age { get; set; }
        public string DueDate { get; set; }
        public string ContactName { get; set; }
        public string CompanyName { get; set; }
        public Models.Point Point { get; set; }


        Job selectedJob;

        public Command PhotoSelected => new Command(async (object item) =>
        {
            await CoreMethods.PushPageModel<PhotoViewerViewModel>(item, false);
        });

        public Command StartJobClicked
        {
            get
            {
                return new Command(async () =>
                {
                    await CoreMethods.PushPageModel<WorkingJobViewModel>(selectedJob, true, true);
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
                            jobsService.InvalidateCache("Jobs");
                            await CoreMethods.PopPageModel(deletedJob);
                        }
                        else
                        {
                            await CoreMethods.DisplayAlert("Deletion failed", "The job could not be deleted. This most likely means authentication issues.", "Ok");
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
                    await CoreMethods.PushPageModel<PartsViewModel>();
                });
            }
        }


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
                Point = selectedJob?.Address?.Point;
                Photos = selectedJob?.Photos;
            }
            else
            {
                selectedJob = new Job();
            }
        }

        public override void ReverseInit(object returnedData)
        {
            base.ReverseInit(returnedData);
            Init(returnedData);
        }
    }

}
