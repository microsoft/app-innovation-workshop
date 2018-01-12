
using Newtonsoft.Json;

namespace ContosoFieldService.Models
{
    public class Part : BaseModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("manufacturer")]
        public string Manufacturer { get; set; }

        [JsonProperty("modelNumber")]
        public string ModelNumber { get; set; }

        [JsonProperty("serialNumber")]
        public string SerialNumber { get; set; }

        [JsonProperty("partNumber")]
        public string PartNumber { get; set; }

        [JsonProperty("priceInUSD")]
        public decimal PriceInUSD { get; set; }

        [JsonProperty("imageSource")]
        public string ImageSource { get; set; }
    }
}
