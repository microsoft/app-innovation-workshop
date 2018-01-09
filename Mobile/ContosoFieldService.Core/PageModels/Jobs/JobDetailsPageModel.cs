using ContosoFieldService.Models;
using FreshMvvm;
using Xamarin.Forms;

namespace ContosoFieldService.PageModels
{
    public class JobDetailsPageModel : FreshBasePageModel
    {
        public string Name { get; set; }
        public string Details { get; set; }


        Job Job;
        public override void Init(object initData)
        {
            if (initData != null)
            {
                Job = (Job)initData;
                Name = Job.Name;
                Details = Job.Details;
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
                    //Todo
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
