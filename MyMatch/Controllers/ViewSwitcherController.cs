using System.Web.Mvc;
using System.Web.WebPages;

namespace MyMatch.Controllers
{
  public class ViewSwitcherController : Controller
  {
    public RedirectResult SwitchView(bool mobile, string mobileType, string returnUrl)
    {
      mobileType = (mobileType == null) ? string.Empty : mobileType.Trim().ToLower();
      if (mobileType == "default")
      {
        HttpContext.ClearOverriddenBrowser();
      }
      else
      {
        if (mobileType == string.Empty) mobileType = "mobile";
        switch (mobileType)
        {
          case "desktop":
            HttpContext.SetOverriddenBrowser("Mozilla/5.0 (Macintosh; U; Intel Mac OS X 10_6_8; en-us) AppleWebKit/534.55.3 (KHTML, like Gecko) Version/5.1.5 Safari/534.55.3");
            break;
          case "tablet":
          case "ipad":
            HttpContext.SetOverriddenBrowser("Mozilla/5.0 (iPad; U; CPU OS 4_3_3 like Mac OS X; en-us) AppleWebKit/533.17.9 (KHTML, like Gecko) Version/5.0.2 Mobile/8J2 Safari/6533.18.5");
            break;
          case "phone":
          case "iphone":
            HttpContext.SetOverriddenBrowser("Mozilla/5.0 (iPhone; U; CPU iPhone OS 4_3_3 like Mac OS X; en-us) AppleWebKit/533.17.9 (KHTML, like Gecko) Version/5.0.2 Mobile/8J2 Safari/6533.18.5");
            break;
          default:
            HttpContext.SetOverriddenBrowser(BrowserOverride.Mobile);
            break;
        }
      }
      return Redirect(returnUrl);
    }
  }
}
