using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
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
using Topcourse.Utility.Security;
using Account = Topcourse.DataAccess.DTO.Account;

namespace CMS.Topcourse.Controllers
{
    public class CourseController : Controller
    {
        private UserFunction Permission { get { return ((UserFunction)Session[SessionsManager.SESSION_PERMISSION]); } }
        // GET: Course
        public ActionResult Index()
        {
            return View();
        }

        // Khoa ngan han
        [AuthorizationRequired]
        public ActionResult Course_Short()
        {

            var userValidate = new UserValidation();
            var urlLogin = Config.UrlRoot + "Account/FormLogin";
            if (!userValidate.IsSigned())
            {
                return Json(new { redirecturl = Config.UrlRoot + "Account/FormLogin" }, JsonRequestBehavior.AllowGet);
            }
            var checkPermission = userValidate.CheckPermissionUser("Course_Short", userValidate.UserId, userValidate.IsAdministrator);
            if (!checkPermission || Permission == null)
            {
                return Json(new { redirecturl = Config.UrlRoot + "common/errorpermission" }, JsonRequestBehavior.AllowGet);
            }

            return PartialView();
        }

        [AuthorizationRequired]
        public ActionResult Course_Short_Partial(CourseRequest request)
        {
            var userValidate = new UserValidation();
            var urlLogin = Config.UrlRoot + "Account/FormLogin";
            if (!userValidate.IsSigned())
            {
                return Json(new { redirecturl = Config.UrlRoot + "Account/FormLogin" }, JsonRequestBehavior.AllowGet);
            }
            var checkPermission = userValidate.CheckPermissionUser("Course_Short", userValidate.UserId, userValidate.IsAdministrator);
            if (!checkPermission || Permission == null)
            {
                return Json(new { redirecturl = Config.UrlRoot + "common/errorpermission" }, JsonRequestBehavior.AllowGet);
            }
            request.CourseType = 1;
            var list = new List<Course>();
            if (userValidate.IsAdministrator || Permission.IsGrant)
            {
                list = AbstractDAOFactory.Instance().CreateCourseDAO().GetListCourse(request);
            }
            else
            {
                list = AbstractDAOFactory.Instance().CreateCourseDAO().GetListCourse(request).Where(p => p.CreatedUser == userValidate.UserName).ToList();
            }
            var listjson = Regex.Replace(JsonConvert.SerializeObject(list), @"\\r\\n|\\n|\\r|\\t", "");
            ViewBag.listjson = listjson;
            var permission = Regex.Replace(JsonConvert.SerializeObject((UserFunction)Session[SessionsManager.SESSION_PERMISSION]), @"\\r\\n|\\n|\\r|\\t", "");
            ViewBag.permission = permission;
            return PartialView();
        }

        [AuthorizationRequired]
        public ActionResult Course_Short_GetInfo(int id)
        {
            var userValidate = new UserValidation();
            var urlLogin = Config.UrlRoot + "Account/FormLogin";
            if (!userValidate.IsSigned())
            {
                return Json(new { redirecturl = Config.UrlRoot + "Account/FormLogin" }, JsonRequestBehavior.AllowGet);
            }
            var checkPermission = userValidate.CheckPermissionUser("Course_Short", userValidate.UserId, userValidate.IsAdministrator);
            if (!checkPermission || Permission == null)
            {
                return Json(new { redirecturl = Config.UrlRoot + "common/errorpermission" }, JsonRequestBehavior.AllowGet);
            }
            var listSpecility = AbstractDAOFactory.Instance().CreateCourseDAO().GetList_Specility(0, 1);
            ViewBag.listSpecility = listSpecility;
            var listAttributes = AbstractDAOFactory.Instance().CreateCourseDAO().GetList_Attributes_Dict("course", "", 0);
            ViewBag.listAttributes = listAttributes;
            if (id <= 0)
            {
                return PartialView(new Course()
                {
                    ExpireDatePromotion = new DateTime()
                });
            }
            var course = AbstractDAOFactory.Instance().CreateCourseDAO().GetInfo_Course(id);

            if (userValidate.IsAdministrator || Permission.IsGrant)
            {

                return PartialView(course);
            }
            else
            {
                if (course.CreatedUser == userValidate.UserName)
                {
                    return PartialView(course);
                }
                else
                    return PartialView(new Course());
            }
        }


        // khóa dài hạn
        [AuthorizationRequired]
        public ActionResult Course_Long()
        {
            var userValidate = new UserValidation();
            var urlLogin = Config.UrlRoot + "Account/FormLogin";
            if (!userValidate.IsSigned())
            {
                return Json(new { redirecturl = Config.UrlRoot + "Account/FormLogin" }, JsonRequestBehavior.AllowGet);
            }
            var checkPermission = userValidate.CheckPermissionUser("Course_Long", userValidate.UserId, userValidate.IsAdministrator);
            if (!checkPermission || Permission == null)
            {
                return Json(new { redirecturl = Config.UrlRoot + "common/errorpermission" }, JsonRequestBehavior.AllowGet);
            }

            return PartialView();
        }

