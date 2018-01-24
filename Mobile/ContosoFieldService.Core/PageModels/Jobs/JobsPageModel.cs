using System;
using System.Threading.Tasks;
using ContosoFieldService.Models;
using ContosoFieldService.Services;
using FreshMvvm;
using MvvmHelpers;
using Xamarin.Forms;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace ContosoFieldService.PageModels
{
    public class JobsPageModel : FreshBasePageModel
    {
        #region Bindable Properties 
        public ObservableRangeCollection<GroupedJobs> Jobs { get; set; }

        public bool IsRefreshing
        {
            get
            {
                return isRefreshing;
            }
            set
            {
                isRefreshing = value;
                RaisePropertyChanged();
            }
        }

        string searchText;
        public string SearchText
        {
            get
            {
                return searchText;
            }
            set
            {
                searchText = value;
                if (string.IsNullOrWhiteSpace(value))
                {
                    ReloadData(true);
                }
                else
                    Search.Execute(value);
            }
        }

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
                RaisePropertyChanged();

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
                    await CoreMethods.PushPageModel<JobDetailsPageModel>(selectedJob);
                });
            }
        }

        public Command Search
        {
            get
            {
                return new Command(async () =>
                {
                    var searchResults = await jobsApiService.SearchJobsAsync(SearchText);
                    Jobs.ReplaceRange(new List<GroupedJobs>
                    {
                        new GroupedJobs("Search Results", searchResults)
                    });
                });
            }
        }

        public Command AddJobClicked
        {
            get
            {
                return new Command(async () =>
                {
                    await CoreMethods.PushPageModel<CreateNewJobPageModel>(null, true, true);
                });
            }
        }

        #endregion

        #region Overrides
        public override async void Init(object initData)
        {
            base.Init(initData);
            Jobs = new ObservableRangeCollection<GroupedJobs>();
        }

        protected override async void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);

            if (Helpers.Settings.UserIsLoggedIn == false)
                await CoreMethods.PushPageModel<LoginPageModel>(null, true, true);

            if (Jobs.Count == 0)
                await ReloadData();

        }

        protected override async void ViewIsDisappearing(object sender, EventArgs e)
        {
            SelectedJob = null;
        }


        public override async void ReverseInit(object returndData)
        {
            base.ReverseInit(returndData);
            SelectedJob = null;
            await ReloadData();
        }

        #endregion

        #region Private Methods
        async Task ReloadData(bool isSilent = false)
        {
            IsRefreshing = !isSilent;
            if (Plugin.Connectivity.CrossConnectivity.Current.IsConnected)
            {
                // Download jobs from server
                var jobs = await jobsApiService.GetJobsAsync();

                // Group jobs by JobStatus
                var groupedJobs = new List<GroupedJobs>
                {
                    new GroupedJobs("Waiting", jobs.Where(x => x.Status == JobStatus.Waiting)),
                    new GroupedJobs("In Progress", jobs.Where(x => x.Status == JobStatus.InProgress)),
                    new GroupedJobs("Complete", jobs.Where(x => x.Status == JobStatus.Complete)),
                };

                // Add only groups that actually have items to the list
                Jobs.ReplaceRange(groupedJobs.Where(x => x.Any()));

                IsRefreshing = false;
            }
            else
            {
                await CoreMethods.DisplayAlert("Network Error", "No internet connectivity found", "OK");
            }
            IsRefreshing = false;
        }
        #endregion

        #region Private Fields
        JobsAPIService jobsApiService = new JobsAPIService();

        bool isRefreshing;
        #endregion
    }
}
