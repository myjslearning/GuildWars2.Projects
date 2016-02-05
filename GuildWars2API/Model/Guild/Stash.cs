using GuildWars2API.Model.Items;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace GuildWars2API.Model.Guild
{
    class Stash
    {
        [JsonProperty("upgrade_id")]
        public int UpgradeID { get; set; }

        [JsonProperty("size")]
        public int Size { get; set; }

        [JsonProperty("coins")]
        public int Coins { get; set; }

        [JsonProperty("note")]
        public string Note { get; set; }

        [JsonProperty("inventory")]
        public List<ItemStack> Inventory { get; set; }
    }
}