        [AuthorizationRequired]
        public ActionResult Course_Long_Partial(CourseRequest request)
        {
            var userValidate = new UserValidation();
            var urlLogin = Config.UrlRoot + "Account/FormLogin";
            if (!userValidate.IsSigned())
            {
                return Json(new { redirecturl = Config.UrlRoot + "Account/FormLogin" }, JsonRequestBehavior.AllowGet);
            }
            var checkPermission = userValidate.CheckPermissionUser("Course_Long", userValidate.UserId, userValidate.IsAdministrator);
            if (!checkPermission || Permission == null)
            {
                return Json(new { redirecturl = Config.UrlRoot + "common/errorpermission" }, JsonRequestBehavior.AllowGet);
            }
            request.CourseType = 2;

            var list = new List<Course>();
            if (userValidate.IsAdministrator || Permission.IsGrant)
            {
                list = AbstractDAOFactory.Instance().CreateCourseDAO().GetListCourse(request);
            }
            else
            {
                list = AbstractDAOFactory.Instance().CreateCourseDAO().GetListCourse(request).Where(p => p.CreatedUser == userValidate.UserName).ToList();
            }
            var listjson = Regex.Replace(JsonConvert.SerializeObject(list), @"\\r\\n|\\n|\\r|\\t", "");
            ViewBag.listjson = listjson;
            var permission = Regex.Replace(JsonConvert.SerializeObject((UserFunction)Session[SessionsManager.SESSION_PERMISSION]), @"\\r\\n|\\n|\\r|\\t", "");
            ViewBag.permission = permission;
            return PartialView();
        }

        [AuthorizationRequired]
        public ActionResult Course_Long_GetInfo(int id)
        {
            var userValidate = new UserValidation();
            var urlLogin = Config.UrlRoot + "Account/FormLogin";
            if (!userValidate.IsSigned())
            {
                return Json(new { redirecturl = Config.UrlRoot + "Account/FormLogin" }, JsonRequestBehavior.AllowGet);
            }
            var checkPermission = userValidate.CheckPermissionUser("Course_Long", userValidate.UserId, userValidate.IsAdministrator);
            if (!checkPermission || Permission == null)
            {
                return Json(new { redirecturl = Config.UrlRoot + "common/errorpermission" }, JsonRequestBehavior.AllowGet);
            }
            var listSpecility = AbstractDAOFactory.Instance().CreateCourseDAO().GetList_Specility(0, 2); // danh mục
            ViewBag.listSpecility = listSpecility;
            var listEducation = AbstractDAOFactory.Instance().CreateAccountDAO().Education_GetList(new Request_Education());
            ViewBag.listEducation = listEducation;
            var listAttributes = AbstractDAOFactory.Instance().CreateCourseDAO().GetList_Attributes_Dict("course", "", 0);
            ViewBag.listAttributes = listAttributes;
            if (id <= 0)
            {
                return PartialView(new Course()
                {
                    ExpireDatePromotion = new DateTime()
                });
            }
            var course = AbstractDAOFactory.Instance().CreateCourseDAO().GetInfo_Course(id);
            if (userValidate.IsAdministrator || Permission.IsGrant)
            {

                return PartialView(course);
            }
            else
            {
                if (course.CreatedUser == userValidate.UserName)
                {
                    return PartialView(course);
                }
                else
                    return PartialView(new Course());
            }
        }

