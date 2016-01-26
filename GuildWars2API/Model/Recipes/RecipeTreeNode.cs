using GuildWars2API.Model.Items;
using System.Collections.Generic;

namespace GuildWars2API.Model.Recipes
{
    public class RecipeTreeNode
    {
        private Item _item;
        private Recipe _currentRecipe;
        private int _currentRecipeIndex = 0;
        
        public List<int> RecipeIDs { get; set; }
        public Recipe Recipe {
            get {
                if(_currentRecipe == null && _currentRecipeIndex < RecipeIDs.Count)
                    Recipe = RecipeAPI.GetRecipe(RecipeIDs[_currentRecipeIndex]);

                return _currentRecipe;
            }
            set { _currentRecipe = value; }
        }

        public int ItemID { get; set; }
        public Item Item {
            get {
                if(_item == null)
                    Item = ItemAPI.GetItem(ItemID);

                return _item;
            }
            set { _item = value; }
        }
        public int Count { get; set; }

        public List<RecipeTreeNode> Children { get; set; }

        public RecipeTreeNode(Item item, int count = 1) : this(item.ID, count) {
            Item = item;
        }

        public RecipeTreeNode(int itemID, int count = 1) 
        {
            Count = count;
            ItemID = itemID;

            PopulateRecipes();
            PopulateChildren();
        }

        protected RecipeTreeNode() { }

        private void PopulateRecipes() {
            //First check RecipeAPI for recipe
            //Check if its not a promotion Item and check mystic forge
            //Get Item object and search for recipe in item

            RecipeIDs = new List<int>(RecipeAPI.RecipesForItem(ItemID));
            if(RecipeIDs != null && RecipeIDs.Count > 0) {
                var test = Recipe; //Cheaty way to load recipe for debugging purpose
            }
            else if((RecipeIDs == null || RecipeIDs.Count <= 0) && !ItemAPI.IsPromotionItem(ItemID)) {
                List<Recipe> mysticForgeRecipes = RecipeAPI.GetMysticForgeRecipe(ItemID);
                if(mysticForgeRecipes.Count <= 0)
                    return;

                foreach(Recipe recipe in mysticForgeRecipes) {
                    RecipeIDs.Add(recipe.ID);
                }
                Recipe = mysticForgeRecipes[0];
            }
            else {
                if(Item.Details != null && Item.Details.RecipeID != 0) {
                    Recipe = RecipeAPI.GetRecipe(Item.Details.RecipeID);
                    if(Recipe != null)
                        RecipeIDs = new List<int>() { Recipe.ID };
                }
            }
        }

        private void PopulateChildren() {
            if(Recipe == null || Recipe.Ingredients == null)
                return;

            foreach(Ingredient ingredient in Recipe.Ingredients) {
                if(ingredient.ItemID < 0) 
                    return;

                if(Children == null)
                    Children = new List<RecipeTreeNode>();

                Children.Add(new RecipeTreeNode(ingredient.ItemID, ingredient.Count));
            }
        }

        public void ChangeRecipe() {
            if(RecipeIDs == null || RecipeIDs.Count <= 1)
                return;

            if(_currentRecipeIndex + 1 < RecipeIDs.Count) {
                _currentRecipeIndex = _currentRecipeIndex + 1;
                _currentRecipe = null;
            }
            else if(_currentRecipeIndex + 1 == RecipeIDs.Count) {
                _currentRecipeIndex = 0;
                _currentRecipe = null;
            }

            Children.Clear();
            PopulateChildren();
        }
    }
}