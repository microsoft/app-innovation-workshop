using System;
using ContosoMaintenance.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ContosoMaintenance.WebAPI.Controllers
{
    [Route("/api/employee")]
    public class EmployeeController : BaseController<Employee>
    {
        public EmployeeController(IConfiguration configuration) : base(configuration)
        {
        }
    }
}
