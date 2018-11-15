using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ContosoMaintenance.Web.Models;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using ContosoMaintenance.Web.Models.ViewModels;
using Newtonsoft.Json;
using ContosoMaintenance.Web.Services;

namespace ContosoMaintenance.Web.Controllers
{
    public class JobsController : Controller
    {
        private IJobService _jobsService;
    
        public JobsController(IJobService jobService)
        {
            _jobsService = jobService;
        }

        public async Task<IActionResult> Index()
        {
            var jobs = await _jobsService.GetJobs();
            JobsViewModel model = new JobsViewModel()  { Jobs = jobs };
            return View(model);
        }

        public async Task<ActionResult> Edit(string id)
        {
            if (id == null) return BadRequest();
            Job job = await _jobsService.GetJob(id);
            if (job == null) return NotFound();

            return View(job);
        }

        public async Task<ActionResult> StartJob(string id)
        {
            if (id == null) return BadRequest();
            Job job = await _jobsService.GetJob(id);
            if (job == null) return NotFound();

            if (job.Status != JobStatus.InProgress)
            {
                job.Status = JobStatus.InProgress;
                var response = await _jobsService.Update(id, job);
                if (!response.Success)
                {
                    TempData["error"] = $"Start Job Failed";
                    TempData["error-details"] = response.Reason;
                }
                return RedirectToAction("Edit", new { id });
            }
            return RedirectToAction("Edit", new { id });
        }

    }
}
