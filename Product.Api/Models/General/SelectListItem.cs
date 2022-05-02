using Newtonsoft.Json;

namespace Product.Api.Models.General
{
    public class SelectListItem
    {
        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

    }
}
