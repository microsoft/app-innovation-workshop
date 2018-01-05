using System;
using FreshMvvm;
using Xamarin.Forms;
using ContosoFieldService.Models;
using ContosoFieldService.Services;
using Microsoft.AppCenter.Analytics;

namespace ContosoFieldService.PageModels
{
    public class CreateNewJobPageModel : FreshBasePageModel
    {

        #region Bindable Properties 
        public string Name { get; set; }
        public string Details { get; set; }


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
                    await jobApiService.CreateJobAsync(job);
                });
            }
        }

        public Command CancelCreation
        {
            get
            {
                return new Command(async () =>
                {
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

