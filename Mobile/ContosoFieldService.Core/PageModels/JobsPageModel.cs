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
        public ObservableRangeCollection<Job> Jobs { get; set; }
        JobsAPIService jobsApiService = new JobsAPIService();

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

        public Command Refresh
        {
            get
            {
                return new Command(async () => {
                    await ReloadData();
                });
            }
        }

        async Task ReloadData()
        {
            IsRefreshing = true;

            var jobs = await jobsApiService.GetJobsAsync();
            Jobs.Clear();
            Jobs.AddRange(jobs);

            IsRefreshing = false;
        }
      
        public Job SelectedJob { get; set; }

        public Command<Job> JobSelected
        {
            get
            {
                return new Command<Job>(async (job) => {
                    await CoreMethods.PushPageModel<JobDetailsPageModel>(job);
                });
            }
        }

        public bool IsRefreshing { get; set; }
    }
}
