using Newtonsoft.Json;

namespace GuildWars2API.Model.Commerce
{
    public class GemExchange
    {
        [JsonProperty("coins_per_gem")]
        public int CoinsPerGem { get; set; }

        [JsonProperty("quantity")]
        public int Quantity { get; set; }
    }
}
