using GuildWars2API.Model.Items;
using GuildWars2API.Model.Recipes;
using GuildWars2API.Network;
using System.Collections.Generic;

using static GuildWars2API.Network.NetworkManager;
using static Newtonsoft.Json.JsonConvert;

namespace GuildWars2API
{
    public static class RecipeAPI
    {
        public static HashSet<int> RecipesForItem(int itemID) {
            string response = UnauthorizedRequest(URLBuilder.GetRecipesByItemID(itemID));
            if(response.Length > 0) {
                DeserializeObject<HashSet<int>>(response);
            }
            return null;
        }

        public static Recipe GetRecipe(int recipeID) {
            string response = UnauthorizedRequest(URLBuilder.GetRecipeByRecipeID(recipeID));
            if(response.Length > 0) {
                return DeserializeObject<Recipe>(response);
            }
            return null;
        }

        public static List<Recipe> GetRecipe(HashSet<int> recipeIDs) => GetLargeRequest<Recipe>(new List<int>(recipeIDs), "recipes");

        public static RecipeTreeNode GetRecipeTree(Item item) => new RecipeTreeNode(item);
    }
}
