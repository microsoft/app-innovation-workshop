using ContosoMaintenance.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ContosoMaintenance.WebAPI.Controllers
{
    [Route("/api/job")]
    public class JobController : BaseController<Job>
    {
        public JobController(IConfiguration configuration) : base(configuration)
        {
        }
    }
}
