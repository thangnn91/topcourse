using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;
using CMS.Topcourse.Controllers.Common;
using CMS.Topcourse.Models;
using Topcourse.DataAccess.DTO;
using Topcourse.DataAccess.Factory;
using Topcourse.Utility;

namespace CMS.Topcourse.Controllers
{
    public class CommonController : Controller
    {
        private string ListSystemID = ConfigurationManager.AppSettings["SystemID"];
        // GET: Common
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Header()
        {
            var userValidate = new UserValidation();
            var m_User = new Users();
            var urlLogin = Config.UrlRoot + "Account/FormLogin";
            try
            {
                string sUrl = Server.UrlEncode(HttpContext.Request.Url.ToString());
                Session["Redirect_Uri"] = sUrl;

                //var filterContext = HttpContext.Request.FilePath;
                //Kiểm tra Authen
                if (!userValidate.IsSigned())
                {
                    Response.Redirect(urlLogin + "?UrlReturn=", false);
                }
                else
                {
                    Session[SessionsManager.SESSION_USERID] = userValidate.UserId;
                    Session[SessionsManager.SESSION_USERNAME] = userValidate.UserName;
                    m_User = AbstractDAOFactory.Instance().CreateUsersDAO().GetByUsername(userValidate.UserName);
                    var IsAdministrator = userValidate.IsAdministrator;
                    var functions = new List<Functions>();
                    functions = IsAdministrator ? AbstractDAOFactory.Instance().CreateFunctionDAO().GetFunction_ByCondition(-1, -1, string.Empty, string.Empty, 1, 1, ListSystemID) : AbstractDAOFactory.Instance().CreateFunctionDAO().GetListFunctionByUserId(userValidate.UserId, -1, ListSystemID);

                    Session[SessionsManager.SESSION_FUNCTIONS] = functions;
                    ViewBag.UserName = userValidate.UserName;
                }
                //Log4Net.LogInfo("SESSION_USERID:" + Session[SessionsManager.SESSION_USERID].ToString());
                ViewBag.UserName = m_User.FullName;
                ViewBag.UrlLogin = urlLogin;
                ViewBag.Email = m_User.Email;
            }
            catch (Exception ex)
            {
                Log4Net.PublishException(ex);
            }
            return PartialView(m_User);
        }

        public ActionResult Menu_Left()
        {
            var functions = new List<Functions>();
            functions = (List<Functions>)Session[SessionsManager.SESSION_FUNCTIONS];
            return PartialView(functions);
        }

        public ActionResult Footer()
        {

            return PartialView();
        }

        public ActionResult ErrorPermission()
        {
            var urlLogin = Config.UrlRoot + "Account/Login";
            ViewBag.UrlLogin = urlLogin;
            return View();
        }

        public ActionResult ErrorNotPage()
        {
            return PartialView();
        }

        public ActionResult ErrorInternalServer()
        {
            return PartialView();
        }

        public static string GetChildMenu(Functions func, List<Functions> list)
        {
            var listChild = list.FindAll(f => f.ParentID == func.FunctionID && f.IsActive == true && f.IsDisplay == true);
            listChild.Sort((f1, f2) => f1.Order.CompareTo(f2.Order));
            var script = "";
            if (listChild.Count > 0)
            {
                script += "<li class=\"m-menu__item  m-menu__item--submenu\" id=\"" + func.ParentID + "\"  function-id=\"" + func.ParentID + "\"  aria-haspopup='true' m-menu-submenu-toggle='hover' >" +
                              "<a href=\"javascript:void(0);\" class=\"m-menu__link m-menu__toggle\" >" +
                              "<i class=\"m-menu__link-bullet m-menu__link-bullet--dot \">" +
                              "<span></span>" + "</i> " + "<span class=\"m-menu__link-text\">" +
                              func.FunctionName +
                              "</span> <i class='m-menu__ver-arrow la la-angle-right'></i>" +
                              "</a>";
                script += "<div class=\"m-menu__submenu\"><span class='m-menu__arrow'></span><ul class='m-menu__subnav'>";
                foreach (var obj in listChild)
                {
                    script += GetChildMenu(obj, list);
                }
                script += "</ul></div></li>";
            }
            else
            {
                script += "<li function-id=\"" + func.FunctionID + "\" class=\"m-menu__item\" data-action=\"" + func.ActionName + "\" aria-haspopup='true'> " +
                              "<a href=\"" + Config.UrlRoot + "#/" + func.Url + "\" class=\"m-menu__link\" >" +
                                  "<i class=\"m-menu__link-bullet m-menu__link-bullet--dot\">" +
                                  "<span></span>" +
                                  "</i>" +
                                  "<span class=\"m-menu__link-text\">" + func.FunctionName + "</span>" +
                              "</a>" +
                          "</li>";
            }
            return script;
        }
    }
}