using System.Web.Mvc;
using System.Web.Routing;

namespace ShortUrl
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Expand",
                url: "{id}/{action}",
                defaults: new { controller = "Expand", action = "Index" },
                constraints: new { id = new PositiveIntegerRouteConstraint() });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
