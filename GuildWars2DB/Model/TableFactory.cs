using GuildWars2DB.Resources;
using System.Collections.Generic;
using System.Data.SQLite;

namespace GuildWars2DB.Model
{
    internal static class TableFactory
    {
        public static void CreateTable(GW2Entities entity, SQLiteHelper helper) {
            if(entity == GW2Entities.WorldBoss || entity == GW2Entities.WorldBossTime) {
                CreateWorldBossTable(helper);
            }
            else if(entity == GW2Entities.Settings) {
                CreateSettingsTable(helper);
            }
            else if(entity == GW2Entities.Item) {
            }
        }

        public static void DropAndReloadTable(GW2Entities entity, SQLiteHelper helper) {
            helper.DropTable(GuildWars2DBDictionary.EntityToName(entity));
            CreateTable(entity, helper);
        }

        #region WorldBosses

        public static void CreateWorldBossTable(SQLiteHelper helper) {
            helper.CreateTable(GetWorldBossTable());
            helper.CreateTable(GetWorldBossTimeTable());

            PopulateWorldBossTable(helper);
        }

        private static SQLiteTable GetWorldBossTable() {
            SQLiteTable worldBossTable = new SQLiteTable(GuildWars2DBDictionary.EntityToName(GW2Entities.WorldBoss));
            worldBossTable.Columns.Add(new SQLiteColumn("ID", true));
            worldBossTable.Columns.Add(new SQLiteColumn("Name", ColType.Text));
            worldBossTable.Columns.Add(new SQLiteColumn("Description", ColType.Text));
            worldBossTable.Columns.Add(new SQLiteColumn("Waypoint", ColType.Text));
            worldBossTable.Columns.Add(new SQLiteColumn("EventID", ColType.Text));
            worldBossTable.Columns.Add(new SQLiteColumn("Level", ColType.Integer));
            worldBossTable.Columns.Add(new SQLiteColumn("IsDone", ColType.Integer));
            worldBossTable.Columns.Add(new SQLiteColumn("DragoniteLootMinimum", ColType.Integer));
            worldBossTable.Columns.Add(new SQLiteColumn("DragoniteLootMaximum", ColType.Integer));
            worldBossTable.Columns.Add(new SQLiteColumn("ItemLoot", ColType.Integer));
            worldBossTable.Columns.Add(new SQLiteColumn("BoxesLoot", ColType.Integer));
            worldBossTable.Columns.Add(new SQLiteColumn("IsTracking", ColType.Integer));

            return worldBossTable;
        }

        private static SQLiteTable GetWorldBossTimeTable() {
            SQLiteTable timeTable = new SQLiteTable(GuildWars2DBDictionary.EntityToName(GW2Entities.WorldBossTime));
            timeTable.Columns.Add(new SQLiteColumn("ID", true));
            timeTable.Columns.Add(new SQLiteColumn("EventID", ColType.Text));
            timeTable.Columns.Add(new SQLiteColumn("Time", ColType.Text));

            return timeTable;
        }

        private static void PopulateWorldBossTable(SQLiteHelper helper) {
            List<Dictionary<string, object>> result = XMLReader.ReadWorldBosses();
            foreach(Dictionary<string, object> worldBossRow in result) {
                List<string> times = worldBossRow["Times"] as List<string>;
                worldBossRow.Remove("Times");

                helper.Insert(GW2Entities.WorldBoss, worldBossRow);

                times.ForEach(t => helper.Insert(GW2Entities.WorldBossTime, new Dictionary<string, object>() {
                    { "Time", t },
                    { "EventID", worldBossRow["EventID"] }
                }));
            }
        }

        #endregion WorldBosses

        #region Settings

        public static void CreateSettingsTable(SQLiteHelper helper) {
            SQLiteTable timeTable = new SQLiteTable(GuildWars2DBDictionary.EntityToName(GW2Entities.Settings));
            timeTable.Columns.Add(new SQLiteColumn("ID", true));
            timeTable.Columns.Add(new SQLiteColumn("Identifier", ColType.Text));
            timeTable.Columns.Add(new SQLiteColumn("Value", ColType.Text));

            helper.CreateTable(timeTable);
        }

        #endregion Settings
    }
}