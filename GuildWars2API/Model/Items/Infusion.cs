using Newtonsoft.Json;
using System.Collections.Generic;

namespace GuildWars2API.Model.Items
{
    public class Infusion
    {
        /// <summary>
        /// Possible values (value, explanation):
        /// Defense – Defensive infusion slot
        /// Offense – Offensive infusion slot(not used for normal armor)
        /// Utility – Utility infusion slot(only used for trinkets)
        /// </summary>
        [JsonProperty("flags")]
        public List<string> Flags { get; set; }

        [JsonProperty("item_id")]
        public int ItemID { get; set; }
    }
}