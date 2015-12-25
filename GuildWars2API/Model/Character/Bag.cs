using GuildWars2API.Model.Items;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace GuildWars2API.Model.Character
{
    public class Bag
    {
        [JsonProperty("id")]
        public int ID { get; set; }

        [JsonProperty("size")]
        public int Size { get; set; }

        [JsonProperty("inventory")]
        public List<ItemStack> Inventory { get; set; }
    }
}