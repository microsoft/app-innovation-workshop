using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ContosoFieldService.Models;

namespace ContosoFieldService.PageModels
{
    public class JobDetailsPageModel
    {
        public JobDetailsPageModel()
        {
        }


        #region Service Calls

        //TODO MOBILE APP : 101
        async Task<List<Job>> GetJobs()
        {
            return new List<Job>();
        }

        #endregion
    }
}
