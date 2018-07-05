using Newtonsoft.Json;

namespace Karakoram.Address.Lookup.Services.Models
{
    public class LoqateRetrieveResult
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("company")]
        public string Company { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("line1")]
        public string Line1 { get; set; }

        [JsonProperty("line2")]
        public string Line2 { get; set; }

        [JsonProperty("line3")]
        public string Line3 { get; set; }

        [JsonProperty("line4")]
        public string Line4 { get; set; }

        [JsonProperty("line5")]
        public string Line5 { get; set; }

        [JsonProperty("province")]
        public string Province { get; set; }

        [JsonProperty("postalCode")]
        public string PostalCode { get; set; }
    }
}
