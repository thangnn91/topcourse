using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using CMS.Topcourse.Controllers.Common;
using CMS.Topcourse.Models;
using Newtonsoft.Json;
using Topcourse.Utility;
using Topcourse.DataAccess.DTO;
using Topcourse.DataAccess.Factory;
using Topcourse.Utility.Security;
using Account = Topcourse.DataAccess.DTO.Account;

namespace CMS.Topcourse.Controllers
{
    public class UserAccountController : Controller
    {
        private UserFunction Permission { get { return ((UserFunction)Session[SessionsManager.SESSION_PERMISSION]); } }
        // Tài khoản
        public ActionResult Index()
        {
            return View();
        }

        // Tài khoản học viên

        [AuthorizationRequired]
        public ActionResult Account()
        {
            var userValidate = new UserValidation();
            var urlLogin = Config.UrlRoot + "Account/FormLogin";
            if (!userValidate.IsSigned())
            {
                return Json(new { redirecturl = Config.UrlRoot + "Account/FormLogin" }, JsonRequestBehavior.AllowGet);
            }
            var checkPermission = userValidate.CheckPermissionUser("Account", userValidate.UserId, userValidate.IsAdministrator);
            if (!checkPermission || Permission == null)
            {
                return Json(new { redirecturl = Config.UrlRoot + "common/errorpermission" }, JsonRequestBehavior.AllowGet);
            }

            return PartialView();
        }

        [AuthorizationRequired]
        public ActionResult Account_Partial(byte userType, byte status)
        {
            var userValidate = new UserValidation();
            var urlLogin = Config.UrlRoot + "Account/FormLogin";
            if (!userValidate.IsSigned())
            {
                return Json(new { redirecturl = Config.UrlRoot + "Account/FormLogin" }, JsonRequestBehavior.AllowGet);
            }
            var checkPermission = userValidate.CheckPermissionUser("Account", userValidate.UserId, userValidate.IsAdministrator);
            if (!checkPermission || Permission == null)
            {
                return Json(new { redirecturl = Config.UrlRoot + "common/errorpermission" }, JsonRequestBehavior.AllowGet);
            }
            var list = AbstractDAOFactory.Instance().CreateAccountDAO().GetListUsers(0, "", userType, status);
            var listjson = Regex.Replace(JsonConvert.SerializeObject(list), @"\\r\\n|\\n|\\r|\\t", "");
            ViewBag.listjson = listjson;
            var permission = Regex.Replace(JsonConvert.SerializeObject((UserFunction)Session[SessionsManager.SESSION_PERMISSION]), @"\\r\\n|\\n|\\r|\\t", "");
            ViewBag.permission = permission;
            return PartialView();
        }

        [AuthorizationRequired]
        public ActionResult Account_GetInfo(int id)
        {
            var userValidate = new UserValidation();
            var urlLogin = Config.UrlRoot + "Account/FormLogin";
            if (!userValidate.IsSigned())
            {
                return Json(new { redirecturl = Config.UrlRoot + "Account/FormLogin" }, JsonRequestBehavior.AllowGet);
            }
            var checkPermission = userValidate.CheckPermissionUser("Account", userValidate.UserId, userValidate.IsAdministrator);
            if (!checkPermission || Permission == null)
            {
                return Json(new { redirecturl = Config.UrlRoot + "common/errorpermission" }, JsonRequestBehavior.AllowGet);
            }
            if (id <= 0)
            {
                return PartialView(new Account());
            }
            var account = AbstractDAOFactory.Instance().CreateAccountDAO().User_GetFull(id, "");
            return PartialView(account);
        }


        // Tk trung tâm, trường
        [AuthorizationRequired]
        public ActionResult Account_Education()
        {
            var userValidate = new UserValidation();
            var urlLogin = Config.UrlRoot + "Account/FormLogin";
            if (!userValidate.IsSigned())
            {
                return Json(new { redirecturl = Config.UrlRoot + "Account/FormLogin" }, JsonRequestBehavior.AllowGet);
            }
            var checkPermission = userValidate.CheckPermissionUser("Account_Education", userValidate.UserId, userValidate.IsAdministrator);
            if (!checkPermission || Permission == null)
            {
                return Json(new { redirecturl = Config.UrlRoot + "common/errorpermission" }, JsonRequestBehavior.AllowGet);
            }
            return PartialView();
        }

        [AuthorizationRequired]
        public ActionResult Account_Education_Partial(byte userType, byte status)
        {
            var userValidate = new UserValidation();
            var urlLogin = Config.UrlRoot + "Account/FormLogin";
            if (!userValidate.IsSigned())
            {
                return Json(new { redirecturl = Config.UrlRoot + "Account/FormLogin" }, JsonRequestBehavior.AllowGet);
            }
            var checkPermission = userValidate.CheckPermissionUser("Account_Education", userValidate.UserId, userValidate.IsAdministrator);
            if (!checkPermission || Permission == null)
            {
                return Json(new { redirecturl = Config.UrlRoot + "common/errorpermission" }, JsonRequestBehavior.AllowGet);
            }
            var list = AbstractDAOFactory.Instance().CreateAccountDAO().GetListUsers(0, "", userType, status);
            var listjson = Regex.Replace(JsonConvert.SerializeObject(list), @"\\r\\n|\\n|\\r|\\t", "");
            ViewBag.listjson = listjson;
            var permission = Regex.Replace(JsonConvert.SerializeObject((UserFunction)Session[SessionsManager.SESSION_PERMISSION]), @"\\r\\n|\\n|\\r|\\t", "");
            ViewBag.permission = permission;
            return PartialView();
        }


