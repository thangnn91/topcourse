using System;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Topcourse.Utility;

namespace CMS.Topcourse
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception exception = Server.GetLastError();
            Log4Net.LogInfo(exception.Message);
            Response.Clear();
            HttpException httpException = exception as HttpException;
            string action = string.Empty;
            if (httpException != null)
            {
                switch (httpException.GetHttpCode())
                {
                    case 404:
                        // page not found 
                        action = "page-not-found";
                        break;
                    case 500:
                        action = "ErrorInternalServer";
                        break;
                    default:
                        action = "page-not-found";
                        break;
                }
                Server.ClearError();
                var notfound_page = string.Format("{0}{1}", Config.UrlRoot, action);
                Response.Redirect(notfound_page, false);
            }
            else
            {
                //TODO: Define action for other exception types
                Server.TransferRequest("~/Home/ErrorInternalServer");
            }
        }
    }
}
