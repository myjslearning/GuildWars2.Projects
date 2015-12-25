using GuildWars2Web.Models;
using System;
using System.Web;
using System.Web.Security;

namespace GuildWars2Web.Classes
{
    public static class Authorization
    {
        public static string GetAuthCookie(HttpResponse context) {
            HttpCookie authCookie = context.Cookies[FormsAuthentication.FormsCookieName];
            if(authCookie?.Value == "")
                return string.Empty;

            FormsAuthenticationTicket authTicket;
            try {
                authTicket = FormsAuthentication.Decrypt(authCookie.Value);
            }
            catch {
                return string.Empty;
            }
            return authTicket.UserData;
        }

        internal static void SetAuthCookie(HttpResponse response, Profile profile, bool remember) {
            bool isPersistent = true;

            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1,
              profile.Username,
              DateTime.Now,
              DateTime.Now.AddDays(7),
              isPersistent,
              profile.Serialize(),
              FormsAuthentication.FormsCookiePath);

            string encTicket = FormsAuthentication.Encrypt(ticket);
            response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));
        }

        internal static void RemoveAuthCookie(HttpResponse response) {
            response.Cookies.Remove(FormsAuthentication.FormsCookieName);
        }

        public static Profile GetProfile(HttpResponse context) {
            string[] userData = GetAuthCookie(context).Split(';');
            if(userData?.Length >= 2) {
                //TODO Retrieve from DB
                return new Profile() { ID = Convert.ToInt32(userData[0]), Username = "Roytazz", AuthRole = ConvertRole(userData[1]) };
            }
            return null;
        }

        private static AuthRoles ConvertRole(string role) {
            if(role.Equals("Admin")) {
                return AuthRoles.Admin;
            }
            else if(role.Equals("User")) {
                return AuthRoles.User;
            }
            else {
                return AuthRoles.Unknown;
            }
        }
    }

    public enum AuthRoles {
        Admin,
        User,
        Unknown
    }
}