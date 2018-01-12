using System;
using ContosoMaintenance.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace ContosoMaintenance.WebAPI.Controllers
{
    [Route("/api/employee")]
    public class EmployeeController : BaseController<Employee>
    {
    }
}
