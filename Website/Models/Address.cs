using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContosoMaintenance.Web.Models
{
    public partial class Address
    {
        [JsonProperty("firstLineAddress")]
        public string FirstLineAddress { get; set; }

        [JsonProperty("secondLineAddress")]
        public object SecondLineAddress { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("zipCode")]
        public string ZipCode { get; set; }

        [JsonProperty("point")]
        public Point Point { get; set; }

        [JsonProperty("id")]
        public object Id { get; set; }

        [JsonProperty("createdAt")]
        public DateTimeOffset CreatedAt { get; set; }

        [JsonProperty("isDeleted")]
        public bool IsDeleted { get; set; }
    }
}
