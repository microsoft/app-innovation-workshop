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
        //"@search.score": 1,
        //    "Name": "Service ATR 42 Engine",
        //    "Details": "General Service",
        //    "Type": "Service",
        //    "Status": "Complete",
        //    "DueDate": "0001-01-01T00:00:00Z",
        //    "id": "3de8f6d0-e1b6-416a-914d-cd13554929a5",
        //    "isDeleted": false
        [JsonProperty("@search.score")]
        public float SearchScore { get; set; }

        [JsonProperty("Details")]
        public string Details { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Type")]
        public string Type { get; set; }

        [JsonProperty("Status")]
        public string Status { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("DueDate")]
        public DateTime DueDate { get; set; }

        [JsonProperty("isDeleted")]
        public bool IsDeleted { get; set; }

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