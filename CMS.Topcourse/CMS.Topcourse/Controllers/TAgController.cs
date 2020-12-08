using System;
using System.Collections.Generic;
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
using Account = Topcourse.DataAccess.DTO.Account;

namespace CMS.Topcourse.Controllers
{
    public class TagController : Controller
    {
        private UserFunction Permission { get { return ((UserFunction)Session[SessionsManager.SESSION_PERMISSION]); } }

        [AuthorizationRequired]
        public ActionResult TagGroup()
        {
            var userValidate = new UserValidation();
            var urlLogin = Config.UrlRoot + "Account/FormLogin";
            if (!userValidate.IsSigned())
            {
                return Json(new { redirecturl = Config.UrlRoot + "Account/FormLogin" }, JsonRequestBehavior.AllowGet);
            }
            var checkPermission = userValidate.CheckPermissionUser("TagGroup", userValidate.UserId, userValidate.IsAdministrator);
            if (!checkPermission || Permission == null)
            {
                return Json(new { redirecturl = Config.UrlRoot + "common/errorpermission" }, JsonRequestBehavior.AllowGet);
            }
            return PartialView();
        }

        [AuthorizationRequired]
        public ActionResult TagGroup_Partial(bool isActive, byte groupType, byte position)
        {
            var userValidate = new UserValidation();
            var urlLogin = Config.UrlRoot + "Account/FormLogin";
            if (!userValidate.IsSigned())
            {
                return Json(new { redirecturl = Config.UrlRoot + "Account/FormLogin" }, JsonRequestBehavior.AllowGet);
            }
            var checkPermission = userValidate.CheckPermissionUser("TagGroup", userValidate.UserId, userValidate.IsAdministrator);
            if (!checkPermission || Permission == null)
            {
                return Json(new { redirecturl = Config.UrlRoot + "common/errorpermission" }, JsonRequestBehavior.AllowGet);
            }
            var list = AbstractDAOFactory.Instance().CreateCourseDAO().TagGroup_Get(new TagGroup() { IsActive = isActive, GroupType = groupType, Position = position }).OrderBy(p => p.Order);
            var listjson = Regex.Replace(JsonConvert.SerializeObject(list), @"\\r\\n|\\n|\\r|\\t", "");
            ViewBag.listjson = listjson;
            var permission = Regex.Replace(JsonConvert.SerializeObject((UserFunction)Session[SessionsManager.SESSION_PERMISSION]), @"\\r\\n|\\n|\\r|\\t", "");
            ViewBag.permission = permission;
            return PartialView();
        }

        #region Sắp xếp TagGroup
        [AuthorizationRequired]
        public ActionResult TagGroup_Position()
        {
            var userValidate = new UserValidation();
            var urlLogin = Config.UrlRoot + "Account/FormLogin";
            if (!userValidate.IsSigned())
            {
                return Json(new { redirecturl = Config.UrlRoot + "Account/FormLogin" }, JsonRequestBehavior.AllowGet);
            }
            var checkPermission = userValidate.CheckPermissionUser("TagGroup", userValidate.UserId, userValidate.IsAdministrator);
            if (!checkPermission || Permission == null)
            {
                return Json(new { redirecturl = Config.UrlRoot + "common/errorpermission" }, JsonRequestBehavior.AllowGet);
            }
            return PartialView();
        }

        [AuthorizationRequired]
        public ActionResult TagGroup_Position_Partial(bool isActive, byte groupType, byte position)
        {
            var userValidate = new UserValidation();
            var urlLogin = Config.UrlRoot + "Account/FormLogin";
            if (!userValidate.IsSigned())
            {
                return Json(new { redirecturl = Config.UrlRoot + "Account/FormLogin" }, JsonRequestBehavior.AllowGet);
            }
            var checkPermission = userValidate.CheckPermissionUser("TagGroup", userValidate.UserId, userValidate.IsAdministrator);
            if (!checkPermission || Permission == null)
            {
                return Json(new { redirecturl = Config.UrlRoot + "common/errorpermission" }, JsonRequestBehavior.AllowGet);
            }
            var list = AbstractDAOFactory.Instance().CreateCourseDAO().TagGroup_Get(new TagGroup() { IsActive = isActive, GroupType = groupType, Position = position }).OrderBy(p => p.Order).ToList();
            return PartialView(list);
        }

