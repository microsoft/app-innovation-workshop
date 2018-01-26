using System;
using Microsoft.Azure.Search.Models;
using Newtonsoft.Json;

namespace ContosoMaintenance.WebAPI.Models
{
    [SerializePropertyNamesAsCamelCase]
    public class BaseModel
    {
        [JsonProperty("id")]
        [System.ComponentModel.DataAnnotations.Key]
        public string Id { get; set; }

        [JsonProperty("createdAt")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("isDeleted")]
        public bool IsDeleted { get; set; }
    }
}