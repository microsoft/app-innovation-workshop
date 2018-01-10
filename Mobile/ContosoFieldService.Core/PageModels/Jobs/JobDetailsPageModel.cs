using System;
using ContosoFieldService.Models;
using FreshMvvm;
using Humanizer;
using Xamarin.Forms;

namespace ContosoFieldService.PageModels
{
    public class JobDetailsPageModel : FreshBasePageModel
    {
        public string Name { get; set; }
        public string Details { get; set; }
        public string Age { get; set; }
        public string DueDate { get; set; }
        public string ContactName { get; set; }
        public string CompanyName { get; set; }

        Job Job;
        public override void Init(object initData)
        {
            if (initData != null)
            {

                Job = (Job)initData;
                Name = Job.Name;
                Details = Job.Details;
                DueDate = DateTime.Now.Humanize();

                Age = Job.CreatedAt.Humanize();
                Details = string.IsNullOrEmpty(Job.Details) ? "Not Supplied" : Job.Details;
                ContactName = string.IsNullOrEmpty(Job?.Customer?.CompanyName) ? "Not Supplied" : Job.Customer.ContactName;
                CompanyName = string.IsNullOrEmpty(Job?.Customer?.ContactName) ? "Not Supplied" : Job.Customer.CompanyName;

               
            }
            else
            {
                Job = new Job();
            }
        }

        public Command StartJobClicked
        {
            get
            {
                return new Command(async () =>
                {
                    await CoreMethods.PushPageModel<WorkingJobPageModel>(null, true, true);
                });
            }
        }

        public Command EditJobClicked
        {
            get
            {
                return new Command(async () =>
                {
                    //Todo 
                });
            }
        }

        public Command DeleteJobClicked
        {
            get
            {
                return new Command(async () =>
                {
                    //Todo
                });
            }
        }

        public Command OrderPartsClicked
        {
            get
            {
                return new Command(async () =>
                {
                    await CoreMethods.PushPageModel<JobsPageModel>();
                });
            }
        }
    }

}
