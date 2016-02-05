using Newtonsoft.Json;
using System.Collections.Generic;

namespace GuildWars2API.Model.Guild
{
    class Upgrade
    {
        [JsonProperty("id")]
        public int ID { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Possible values (value, explanation):
        /// AccumulatingCurrency – Used for mine capacity upgrades
        /// BankBag – Used for guild bank upgrades. Additional fields include: 
        ///     BagMaxItems – The maximum item slots of the guild bank tab
        ///     BagMaxCoins – The maximum amount of coins that can be stored in the bank tab
        /// Boost – Used for permanent guild buffs such as waypoint cost reduction
        /// Claimable – Used for guild WvW tactics
        /// Consumable – Used for banners and guild siege
        /// Decoration – Used for decorations that must be crafted by a Scribe
        /// Hub – Used for the Guild Initiative office unlock
        /// Unlock – Used for permanent unlocks, such as merchants and arena decorations
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("bag_max_items")]
        public int BagMaxItems { get; set; }

        /// <summary>
        /// Optional - Only used when Type is BankBag
        /// </summary>
        [JsonProperty("bag_max_coins")]
        public int BagMaxCoins { get; set; }

        /// <summary>
        /// Optional - Only used when Type is BankBag
        /// </summary>
        [JsonProperty("icon")]
        public string Icon { get; set; }

        [JsonProperty("build_time")]
        public int BuildTime { get; set; }

        [JsonProperty("required_level")]
        public int RequiredLevel { get; set; }

        [JsonProperty("experience")]
        public int Experience { get; set; }

        [JsonProperty("prequisites")]
        public List<int> Prequisites { get; set; }

        [JsonProperty("costs")]
        public List<UpgradeCost> Costs { get; set; }
    }
}
