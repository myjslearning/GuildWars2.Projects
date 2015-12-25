using Newtonsoft.Json;
using System.Collections.Generic;

namespace GuildWars2API.Model.Items
{
    public class Details
    {
        /// <summary>
        /// Used in (value, explanation):
        /// Armor, Consumable, Container, Gathering tools, Gizmo
        /// Salvage kits, Trinkets, Upgrade Components, Weapon
        ///
        /// Possible values for Armor:
        /// Boots – Feet slot, Coat – Chest slot, Gloves – Hands slot
        /// Helm – Helm slot, HelmAquatic – Breathing apparatus slot
        /// Leggings – Legs slot, Shoulders – Shoulders slot
        ///
        /// Possible values for Consumable:
        /// AppearanceChange – For Total Makeover Kits, Self-Style Hair Kits, and Name Change Contracts
        /// Booze – Alcohol consumables, Utility – Utility boosts(Potions etc.)
        /// ContractNpc – For Trading Post Express, Merchant Express, Golem Banker
        /// Food – Food consumables, Generic – Various consumables
        /// Halloween – Some boosters, Transmutation – Skin consumables
        /// Immediate – Consumables granting immediate effect(most boosters, Heavy Tome of Knowledge)
        /// Unlock – Unlock consumables, UpgradeRemoval – For Upgrade Extractor
        ///
        /// Possible values for Container:
        /// Default
        /// GiftBox – For some presents and most dye kits
        /// OpenUI – For containers that have their own UI when opening(Black Lion Chest)
        ///
        /// Possible values for Gathering tools:
        /// Foraging – For harvesting sickles
        /// Logging – For logging axes
        /// Mining – For mining picks
        ///
        /// Possible values for Gizmo:
        /// Default
        /// ContainerKey – For Black Lion Chest Keys.
        /// RentableContractNpc – For time-limited NPC services(e.g.Golem Banker, Personal Merchant Express)
        /// UnlimitedConsumable – For Permanent Self-Style Hair Kit
        ///
        /// Possible values for Salvage kits:
        /// Salvage
        ///
        /// Possible values for Trinket:
        /// Accessory – Accessory, Amulet – Amulet, Ring – Ring
        ///
        /// Possible values for Upgrade component:
        /// Default – Infusions and Jewels(and historical PvP runes/sigils)
        /// Gem – Universal upgrades(Gemstones, Doubloons, and Marks/Crests/etc.)
        /// Rune – Rune, Sigil – Sigil
        ///
        /// Possible values for Weapon:
        /// - One-handed main hand:
        /// Axe, Dagger, Mace, Pistol, Scepter, Sword
        /// - One-handed off hand:
        /// Focus, Shield, Torch, Warhorn
        /// - Two-handed:
        /// Greatsword, Hammer, LongBow, Rifle, ShortBow, Staff
        /// - Aquatic:
        /// Harpoon, Speargun, Trident
        /// - Other:
        /// LargeBundle, SmallBundle, Toy, TwoHandedToy
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        /// Used in:
        /// Armor
        ///
        /// Possible values:
        /// Heavy, Medium, Light, Clothing
        /// </summary>
        [JsonProperty("weight_class")]
        public string WeightClass { get; set; }

        /// <summary>
        /// Used in:
        /// Armor, Weapons
        /// </summary>
        [JsonProperty("defense")]
        public int Defense { get; set; }

        /// <summary>
        /// Used in:
        /// Armor, Back Item, Trinket, Weapon
        /// </summary>
        [JsonProperty("infusion_slots")]
        public List<Infusion> InfusionSlots { get; set; }

        /// <summary>
        /// Used in:
        /// Armor, Back item, Trinket, Upgrade Component, Weapon
        /// </summary>
        [JsonProperty("infix_upgrade")]
        public Infix InfixUpgrade { get; set; }

