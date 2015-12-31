using System.Collections.Generic;
using System.Linq;

namespace GuildWars2API.Model.Recipes
{
    public class RecipeTreeNode
    {
        public List<Recipe> Recipes { get; set; }

        public int CurrentRecipeIndex { get; set; }

        private Recipe CurrentRecipe {
            get {
                if(Recipes == null || Recipes.Count <= 0)
                    return null;

                if(CurrentRecipeIndex >= Recipes.Count) {
                    return Recipes[0];
                }
                return Recipes[CurrentRecipeIndex];
            }
        }

        public List<RecipeTreeNode> Children { get; set; }

        public RecipeTreeNode(Items.Item item) {
            CurrentRecipeIndex = 0;

            PopulateRecipes(item);
            PopulateChildren();
        }

        private void PopulateRecipes(Items.Item item) {
            if(item.Details != null && item.Details.RecipeID != 0) {
                Recipes = new List<Recipe>() { RecipeAPI.GetRecipe(item.Details.RecipeID) };
            }
            else {
                HashSet<int> recipeIDs = RecipeAPI.RecipesForItem(item.ID);
                if(recipeIDs != null)                          
                    Recipes = RecipeAPI.GetRecipe(recipeIDs);
            }
        }

        private void PopulateChildren() {
            if(CurrentRecipe == null || CurrentRecipe.Ingredients == null)
                return;

            HashSet<int> ingredientsIDs = new HashSet<int>(CurrentRecipe.Ingredients.Select(i => i.ItemID).ToList());
            if(ingredientsIDs != null) {
                Children = new List<RecipeTreeNode>();
                List<Items.Item> items = ItemAPI.GetItem(ingredientsIDs);

                foreach(Items.Item item in items) {
                    Children.Add(new RecipeTreeNode(item));
                }
            }
        }
    }
}
