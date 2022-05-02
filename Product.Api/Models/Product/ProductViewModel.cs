using Newtonsoft.Json;

namespace Product.Api.Models.Product
{
    public class ProductViewModel
    {
        [JsonProperty("guid")]
        public string Guid { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("productGroupNk")]
        public string ProductGroupNk { get; set; }

        [JsonProperty("price")]
        public decimal Price { get; set; }

        [JsonProperty("comments")]
        public string Comments { get; set; }
    }
}
