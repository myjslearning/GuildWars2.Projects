using Newtonsoft.Json;

namespace GuildWars2API.Model.Commerce
{
    public class ItemListing
    {
        [JsonProperty("id")]
        public int ID { get; set; }

        [JsonProperty("whitelisted")]
        public bool Whitelisted { get; set; }

        [JsonProperty("buys")]
        public PriceInfo Buys { get; set; }

        [JsonProperty("sells")]
        public PriceInfo Sells { get; set; }
    }
}