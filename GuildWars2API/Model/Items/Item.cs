using Newtonsoft.Json;
using System.Collections.Generic;

namespace GuildWars2API.Model.Items
{
    public class Item
    {
        [JsonProperty("id")]
        public int ID { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("icon")]
        public string Icon { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// Possible values (value, explanation):
        /// Armor – Armor
        /// Back – Back item, Bag – Bags
        /// Consumable – Consumables
        /// Container – Containers
        /// CraftingMaterial – Crafting materials
        /// Gathering – Gathering tools
        /// Gizmo – Gizmos
        /// MiniPet – Miniatures
        /// Tool – Salvage kits
        /// Trait – Trait guides
        /// Trinket – Trinkets
        /// Trophy – Trophies
        /// UpgradeComponent – Upgrade components
        /// Weapon – Weapons
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        /// Possible values:
        /// Junk, Basic, Fine, Masterwork, Rare
        /// Exotic, Ascended, Legendary
        /// </summary>
        [JsonProperty("rarity")]
        public string Rarity { get; set; }

        [JsonProperty("level")]
        public int Level { get; set; }

        [JsonProperty("vendor_value")]
        public int VendorValue { get; set; }

        [JsonProperty("default_skin")]
        public int DefaultSkin { get; set; }

        /// <summary>
        /// Skin applied to the Item, if it's different from the original
        /// </summary>
        [JsonProperty("skin")]
        public int Skin { get; set; }

        /// <summary>
        /// Possible values (value, explanation):
        /// AccountBindOnUse – Account bound on use
        /// AccountBound – Account bound on acquire
        /// HideSuffix – Hide the suffix of the upgrade component
        /// MonsterOnly
        /// NoMysticForge – Not usable in the Mystic Forge
        /// NoSalvage – Not salvageable
        /// NoSell – Not sellable
        /// NotUpgradeable – Not upgradeabl
        /// NoUnderwater – Not available underwater
        /// SoulbindOnAcquire – Soulbound on acquire
        /// SoulBindOnUse – Soulbound on use
        /// Unique – Unique
        /// </summary>
        [JsonProperty("flags")]
        public List<string> Flags { get; set; }

        /// <summary>
        /// Asura, Charr, Human, Norn, Sylvari
        /// Guardian, Mesmer, Ranger, Warrior
        /// </summary>
        [JsonProperty("restrictions")]
        public List<string> Restrictions { get; set; }

        /// <summary>
        /// Activity – Usable in activities
        /// Dungeon – Usable in dungeons
        /// Pve – Usable in general PvE
        /// Pvp – Usable in PvP
        /// PvpLobby – Usable in the Heart of the Mists
        /// Wvw – Usable in World vs.World
        /// </summary>
        [JsonProperty("game_types")]
        public List<string> GameTypes { get; set; }

        [JsonProperty("details")]
        public Details Details { get; set; }
    }
}