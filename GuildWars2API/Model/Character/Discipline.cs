using Newtonsoft.Json;

namespace GuildWars2API.Model.Character
{
    public class Discipline
    {
        /// <summary>
        /// Possible values (value):
        /// Artificer
        /// Armorsmith, Chef, Huntsman, Jeweler
        /// Leatherworker, Tailor, Weaponsmith
        /// </summary>
        [JsonProperty("discipline")]
        public string DisciplineName { get; set; }

        [JsonProperty("rating")]
        public int Rating { get; set; }

        [JsonProperty("active")]
        public bool Active { get; set; }
    }
}