        [HttpPost]
        public JsonResult SaveOrderTagGroup(List<NestedOrder> listOrder)
        {
            var ReturnData = new ReturnData();
            var userValidate = new UserValidation();
            if (!userValidate.IsSigned())
            {
                return Json(new { redirecturl = Config.UrlRoot + "Account/FormLogin" }, JsonRequestBehavior.AllowGet);
            }
            if (!userValidate.IsAdministrator || Permission == null || Permission.ActionName != "TagGroup" || !Permission.IsUpdate)
            {
                ReturnData.ResponseCode = -101;
                ReturnData.Description = "Bạn không có quyền sử dụng chức năng này";
                return Json(ReturnData);
            }
            if (listOrder.Count <= 0)
            {
                ReturnData.ResponseCode = -100;
                ReturnData.Description = "không tồn tại danh sách chức năng sắp xếp mới";
                return Json(ReturnData);
            }
            try
            {
                // function SP_TagGroup_UpdateOrder
                foreach (var t in listOrder)
                {
                    AbstractDAOFactory.Instance().CreateCourseDAO().SP_TagGroup_UpdateOrder(new TagGroup() { TagGroupId = t.Id, Order = t.Order });
                }

                ReturnData.ResponseCode = 1;
                ReturnData.Description = "Sắp xếp nhóm Thẻ thành công";
                //Ghi log

                AbstractDAOFactory.Instance().CreateUsersLogDAO().InsertUsersLog(new UsersLog
                {
                    FunctionID = Permission.FunctionID,
                    FunctionName = Permission.FunctionName,
                    Description = ReturnData.Description,
                    UserID = userValidate.UserId,
                    ClientIP = userValidate.ClientIP
                });
            }
            catch (Exception ex)
            {
                Log4Net.PublishException(ex);
                ReturnData.ResponseCode = -99;
                ReturnData.Description = "Hệ thống đang bận. Vui lòng quay lại sau";
            }
            return Json(ReturnData);
        }
        #endregion


        [AuthorizationRequired]
        public ActionResult TagGroup_GetInfo(int id)
        {
            var userValidate = new UserValidation();
            var urlLogin = Config.UrlRoot + "Account/FormLogin";
            if (!userValidate.IsSigned())
            {
                return Json(new { redirecturl = Config.UrlRoot + "Account/FormLogin" }, JsonRequestBehavior.AllowGet);
            }
            var checkPermission = userValidate.CheckPermissionUser("TagGroup", userValidate.UserId, userValidate.IsAdministrator);
            if (!checkPermission || Permission == null)
            {
                return Json(new { redirecturl = Config.UrlRoot + "common/errorpermission" }, JsonRequestBehavior.AllowGet);
            }
            var listTag = AbstractDAOFactory.Instance().CreateCourseDAO().Tag_Get(new Tag() { Status = true }).Where(p => p.Status).ToList();
            ViewBag.listTag = listTag;
            if (id <= 0)
            {
                return PartialView(new TagGroup());
            }
            var tag = AbstractDAOFactory.Instance().CreateCourseDAO().TagGroup_Get(new TagGroup() { TagGroupId = id, Position = 99 }).SingleOrDefault();
            return PartialView(tag);
        }

