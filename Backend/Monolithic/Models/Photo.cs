using System;
namespace ContosoMaintenance.WebAPI.Models
{
    public class Photo
    {
        public string Id { get; set; }
        public string LargeUrl { get; set; }
        public string MediumUrl { get; set; }
        public string IconUrl { get; set; }
    }
}
