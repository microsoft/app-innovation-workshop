using Newtonsoft.Json;
using System;

namespace CognitiveServicesBot.Model
{
    //Data model for search
    public class SearchResult
    {
        [JsonProperty("@odata.context")]
        public string odatacontext { get; set; }
        public Value[] value { get; set; }
    }

    //Data model for fetching facets
    public class FacetResult
    {
        [JsonProperty("@odata.context")]
        public string odatacontext { get; set; }
        [JsonProperty("@search.facets")]
        public SearchFacets searchfacets { get; set; }
        public Value[] value { get; set; }
    }

    public class Value
    {
    //    "@search.score":1.0,
    //     "name":"Testing again",
    //     "details":"oh I love to test. It\u2019s so much fun! ",
    //     "type":"Installation",
    //     "status":"Waiting",
    //     "id":"c0f66ccd-f5c6-418b-87a7-8497cefbaa0d",
    //     "createdAt":"2018-01-25T00:34:49.753Z"
        [JsonProperty("@search.score")]
        public float searchscore { get; set; }
        public string details { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public string status { get; set; }
        public string id { get; set; }
        public DateTime createdAt { get; set; }
    }

    public class SearchFacets
    {
        [JsonProperty("Category@odata.type")]
        public string Categoryodatatype { get; set; }
        public Category[] Category { get; set; }
    }

    public class Category
    {
        public int count { get; set; }
        public string value { get; set; }
    }
}