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
            sp.IncludeTotalResultCount = true;
            sp.HighlightFields = new[] { "Name" };
            sp.HighlightPreTag = "[";
            sp.HighlightPostTag = "]";
            sp.MinimumCoverage = 50;
            sp.Top = 100;

            var indexClient = serviceClient.Indexes.GetClient("job-index");

            var response = await indexClient.Documents.SearchAsync<Job>(keyword, sp);

            var jobList = new List<Job>();
            foreach (var document in response.Results)
            {
                Job job;
                if (document.Highlights.Count > 0)
                {
                    //We can hit-highlight this puppy! 
                    job = new Job
                    {
                        Name = document.Highlights.FirstOrDefault().Value.FirstOrDefault().ToString(),
                        Details = document.Document.Details
                    };
                }
                else
                {
                    job = new Job
                    {
                        Name = document.Document.Name,
                        Details = document.Document.Details
                    };
                }

                jobList.Add(job);
            }
            return jobList; 
        }


    }
}
