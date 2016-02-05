using GuildWars2DB;
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
              profile.ID.ToString(),
              FormsAuthentication.FormsCookiePath);

            string encTicket = FormsAuthentication.Encrypt(ticket);
            response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));
        }

        internal static void RemoveAuthCookie(HttpResponse response) {
            response.Cookies.Remove(FormsAuthentication.FormsCookieName);
        }

        public static Profile GetProfile(HttpResponse context) {
            string userData = GetAuthCookie(context);
            if(userData?.Length > 0) {
                return new Profile(UserDB.GetUser(userData));
            }
            return null;
        }

        public static AuthRoles ConvertRole(int roleNum) {
            foreach(AuthRoles role in Enum.GetValues(typeof(AuthRoles))) {
                if((int)role == roleNum)
                    return role;
            }

            return AuthRoles.Unknown;
        }
    }

    public enum AuthRoles {
        Admin,
        User,
        Unknown
    }
}