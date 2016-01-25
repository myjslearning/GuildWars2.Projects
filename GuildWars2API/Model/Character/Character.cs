using GuildWars2API.Model.Items;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace GuildWars2API.Model.Character
{
    public class Character
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Possible values (value):
        /// Asura, Charr, Human, Norn, Sylvari
        /// </summary>
        [JsonProperty("race")]
        public string Race { get; set; }

        /// <summary>
        /// Possible values (value):
        /// Male, Female
        /// </summary>
        [JsonProperty("gender")]
        public string Gender { get; set; }

        /// <summary>
        /// Possible values (value):
        /// Elementalist, Engineer, Guardian, Mesmer, Necromancer,
        /// Ranger, Revenant, Thief, Warrior
        /// </summary>
        [JsonProperty("profession")]
        public string Profession { get; set; }

        [JsonProperty("level")]
        public int Level { get; set; }

        [JsonProperty("guild")]
        public string Guild { get; set; }

        [JsonProperty("created")]
        public string Created { get; set; }

        [JsonProperty("age")]
        public int Age { get; set; }

        [JsonProperty("deaths")]
        public int Deaths { get; set; }

        [JsonProperty("bags")]
        public List<Bag> Bags { get; set; }

        [JsonProperty("equipment")]
        public List<Equipment> Equipment { get; set; }

        [JsonProperty("crafting")]
        public List<Discipline> Crafting { get; set; }

        [JsonProperty("specializations")]
        public SpecializationGroups Specializations { get; set; }
    }
}