using GuildWars2API.Model.Items;
using GuildWars2API.Model.Recipes;
using GuildWars2API.Network;
using System.Collections.Generic;
using System.Linq;
using static GuildWars2API.Network.NetworkManager;
using static Newtonsoft.Json.JsonConvert;

namespace GuildWars2API
{
    public static class RecipeAPI
    {
        public static HashSet<int> RecipesForItem(int itemID) {
            string response = UnauthorizedRequest(URLBuilder.GetRecipesByItemID(itemID));
            if(response.Length > 0) {
                return DeserializeObject<HashSet<int>>(response);
            }
            return null;
        }

        public static Recipe GetRecipe(int recipeID) {
            string response = UnauthorizedRequest(URLBuilder.GetRecipeByID(recipeID));
            if(response.Length > 0) {
                return DeserializeObject<Recipe>(response);
            }
            return null;
        }

        public static List<Recipe> GetRecipe(HashSet<int> recipeIDs) => GetLargeRequest<Recipe>(recipeIDs, "recipes");

        public static RecipeTreeNode GetRecipeTree(Item item) => new RecipeTreeNode(item.ID);

        #region Mystic Forge

        public static List<Recipe> GetMysticForgeRecipe(int itemID) {
            List<Recipe> mysticForgeRecipes = ReadMysticForgeRecipes();
            return mysticForgeRecipes.Where(r => r.OutputItemID == itemID).ToList();
        }

        public static List<Recipe> GetMysticForgeRecipe(HashSet<int> itemIDs) {
            List<Recipe> mysticForgeRecipes = ReadMysticForgeRecipes();
            return mysticForgeRecipes.Where(r => itemIDs.Contains(r.OutputItemID)).ToList();
        }

        public static List<Recipe> GetMysticForgeRecipe(string name) {
            List<Recipe> mysticForgeRecipes = ReadMysticForgeRecipes();
            return mysticForgeRecipes.Where(r => r.Name.Equals(name)).ToList();
        }

        private static List<Recipe> ReadMysticForgeRecipes(bool useLocalRecipes = true) {
            try {
                if(useLocalRecipes) {
                    string test = Properties.Resources.MysticForgeRecipes;
                    return DeserializeObject<List<Recipe>>(Properties.Resources.MysticForgeRecipes);
                }
                else {
                    string response = NetworkManager.UnauthorizedRequest(URLBuilder.GetMysticForgeRecipes());
                    if(response.Length > 0) {
                        return DeserializeObject<List<Recipe>>(response);
                    }
                }
            }
            catch { }
            return new List<Recipe>();
        }

        #endregion Mystic Forge
    }
}
