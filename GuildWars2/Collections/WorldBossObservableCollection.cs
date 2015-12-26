using GuildWars2.Model;
using GuildWars2.Other;
using GuildWars2DB;
using GuildWars2DB.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Threading;

namespace GuildWars2.Collections
{
    internal class WorldBossObservableCollection : PropChangeObservableCollection<DisplayWorldBoss>
    {
        private static TimeSpan DAILY_RESET = new TimeSpan(2, 0, 0);    //2:00am

        public WorldBossObservableCollection() {
            AddRange(DBParser.ParseWorldBosses(GW2DB.GetTable(GW2Entities.WorldBoss), GW2DB.GetTable(GW2Entities.WorldBossTime)));

            Tick();

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 1, 0);
            timer.Tick += (sender, e) => Tick();
            timer.Start();
        }

        #region Tick

        private void Tick() {
            SortEvents();
            if(IsResetRequired()) {
                this.ToList().ForEach(b => b.IsDone = false);
            }
            UpdateEvents();
        }

        private bool IsResetRequired() => DateTime.Now.Hour == DAILY_RESET.Hours && DateTime.Now.Minute >= DAILY_RESET.Minutes;

        private void SortEvents() {
            if(this.Count <= 0)
                return;

            List<DisplayWorldBoss> bosses = this.OrderBy(b => b.TimeLeft).ToList();

            if(bosses[0]?.EventID != this[0]?.EventID) {
                this.ClearItems();
                AddRange(bosses);
            }
        }

        private void UpdateEvents() {
            if(this.Count <= 0)
                return;

            this[0].Level = this[0].Level;  //Cheaty way to update
        }

        #endregion Tick

        private void AddRange(ICollection<DisplayWorldBoss> bosses) {
            if(bosses == null)
                return;

            bosses.ToList().ForEach(b => this.Add(b));
        }
    }
}