using Newtonsoft.Json;
using System.Collections.Generic;

namespace GuildWars2API.Model.Account
{
    internal class Account
    {
        [JsonProperty("id")]
        public string ID { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("world")]
        public int World { get; set; }

        [JsonProperty("guilds")]
        public List<string> Guilds { get; set; }
    }
}