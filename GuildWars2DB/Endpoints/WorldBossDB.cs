using GuildWars2DB.Model;
using System.Data;

namespace GuildWars2DB
{
    public static class WorldBossDB
    {
        public static DataTable WorldBossTable => GW2DB.GetTable(GW2Entities.WorldBoss);
        public static DataTable WorldBossTimeTable => GW2DB.GetTable(GW2Entities.WorldBossTime);

        public static void Update(string changeColum, object changeValue, string conditionName, object conditionValue) {
            GW2DB.Update(GW2Entities.WorldBoss, changeColum, changeValue, conditionName, conditionValue);
        }
    }
}
