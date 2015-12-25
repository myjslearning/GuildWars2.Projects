using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace GuildWars2API.Model.Event
{
    public class WorldBoss : Event, INotifyPropertyChanged
    {
        private static TimeSpan DAILY_RESET = new TimeSpan(2, 0, 0);    //2:00am

        private bool _isDone;

        public bool IsDone {
            get { return this._isDone; }
            set {
                if(_isDone != value) {
                    _isDone = value;
                    NotifyPropertyChanged("IsDone");
                }
            }
        }

        public bool IsDoneNoNotify {
            set {
                _isDone = value;
            }
        }

        public List<TimeSpan> Times { get; set; }

        public int DragoniteLootMinimum { get; set; }

        public int DragoniteLootMaximum { get; set; }

        public int ItemLoot { get; set; }

        public int BoxesLoot { get; set; }

        private List<TimeSpan> SortCollection(List<TimeSpan> timesToSort) {
            if(timesToSort != null && timesToSort.Count > 0) {
                return timesToSort.OrderBy(t => t.Hours).ToList();
            }
            return timesToSort;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "") {
            if(PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}