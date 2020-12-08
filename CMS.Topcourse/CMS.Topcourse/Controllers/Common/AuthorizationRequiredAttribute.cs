using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMS.Topcourse.Controllers.Common
{
    public class AuthorizationRequiredAttribute : AuthorizeAttribute
    {
        #region Overrides of AuthorizeAttribute

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var skipAuthorization = filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true) 
                || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true);

            if (skipAuthorization) return;

            base.OnAuthorization(filterContext);

            //now look to see if this is an ajax request, and if so, we'll return a custom status code
            if (filterContext.Result == null || filterContext.Result.GetType() != typeof(HttpUnauthorizedResult) || !filterContext.HttpContext.Request.IsAjaxRequest())
                return;

            if (filterContext.Result.GetType() == typeof(HttpUnauthorizedResult) && filterContext.HttpContext.Request.IsAjaxRequest())
            {
                var redirectToUrl = System.Web.Security.FormsAuthentication.LoginUrl + "?ReturnUrl=" + filterContext.HttpContext.Server.UrlEncode(filterContext.HttpContext.Request.UrlReferrer.PathAndQuery);
                filterContext.HttpContext.Response.StatusCode = 403;
                filterContext.Result = new JavaScriptResult() { Script = "window.location = '" + redirectToUrl + "'" };
            }
        }
        #endregion
    }
}