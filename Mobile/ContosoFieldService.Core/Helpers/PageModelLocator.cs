using System;
using System.Linq;
using ContosoFieldService.Helpers;
using ContosoFieldService.ViewModels;
using Xamarin.Forms;
using ContosoFieldService.Pages;

namespace ContosoFieldService
{
    /// <summary>
    /// Used to display dummy data at design time
    /// </summary>
    public static class PageModelLocator
    {
        static JobsViewModel jobsPageModel;
        public static JobsViewModel JobsPageModel => jobsPageModel ?? (jobsPageModel = new JobsViewModel { Jobs = DummyData.GetGroupedDummyJobs() });
        static JobDetailsViewModel jobDetailsPageModel;
        public static JobDetailsViewModel JobDetailsPageModel
        {
            get
            {
                if (jobDetailsPageModel == null)
                {
                    jobDetailsPageModel = new JobDetailsViewModel();
                    jobDetailsPageModel.Init(DummyData.GetDummyJobs().First());
                }

                return jobDetailsPageModel;
            }
        }
        static WorkingJobViewModel workingJobViewModel;
        public static WorkingJobViewModel WorkingJobViewModel
        {
            get
            {
                if (workingJobViewModel == null)
                {
                    workingJobViewModel = new WorkingJobViewModel();
                    workingJobViewModel.Init(DummyData.GetDummyJobs().First());
                }

                return workingJobViewModel;
            }
        }

        static PartsViewModel partsPageModel;
        public static PartsViewModel PartsPageModel => partsPageModel ?? (partsPageModel = new PartsViewModel { Parts = DummyData.GetDummyParts() });
        static PartDetailsViewModel partDetailsPageModel;
        public static PartDetailsViewModel PartDetailsPageModel
        {
            get
            {
                if (partDetailsPageModel == null)
                {
                    partDetailsPageModel = new PartDetailsViewModel();
                    partDetailsPageModel.Init(DummyData.GetDummyParts().First());
                }

                return partDetailsPageModel;
            }
        }

        static SettingsViewModel settingsPageModel;
        public static SettingsViewModel SettingsPageModel = settingsPageModel ?? (settingsPageModel = new SettingsViewModel());
    }
}

