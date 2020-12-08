using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using CMS.Topcourse.Controllers.Common;
using CMS.Topcourse.Models;
using Newtonsoft.Json;
using Topcourse.DataAccess.DTO;
using Topcourse.DataAccess.Factory;
using Topcourse.Utility;

namespace CMS.Topcourse.Controllers
{
    public class EducationController : Controller
    {

        private UserFunction Permission { get { return ((UserFunction)Session[SessionsManager.SESSION_PERMISSION]); } }

        [AuthorizationRequired]
        public ActionResult Education()
        {

            var userValidate = new UserValidation();
            var urlLogin = Config.UrlRoot + "Account/FormLogin";
            if (!userValidate.IsSigned())
            {
                return Json(new { redirecturl = Config.UrlRoot + "Account/FormLogin" }, JsonRequestBehavior.AllowGet);
            }
            var checkPermission = userValidate.CheckPermissionUser("Education", userValidate.UserId, userValidate.IsAdministrator);
            if (!checkPermission || Permission == null)
            {
                return Json(new { redirecturl = Config.UrlRoot + "common/errorpermission" }, JsonRequestBehavior.AllowGet);
            }
            var listUser = AbstractDAOFactory.Instance().CreateAccountDAO().GetListUsers(0, "", 2, 1);
            ViewBag.listUser = listUser;
            return PartialView();
        }

        [AuthorizationRequired]
        public ActionResult Education_Partial(Request_Education request)
        {
            var userValidate = new UserValidation();
            var urlLogin = Config.UrlRoot + "Account/FormLogin";
            if (!userValidate.IsSigned())
            {
                return Json(new { redirecturl = Config.UrlRoot + "Account/FormLogin" }, JsonRequestBehavior.AllowGet);
            }
            var checkPermission = userValidate.CheckPermissionUser("Education", userValidate.UserId, userValidate.IsAdministrator);
            if (!checkPermission || Permission == null)
            {
                return Json(new { redirecturl = Config.UrlRoot + "common/errorpermission" }, JsonRequestBehavior.AllowGet);
            }
            var list = AbstractDAOFactory.Instance().CreateAccountDAO().Education_GetList(request);
            var listjson = Regex.Replace(JsonConvert.SerializeObject(list), @"\\r\\n|\\n|\\r|\\t", "");
            ViewBag.listjson = listjson;
            var permission = Regex.Replace(JsonConvert.SerializeObject((UserFunction)Session[SessionsManager.SESSION_PERMISSION]), @"\\r\\n|\\n|\\r|\\t", "");
            ViewBag.permission = permission;
            return PartialView();
        }

        [AuthorizationRequired]
        public ActionResult Education_GetInfo(int id)
        {
            var userValidate = new UserValidation();
            var urlLogin = Config.UrlRoot + "Account/FormLogin";
            if (!userValidate.IsSigned())
            {
                return Json(new { redirecturl = Config.UrlRoot + "Account/FormLogin" }, JsonRequestBehavior.AllowGet);
            }
            var checkPermission = userValidate.CheckPermissionUser("Education", userValidate.UserId, userValidate.IsAdministrator);
            if (!checkPermission || Permission == null)
            {
                return Json(new { redirecturl = Config.UrlRoot + "common/errorpermission" }, JsonRequestBehavior.AllowGet);
            }
            var listUser = AbstractDAOFactory.Instance().CreateAccountDAO().GetListUsers(0, "", 2, 1);
            ViewBag.listUser = listUser;
            if (id <= 0)
            {
                return PartialView(new Education());
            }
            var education = AbstractDAOFactory.Instance().CreateAccountDAO().Education_Detail(id, "");
            return PartialView(education);
        }



