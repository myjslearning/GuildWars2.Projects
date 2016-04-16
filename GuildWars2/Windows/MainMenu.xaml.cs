using GuildWars2.Classes.Logger;
using GuildWars2.Controls;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;

namespace GuildWars2.Windows
{
    public partial class MainMenu : MetroWindow
    {
        private Dictionary<Type, FrameworkElement> _pages;
        public ObservableCollection<Tuple<string, string>> Keys { get; }

        public MainMenu() {
            InitializeComponent();

            Keys = new ObservableCollection<Tuple<string, string>>() { new Tuple<string, string>("No API key selected", null) };
            Settings.KeyAdded += Settings_KeyAdded;
        }

        private FrameworkElement GetUIElement<T>() where T : FrameworkElement {
            if(_pages == null)
                _pages = new Dictionary<Type, FrameworkElement>();

            if(_pages.ContainsKey(typeof(T))) {
                return _pages[typeof(T)];
            }
            else {
                try {
                    _pages.Add(typeof(T), Activator.CreateInstance(typeof(T)) as FrameworkElement);
                    return GetUIElement<T>();
                }
                catch(Exception ex) {
                    LogManager.LogException(ex, "Failed to create instance of " + typeof(T), false);
                    return new FrameworkElement();
                }
            }
        }

        #region Events

        private void Settings_KeyAdded(object sender, KeyAddedArgs e) {
            Keys.Add(new Tuple<string, string>(e.Key.Name, e.Key.Key));

            if(Keys.Count == 2 && Keys[0].Item2 == null) {  //Remove default key (No API key selected)
                Keys.RemoveAt(0);   
                SplitButton_KeySelector.SelectedIndex = 0;
            }
        }
        
        private void MainGame_Click(object sender, RoutedEventArgs e) {
            string path = GuildWars2DB.SettingDB.GetAppPath(); 
            if(path != null)
                return;

            try {
                Process.Start(path);
                Classes.NotifyHandler.Instance.AddNotification("GW2 is being launched");
            }
            catch(Exception ex) {
                LogManager.LogException(ex, string.Format("Unable to launch GW2 Application with path: {0}", path), false);
            }
        }

        private void Settings_Click(object sender, RoutedEventArgs e) {
            SettingsFlyout.IsOpen = true;
        }

        private void Tools_Click(object sender, System.Windows.Input.MouseButtonEventArgs e) {
            ContentFrame.Navigate(GetUIElement<PaletteSelector>());
        }

        private void ContentFrame_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e) {
            while(ContentFrame.CanGoBack) {
                ContentFrame.RemoveBackEntry();
            }
        }

        #endregion Events
    }
}