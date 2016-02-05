using Newtonsoft.Json;

namespace GuildWars2API.Model.Guild
{
    class Member
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("rank")]
        public string Rank { get; set; }

        [JsonProperty("joined")]
        public string Joined { get; set; }  //TODO ISO-8601
    }
}
