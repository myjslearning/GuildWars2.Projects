using GuildWars2.Collections;
using GuildWars2.Other;
using GuildWars2API;
using GuildWars2API.Model.Items;
using GuildWars2API.Model.Recipes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace GuildWars2.Model
{
    public class DisplayTreeItemRecipe : INotifyPropertyChanged
    {
        private Item _item;
        private string _amountCrafted;
        private object _lockObj = new object();

        public int ID { get; set; }

        public int ItemID { get; set; }

        public Item Item {
            get {
                if(_item == null) {
                    lock (_lockObj) {
                        if(_item == null) {
                            if(ItemID == 0 && Recipes.Count >= CurrentRecipeIndex + 1) {
                                _item = ItemAPI.GetItem(Recipes[CurrentRecipeIndex].OutputItemID);
                            }
                            else {
                                _item = ItemAPI.GetItem(ItemID);
                            }
                        }
                    }
                }
                return _item;
            }
            set {
                _item = value;
            }
        }

        public string DisplayType {
            get {
                if(_item != null) {
                    return AddSpacesToSentence(_item.Type, true);
                }
                return string.Empty;
            }
        }

        public string LevelColor => ColorManager.GreenToRed(Item.Level, 0, 80).ToString();

        public string RarityColor => ColorManager.GetItemColor(Item.Rarity);

        public bool ShowSingleAmount => !(SingleAmountNeeded == TotalAmountNeeded);

        public bool ShowCrafingProgress => !(ID == -1);

        public string CraftedCompletedIcon {
            get {
                int result;
                if(Int32.TryParse(AmountCrafted, out result)) {
                    if(result >= TotalAmountNeeded) {
                        return "pack://application:,,,/Resources/Images/Icons/check_green.png";
                    }
                }
                return "pack://application:,,,/Resources/Images/Icons/cross_red.png";
            }
        }

        public List<Recipe> Recipes { get; set; }

        public int TotalAmountNeeded { get; set; }

        public int SingleAmountNeeded { get; set; }

        public string AmountCrafted {
            get {
                return _amountCrafted;
            }
            set {
                _amountCrafted = value;
                NotifyPropertyChanged("AmountCrafted");
                NotifyPropertyChanged("CraftedCompletedIcon");
            }
        }

        public int CurrentRecipeIndex { get; set; }

        public PropChangeObservableCollection<DisplayTreeItemRecipe> Items { get; set; }

        public DisplayTreeItemRecipe() {
        }

        public DisplayTreeItemRecipe(int itemID, int singleAmount, int totalAmount) {
            this.ItemID = itemID;
            this.SingleAmountNeeded = singleAmount;
            this.TotalAmountNeeded = totalAmount;
            this.AmountCrafted = "0";
        }

        public DisplayTreeItemRecipe(Recipe recipe, int singleAmount, int totalAmount)
            : this(new List<Recipe>() { recipe }, singleAmount, totalAmount) { }

        public DisplayTreeItemRecipe(List<Recipe> recipes, int singleAmount, int totalAmount) {
            this.Items = new PropChangeObservableCollection<DisplayTreeItemRecipe>();
            this.Recipes = recipes;
            this.CurrentRecipeIndex = 0;
            this.SingleAmountNeeded = singleAmount;
            this.TotalAmountNeeded = totalAmount;
            this.AmountCrafted = "0";

            CreateChildren();
        }

        /// <summary>
        /// Initializes all childeren that initialize themselfs.
        /// </summary>
        private void CreateChildren() {
            if(Recipes.Count >= CurrentRecipeIndex + 1) {
                foreach(Ingredient ingredient in Recipes[CurrentRecipeIndex].Ingredients) {
                    List<Recipe> results = RecipeAPI.AvailableRecipes(ingredient.ID);
                    if(results != null && results.Count > 0) {
                        this.Items.Add(new DisplayTreeItemRecipe(results, ingredient.Count, ingredient.Count * this.TotalAmountNeeded));
                    }
                    else {
                        this.Items.Add(new DisplayTreeItemRecipe(ingredient.ID, ingredient.Count, ingredient.Count * this.TotalAmountNeeded));
                    }
                }
            }
        }

        /// <summary>
        /// Adds spaces before mid-sentence capital characters. Preserves acronyms.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="preserveAcronyms"></param>
        /// <returns></returns>
        private string AddSpacesToSentence(string text, bool preserveAcronyms) {
            if(string.IsNullOrWhiteSpace(text)) {
                return string.Empty;
            }
            StringBuilder newText = new StringBuilder(text.Length * 2);
            newText.Append(text[0]);
            for(int i = 1; i < text.Length; i++) {
                if(char.IsUpper(text[i])) {
                    if((text[i - 1] != ' ' && !char.IsUpper(text[i - 1]))
                        || (preserveAcronyms && char.IsUpper(text[i - 1])
                        && i < text.Length - 1 && !char.IsUpper(text[i + 1]))) {
                        newText.Append(' ');
                    }
                }
                newText.Append(text[i]);
            }
            return newText.ToString();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "") {
            if(PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}