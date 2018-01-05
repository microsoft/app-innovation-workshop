using System;
using Microsoft.Azure.Search.Models;
using Newtonsoft.Json;

namespace ContosoMaintenance.WebAPI.Models
{
    [SerializePropertyNamesAsCamelCase]
    public class BaseModel
    {
        [System.ComponentModel.DataAnnotations.Key]
        public string Id { get; set; }    

        [JsonProperty("createdAt")]
        public DateTime CreatedAt { get; set; }    


    }
}

