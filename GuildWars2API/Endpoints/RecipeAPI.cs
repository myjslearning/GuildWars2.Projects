using GuildWars2API.Model.Recipes;
using GuildWars2API.Network;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace GuildWars2API
{
    public static class RecipeAPI
    {
        public static List<Recipe> RecipesForItem(int itemID) {
            string response = NetworkManager.UnauthorizedRequest(URLBuilder.GetRecipesByItemID(itemID));
            if(response.Length > 0) {
                List<int> recipeIDs = JsonConvert.DeserializeObject<List<int>>(response);
                return GetRecipe(new HashSet<int>(recipeIDs));
            }
            return null;
        }

        public static Recipe GetRecipe(int recipeID) {
            string response = NetworkManager.UnauthorizedRequest(URLBuilder.GetRecipeByRecipeID(recipeID));
            if(response.Length > 0) {
                return JsonConvert.DeserializeObject<Recipe>(response);
            }
            return null;
        }

        /// <summary>
        /// Gets the recipe with the corresponding recipeID
        /// </summary>
        /// <param name="recipeIDs"></param>
        /// <returns></returns>
        public static List<Recipe> GetRecipe(HashSet<int> recipeIDs) => NetworkManager.GetLargeRequest<Recipe>(new List<int>(recipeIDs), "recipes");
    }
}
