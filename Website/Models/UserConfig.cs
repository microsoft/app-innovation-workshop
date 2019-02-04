using ContosoMaintenance.Web.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContosoMaintenance.Web.Models
{

    public class UserConfig : IUserConfig
    {
        public bool UserOverrideConfig { get; set; } = false;

        public Config DefaultConfig { get; set; } = new Config();

        public Config OverrideConfig { get; set; } = new Config();

        public Config GetConfig()
        {
            return (UserOverrideConfig) ? OverrideConfig : DefaultConfig;
        }
    }

}
