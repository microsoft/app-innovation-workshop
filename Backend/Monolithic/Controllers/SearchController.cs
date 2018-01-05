using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContosoMaintenance.WebAPI.Models;
using ContosoMaintenance.WebAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;

namespace ContosoMaintenance.WebAPI.Controllers
{
    public class SearchController : Controller 
    {
        [Route("/api/search/jobs")]
        public async Task<List<Job>> Get(string keyword)
        {
            var sp = new SearchParameters();
            var serviceClient = new SearchServiceClient(Helpers.Keys.AzureSearchServiceName, new SearchCredentials(Helpers.Keys.AzureSearchApiKey));
            var indexClient = serviceClient.Indexes.GetClient("job-index");

            var response = await indexClient.Documents.SearchAsync<Job>(keyword, sp);

            DocumentDBRepositoryBase<Job> DBRepository = new DocumentDBRepositoryBase<Job>();


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
