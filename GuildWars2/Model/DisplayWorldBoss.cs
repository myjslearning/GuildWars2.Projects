using GuildWars2.Other;
using GuildWars2API.Model.Event;
using GuildWars2DB;
using System;
using System.Linq;

namespace GuildWars2.Model
{
    public class DisplayWorldBoss : WorldBoss
    {
        private bool _isTracking;

        public DisplayWorldBoss() {
            base.PropertyChanged += (sender, e) => {
                if(e.PropertyName.Equals("IsDone")) {
                    NotifyPropertyChanged("IsDoneIcon");
                }
                else if(e.PropertyName.Equals("IsTracking")) {
                    NotifyPropertyChanged("IsTrackingIcon");
                }
                else if(e.PropertyName.Equals("IsDoneIcon")) {
                    WorldBossDB.Update("IsDone", IsDone, "EventID", EventID);
                }
                else if(e.PropertyName.Equals("IsTrackingIcon")) {
                    WorldBossDB.Update("IsTracking", IsTracking, "EventID", EventID);
                }
            };
        }

        public bool IsTracking {
            get { return _isTracking; }
            set {
                if(_isTracking != value) {
                    _isTracking = value;
                    NotifyPropertyChanged("IsTracking");
                }
            }
        }

        public bool IsTrackingNoNotify {
            set {
                if(_isTracking != value) {
                    _isTracking = value;
                }
            }
        }

        public string IsTrackingIcon {
            get {
                if(IsTracking) {
                    return "pack://application:,,,/Resources/Images/Icons/check_green.png";
                }
                else {
                    return "pack://application:,,,/Resources/Images/Icons/check_grey.png";
                }
            }
        }

        public string IsDoneIcon {
            get {
                if(IsDone) {
                    return "pack://application:,,,/Resources/Images/Icons/check_green.png";
                }
                else {
                    return "pack://application:,,,/Resources/Images/Icons/cross_red.png";
                }
            }
        }

        public TimeSpan TimeLeft {
            get {
                if(NextTime == null)
                    return new TimeSpan(-1, -1, -1);

                TimeSpan now = Now;
                if(now > NextTime) {
                    TimeSpan timeUntilMidnight = new TimeSpan(23 - now.Hours, 60 - now.Minutes, 0);
                    return NextTime + timeUntilMidnight;
                }
                else {
                    return NextTime - now;
                }
            }
        }

        public TimeSpan NextTime {
            get {
                if(Times == null)
                    return new TimeSpan(-1, -1, -1);

                if(Times.Any(t => t > Now)) {
                    return Times.Where(t => t > Now).First();
                }
                else {
                    return Times[0];
                }
            }
        }

        public TimeSpan LastTime {
            get {
                if(Times == null)
                    return new TimeSpan(-1, -1, -1);

                if(Times.Any(t => t < Now)) {
                    return Times.Where(t => t < Now).OrderByDescending(t => t).ToList().First();
                }
                else {
                    return Times[Times.Count - 1];
                }
            }
        }

        public string DragoniteOreString {
            get {
                if(this.DragoniteLootMinimum == this.DragoniteLootMaximum) {
                    return this.DragoniteLootMinimum.ToString();
                }
                else {
                    return this.DragoniteLootMinimum + "-" + this.DragoniteLootMaximum;
                }
            }
        }

        public string LevelColor => ColorManager.GreenToRed(Level, 0, 80).ToString();

        public string TimeLeftString => TimeLeft.ToString(@"hh\:mm");

        public string NextTimeString => NextTime.ToString(@"hh\:mm");

        private TimeSpan Now => new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, 0);
    }
}