using Newtonsoft.Json;

namespace GuildWars2API.Model.Items
{
    internal class ItemSearch
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("item_id")]
        public int ItemID { get; set; }
    }
}