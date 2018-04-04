using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

using ContosoMaintenance.AdminWebApp.Models;
using ContosoMaintenance.AdminWebApp.Services;


namespace ContosoMaintenance.AdminWebApp.Controllers
{
    public class JobsController : Controller
    {

        private IConfiguration configuration;

        public List<Job> Jobs { get; set; }

        public JobsController(IConfiguration configuration)
        {
            this.configuration = configuration;    
        }

        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            var jobsApiService = new JobsAPIService(configuration);

            var results = await jobsApiService.GetJobsAsync();

            return View(results);
        }
    }
}
