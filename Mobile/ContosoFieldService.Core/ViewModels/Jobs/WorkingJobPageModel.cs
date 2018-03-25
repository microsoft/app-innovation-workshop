using System;
using FreshMvvm;
using Microsoft.AppCenter.Analytics;
using Xamarin.Forms;
using Humanizer;
using System.Timers;
using System.Threading.Tasks;
using Plugin.Media;
using Plugin.Media.Abstractions;
using ContosoFieldService.Models;
using ContosoFieldService.Services;

namespace ContosoFieldService.ViewModels
{
    public class WorkingJobPageModel : FreshBasePageModel
    {
        JobsAPIService jobService = new JobsAPIService();
        PhotoAPIService photoService = new PhotoAPIService();

        Job selectedJob;
        DateTime startedJobTime;
        Timer timer;
        int increment;


        public string Name { get; set; }
        public string Details { get; set; }
        public string Duration { get; set; }
        public string Billable { get; set; }
        public bool CameraSupported { get => CrossMedia.Current.IsCameraAvailable ? true : false; }

        public override void Init(object initData)
        {
            base.Init(initData);
            selectedJob = (Job)initData;
            Name = selectedJob.Name;
            Details = selectedJob.Details;
        }

        protected override async void ViewIsAppearing(object sender, EventArgs e)
        {
            startedJobTime = DateTime.Now;

            Billable = "Billable";
            Duration = "0 seconds";
            RaisePropertyChanged("Billable");
            RaisePropertyChanged("Duration");

            timer = new Timer(1000);
            timer.Enabled = true;
            timer.Elapsed += Timer_Elapsed;
            timer.Start();

            selectedJob.Status = JobStatus.InProgress;
            var updatedJob = await jobService.UpdateJob(selectedJob);
        }

        void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            DateTime now = DateTime.Now;
            var timeSpan = startedJobTime - now;
            Duration = timeSpan.Humanize();
            RaisePropertyChanged("Duration");

            increment++;
            Billable = $"Billable ${(increment * 3)}";
            RaisePropertyChanged("Billable");
        }

        public Command CompleteClicked
        {
            get
            {
                return new Command(async () =>
                {
                    Analytics.TrackEvent("Job Compeleted");

                    //TODO: Show Loading indicators
                    selectedJob.Status = JobStatus.Complete;
                    var updatedJob = await jobService.UpdateJob(selectedJob);


                    await CoreMethods.PopPageModel(updatedJob, true, true);
                });
            }
        }

        public Command SnapPhotoClicked
        {
            get
            {
                return new Command(async () =>
                {
                    MediaFile file = null;

                    if (CrossMedia.Current.IsCameraAvailable)
                    {
                        file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                        {
                            DefaultCamera = CameraDevice.Rear,
                            SaveMetaData = true,
                            SaveToAlbum = true
                        });
                    }
                    else if (CrossMedia.Current.IsPickPhotoSupported)
                    {
                        file = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
                        {
                            SaveMetaData = true
                        });
                    }

                    if (file != null)
                    {
                        Analytics.TrackEvent("Taking a photo");

                        var updatedJob = await photoService.UploadPhotoAsync(selectedJob.Id, file);

                        // TODO: Update current job with updatedjob

                        await CoreMethods.DisplayAlert("Saved", "Image Saved", "OK");
                    }
                });
            }
        }

        public Command QuickBreakClicked
        {
            get
            {
                return new Command(async () =>
                {
                    Analytics.TrackEvent("Quick Break");
                    if (increment >= 10)
                        increment = increment - 10;
                });
            }
        }
    }
}
