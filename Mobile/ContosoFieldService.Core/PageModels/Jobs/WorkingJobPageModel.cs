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

namespace ContosoFieldService.PageModels
{
    public class WorkingJobPageModel : FreshBasePageModel
    {
        Job selectedJob;
        DateTime startedJobTime;
        Timer timer;
        int increment;


        public string Duration { get; set; }
        public string Billable { get; set; }
        public bool CameraSupported { get => CrossMedia.Current.IsCameraAvailable ? true : false; }

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
                    Helpers.Settings.UserIsLoggedIn = true;
                    Analytics.TrackEvent("Job Compeleted");
                    await CoreMethods.PopPageModel(true, true);
                });
            }
        }

        public Command SnapPhotoClicked
        {
            get
            {
                return new Command(async () =>
                {
                    if(CrossMedia.Current.IsCameraAvailable == false)
                    {
                        await CoreMethods.DisplayAlert("Camera Unavailable", "Unable to use your camera at this time", "OK");
                        Analytics.TrackEvent("Camera Unavailable");
                        return;
                    }

                    var options = new StoreCameraMediaOptions
                    {
                        DefaultCamera = CameraDevice.Rear,
                        SaveMetaData = true,
                        SaveToAlbum = true,
                        Name = selectedJob.Name + DateTime.Now,
                    };
               
                    Analytics.TrackEvent("Taking a photo");
                    await CrossMedia.Current.TakePhotoAsync(options);
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
                    if(increment >= 10)
                        increment = increment - 10;
                });
            }
        }

    }
}