        // tao short course
        [HttpPost]
        [ValidateInput(false)]
        [AuthorizationRequired]
        public JsonResult Insert_UpdateCourse(Course request)
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
                Log4Net.LogInfo($"Insert_UpdateCourse requestData:  {request.CoursesID}|{request.Title}|{request.TitleDisplay}|{request.EducationID}");
                var ExpireDatePromotion = new DateTime();
                if (!string.IsNullOrEmpty(request.ExpireDatePromotion_String))
                {
                    ExpireDatePromotion = DateTime.ParseExact(request.ExpireDatePromotion_String.Trim(), "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
                }

                request.ExpireDatePromotion = ExpireDatePromotion;
                if (request.CoursesID <= 0) // insert
                {
                    // Ảnh đại diện
                    request.ImageUrl = request.ImageUrl.Replace(ConfigurationManager.AppSettings["New_Root_URL"] + "images/", "");
                    request.CreatedUser = userValidate.UserName;
                    request.PublishDate = DateTime.Now.AddMinutes(5);
                    var result = AbstractDAOFactory.Instance().CreateCourseDAO().Insert_Course(request);
                    Log4Net.LogInfo($"Insert_UpdateCourse responseData:  {result}");
                    returnData.ResponseCode = result;
                    if (result >= 0)
                    {
                        returnData.Description = "Tạo khóa học thành công";

                        // Ghi log
                        AbstractDAOFactory.Instance().CreateUsersLogDAO().InsertUsersLog(new UsersLog
                        {
                            FunctionID = Permission.FunctionID,
                            FunctionName = Permission.FunctionName,
                            Description = returnData.Description + " Khóa học:" + request.Title + "|CourseType: " + request.CourseType,
                            UserID = userValidate.UserId,
                            ClientIP = userValidate.ClientIP
                        });
                    }
                    else
                    {
                        switch (result)
                        {
                            case -804:
                                returnData.Description = "Ngành học không tồn tại.";
                                break;
                            case -802:
                                returnData.Description = "Khóa học này đã tồn tại";
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
                    request.ImageUrl = request.ImageUrl.Replace(ConfigurationManager.AppSettings["New_Root_URL"] + "images/", "");
                    request.UpdateUser = userValidate.UserName;
                    request.PublishDate = DateTime.Now;
                    var result = AbstractDAOFactory.Instance().CreateCourseDAO().Update_Course(request);
                    Log4Net.LogInfo($"Insert_UpdateCourse responseData:  {result} ");
                    returnData.ResponseCode = result;
                    if (result >= 0)
                    {
                        returnData.Description = "Cập nhật khóa học thành công";

                        // Ghi log
                        AbstractDAOFactory.Instance().CreateUsersLogDAO().InsertUsersLog(new UsersLog
                        {
                            FunctionID = Permission.FunctionID,
                            FunctionName = Permission.FunctionName,
                            Description = returnData.Description + " Khóa học:" + request.Title + "|CourseType: " + request.CourseType,
                            UserID = userValidate.UserId,
                            ClientIP = userValidate.ClientIP
                        });
                    }
                    else
                    {
                        switch (result)
                        {
                            case -901:
                                returnData.Description = "Không tôn tại trường.";
                                break;
                            case -804:
                                returnData.Description = "Ngành học không tồn tại.";
                                break;
                            case -802:
                                returnData.Description = "Khóa học này đã tồn tại";
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

        //
        [HttpPost]
        [AuthorizationRequired]
        public JsonResult Delete_Course(int courseId)
        {
            var returnData = new ReturnData();
            try
            {
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

                if (courseId <= 0)
                {
                    returnData.ResponseCode = -600;
                    returnData.Description = "Tham số đầu vào không hợp lệ";
                    return Json(returnData);
                }

                var result = AbstractDAOFactory.Instance().CreateCourseDAO().Delete_Course(courseId, userValidate.UserName);
                returnData.ResponseCode = result;
                if (result >= 0)
                {
                    returnData.Description = "Xóa khóa học thành công";

                    // Ghi log
                    AbstractDAOFactory.Instance().CreateUsersLogDAO().InsertUsersLog(new UsersLog
                    {
                        FunctionID = Permission.FunctionID,
                        FunctionName = Permission.FunctionName,
                        Description = returnData.Description + " Khóa học: CourseId: " + courseId,
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

        // Cập nhật trạng thái
        [HttpPost]
        [AuthorizationRequired]
        public JsonResult UpdateStatus_Course(int courseId, int status)
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
                if (courseId <= 0)
                {
                    returnData.ResponseCode = -600;
                    returnData.Description = "Tham số đầu vào không hợp lệ";
                    return Json(returnData);
                }

                var result = AbstractDAOFactory.Instance().CreateCourseDAO().UpdateStatus_Course(courseId, status, userValidate.UserName);
                returnData.ResponseCode = result;
                if (result >= 0)
                {
                    returnData.Description = "Cập nhập trạng thái khóa học thành công";

                    // Ghi log
                    AbstractDAOFactory.Instance().CreateUsersLogDAO().InsertUsersLog(new UsersLog
                    {
                        FunctionID = Permission.FunctionID,
                        FunctionName = Permission.FunctionName,
                        Description = returnData.Description + " Khóa học: CourseId: " + courseId + "|Status:" + status,
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

        // Danh mục - ngành nghề
        [AuthorizationRequired]
        public ActionResult Specility()
        {

            var userValidate = new UserValidation();
            var urlLogin = Config.UrlRoot + "Account/FormLogin";
            if (!userValidate.IsSigned())
            {
                return Json(new { redirecturl = Config.UrlRoot + "Account/FormLogin" }, JsonRequestBehavior.AllowGet);
            }
            var checkPermission = userValidate.CheckPermissionUser("Specility", userValidate.UserId, userValidate.IsAdministrator);
            if (!checkPermission || Permission == null)
            {
                return Json(new { redirecturl = Config.UrlRoot + "common/errorpermission" }, JsonRequestBehavior.AllowGet);
            }

            return PartialView();
        }

        [AuthorizationRequired]
        public ActionResult Specility_Partial(byte courseType)
        {
            var userValidate = new UserValidation();
            var urlLogin = Config.UrlRoot + "Account/FormLogin";
            if (!userValidate.IsSigned())
            {
                return Json(new { redirecturl = Config.UrlRoot + "Account/FormLogin" }, JsonRequestBehavior.AllowGet);
            }
            var checkPermission = userValidate.CheckPermissionUser("Specility", userValidate.UserId, userValidate.IsAdministrator);
            if (!checkPermission || Permission == null)
            {
                return Json(new { redirecturl = Config.UrlRoot + "common/errorpermission" }, JsonRequestBehavior.AllowGet);
            }
            var list = AbstractDAOFactory.Instance().CreateCourseDAO().GetList_Specility(0, courseType);
            var listjson = Regex.Replace(JsonConvert.SerializeObject(list), @"\\r\\n|\\n|\\r|\\t", "");
            ViewBag.listjson = listjson;
            var permission = Regex.Replace(JsonConvert.SerializeObject((UserFunction)Session[SessionsManager.SESSION_PERMISSION]), @"\\r\\n|\\n|\\r|\\t", "");
            ViewBag.permission = permission;
            return PartialView();
        }

        [AuthorizationRequired]
        public ActionResult Specility_GetInfo(int id)
        {
            var userValidate = new UserValidation();
            var urlLogin = Config.UrlRoot + "Account/FormLogin";
            if (!userValidate.IsSigned())
            {
                return Json(new { redirecturl = Config.UrlRoot + "Account/FormLogin" }, JsonRequestBehavior.AllowGet);
            }
            var checkPermission = userValidate.CheckPermissionUser("Specility", userValidate.UserId, userValidate.IsAdministrator);
            if (!checkPermission || Permission == null)
            {
                return Json(new { redirecturl = Config.UrlRoot + "common/errorpermission" }, JsonRequestBehavior.AllowGet);
            }
            if (id <= 0)
            {
                return PartialView(new Specility());
            }
            var detail = AbstractDAOFactory.Instance().CreateCourseDAO().GetList_Specility(id, 0).SingleOrDefault();
            return PartialView(detail);
        }


        [HttpPost]
        [AuthorizationRequired]
        public JsonResult Insert_UpdateSpecility(Specility request)
        {
            var returnData = new ReturnData();
            var userValidate = new UserValidation();
            if (!userValidate.IsSigned())
            {
                return Json(new { redirecturl = Config.UrlRoot + "Account/FormLogin" }, JsonRequestBehavior.AllowGet);
            }
            var checkPermission = userValidate.CheckPermissionUser("Specility", userValidate.UserId, userValidate.IsAdministrator);
            if (!checkPermission || Permission == null)
            {
                returnData.ResponseCode = -101;
                returnData.Description = "Bạn không có quyền sử dụng chức năng này";
                return Json(returnData);
            }
            try
            {
                request.CreatedUser = userValidate.UserName;
                var result = AbstractDAOFactory.Instance().CreateCourseDAO().Speciality_Insert_Update(request);
                returnData.ResponseCode = result;
                if (request.Id <= 0) // insert
                {
                    if (result >= 0)
                    {
                        returnData.Description = request.Id <= 0 ? "Tạo thành công" : "Sửa thành công";

                        // Ghi log
                        AbstractDAOFactory.Instance().CreateUsersLogDAO().InsertUsersLog(new UsersLog
                        {
                            FunctionID = Permission.FunctionID,
                            FunctionName = Permission.FunctionName,
                            Description = returnData.Description + " Danh muc:" + request.Name,
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
        [AuthorizationRequired]
        public JsonResult Delete_Specility(int id)
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

                var request = new Specility
                {
                    Id = id,
                    CreatedUser = userValidate.UserName
                };
                var result = AbstractDAOFactory.Instance().CreateCourseDAO().Speciality_Delete(request);
                returnData.ResponseCode = result;
                if (result >= 0)
                {
                    returnData.Description = "Xóa thành công";

                    // Ghi log
                    AbstractDAOFactory.Instance().CreateUsersLogDAO().InsertUsersLog(new UsersLog
                    {
                        FunctionID = Permission.FunctionID,
                        FunctionName = Permission.FunctionName,
                        Description = returnData.Description + " Khóa học: Id: " + id,
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


        // học bổng
        [AuthorizationRequired]
        public ActionResult Scholarship()
        {

            var userValidate = new UserValidation();
            var urlLogin = Config.UrlRoot + "Account/FormLogin";
            if (!userValidate.IsSigned())
            {
                return Json(new { redirecturl = Config.UrlRoot + "Account/FormLogin" }, JsonRequestBehavior.AllowGet);
            }
            var checkPermission = userValidate.CheckPermissionUser("Scholarship", userValidate.UserId, userValidate.IsAdministrator);
            if (!checkPermission || Permission == null)
            {
                return Json(new { redirecturl = Config.UrlRoot + "common/errorpermission" }, JsonRequestBehavior.AllowGet);
            }

            return PartialView();
        }

        [AuthorizationRequired]
        public ActionResult Scholarship_Partial(string fromDate, string toDate, byte type, byte status)
        {
            var userValidate = new UserValidation();
            var urlLogin = Config.UrlRoot + "Account/FormLogin";
            if (!userValidate.IsSigned())
            {
                return Json(new { redirecturl = Config.UrlRoot + "Account/FormLogin" }, JsonRequestBehavior.AllowGet);
            }
            var checkPermission = userValidate.CheckPermissionUser("Scholarship", userValidate.UserId, userValidate.IsAdministrator);
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
            var list = AbstractDAOFactory.Instance().CreateCourseDAO().Scholarship_GetPage(new Scholarship()
            {
                Type = type,
                FromDate = fDate,
                ToDate = tDate,
                Status = status
            });
            var listjson = Regex.Replace(JsonConvert.SerializeObject(list), @"\\r\\n|\\n|\\r|\\t", "");
            ViewBag.listjson = listjson;
            var permission = Regex.Replace(JsonConvert.SerializeObject((UserFunction)Session[SessionsManager.SESSION_PERMISSION]), @"\\r\\n|\\n|\\r|\\t", "");
            ViewBag.permission = permission;
            return PartialView();
        }

        [AuthorizationRequired]
        public ActionResult Scholarship_GetInfo(int id)
        {
            var userValidate = new UserValidation();
            var urlLogin = Config.UrlRoot + "Account/FormLogin";
            if (!userValidate.IsSigned())
            {
                return Json(new { redirecturl = Config.UrlRoot + "Account/FormLogin" }, JsonRequestBehavior.AllowGet);
            }
            var checkPermission = userValidate.CheckPermissionUser("Scholarship", userValidate.UserId, userValidate.IsAdministrator);
            if (!checkPermission || Permission == null)
            {
                return Json(new { redirecturl = Config.UrlRoot + "common/errorpermission" }, JsonRequestBehavior.AllowGet);
            }
            if (id <= 0)
            {
                return PartialView(new Scholarship() { PublishDate = DateTime.Now.AddMinutes(20) });
            }
            var detail = AbstractDAOFactory.Instance().CreateCourseDAO().Scholarship_Get_Similar(id);
            return PartialView(detail);
        }

        [HttpPost]
        [ValidateInput(false)]
        [AuthorizationRequired]
        public JsonResult Insert_UpdateScholarship(Scholarship request)
        {
            var returnData = new ReturnData();
            var userValidate = new UserValidation();
            if (!userValidate.IsSigned())
            {
                return Json(new { redirecturl = Config.UrlRoot + "Account/FormLogin" }, JsonRequestBehavior.AllowGet);
            }
            var checkPermission = userValidate.CheckPermissionUser("Scholarship", userValidate.UserId, userValidate.IsAdministrator);
            if (!checkPermission || Permission == null)
            {
                returnData.ResponseCode = -101;
                returnData.Description = "Bạn không có quyền sử dụng chức năng này";
                return Json(returnData);
            }
            try
            {
                var publishDate = DateTime.Now;
                if (!string.IsNullOrEmpty(request.PublishDate_String))
                {
                    publishDate = DateTime.ParseExact(request.PublishDate_String, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
                }
                request.PublishDate = publishDate;
                if (request.Id <= 0) // insert
                {
                    // Ảnh
                    request.Banner = request.Banner.Replace(ConfigurationManager.AppSettings["New_Root_URL"] + "images/", "");
                    request.Avatar = request.Banner.Replace(ConfigurationManager.AppSettings["New_Root_URL"] + "images/", "");

                    request.CreatedUser = userValidate.UserName;
                    var result = AbstractDAOFactory.Instance().CreateCourseDAO().Scholarship_Insert(request);
                    returnData.ResponseCode = result;
                }
                else  // Update
                {
                    // Ảnh
                    request.Banner = request.Banner.Replace(ConfigurationManager.AppSettings["New_Root_URL"] + "images/", "");
                    request.Avatar = request.Banner.Replace(ConfigurationManager.AppSettings["New_Root_URL"] + "images/", "");

                    request.CreatedUser = userValidate.UserName;
                    var result = AbstractDAOFactory.Instance().CreateCourseDAO().Scholarship_Update(request);
                    returnData.ResponseCode = result;
                }

                if (returnData.ResponseCode >= 0)
                {
                    returnData.Description = request.Id <= 0 ? "Tạo thành công" : "Sửa thành công";

                    // Ghi log
                    AbstractDAOFactory.Instance().CreateUsersLogDAO().InsertUsersLog(new UsersLog
                    {
                        FunctionID = Permission.FunctionID,
                        FunctionName = Permission.FunctionName,
                        Description = returnData.Description + " Học bổng:" + request.Title,
                        UserID = userValidate.UserId,
                        ClientIP = userValidate.ClientIP
                    });
                }
                else
                {
                    switch (returnData.ResponseCode)
                    {
                        case -1001:
                            returnData.Description = "Học bổng không tồn tại";
                            break;
                        case -1002:
                            returnData.Description = "Học bổng đã tồn tại trên hệ thống";
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
        public JsonResult Delete_Scholarship(int id)
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

                var result = AbstractDAOFactory.Instance().CreateCourseDAO().Scholarship_Delete(new Scholarship() { Id = id, CreatedUser = userValidate.UserName });
                returnData.ResponseCode = result;
                if (result >= 0)
                {
                    returnData.Description = "Xóa thành công";

                    // Ghi log
                    AbstractDAOFactory.Instance().CreateUsersLogDAO().InsertUsersLog(new UsersLog
                    {
                        FunctionID = Permission.FunctionID,
                        FunctionName = Permission.FunctionName,
                        Description = returnData.Description + " Học bổng: Id: " + id,
                        UserID = userValidate.UserId,
                        ClientIP = userValidate.ClientIP
                    });
                }
                else
                {
                    switch (result)
                    {
                        case -99:
                            returnData.Description = "Lỗi hệ thống. Vui lòng liên hệ quản trị viên !";
                            break;
                        case -600:
                            returnData.Description = "Vui lòng nhập đầy đủ thông tin";
                            break;
                        case -901:
                            returnData.Description = "Học bổng không tôn tại. Vui lòng thử lại sau !";
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



        // Đk trực tuyến
        [AuthorizationRequired]
        public ActionResult Register_Course()
        {
            var userValidate = new UserValidation();
            var urlLogin = Config.UrlRoot + "Account/FormLogin";
            if (!userValidate.IsSigned())
            {
                return Json(new { redirecturl = Config.UrlRoot + "Account/FormLogin" }, JsonRequestBehavior.AllowGet);
            }
            var checkPermission = userValidate.CheckPermissionUser("Register_Course", userValidate.UserId, userValidate.IsAdministrator);
            if (!checkPermission || Permission == null)
            {
                return Json(new { redirecturl = Config.UrlRoot + "common/errorpermission" }, JsonRequestBehavior.AllowGet);
            }

            return PartialView();
        }

        public ActionResult Register_Course_Partial(string fromDate, string toDate, byte type)
        {

            var userValidate = new UserValidation();
            var urlLogin = Config.UrlRoot + "Account/FormLogin";
            if (!userValidate.IsSigned())
            {
                return Json(new { redirecturl = Config.UrlRoot + "Account/FormLogin" }, JsonRequestBehavior.AllowGet);
            }
            var checkPermission = userValidate.CheckPermissionUser("Register_Course", userValidate.UserId, userValidate.IsAdministrator);
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
            var list = AbstractDAOFactory.Instance().CreateCourseDAO().Course_Register_Get(fDate, tDate, type);
            var listjson = Regex.Replace(JsonConvert.SerializeObject(list), @"\\r\\n|\\n|\\r|\\t", "");
            ViewBag.listjson = listjson;
            var permission = Regex.Replace(JsonConvert.SerializeObject((UserFunction)Session[SessionsManager.SESSION_PERMISSION]), @"\\r\\n|\\n|\\r|\\t", "");
            ViewBag.permission = permission;
            return PartialView();
        }

        public ActionResult Register_Course_GetInfo(int id)
        {
            var userValidate = new UserValidation();
            var urlLogin = Config.UrlRoot + "Account/FormLogin";
            if (!userValidate.IsSigned())
            {
                return Json(new { redirecturl = Config.UrlRoot + "Account/FormLogin" }, JsonRequestBehavior.AllowGet);
            }
            var checkPermission = userValidate.CheckPermissionUser("Register_Course", userValidate.UserId, userValidate.IsAdministrator);
            if (!checkPermission || Permission == null)
            {
                return Json(new { redirecturl = Config.UrlRoot + "common/errorpermission" }, JsonRequestBehavior.AllowGet);
            }
            var data = AbstractDAOFactory.Instance().CreateCourseDAO().Course_Register_Get_Detail(id);
            return PartialView(data);
        }

        [HttpPost]
        [AuthorizationRequired]
        public JsonResult Update_Register_Course(Course_Register request)
        {
            var returnData = new ReturnData();
            var userValidate = new UserValidation();
            if (!userValidate.IsSigned())
            {
                return Json(new { redirecturl = Config.UrlRoot + "Account/FormLogin" }, JsonRequestBehavior.AllowGet);
            }
            var checkPermission = userValidate.CheckPermissionUser("Register_Course", userValidate.UserId, userValidate.IsAdministrator);
            if (!checkPermission || Permission == null)
            {
                returnData.ResponseCode = -101;
                returnData.Description = "Bạn không có quyền sử dụng chức năng này";
                return Json(returnData);
            }
            try
            {
                if (request == null || request.Id <= 0)
                {
                    returnData.ResponseCode = -600;
                    returnData.Description = "Tham số không hợp lệ. Vui lòng thử lại sau";
                    return Json(returnData);
                }

                var data = AbstractDAOFactory.Instance().CreateCourseDAO().Course_Register_Get_Detail((int)request.Id);
                if (data == null || data.Id <= 0)
                {
                    returnData.ResponseCode = -100;
                    returnData.Description = "Đơn đăng ký trực tuyến này không tồn tại. ";
                    return Json(returnData);
                }

                request.CoursesID = data.CoursesID;
                request.CreatedUser = userValidate.UserName;
                var result = AbstractDAOFactory.Instance().CreateCourseDAO().SP_Course_Register_Update(request);
                returnData.ResponseCode = result;
                if (returnData.ResponseCode >= 0)
                {
                    returnData.Description = "Sửa thành công";

                    // Ghi log
                    AbstractDAOFactory.Instance().CreateUsersLogDAO().InsertUsersLog(new UsersLog
                    {
                        FunctionID = Permission.FunctionID,
                        FunctionName = Permission.FunctionName,
                        Description = returnData.Description + " Đăng ký trực tuyến: Id:" + request.Id + "| Phone:" + request.Phone + "|Email: " + request.Email,
                        UserID = userValidate.UserId,
                        ClientIP = userValidate.ClientIP
                    });
                }
                else
                {
                    switch (returnData.ResponseCode)
                    {
                        case -801:
                            returnData.Description = "Khóa học không tồn tại";
                            break;
                        case -1101:
                            returnData.Description = "Đơn này không tồn tại trên hệ thống";
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
        public JsonResult Delete_Register_Course(int id)
        {
            var returnData = new ReturnData();
            var userValidate = new UserValidation();
            if (!userValidate.IsSigned())
            {
                return Json(new { redirecturl = Config.UrlRoot + "Account/FormLogin" }, JsonRequestBehavior.AllowGet);
            }
            var checkPermission = userValidate.CheckPermissionUser("Register_Course", userValidate.UserId, userValidate.IsAdministrator);
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
                    returnData.Description = "Tham số không hợp lệ. Vui lòng thử lại sau";
                    return Json(returnData);
                }

                var result = AbstractDAOFactory.Instance().CreateCourseDAO().Course_Register_Delete(new Course_Register()
                {
                    Id = id,
                    CreatedUser = userValidate.UserName,
                    ClientIP = Config.GetIP()
                });
                returnData.ResponseCode = result;
                if (returnData.ResponseCode >= 0)
                {
                    returnData.Description = "Xóa thành công";

                    // Ghi log
                    AbstractDAOFactory.Instance().CreateUsersLogDAO().InsertUsersLog(new UsersLog
                    {
                        FunctionID = Permission.FunctionID,
                        FunctionName = Permission.FunctionName,
                        Description = returnData.Description + " Xóa đơn: " + id,
                        UserID = userValidate.UserId,
                        ClientIP = userValidate.ClientIP
                    });
                }
                else
                {
                    switch (returnData.ResponseCode)
                    {
                        case -1101:
                            returnData.Description = "Đơn này không tồn tại trên hệ thống";
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


        // Liên hệ Trường, cơ sở 
        [AuthorizationRequired]
        public ActionResult Education_Contact()
        {
            var userValidate = new UserValidation();
            var urlLogin = Config.UrlRoot + "Account/FormLogin";
            if (!userValidate.IsSigned())
            {
                return Json(new { redirecturl = Config.UrlRoot + "Account/FormLogin" }, JsonRequestBehavior.AllowGet);
            }
            var checkPermission = userValidate.CheckPermissionUser("Education_Contact", userValidate.UserId, userValidate.IsAdministrator);
            if (!checkPermission || Permission == null)
            {
                return Json(new { redirecturl = Config.UrlRoot + "common/errorpermission" }, JsonRequestBehavior.AllowGet);
            }
            return PartialView();
        }

        [AuthorizationRequired]
        public ActionResult Education_Contact_Partial(string fromDate, string toDate)
        {
            var userValidate = new UserValidation();
            var urlLogin = Config.UrlRoot + "Account/FormLogin";
            if (!userValidate.IsSigned())
            {
                return Json(new { redirecturl = Config.UrlRoot + "Account/FormLogin" }, JsonRequestBehavior.AllowGet);
            }
            var checkPermission = userValidate.CheckPermissionUser("Education_Contact", userValidate.UserId, userValidate.IsAdministrator);
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

            var list = AbstractDAOFactory.Instance().CreateCourseDAO().Education_Contact_Get(fDate, tDate, 0, 0, new DateTime(), new DateTime());
            var listjson = Regex.Replace(JsonConvert.SerializeObject(list), @"\\r\\n|\\n|\\r|\\t", "");
            ViewBag.listjson = listjson;
            var permission = Regex.Replace(JsonConvert.SerializeObject((UserFunction)Session[SessionsManager.SESSION_PERMISSION]), @"\\r\\n|\\n|\\r|\\t", "");
            ViewBag.permission = permission;
            return PartialView();
        }

        [AuthorizationRequired]
        public ActionResult Education_Contact_GetInfo(int id)
        {
            var userValidate = new UserValidation();
            var urlLogin = Config.UrlRoot + "Account/FormLogin";
            if (!userValidate.IsSigned())
            {
                return Json(new { redirecturl = urlLogin }, JsonRequestBehavior.AllowGet);
            }
            var checkPermission = userValidate.CheckPermissionUser("Education_Contact", userValidate.UserId, userValidate.IsAdministrator);
            if (!checkPermission || Permission == null)
            {
                return Json(new { redirecturl = Config.UrlRoot + "common/errorpermission" }, JsonRequestBehavior.AllowGet);
            }
            //var listSpecility = AbstractDAOFactory.Instance().CreateCourseDAO().GetList_Specility(0, 1);
            //ViewBag.listSpecility = listSpecility;
            var listEducation = AbstractDAOFactory.Instance().CreateAccountDAO().Education_GetList(new Request_Education());
            ViewBag.listEducation = listEducation;
            var data = AbstractDAOFactory.Instance().CreateCourseDAO().Education_Contact_Get_Detail(id);

            // gửi email 
            //var sendEmail = new POP3Auth().SendEmail("", "", "cuongbx@1fintech.vn", "cai deo gi day");
            return PartialView(data);
        }

        [HttpPost]
        [AuthorizationRequired]
        public JsonResult Education_Contact_Update(Education_Contact request)
        {
            var returnData = new ReturnData();
            var userValidate = new UserValidation();
            if (!userValidate.IsSigned())
            {
                return Json(new { redirecturl = Config.UrlRoot + "Account/FormLogin" }, JsonRequestBehavior.AllowGet);
            }
            var checkPermission = userValidate.CheckPermissionUser("Education_Contact", userValidate.UserId, userValidate.IsAdministrator);
            if (!checkPermission || Permission == null)
            {
                returnData.ResponseCode = -101;
                returnData.Description = "Bạn không có quyền sử dụng chức năng này";
                return Json(returnData);
            }
            try
            {
                if (request == null || request.Id <= 0)
                {
                    returnData.ResponseCode = -600;
                    returnData.Description = "Tham số không hợp lệ. Vui lòng thử lại sau";
                    return Json(returnData);
                }

                var data = AbstractDAOFactory.Instance().CreateCourseDAO().Education_Contact_Get_Detail((int)request.Id);
                if (data == null || data.Id <= 0)
                {
                    returnData.ResponseCode = -101;
                    returnData.Description = "Liên hệ không tồn tại. Vui lòng thử lại sau";
                    return Json(returnData);
                }

                request.SpecialityID = data.SpecialityID;
                request.EstimatedTime = data.EstimatedTime;
                request.UpdateUser = userValidate.UserName;
                var result = AbstractDAOFactory.Instance().CreateCourseDAO().Education_Contact_Update(request);
                returnData.ResponseCode = result;
                if (returnData.ResponseCode >= 0)
                {
                    returnData.Description = "Sửa thành công";

                    // Ghi log
                    AbstractDAOFactory.Instance().CreateUsersLogDAO().InsertUsersLog(new UsersLog
                    {
                        FunctionID = Permission.FunctionID,
                        FunctionName = Permission.FunctionName,
                        Description = returnData.Description + " Đăng ký trực tuyến: Id:" + request.Id + "| Phone:" + request.Phone + "|Email: " + request.Email,
                        UserID = userValidate.UserId,
                        ClientIP = userValidate.ClientIP
                    });
                }
                else
                {
                    switch (returnData.ResponseCode)
                    {
                        case -804:
                            returnData.Description = "Không tồn tại ngành học";
                            break;
                        case -901:
                            returnData.Description = "Không tồn tại trường";
                            break;
                        case -1103:
                            returnData.Description = "Không tồn tại liên hệ này. Vui lòng thử lại sau";
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
        public JsonResult Education_Contact_Delete(int id)
        {
            var returnData = new ReturnData();
            var userValidate = new UserValidation();
            if (!userValidate.IsSigned())
            {
                return Json(new { redirecturl = Config.UrlRoot + "Account/FormLogin" }, JsonRequestBehavior.AllowGet);
            }
            var checkPermission = userValidate.CheckPermissionUser("Education_Contact", userValidate.UserId, userValidate.IsAdministrator);
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
                    returnData.Description = "Tham số không hợp lệ. Vui lòng thử lại sau";
                    return Json(returnData);
                }

                var result = AbstractDAOFactory.Instance().CreateCourseDAO().Education_Contact_Delete(new Education_Contact() { Id = id, CreatedUser = userValidate.UserName });
                returnData.ResponseCode = result;
                if (returnData.ResponseCode >= 0)
                {
                    returnData.Description = "Xóa thành công";

                    // Ghi log
                    AbstractDAOFactory.Instance().CreateUsersLogDAO().InsertUsersLog(new UsersLog
                    {
                        FunctionID = Permission.FunctionID,
                        FunctionName = Permission.FunctionName,
                        Description = returnData.Description + " Xóa đơn: " + id,
                        UserID = userValidate.UserId,
                        ClientIP = userValidate.ClientIP
                    });
                }
                else
                {
                    switch (returnData.ResponseCode)
                    {
                        case -1103:
                            returnData.Description = "Không tồn tại liên hệ này. Vui lòng thử lại sau";
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



        // Log đk cơ sở đào tạo
        [AuthorizationRequired]
        public ActionResult RegisterEdu()
        {
            var userValidate = new UserValidation();
            var urlLogin = Config.UrlRoot + "Account/FormLogin";
            if (!userValidate.IsSigned())
            {
                return Json(new { redirecturl = Config.UrlRoot + "Account/FormLogin" }, JsonRequestBehavior.AllowGet);
            }
            var checkPermission = userValidate.CheckPermissionUser("RegisterEdu", userValidate.UserId, userValidate.IsAdministrator);
            if (!checkPermission || Permission == null)
            {
                return Json(new { redirecturl = Config.UrlRoot + "common/errorpermission" }, JsonRequestBehavior.AllowGet);
            }
            return PartialView();
        }

        [AuthorizationRequired]
        public ActionResult RegisterEdu_Partial(string fromDate, string toDate, string email, string phone)
        {
            var userValidate = new UserValidation();
            var urlLogin = Config.UrlRoot + "Account/FormLogin";
            if (!userValidate.IsSigned())
            {
                return Json(new { redirecturl = Config.UrlRoot + "Account/FormLogin" }, JsonRequestBehavior.AllowGet);
            }
            var checkPermission = userValidate.CheckPermissionUser("RegisterEdu", userValidate.UserId, userValidate.IsAdministrator);
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

            var list = AbstractDAOFactory.Instance().CreateCourseDAO().RegisterEdu_Request_GetPage(fDate, tDate, email, "", phone);
            var listjson = Regex.Replace(JsonConvert.SerializeObject(list), @"\\r\\n|\\n|\\r|\\t", "");
            ViewBag.listjson = listjson;
            var permission = Regex.Replace(JsonConvert.SerializeObject((UserFunction)Session[SessionsManager.SESSION_PERMISSION]), @"\\r\\n|\\n|\\r|\\t", "");
            ViewBag.permission = permission;
            return PartialView();
        }

        [AuthorizationRequired]
        public ActionResult RegisterEdu_GetInfo(int id)
        {
            var userValidate = new UserValidation();
            var urlLogin = Config.UrlRoot + "Account/FormLogin";
            if (!userValidate.IsSigned())
            {
                return Json(new { redirecturl = Config.UrlRoot + "Account/FormLogin" }, JsonRequestBehavior.AllowGet);
            }
            var checkPermission = userValidate.CheckPermissionUser("RegisterEdu", userValidate.UserId, userValidate.IsAdministrator);
            if (!checkPermission || Permission == null)
            {
                return Json(new { redirecturl = Config.UrlRoot + "common/errorpermission" }, JsonRequestBehavior.AllowGet);
            }
            var data = AbstractDAOFactory.Instance().CreateCourseDAO().RegisterEdu_Request_Detail(id);
            return PartialView(data);
        }

    }
}