        [AuthorizationRequired]
        public ActionResult Account_Education_GetInfo(int id)
        {
            var userValidate = new UserValidation();
            var urlLogin = Config.UrlRoot + "Account/FormLogin";
            if (!userValidate.IsSigned())
            {
                return Json(new { redirecturl = Config.UrlRoot + "Account/FormLogin" }, JsonRequestBehavior.AllowGet);
            }
            var checkPermission = userValidate.CheckPermissionUser("Account_Education", userValidate.UserId, userValidate.IsAdministrator);
            if (!checkPermission || Permission == null)
            {
                return Json(new { redirecturl = Config.UrlRoot + "common/errorpermission" }, JsonRequestBehavior.AllowGet);
            }
            if (id <= 0)
            {
                return PartialView(new Account());
            }
            var account = AbstractDAOFactory.Instance().CreateAccountDAO().User_GetFull(id, "");
            return PartialView(account);
        }
        // 



        // tao tk
        [HttpPost]
        [AuthorizationRequired]
        public JsonResult RegisterUser(Account request)
        {
            var returnData = new ReturnData();
            var userValidate = new UserValidation();
            if (!userValidate.IsSigned())
            {
                return Json(new { redirecturl = Config.UrlRoot + "Account/FormLogin" }, JsonRequestBehavior.AllowGet);
            }
            var checkPermission = userValidate.CheckPermissionUser("Account", userValidate.UserId, userValidate.IsAdministrator);
            if (!checkPermission || Permission == null)
            {
                returnData.ResponseCode = -101;
                returnData.Description = "Bạn không có quyền sử dụng chức năng này";
                return Json(returnData);
            }
            try
            {
                if (string.IsNullOrEmpty(request?.Username)
                    || string.IsNullOrEmpty(request.Password)
                    || string.IsNullOrEmpty(request.Mobile)
                    || string.IsNullOrEmpty(request.Fullname))
                {
                    returnData.ResponseCode = -600;
                    returnData.Description = "Vui lòng nhập đẩy đủ thông tin";
                    return Json(returnData);
                }

                request.CreatedUser = userValidate.UserName;
                request.Password = Encrypt.Md5(request.Username + request.Password);

                // ảnh Avatar
                 
                var result = AbstractDAOFactory.Instance().CreateAccountDAO().CMS_RegisterUser(request);
                returnData.ResponseCode = result;
                if (result >= 0)
                {
                    returnData.Description = "Tạo tài khoản thành công";

                    // Ghi log
                    AbstractDAOFactory.Instance().CreateUsersLogDAO().InsertUsersLog(new UsersLog
                    {
                        FunctionID = Permission.FunctionID,
                        FunctionName = Permission.FunctionName,
                        Description = returnData.Description + " TK:" + request.Username,
                        UserID = userValidate.UserId,
                        ClientIP = userValidate.ClientIP
                    });
                }
                else
                {
                    switch (result)
                    {
                        case -46:
                            returnData.Description = "Tài khoản đã tồn tại trên hệ thống";
                            break;
                        case -54:
                            returnData.Description = "Email đã tồn tại trên hệ thống";
                            break;
                        case -600:
                            returnData.Description = "Vui lòng nhập đầy đủ thông tin";
                            break;
                        default:
                            returnData.Description = "Lỗi từ hệ thống. Vui lòng quay lại sau";
                            break;
                    }
                }

                return Json(returnData);
            }
            catch (Exception ex)
            {
                Log4Net.PublishException(ex);
                returnData.ResponseCode = -969;
                returnData.Description = "Hệ thống đang bận. Vui lòng quay lại sau";
                return Json(returnData);
            }
        }

        // Đổi thông tin tài khoản
        [HttpPost]
        [AuthorizationRequired]
        public JsonResult User_UpdateInfo(Account request)
        {
            var returnData = new ReturnData();
            var userValidate = new UserValidation();
            if (!userValidate.IsSigned())
            {
                return Json(new { redirecturl = Config.UrlRoot + "Account/FormLogin" }, JsonRequestBehavior.AllowGet);
            }
            var checkPermission = userValidate.CheckPermissionUser("Account", userValidate.UserId, userValidate.IsAdministrator);
            if (!checkPermission || Permission == null)
            {
                returnData.ResponseCode = -101;
                returnData.Description = "Bạn không có quyền sử dụng chức năng này";
                return Json(returnData);
            }
            try
            {
                if (string.IsNullOrEmpty(request?.Username)
                    || request.UserID <= 0
                    || string.IsNullOrEmpty(request.Mobile)
                    || string.IsNullOrEmpty(request.Fullname)
                    || string.IsNullOrEmpty(request.UserProfileInfo))
                {
                    returnData.ResponseCode = -600;
                    returnData.Description = "Vui lòng nhập đẩy đủ thông tin";
                    return Json(returnData);
                }

                request.CreatedUser = userValidate.UserName;
                var result = AbstractDAOFactory.Instance().CreateAccountDAO().User_UpdateProfile(request);
                returnData.ResponseCode = result;
                if (result >= 0)
                {
                    returnData.Description = "Cập nhật thông tin tài khoản thành công";
                    // Ghi log
                    AbstractDAOFactory.Instance().CreateUsersLogDAO().InsertUsersLog(new UsersLog
                    {
                        FunctionID = Permission.FunctionID,
                        FunctionName = Permission.FunctionName,
                        Description = returnData.Description + " TK:" + request.Username,
                        UserID = userValidate.UserId,
                        ClientIP = userValidate.ClientIP
                    });
                }
                else
                {
                    switch (result)
                    {
                        case -50:
                            returnData.Description = "Tài khoản không tôn tại trên hệ thống";
                            break;
                        case -49:
                            returnData.Description = "Tài khoản không hoạt động.";
                            break;
                        case -48:
                            returnData.Description = "Tài khoản đang bị khóa.";
                            break;
                        case -33:
                            returnData.Description = "Tài khoản chưa được kích hoạt.";
                            break;
                        case -600:
                            returnData.Description = "Vui lòng nhập đầy đủ thông tin";
                            break;
                        default:
                            returnData.Description = "Lỗi từ hệ thống. Vui lòng quay lại sau";
                            break;
                    }
                }
                return Json(returnData);
            }
            catch (Exception ex)
            {
                Log4Net.PublishException(ex);
                returnData.ResponseCode = -969;
                returnData.Description = "Hệ thống đang bận. Vui lòng quay lại sau";
                return Json(returnData);
            }
        }

