namespace GuildWars2DB.Model
{
    public static class GuildWars2DBDictionary
    {
        public static string EntityToName(GW2Entities entity) {
            if(entity == GW2Entities.WorldBoss) {
                return "WorldBoss";
            }
            else if(entity == GW2Entities.WorldBossTime) {
                return "WorldBossTime";
            }
            else if(entity == GW2Entities.AccountValue) {
                return "AccountValue";
            }
            else if(entity == GW2Entities.User) {
                return "User";
            }
            return null;
        }
    }

    public enum GW2Entities
    {
        WorldBoss,
        WorldBossTime,
        User,
        AccountValue
    }
}