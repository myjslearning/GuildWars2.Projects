using GuildWars2Web.Classes;
using GuildWars2Web.Models;
using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Web.Mvc;

namespace GuildWars2Web.Controllers
{
    public class BaseController : Controller        //Top-right profile button dissapears in mobile-layout
    {
        public BaseController() {
            ViewBag.Title = "Guild Wars 2 Web";
            ViewBag.Version = GetApplicationVersion();
        }

#pragma warning disable CSE0003 // Use expression-bodied members
        public ProfileViewModel GetViewModelProfile() {
            return GetDefaultProfile();

#pragma warning disable CS0162 // Unreachable code detected
            Profile profile = GetProfile();
#pragma warning restore CS0162 // Unreachable code detected
            return new ProfileViewModel {
                Username = profile.Username,
                Role = profile.Role,
                Level = profile.Level,
                Rank = profile.Rank,
                SubDate = profile.SubDate
                //Avatar = profile.Avatar,
            };
        }
#pragma warning restore CSE0003 // Use expression-bodied members

        private Models.Profile GetProfile() {
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
        private ProfileViewModel GetDefaultProfile() {
            return new ProfileViewModel() {
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