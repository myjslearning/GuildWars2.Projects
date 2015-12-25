using GuildWars2.Collections;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GuildWars2.Windows.Pages
{
    /// <summary>
    /// Interaction logic for WorldBosses.xaml
    /// </summary>
    public partial class WorldBosses : Page
    {
        private WorldBossObservableCollection _bossesItemSource;

        public WorldBosses() {
            InitializeComponent();
            StartBackgroundWorker();
        }

        #region BackgroundWorker

        private void StartBackgroundWorker() {
            BackgroundWorker bg = new BackgroundWorker();
            bg.DoWork += Bg_DoWork;
            bg.RunWorkerCompleted += Bg_RunWorkerCompleted;
            bg.RunWorkerAsync();
        }

        private void Bg_DoWork(object sender, DoWorkEventArgs e) {
            _bossesItemSource = new WorldBossObservableCollection();
            _bossesItemSource.CollectionChanged += (senderBoss, eBoss) => { UpdateCurrentWorldBoss(); };
        }

        private void Bg_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            Bosses_ListView.ItemsSource = _bossesItemSource;
            Bosses_ListView.SelectedIndex = 0;
            UpdateCurrentWorldBoss();

            Bosses_ProgressBar.Visibility = Visibility.Collapsed;
            Bosses_ListView.Visibility = Visibility.Visible;
        }

        #endregion BackgroundWorker

        #region Events

        private void IsDone_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
            if(Bosses_ListView.SelectedIndex >= 0 && Bosses_ListView.SelectedIndex <= _bossesItemSource.Count) {
                if(_bossesItemSource[Bosses_ListView.SelectedIndex].IsDone) {
                    _bossesItemSource[Bosses_ListView.SelectedIndex].IsDone = false;
                }
                else {
                    _bossesItemSource[Bosses_ListView.SelectedIndex].IsDone = true;
                }
            }
        }

        private void Waypoint_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            if(sender.GetType() == typeof(Image)) {
                (sender as Image).Source = new System.Windows.Media.Imaging.BitmapImage(new Uri("pack://application:,,,/Resources/Images/Icons/waypoint_hover.png"));
            }
        }

        private void Waypoint_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
            if(sender.GetType() == typeof(Image)) {
                (sender as Image).Source = new System.Windows.Media.Imaging.BitmapImage(new Uri("pack://application:,,,/Resources/Images/Icons/waypoint.png"));
                Clipboard.SetText(_bossesItemSource[Bosses_ListView.SelectedIndex].Waypoint);
            }
        }

        #endregion Events

        private void UpdateCurrentWorldBoss() {
            if(_bossesItemSource.Count <= 0)
                return;
            
            CurrentBoss_Control.CurrentWorldBoss = _bossesItemSource.OrderByDescending(e => e.LastTime).ThenByDescending(e => e.LastTime.Hours).First();
        }
    }
}