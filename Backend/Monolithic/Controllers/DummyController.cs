using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ContosoMaintenance.WebAPI.Services;
using ContosoMaintenance.WebAPI.DummyData;
using ContosoMaintenance.WebAPI.Models;

namespace ContosoMaintenance.WebAPI.Controllers
{
    [Route("/api/dummy")]
    public class DummyController : Controller
    {
        DocumentDBRepositoryBase<Job> jobs = new DocumentDBRepositoryBase<Job>();
        DocumentDBRepositoryBase<Customer> customers = new DocumentDBRepositoryBase<Customer>();
        DocumentDBRepositoryBase<Employee> employees = new DocumentDBRepositoryBase<Employee>();
        DocumentDBRepositoryBase<Part> parts = new DocumentDBRepositoryBase<Part>();

        public DummyController(IConfiguration configuration)
        {
            jobs.Initialize(configuration["AzureCosmosDb:Endpoint"], configuration["AzureCosmosDb:Key"], configuration["AzureCosmosDb:DatabaseId"]);
            customers.Initialize(configuration["AzureCosmosDb:Endpoint"], configuration["AzureCosmosDb:Key"], configuration["AzureCosmosDb:DatabaseId"]);
            employees.Initialize(configuration["AzureCosmosDb:Endpoint"], configuration["AzureCosmosDb:Key"], configuration["AzureCosmosDb:DatabaseId"]);
            parts.Initialize(configuration["AzureCosmosDb:Endpoint"], configuration["AzureCosmosDb:Key"], configuration["AzureCosmosDb:DatabaseId"]);
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

            foreach (var item in container.Customers)
                await customers.CreateItemAsync(item);

            foreach (var item in container.Employees)
                await employees.CreateItemAsync(item);

            foreach (var item in container.Parts)
                await parts.CreateItemAsync(item);

            return "Dummy data created.";
        }
    }
}