        // Khóa mở tài khoản
        [HttpPost]
        [AuthorizationRequired]
        public JsonResult Block_User(Account request)
        {
            var returnData = new ReturnData();
            var userValidate = new UserValidation();
            if (!userValidate.IsSigned())
            {
                return Json(new { redirecturl = Config.UrlRoot + "Account/FormLogin" }, JsonRequestBehavior.AllowGet);
            }
            try
            {
                if (string.IsNullOrEmpty(request?.Username))
                {
                    returnData.ResponseCode = -600;
                    returnData.Description = "Vui lòng nhập đẩy đủ thông tin";
                    return Json(returnData);
                }

                request.CreatedUser = userValidate.UserName;
                request.Reason = "Khóa tài khoản bởi admin " + userValidate.UserName;

                var account = AbstractDAOFactory.Instance().CreateAccountDAO().User_GetSingle(0, request.Username);
                if (account == null || account.UserID <= 0)
                {
                    returnData.ResponseCode = -101;
                    returnData.Description = "Tài khoản không tôn tại trên hệ thống";
                    return Json(returnData);
                }

                var result = AbstractDAOFactory.Instance().CreateAccountDAO().User_Block(request);
                returnData.Extend = AbstractDAOFactory.Instance().CreateAccountDAO().User_GetSingle(0, request.Username).Status.ToString();
                returnData.ResponseCode = result;
                if (result >= 0)
                {
                    returnData.Description = "Khóa tài khoản thành công";

                    // Ghi log
                    AbstractDAOFactory.Instance().CreateUsersLogDAO().InsertUsersLog(new UsersLog
                    {
                        FunctionID = Permission.FunctionID,
                        FunctionName = Permission.FunctionName,
                        Description = returnData.Description + " TK:" + request.Username,
                        UserID = userValidate.UserId,
                        ClientIP = userValidate.ClientIP
                    });
                }
                else
                {
                    switch (result)
                    {
                        case -33:
                            returnData.Description = "Tài khoản chưa kích hoạt";
                            break;
                        case -50:
                            returnData.Description = "Tài khoản không tồn tại";
                            break;
                        case -49:
                            returnData.Description = "Tài khoản không hoạt động";
                            break;
                        case -99:
                            returnData.Description = "Lỗi hệ thống. Vui lòng liên hệ quản trị viên";
                            break;
                        case -48:
                            returnData.Description = "Tài khoản đang bị khóa";
                            break;
                        case -600:
                            returnData.Description = "Vui lòng nhập đầy đủ thông tin";
                            break;
                        default:
                            returnData.Description = "Lỗi từ hệ thống. Vui lòng quay lại sau";
                            break;
                    }
                }

                return Json(returnData);
            }
            catch (Exception ex)
            {
                Log4Net.PublishException(ex);
                returnData.ResponseCode = -969;
                returnData.Description = "Hệ thống đang bận. Vui lòng quay lại sau";
                return Json(returnData);
            }
        }

        [HttpPost]
        [AuthorizationRequired]
        public JsonResult UnBlock_User(Account request)
        {
            var returnData = new ReturnData();
            var userValidate = new UserValidation();
            if (!userValidate.IsSigned())
            {
                return Json(new { redirecturl = Config.UrlRoot + "Account/FormLogin" }, JsonRequestBehavior.AllowGet);
            }
            try
            {
                if (string.IsNullOrEmpty(request?.Username))
                {
                    returnData.ResponseCode = -600;
                    returnData.Description = "Vui lòng nhập đẩy đủ thông tin";
                    return Json(returnData);
                }

                request.CreatedUser = userValidate.UserName;
                request.Reason = "Mở khóa tài khoản bởi admin " + userValidate.UserName;
                var account = AbstractDAOFactory.Instance().CreateAccountDAO().User_GetSingle(0, request.Username);
                if (account == null || account.UserID <= 0)
                {
                    returnData.ResponseCode = -101;
                    returnData.Description = "Tài khoản không tôn tại trên hệ thống";
                    return Json(returnData);
                }

                var result = AbstractDAOFactory.Instance().CreateAccountDAO().User_UnBlock(request);
                returnData.Extend = AbstractDAOFactory.Instance().CreateAccountDAO().User_GetSingle(0, request.Username).Status.ToString();
                returnData.ResponseCode = result;
                if (result >= 0)
                {
                    returnData.Description = "Mở khóa tài khoản thành công";
                    // Ghi log
                    AbstractDAOFactory.Instance().CreateUsersLogDAO().InsertUsersLog(new UsersLog
                    {
                        FunctionID = Permission.FunctionID,
                        FunctionName = Permission.FunctionName,
                        Description = returnData.Description + " TK:" + request.Username,
                        UserID = userValidate.UserId,
                        ClientIP = userValidate.ClientIP
                    });
                }
                else
                {
                    switch (result)
                    {
                        case -50:
                            returnData.Description = "Tài khoản không tồn tại";
                            break;
                        case -47:
                            returnData.Description = "Tài khoản không bị khóa";
                            break;
                        case -99:
                            returnData.Description = "Lỗi hệ thống. Vui lòng liên hệ quản trị viên";
                            break;
                        case -600:
                            returnData.Description = "Vui lòng nhập đầy đủ thông tin";
                            break;
                        default:
                            returnData.Description = "Lỗi từ hệ thống. Vui lòng quay lại sau";
                            break;
                    }
                }
                return Json(returnData);
            }
            catch (Exception ex)
            {
                Log4Net.PublishException(ex);
                returnData.ResponseCode = -969;
                returnData.Description = "Hệ thống đang bận. Vui lòng quay lại sau";
                return Json(returnData);
            }
        }

