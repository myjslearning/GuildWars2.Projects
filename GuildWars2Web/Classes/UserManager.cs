using GuildWars2DB;

namespace GuildWars2Web.Classes
{
    public static class UserManager
    {
        public static bool IsValid(string username, string password) => UserDB.IsValid(username, password);

        public static void RegisterUser(string username, string password) {
            UserDB.RegisterUser(username, password);
        }
    }
}
