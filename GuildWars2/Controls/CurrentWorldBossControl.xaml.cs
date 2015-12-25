using GuildWars2.Model;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GuildWars2.Controls
{
    /// <summary>
    /// Interaction logic for CurrentEvent.xaml
    /// </summary>
    public partial class CurrentWorldBossControl : UserControl
    {
        private DisplayWorldBoss _currentEvent;

        public DisplayWorldBoss CurrentWorldBoss {
            get { return _currentEvent; }
            set {
                if(_currentEvent == null || _currentEvent.EventID != value.EventID) {
                    this._currentEvent = value;
                    UpdateWorldBoss();
                }
            }
        }

        public CurrentWorldBossControl() {
            InitializeComponent();
        }

        private void UpdateWorldBoss() {
            BossName.Text = CurrentWorldBoss.Name;
            Description.Text = CurrentWorldBoss.Description;
            DragoniteOreString.Text = CurrentWorldBoss.DragoniteOreString;
            RareExoticItemString.Text = CurrentWorldBoss.ItemLoot.ToString();
            ContainersString.Text = CurrentWorldBoss.BoxesLoot.ToString();
        }

        #region Events

        private void Waypoint_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            if(sender.GetType() == typeof(Image)) {
                (sender as Image).Source = new System.Windows.Media.Imaging.BitmapImage(new Uri("pack://application:,,,/Resources/Images/Icons/waypoint_hover.png"));
            }
        }

        private void Waypoint_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
            if(sender.GetType() == typeof(Image)) {
                (sender as Image).Source = new System.Windows.Media.Imaging.BitmapImage(new Uri("pack://application:,,,/Resources/Images/Icons/waypoint.png"));
                Clipboard.SetText(CurrentWorldBoss.Waypoint);
            }
        }

        #endregion Events
    }
}