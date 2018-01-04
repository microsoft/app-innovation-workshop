using System;
using Newtonsoft.Json;

namespace ContosoMaintenance.WebAPI.Models
{
    public class Customer : BaseModel
    {
        [JsonProperty ("firstName")]
        public string FirstName { get; set; }

        [JsonProperty ("lastName")]
        public string LastName { get; set; }

        [JsonProperty ("emailAddress")]
        public string EmailAddress { get; set; }

        [JsonProperty ("cellNumber")]
        public string CellNumber { get; set; }

        [JsonProperty("address")]
        public Location Address { get; set; }

    }
}
