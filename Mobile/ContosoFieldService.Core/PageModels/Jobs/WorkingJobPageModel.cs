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
        int tenSecondCount;


        public string Duration { get; set; }
        public string Billable { get; set; }
        public bool CameraSupported { get => CrossMedia.Current.IsCameraAvailable ? true : false; }

        protected override async void ViewIsAppearing(object sender, EventArgs e)
        {
            startedJobTime = DateTime.Now;

            timer = new Timer(500);
            timer.Enabled = true;
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }
 
        void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            
            Duration = startedJobTime.Humanize();
            RaisePropertyChanged("Duration");

            tenSecondCount++;
            Billable = $"Billable ${(tenSecondCount * 10)}";
            RaisePropertyChanged("Billable");
        }

        public Command CompleteClicked
        {
            get
            {
                return new Command(async () =>
                {
                    Helpers.Settings.UserIsLoggedIn = true;
                    Analytics.TrackEvent("Job Compelted");
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

    }
}
