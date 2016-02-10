using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace MyMatch.App_Start
{
    public class BundleMobileConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquerymobile")
                .Include("~/Scripts/jquery-1.8.2.js",
                "~/Scripts/AutoComplete.js",
                //"~/Scripts/myMobileScript.js",
                "~/Scripts/jquery-1.8.2.min.js",
                "~/Scripts/jquery.mobile-1.1.2.js",
                "~/Scripts/jquery-ui-1.8.24.js",
                "~/Scripts/jquery-ui-1.8.24.min.js",
                "~/Scripts/jquery.unobtrusive-ajax.js",
                "~/Scripts/jquery.unobtrusive-ajax.min.js",
                "~/Scripts/myscript.js"));

            bundles.Add(new StyleBundle("~/Content/Mobile/css")
                .Include("~/Content/Site.css"
                //"~/Content/themes/base/jquery.ui.datepicker.css",
                //"~/Content/jquery.ui.datepicker.mobile.css"
                ));

            bundles.Add(new StyleBundle("~/Content/jquerymobile/css")
                .Include("~/Content/jquery.mobile-1.1.2.css",
                "~/Content/jquery.mobile-1.1.2.min.css",
                "~/Content/jquery.mobile.structure-1.1.2.css",
                "~/Content/jquery.mobile.structure-1.1.2.min.css",
                "~/Content/jquery.mobile.theme-1.1.2.css",
                "~/Content/jquery.mobile.theme-1.1.2.min.css",
                "~/Content/jquery.mobile.theme-1.1.2.css"
                               ));
        }
    }
}