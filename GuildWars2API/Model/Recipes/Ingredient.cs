using Newtonsoft.Json;

namespace GuildWars2API.Model.Recipes
{
    public class Ingredient
    {
        [JsonProperty("item_id")]
        public int ID { get; set; }

        [JsonProperty("count")]
        public int Count { get; set; }
    }
}