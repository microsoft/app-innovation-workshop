using System;
using FreshMvvm;
using Microsoft.AppCenter.Analytics;
using Xamarin.Forms;
using Humanizer;
using System.Timers;
using System.Threading.Tasks;

namespace ContosoFieldService.PageModels
{
    public class WorkingJobPageModel : FreshBasePageModel
    {
        DateTime startedJobTime;
        Timer timer;
        int tenSecondCount;

        public string Duration { get; set; }
        public string Billable { get; set; }

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


    }
}
