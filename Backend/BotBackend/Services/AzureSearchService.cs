using System.Net.Http;
using System.Web.Configuration;
using System.Threading.Tasks;
using CognitiveServicesBot.Model;
using Newtonsoft.Json;
using System;
using System.Net;

namespace CognitiveServicesBot.Services
{
    [Serializable]
    public class AzureSearchService
    {
        static readonly string QueryString = $"https://{WebConfigurationManager.AppSettings["SearchName"]}.search.windows.net/indexes/{WebConfigurationManager.AppSettings["IndexName"]}/docs?api-key={WebConfigurationManager.AppSettings["SearchKey"]}&api-version=2016-09-01&";

        public async Task<FacetResult> FetchFacets()
        {
            using (var httpClient = new HttpClient())
            {
                string facetQuey = $"{QueryString}facet=Category";
                string response = await httpClient.GetStringAsync(facetQuey);
                return JsonConvert.DeserializeObject<FacetResult>(response);
            }
        }

        //Although this method is not used, but it show a nice demo of wide search of the index
        //public async Task<SearchResult> Search(string value)
        //{
        //    if (string.IsNullOrEmpty(value))
        //        throw new ArgumentNullException("Cannot search with a null value");

        //    using (var httpClient = new HttpClient())
        //    {
        //        string parsedSearch = WebUtility.UrlEncode(value);
        //        string query = $"{QueryString}search='{parsedSearch}'";
        //        string response = await httpClient.GetStringAsync(query);
        //        return JsonConvert.DeserializeObject<SearchResult>(response);
        //    }
        //}

        public async Task<SearchResult> FilterByStatus(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException("Cannot search with a null value");

            using (var httpClient = new HttpClient())
            {
                string parsedStatus = WebUtility.UrlEncode(value.Replace(" ", "+"));
                //$filter=status eq 'Waiting'
                string query = $"{QueryString}$filter=Status eq '{value}'";
                string response = await httpClient.GetStringAsync(query);
                return JsonConvert.DeserializeObject<SearchResult>(response);
            }
        }
    }
}