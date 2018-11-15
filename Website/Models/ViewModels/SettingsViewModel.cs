using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContosoMaintenance.Web.Models.ViewModels
{
    public class SettingsViewModel
    {

        public bool UserOverrideConfig { get; set; } = false;

        public string OverrideBaseUrl { get; set; }
        public string OverrideAPIManagementKey { get; set; }

        public string DefaultBaseUrl { get; set; }
        public string DefaultAPIManagementKey { get; set; }
    }
}
