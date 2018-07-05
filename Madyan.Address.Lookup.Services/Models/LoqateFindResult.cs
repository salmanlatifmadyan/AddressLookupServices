using Newtonsoft.Json;

namespace Madyan.Address.Lookup.Services.Models
{
    public class LoqateFindResult
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("highlight")]
        public string Highlight { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }
}
