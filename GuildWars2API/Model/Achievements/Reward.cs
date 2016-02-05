using Newtonsoft.Json;

namespace GuildWars2API.Model.Achievements
{
    public class Reward
    {
        /// <summary>
        /// /// Possible values:
        /// Item, Mastery
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        /// Optional, only used when Type is Item
        /// </summary>
        [JsonProperty("id")]
        public int ID { get; set; }

        /// <summary>
        /// Optional, only used when Type is Item
        /// </summary>
        [JsonProperty("count")]
        public int Count { get; set; }

        /// <summary>
        /// Optional, only used when Type is Mastery
        /// Possible values:
        /// Tyria, Maguuma
        /// </summary>
        [JsonProperty("region")]
        public string Region { get; set; }
    }
}