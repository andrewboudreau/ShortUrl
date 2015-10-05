using System.Web;
using System.Web.Routing;

namespace ShortUrl
{
    public class PositiveIntegerRouteConstraint : IRouteConstraint
    {
        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            int val;
            int.TryParse(values[parameterName].ToString(), out val);
            return val > 0;
        }
    }
}