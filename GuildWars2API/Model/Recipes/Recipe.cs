using Newtonsoft.Json;
using System.Collections.Generic;

namespace GuildWars2API.Model.Recipes
{
    public class Recipe
    {
        [JsonProperty("id")]
        public int ID { get; set; }

        /// <summary>
        /// Possible values:
        /// - Weapon recipes: Axe, Dagger, Focus, Greatsword, Hammer, Harpoon, LongBow, Mace, Pistol, Rifle, Scepter, Shield, ShortBow, Speargun, Staff, Sword, Torch, Trident, Warhorn
        /// - Armor recipes: Boots, Coat, Gloves, Helm, Leggings, Shoulders
        /// - Trinket recipes: Amulet, Earring, Ring
        /// - Food recipes: Dessert, Feast, IngredientCooking, Meal, Seasoning, Snack, Soup
        /// - Crafting component recipes: Component, Inscription, Insignia
        /// - Refinement recipes: Refinement, RefinementEctoplasm, RefinementObsidian
        /// - Other recipes: Backpack, Bag, Bulk, Consumable, Dye, Potion, UpgradeComponent
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("output_item_id")]
        public int OutputItemID { get; set; }

        [JsonProperty("output_item_count")]
        public string OutputItemCount { get; set; }

        [JsonProperty("time_to_craft_ms")]
        public int TimeToCraftInMs { get; set; }

        /// <summary>
        /// Possible values:
        /// Artificer, Armorsmith, Chef, Huntsman
        /// Jeweler, Leatherworker, Tailor, Weaponsmith
        /// !!! In case of external request(http://gw2profits.com/json/): Mystic Forge, Buy, Double Click, Salvage, Charge, Achievement
        /// </summary>
        [JsonProperty("disciplines")]
        public List<string> Disciplines { get; set; }

        [JsonProperty("min_rating")]
        public int MinRating { get; set; }

        /// <summary>
        /// Possible value (value, explanation):
        /// AutoLearned – Indicates that a recipe automatically unlocks upon reaching the required discipline rating.
        /// LearnedFromItem – Indicates that a recipe is unlocked by consuming a recipe sheet.
        /// /// !!! In case of external request(http://gw2profits.com/json/): Promotion - Indicates that is item is able to be crafted by promoting lower tier ingredients.
        /// </summary>
        [JsonProperty("flags")]
        public List<string> Flags { get; set; }

        [JsonProperty("ingredients")]
        public List<Ingredient> Ingredients { get; set; }

        /// <summary>
        /// Optional property. This only is used when getting mystic forge recipes from an external site (http://gw2profits.com/json/)
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}