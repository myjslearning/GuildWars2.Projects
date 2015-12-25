using Newtonsoft.Json;

namespace GuildWars2API.Model.Account
{
    public class WalletCurrency
    {
        [JsonProperty("id")]
        public int ID { get; set; }

        [JsonProperty("value")]
        public int Value { get; set; }
    }
}