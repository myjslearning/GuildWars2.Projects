using GuildWars2Web.Classes;
using GuildWars2Web.Models;
using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Web.Mvc;

namespace GuildWars2Web.Controllers
{
    public class BaseController : Controller
    {
        public BaseController() {
            ViewBag.Title = "Guild Wars 2 Web";
            ViewBag.Version = GetApplicationVersion();
        }

        public Profile GetProfile() {
            if(Session["Profile"]?.GetType() != typeof(Profile)) {
                Session["Profile"] = Authorization.GetProfile(HttpContext.ApplicationInstance.Response);
            }
            return Session["Profile"] as Profile;
        }
        
        private string GetApplicationVersion() {
            Assembly assembly = Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            return fvi.FileVersion;
        }

        [SuppressMessage("", "CSE0003")]
        private Profile GetDefaultProfile() {
            return new Profile() {
                Username = "Roytazz.5932",
                Avatar = "~/Content/stock_img/user4-128x128.jpg",
                Role = "Website Developer",
                Level = 4000,
                Rank = "MLG",
                SubDate = DateTime.Now.Subtract(new TimeSpan(300, 0, 0, 0))
            };
        }
    }
}