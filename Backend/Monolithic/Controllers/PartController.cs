using ContosoMaintenance.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ContosoMaintenance.WebAPI.Controllers
{
    [Route("/api/part")]
    public class PartController : BaseController<Part>
    {
        public PartController(IConfiguration configuration) : base(configuration)
        {
        }
    }
}
