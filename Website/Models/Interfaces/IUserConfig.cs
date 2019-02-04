using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContosoMaintenance.Web.Models.Interfaces
{

    public interface IUserConfig
    {
        bool UserOverrideConfig { get; set; }
        Config DefaultConfig { get; set; }
        Config OverrideConfig { get; set; }
        Config GetConfig();
    }
}
