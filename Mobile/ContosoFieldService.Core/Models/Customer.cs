using System;
using Newtonsoft.Json;

namespace ContosoFieldService.Models
{
    public class Customer : BaseModel
    {
        [JsonProperty("companyName")]
        public string CompanyName { get; set; }

        [JsonProperty("address")]
        public Location Address { get; set; }

        [JsonProperty("contactNumber")]
        public string ContactNumber { get; set; }

        [JsonProperty("contactName")]
        public string ContactName { get; set; }
    }
}
