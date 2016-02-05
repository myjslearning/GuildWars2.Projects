using Newtonsoft.Json;

namespace GuildWars2API.Model.Guild
{
    public class UpgradeItem
    {
        [JsonProperty("upgrade_id")]
        public int UpgradeID { get; set; }

        [JsonProperty("count")]
        public int Count { get; set; }
    }
}