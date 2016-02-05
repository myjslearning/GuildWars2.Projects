using Newtonsoft.Json;
using System.Collections.Generic;

namespace GuildWars2API.Model.Guild
{
    class Emblem
    {
        [JsonProperty("id")]
        public int ID { get; set; }

        [JsonProperty("layers")]
        public List<string> Layers { get; set; }
    }
}
