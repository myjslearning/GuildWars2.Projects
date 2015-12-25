using Newtonsoft.Json;
using System.Collections.Generic;

namespace GuildWars2API.Model.Character
{
    public class SpecializationGroups
    {
        [JsonProperty("pve")]
        public List<Specialization> PvE { get; set; }

        [JsonProperty("pvp")]
        public List<Specialization> PvP { get; set; }

        [JsonProperty("wvw")]
        public List<Specialization> WvW { get; set; }
    }
}