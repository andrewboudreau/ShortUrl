using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ShortUrl
{
    public class ShortUrlMvcApplication : System.Web.HttpApplication
    {
        private static readonly Version version = typeof(ShortUrlMvcApplication).Assembly.GetName().Version;

        public static string Version()
        {
            return $"{version.Major}.{version.Minor}.{version.Build}";
        }
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
