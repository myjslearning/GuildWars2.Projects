using Newtonsoft.Json;

namespace GuildWars2API.Model.Achievements
{
    public class Daily
    {
        [JsonProperty("id")]
        public int ID { get; set; }

        [JsonProperty("level")]
        public LevelRequirement Level { get; set; }
    }
}