        [HttpPost]
        [AuthorizationRequired]
        public JsonResult Insert_UpdateTagGroup(TagGroup request)
        {
            var returnData = new ReturnData();
            var userValidate = new UserValidation();
            if (!userValidate.IsSigned())
            {
                return Json(new { redirecturl = Config.UrlRoot + "Account/FormLogin" }, JsonRequestBehavior.AllowGet);
            }
            var checkPermission = userValidate.CheckPermissionUser("TagGroup", userValidate.UserId, userValidate.IsAdministrator);
            if (!checkPermission || Permission == null)
            {
                returnData.ResponseCode = -101;
                returnData.Description = "Bạn không có quyền sử dụng chức năng này";
                return Json(returnData);
            }
            try
            {
                if (request == null
                    || string.IsNullOrEmpty(request.TagGroupName)
                    || request.GroupType <= 0)
                {
                    returnData.ResponseCode = -600;
                    returnData.Description = "Tham số đầu vào không hợp lệ. Vui lòng nhập đầy đủ thông tin";
                    return Json(returnData);
                }


                if (request.TagGroupId <= 0) // insert
                {
                    request.CreatedUser = userValidate.UserName;
                    var result = AbstractDAOFactory.Instance().CreateCourseDAO().TagGroup_Insert(request);
                    returnData.ResponseCode = result;
                }
                else
                {
                    request.CreatedUser = userValidate.UserName;
                    var result = AbstractDAOFactory.Instance().CreateCourseDAO().TagGroup_Update(request);
                    returnData.ResponseCode = result;
                }

                if (returnData.ResponseCode >= 0)
                {
                    returnData.Description = request.TagGroupId <= 0 ? "Tạo thành công" : "Cập nhật thành công";

                    // Ghi log
                    AbstractDAOFactory.Instance().CreateUsersLogDAO().InsertUsersLog(new UsersLog
                    {
                        FunctionID = Permission.FunctionID,
                        FunctionName = Permission.FunctionName,
                        Description = returnData.Description + "TagGroup :" + request.TagGroupName,
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
                        case -1201:
                            returnData.Description = "Nhóm Tag đã tồn tại trên hệ thống";
                            break;
                        case -1202:
                            returnData.Description = "Nhóm Tag không tồn tại trên hệ thống";
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
        public JsonResult Delete_TagGroup(int id)
        {
            var returnData = new ReturnData();
            try
            {
                var userValidate = new UserValidation();
                if (!userValidate.IsSigned())
                {
                    return Json(new { redirecturl = Config.UrlRoot + "Account/FormLogin" }, JsonRequestBehavior.AllowGet);
                }
                var checkPermission = userValidate.CheckPermissionUser("TagGroup", userValidate.UserId, userValidate.IsAdministrator);
                if (!checkPermission || Permission == null)
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


                var result = AbstractDAOFactory.Instance().CreateCourseDAO().SP_TagGroup_Delete(new TagGroup() { TagGroupId = id, CreatedUser = userValidate.UserName });
                returnData.ResponseCode = result;

                if (returnData.ResponseCode >= 0)
                {
                    returnData.Description = "Xóa thẻ thành công";

                    // Ghi log
                    AbstractDAOFactory.Instance().CreateUsersLogDAO().InsertUsersLog(new UsersLog
                    {
                        FunctionID = Permission.FunctionID,
                        FunctionName = Permission.FunctionName,
                        Description = returnData.Description + "TagId :" + id,
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
                        case -1203:
                            returnData.Description = "Tag không tồn tại trên hệ thống";
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


        [AuthorizationRequired]
        public ActionResult Tag()
        {
            var userValidate = new UserValidation();
            var urlLogin = Config.UrlRoot + "Account/FormLogin";
            if (!userValidate.IsSigned())
            {
                return Json(new { redirecturl = Config.UrlRoot + "Account/FormLogin" }, JsonRequestBehavior.AllowGet);
            }
            var checkPermission = userValidate.CheckPermissionUser("Tag", userValidate.UserId, userValidate.IsAdministrator);
            if (!checkPermission || Permission == null)
            {
                return Json(new { redirecturl = Config.UrlRoot + "common/errorpermission" }, JsonRequestBehavior.AllowGet);
            }
            var listTagGroup = AbstractDAOFactory.Instance().CreateCourseDAO().TagGroup_Get(new TagGroup() { TagGroupId = 0, TagGroupName = "", GroupType = 0, Position = 99 }).Where(p => p.IsActive).ToList();
            ViewBag.listTagGroup = listTagGroup;
            return PartialView();
        }

        [AuthorizationRequired]
        public ActionResult Tag_Partial(bool status, int tagGroupId)
        {
            var userValidate = new UserValidation();
            var urlLogin = Config.UrlRoot + "Account/FormLogin";
            if (!userValidate.IsSigned())
            {
                return Json(new { redirecturl = Config.UrlRoot + "Account/FormLogin" }, JsonRequestBehavior.AllowGet);
            }
            var checkPermission = userValidate.CheckPermissionUser("Tag", userValidate.UserId, userValidate.IsAdministrator);
            if (!checkPermission || Permission == null)
            {
                return Json(new { redirecturl = Config.UrlRoot + "common/errorpermission" }, JsonRequestBehavior.AllowGet);
            }
            var list = AbstractDAOFactory.Instance().CreateCourseDAO().Tag_Get(new Tag() { Status = status, TagGroupId = tagGroupId }).OrderBy(p => p.Order);
            var listjson = Regex.Replace(JsonConvert.SerializeObject(list), @"\\r\\n|\\n|\\r|\\t", "");
            ViewBag.listjson = listjson;
            var permission = Regex.Replace(JsonConvert.SerializeObject((UserFunction)Session[SessionsManager.SESSION_PERMISSION]), @"\\r\\n|\\n|\\r|\\t", "");
            ViewBag.permission = permission;
            return PartialView();
        }

        //Sắp xếp thẻ 
        [AuthorizationRequired]
        public ActionResult Tag_Position()
        {
            var userValidate = new UserValidation();
            var urlLogin = Config.UrlRoot + "Account/FormLogin";
            if (!userValidate.IsSigned())
            {
                return Json(new { redirecturl = Config.UrlRoot + "Account/FormLogin" }, JsonRequestBehavior.AllowGet);
            }
            var checkPermission = userValidate.CheckPermissionUser("Tag", userValidate.UserId, userValidate.IsAdministrator);
            if (!checkPermission || Permission == null)
            {
                return Json(new { redirecturl = Config.UrlRoot + "common/errorpermission" }, JsonRequestBehavior.AllowGet);
            }
            var listTagGroup = AbstractDAOFactory.Instance().CreateCourseDAO().TagGroup_Get(new TagGroup() { TagGroupId = 0, TagGroupName = "", GroupType = 0 }).Where(p => p.IsActive).ToList();
            ViewBag.listTagGroup = listTagGroup;
            return PartialView();
        }


        [AuthorizationRequired]
        public ActionResult Tag_Position_Partial(bool status, int tagGroupId)
        {
            var userValidate = new UserValidation();
            var urlLogin = Config.UrlRoot + "Account/FormLogin";
            if (!userValidate.IsSigned())
            {
                return Json(new { redirecturl = Config.UrlRoot + "Account/FormLogin" }, JsonRequestBehavior.AllowGet);
            }
            var checkPermission = userValidate.CheckPermissionUser("Tag", userValidate.UserId, userValidate.IsAdministrator);
            if (!checkPermission || Permission == null)
            {
                return Json(new { redirecturl = Config.UrlRoot + "common/errorpermission" }, JsonRequestBehavior.AllowGet);
            }
            var list = AbstractDAOFactory.Instance().CreateCourseDAO().Tag_Get(new Tag() { Status = true, TagGroupId = tagGroupId }).OrderBy(p => p.Order).ToList();
            return PartialView(list);
        }

        [HttpPost]
        public JsonResult SaveOrderTag(List<NestedOrder> listOrder)
        {
            var ReturnData = new ReturnData();
            var userValidate = new UserValidation();
            if (!userValidate.IsSigned())
            {
                return Json(new { redirecturl = Config.UrlRoot + "Account/FormLogin" }, JsonRequestBehavior.AllowGet);
            }
            if (!userValidate.IsAdministrator || Permission == null || Permission.ActionName != "Tag" || !Permission.IsUpdate)
            {
                ReturnData.ResponseCode = -101;
                ReturnData.Description = "Bạn không có quyền sử dụng chức năng này";
                return Json(ReturnData);
            }
            if (listOrder.Count <= 0)
            {
                ReturnData.ResponseCode = -100;
                ReturnData.Description = "không tồn tại danh sách chức năng sắp xếp mới";
                return Json(ReturnData);
            }
            try
            {
                // function
                foreach (var t in listOrder)
                {
                    AbstractDAOFactory.Instance().CreateCourseDAO().SP_Tag_UpdateOrder(new Tag() { TagId = t.Id, Order = t.Order });
                }

                ReturnData.ResponseCode = 1;
                ReturnData.Description = "Sắp xếp Thẻ (tag) thành công";
                //Ghi log

                AbstractDAOFactory.Instance().CreateUsersLogDAO().InsertUsersLog(new UsersLog
                {
                    FunctionID = Permission.FunctionID,
                    FunctionName = Permission.FunctionName,
                    Description = ReturnData.Description,
                    UserID = userValidate.UserId,
                    ClientIP = userValidate.ClientIP
                });

            }
            catch (Exception ex)
            {
                Log4Net.PublishException(ex);
                ReturnData.ResponseCode = -99;
                ReturnData.Description = "Hệ thống đang bận. Vui lòng quay lại sau";
            }
            return Json(ReturnData);
        }


        [AuthorizationRequired]
        public ActionResult Tag_GetInfo(int id, bool status)
        {
            var userValidate = new UserValidation();
            var urlLogin = Config.UrlRoot + "Account/FormLogin";
            if (!userValidate.IsSigned())
            {
                return Json(new { redirecturl = Config.UrlRoot + "Account/FormLogin" }, JsonRequestBehavior.AllowGet);
            }
            var checkPermission = userValidate.CheckPermissionUser("Tag", userValidate.UserId, userValidate.IsAdministrator);
            if (!checkPermission || Permission == null)
            {
                return Json(new { redirecturl = Config.UrlRoot + "common/errorpermission" }, JsonRequestBehavior.AllowGet);
            }
            if (id <= 0)
            {
                return PartialView(new Tag());
            }
            var tag = AbstractDAOFactory.Instance().CreateCourseDAO().Tag_Get(new Tag() { TagId = id, Status = status }).SingleOrDefault();
            return PartialView(tag);
        }

        // Lấy danh sách các bài theo loại  -0:default, 1:Trường, 2:khóa ngắn, 3:khóa dài, 4:học bổng
        [AuthorizationRequired]
        public ActionResult Tag_LinkTarget(int tagId, string tagName)
        {
            var userValidate = new UserValidation();
            var urlLogin = Config.UrlRoot + "Account/FormLogin";
            if (!userValidate.IsSigned())
            {
                return Json(new { redirecturl = Config.UrlRoot + "Account/FormLogin" }, JsonRequestBehavior.AllowGet);
            }
            var checkPermission = userValidate.CheckPermissionUser("Tag", userValidate.UserId, userValidate.IsAdministrator);
            if (!checkPermission || Permission == null)
            {
                return Json(new { redirecturl = Config.UrlRoot + "common/errorpermission" }, JsonRequestBehavior.AllowGet);
            }
            ViewBag.tagId = tagId;
            ViewBag.TagName = tagName;
            var listTagGroup = AbstractDAOFactory.Instance().CreateCourseDAO().TagGroup_Get(new TagGroup() { Position = 99 }).Where(p => p.IsActive).ToList();
            ViewBag.listTagGroup = listTagGroup;
            return PartialView();
        }

        [HttpPost]
        [AuthorizationRequired]
        public JsonResult Tag_LinkTarget_Function(int tagGroupId, int tagId, string listTagetId)
        {
            var returnData = new ReturnData();
            var userValidate = new UserValidation();
            if (!userValidate.IsSigned())
            {
                return Json(new { redirecturl = Config.UrlRoot + "Account/FormLogin" }, JsonRequestBehavior.AllowGet);
            }
            var checkPermission = userValidate.CheckPermissionUser("Tag", userValidate.UserId, userValidate.IsAdministrator);
            if (!checkPermission || Permission == null)
            {
                returnData.ResponseCode = -101;
                returnData.Description = "Bạn không có quyền sử dụng chức năng này";
                return Json(returnData);
            }
            try
            {
                if (tagGroupId <= 0 || tagId <= 0)
                {
                    returnData.ResponseCode = -600;
                    returnData.Description = "Tham số đầu vào không hợp lệ";
                    return Json(returnData);
                }

                var result = AbstractDAOFactory.Instance().CreateCourseDAO().TagLinkedTarget_Insert(new TagLinkedTarget()
                {
                    TagGroupId = tagGroupId,
                    TagId = tagId,
                    ListTagetId = listTagetId,
                    CreatedUser = userValidate.UserName
                });
                returnData.ResponseCode = result;
                if (returnData.ResponseCode >= 0)
                {
                    returnData.Description = "Gắn thẻ bài viết thành công";
                    // Ghi log
                    AbstractDAOFactory.Instance().CreateUsersLogDAO().InsertUsersLog(new UsersLog
                    {
                        FunctionID = Permission.FunctionID,
                        FunctionName = Permission.FunctionName,
                        Description = returnData.Description + "tagGroupId :" + tagGroupId + " |tagId:" + tagId + " |listTagetId:" + listTagetId,
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
                        case -1202:
                            returnData.Description = "Nhóm thẻ không tồn tại trên hệ thống";
                            break;
                        case -1203:
                            returnData.Description = "Thẻ không tồn tại trên hệ thống";
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

        [AuthorizationRequired]
        public ActionResult Tag_LinkTarget_Partial(byte groupType, int tagGroupId, int tagId)
        {
            var userValidate = new UserValidation();
            var urlLogin = Config.UrlRoot + "Account/FormLogin";
            if (!userValidate.IsSigned())
            {
                return Json(new { redirecturl = Config.UrlRoot + "Account/FormLogin" }, JsonRequestBehavior.AllowGet);
            }
            var checkPermission = userValidate.CheckPermissionUser("Tag", userValidate.UserId, userValidate.IsAdministrator);
            if (!checkPermission || Permission == null)
            {
                return Json(new { redirecturl = Config.UrlRoot + "common/errorpermission" }, JsonRequestBehavior.AllowGet);
            }


            var listCheck = AbstractDAOFactory.Instance().CreateCourseDAO().TagLinkedTarget_Get(new TagLinkedTarget() { TagGroupId = tagGroupId, TagId = tagId }).Where(p => p.IsCheck).ToList();

            ViewBag.listEdu = "";
            ViewBag.listCourse = "";
            ViewBag.ListScho = "";

            //var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            //serializer.MaxJsonLength = Int32.MaxValue;
            if (groupType == 1) // trường
            {
                var listEdu = AbstractDAOFactory.Instance().CreateAccountDAO().Education_GetList(new Request_Education() { });
                foreach (var obj in listEdu)
                {
                    foreach (var check in listCheck)
                    { 
                        if (check.TargetId == obj.EduId && check.IsCheck)
                        {
                            obj.IsCheck = true;
                        }
                    }
                }
                //var listjson = serializer.Serialize(listEdu);
                //var listjson = Regex.Replace(JsonConvert.SerializeObject(listEdu), @"\\r\\n|\\n|\\r|\\t", "");
                ViewBag.listEdu = listEdu;
            }
            else if (groupType == 2)  // khóa ngắn
            {
                var listCourse = AbstractDAOFactory.Instance().CreateCourseDAO().GetListCourse(new CourseRequest() { CourseType = 1 });
                foreach (var obj in listCourse)
                {
                    foreach (var check in listCheck)
                    {
                        if (check.TargetId == obj.CoursesID && check.IsCheck)
                        {
                            obj.IsCheck = true;
                        }
                    }
                }
                //var listjson = serializer.Serialize(listCourse);
                //var listjson = Regex.Replace(JsonConvert.SerializeObject(listCourse), @"\\r\\n|\\n|\\r|\\t", "");
                ViewBag.listCourse = listCourse;
            }
            else if (groupType == 3)  // khóa dài
            {
                var listCourse = AbstractDAOFactory.Instance().CreateCourseDAO().GetListCourse(new CourseRequest() { CourseType = 2 });
                foreach (var obj in listCourse)
                {
                    foreach (var check in listCheck)
                    {
                        if (check.TargetId == obj.CoursesID && check.IsCheck)
                        {
                            obj.IsCheck = true;
                        }
                    }
                }
                //var listjson = serializer.Serialize(listCourse);
                //var listjson = Regex.Replace(JsonConvert.SerializeObject(listCourse), @"\\r\\n|\\n|\\r|\\t", "");
                ViewBag.listCourse = listCourse;
            }
            else // học bổng
            {
                var listScho = AbstractDAOFactory.Instance().CreateCourseDAO().Scholarship_GetPage(new Scholarship() { Type = 0, FromDate = new DateTime(), ToDate = new DateTime(), Status = 0 });
                foreach (var obj in listScho)
                {
                    foreach (var check in listCheck)
                    {
                        if (check.TargetId == obj.Id && check.IsCheck)
                        {
                            obj.IsCheck = true;
                        }
                    }
                }
                //var listjson = serializer.Serialize(listScho);
                //var listjson = Regex.Replace(JsonConvert.SerializeObject(listScho), @"\\r\\n|\\n|\\r|\\t", "");
                ViewBag.ListScho = listScho;
            }
            return PartialView();
        }

        [AuthorizationRequired]
        [HttpGet]
        public JsonResult GetTagId_ByType(byte groupType)
        {
            var returnData = new ReturnData();
            var userValidate = new UserValidation();
            if (!userValidate.IsSigned())
            {
                return Json(new { redirecturl = Config.UrlRoot + "Account/FormLogin" }, JsonRequestBehavior.AllowGet);
            }
            var list = AbstractDAOFactory.Instance().CreateCourseDAO().TagGroup_Get(new TagGroup() { IsActive = true, GroupType = groupType , Position = 99}).Where(p => p.IsActive).ToList();
            return Json(new { Data = list, Code = 0, Description = "Lấy thành công" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AuthorizationRequired]
        public JsonResult InsertUpdate_Tag(Tag request)
        {
            var returnData = new ReturnData();
            var userValidate = new UserValidation();
            if (!userValidate.IsSigned())
            {
                return Json(new { redirecturl = Config.UrlRoot + "Account/FormLogin" }, JsonRequestBehavior.AllowGet);
            }
            var checkPermission = userValidate.CheckPermissionUser("Tag", userValidate.UserId, userValidate.IsAdministrator);
            if (!checkPermission || Permission == null)
            {
                returnData.ResponseCode = -101;
                returnData.Description = "Bạn không có quyền sử dụng chức năng này";
                return Json(returnData);
            }
            try
            {
                if (string.IsNullOrEmpty(request?.TagName) || string.IsNullOrEmpty(request.TagKey))
                {
                    returnData.ResponseCode = -600;
                    returnData.Description = "Tham số đầu vào không hợp lệ. Vui lòng nhập đầy đủ thông tin";
                    return Json(returnData);
                }

                if (request.TagId <= 0)
                {
                    request.CreatedUser = userValidate.UserName;
                    var result = AbstractDAOFactory.Instance().CreateCourseDAO().Tag_Insert(request);
                    returnData.ResponseCode = result;
                }
                else
                {
                    request.CreatedUser = userValidate.UserName;
                    var result = AbstractDAOFactory.Instance().CreateCourseDAO().SP_Tag_Update(request);
                    returnData.ResponseCode = result;
                }

                if (returnData.ResponseCode >= 0)
                {
                    returnData.Description = request.TagId <= 0 ? "Tạo thành công" : "Cập nhật thành công";

                    // Ghi log
                    AbstractDAOFactory.Instance().CreateUsersLogDAO().InsertUsersLog(new UsersLog
                    {
                        FunctionID = Permission.FunctionID,
                        FunctionName = Permission.FunctionName,
                        Description = returnData.Description + "TagGroup :" + request.TagName + " |TagKey:" + request.TagKey,
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
                        case -1203:
                            returnData.Description = "Tag không tồn tại trên hệ thống";
                            break;
                        case -1204:
                            returnData.Description = "Tag đã tồn tại trên hệ thống";
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
        public JsonResult Delete_Tag(int id)
        {
            var returnData = new ReturnData();
            try
            {
                var userValidate = new UserValidation();
                if (!userValidate.IsSigned())
                {
                    return Json(new { redirecturl = Config.UrlRoot + "Account/FormLogin" }, JsonRequestBehavior.AllowGet);
                }
                var checkPermission = userValidate.CheckPermissionUser("TagGroup", userValidate.UserId, userValidate.IsAdministrator);
                if (!checkPermission || Permission == null)
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

                var result = AbstractDAOFactory.Instance().CreateCourseDAO().SP_Tag_Delete(new Tag() { TagId = id, CreatedUser = userValidate.UserName });
                returnData.ResponseCode = result;

                if (returnData.ResponseCode >= 0)
                {
                    returnData.Description = "Xóa thẻ thành công";

                    // Ghi log
                    AbstractDAOFactory.Instance().CreateUsersLogDAO().InsertUsersLog(new UsersLog
                    {
                        FunctionID = Permission.FunctionID,
                        FunctionName = Permission.FunctionName,
                        Description = returnData.Description + "TagId :" + id,
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
                        case -1203:
                            returnData.Description = "Tag không tồn tại trên hệ thống";
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