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
        static JobsPageModel jobsPageModel;
        public static JobsPageModel JobsPageModel => jobsPageModel ?? (jobsPageModel = new JobsPageModel { Jobs = DummyData.GetGroupedDummyJobs() });
        static JobDetailsPageModel jobDetailsPageModel;
        public static JobDetailsPageModel JobDetailsPageModel
        {
            get
            {
                if (jobDetailsPageModel == null)
                {
                    jobDetailsPageModel = new JobDetailsPageModel();
                    jobDetailsPageModel.Init(DummyData.GetDummyJobs().First());
                }

                return jobDetailsPageModel;
            }
        }

        static PartsPageModel partsPageModel;
        public static PartsPageModel PartsPageModel => partsPageModel ?? (partsPageModel = new PartsPageModel { Parts = DummyData.GetDummyParts() });
        static PartDetailsPageModel partDetailsPageModel;
        public static PartDetailsPageModel PartDetailsPageModel
        {
            get
            {
                if (partDetailsPageModel == null)
                {
                    partDetailsPageModel = new PartDetailsPageModel();
                    partDetailsPageModel.Init(DummyData.GetDummyParts().First());
                }

                return partDetailsPageModel;
            }
        }

        static SettingsPageModel settingsPageModel;
        public static SettingsPageModel SettingsPageModel = settingsPageModel ?? (settingsPageModel = new SettingsPageModel());
    }
}

