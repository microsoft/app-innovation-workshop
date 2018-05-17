using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ContosoMaintenance.WebAPI.Services;
using ContosoMaintenance.WebAPI.DummyData;
using ContosoMaintenance.WebAPI.Models;
using ContosoMaintenance.WebAPI.Helpers;

namespace ContosoMaintenance.WebAPI.Controllers
{
    [Route("/api/dummy")]
    public class DummyController : Controller
    {
        DocumentDBRepositoryBase<Job> jobs = new DocumentDBRepositoryBase<Job>();
        DocumentDBRepositoryBase<Part> parts = new DocumentDBRepositoryBase<Part>();

        public DummyController(IConfiguration configuration)
        {
            jobs.Initialize(configuration["AzureCosmosDb:Endpoint"], configuration["AzureCosmosDb:Key"], Constants.DatabaseId);
            parts.Initialize(configuration["AzureCosmosDb:Endpoint"], configuration["AzureCosmosDb:Key"], Constants.DatabaseId);
        }

        /// <summary>
        /// Generates some dummy data to play around with and writes them to the database
        /// </summary>
        /// <returns>The dummy data async.</returns>
        [HttpGet]
        public async Task<string> CreateDummyDataAsync()
        {
            var container = new DummyDataContainer();

            foreach (var item in container.Jobs)
                await jobs.CreateItemAsync(item);

            foreach (var item in container.Parts)
                await parts.CreateItemAsync(item);

            return "Dummy data created.";
        }
    }
}
