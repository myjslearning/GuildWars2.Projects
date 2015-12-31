using Newtonsoft.Json;

namespace GuildWars2API.Model.Recipes
{
    public class Ingredient
    {
        [JsonProperty("item_id")]
        public int ItemID { get; set; }

        [JsonProperty("count")]
        public int Count { get; set; }
    }
}