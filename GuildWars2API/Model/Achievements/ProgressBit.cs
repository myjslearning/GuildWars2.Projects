using Newtonsoft.Json;

namespace GuildWars2API.Model.Achievements
{
    public class ProgressBit
    {
        /// <summary>
        /// Possible values:
        /// Text, Item, Minipet, Skin
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        /// Optional, only used when Type is Item, Mini or Skin IF applicable
        /// </summary>
        [JsonProperty("id")]
        public int ID { get; set; }

        /// <summary>
        /// Optional, only used when Type is Text
        /// </summary>
        [JsonProperty("text")]
        public int Text { get; set; }
    }
}