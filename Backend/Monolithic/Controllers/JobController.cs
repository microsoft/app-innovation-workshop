using ContosoMaintenance.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace ContosoMaintenance.WebAPI.Controllers
{
    [Route("/api/job")]
    public class JobController : BaseController<Job>
    {
    }
}
