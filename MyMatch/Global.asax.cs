using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using MyMatch.Models;
using MyMatch.Models.chat;

namespace MyMatch
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
                     
            Database.SetInitializer<mymatch_profileDB>(new DropCreateDatabaseIfModelChanges<mymatch_profileDB>());
            Database.SetInitializer<mymatch_ChatDB>(new DropCreateDatabaseIfModelChanges<mymatch_ChatDB>());
            
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        public class dataDBInitializer : DropCreateDatabaseIfModelChanges<mymatch_profileDB>
        {
            protected override void Seed(mymatch_profileDB context)
            {
                base.Seed(context);
            }
        }
        public class dataDBInitializer1 : DropCreateDatabaseIfModelChanges<mymatch_ChatDB>
        {
            protected override void Seed(mymatch_ChatDB context)
            {
                base.Seed(context);
            }
        }
    }
}