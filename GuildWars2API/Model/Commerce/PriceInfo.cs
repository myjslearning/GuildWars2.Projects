using Newtonsoft.Json;

namespace GuildWars2API.Model.Commerce
{
    public class PriceInfo
    {
        [JsonProperty("listings")]
        public int Listings { get; set; }

        [JsonProperty("unit_price")]
        public int UnitPrice { get; set; }

        [JsonProperty("quantity")]
        public int Quantity { get; set; }
    }
}