using System;

using Newtonsoft.Json;

namespace ContosoMaintenance.AdminWebApp.Models
{
    public class Customer : BaseModel
    {
        [JsonProperty("companyName")]
        public string CompanyName { get; set; }

        [JsonProperty("contactNumber")]
        public string ContactNumber { get; set; }

        [JsonProperty("contactName")]
        public string ContactName { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("website")]
        public string Website { get; set; }

    }
}