        /// <summary>
        /// Used in:
        /// Armor, Back item, Trinket, Weapon
        /// </summary>
        [JsonProperty("suffix_item_id")]
        public int SuffixItemID { get; set; }

        /// <summary>
        /// Used in:
        /// Weapon, Back item, Trinket, Weapon
        /// </summary>
        [JsonProperty("secondary_suffix_item_id")]
        public string SecondarySuffixItemID { get; set; }

        /// <summary>
        /// Used in:
        /// Bag
        /// </summary>
        [JsonProperty("size")]
        public int Size { get; set; }

        /// <summary>
        /// Used in:
        /// Bag
        /// </summary>
        [JsonProperty("no_sell_or_sort")]
        public bool NoSellOrSort { get; set; }

        /// <summary>
        /// Used in:
        /// Consumable
        /// </summary>
        [JsonProperty("duration_ms")]
        public int duration_ms { get; set; }

        /// <summary>
        /// Used in:
        /// Consumable
        ///
        /// Possible values for Consumable(value, explanation):
        /// BagSlot – For Bag Slot Expansion
        /// BankTab – For Bank Tab Expansion
        /// CollectibleCapacity – For Storage Expander
        /// CraftingRecipe – Crafting recipes
        /// Content – Finishers and Collection unlocks, and Commander's Compendium
        /// Dye – Dyes, Unknown – Outfits
        /// </summary>
        [JsonProperty("unlock_type")]
        public string UnlockType { get; set; }

        /// <summary>
        /// Used in:
        /// Consumable
        /// </summary>
        [JsonProperty("color_id")]
        public int ColorID { get; set; }

        /// <summary>
        /// Used in:
        /// Consumable
        /// </summary>
        [JsonProperty("recipe_id")]
        public int RecipeID { get; set; }

        /// <summary>
        /// Used in:
        /// Salvage kits
        /// </summary>
        [JsonProperty("charges")]
        public int Charges { get; set; }

        /// <summary>
        /// Used in:
        /// Upgrade component
        ///
        /// Possible values for Upgrade component:
        /// - Weapons:
        ///     Axe, Dagger, Focus,Greatsword, Hammer, Harpoon, LongBow
        /// Mace, Pistol, Rifle, Scepter, Shield, ShortBow, Speargun
        ///     Staff, Sword, Torch, Trident, Warhorn
        /// - Armor:
        ///     HeavyArmor, MediumArmor, LightArmor
        /// - Trinkets:
        ///     Trinket
        /// </summary>
        [JsonProperty("flags")]
        public List<string> Flags { get; set; }

        /// <summary>
        /// Used in:
        /// Upgrade component
        ///
        /// Possible values for Upgrade component(value, explanation):
        /// Defense – Defensive infusion
        /// Offense – Offensive infusion
        /// Utility – Utility infusion
        /// </summary>
        [JsonProperty("infusion_upgrade_flags")]
        public List<string> InfusionUpgradeFlags { get; set; }

        /// <summary>
        /// Used in:
        /// Upgrade component
        /// </summary>
        [JsonProperty("suffix")]
        public string Suffix { get; set; }

        /// <summary>
        /// Used in:
        /// Upgrade component
        /// </summary>
        [JsonProperty("bonuses")]
        public List<string> Bonuses { get; set; }

        /// <summary>
        /// Used in:
        /// Weapon
        ///
        /// Possible values for Weapon(value, explanation):
        /// Fire – Fire damage
        /// Ice – Ice damage
        /// Lightning – Lighting damage
        /// Physical – Physical damage
        /// Choking – (not used)
        /// </summary>
        [JsonProperty("damage_type")]
        public string DamageType { get; set; }

        /// <summary>
        /// Used in:
        /// Weapon
        /// </summary>
        [JsonProperty("min_power")]
        public int MinPower { get; set; }

        /// <summary>
        /// Used in:
        /// Weapon
        /// </summary>
        [JsonProperty("max_power")]
        public int MaxPower { get; set; }
    }
}