using Newtonsoft.Json;
using System.Collections.Generic;

namespace GuildWars2API.Model.Character
{
    public class Specialization
    {
        [JsonProperty("id")]
        public int ID { get; set; }

        [JsonProperty("traits")]
        public List<string> Traits { get; set; }
    }
}