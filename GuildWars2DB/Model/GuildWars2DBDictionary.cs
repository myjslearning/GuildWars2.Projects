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
            else if(entity == GW2Entities.Settings) {
                return "Setting";
            }
            else if(entity == GW2Entities.Item) {
                return "Item";
            }
            return null;
        }

        public static string SettingToName(GW2Settings setting) {
            if(setting == GW2Settings.MainGame) {
                return "MainGame";
            }
            else if(setting == GW2Settings.SaveWorldBossTracking) {
                return "SaveWorldBossTracking";
            }
            return null;
        }
    }

    public enum GW2Entities
    {
        WorldBoss,
        WorldBossTime,
        Settings,
        Item
    }

    public enum GW2Settings      //Write Settings to a settings file. Not in DB
    {
        MainGame,
        SaveWorldBossTracking,
    }
}