using Newtonsoft.Json;

namespace GuildWars2API.Model.Commerce
{
    public class WalletCurrencyInfo
    {
        [JsonProperty("id")]
        public int ID { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// A number that can be used to sort the list of currencies when ordered from least to greatest.
        /// </summary>
        [JsonProperty("order")]
        public string Order { get; set; }

        [JsonProperty("icon")]
        public string Icon { get; set; }
    }
}