        [HttpPost]
        [ValidateInput(false)]
        [AuthorizationRequired]
        public JsonResult Insert_UpdateEducation(Education request)
        {
            var returnData = new ReturnData();
            var userValidate = new UserValidation();
            if (!userValidate.IsSigned())
            {
                return Json(new { redirecturl = Config.UrlRoot + "Account/FormLogin" }, JsonRequestBehavior.AllowGet);
            }
            var checkPermission = userValidate.CheckPermissionUser("Education", userValidate.UserId, userValidate.IsAdministrator);
            if (!checkPermission || Permission == null)
            {
                returnData.ResponseCode = -101;
                returnData.Description = "Bạn không có quyền sử dụng chức năng này";
                return Json(returnData);
            }
            try
            {
                Log4Net.LogInfo($"Insert_UpdateEducation : {request.EduId}|{request.EduName}");
                if (request.EduId <= 0) // insert
                {
                    request.Avatar = request.Avatar.Replace(ConfigurationManager.AppSettings["New_Root_URL"] + "images/", "");
                    request.Logo = request.Logo.Replace(ConfigurationManager.AppSettings["New_Root_URL"] + "images/", "");

                    request.CreatedUser = userValidate.UserName;
                    var result = AbstractDAOFactory.Instance().CreateAccountDAO().SP_Education_Insert(request);
                    Log4Net.LogInfo($"Insert_UpdateEducation Insert response: {result}");
                    returnData.ResponseCode = result;
                    if (result >= 0)
                    {
                        returnData.Description = "Tạo thành công";

                        // Ghi log
                        AbstractDAOFactory.Instance().CreateUsersLogDAO().InsertUsersLog(new UsersLog
                        {
                            FunctionID = Permission.FunctionID,
                            FunctionName = Permission.FunctionName,
                            Description = returnData.Description + "Trường :" + request.EduName + "",
                            UserID = userValidate.UserId,
                            ClientIP = userValidate.ClientIP
                        });
                    }
                    else
                    {
                        switch (result)
                        {
                            case -902:
                                returnData.Description = "Tên trường đã tồn tại trên hệ thống. Vui lòng chọn tên khác";
                                break;
                            case -600:
                                returnData.Description = "Vui lòng nhập đầy đủ thông tin";
                                break;
                            default:
                                returnData.Description = "Lỗi từ hệ thống. Vui lòng quay lại sau";
                                break;
                        }
                    }
                }
                else // Update
                {
                    request.Avatar = request.Avatar.Replace(ConfigurationManager.AppSettings["New_Root_URL"] + "images/", "");
                    request.Logo = request.Logo.Replace(ConfigurationManager.AppSettings["New_Root_URL"] + "images/", "");

                    request.CreatedUser = userValidate.UserName;
                    var result = AbstractDAOFactory.Instance().CreateAccountDAO().SP_Education_Update(request);
                    Log4Net.LogInfo($"Insert_UpdateEducation Update response: {result}");
                    returnData.ResponseCode = result;
                    if (result >= 0)
                    {
                        returnData.Description = "Cập nhật thành công";

                        // Ghi log
                        AbstractDAOFactory.Instance().CreateUsersLogDAO().InsertUsersLog(new UsersLog
                        {
                            FunctionID = Permission.FunctionID,
                            FunctionName = Permission.FunctionName,
                            Description = returnData.Description + "Trường :" + request.EduName + "",
                            UserID = userValidate.UserId,
                            ClientIP = userValidate.ClientIP
                        });
                    }
                    else
                    {
                        switch (result)
                        {
                            case -600:
                                returnData.Description = "Vui lòng nhập đầy đủ thông tin";
                                break;
                            default:
                                returnData.Description = "Lỗi từ hệ thống. Vui lòng quay lại sau";
                                break;
                        }
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
        [ValidateInput(false)]
        [AuthorizationRequired]
        public JsonResult Upload_Insert_UpdateEducation()
        {
            var returnData = new ReturnData();
            var userValidate = new UserValidation();
            if (!userValidate.IsSigned())
            {
                return Json(new { redirecturl = Config.UrlRoot + "Account/FormLogin" }, JsonRequestBehavior.AllowGet);
            }
            var checkPermission = userValidate.CheckPermissionUser("Education", userValidate.UserId, userValidate.IsAdministrator);
            if (!checkPermission || Permission == null)
            {
                returnData.ResponseCode = -101;
                returnData.Description = "Bạn không có quyền sử dụng chức năng này";
                return Json(returnData);
            }

            var headers = Request.Headers;
         
            try
            {
                var eduId = Convert.ToInt32(Request.Form["EduId"]);
                var eduName = Request.Form["EduName"];

                var mediaPath = ConfigurationManager.AppSettings["UploadFileSlide"] + "/" + eduName + "/";

                if (!Directory.Exists(mediaPath))
                {
                    Directory.CreateDirectory(mediaPath);
                }

                for (int i = 0; i < Request.Files.Count; i++)
                {
                    var file = Request.Files[i];
              
                    if (file != null && file.ContentLength > 0)
                    {
                        var fileS = Path.GetFileName(file.FileName);
                        string ext = Path.GetExtension(file.FileName);
                        var fileName = Path.GetFileName(file.FileName);


                        var path = Path.Combine(mediaPath, fileName);
                        file.SaveAs(path);
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
        public JsonResult Delete_Education(int eduId)
        {
            var returnData = new ReturnData();
            try
            {
                var userValidate = new UserValidation();
                if (!userValidate.IsSigned())
                {
                    return Json(new { redirecturl = Config.UrlRoot + "Account/FormLogin" }, JsonRequestBehavior.AllowGet);
                }
                var checkPermission = userValidate.CheckPermissionUser("Education", userValidate.UserId, userValidate.IsAdministrator);
                if (!checkPermission || Permission == null)
                {
                    returnData.ResponseCode = -101;
                    returnData.Description = "Bạn không có quyền sử dụng chức năng này";
                    return Json(returnData);
                }

                if (eduId <= 0)
                {
                    returnData.ResponseCode = -600;
                    returnData.Description = "Tham số đầu vào không hợp lệ";
                    return Json(returnData);
                }

                var requestData = new Request_Education
                {
                    EduId = eduId,
                    CreatedUser = userValidate.UserName
                };

                var result = AbstractDAOFactory.Instance().CreateAccountDAO().SP_Education_Delete(requestData);
                returnData.ResponseCode = result;
                if (result >= 0)
                {
                    returnData.Description = "Xóa thành công";

                    // Ghi log
                    AbstractDAOFactory.Instance().CreateUsersLogDAO().InsertUsersLog(new UsersLog
                    {
                        FunctionID = Permission.FunctionID,
                        FunctionName = Permission.FunctionName,
                        Description = returnData.Description + " Trường: Id: " + eduId + "|",
                        UserID = userValidate.UserId,
                        ClientIP = userValidate.ClientIP
                    });
                }
                else
                {
                    switch (result)
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
                returnData.ResponseCode = -99;
                returnData.Description = "Hệ thống đang bận. Vui lòng quay lại sau";
                return Json(returnData);
            }
        }

    }
}