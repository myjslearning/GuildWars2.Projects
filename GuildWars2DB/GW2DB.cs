﻿using GuildWars2DB.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;

namespace GuildWars2DB
{
    public class GW2DB
    {
        private const string DATA_SOURCE = "GuildWars2DB.sqlite";

        public static T GetSetting<T>(GW2Settings setting) => GetValue<T>(GW2Entities.Settings, "Identifier", GuildWars2DBDictionary.SettingToName(setting), "Value");

        public static DataTable GetTable(GW2Entities table) {
            DataTable result;

            using(SQLiteConnection conn = GetDatabase()) {
                using(SQLiteCommand cmd = new SQLiteCommand()) {
                    cmd.Connection = conn;
                    conn.Open();

                    SQLiteHelper helper = new SQLiteHelper(cmd);

                    if(!helper.IsTableCreated(table)) {
                        TableFactory.CreateTable(table, helper);
                    }
                    result = helper.SelectAll(table);

                    //conn.Close();
                }
            }

            return result;
        }

        private static T GetValue<T>(GW2Entities table, string IDcolumName, string ID, string columToGet) {
            DataTable result;

            using(SQLiteConnection conn = GetDatabase()) {
                using(SQLiteCommand cmd = new SQLiteCommand()) {
                    cmd.Connection = conn;
                    conn.Open();

                    SQLiteHelper helper = new SQLiteHelper(cmd);

                    if(!helper.IsTableCreated(table)) {
                        TableFactory.CreateTable(table, helper);
                    }

                    result = helper.Select("select * from @table where @IDcolumName = @ID;", new Dictionary<string, object>() {
                        { "@ID", ID },
                        { "@IDcolumName", IDcolumName },
                        { "@table", GuildWars2DBDictionary.EntityToName(table) }
                    });
                }
            }

            if(result?.Rows?.Count > 0) {
                return TryToCast<T>(result.Rows[0]?[columToGet]);
            }
            else {
                return default(T);
            }
        }

        public static void Insert(GW2Entities table, Dictionary<string, object> values) {
            using(SQLiteConnection conn = GetDatabase()) {
                using(SQLiteCommand cmd = new SQLiteCommand()) {
                    cmd.Connection = conn;
                    conn.Open();

                    SQLiteHelper helper = new SQLiteHelper(cmd);

                    if(!helper.IsTableCreated(table)) {
                        TableFactory.CreateTable(table, helper);
                    }

                    helper.Insert(table, values);

                    //conn.Close();
                }
            }
        }

        public static void Update(GW2Entities table, string changeColum, object changeValue, string conditionName, object conditionValue) {
            var changes = new Dictionary<string, object>() {
                { changeColum, changeValue }
            };
            Update(table, changes, conditionName, conditionValue);
        }

        private static void Update(GW2Entities table, Dictionary<string, object> changes, string conditionName, object conditionValue) {
            using(SQLiteConnection conn = GetDatabase()) {
                using(SQLiteCommand cmd = new SQLiteCommand()) {
                    cmd.Connection = conn;
                    conn.Open();

                    SQLiteHelper helper = new SQLiteHelper(cmd);

                    if(!helper.IsTableCreated(table)) {
                        TableFactory.CreateTable(table, helper);
                    }

                    helper.Update(GuildWars2DBDictionary.EntityToName(table), changes, conditionName, conditionValue);

                    //conn.Close();
                }
            }
        }

        #region Utility

        private static bool IsDatabaseCreated() => File.Exists(DATA_SOURCE);

        private static SQLiteConnection GetDatabase() {
            if(!IsDatabaseCreated())
                SQLiteConnection.CreateFile(DATA_SOURCE);

            return new SQLiteConnection($"data source={DATA_SOURCE}");
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

        #endregion Utility
    }
}