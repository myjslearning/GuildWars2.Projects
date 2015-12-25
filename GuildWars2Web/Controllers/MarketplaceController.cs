using System.Web.Mvc;

namespace GuildWars2Web.Controllers
{
    public class MarketplaceController : BaseController
    {
        [Authorize]
        public ActionResult Index() => View();
    }
}