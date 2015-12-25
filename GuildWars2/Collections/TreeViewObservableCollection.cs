using GuildWars2.Model;
using GuildWars2API;
using GuildWars2API.Model.Items;
using System.Collections.Generic;

namespace GuildWars2.Collections
{
    public class TreeViewObservableCollection : PropChangeObservableCollection<DisplayTreeItemRecipe>
    {
        public List<DisplayTreeItemRecipe> Trees { get; set; }

        public TreeViewObservableCollection() {
            //Trees = XMLSerialization.Deserialize<DisplayTreeItemRecipe>(IOHandler.GetCraftingRecipesXMLPath); //TODO
        }

        #region Populating

        /// <summary>
        /// Populates the tree.
        /// </summary>
        /// <param name="item"></param>
        public void PopulateTree(Item item) {
            if(item == null)
                return;

            DisplayTreeItemRecipe recipeToAdd;
            if(item.Details != null && item.Details.RecipeID != 0) {
                recipeToAdd = new DisplayTreeItemRecipe(GW2API.GetRecipe(item.Details.RecipeID), 1, 1);
            }
            else {
                recipeToAdd = new DisplayTreeItemRecipe(GW2API.AvailableRecipes(item.ID), 1, 1);
            }

            if(recipeToAdd != null) {
                recipeToAdd.Item = item;
                recipeToAdd.ID = -1;
                AddToTree(recipeToAdd);
            }
        }

        /// <summary>
        /// Adds a new item to the tree on the dispatcher.
        /// </summary>
        /// <param name="item"></param>
        private void AddToTree(DisplayTreeItemRecipe item) {
            App.Current.Dispatcher.Invoke(delegate {
                this.ClearItems();
                this.Add(item);
            });
        }

        #endregion Populating
    }
}