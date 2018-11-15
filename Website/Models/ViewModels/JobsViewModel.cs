using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContosoMaintenance.Web.Models.ViewModels
{
    public class JobsViewModel
    {
        public IEnumerable<Job> Jobs { get; set; } = new List<Job>();
    }
}
