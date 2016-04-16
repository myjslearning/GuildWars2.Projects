using GuildWars2API.Model.Event;
using System;
using System.Collections.Generic;

namespace GuildWars2DB
{
    public class SettingDB
    {
        public static List<WorldBoss> GetWorldBosses() {
            return new List<WorldBoss>();
        }

        public static List<Tuple<string, string>> GetApiKeys() {
            return new List<Tuple<string, string>>();
        }

        public static void RemoveKey(List<Tuple<string, string>> keys) {
        }

        public static void AddKey(string name, string key) {
        }

        public static void SetAppPath(string path) {
        }

        public static string GetAppPath() {
            return string.Empty;
        }
    }
}
