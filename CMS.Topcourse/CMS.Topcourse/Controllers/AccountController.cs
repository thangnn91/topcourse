using System;
using System.Configuration;
using System.Web.Mvc;
using CMS.Topcourse.Controllers.Common;
using CMS.Topcourse.Models;
using Topcourse.DataAccess.Factory;
using Topcourse.Utility;
using Topcourse.Utility.Security;

namespace CMS.Topcourse.Controllers
{
    public class AccountController : Controller
    {
        private int SystemID = Int32.Parse(ConfigurationManager.AppSettings["SystemID"]);  // Hệ thống 

        // GET: /Account/
        private UserValidation m_UserValidation = new UserValidation();
        public ActionResult FormLogin(string act)
        {
            if (!string.IsNullOrEmpty(act) && act == "out")
            {
                UserValidation m_UserValidation = new UserValidation();   
                m_UserValidation.SignOut();
                Session.Abandon();
                Session.RemoveAll();
                Response.Redirect("~/", true);
            }
            ViewBag.act = String.IsNullOrEmpty(act) ? "0" : act;
            return View();
        }

        [HttpPost]
        public JsonResult Login(string Username, string Password)
        {
            try
            {
                if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
                    return Json(new { success = false, statusCode = -1, msg = "Dữ liệu không được bỏ trống" });
                var password = Encrypt.Md5(Password.Trim());
                var responseCode = 0;
                var m_Users = AbstractDAOFactory.Instance().CreateUsersDAO().Authentication(Username.Trim(), "", password, SystemID, ref responseCode);
                if (responseCode > 0)
                {
                    if (m_Users != null && m_Users.UserID > 0)
                    {
                        if (m_Users.IsLock)
                        {
                            //Mở khóa User Locked
                            var m_unlock = AbstractDAOFactory.Instance().CreateUsersDAO().UpdateUserLock(m_Users.UserID, false);
                            Log4Net.LogInfo("Unlock Tài khoản :" + m_unlock);
                        }
                        if (m_Users.Status)
                        {
                            Session["LoginType"] = 1;
                            string SessionID = Guid.NewGuid().ToString();
                            m_UserValidation.SignIn(m_Users.UserID, m_Users.Username, m_Users.IsAdministrator, SessionID, "");
                            var UrlRedirect = Session["Redirect_Uri"] == null ? Config.UrlRoot : Server.UrlDecode(Session["Redirect_Uri"].ToString());
                            return Json(new { success = true, statusCode = 1, msg = "Đăng Nhập Thành Công", url = UrlRedirect });
                        }
                        return Json(new { success = false, statusCode = -102, msg = "Tài khoản của bạn đã bị block" });
                    }
                }
                return Json(new { success = false, statusCode = -1, msg = "Tài khoản hoặc mật khẩu không chính xác" });
            }
            catch (Exception ex)
            {
                Log4Net.PublishException(ex);
                return Json(new { success = false, statusCode = -99, msg = "Hệ thống bận vui lòng quay lại sau" });
            }
        }

        [HttpPost]
        public JsonResult CheckAuthen()
        {
            UserValidation userValidate = new UserValidation();
            if (userValidate.IsSigned() == false)
            {
                return Json(new { success = false, statusCode = -1, msg = "Tài khoản chưa login" });
            }
            return Json(new { success = true, statusCode = 1, msg = "Vẫn login" });
        }



        // Tài khoản
    }
}