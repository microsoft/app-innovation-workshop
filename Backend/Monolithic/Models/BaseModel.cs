using System;
using Newtonsoft.Json;

namespace ContosoMaintenance.WebAPI.Models
{
    public class BaseModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }    
    }
}
