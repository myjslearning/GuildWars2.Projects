using GuildWars2.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace GuildWars2.Other
{
    internal class DBParser
    {
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

            foreach(DataRow row in times.Rows) {
                string eventID = row["EventID"].ToString();

                if(!result.ContainsKey(eventID)) {
                    result.Add(eventID, new List<TimeSpan>());
                }
                result[eventID].Add(TimeSpan.Parse(row["Time"].ToString()));
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
    }
}