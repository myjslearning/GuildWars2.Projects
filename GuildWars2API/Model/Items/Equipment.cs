using Newtonsoft.Json;
using System.Collections.Generic;

namespace GuildWars2API.Model.Items
{
    public class Equipment
    {
        [JsonProperty("id")]
        public int ID { get; set; }

        /// <summary>
        /// Possible values(value, explanation):
        /// HelmAquatic
        /// Backpack, Coat, Boots, Gloves, Helm, Leggings, Shoulders,
        /// Accessory1, Accessory2, Ring1, Ring2, Amulet
        /// WeaponAquaticA, WeaponAquaticB, Sickle, Axe, Pick
        /// WeaponA1 - primary mainhand
        /// WeaponA2 - primary offhand
        /// WeaponB1 - secondary mainhand
        /// WeaponB2 - secondary offhand
        /// </summary>
        [JsonProperty("slot")]
        public string Slot { get; set; }

        [JsonProperty("upgrades")]
        public List<int> Upgrades { get; set; }

        [JsonProperty("infusions")]
        public List<int> Infusions { get; set; }

        [JsonProperty("skin")]
        public int Skin { get; set; }
    }
}