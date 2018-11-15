using ContosoMaintenance.Web.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContosoMaintenance.Web.Models
{
    public class Config
    {
        public string BaseUrl { get; set; }
        public string APIManagementKey { get; set; }
    }

}
