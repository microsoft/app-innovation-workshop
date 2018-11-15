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
using Microsoft.AspNetCore.Diagnostics;
using ContosoMaintenance.Web.Models.Interfaces;

namespace ContosoMaintenance.Web.Controllers
{
    public class HomeController : Controller
    {
        private IUserConfig _config;

        public HomeController(IHttpClientFactory factory, IUserConfig config, IHttpContextAccessor httpContextAccessor)
        {
            _config = config;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }


      

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [Route("/error")]
        public IActionResult Error()
        {
            var feature = this.HttpContext.Features.Get<IExceptionHandlerFeature>();
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier , ServerException = feature.Error, });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [Route("/error/404")]
        public IActionResult Error404()
        {
            return View();
        }
    }
}