        // Đổi mật khẩu
        [HttpPost]
        [AuthorizationRequired]
        public JsonResult User_ChangePass(int userId, string newPassword)
        {
            var returnData = new ReturnData();
            var userValidate = new UserValidation();
            if (!userValidate.IsSigned())
            {
                return Json(new { redirecturl = Config.UrlRoot + "Account/FormLogin" }, JsonRequestBehavior.AllowGet);
            }
            try
            {
                if (userId <= 0)
                {
                    returnData.ResponseCode = -1;
                    returnData.Description = "Không tìm thấy tài khoản cần reset mật khẩu. Vui lòng thử lại";
                    return Json(returnData);
                }

                if (string.IsNullOrEmpty(newPassword))
                {
                    returnData.ResponseCode = -600;
                    returnData.Description = "Vui lòng nhập password mới";
                    return Json(returnData);
                }

                var user = AbstractDAOFactory.Instance().CreateAccountDAO().User_GetSingle(userId, "");
                if (user == null || user.UserID <= 0)
                {
                    returnData.ResponseCode = -101;
                    returnData.Description = "Tài khoản không tôn tại trên hệ thống";
                    return Json(returnData);
                }

                var password = Encrypt.Md5(user.Username + newPassword);
                byte logType = 7; //--4:ChangePassword, 5: ResetPassBySMS, 6: ResetPassByEmail, 7: ResetPassBySupport(CMS)
                var description = "Reset mật khẩu bởi admin " + userValidate.UserName;

                var result = AbstractDAOFactory.Instance().CreateAccountDAO().User_ChangePassword(userId, password, logType, userValidate.UserName, description);
                returnData.ResponseCode = result;
                if (result >= 0)
                {
                    returnData.Description = "Reset mật khẩu thành công";
                    // Ghi log
                    AbstractDAOFactory.Instance().CreateUsersLogDAO().InsertUsersLog(new UsersLog
                    {
                        FunctionID = Permission.FunctionID,
                        FunctionName = Permission.FunctionName,
                        Description = returnData.Description + " TK:" + user.Username,
                        UserID = userValidate.UserId,
                        ClientIP = userValidate.ClientIP
                    });
                }
                else
                {
                    switch (result)
                    {
                        case -49:
                            returnData.Description = "Tài khoản không hoạt động";
                            break;
                        case -48:
                            returnData.Description = "Tài khoản đang bị khóa";
                            break;
                        case -50:
                            returnData.Description = "Tài khoản không tôn tại trên hệ thống";
                            break;
                        case -33:
                            returnData.Description = "Tài khoản Chưa kích hoạt";
                            break;
                        case -53:
                            returnData.Description = "Mật khẩu không hợp lệ. Bao gồm chữ hoa chữ thường và số";
                            break;
                        case -99:
                            returnData.Description = "Lỗi hệ thống. Vui lòng liên hệ quản trị viên";
                            break;
                        case -600:
                            returnData.Description = "Vui lòng nhập đầy đủ thông tin";
                            break;
                        default:
                            returnData.Description = "Lỗi từ hệ thống. Vui lòng quay lại sau";
                            break;
                    }
                }

                return Json(returnData);
            }
            catch (Exception ex)
            {
                Log4Net.PublishException(ex);
                returnData.ResponseCode = -969;
                returnData.Description = "Hệ thống đang bận. Vui lòng quay lại sau";
                return Json(returnData);
            }
        }


        [HttpPost]
        [AuthorizationRequired]
        public JsonResult User_Delete(int userId)
        {
            var returnData = new ReturnData();
            var userValidate = new UserValidation();
            if (!userValidate.IsSigned())
            {
                return Json(new { redirecturl = Config.UrlRoot + "Account/FormLogin" }, JsonRequestBehavior.AllowGet);
            }
            try
            {
                if (userId <= 0)
                {
                    returnData.ResponseCode = -1;
                    returnData.Description = "Không tìm thấy tài khoản cần xóa. Vui lòng thử lại";
                    return Json(returnData);
                }

                var user = AbstractDAOFactory.Instance().CreateAccountDAO().User_GetSingle(userId, "");
                if (user == null || user.UserID <= 0)
                {
                    returnData.ResponseCode = -2;
                    returnData.Description = "Tài khoản không hợp lệ. Vui lòng thử lại";
                    return Json(returnData);
                }

                var result = AbstractDAOFactory.Instance().CreateAccountDAO().CMS_DeleteUser(userId, "", userValidate.UserName);
                returnData.ResponseCode = result;
                if (result >= 0)
                {
                    returnData.Description = "Xóa tài khoản thành công";
                    // Ghi log
                    AbstractDAOFactory.Instance().CreateUsersLogDAO().InsertUsersLog(new UsersLog
                    {
                        FunctionID = Permission.FunctionID,
                        FunctionName = Permission.FunctionName,
                        Description = returnData.Description + " TK:" + user.Username,
                        UserID = userValidate.UserId,
                        ClientIP = userValidate.ClientIP
                    });
                }
                else
                {
                    switch (result)
                    {
                        case -50:
                            returnData.Description = "Tài khoản không tôn tại trên hệ thống";
                            break;
                        case -99:
                            returnData.Description = "Lỗi hệ thống. Vui lòng liên hệ quản trị viên";
                            break;
                        case -600:
                            returnData.Description = "Vui lòng nhập đầy đủ thông tin";
                            break;
                        default:
                            returnData.Description = "Lỗi từ hệ thống. Vui lòng quay lại sau";
                            break;
                    }
                }
                return Json(returnData);
            }
            catch (Exception ex)
            {
                Log4Net.PublishException(ex);
                returnData.ResponseCode = -969;
                returnData.Description = "Hệ thống đang bận. Vui lòng quay lại sau";
                return Json(returnData);
            }
        }

