using GuildWars2DB.Model;
using System.Collections.Generic;
using System.Data;

namespace GuildWars2DB
{
    public static class UserDB
    {
        private static DataTable UserTable => GW2DB.GetTable(GW2Entities.User);

        public static bool CheckUser(string username, string password) {
            DataRow row = FindUser(username);
            if(row != null) 
                return password.Equals(row["Password"]);

            return false;
        }

        public static Dictionary<string, object> GetUser(string username) {
            DataRow row = FindUser(username);
            if(row != null) {
                Dictionary<string, object> user = new Dictionary<string, object>();
                user.Add("Username", row["Username"]);
                user.Add("AuthRole", row["AuthRole"]);
                user.Add("Level", row["Level"]);
                user.Add("Role", row["Role"]);
                user.Add("Rank", row["Rank"]);
                //user.Add("Avatar", row["Avatar"]);
                user.Add("SubDate", row["SubDate"]);

                return user;
            }
            return null;        
        }

        public static void CreateUser(Dictionary<string, object> user) {
            GW2DB.Insert(GW2Entities.User, user);
        }

        private static DataRow FindUser(string username) {
            DataRow[] rows = UserTable.Select($"Username = {username}");
            if(rows.Length == 1)
                return rows[0];

            return null;
        }
    }
}