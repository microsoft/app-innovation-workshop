using Newtonsoft.Json;
using System;

namespace CognitiveServicesBot.Model
{
    //As we are using direct Http calls to the Search APIs, we needed to use the below models
    //In the future, this should be replaced with Azure Search Nuget for cleaner implementation.
    //Data model for search
    public class SearchResult
    {
        [JsonProperty("@odata.context")]
        public string odatacontext { get; set; }

        [JsonProperty("value")]
        public Value[] Values { get; set; }
    }

    //Data model for fetching facets
    public class FacetResult
    {
        [JsonProperty("@odata.context")]
        public string ODataContext { get; set; }

        [JsonProperty("@search.facets")]
        public SearchFacets SearchFacets { get; set; }

        [JsonProperty("value")]
        public Value[] Values { get; set; }
    }

    public class Value
    {
        //The below JSON is returned from the search service:
        //    "@search.score":1.0,
        //     "name":"Testing again",
        //     "details":"oh I love to test. It\u2019s so much fun! ",
        //     "type":"Installation",
        //     "status":"Waiting",
        //     "id":"c0f66ccd-f5c6-418b-87a7-8497cefbaa0d",
        //     "createdAt":"2018-01-25T00:34:49.753Z"
        [JsonProperty("@search.score")]
        public float searchscore { get; set; }

        [JsonProperty("details")]
        public string Details { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("createdAt")]
        public DateTime CreatedAt { get; set; }
    }

    public class SearchFacets
    {
        [JsonProperty("Category@odata.type")]
        public string Categoryodatatype { get; set; }

        [JsonProperty("Category")]
        public Category[] Category { get; set; }
    }

    public class Category
    {
        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }
}