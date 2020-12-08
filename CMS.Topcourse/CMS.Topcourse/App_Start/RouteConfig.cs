using System.Web.Mvc;
using System.Web.Routing;

namespace CMS.Topcourse
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //    name: "spa2_id",
            //    url: "Home/spa_2/{partial}/{id}",
            //    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional, partial = UrlParameter.Optional }
            //);

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
