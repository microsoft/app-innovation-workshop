using System;
using System.Threading.Tasks;
using ContosoFieldService.Models;
using ContosoFieldService.Services;
using FreshMvvm;
using MvvmHelpers;
using Xamarin.Forms;

namespace ContosoFieldService.PageModels
{
    public class JobsPageModel : FreshBasePageModel
    {
        #region Bindable Properties 
        public ObservableRangeCollection<Job> Jobs { get; set; }
 
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
                    Jobs.Clear();
                    Jobs.AddRange(searchResults);
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
            Jobs = new ObservableRangeCollection<Job>();
        }

        protected override async void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);

            if (Helpers.Settings.UserIsLoggedIn == false)
                await CoreMethods.PushPageModel<LoginPageModel>(null, true, true);

            if(Jobs.Count == 0)
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
            if(Plugin.Connectivity.CrossConnectivity.Current.IsConnected)
            {
                var jobs = await jobsApiService.GetJobsAsync();
                Jobs.Clear();
                Jobs.AddRange(jobs);
                IsRefreshing = false;
            }
            else{
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
