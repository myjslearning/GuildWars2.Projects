using Newtonsoft.Json;

namespace GuildWars2API.Model.Achievements
{
    class Dailies
    {
        [JsonProperty("pve")]
        public Daily PVE { get; set; }

        [JsonProperty("pvp")]
        public Daily PVP { get; set; }

        [JsonProperty("wvw")]
        public Daily WVW { get; set; }

        [JsonProperty("special")]
        public Daily Special { get; set; }
    }
}
