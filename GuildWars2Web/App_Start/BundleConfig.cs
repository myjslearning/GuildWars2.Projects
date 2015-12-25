using System.Web.Optimization;

namespace GuildWars2Web.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles) {
            bundles.Add(new StyleBundle("~/bundles/style").Include(
                "~/Content/CSS/GuildWars2.css",
                "~/Content/CSS/AdminLTE.min.css",
                "~/Content/CSS/skins/skin-red.css",
                "https://code.ionicframework.com/ionicons/2.0.1/css/ionicons.min.css",
                "https://maxcdn.bootstrapcdn.com/font-awesome/4.4.0/css/font-awesome.min.css"));
        }
    }
}
