using CMS.Topcourse.Controllers.Common;
using CMS.Topcourse.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Topcourse.DataAccess.DTO;
using Topcourse.DataAccess.Factory;
using Topcourse.Utility;

namespace CMS.Topcourse.Controllers
{
    public class PostController : Controller
    {
        private UserFunction Permission { get { return ((UserFunction)Session[SessionsManager.SESSION_PERMISSION]); } }
        public ActionResult Index()
       {
            return View();
       }
        [AuthorizationRequired]
        public ActionResult Post_Short()
        {

            var userValidate = new UserValidation();
            var urlLogin = Config.UrlRoot + "Account/FormLogin";
            if (!userValidate.IsSigned())
            {
                return Json(new { redirecturl = Config.UrlRoot + "Account/FormLogin" }, JsonRequestBehavior.AllowGet);
            }
            var checkPermission = userValidate.CheckPermissionUser("Post_Short", userValidate.UserId, userValidate.IsAdministrator);
            if (!checkPermission || Permission == null)
            {
                return Json(new { redirecturl = Config.UrlRoot + "common/errorpermission" }, JsonRequestBehavior.AllowGet);
            }

            return PartialView();
        }

        [AuthorizationRequired]
        public ActionResult Post_Short_Partial(byte status)
        {
            var userValidate = new UserValidation();
            var urlLogin = Config.UrlRoot + "Account/FormLogin";
            if (!userValidate.IsSigned())
            {
                return Json(new { redirecturl = Config.UrlRoot + "Account/FormLogin" }, JsonRequestBehavior.AllowGet);
            }
            var checkPermission = userValidate.CheckPermissionUser("Post_Short", userValidate.UserId, userValidate.IsAdministrator);
            if (!checkPermission || Permission == null)
            {
                return Json(new { redirecturl = Config.UrlRoot + "common/errorpermission" }, JsonRequestBehavior.AllowGet);
            }
            var list = new List<Post>();
            //var tDate = DateTime.Now;
            //var fDate = tDate.AddDays(-1);
            //if (fromDate != null && toDate != null)
            //{
            //    fDate = DateTime.ParseExact(fromDate, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            //    tDate = DateTime.ParseExact(toDate, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            //}
            if (userValidate.IsAdministrator || Permission.IsGrant)
            {
                list = AbstractDAOFactory.Instance().CreatePostDAO().GetAllPost(new PostRequest() {                
                    Status = status
                });
            }          
            var listjson = Regex.Replace(JsonConvert.SerializeObject(list), @"\\r\\n|\\n|\\r|\\t", "");
            ViewBag.listjson = listjson;
            var permission = Regex.Replace(JsonConvert.SerializeObject((UserFunction)Session[SessionsManager.SESSION_PERMISSION]), @"\\r\\n|\\n|\\r|\\t", "");
            ViewBag.permission = permission;
            return PartialView();
        }

        [AuthorizationRequired]
        public ActionResult Post_Short_GetInfo(int id)
        {
            var userValidate = new UserValidation();
            var urlLogin = Config.UrlRoot + "Account/FormLogin";
            if (!userValidate.IsSigned())
            {
                return Json(new { redirecturl = Config.UrlRoot + "Account/FormLogin" }, JsonRequestBehavior.AllowGet);
            }
            var checkPermission = userValidate.CheckPermissionUser("Post_Short", userValidate.UserId, userValidate.IsAdministrator);
            if (!checkPermission || Permission == null)
            {
                return Json(new { redirecturl = Config.UrlRoot + "common/errorpermission" }, JsonRequestBehavior.AllowGet);
            }
            if (id <= 0)
            {
                return PartialView(new Post());
            }
            var detail = AbstractDAOFactory.Instance().CreatePostDAO().GetInfo_Post(id);
            return PartialView(detail);
        }

