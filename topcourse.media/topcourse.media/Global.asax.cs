using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace _1fintech.media
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //JobScheduler.Start();
        }

    

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

              
            //var Host = Request.UrlReferrer != null ? Request.UrlReferrer.Host : string.Empty;
            // string cookieDomain = FormsAuthentication.CookieDomain;

            //Common.Utilities.Log.NLogManager.LogMessage("Host:" + Host+":"+ cookieDomain);
            //if (!string.IsNullOrEmpty(Host) && !cookieDomain.Contains(Host)) return;
            Response.AppendHeader("Access-Control-Allow-Origin", "*");

            string Origin = Request.Headers["Origin"];
            if (!string.IsNullOrEmpty(Origin))
            {
                HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", Origin);
                Response.Headers.Add("Access-Control-Allow-Credentials", "true");

            }


            if (HttpContext.Current.Request.HttpMethod == "OPTIONS")
            {
                Response.Headers.Remove("X-Frame-Options");
                Response.AddHeader("X-Frame-Options", "AllowAll");
                HttpContext.Current.Response.AddHeader("Access-Control-Allow-Methods", "POST,GET,OPTIONS,PUT,DELETE");
                HttpContext.Current.Response.AddHeader("Access-Control-Allow-Headers",
                    "Content-Type, Authorization, Accept");
                HttpContext.Current.Response.End();
            }

            if (Request.Url.Host.StartsWith("www"))
            {
                UriBuilder builder = new UriBuilder(Request.Url);
                builder.Host = Request.Url.Host.Replace("www.", "");
                Response.StatusCode = 301;
                Response.AddHeader("Location", builder.ToString().Replace(":80", ""));
                Response.End();

            }
        }
    }
}
