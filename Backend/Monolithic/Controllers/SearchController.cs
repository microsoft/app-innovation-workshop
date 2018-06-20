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
            serviceClient = new SearchServiceClient(configuration["AzureSearch:ServiceName"], new SearchCredentials(configuration["AzureSearch:ApiKey"]));
        }

        /// <summary>
        /// Searches for a Job by a keyword
        /// </summary>
        /// <returns>Result list of Jobs</returns>
        /// <param name="keyword">Search keyword</param>
        /// <param name="suggestions">Should return suggestions or full search?</param>
        [HttpGet]
        [Route("/api/search/jobs")]
        public async Task<List<Job>> Get(string keyword, bool suggestions = false)
        {
            var indexClient = serviceClient.Indexes.GetClient("job-index");
            var jobList = new List<Job>();

            if(suggestions)
            {
                var sp = new SuggestParameters();
                sp.HighlightPreTag = "[";
                sp.HighlightPostTag = "]";
                sp.UseFuzzyMatching = true;
                sp.MinimumCoverage = 75;
                sp.Top = 100;

                var response = await indexClient.Documents.SuggestAsync<Job>(keyword, "suggestions", sp);

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
            }
            else
            {
                var sp = new SearchParameters();
                var response = await indexClient.Documents.SearchAsync<Job>(keyword, sp);
                foreach (var document in response.Results)
                {
                    Job job = new Job
                    {
                        Name = document.Document.Name,
                        Details = document.Document.Details
                    };
                    jobList.Add(job);
                }
            }
            return jobList;
        }
    }
}
