using Common.Utilities.IpAddress;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;

namespace _1fintech.media
{
    public class ImagesHandler : IHttpHandler
    {
        #region IHttpHandler Members

        bool IHttpHandler.IsReusable
        {
            get { return true; }
        }

        void IHttpHandler.ProcessRequest(HttpContext context)
        {
            HttpRequest req = context.Request;
            string path = req.PhysicalPath;
            string extension = null;

            if (req.UrlReferrer != null && req.UrlReferrer.Host.Length > 0)
            {
                if (CultureInfo.InvariantCulture.CompareInfo.Compare(req.Url.Host, req.UrlReferrer.Host, CompareOptions.IgnoreCase) != 0)
                {
                    path = context.Server.MapPath("~/images/backoff.gif");
                }
            }
            string contentType = null;
            extension = Path.GetExtension(path).ToLower();
            switch (extension)
            {
                case ".gif":
                    contentType = "image/gif";
                    break;
                case ".jpg":
                    contentType = "image/jpeg";
                    break;
                case ".png":
                    contentType = "image/png";
                    break;
                default:
                    throw new NotSupportedException("Unrecognized image type.");
            }
            if (!File.Exists(path))
            {
                context.Response.ContentType = "text/plain";
                context.Response.StatusCode = 404;
                context.Response.Write("Image not found");
            }
            else if (req.RawUrl.StartsWith("/Upload/Images/VerifyAccount") && IPAddressHelper.GetClientIP() != "::1")
            {
                context.Response.ContentType = "text/plain";
                context.Response.StatusCode = 403;
                context.Response.Write("Access deny :D");
            }
            else
            {
                context.Response.StatusCode = 200;
                context.Response.ContentType = contentType;
                context.Response.WriteFile(path);
            }
        }

        #endregion
    }
}