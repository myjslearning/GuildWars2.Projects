using Newtonsoft.Json;

namespace GuildWars2API.Model.Commerce
{
    public class WalletCurrency
    {
        [JsonProperty("id")]
        public int ID { get; set; }

        [JsonProperty("value")]
        public int Value { get; set; }
    }
}