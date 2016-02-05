using Newtonsoft.Json;
using System.Collections.Generic;

namespace GuildWars2API.Model.Achievements
{
    class Achievement
    {
        [JsonProperty("id")]
        public int ID { get; set; }

        [JsonProperty("icon")]
        public string Icon { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("requirement")]
        public string Requirement { get; set; }

        /// <summary>
        /// Possible values (value, explanation):
        /// Default - A default achievement
        /// ItemSet - Achievement is linked to Collections
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        /// Possible values (value, explanation):
        /// Pvp - can only get progress in PvP or WvW
        /// CategoryDisplay - is a meta achievement
        /// MoveToTop - affects in-game UI collation
        /// IgnoreNearlyComplete - doesn't appear in the "nearly complete" UI
        /// Repeatable - can be repeated multiple times
        /// </summary>
        [JsonProperty("flags")]
        public List<string> Flags { get; set; }

        [JsonProperty("tiers")]
        public List<Tier> Tiers { get; set; }

        [JsonProperty("rewards")]
        public List<Reward> Rewards { get; set; }

        [JsonProperty("bits")]
        public List<ProgressBit> Bits { get; set; }

        [JsonProperty("points_cap")]
        public int PointsCap { get; set; }
    }
}
