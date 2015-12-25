using Newtonsoft.Json;
using System.Collections.Generic;

namespace GuildWars2API.Model.Items
{
    internal class Skin
    {
        [JsonProperty("id")]
        public int ID { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Possible values (value, explanation):
        /// ShowInWardrobe – When displayed in the account wardrobe (set for all skins listed in the API).
        /// NoCost – When applying the skin is free.
        /// HideIfLocked – When the skin is hidden until it is unlocked.
        /// </summary>
        [JsonProperty("flags")]
        public List<string> Flags { get; set; }

        /// <summary>
        /// Possible values (value, explanation):
        /// Armor, Weapon, Back
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        /// Race restrictions that apply to the skin, e.g. "Human"
        /// </summary>
        [JsonProperty("restrictions")]
        public List<string> Restrictions { get; set; }

        [JsonProperty("icon")]
        public string Icon { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("details")]
        public Details Details { get; set; }
    }
}