using Newtonsoft.Json;

namespace Advania.ProductApi.Models
{
    public class Product
    {
        [JsonProperty("id")]
        public string Id { get; set; }  // Ensure this property exists

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("price")]
        public decimal Price { get; set; }
    }
}
