using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ContosoFieldService.Models;
using FreshMvvm;

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
    }
}
