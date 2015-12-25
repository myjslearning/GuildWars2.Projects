using Newtonsoft.Json;
using System.Collections.Generic;

namespace GuildWars2API.Model.Items
{
    public class Infix
    {
        [JsonProperty("attributes")]
        public List<ItemAttribute> Attributes { get; set; }

        [JsonProperty("buff")]
        public Buff Buff { get; set; }
    }
}