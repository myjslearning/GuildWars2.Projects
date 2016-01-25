using GuildWars2.Collections;
using GuildWars2API.Model.Items;
using System.Threading;
using System.Windows.Controls;

namespace GuildWars2.Windows.Pages
{
    /// <summary>
    /// Interaction logic for Craftin1g.xaml
    /// </summary>
    public partial class Crafting : Page
    {
        private TreeViewObservableCollection treeviewItemSource;

        public Crafting() {
            InitializeComponent();
            treeviewItemSource = new TreeViewObservableCollection();
            //Crafting_Treeview.ItemsSource = treeviewItemSource;
            
            /*RecipeTreeNode tree = new RecipeTreeNode(ItemAPI.SearchItem("Tempered Spinal Blades")[0].ID);
            treeviewItemSource.Add(tree as DisplayRecipeTree);*/
        }

        public void StartCrafting(Item item) {
            Thread thread = new Thread(new ThreadStart(delegate {
                if(item.GetType() == typeof(Item)) {
                    //SHOW LOADING ICON
                    //treeviewItemSource.PopulateTree(item as Item);
                    //HIDE LOADING ICON
                }
            }));
            thread.Name = "Thread_PopulateCraftingTree";
            thread.Start();
        }
    }
}