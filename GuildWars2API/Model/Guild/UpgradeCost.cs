using Newtonsoft.Json;

namespace GuildWars2API.Model.Guild
{
    public class UpgradeCost
    {
        /// <summary>
        /// Possible values:
        /// Item, Collectible, Currency
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("item_id")]
        public int ItemID { get; set; }
    }
}