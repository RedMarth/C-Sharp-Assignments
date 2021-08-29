using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace CST8333ProjectByJonathanCordingley
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

/*        void Application_Start(object sender, EventArgs e)
        {
            var sorted1 = "false";
            var sorted2 = "false";
            Application["Sorted1"] = sorted1;
            Application["Sorted2"] = sorted2;
        }*/
    }
}
