using GuildWars2DB;
using GuildWars2Web.Classes;
using GuildWars2Web.Models;
using System.Diagnostics.CodeAnalysis;
using System.Web.Mvc;

namespace GuildWars2Web.Controllers
{
    public class AccountController : BaseController
    {
        [HttpPost]
        public ActionResult Login(LoginViewModel model) {
            if(ModelState.IsValid && UserManager.IsValid(model.Username, model.Password)) {
                Profile profile = new Profile(UserDB.GetUser(model.Username));
                Authorization.SetAuthCookie(HttpContext.ApplicationInstance.Response, profile, model.RememberMe);          

                ViewBag.LoggedIn = true;
                return base.PartialView("~/Views/Account/_Login.cshtml", model);
            }

            ViewBag.LoggedIn = false;
            return PartialView("~/Views/Account/_Login.cshtml", model);
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel model) {
            if(ModelState.IsValid) {
                UserManager.RegisterUser(model.Username, model.Password);

                ViewBag.Registered = true;
                return PartialView("~/Views/Account/_Register.cshtml");
            }
            ViewBag.Registered = false;     
            return PartialView("~/Views/Account/_Register.cshtml", model);
        }

        [SuppressMessage("", "CSE0003:Use expression-bodied members")]
        public ActionResult SignOut() {
            Authorization.RemoveAuthCookie(HttpContext.ApplicationInstance.Response);
            return RedirectToAction("Index", "Event");
        }

        public ActionResult _UserAccount() => View(GetViewModelProfile());

        public ActionResult _UserSidebar() => View(GetViewModelProfile());
    }
}