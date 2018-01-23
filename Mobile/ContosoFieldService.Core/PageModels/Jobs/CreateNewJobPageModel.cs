using System;
using ContosoFieldService.Models;
using ContosoFieldService.Services;
using FreshMvvm;
using Microsoft.AppCenter.Analytics;
using Xamarin.Forms;

namespace ContosoFieldService.PageModels
{
    public class CreateNewJobPageModel : FreshBasePageModel
    {
        #region Bindable Properties 
        public string Name { get; set; }
        public string Details { get; set; }
        public DateTime CurrentDate { get; set; }
        public DateTime DueDate { get; set; }

        public override void Init(object initData)
        {
            base.Init(initData);

            DueDate = DateTime.Now;
            CurrentDate = DateTime.Now;
        }


        public Command CreateJob
        {
            get
            {
                return new Command(async () =>
                {
                    var job = new Job
                    {
                        Name = Name,
                        Details = Details
                    };
                    Analytics.TrackEvent("New Job Created");
                    try
                    {
                        await jobApiService.CreateJobAsync(job);
                    }
                    catch(Exception)
                    {
                        await CoreMethods.DisplayAlert("Network Error", "No connectivity", "OK");
                    }
                });
            }
        }

        public Command CancelClicked
        {
            get
            {
                return new Command(async () =>
                {
                    Analytics.TrackEvent("Cancel Job Creation");
                    await CoreMethods.PopPageModel(true, true);
                });
            }

        }

        #endregion

        #region Services

        JobsAPIService jobApiService = new JobsAPIService();

        #endregion

    }
}

