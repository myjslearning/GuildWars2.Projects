using Newtonsoft.Json;

namespace GuildWars2API.Model.Guild
{
    public class GuildDetails
    {
        [JsonProperty("guild_id")]
        public string GuildID { get; set; }

        [JsonProperty("guild_name")]
        public string GuildName { get; set; }

        [JsonProperty("tag")]
        public string Tag { get; set; }

        [JsonProperty("emblem")]
        public object Emblem { get; set; }      //TODO
    }
}
