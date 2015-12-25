using Newtonsoft.Json;

namespace GuildWars2API.Model.Items
{
    public class Buff
    {
        [JsonProperty("skill_id")]
        public string SkillID { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }
}