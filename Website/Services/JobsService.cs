using ContosoMaintenance.Web.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text;
using System.Net;
using ContosoMaintenance.Web.Models.Interfaces;

namespace ContosoMaintenance.Web.Services
{
    public class JobsService : IJobService
    {
        private IHttpClientFactory _clientFactory;
        private Config _config;

        public JobsService(IHttpClientFactory clientFactory, IUserConfig config)
        {
            _clientFactory = clientFactory;
            _config = config.GetConfig();
        }

        public async Task<IEnumerable<Job>> GetJobs()
        {
            IEnumerable<Job> jobs = new List<Job>();
            try
            {
                var baseUri = new Uri(_config.BaseUrl);
                using (HttpClient client = _clientFactory.CreateClient())
                {
                    if (!string.IsNullOrWhiteSpace(_config.APIManagementKey))
                        client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", _config.APIManagementKey);
                    using (HttpResponseMessage res = await client.GetAsync(new Uri(baseUri, "job")))
                    using (HttpContent resContent = res.Content)
                    {
                        string data = await resContent.ReadAsStringAsync();
                        jobs = JsonConvert.DeserializeObject<IEnumerable<Job>>(data);
                    }
                }
               
            }
            catch { }
            return jobs;
        }


        public async Task<Job> GetJob(string id)
        {
            Job job = null;
            try
            {
                var baseUri = new Uri(_config.BaseUrl);
                using (HttpClient client = _clientFactory.CreateClient())
                {
                    if (!string.IsNullOrWhiteSpace(_config.APIManagementKey))
                        client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", _config.APIManagementKey);

                    using (HttpResponseMessage res = await client.GetAsync(new Uri(baseUri, $"job/{id}")))
                    using (HttpContent resContent = res.Content)
                    {
                        string data = await resContent.ReadAsStringAsync();
                        var jobs = JsonConvert.DeserializeObject<IEnumerable<Job>>(data);//the api returns an array with one item
                        if (jobs != null && jobs.Any())
                            job = jobs.ToList()[0];
                    }
                }
            }
            catch { }
            return job;
        }

        public async Task<APIResponse> Update(string id, Job job)
        {
            try
            {
                var baseUri = new Uri(_config.BaseUrl);
                var json = JsonConvert.SerializeObject(job);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                using (HttpClient client = _clientFactory.CreateClient())
                {
                    if (!string.IsNullOrWhiteSpace(_config.APIManagementKey))
                        client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", _config.APIManagementKey);

                    using (HttpResponseMessage res = await client.PutAsync(new Uri(baseUri, $"job/{id}"), content))
                    using (HttpContent resContent = res.Content)
                    {
                        return new APIResponse { Success = res.IsSuccessStatusCode, StatusCode = res.StatusCode, Reason = res.ReasonPhrase };
                    }
                }
               
            }
            catch (Exception ex)
            {
                return new APIResponse { Success = false, StatusCode = HttpStatusCode.InternalServerError, Reason = ex.Message};
            }
        }

        
    }

    public interface IJobService
    {
        Task<IEnumerable<Job>> GetJobs();
        Task<Job> GetJob(string id);
        Task<APIResponse> Update(string id, Job job);
    }

}
