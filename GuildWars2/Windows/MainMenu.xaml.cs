using MahApps.Metro.Controls;

namespace GuildWars2.Windows
{
    public partial class MainMenu : MetroWindow
    {
        public MainMenu() {
            InitializeComponent();
        }

        private void MetroWindow_ContextMenuOpening(object sender, System.Windows.Controls.ContextMenuEventArgs e) {
            e.Handled = false;
        }
    }
}
