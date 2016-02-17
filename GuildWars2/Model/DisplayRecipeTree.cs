using GuildWars2.Collections;
using GuildWars2API.Model.Items;
using GuildWars2API.Model.Recipes;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace GuildWars2.Model
{
    class DisplayRecipeTree : RecipeTreeNode, INotifyPropertyChanged
    {
        //Add display properties
        //Also optimize the loading of the Item Objects. Maybe do this in batch?
        public PropChangeObservableCollection<DisplayRecipeTree> Items { get; set; }

        public DisplayRecipeTree(int itemID, int count = 1) : base(itemID, count) {
            PopulateItems();
        }

        public DisplayRecipeTree(Item item, int count = 1) : base(item, count) {
            PopulateItems();
        }

        public DisplayRecipeTree(RecipeTreeNode node) {
            this.Children = node.Children;
            this.Count = node.Count;
            this.Item = node.Item;
            this.ItemID = node.ItemID;
            this.Recipe = node.Recipe;
            this.RecipeIDs = node.RecipeIDs;
            PopulateItems();
        }

        private void PopulateItems() {
            if(Children == null || Children.Count <= 0)
                return;

            var items = new PropChangeObservableCollection<DisplayRecipeTree>();
            Children.ForEach(c => { items.Add(new DisplayRecipeTree(c)); });
            Items = items;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "") {
            if(PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
