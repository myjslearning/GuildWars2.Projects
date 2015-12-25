using Microsoft.Win32;
using System.Windows;
using System.Windows.Controls;

namespace GuildWars2.Controls
{
    /// <summary>
    /// Interaction logic for PreferenceSettingsControl.xaml
    /// </summary>
    public partial class PreferenceSettingsControl : UserControl
    {
        public PreferenceSettingsControl() {
            InitializeComponent();
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e) {
            //Update settings
        }

        private void NotifyPathButton_Click(object sender, RoutedEventArgs e) {
            string result = OpenFileDialog();
            if(result != null) {
                MainGame_Textbox.Text = result;
            }
        }
        private void MainGameButton_Click(object sender, RoutedEventArgs e) {
            string result = OpenFileDialog();
            if(result != null) {
                MainGame_Textbox.Text = result;
            }
        }

        private string OpenFileDialog() {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if(openFileDialog.ShowDialog() == true) {
                return openFileDialog.FileName;
            }
            return null;
        }
    }
}