        //Lấy country vn ( done )
        [HttpGet]
        [AuthorizationRequired]
        public JsonResult GetLocation(int locationType, int? parentCode)
        {
            try
            {
                var userValidate = new UserValidation();
                if (!userValidate.IsSigned())
                {
                    var url = Config.UrlRoot + "Account/FormLogin";
                    var urlLogout = string.Format("{0}?act=out", url);
                    return Json(new { redirectUrl = Url.Action("FormLogin", "Account"), isRedirect = true });
                }
                var listLocation = AbstractDAOFactory.Instance().CreateAccountDAO().GetLocation(locationType, parentCode, -1);
                return Json(new { status = 1, msg = "Success", item = listLocation }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Log4Net.PublishException(ex);
                return Json(new { status = -99, msg = " Hệ Thống Đang Bận. Xin thử lại sau " }, JsonRequestBehavior.AllowGet);
            }
        }

        // Comment
        [AuthorizationRequired]
        public ActionResult Comment()
        {
            var userValidate = new UserValidation();
            var urlLogin = Config.UrlRoot + "Account/FormLogin";
            if (!userValidate.IsSigned())
            {
                return Json(new { redirecturl = Config.UrlRoot + "Account/FormLogin" }, JsonRequestBehavior.AllowGet);
            }
            var checkPermission = userValidate.CheckPermissionUser("Comment", userValidate.UserId, userValidate.IsAdministrator);
            if (!checkPermission || Permission == null)
            {
                return Json(new { redirecturl = Config.UrlRoot + "common/errorpermission" }, JsonRequestBehavior.AllowGet);
            }
            var list_education = AbstractDAOFactory.Instance().CreateAccountDAO().Education_GetList(new Request_Education()).ToList();
            ViewBag.list_education = list_education;
            var list_course = AbstractDAOFactory.Instance().CreateCourseDAO().GetListCourse(new CourseRequest() { CourseType = 1 }).ToList();
            ViewBag.list_course = list_course;
            var list_scholarship = AbstractDAOFactory.Instance().CreateCourseDAO().Scholarship_GetPage(new Scholarship(){Type = 0 , FromDate = new DateTime(), ToDate = new DateTime()}).ToList();
            ViewBag.list_scholarship = list_scholarship;
            return PartialView();
        }


        [HttpGet]
        [AuthorizationRequired]
        public JsonResult GetCourse(byte courseType)
        {
            try
            {
                var userValidate = new UserValidation();
                if (!userValidate.IsSigned())
                {
                    var url = Config.UrlRoot + "Account/FormLogin";
                    var urlLogout = string.Format("{0}?act=out", url);
                    return Json(new { redirectUrl = Url.Action("FormLogin", "Account"), isRedirect = true });
                }
                var listCourse = AbstractDAOFactory.Instance().CreateCourseDAO().GetListCourse(new CourseRequest() { CourseType = courseType }).ToList();
                return Json(new { status = 1, msg = "Success", item = listCourse }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Log4Net.PublishException(ex);
                return Json(new { status = -696, msg = " Hệ Thống Đang Bận. Xin thử lại sau " }, JsonRequestBehavior.AllowGet);
            }
        }


        [AuthorizationRequired]
        public ActionResult Comment_Partial(int targetId, byte type, string userName)
        {
            var userValidate = new UserValidation();
            var urlLogin = Config.UrlRoot + "Account/FormLogin";
            if (!userValidate.IsSigned())
            {
                return Json(new { redirecturl = Config.UrlRoot + "Account/FormLogin" }, JsonRequestBehavior.AllowGet);
            }
            var checkPermission = userValidate.CheckPermissionUser("Comment", userValidate.UserId, userValidate.IsAdministrator);
            if (!checkPermission || Permission == null)
            {
                return Json(new { redirecturl = Config.UrlRoot + "common/errorpermission" }, JsonRequestBehavior.AllowGet);
            }

            var userId = 0;
            if (!string.IsNullOrEmpty(userName))
            {
                var user = AbstractDAOFactory.Instance().CreateAccountDAO().User_GetSingle(0, userName);
                userId = user.UserID;
            }

            var request = new Comment_Request
            {
                TargetID = targetId,
                Type = type,
                UserID = userId
            };

            var list = AbstractDAOFactory.Instance().CreateAccountDAO().Comment_GetList(request);
            var listjson = Regex.Replace(JsonConvert.SerializeObject(list), @"\\r\\n|\\n|\\r|\\t", "");
            ViewBag.listjson = listjson;
            var permission = Regex.Replace(JsonConvert.SerializeObject((UserFunction)Session[SessionsManager.SESSION_PERMISSION]), @"\\r\\n|\\n|\\r|\\t", "");
            ViewBag.permission = permission;
            return PartialView();
        }

        [AuthorizationRequired]
        public ActionResult Comment_Detail_Partial(int parentId, int targetId, byte type, string title)
        {
            var userValidate = new UserValidation();
            var urlLogin = Config.UrlRoot + "Account/FormLogin";
            if (!userValidate.IsSigned())
            {
                return Json(new { redirecturl = Config.UrlRoot + "Account/FormLogin" }, JsonRequestBehavior.AllowGet);
            }
            var checkPermission = userValidate.CheckPermissionUser("Comment", userValidate.UserId, userValidate.IsAdministrator);
            if (!checkPermission || Permission == null)
            {
                return Json(new { redirecturl = Config.UrlRoot + "common/errorpermission" }, JsonRequestBehavior.AllowGet);
            }
            var request = new Comment_Request
            {
                TargetID = targetId,
                Type = type,
                ParentID = parentId
            };
            ViewBag.parentId = parentId;
            ViewBag.targetId = targetId;
            ViewBag.type = type;
            ViewBag.title = title;
            var list = AbstractDAOFactory.Instance().CreateAccountDAO().Comment_GetList(request);
            return PartialView(list);
        }

        [AuthorizationRequired]
        public ActionResult Comment_Detail_Partial_GetComment(int parentId, int targetId, byte type)
        {
            var userValidate = new UserValidation();
            var urlLogin = Config.UrlRoot + "Account/FormLogin";
            if (!userValidate.IsSigned())
            {
                return Json(new { redirecturl = Config.UrlRoot + "Account/FormLogin" }, JsonRequestBehavior.AllowGet);
            }
            var checkPermission = userValidate.CheckPermissionUser("Comment", userValidate.UserId, userValidate.IsAdministrator);
            if (!checkPermission || Permission == null)
            {
                return Json(new { redirecturl = Config.UrlRoot + "common/errorpermission" }, JsonRequestBehavior.AllowGet);
            }
            var request = new Comment_Request
            {
                TargetID = targetId,
                Type = type,
                ParentID = parentId
            };
            var list = AbstractDAOFactory.Instance().CreateAccountDAO().Comment_GetList(request);
            return PartialView(list);
        }


        [AuthorizationRequired]
        public ActionResult Comment_GetInfo(int id, int targetId, byte type , int parentId, string title)   // thêm sửa
        {
            var userValidate = new UserValidation();
            var urlLogin = Config.UrlRoot + "Account/FormLogin";
            if (!userValidate.IsSigned())
            {
                return Json(new { redirecturl = Config.UrlRoot + "Account/FormLogin" }, JsonRequestBehavior.AllowGet);
            }
            var checkPermission = userValidate.CheckPermissionUser("Comment", userValidate.UserId, userValidate.IsAdministrator);
            if (!checkPermission || Permission == null)
            {
                return Json(new { redirecturl = Config.UrlRoot + "common/errorpermission" }, JsonRequestBehavior.AllowGet);
            }

            var listUser = AbstractDAOFactory.Instance().CreateAccountDAO().GetListUsers(0, "", 3, 1); // Lấy usertype = 3 (loại tự tạo)
            ViewBag.listUser = listUser;

            var comment = new CommentModel();
            if (id > 0)
            {
                comment = AbstractDAOFactory.Instance().CreateAccountDAO().Comment_GetList(new Comment_Request() { Id = id, TargetID = targetId, Type = type }).SingleOrDefault();
            }
            ViewBag.targetId = targetId;
            ViewBag.type = type;
            ViewBag.parentId = parentId;
            ViewBag.title = title;
            return PartialView(comment);
        }

        [HttpPost]
        public JsonResult Delete_Comment(int id)
        {
            var returnData = new ReturnData();
            var userValidate = new UserValidation();
            if (!userValidate.IsSigned())
            {
                return Json(new { redirecturl = Config.UrlRoot + "Account/FormLogin" }, JsonRequestBehavior.AllowGet);
            }
            var checkPermission = userValidate.CheckPermissionUser("Comment", userValidate.UserId, userValidate.IsAdministrator);
            if (!checkPermission || Permission == null)
            {
                returnData.ResponseCode = -101;
                returnData.Description = "Bạn không có quyền sử dụng chức năng này";
                return Json(returnData);
            }
            try
            {
                if (id <= 0)
                {
                    returnData.ResponseCode = -600;
                    returnData.Description = "tham số đầu vào không hợp lệ. Vui lòng quay lại sau";
                    return Json(returnData);
                }

                var result = AbstractDAOFactory.Instance().CreateAccountDAO().User_Comment_Delete(new Comment_Request() { Id = id, UpdateUser = userValidate.UserName });
                returnData.ResponseCode = result;
                if (returnData.ResponseCode >= 0)
                {
                    returnData.Description = "Xóa bình luận thành công";

                    // Ghi log
                    AbstractDAOFactory.Instance().CreateUsersLogDAO().InsertUsersLog(new UsersLog
                    {
                        FunctionID = Permission.FunctionID,
                        FunctionName = Permission.FunctionName,
                        Description = returnData.Description + " ID:" + id,
                        UserID = userValidate.UserId,
                        ClientIP = userValidate.ClientIP
                    });
                }
                else
                {
                    switch (returnData.ResponseCode)
                    {
                        case -600:
                            returnData.Description = "Vui lòng nhập đầy đủ thông tin";
                            break;
                        default:
                            returnData.Description = "Lỗi từ hệ thống. Vui lòng quay lại sau";
                            break;
                    }
                }
                return Json(returnData);
            }
            catch (Exception ex)
            {
                Log4Net.PublishException(ex);
                returnData.ResponseCode = -969;
                returnData.Description = "Hệ thống đang bận. Vui lòng quay lại sau";
                return Json(returnData);
            }

        }


        // Quản lý thanh toán
        [AuthorizationRequired]
        public ActionResult PaymentOrder()
        {
            var userValidate = new UserValidation();
            var urlLogin = Config.UrlRoot + "Account/FormLogin";
            if (!userValidate.IsSigned())
            {
                return Json(new { redirecturl = Config.UrlRoot + "Account/FormLogin" }, JsonRequestBehavior.AllowGet);
            }
            var checkPermission = userValidate.CheckPermissionUser("PaymentOrder", userValidate.UserId, userValidate.IsAdministrator);
            if (!checkPermission || Permission == null)
            {
                return Json(new { redirecturl = Config.UrlRoot + "common/errorpermission" }, JsonRequestBehavior.AllowGet);
            }
            return PartialView();
        }


        [AuthorizationRequired]
        public ActionResult PaymentOrder_Partial(string fromDate, string toDate, byte status, byte courseType)
        {
            var userValidate = new UserValidation();
            var urlLogin = Config.UrlRoot + "Account/FormLogin";
            if (!userValidate.IsSigned())
            {
                return Json(new { redirecturl = Config.UrlRoot + "Account/FormLogin" }, JsonRequestBehavior.AllowGet);
            }
            var checkPermission = userValidate.CheckPermissionUser("PaymentOrder", userValidate.UserId, userValidate.IsAdministrator);
            if (!checkPermission || Permission == null)
            {
                return Json(new { redirecturl = Config.UrlRoot + "common/errorpermission" }, JsonRequestBehavior.AllowGet);
            }

            var tDate = DateTime.Now;
            var fDate = tDate.AddDays(-1);
            if (fromDate != null && toDate != null)
            {
                fDate = DateTime.ParseExact(fromDate, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                tDate = DateTime.ParseExact(toDate, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            }
            var list = AbstractDAOFactory.Instance().CreateAccountDAO().OrderPayment_Get(0, status, courseType, fDate, tDate);
            var listjson = Regex.Replace(JsonConvert.SerializeObject(list), @"\\r\\n|\\n|\\r|\\t", "");
            ViewBag.listjson = listjson;
            var permission = Regex.Replace(JsonConvert.SerializeObject((UserFunction)Session[SessionsManager.SESSION_PERMISSION]), @"\\r\\n|\\n|\\r|\\t", "");
            ViewBag.permission = permission;
            return PartialView();
        }


        // Chi tiết sản phẩm thanh toán
        [AuthorizationRequired]
        public ActionResult PaymentOrder_Detail(long orderId)
        {
            var userValidate = new UserValidation();
            var urlLogin = Config.UrlRoot + "Account/FormLogin";
            if (!userValidate.IsSigned())
            {
                return Json(new { redirecturl = Config.UrlRoot + "Account/FormLogin" }, JsonRequestBehavior.AllowGet);
            }
            var checkPermission = userValidate.CheckPermissionUser("PaymentOrder", userValidate.UserId, userValidate.IsAdministrator);
            if (!checkPermission || Permission == null)
            {
                return Json(new { redirecturl = Config.UrlRoot + "common/errorpermission" }, JsonRequestBehavior.AllowGet);
            }
            var list = AbstractDAOFactory.Instance().CreateAccountDAO().OrderPayment_GetProduct(orderId);
            var listjson = Regex.Replace(JsonConvert.SerializeObject(list), @"\\r\\n|\\n|\\r|\\t", "");
            ViewBag.listjson = listjson;
            return PartialView(list);
        }

        [HttpPost]
        [AuthorizationRequired]
        public JsonResult PaymentOrder_CheckOrder(long id)
        {
            var orderConfirm = new OrderPayment_ConfirmStatus();
            var userValidate = new UserValidation();
            if (!userValidate.IsSigned())
            {
                return Json(new { redirecturl = Config.UrlRoot + "Account/FormLogin" }, JsonRequestBehavior.AllowGet);
            }
            var checkPermission = userValidate.CheckPermissionUser("PaymentOrder", userValidate.UserId, userValidate.IsAdministrator);
            if (!checkPermission || Permission == null)
            {
                orderConfirm.ResponseCode = -101;
                orderConfirm.Description = "Bạn không có quyền sử dụng chức năng này";
                return Json(orderConfirm);
            }
            try
            {
                if (id <= 0)
                {
                    orderConfirm.ResponseCode = -600;
                    orderConfirm.Description = "tham số đầu vào không hợp lệ. Vui lòng quay lại sau";
                    return Json(orderConfirm);
                }

                var order = AbstractDAOFactory.Instance().CreateAccountDAO().OrderPayment_Get(id, 0, 0, new DateTime(), new DateTime()).SingleOrDefault();

                if (order == null || order.OrderId <= 0)
                {
                    orderConfirm.ResponseCode = -601;
                    orderConfirm.Description = "Không tìm thầy giao dịch";
                    return Json(orderConfirm);
                }

                // check order
                var querydr = ConfigurationManager.AppSettings["querydr"];
                var vnpHashSecret = ConfigurationManager.AppSettings["vnp_HashSecret"];
                var vnp_TmnCode = ConfigurationManager.AppSettings["vnp_TmnCode"];
                var vnpay = new VnPayLibrary();
                var createDate = DateTime.Now;
                vnpay.AddRequestData("vnp_Version", "2.0.0");
                vnpay.AddRequestData("vnp_Command", "querydr");
                vnpay.AddRequestData("vnp_TmnCode", vnp_TmnCode.ToString());
                vnpay.AddRequestData("vnp_Merchant", "VNPAY");
                vnpay.AddRequestData("vnp_TxnRef", order.OrderId.ToString());
                vnpay.AddRequestData("vnp_OrderInfo", order.OrderInfo);
                vnpay.AddRequestData("vnp_TransDate", order.PayDate.ToString(CultureInfo.InvariantCulture));
                vnpay.AddRequestData("vnp_CreateDate", createDate.ToString("yyyyMMddHHmmss"));
                vnpay.AddRequestData("vnp_IpAddr", Config.GetIP());
                var paymentUrl = vnpay.CreateRequestUrl(querydr, vnpHashSecret);
                var strDatax = "";
                var request = (HttpWebRequest)WebRequest.Create(paymentUrl);
                request.AutomaticDecompression = DecompressionMethods.GZip;
                using (var response = (HttpWebResponse)request.GetResponse())
                using (var stream = response.GetResponseStream()) if (stream != null) using (var reader = new StreamReader(stream)) { strDatax = reader.ReadToEnd(); }

                var query = HttpUtility.ParseQueryString(strDatax);
                var responseCode = query.Get("vnp_ResponseCode");
                orderConfirm.ResponseCode = Convert.ToInt32(responseCode);
                if (Convert.ToInt32(responseCode) == 0)
                {
                    var amount = query.Get("vnp_Amount");
                    var bankCode = query.Get("vnp_BankCode");
                    var message = query.Get("vnp_Message");
                    var orderInfo = query.Get("vnp_OrderInfo");
                    var payDate = query.Get("vnp_PayDate");
                    var tmnCode = query.Get("vnp_TmnCode");
                    var transactionNo = query.Get("vnp_TransactionNo");
                    var transactionStatus = query.Get("vnp_TransactionStatus");
                    var transactionType = query.Get("vnp_TransactionType");
                    var bankTransNo = query.Get("vnp_BankTranNo");
                    var cardType = query.Get("vnp_CardType");
                    var orderId = query.Get("vnp_TxnRef");

                    orderConfirm.Amount = Convert.ToInt64(amount);
                    orderConfirm.BankCode = bankCode;
                    orderConfirm.TransRefID = transactionNo;
                    orderConfirm.TmnCode = tmnCode;
                    orderConfirm.PayDate = Convert.ToDecimal(payDate);
                    orderConfirm.BankTransNo = bankTransNo;
                    orderConfirm.CardType = cardType;
                    orderConfirm.OrderID = Convert.ToInt64(orderId);
                    orderConfirm.TransactionStatus = Convert.ToInt32(transactionStatus);
                }
                return Json(orderConfirm);
            }
            catch (Exception ex)
            {
                Log4Net.PublishException(ex);
                orderConfirm.ResponseCode = -969;
                orderConfirm.Description = "Hệ thống đang bận. Vui lòng quay lại sau";
                return Json(orderConfirm);
            }
        }


        [HttpPost]
        [AuthorizationRequired]
        public JsonResult PaymentOrder_UpdateStatus(OrderPayment_ConfirmStatus request)
        {

            var returnData = new ReturnData();
            var userValidate = new UserValidation();
            if (!userValidate.IsSigned())
            {
                return Json(new { redirecturl = Config.UrlRoot + "Account/FormLogin" }, JsonRequestBehavior.AllowGet);
            }
            var checkPermission = userValidate.CheckPermissionUser("PaymentOrder", userValidate.UserId, userValidate.IsAdministrator);
            if (!checkPermission || Permission == null)
            {
                returnData.ResponseCode = -101;
                returnData.Description = "Bạn không có quyền sử dụng chức năng này";
                return Json(returnData);
            }
            try
            {
                if (request.OrderID <= 0)
                {
                    returnData.ResponseCode = -600;
                    returnData.Description = "Tham số đầu vào không hợp lệ. Vui lòng thử lại sau !";
                    return Json(returnData);
                }

                if (request.Status == 1)// thanh cong
                {
                    if (string.IsNullOrEmpty(request.TransRefID))
                    {
                        returnData.ResponseCode = -601;
                        returnData.Description = "Vui lòng nhập Mã giao dịch bên ngân hàng. !";
                        return Json(returnData);
                    }
                }

                var result = AbstractDAOFactory.Instance().CreateAccountDAO().SP_OrderPayment_ConfirmStatus(request);
                returnData.ResponseCode = result;
                if (returnData.ResponseCode >= 0)
                {
                    returnData.Description = "Cập nhật trạng thái giao dịch thành công";

                    // Ghi log
                    AbstractDAOFactory.Instance().CreateUsersLogDAO().InsertUsersLog(new UsersLog
                    {
                        FunctionID = Permission.FunctionID,
                        FunctionName = Permission.FunctionName,
                        Description = returnData.Description + " orderId :" + request.OrderID,
                        UserID = userValidate.UserId,
                        ClientIP = userValidate.ClientIP
                    });
                }
                else
                {
                    switch (returnData.ResponseCode)
                    {
                        case -600:
                            returnData.Description = "Vui lòng nhập đầy đủ thông tin";
                            break;
                        case -1303:
                            returnData.Description = "Thông tin thanh toán không tôn tại";
                            break;
                        case -1304:
                            returnData.Description = "Trạng thái thanh toán không hợp lệ";
                            break;
                        case -1305:
                            returnData.Description = "Trạng thái thanh toán đã thành công. Vui lòng thử lại sau";
                            break;
                        default:
                            returnData.Description = "Lỗi từ hệ thống. Vui lòng quay lại sau";
                            break;
                    }
                }

                return Json(returnData);
            }
            catch (Exception ex)
            {
                Log4Net.PublishException(ex);
                returnData.ResponseCode = -969;
                returnData.Description = "Hệ thống đang bận. Vui lòng quay lại sau";
                return Json(returnData);
            }

        }



        public ActionResult Querydr(long orderId)
        {



            var querydr = ConfigurationManager.AppSettings["querydr"];
            var vnpHashSecret = ConfigurationManager.AppSettings["vnp_HashSecret"];
            var vnpay = new VnPayLibrary();
            var createDate = DateTime.Now;
            vnpay.AddRequestData("vnp_Version", "2.0.0");
            vnpay.AddRequestData("vnp_Command", "querydr");
            vnpay.AddRequestData("vnp_TmnCode", "PTGD0001");
            vnpay.AddRequestData("vnp_Merchant", "VNPAY");
            vnpay.AddRequestData("vnp_TxnRef", orderId.ToString());
            vnpay.AddRequestData("vnp_OrderInfo", "Nop phi giu cho. Khoa hoc Google Adwords");
            //vnpay.AddRequestData("vnp_TransactionNo", "13870295");
            vnpay.AddRequestData("vnp_TransDate", "20180731104120");
            vnpay.AddRequestData("vnp_CreateDate", createDate.ToString("yyyyMMddHHmmss"));
            vnpay.AddRequestData("vnp_IpAddr", Config.GetIP());
            var paymentUrl = vnpay.CreateRequestUrl(querydr, vnpHashSecret);
            var strDatax = "";
            var request = (HttpWebRequest)WebRequest.Create(paymentUrl);
            request.AutomaticDecompression = DecompressionMethods.GZip;
            using (var response = (HttpWebResponse)request.GetResponse())
            using (var stream = response.GetResponseStream()) if (stream != null) using (var reader = new StreamReader(stream)) { strDatax = reader.ReadToEnd(); }
            //var data = strDatax.Split('&');
            //var str = data.Length;
            //if (str==12)
            //{
            //order.Amount = Convert.ToDecimal(data[0].Substring(data[0].IndexOf("vnp_Amount=", StringComparison.Ordinal) + 11));
            //order.vpc_BankCode = data[1].Substring(data[1].IndexOf("vnp_BankCode=", StringComparison.Ordinal) + 13);
            //order.vpc_Message = HttpUtility.UrlDecode(data[2].Substring(data[2].IndexOf("vnp_Message=", StringComparison.Ordinal) + 12));
            //order.OrderDescription = HttpUtility.UrlDecode(data[3].Substring(data[3].IndexOf("vnp_OrderInfo=", StringComparison.Ordinal) + 14));
            //order.vpc_TxnResponseCode = data[5].Substring(data[5].IndexOf("vnp_ResponseCode=", StringComparison.Ordinal) + 17);
            //order.OrderId = Convert.ToInt32(data[10].Substring(data[10].IndexOf("vnp_TxnRef=", StringComparison.Ordinal) + 11));
            //return View(order);
            //}
            //else
            //{
            ViewBag.strDatax = strDatax;
            return View();
        }

    }
}