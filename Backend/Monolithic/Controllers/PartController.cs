using ContosoMaintenance.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace ContosoMaintenance.WebAPI.Controllers
{
    [Route("/api/part")]
    public class PartController : BaseController<Part>
    {
    }
}
