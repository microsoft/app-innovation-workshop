using System;
using ContosoFieldService.Models;
using ContosoFieldService.Services;
using FreshMvvm;
using Microsoft.AppCenter.Analytics;
using Xamarin.Forms;
using Spatial = Microsoft.Azure.Documents.Spatial;


namespace ContosoFieldService.ViewModels
{
    public class CreateNewJobViewModel : BaseViewModel
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

        public Command CreateJobClicked
        {
            get
            {
                return new Command(async () =>
                {
                    var job = new Job
                    {
                        Name = Name,
                        Details = Details,
                        CreatedAt = DateTime.Now,
                        DueDate = DueDate
                    };


                    // Add current location to the job
                    var location = await Plugin.Geolocator.CrossGeolocator.Current.GetPositionAsync();
                    job.Address = new Location { Point = new Models.Point(location.Latitude, location.Longitude) };

                    // Add job to database
                    var response = await jobApiService.CreateJobAsync(job);
                    await HandleResponseCodeAsync(response.code);

                    if (response.result != null)
                    {
                        job = response.result;
                        Analytics.TrackEvent("New Job Created");
                        await CoreMethods.PopPageModel(job, true, true);
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
                    await CoreMethods.PopPageModel(false, true);
                });
            }

        }

        #endregion

        #region Services

        JobsAPIService jobApiService = new JobsAPIService();

        #endregion

    }
}

