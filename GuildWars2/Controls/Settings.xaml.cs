using GuildWars2.Classes;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Controls;

namespace GuildWars2.Controls
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : UserControl
    {
        public event EventHandler<KeyAddedArgs> KeyAdded;
        public ObservableCollection<ApiKey> Keys { get; }

        public Settings() {
            InitializeComponent();

            Keys = new ObservableCollection<ApiKey>();

            var foundKeys = GuildWars2DB.SettingDB.GetApiKeys();
            foundKeys.ForEach(k => Keys.Add(new ApiKey() { Name = k.Item1, Key = k.Item2 }));
            Keys.CollectionChanged += Keys_CollectionChanged;

            ListView_Keys.ItemsSource = Keys;
        }

        #region Events

        private void Keys_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e) {
            if(e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove) {
                var removedKeys = e.OldItems.Cast<ApiKey>().ToList();  
                var keys = new List<Tuple<string, string>>();
                removedKeys.ForEach(k => keys.Add(new Tuple<string, string>(k.Name, k.Key)));

                GuildWars2DB.SettingDB.RemoveKey(keys);
            }
        }

        private void Button_KeyAdd_Click(object sender, System.Windows.RoutedEventArgs e) {
            if(Textbox_KeyName.Text.Length <= 1 || Textbox_KeyName.Text.Length <= 1)
                return;

            var key = new ApiKey() { Name = Textbox_KeyName.Text, Key = Textbox_Key.Text };
            GuildWars2DB.SettingDB.AddKey(key.Name, key.Key);
            Keys.Add(key);

            //Trigger event
            if(KeyAdded != null)
                NotifyHandler.Instance.AddNotification("API has been added");
                KeyAdded(this, new KeyAddedArgs() { Key = key });

            Textbox_Key.Text = string.Empty;
            Textbox_KeyName.Text = string.Empty;
        }

        private void MainGameButton_Click(object sender, System.Windows.RoutedEventArgs e) {
            string path = OpenFileDialog();
            if(path != null && File.Exists(path)) {
                GuildWars2DB.SettingDB.SetAppPath(path);
                TextBox_MainGame.Text = path;
            }
        }

        private string OpenFileDialog() {
            var openFileDialog = new OpenFileDialog();
            if(openFileDialog.ShowDialog() == true) {
                return openFileDialog.FileName;
            }
            return null;
        }

        private void TextBox_MainGame_KeyDown(object sender, System.Windows.Input.KeyEventArgs e) {
            e.Handled = true;
        }

        #endregion Events
    }

    public class ApiKey {
        public string Name { get; set; }
        public string Key { get; set; }
    }

    public class KeyAddedArgs : EventArgs {
        public ApiKey Key { get; set; }
    }
}