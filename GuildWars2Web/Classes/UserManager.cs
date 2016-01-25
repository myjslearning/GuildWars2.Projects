namespace GuildWars2Web.Classes
{
    public static class UserManager
    {
#pragma warning disable CSE0003
        public static bool IsValid(string username, string password) {
            //TODO Check in DB
            return true;
        }
#pragma warning restore CSE0003

        public static void RegisterUser(string username, string password) {
            //TODO Create in DB
        }
    }
}
