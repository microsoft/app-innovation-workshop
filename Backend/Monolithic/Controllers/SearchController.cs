using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContosoMaintenance.WebAPI.Models;
using ContosoMaintenance.WebAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using Microsoft.Extensions.Configuration;

namespace ContosoMaintenance.WebAPI.Controllers
{
    public class SearchController : Controller
    {
        SearchServiceClient serviceClient;

        public SearchController(IConfiguration configuration)
        {
            serviceClient = new SearchServiceClient(configuration["AzureSearch:AzureSearchServiceName"], new SearchCredentials(configuration["AzureSearch:AzureSearchApiKey"]));
        }

        [Route("/api/search/jobs")]
        public async Task<List<Job>> Get(string keyword)
        {
            var sp = new SearchParameters();
            var indexClient = serviceClient.Indexes.GetClient("job-index");

            var response = await indexClient.Documents.SearchAsync<Job>(keyword, sp);

            var jobList = new List<Job>();
            foreach (var document in response.Results)
            {
                Job job = new Job
                {
                    Name = document.Document.Name,
                    Details = document.Document.Details
                };

                jobList.Add(job);
            }
            return jobList;
        }


    }
}
