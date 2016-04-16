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
#pragma warning disable CSE0003 
        public static HashSet<int> RecipesForItem(int itemID) {
            return UnauthorizedRequest<HashSet<int>>(URLBuilder.GetRecipesByItemID(itemID));
        }
        
        public static Recipe GetRecipe(int recipeID) {
            return UnauthorizedRequest<Recipe>(URLBuilder.GetRecipeByID(recipeID));
        }
#pragma warning restore CSE0003 
        public static List<Recipe> GetRecipe(HashSet<int> recipeIDs) => LargeRequest<Recipe>(recipeIDs, "recipes");

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
                    return UnauthorizedRequest<List<Recipe>>(URLBuilder.GetMysticForgeRecipes());
                }
            }
            catch { }
            return new List<Recipe>();
        }

        #endregion Mystic Forge
    }
}
