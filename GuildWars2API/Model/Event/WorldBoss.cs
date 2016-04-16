using System;
using System.Collections.Generic;
using System.Linq;

namespace GuildWars2API.Model.Event
{
    public class WorldBoss : Event
    {
        public TimeSpan DailyReset => new TimeSpan(2, 0, 0);

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
    }
}