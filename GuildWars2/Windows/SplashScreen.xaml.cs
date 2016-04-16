using MahApps.Metro.Controls;
using System.Net;

namespace GuildWars2.Windows
{
    /// <summary>
    /// Interaction logic for SplashScreen.xaml
    /// </summary>
    public partial class SplashScreen : MetroWindow
    {
        public SplashScreen() {
            if(HasInternetConnection()) {
                OpenMainWindow();
            }
            else {
                InitializeComponent();
            }
        }

        private static bool HasInternetConnection() {
            try {
                using(var client = new WebClient()) {
                    using(var stream = client.OpenRead("http://www.google.com")) {
                        return true;
                    }
                }
            }
            catch {
                return false;
            }
        }

        private void OpenMainWindow() {
            new MainMenu().Show();
            this.Close();
        }

        private void ButtonYes_Click(object sender, System.Windows.RoutedEventArgs e) {
            OpenMainWindow();
        }

        private void ButtonNo_Click(object sender, System.Windows.RoutedEventArgs e) {
            this.Close();
        }
    }
}