        [HttpPost]
        [AuthorizationRequired]
        public JsonResult UpdateStatus_Post(int postId, int status)
        {
            var returnData = new ReturnData();
            try
            {
                var userValidate = new UserValidation();
                if (!userValidate.IsSigned())
                {
                    return Json(new { redirecturl = Config.UrlRoot + "Account/FormLogin" }, JsonRequestBehavior.AllowGet);
                }
                if (Permission == null || !userValidate.IsAdministrator)
                {
                    returnData.ResponseCode = -101;
                    returnData.Description = "Bạn không có quyền sử dụng chức năng này";
                    return Json(returnData);
                }
                if (postId <= 0)
                {
                    returnData.ResponseCode = -600;
                    returnData.Description = "Tham số đầu vào không hợp lệ";
                    return Json(returnData);
                }

                var result = AbstractDAOFactory.Instance().CreatePostDAO().UpdateStatus_Post(postId, status, userValidate.UserName);
                returnData.ResponseCode = result;
                if (result >= 0)
                {
                    returnData.Description = "Cập nhập trạng thái bài viết thành công";

                    // Ghi log
                    AbstractDAOFactory.Instance().CreateUsersLogDAO().InsertUsersLog(new UsersLog
                    {
                        FunctionID = Permission.FunctionID,
                        FunctionName = Permission.FunctionName,
                        Description = returnData.Description + " Bài viết: PostId: " + postId + "|Status:" + status,
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

        [HttpPost]
        [ValidateInput(false)]
        [AuthorizationRequired]
        public JsonResult Insert_UpdatePost(Post request)
        {
            var returnData = new ReturnData();
            var userValidate = new UserValidation();
            if (!userValidate.IsSigned())
            {
                return Json(new { redirecturl = Config.UrlRoot + "Account/FormLogin" }, JsonRequestBehavior.AllowGet);
            }
            if (Permission == null)
            {
                returnData.ResponseCode = -101;
                returnData.Description = "Bạn không có quyền sử dụng chức năng này";
                return Json(returnData);
            }
            try
            {               
                if (request.PostID <= 0) // insert
                {
                    // Ảnh đại diện
                    request.ThumbailImage = request.ThumbailImage.Replace(ConfigurationManager.AppSettings["New_Root_URL"] + "images/", "");
                    request.CreatedUser = userValidate.UserName;                
                    var result = AbstractDAOFactory.Instance().CreatePostDAO().Insert_Post(request);
                    Log4Net.LogInfo($"Insert_UpdatePost responseData:  {result}");
                    returnData.ResponseCode = result;
                    if (result >= 0)
                    {
                        returnData.Description = "Tạo bài viết thành công";

                        // Ghi log
                        AbstractDAOFactory.Instance().CreateUsersLogDAO().InsertUsersLog(new UsersLog
                        {
                            FunctionID = Permission.FunctionID,
                            FunctionName = Permission.FunctionName,
                            Description = returnData.Description + " Bài viết:" + request.Title,
                            UserID = userValidate.UserId,
                            ClientIP = userValidate.ClientIP
                        });
                    }
                    else
                    {
                        switch (result)
                        {
                            case -901:
                                returnData.Description = "Bài viết không tồn tại.";
                                break;
                            case -804:
                                returnData.Description = "Bài viết này đã tồn tại";
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
                    // Ảnh đại diện
                    request.ThumbailImage = request.ThumbailImage.Replace(ConfigurationManager.AppSettings["New_Root_URL"] + "images/", "");
                    request.UpdateBy = userValidate.UserName;
                    var result = AbstractDAOFactory.Instance().CreatePostDAO().Update_Post(request);
                    Log4Net.LogInfo($"Insert_UpdatePost responseData:  {result} ");
                    returnData.ResponseCode = result;
                    if (result >= 0)
                    {
                        returnData.Description = "Cập nhật bài viết thành công";

                        // Ghi log
                        AbstractDAOFactory.Instance().CreateUsersLogDAO().InsertUsersLog(new UsersLog
                        {
                            FunctionID = Permission.FunctionID,
                            FunctionName = Permission.FunctionName,
                            Description = returnData.Description + " Bài viết:" + request.Title,
                            UserID = userValidate.UserId,
                            ClientIP = userValidate.ClientIP
                        });
                    }
                    else
                    {
                        switch (result)
                        {
                            case -901:
                                returnData.Description = "Bài viết không tồn tại.";
                                break;
                            case -804:
                                returnData.Description = "Bài viết này đã tồn tại";
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
        public JsonResult Delete_Post(int id)
        {
            var returnData = new ReturnData();
            try
            {
                var userValidate = new UserValidation();
                if (!userValidate.IsSigned())
                {
                    return Json(new { redirecturl = Config.UrlRoot + "Account/FormLogin" }, JsonRequestBehavior.AllowGet);
                }
                if (Permission == null || !userValidate.IsAdministrator)
                {
                    returnData.ResponseCode = -101;
                    returnData.Description = "Bạn không có quyền sử dụng chức năng này";
                    return Json(returnData);
                }

                if (id <= 0)
                {
                    returnData.ResponseCode = -600;
                    returnData.Description = "Tham số đầu vào không hợp lệ";
                    return Json(returnData);
                }
                var result = AbstractDAOFactory.Instance().CreatePostDAO().Delete_Post(id, userValidate.UserName);
                returnData.ResponseCode = result;
                if (result >= 0)
                {
                    returnData.Description = "Xóa thành công";

                    // Ghi log
                    AbstractDAOFactory.Instance().CreateUsersLogDAO().InsertUsersLog(new UsersLog
                    {
                        FunctionID = Permission.FunctionID,
                        FunctionName = Permission.FunctionName,
                        Description = returnData.Description + " Bài viết: Id: " + id,
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