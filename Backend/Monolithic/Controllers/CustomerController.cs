using System;
using ContosoMaintenance.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace ContosoMaintenance.WebAPI.Controllers
{
    [Route("/api/customer")]
    public class CustomerController : BaseController<Customer>
    {
    }
}
