using System;
using FreshMvvm;
using MvvmHelpers;
using ContosoFieldService.Models;
using ContosoFieldService.Services;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace ContosoFieldService.PageModels
{
    public class JobsPageModel : FreshBasePageModel
    {
        #region Bindable Properties 
        public ObservableRangeCollection<Job> Jobs { get; set; }
        public bool IsRefreshing { get; set; }
        public string SearchText { get; set; }

        Job selectedJob;
        public Job SelectedJob
        {
            get
            {
                return selectedJob;
            }
            set
            {
                selectedJob = value;
                if (value != null)
                    JobSelected.Execute(value);
            }
        }

        #endregion

        #region Bindable Commands
        public Command Refresh
        {
            get
            {
                return new Command(async () =>
                {
                    await ReloadData();
                });
            }
        }

        public Command<Job> JobSelected
        {
            get
            {
                return new Command<Job>(async (job) =>
                {
                    await CoreMethods.PushPageModel<JobDetailsPageModel>(job);
                });
            }
        }

        public Command Search
        {
            get
            {
                return new Command(async () =>
                {
                    //TODO implement search service
                });
            }
        }
        #endregion

        #region Overrides
        public override void Init(object initData)
        {
            base.Init(initData);

            Jobs = new ObservableRangeCollection<Job>();
        }

        protected override async void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
            await ReloadData();
        }
        #endregion

        #region Private Methods
        async Task ReloadData()
        {
            IsRefreshing = true;

            var jobs = await jobsApiService.GetJobsAsync();
            Jobs.Clear();
            Jobs.AddRange(jobs);

            IsRefreshing = false;
        }
        #endregion

        #region Private Fields
        JobsAPIService jobsApiService = new JobsAPIService();
        #endregion
    }
}
