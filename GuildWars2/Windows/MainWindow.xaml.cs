using GuildWars2.Other;
using GuildWars2.Other.Notification;
using GuildWars2.Windows.Pages;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace GuildWars2.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private Dictionary<Type, Page> _pages;

        public MainWindow() {
            NotifyHandler.NotificationAdded += NotifyHandler_ShowNotification;
            _pages = new Dictionary<Type, Page>();

            InitializeComponent();
        }

        private Page GetPage<T>() where T : Page {
            if(_pages.ContainsKey(typeof(T))) {
                return _pages[typeof(T)];
            }
            else {
                try {
                    _pages.Add(typeof(T), Activator.CreateInstance(typeof(T)) as Page);
                    return GetPage<T>();
                }
                catch(Exception ex) {
                    new ErrorHandler(ex, "Failed to create instance of " + typeof(T), true, false);
                    return new Page();
                }
            }
        }

        #region Events

        /// <summary>
        /// Launches Guild Wars 2 if the path has been set.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainGame_Click(object sender, System.Windows.RoutedEventArgs e) {
            string path = null; //Retrieve GW2 application path
            try {
                if(path != null) {
                    Process.Start(path);
                }
            }
            catch(Exception ex) {
                Console.WriteLine($"[ERROR] Error during startup of MainGame. Path:{path}");
                Console.WriteLine(ex.Message + "\n" + ex.ToString());
            }
        }

        private void MenuItem_Click(object sender, System.Windows.RoutedEventArgs e) {
            if(!(sender is Control))
                return;

            string name = (sender as Control).Name;
            if(name.Contains("WorldBosses")) {
                MainFrame.Navigate(GetPage<WorldBosses>());
            }
            else if(name.Contains("Marketplace")) {
                //MainFrame.Navigate(GetPage<Marketplace>());
            }
            else if(name.Contains("Crafting")) {
                MainFrame.Navigate(GetPage<Crafting>());
            }
            else if(name.Contains("Character")) {
                //MainFrame.Navigate(GetPage<Character>());
            }
            else if(name.Contains("Tools")) {
                //MainFrame.Navigate(GetPage<Tools>());
            }
            else if(name.Contains("Settings")) {
                MainFrame.Navigate(GetPage<Settings>());
            }
        }

        /// <summary>
        /// Called after navigating to a new Frame. Removes history to keep memory resources low.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainFrame_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e) {
            if(!MainFrame.CanGoBack && !MainFrame.CanGoForward)
                return;

            var entry = MainFrame.RemoveBackEntry();
            while(entry != null) {
                entry = MainFrame.RemoveBackEntry();
            }
        }

        /// <summary>
        /// Handles the Hide/Show of the menu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HideShowMenu_Click(object sender, System.Windows.Input.MouseButtonEventArgs e) {
            if(MainGrid.ColumnDefinitions[0].MaxWidth == 0) {
                MainGrid.ColumnDefinitions[0].MaxWidth = 370;
                MenuBorder.Visibility = System.Windows.Visibility.Visible;
                NotifyBar.Margin = new System.Windows.Thickness(0, 0, 0, 0);
                Frame_Grid.Margin = new System.Windows.Thickness(0, 0, 0, 0);
                HideShow_Grid.Background = new VisualBrush() {
                    Stretch = Stretch.Uniform,
                    Visual = (Visual)this.FindResource("appbar_chevron_left")
                };
            }
            else {
                MainGrid.ColumnDefinitions[0].MaxWidth = 0;
                MenuBorder.Visibility = System.Windows.Visibility.Hidden;
                NotifyBar.Margin = new System.Windows.Thickness(-4, 0, 0, 0);   //A weird bug where there is a tiny space between the notifybar and the window-border
                Frame_Grid.Margin = new System.Windows.Thickness(-4, 0, 0, 0);
                HideShow_Grid.Background = new VisualBrush() {
                    Stretch = Stretch.Uniform,
                    Visual = (Visual)this.FindResource("appbar_chevron_right")
                };
            }
        }

        /// <summary>
        /// Called when a new notification is pushed to show to the user.
        /// </summary>
        /// <param name="notification"></param>
        /// <param name="e"></param>
        private void NotifyHandler_ShowNotification(object sender, NotifyEventArgs e) {
            App.Current.Dispatcher.Invoke(delegate {
                Notification_Label.Content = e.Notification;
                Storyboard sb = this.FindResource("ShowNotification") as Storyboard;
                sb.Begin();
            });
        }

        #endregion Events
    }
}