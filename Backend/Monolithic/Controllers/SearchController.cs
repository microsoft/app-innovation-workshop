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

        [HttpGet]
        [Route("/api/search/jobs")]
        public async Task<List<Job>> Get(string keyword)
        {
            var sp = new SuggestParameters();
            sp.HighlightPreTag = "[";
            sp.HighlightPostTag = "]";
            sp.UseFuzzyMatching = true;
            sp.MinimumCoverage = 50;
            sp.Top = 100;

            var indexClient = serviceClient.Indexes.GetClient("job-index");

            var response = await indexClient.Documents.SuggestAsync<Job>(keyword, "suggestions", sp);

            var jobList = new List<Job>();
            foreach (var document in response.Results)
            {
                var job = new Job
                {
                    Name = document.Text,
                    Details = document.Document.Details,
                    Status = document.Document.Status,
                };

                jobList.Add(job);
            }
            return jobList;
        }


    }
}
