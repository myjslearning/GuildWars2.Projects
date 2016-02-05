using GuildWars2DB;
using System;
using System.Collections.Generic;

namespace GuildWars2Web.Classes
{
    public static class UserManager
    {
#pragma warning disable CSE0003
        public static bool IsValid(string username, string password) {
            return UserDB.CheckUser(username, password);
        }
#pragma warning restore CSE0003

        public static void RegisterUser(string username, string password) {
            Dictionary<string, object> userProperties = new Dictionary<string, object>() {      
                { "Username", username },
                { "Password", password },
                { "AuthRole", 1 },
                { "Level", 1 },
                { "Role", "User" },
                { "Rank", "Scrub" },
                //{ "Avatar", "c://image.png" },
                { "SubDate", DateTime.Now }
            };
            UserDB.CreateUser(userProperties);
        }
    }
}
