using GuildWars2Web.Classes;
using System;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace GuildWars2Web
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e) {
            if(Context.User != null) {
                string[] roles = Authorization.GetAuthCookie(Response).Split(';');

                if(roles.Length >= 2) {
                    Context.User = new GenericPrincipal(Context.User.Identity, new string[1] { roles[1] });
                }
            }
        }
    }
}
