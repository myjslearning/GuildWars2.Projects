using GuildWars2.Model;
using GuildWars2DB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Threading;

namespace GuildWars2.Collections
{
    internal class WorldBossObservableCollection : PropChangeObservableCollection<DisplayWorldBoss>
    {
        private static TimeSpan DAILY_RESET = new TimeSpan(2, 0, 0);    //2:00am

        public WorldBossObservableCollection() {
            InitCollection();

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
                bosses.ToList().ForEach(b => this.Add(b));
            }
        }

        private void UpdateEvents() {
            if(this.Count <= 0)
                return;

            this[0].Level = this[0].Level;  //Cheaty way to update
        }

        #endregion Tick

        #region Initialization

        private void InitCollection() {
            List<DisplayWorldBoss> bosses = ParseWorldBosses(WorldBossDB.WorldBossTable, WorldBossDB.WorldBossTimeTable);
            bosses.ToList().ForEach(b => this.Add(b));
        }

        public static List<DisplayWorldBoss> ParseWorldBosses(DataTable worldBosses, DataTable times) {
            List<DisplayWorldBoss> bosses = new List<DisplayWorldBoss>();
            Dictionary<string, List<TimeSpan>> bossesTimes = ParseWorldBossTimes(times);

            foreach(DataRow row in worldBosses.Rows) {
                bosses.Add(new DisplayWorldBoss() {
                    Name = row["Name"].ToString(),
                    Description = row["Description"].ToString(),
                    Waypoint = row["Waypoint"].ToString(),
                    EventID = row["EventID"].ToString(),
                    Level = TryToCast<int>(row["Level"]),
                    Times = bossesTimes[row["EventID"].ToString()],
                    IsDoneNoNotify = TryToCast<bool>(row["IsDone"]),
                    DragoniteLootMinimum = TryToCast<int>(row["DragoniteLootMinimum"]),
                    DragoniteLootMaximum = TryToCast<int>(row["DragoniteLootMaximum"]),
                    ItemLoot = TryToCast<int>(row["ItemLoot"]),
                    BoxesLoot = TryToCast<int>(row["BoxesLoot"]),
                    IsTrackingNoNotify = TryToCast<bool>(row["IsTracking"])
                });
            }

            return bosses;
        }

        private static Dictionary<string, List<TimeSpan>> ParseWorldBossTimes(DataTable times) {
            Dictionary<string, List<TimeSpan>> result = new Dictionary<string, List<TimeSpan>>();
            bool DayLightSaving = TimeZoneInfo.Local.IsDaylightSavingTime(DateTime.Now);

            foreach(DataRow row in times.Rows) {
                string eventID = row["EventID"].ToString();

                if(!result.ContainsKey(eventID)) {
                    result.Add(eventID, new List<TimeSpan>());
                }

                TimeSpan time = TimeSpan.Parse(row["Time"].ToString());
                if(!DayLightSaving) {
                    if(time.Hours == 0) {
                        time = new TimeSpan(23, time.Minutes, 0);
                    }
                    else {
                        time = new TimeSpan(time.Hours - 1, time.Minutes, 0);
                    }
                }
                result[eventID].Add(time);
            }

            return result;
        }

        private static T TryToCast<T>(object value) {
            if(value == null)
                return default(T);

            if(value is T) {
                return (T)value;
            }
            else {
                try {
                    return (T)Convert.ChangeType(value, typeof(T));
                }
                catch(InvalidCastException) {
                    return default(T);
                }
            }
        }

        #endregion Initialization
    }
}