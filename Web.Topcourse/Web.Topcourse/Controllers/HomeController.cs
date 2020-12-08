using Common.Caching.CachingHelper;
using Common.Model;
using Common.Utilities.IpAddress;
using Common.Utilities.Log;
using Common.Utilities.Security;
using DataAccess.Topcourse.Factory;
using Facebook;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Security;
using Utilities.Util;
using Utility;
using VNPAY_CS_ASPX;
using Web.Topcourse.Helper;
using Web.Topcourse.Models;

namespace Web.Topcourse.Controllers
{
    public class HomeController : Controller
    {
        [Route("~/", Name = "home_index")]
        public ActionResult Index(string messeger)
        {
            ViewBag.Messenger = messeger;
            //Lay danh sach cac khoa noi bat
            var request = new CourseRequest { CourseType = 2, IsHotHomePage = true, PageSize = 12, CurrentPage = 1 };
            int totalRecord = 0;
            var listCourseHot = AbstractFactory.Instance().TopcourseDAO().GetListCourse(request, ref totalRecord);
            request.CourseType = 1;
            var listCourseHot2 = AbstractFactory.Instance().TopcourseDAO().GetListCourse(request, ref totalRecord);
            listCourseHot.AddRange(listCourseHot2);
            //Limit take 3 courses hot
            //ViewBag.ListCourseHot = listCourseHot.Take(3);
            //Take all courses hot
            ViewBag.ListCourseHot = listCourseHot;
            var listSpecilityCourse = AbstractFactory.Instance().TopcourseDAO().GetListSpecilityCourse(-1, 2).OrderBy(o => o.Name).ToList();
            ViewBag.ListSpecilityCourse = listSpecilityCourse ?? new List<SpecilityCourse>();
            var listSpecilityCourseShort = AbstractFactory.Instance().TopcourseDAO().GetListSpecilityCourse(-1, 1).OrderBy(o => o.Name).ToList();
            ViewBag.ListSpecilityCourseShort = listSpecilityCourseShort ?? new List<SpecilityCourse>();

            //Get all banner image
            var bannerImages = AbstractFactory.Instance().TopcourseDAO().GetBannerImages();
            if (bannerImages != null && bannerImages.Any())
            {
                ViewBag.BannerSliders = bannerImages.Where(x => x.Type == (int)Enums.BannerImageType.BannerSlider);
                ViewBag.BannerAds = bannerImages.Where(x => x.Type == (int)Enums.BannerImageType.BannerAds).Take(2);
                ViewBag.BannerDomestics = bannerImages.Where(x => x.Type == (int)Enums.BannerImageType.BannerDomestic).Take(3);
                ViewBag.BannerForeigns = bannerImages.Where(x => x.Type == (int)Enums.BannerImageType.BannerForeign).Take(3);
                ViewBag.BannerAbroads = bannerImages.Where(x => x.Type == (int)Enums.BannerImageType.BannerAbroad).Take(3);
            }
            ViewBag.BannerImages = bannerImages ?? new List<BannerImage>();
            return View();
        }
        [Route("~/hoc-vien")]
        public ActionResult Trainees()
        {
            if (Request.IsAjaxRequest())
                return PartialView();
            else
                return View();
        }
        [Route("~/cam-on")]
        public ActionResult thanks()
        {
            if (Request.IsAjaxRequest())
                return PartialView();
            else
                return View();
        }

        [Route("~/doi-tac")]
        public ActionResult Partner()
        {
            ViewBag.Message = "Your application description page.";
            if (Request.IsAjaxRequest())
                return PartialView();
            else
                return View();
        }

        [Route("~/bai-viet")]
        public ActionResult PostAllPading()
        {
            return View();
        }
        [Route("~/bai-viet/{alias}-{id}")]
        public ActionResult PostDetail(int id)
        {
            var dao = AbstractFactory.Instance().TopcourseDAO();
            var model = dao.PostDetail(id);
            ViewBag.Related = dao.GetPostRelated(id);
            return View(model);
        }
        [HttpGet]
        public JsonResult LoadData(int page, int pageSize)
        {
            int totalRecord = 0;
            var dao = AbstractFactory.Instance().TopcourseDAO();
            var request = new PostRequest()
            {
                PageIndex = page,
                PageSize = pageSize
            };
            var listPost = dao.GetListPost(request, ref totalRecord);
            return Json(new
            {
                data = listPost,
                totalRow = totalRecord,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }
        [Route("~/gioi-thieu")]
        [HttpGet]
        public ActionResult Information()
        {
            ViewBag.Message = "Your application description page.";
            if (Request.IsAjaxRequest())
                return PartialView();
            else
                return View();
        }

        [Route("~/dang-ky-doi-tac")]
        public ActionResult RegisterPartner()
        {
            return View();
        }

        [Route("~/danh-sach-truong")]
        public ActionResult ListSchool(RequestGetSchool request)
        {
            var dao = AbstractFactory.Instance().TopcourseDAO();
            request.ListTagID = string.IsNullOrEmpty(request.ListTagID) ? null : request.ListTagID;
            request.CurrentPage = 1;
            request.PageSize = 1000;
            int totalRecord = 0;
            ViewBag.Request = request;
            var listSchool = dao.GetListSchool(request, ref totalRecord);
            if (listSchool != null && listSchool.Any())
            {
                var listSearchSchool = new List<SearchShool>();
                foreach (var item in listSchool)
                {
                    var courseOfSchool = dao.GetCourseBySchool(item.EduId, 2, null);
                    listSearchSchool.Add(new SearchShool { School = item, Courses = courseOfSchool });
                }

                var requestGetTag = new RequestGetTag { GroupType = (int)Enums.TagGroupType.School };
                var listSchoolTag = dao.GetListTag(requestGetTag);
                if (listSchoolTag != null && listSchoolTag.Any())
                {
                    var listTagGroup = new List<TagGroup>();
                    foreach (var item in listSchoolTag)
                    {
                        if (!listTagGroup.Exists(o => o.TagGroupId == item.TagGroupId))
                        {
                            var listTag = new List<Tag>();
                            var tagGroup = new TagGroup();
                            tagGroup.TagGroupId = item.TagGroupId;
                            tagGroup.TagGroupName = item.TagGroupName;
                            listTag.Add(new Tag { TagId = item.TagId, TagKey = item.TagKey, TagName = item.TagName });
                            tagGroup.Tags = listTag;
                            listTagGroup.Add(tagGroup);
                        }
                        else
                        {
                            var objectTagGroup = listTagGroup.FirstOrDefault(o => o.TagGroupId == item.TagGroupId);
                            objectTagGroup.Tags.Add(new Tag { TagId = item.TagId, TagKey = item.TagKey, TagName = item.TagName });
                        }
                    }
                    ViewBag.ListSchoolTag = listTagGroup;
                }
                ViewBag.TotalRecord = totalRecord;
                if (Request.IsAjaxRequest())
                    return PartialView(listSearchSchool);
                else
                    return View(listSearchSchool);
            }
            ViewBag.TotalRecord = 0;
            if (Request.IsAjaxRequest())
                return PartialView(new List<SearchShool>());
            else
                return View(new List<SearchShool>());
        }
        public ActionResult ListSchoolData(RequestGetSchool request)
        {
            var dao = AbstractFactory.Instance().TopcourseDAO();
            request.ListTagID = string.IsNullOrEmpty(request.ListTagID) ? null : request.ListTagID;
            request.PageSize = 12;
            int totalRecord = 0;
            var listSchool = dao.GetListSchool(request, ref totalRecord);
            if (listSchool != null && listSchool.Any())
            {
                var listSearchSchool = new List<SearchShool>();
                foreach (var item in listSchool)
                {
                    var courseOfSchool = dao.GetCourseBySchool(item.EduId, 2, null);
                    listSearchSchool.Add(new SearchShool { School = item, Courses = courseOfSchool });
                }
                ViewBag.TotalRecord = totalRecord;
                return PartialView(listSearchSchool);
            }
            ViewBag.TotalRecord = 0;
            return PartialView(new List<SearchShool>());
        }

        [Route("~/chi-tiet-truong/{name}-{id}")]
        public ActionResult SchoolDetail(int id)
        {
            var dao = AbstractFactory.Instance().TopcourseDAO();
            var school = dao.GetSchoolDetail(id, string.Empty);

            if (school != null && school.EduId > 0)
            {
                if (!string.IsNullOrEmpty(school.SlideImage))
                {
                    school.ListSlide = school.SlideImage.Split('|').ToList();
                }
                var courseOfSchool = dao.GetCourseBySchool(id, 100, null);
                var schoolInfo = new SearchShool { School = school, Courses = courseOfSchool };
                ViewBag.TotalRecord = courseOfSchool.Count;

                var listComment = AbstractFactory.Instance().TopcourseDAO().GetListComment(new RequestComment { TargetID = id, Type = 1, ParentID = -1 });
                ViewBag.ListComment = (listComment == null || !listComment.Any()) ? new List<CommentData>() : listComment;

                var listSchoolSimilar = AbstractFactory.Instance().TopcourseDAO().GetListSchoolSimilar(id);
                ViewBag.ListSchoolSimilar = listSchoolSimilar;

                var listSpecilityCourse = dao.GetListSpecilityCourse(-1, 2);
                ViewBag.ListSpecilityCourse = listSpecilityCourse ?? new List<SpecilityCourse>();
                return View(schoolInfo);
            }

            return View(new SearchShool());
        }

        [Route("~/danh-sach-hoc-bong")]
        public ActionResult ListScholarship(RequestScholarship request)
        {
            int totalRecord = 0;
            request.CurrentPage = 1;
            request.PageSize = 4;
            var listScholarship = AbstractFactory.Instance().TopcourseDAO().GetListScholarship(request, ref totalRecord);
            if (listScholarship != null && listScholarship.Any())
            {
                ViewBag.TotalRecord = totalRecord;
                var requestGetTag = new RequestGetTag { GroupType = (int)Enums.TagGroupType.Sholarship };
                var listScholarshipTag = AbstractFactory.Instance().TopcourseDAO().GetListTag(requestGetTag);
                if (listScholarshipTag != null && listScholarshipTag.Any())
                {
                    var listTagGroup = new List<TagGroup>();
                    foreach (var item in listScholarshipTag)
                    {
                        if (!listTagGroup.Exists(o => o.TagGroupId == item.TagGroupId))
                        {
                            var listTag = new List<Tag>();
                            var tagGroup = new TagGroup();
                            tagGroup.TagGroupId = item.TagGroupId;
                            tagGroup.TagGroupName = item.TagGroupName;
                            listTag.Add(new Tag { TagId = item.TagId, TagKey = item.TagKey, TagName = item.TagName });
                            tagGroup.Tags = listTag;
                            listTagGroup.Add(tagGroup);
                        }
                        else
                        {
                            var objectTagGroup = listTagGroup.FirstOrDefault(o => o.TagGroupId == item.TagGroupId);
                            objectTagGroup.Tags.Add(new Tag { TagId = item.TagId, TagKey = item.TagKey, TagName = item.TagName });
                        }
                    }
                    ViewBag.ListTag = listTagGroup;
                }
            }
            return View(listScholarship);
        }
        public ActionResult ListScholarshipData(RequestScholarship request)
        {
            int totalRecord = 0;
            request.CurrentPage = 1;
            request.PageSize = 12;
            var listScholarship = AbstractFactory.Instance().TopcourseDAO().GetListScholarship(request, ref totalRecord);
            ViewBag.TotalRecord = totalRecord;
            return PartialView(listScholarship);
        }

        [Route("~/chi-tiet-hoc-bong/{name}-{id}")]
        public ActionResult Scholarship_Detail(int id)
        {
            var dao = AbstractFactory.Instance().TopcourseDAO();
            var scholarship = dao.GetScholarshipDetail(id);

            if (scholarship != null && scholarship.Id > 0)
            {
                var userId = SessionAccount.UserID;
                if (userId > 0)
                {
                    var log = new CacheService().GetLog(userId);
                    ViewBag.LogsView = (log != null && log.Logs.Count > 0) ? log.Logs : null;
                }
                var listComment = dao.GetListComment(new RequestComment { TargetID = id, Type = 3, ParentID = -1 });
                ViewBag.ListComment = (listComment == null || !listComment.Any()) ? new List<CommentData>() : listComment;

                return View(scholarship);
            }
            return View(new Scholarship());
        }


        [Route("~/tuyen-dung")]
        public ActionResult Recruitment()
        {
            return View();
        }

        [HttpPost]
        public JsonResult RegisterPartnerPost(RequestRegisterEdu request)
        {
            request.ClientIP = IPAddressHelper.GetClientIP();
            if (GlobalCachingProvider.Instance.GetItemNotRemove(request.ClientIP) != null)
            {
                return Json(-1001);
            }
            var result = AbstractFactory.Instance().TopcourseDAO().RegisterEducation(request);
            if (result > 0)
            {
                GlobalCachingProvider.Instance.AddItemExpired(request.ClientIP, result, DateTimeOffset.UtcNow.AddMinutes(1));
            }
            return Json(result);
        }

        public ActionResult AddCourse(int index)
        {
            ViewBag.CourseIndex = index;
            return PartialView();
        }

        [Route("~/kich-hoat-tai-khoan")]
        public ActionResult ActiveAccount(string activecode, string data, string sign)
        {
            if (string.IsNullOrEmpty(activecode) || string.IsNullOrEmpty(data) || string.IsNullOrEmpty(sign))
            {
                NLogManager.LogMessage($"request data invalid>>data:{data}>>sign:{sign}");
                return RedirectToAction("Index", new { messeger = "Kích hoạt tài khoản thất bại, dữ liệu không hợp lệ" });
            }
            data = data.Replace(" ", "+");
            string dataPlainText = Security.AESDecryptString(data, ConfigurationManager.AppSettings["SECURE_KEY"]);
            var arrayData = dataPlainText.Split('|');
            if (arrayData.Length != 3)
            {
                NLogManager.LogMessage($"arrayData length invalid>>dataPlainText:{dataPlainText}>>sign:{sign}");
                return RedirectToAction("Index", new { messeger = "Kích hoạt tài khoản thất bại, dữ liệu không hợp lệ" });
            }

            if (arrayData[0] != activecode)
            {
                NLogManager.LogMessage($"arrayData[0]!= activecode>>dataPlainText:{dataPlainText}>>sign:{sign}");
                return RedirectToAction("Index", new { messeger = "Kích hoạt tài khoản thất bại, mã kích hoạt không đúng" });
            }
            //if((DateTime.Now - new DateTime(Convert.ToInt64(arrayData[1]))).TotalSeconds > 86400)
            //{
            //    NLogManager.LogMessage($"arrayData[0]!= activecode>>dataPlainText:{dataPlainText}>>sign:{sign}");
            //    return RedirectToAction("Index", new { messeger = "Kích hoạt tài khoản thất bại, mã kích hoạt không đúng" });
            //}
            if (sign != Security.MD5Encrypt(dataPlainText + "djtk0nmeckungm4y"))
            {
                NLogManager.LogMessage($"sign invalid:{dataPlainText}>>sign:{sign}");
                return RedirectToAction("Index", new { messeger = "Kích hoạt tài khoản thất bại, chữ ký không đúng" });
            }
            var requestActive = new ProfileStudent
            {
                Username = arrayData[2]
            };
            var result = AbstractFactory.Instance().TopcourseDAO().ConfirmUser(requestActive);
            if (result < 0)
            {
                NLogManager.LogMessage($"ConfirmUser fail>>result:{result}");
                if (result == -52)
                    return RedirectToAction("Index", new { messeger = "Kích hoạt tài khoản thất bại, trạng thái tài khoản không hợp lệ" });
                else if (result == -50)
                    return RedirectToAction("Index", new { messeger = "Kích hoạt tài khoản thất bại, tài khoản không tồn tại" });
                return RedirectToAction("Index", new { messeger = "Kích hoạt tài khoản thất bại" });
            }
            return RedirectToAction("Index", new { messeger = "Kích hoạt tài khoản thành công" });
        }

        [HttpPost]
        public JsonResult RegisterStudent(ProfileStudent request)
        {
            if (string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password) || string.IsNullOrEmpty(request.Mobile) || string.IsNullOrEmpty(request.CaptchaResponse))
            {
                NLogManager.LogMessage($"request invalid >> requestData: {JsonConvert.SerializeObject(request)}");
                return Json(new ResponseData { ResponseCode = CommonError.INVALID_DATA, Description = "Du lieu dau vao ko hop le" });
            }
            //Check gcaptcha
            if (!Utils.IsReCaptchValid(request.CaptchaResponse))
            {
                return Json(new ResponseData { ResponseCode = CommonError.CAPTCHA_INVALID, Description = "Captcha ko hop le" });
            }
            request.RegisterType = (int)Enums.RegisterType.Normal;
            request.ClientIP = IPAddressHelper.GetClientIP();
            request.Password = Security.MD5Encrypt($"{request.Username}{request.Password}");

            string userProfile = string.Empty;
            if (!string.IsNullOrEmpty(request.Firstname))
                userProfile += $"Firstname#0#{Utils.DbFormatString(request.Firstname)}|";
            if (!string.IsNullOrEmpty(request.Lastname))
                userProfile += $"Lastname#0#{Utils.DbFormatString(request.Lastname)}";

            request.UserProfileInfo = userProfile;
            request.FullName = request.Lastname + " " + request.Firstname;
            var result = AbstractFactory.Instance().TopcourseDAO().RegisterStudent(request);
            if (result < 0)
                return Json(new ResponseData { ResponseCode = CommonError.GetErrorCode((int)result), Description = "Dang ky that bai" });

            string activeCode = Utils.GenerateRandomNumber(10);
            var dataPlainText = activeCode + "|" + DateTime.Now.Ticks + "|" + request.Username;
            var secureKey = ConfigurationManager.AppSettings["SECURE_KEY"];
            var dataEncrypt = Security.AESEncryptString(dataPlainText, secureKey);
            var sign = Security.MD5Encrypt(dataPlainText + "djtk0nmeckungm4y");
            var linkActive = $"{Utils.Domain}kich-hoat-tai-khoan?activecode={activeCode}&data={HttpUtility.UrlEncode(dataEncrypt)}&sign={sign}";
            StringBuilder content = new StringBuilder();

            content.Append($"Chúc mừng bạn đã đăng ký tài khoản thành công, vui lòng click vào link phía dưới để kích hoạt tài khoản:<br/><br/>");
            content.Append(linkActive);
            string contentHTML = content.ToString();

            var requestSendMail = new MailService.EmailModel
            {
                ToMail = request.Username,
                Title = "[EDUNET]Kích hoạt tài khoản",
                IsResend = false,
                ServiceID = (int)Enums.MailServiceID.SendMailActive,
                Content = contentHTML,
                Sign = Security.MD5Encrypt($"{request.Username}{contentHTML[0]}{secureKey}") //{requestSendMail.ToMail}{requestSendMail.Param}{requestSendMail.MailID}{secretKey}
            };
            var resultSendMail = new MailService.SendMailServiceClient().SendEmailManual(requestSendMail);
            return Json(new ResponseData { ResponseCode = CommonError.DONE, Description = "Dang ky thanh cong" });
        }

        [HttpPost]
        public JsonResult UserAuthen(UserAuthen request)
        {
            if (string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
            {
                NLogManager.LogMessage($"request invalid >> requestData: {JsonConvert.SerializeObject(request)}");
                return Json(new ResponseData { ResponseCode = CommonError.INVALID_DATA, Description = "Du lieu dau vao ko hop le" });
            }

            request.ClientIP = IPAddressHelper.GetClientIP();
            request.Salt = DateTime.Now.Ticks.ToString();
            request.Password = Security.MD5Encrypt(Security.MD5Encrypt($"{request.Username}{request.Password}") + request.Salt);

            var result = AbstractFactory.Instance().TopcourseDAO().UserAuthen(request);
            if (result > 0)
            {
                var basicInfo = AbstractFactory.Instance().TopcourseDAO().GetBasicInfo(request.Username, 0);
                if (basicInfo != null && basicInfo.UserID > 0)
                {
                    string cookieUsername = SessionAccount.SessionName(result, request.Username, request.ClientIP, basicInfo.Fullname, basicInfo.UserType, DateTime.Now.Ticks);
                    FormsAuthentication.SetAuthCookie(cookieUsername, true);
                    return Json(new ResponseData { ResponseCode = CommonError.DONE, Description = "Dang nhap thanh cong" });
                }
            }
            NLogManager.LogMessage($"Authen fail>> user: {request.Username}>>result:{result}");
            if (result == -33)
            {

            }
            return Json(new ResponseData { ResponseCode = CommonError.GetErrorCode((int)result), Description = "Dang nhap that bai" });
        }

        [Authorize]
        [Route("~/thong-tin-tai-khoan")]
        public ActionResult AccountInfo()
        {
            var userID = SessionAccount.UserID;
            var userInfo = AbstractFactory.Instance().TopcourseDAO().GetFullInfo(string.Empty, userID);
            var avatarDirectory = $"{ConfigurationManager.AppSettings["MediaImageUploadDirectory"]}Avatar\\{userID}\\" + Security.MD5Encrypt($"{userID}_avatar") + ".png";
            if (!System.IO.File.Exists(avatarDirectory))
                userInfo.Avatar = $"{Utils.Domain}Content/images/TopCourses_logo_color.png";
            else
                userInfo.Avatar = $"{Utils.Domain}Upload/Avatar/{userID}/" + Security.MD5Encrypt($"{userID}_avatar") + ".png";

            var userType = userInfo.UserType;
            ViewBag.UserType = userType;
            //Lay log cac khoa hoc da xem
            var log = new CacheService().GetLog(userID);
            ViewBag.LogsView = (log != null && log.Logs.Count > 0) ? log.Logs : null;
            if (userType == 1)
            {
                //Lay thong tin thanh toan cua user
                int totalRecord = 0;
                var listPayment = AbstractFactory.Instance().TopcourseDAO().GetListOrder(new RequestGetOrder { UserID = userID, Stautus = 99, PageSize = 1000, CurrentPage = 1 }, ref totalRecord);
                ViewBag.ListPayment = listPayment;
                ViewBag.TotalRecord = totalRecord;
            }
            else
            {
                //Lay thong tin thanh toan cua user, sau nay se sua lai
                int totalRecord = 0;
                var listPayment = AbstractFactory.Instance().TopcourseDAO().GetListOrder(new RequestGetOrder { UserID = userID, Stautus = 99, PageSize = 1000, CurrentPage = 1 }, ref totalRecord);
                ViewBag.ListPayment = listPayment;
                ViewBag.TotalRecord = totalRecord;
            }

            return View(userInfo);
        }

        [Authorize]
        [HttpPost]
        public JsonResult UpdateProfile(UserInfoUpdate request)
        {
            if (request == null || string.IsNullOrEmpty(request.Fullname) || string.IsNullOrEmpty(request.Gender) ||
                string.IsNullOrEmpty(request.Mobile) || string.IsNullOrEmpty(request.Address))
            {
                NLogManager.LogMessage($"request invalid >> requestData: {JsonConvert.SerializeObject(request)}");
                return Json(new ResponseData { ResponseCode = CommonError.INVALID_DATA, Description = "Du lieu dau vao ko hop le" });
            }
            var partOfName = StringUtility.SplitFullName(request.Fullname);
            string userProfileInfo = string.Empty;
            userProfileInfo += $"Gender#0#{Utils.DbFormatString(request.Gender)}|Address#0#{Utils.DbFormatString(request.Address)}|" +
                $"Firstname#0#{Utils.DbFormatString(partOfName[1])}|Lastname#0#{Utils.DbFormatString(partOfName[0])}";

            request.UserID = SessionAccount.UserID;
            request.UserProfileInfo = userProfileInfo;
            request.ClientIP = IPAddressHelper.GetClientIP();
            request.CreatedUser = SessionAccount.UserName;
            //Get basic xem có email chưa, chưa có thì mới gán

            var basicInfo = AbstractFactory.Instance().TopcourseDAO().GetBasicInfo(string.Empty, request.UserID);
            if (!string.IsNullOrEmpty(basicInfo.Email))
                request.Email = string.Empty;

            var result = AbstractFactory.Instance().TopcourseDAO().UpdateProfile(request);
            if (result < 0)
            {
                NLogManager.LogMessage($"UpdateProfile fail >> requestData: {JsonConvert.SerializeObject(request)} >> result: {result}");
                return Json(new ResponseData { ResponseCode = CommonError.GetErrorCode(result), Description = "Cap nhat that bai" });
            }
            return Json(new ResponseData { ResponseCode = CommonError.DONE, Description = "Cap nhat thanh cong" });
        }

        [Authorize]
        [HttpPost]
        public JsonResult UploadAvatar(UserInfoUpdate request)
        {
            try
            {
                if (request == null || string.IsNullOrEmpty(request.Avatar))
                {
                    NLogManager.LogMessage($"request invalid >> requestData: {JsonConvert.SerializeObject(request)}");
                    return Json(new ResponseData { ResponseCode = CommonError.INVALID_DATA, Description = "Du lieu dau vao ko hop le" });
                }

                //Xu ly luu anh
                var userID = SessionAccount.UserID;

                var avatarDirectory = $"{ConfigurationManager.AppSettings["MediaImageUploadDirectory"]}Avatar\\{userID}\\";
                if (!Directory.Exists(avatarDirectory))
                    Directory.CreateDirectory(avatarDirectory);

                byte[] imageByte = Convert.FromBase64String(request.Avatar);
                var size = imageByte.Length;
                MemoryStream ms = new MemoryStream(imageByte, 0, imageByte.Length);
                ms.Write(imageByte, 0, imageByte.Length);
                System.Drawing.Image img = System.Drawing.Image.FromStream(ms, true);
                //Them prefix de chong do`
                var prefixFileName = Security.MD5Encrypt($"{userID}_avatar") + ".png";
                var filePath = $"{avatarDirectory}{prefixFileName}";
                img.Save(filePath);
                AbstractFactory.Instance().TopcourseDAO().UpdateAvatar(new UpdateAvatar { UserID = userID, Avatar = prefixFileName, ClientIP = IPAddressHelper.GetClientIP() });
                return Json(new ResponseData { ResponseCode = CommonError.DONE, Description = "Cap nhat thanh cong" });
            }
            catch (Exception ex)
            {
                NLogManager.PublishException(ex);
                return Json(new ResponseData { ResponseCode = CommonError.SYSTEM_ERROR, Description = "Loi he thong" });
            }

        }


        [HttpPost]
        public JsonResult SendRequestChangePassword(ProfileStudent request)
        {
            if (string.IsNullOrEmpty(request.Username))
            {
                NLogManager.LogMessage($"request invalid >> requestData: {JsonConvert.SerializeObject(request)}");
                return Json(new ResponseData { ResponseCode = CommonError.INVALID_DATA, Description = "Du lieu dau vao ko hop le" });
            }
            //Get basic info
            var basicInfo = AbstractFactory.Instance().TopcourseDAO().GetBasicInfo(request.Username, 0);
            if (basicInfo == null || basicInfo.UserID <= 0)
            {
                NLogManager.LogMessage($"account not exist >> requestData: {JsonConvert.SerializeObject(request)} >> basicInfo: {JsonConvert.SerializeObject(basicInfo)}");
                return Json(new ResponseData { ResponseCode = CommonError.USER_NOT_EXIST, Description = "Khong tim thay thong tin tai khoan" });
            }
            //Tien hanh gui mail doi mat khau
            string secureCode = Utils.GenerateRandomNumber(10);
            var dataPlainText = secureCode + "|" + DateTime.Now.Ticks + "|" + basicInfo.Username;
            var secureKey = ConfigurationManager.AppSettings["SECURE_KEY"];
            var dataEncrypt = Security.AESEncryptString(dataPlainText, secureKey);
            var sign = Security.MD5Encrypt(dataPlainText + "djtk0nmeckungm4y");
            var linkActive = $"{Utils.Domain}quen-mat-khau?securecode={secureCode}&data={HttpUtility.UrlEncode(dataEncrypt)}&sign={sign}";
            var linkActiveHref = "<a href='" + linkActive + "'>" + linkActive + "<a>";
            StringBuilder content = new StringBuilder();

            content.Append($"Vui lòng click vào link phía dưới để cập nhật mật khẩu mới:<br/>");
            content.Append(linkActiveHref);
            string contentHTML = content.ToString();

            var requestSendMail = new MailService.EmailModel
            {
                ToMail = request.Username,
                Title = "[EDUNET]Lấy lại mật khẩu",
                IsResend = false,
                ServiceID = (int)Enums.MailServiceID.SendMailDefault,
                Content = contentHTML,
                Sign = Security.MD5Encrypt($"{request.Username}{contentHTML[0]}{secureKey}") //{requestSendMail.ToMail}{requestSendMail.Param}{requestSendMail.MailID}{secretKey}
            };
            var resultSendMail = new MailService.SendMailServiceClient().SendEmailManual(requestSendMail);
            //Cache data, cho phep 1 user su dung 1 lan
            if (resultSendMail.ResponseCode >= 0)
            {
                GlobalCachingProvider.Instance.AddItemExpired($"{request.Username}_{secureCode}", "forget_password", DateTimeOffset.UtcNow.AddHours(24));
            }
            return Json(new ResponseData { ResponseCode = CommonError.DONE, Description = "Gui yeu cau thanh cong" });
        }

        [Route("~/quen-mat-khau")]
        public ActionResult ForgetPassword(string securecode, string data, string sign)
        {
            if (Request.IsAjaxRequest())
                return PartialView();
            else
                return View();
        }

        [HttpPost]
        public JsonResult ChangePasswordForgetAction(string securecode, string data, string sign, string password)
        {
            var combineData = $"{securecode}|{data}|{sign}";
            if (string.IsNullOrEmpty(securecode) || string.IsNullOrEmpty(data) || string.IsNullOrEmpty(sign) || string.IsNullOrEmpty(password))
            {
                NLogManager.LogMessage($"request data invalid>>data:{combineData}");
                return Json(new ResponseData { ResponseCode = CommonError.INVALID_DATA, Description = "Du lieu dau vao khong hop le" });
            }
            data = data.Replace(" ", "+");
            string dataPlainText = Security.AESDecryptString(data, ConfigurationManager.AppSettings["SECURE_KEY"]);
            var arrayData = dataPlainText.Split('|');
            if (arrayData.Length != 3)
            {
                NLogManager.LogMessage($"arrayData.Length != 3>>data:{combineData}");
                return Json(new ResponseData { ResponseCode = CommonError.INVALID_DATA, Description = "Du lieu dau vao khong hop le" });
            }

            if (arrayData[0] != securecode)
            {
                NLogManager.LogMessage($"arrayData[0] != securecode>>data:{combineData}");
                return Json(new ResponseData { ResponseCode = CommonError.SECURECODE_INVALID, Description = "Ma bao mat khong chinh xac" });
            }

            if (sign != Security.MD5Encrypt(dataPlainText + "djtk0nmeckungm4y"))
            {
                NLogManager.LogMessage($"sign != Security.MD5Encrypt>>data:{combineData}");
                return Json(new ResponseData { ResponseCode = CommonError.SIGN_INVALID, Description = "Chu ky ko hop le" });
            }
            //Get basic info
            var basicInfo = AbstractFactory.Instance().TopcourseDAO().GetBasicInfo(arrayData[2], 0);
            if (basicInfo == null || basicInfo.UserID <= 0)
            {
                NLogManager.LogMessage($"account not exist >> data:{combineData} >> basicInfo: {JsonConvert.SerializeObject(basicInfo)}");
                return Json(new ResponseData { ResponseCode = CommonError.USER_NOT_EXIST, Description = "Khong tim thay thong tin tai khoan" });
            }

            //Lay data trong cache
            var cacheForgetPass = GlobalCachingProvider.Instance.GetItemNotRemove($"{basicInfo.Username}_{securecode}");
            if (cacheForgetPass == null)
                return Json(new ResponseData { ResponseCode = CommonError.DATA_EXPIRE, Description = "Du lieu khong hop le hoac da het han" });

            var requestUpdate = new UpdatePassword
            {
                UserID = basicInfo.UserID,
                LogType = (byte)Enums.ChangePassType.ResetPassByEmail,
                CreatedUser = basicInfo.Username,
                PasswordNew = Security.MD5Encrypt($"{basicInfo.Username}{password}"),
                ClientIP = IPAddressHelper.GetClientIP()
            };
            var result = AbstractFactory.Instance().TopcourseDAO().UpdatePassword(requestUpdate);
            if (result < 0)
            {
                NLogManager.LogMessage($"UpdatePassword fail>>result:{result}");
                return Json(new ResponseData { ResponseCode = CommonError.GetErrorCode(result), Description = "Cap nhat mat khau that bai, loi db: " + result });
            }
            GlobalCachingProvider.Instance.RemoveItem($"{basicInfo.Username}_{securecode}");
            return Json(new ResponseData { ResponseCode = CommonError.DONE, Description = "Cap nhat mat khau thanh cong" });
        }

        [HttpPost]
        [Authorize]
        public JsonResult ChangePasswordAction(string password, string newPassword)
        {
            if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(newPassword))
            {
                NLogManager.LogMessage($"request data invalid");
                return Json(new ResponseData { ResponseCode = CommonError.INVALID_DATA, Description = "Du lieu dau vao khong hop le" });
            }
            string userName = SessionAccount.UserName;
            var requestUpdate = new UpdatePassword
            {
                UserID = SessionAccount.UserID,
                LogType = (byte)Enums.ChangePassType.ChangePass,
                CreatedUser = userName,
                Password = Security.MD5Encrypt($"{userName}{password}"),
                PasswordNew = Security.MD5Encrypt($"{userName}{newPassword}"),
                ClientIP = IPAddressHelper.GetClientIP()
            };
            var result = AbstractFactory.Instance().TopcourseDAO().UpdatePassword(requestUpdate);
            if (result < 0)
            {
                NLogManager.LogMessage($"ChangePassword fail>>result:{result}");
                return Json(new ResponseData { ResponseCode = CommonError.GetErrorCode(result), Description = "Cap nhat mat khau that bai, loi db: " + result });
            }
            return Json(new ResponseData { ResponseCode = CommonError.DONE, Description = "Cap nhat mat khau thanh cong" });
        }

        [HttpGet]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            CookieUtility.RemoveAllCookie();
            return RedirectToRoute("home_index");
        }


        #region Ham xu ly fb login
        private Uri RediredtUri

        {
            get

            {
                var uriBuilder = new UriBuilder(Request.Url);

                uriBuilder.Query = null;

                uriBuilder.Fragment = null;

                uriBuilder.Path = Url.Action("FacebookCallback");

                return uriBuilder.Uri;
            }

        }

        [HttpGet]
        public JsonResult Facebook()
        {

            var fb = new FacebookClient();
            var loginUrl = fb.GetLoginUrl(new

            {
                client_id = ConfigurationManager.AppSettings["FACEBOOK_APP_ID"],

                client_secret = ConfigurationManager.AppSettings["FACEBOOK_APP_KEY"],

                redirect_uri = RediredtUri.AbsoluteUri,

                response_type = "code",

                scope = "email"

            });

            return Json(new { AuthenUrl = loginUrl.AbsoluteUri }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult FacebookCallback(string code)

        {

            var fb = new FacebookClient();

            dynamic result = fb.Post("oauth/access_token", new
            {
                client_id = ConfigurationManager.AppSettings["FACEBOOK_APP_ID"],

                client_secret = ConfigurationManager.AppSettings["FACEBOOK_APP_KEY"],

                redirect_uri = RediredtUri.AbsoluteUri,

                code = code

            });

            var accessToken = result.access_token;

            fb.AccessToken = accessToken;

            dynamic me = fb.Get("me?fields=id,name,first_name,last_name,email");

            string id = me.id;

            //Lay thong tin gan ket
            var requestGetAssociateUser = new OAuthenAccount
            {
                OAuthAccount = id,
                OAuthSystemID = (int)Enums.OAuthSystemID.Facebook
            };
            var oathAccount = AbstractFactory.Instance().TopcourseDAO().GetAssociateUser(requestGetAssociateUser);
            if (oathAccount != null && oathAccount.OAuthID == -969)
            {
                NLogManager.LogMessage($"Exception, ko xu ly gi ca >> request: {JsonConvert.SerializeObject(requestGetAssociateUser)} >> response:{JsonConvert.SerializeObject(oathAccount)}");
                return RedirectToAction("Index", new { messeger = "Đăng nhập thất bại" });
            }
            //Chua gan ket
            if (oathAccount == null || oathAccount.OAuthID <= 0)
            {
                int userID = 0;
                string userName = string.Empty;
                string fullName = string.Empty;
                string clientIP = IPAddressHelper.GetClientIP();

                //Goi dang ky
                string randomPassword = Guid.NewGuid().ToString();
                string userProfile = string.Empty;
                if (!string.IsNullOrEmpty(me.first_name))
                    userProfile += $"Firstname#0#{Utils.DbFormatString(me.first_name)}";
                if (!string.IsNullOrEmpty(me.last_name))
                    userProfile += (string.IsNullOrEmpty(userProfile) ? $"Lastname#0#{Utils.DbFormatString(me.last_name)}" : $"|Lastname#0#{Utils.DbFormatString(me.last_name)}");

                var requestRegStudent = new ProfileStudent
                {
                    Username = $"{id}@facebook.com",
                    Password = Security.MD5Encrypt($"{id}@facebook.com{randomPassword}"),
                    ClientIP = clientIP,
                    RegisterType = (int)Enums.RegisterType.OAuthen,
                    FullName = me.name,
                    UserProfileInfo = userProfile
                };
                var resultReg = AbstractFactory.Instance().TopcourseDAO().RegisterStudent(requestRegStudent);
                if (resultReg < 0)
                {
                    NLogManager.LogMessage($"Tu dong dang ky oauth that bai>>request:{JsonConvert.SerializeObject(requestRegStudent)} >> result: {resultReg}");
                    return RedirectToAction("Index", new { messeger = "Đăng nhập thất bại" });
                }

                userID = resultReg;
                userName = requestRegStudent.Username;
                fullName = requestRegStudent.FullName;
                //Goi gan ket
                requestGetAssociateUser.UserID = userID;
                requestGetAssociateUser.ClientIP = clientIP;
                var resultAssociateAccount = AbstractFactory.Instance().TopcourseDAO().CreateAssociateUser(requestGetAssociateUser);
                if (resultAssociateAccount < 0)
                {
                    NLogManager.LogMessage($"CreateAssociateUser that bai>>request:{JsonConvert.SerializeObject(requestGetAssociateUser)} >> result: {resultAssociateAccount}");
                    return RedirectToAction("Index", new { messeger = "Đăng nhập thất bại" });
                }
                string cookieUsername = SessionAccount.SessionName(userID, userName, clientIP, fullName, 1, DateTime.Now.Ticks);
                FormsAuthentication.SetAuthCookie(cookieUsername, true);
                return RedirectToAction("Index");
            }
            //Da gan ket
            else
            {
                //Lay basic info theo userid
                var basicInfo = AbstractFactory.Instance().TopcourseDAO().GetBasicInfo(string.Empty, oathAccount.UserID);
                if (basicInfo == null || basicInfo.UserID <= 0)
                {
                    NLogManager.LogMessage($"GetBasicInfo da gan ket that bai>>UserID:{oathAccount.UserID} >> response: {basicInfo}");
                    return RedirectToAction("Index", new { messeger = "Đăng nhập thất bại" });
                }
                string cookieUsername = SessionAccount.SessionName(basicInfo.UserID, basicInfo.Username, IPAddressHelper.GetClientIP(), basicInfo.Fullname, 1, DateTime.Now.Ticks);
                FormsAuthentication.SetAuthCookie(cookieUsername, true);
                return RedirectToAction("Index");
            }
        }


        [Route("~/tim-kiem")]
        public ActionResult Search(CourseRequest request)
        {
            int totalRecord = 0;
            //string cookieSearch = string.Empty;
            //if (Request.Cookies["_cookieSearchCourse"] != null)
            //{
            //    cookieSearch = Request.Cookies["_cookieSearchCourse"].Value;
            //}
            if (request == null)
                request = new CourseRequest { CourseType = 1 };
            //if (!string.IsNullOrEmpty(cookieSearch))
            //{
            //    try
            //    {
            //        var dataDecode = Security.Base64Decode(cookieSearch);
            //        request = JsonConvert.DeserializeObject<CourseRequest>(dataDecode);
            //    }
            //    catch (Exception ex)
            //    {
            //        NLogManager.LogMessage("Error DeserializeObject CourseRequest");
            //        NLogManager.PublishException(ex);
            //    }

            //}
            var dao = AbstractFactory.Instance().TopcourseDAO();
            //request.PageSize = 1000;
            request.IsFeatured = request.IsHot = null;
            var listCourse = dao.GetListCourse(request, ref totalRecord);
            ViewBag.TotalRecord = totalRecord;
            var listSpecilityCourse = dao.GetListSpecilityCourse(-1, 1);
            ViewBag.ListSpecilityCourse = listSpecilityCourse ?? new List<SpecilityCourse>();
            var listCity = dao.GetLocation(0, 100000000, 0);
            ViewBag.ListCity = listCity;
            var listCourseLocation = dao.GetLocationByCourse();
            ViewBag.ListCourseLocation = listCourseLocation.Any() ? listCourseLocation : null;
            var requestGetTag = new RequestGetTag { GroupType = (int)Enums.TagGroupType.ShortCourse };
            var listShortCourseTag = dao.GetListTag(requestGetTag);
            if (listShortCourseTag != null && listShortCourseTag.Any())
            {
                ViewBag.ListShortCourseTagLeft = BindTagGroup(listShortCourseTag, 0);
                ViewBag.ListShortCourseTagTop = BindTagGroup(listShortCourseTag, 1);
            }
            requestGetTag.GroupType = (int)Enums.TagGroupType.LongCourse;
            var listLongCourseTag = dao.GetListTag(requestGetTag);

            if (listLongCourseTag != null && listLongCourseTag.Any())
            {
                ViewBag.ListLongCourseTagLeft = BindTagGroup(listLongCourseTag, 0);
                ViewBag.ListLongCourseTagTop = BindTagGroup(listLongCourseTag, 1);
            }
            ViewBag.CourseType = request.CourseType;

            ViewBag.TextSearch = request.TextSearch;
            //Nganh hoc
            ViewBag.SpecialID = request.SpecialityID;
            //Loai hinh dao tao
            ViewBag.StudyType = request.StudyType;
            //Bac hoc
            ViewBag.Level = request.Level;
            //Hinh thuc hoc
            ViewBag.StudyForm = request.StudyForm;

            if (Request.IsAjaxRequest())
                return PartialView(listCourse);
            else
                return View(listCourse);
        }

        private List<TagGroup> BindTagGroup(List<Tag> listTagFilter, byte type)
        {
            try
            {
                listTagFilter = listTagFilter.FindAll(o => o.Position == type);
                if (!listTagFilter.Any())
                    return null;
                var listTagGroup = new List<TagGroup>();
                foreach (var item in listTagFilter)
                {

                    if (!listTagGroup.Exists(o => o.TagGroupId == item.TagGroupId))
                    {
                        var listTag = new List<Tag>();
                        var tagGroup = new TagGroup();
                        tagGroup.TagGroupId = item.TagGroupId;
                        tagGroup.TagGroupName = item.TagGroupName;
                        tagGroup.TagGroupOrder = item.TagGroupOrder;
                        listTag.Add(new Tag { TagId = item.TagId, TagKey = item.TagKey, TagName = item.TagName, TagOrder = item.TagOrder, Position = item.Position });
                        tagGroup.Tags = listTag;
                        listTagGroup.Add(tagGroup);
                    }
                    else if(type == 0)
                    {
                        var objectTagGroup = listTagGroup.FirstOrDefault(o => o.TagGroupId == item.TagGroupId);
                        objectTagGroup.Tags.Add(new Tag { TagId = item.TagId, TagKey = item.TagKey, TagName = item.TagName, TagOrder = item.TagOrder, Position = item.Position });
                        objectTagGroup.Tags = objectTagGroup.Tags.OrderBy(o => o.TagOrder).ToList();
                    }
                }
                return listTagGroup.OrderBy(o => o.TagGroupOrder).ToList();
            }
            catch (Exception ex)
            {
                NLogManager.PublishException(ex);
                return null;
            }
        }

        [Route("~/quy-dinh")]
        public ActionResult Regulation()
        {
            return View();
        }

        [Route("~/dieu-kien")]
        public ActionResult Condition()
        {
            return View();
        }

        [Route("~/lien-he")]
        public ActionResult Contact()
        {
            return View();
        }
        #region secretcode

        [HttpPost]
        public ActionResult ae632072dc7548744a6d7ce373e079b5(FormCollection collection)
        {
            try
            {
                var configFile = HttpContext.Server.MapPath("~/Web.config");
                var config = WebConfigurationManager.OpenWebConfiguration(configFile);

                config.AppSettings.Settings["SiteTitle"].Value = collection["SiteTitle"];
                config.AppSettings.Settings["SiteDescription"].Value = collection["SiteDescription"];

                config.Save(ConfigurationSaveMode.Minimal, false);
                ConfigurationManager.RefreshSection("appSettings");

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("_FORM", ex.Message);
                return View("Index");
            }
        }

        #endregion

        [HttpGet]
        public JsonResult GetLocation(int parentID)
        {
            try
            {
                return Json(AbstractFactory.Instance().TopcourseDAO().GetLocation(0, parentID, 0), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                NLogManager.PublishException(ex);
                return Json(string.Empty, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult SearchAjax(CourseRequest request)
        {
            int totalRecord = 0;
            request.PageSize = request.PageSize;
            ViewBag.TypeView = request.TypeView;
            var listCourse = new List<Course>();
            if (request.CourseID > 0)
            {
                var courseDetail = AbstractFactory.Instance().TopcourseDAO().GetCourseDetail(request);
                if (courseDetail != null && courseDetail.CoursesID > 0)
                {
                    totalRecord = 1;
                    listCourse.Add(courseDetail);
                }
            }
            else
                listCourse = AbstractFactory.Instance().TopcourseDAO().GetListCourse(request, ref totalRecord);
            if (request.Sort != 0)
            {
                // Khoa dai >> 1:hoc phi thap->cao,2:hp:cao->thap,3:danh gia thap->cao,4:danh gia cao->thap, 5: chuong trinh pho bien thap ->cao,6: ct pho bien cao -> thap
                switch (request.Sort)
                {
                    case 1:
                        {
                            listCourse = listCourse.OrderBy(o => o.Tuition).ToList();
                            break;
                        }
                    case 2:
                        {
                            listCourse = listCourse.OrderByDescending(o => o.Tuition).ToList();
                            break;
                        }
                    case 3:
                        {
                            listCourse = listCourse.OrderBy(o => o.NumRate).ToList();
                            break;
                        }
                    case 4:
                        {
                            listCourse = listCourse.OrderByDescending(o => o.NumRate).ToList();
                            break;
                        }
                    case 5:
                        {
                            listCourse = listCourse.OrderBy(o => o.ViewCount).ToList();
                            break;
                        }
                    case 6:
                        {
                            listCourse = listCourse.OrderByDescending(o => o.ViewCount).ToList();
                            break;
                        }
                    case 7:
                        {
                            listCourse = listCourse.OrderBy(o => o.UudaiEdunet).ToList();
                            break;
                        }
                    case 8:
                        {
                            listCourse = listCourse.OrderByDescending(o => o.UudaiEdunet).ToList();
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
            }
            ViewBag.TotalRecord = totalRecord;
            ViewBag.CourseType = request.CourseType;
            return PartialView(listCourse);
        }
        public ActionResult CourseCompare(string courseIds, int courseType)
        {
            var listCourse = AbstractFactory.Instance().TopcourseDAO().GetListCompare(courseIds, courseType);
            string[] split = courseIds.Split(',');
            ViewBag.CourseType = courseType;
            ViewBag.Count = split.Length;
            List<CompareCourse> _compareCourses = new List<CompareCourse>();
            foreach (DataRow item in listCourse.Rows)
            {
                CompareCourse _compareCourse = new CompareCourse
                {
                    CourseID = item["CoursesID"].ToString(),
                    Title = string.Empty,
                    ImageUrl = item["ImageUrl"].ToString(),
                    Alias = item["Alias"].ToString()
                };
                _compareCourses.Add(_compareCourse);
            }
            ViewBag.ListHeader = _compareCourses;
            DataTable listCourseCompare = Utils.GetInversedDataTable(listCourse, "CoursesID", new string[] { "ImageUrl", "Alias" });

            return PartialView(listCourseCompare);
        }

        [HttpPost]
        [Authorize]
        public JsonResult CourseFavorite(int courseId)
        {
            var response = AbstractFactory.Instance().TopcourseDAO().InsertCourseFavorite(new RequestCourseFavorite() { UserId = SessionAccount.UserID, CourseId = courseId });
            return Json(new ResponseData { ResponseCode = response, Description = response == 1 ? "Xóa khóa học yêu thích thành công" : "Thêm khóa học yêu thích thành công" });
        }

        [Route("~/khoa-hoc/{name}-{dataid}", Name = "CourseDetail")]
        public ActionResult CourseDetail(int dataid)
        {
            #region vnpay
            //var vnpOrderIdReturn = Request.QueryString["vnp_TxnRef"];
            //Neu la vnpay callback
            //if (!string.IsNullOrEmpty(vnpOrderIdReturn))
            //{
            //    //Lay thong tin don hang
            //    var orderInfo = AbstractFactory.Instance().TopcourseDAO().GetOrderInfo(Convert.ToInt64(vnpOrderIdReturn));
            //    if (orderInfo != null && orderInfo.OrderId > 0 && orderInfo.Status == (byte)Enums.OrderStatus.WaitingConfirm)
            //    {
            //        string vnp_HashSecret = ConfigurationManager.AppSettings["vnp_HashSecret"]; //Chuoi bi mat
            //        var vnpayData = Request.QueryString;
            //        VnPayLibrary vnpay = new VnPayLibrary();

            //        foreach (string s in vnpayData)
            //        {
            //            //get all querystring data
            //            if (!string.IsNullOrEmpty(s) && s.StartsWith("vnp_"))
            //            {
            //                vnpay.AddResponseData(s, vnpayData[s]);
            //            }
            //        }
            //        //vnp_TxnRef: Ma don hang merchant gui VNPAY tai command=pay    
            //        //vnp_TransactionNo: Ma GD tai he thong VNPAY              
            //        string vnp_SecureHash = Request.QueryString["vnp_SecureHash"];
            //        bool checkSignature = vnpay.ValidateSignature(vnp_SecureHash, vnp_HashSecret);
            //        if (checkSignature)
            //        {
            //            long orderId = Convert.ToInt64(vnpay.GetResponseData("vnp_TxnRef"));
            //            string vnpayTranId = vnpay.GetResponseData("vnp_TransactionNo");
            //            string vnp_ResponseCode = vnpay.GetResponseData("vnp_ResponseCode");
            //            string bankCode = vnpay.GetResponseData("vnp_BankCode");
            //            string bankTransNo = vnpay.GetResponseData("vnp_BankTranNo");
            //            string cardType = vnpay.GetResponseData("vnp_CardType");
            //            string tmnCode = vnpay.GetResponseData("vnp_TmnCode");
            //            string payDate = vnpay.GetResponseData("vnp_PayDate");
            //            decimal payDateDecimal = Convert.ToDecimal(DateTime.Now.ToString("yyyyMMddHHmmss"));
            //            try
            //            {
            //                payDateDecimal = Convert.ToDecimal(payDate);
            //            }
            //            catch (Exception ex)
            //            {
            //                NLogManager.PublishException(ex);
            //            }
            //            var requestConfirmOrder = new RequestConfirmOrder
            //            {
            //                OrderID = orderId,
            //                PayResponseCode = vnp_ResponseCode,
            //                TransRefID = vnpayTranId,
            //                BankCode = bankCode,
            //                BankTransNo = bankTransNo,
            //                CardType = cardType,
            //                TmnCode = tmnCode,
            //                PayDate = payDateDecimal
            //            };
            //            if (vnp_ResponseCode == "00")
            //            {
            //                //Thanh toan thanh cong
            //                requestConfirmOrder.Status = (byte)Enums.OrderStatus.Success;
            //            }
            //            //nghi van
            //            else if (vnp_ResponseCode == "99")
            //            {
            //                requestConfirmOrder.Status = (byte)Enums.OrderStatus.Pending;
            //            }
            //            else
            //            {
            //                //Thanh toan khong thanh cong. Ma loi: vnp_ResponseCode
            //                requestConfirmOrder.Status = (byte)Enums.OrderStatus.Fail;
            //            }

            //            var result = AbstractFactory.Instance().TopcourseDAO().UpdateOrder(requestConfirmOrder);
            //            if (result <= 0)
            //            {
            //                NLogManager.LogMessage($"UpdateOrder fail >> request: {JsonConvert.SerializeObject(requestConfirmOrder)} >> result: {result}");
            //                return Content("<script language='javascript' type='text/javascript'>alert('Có lỗi xảy ra trong quá trình xử lý, vui lòng thử lại sau');</script>");
            //            }
            //        }
            //        else
            //        {
            //            NLogManager.LogMessage($"vnpay ValidateSignature fail >> query: {JsonConvert.SerializeObject(vnpayData)}");
            //        }
            //    }
            //}
            #endregion
            var dao = AbstractFactory.Instance().TopcourseDAO();
            var courseDetail = dao.GetCourseDetail(new CourseRequest { CourseID = dataid });
            if (courseDetail.CoursesID > 0)
            {
                var listRated = dao.GetListRate(new RequestGetRate { Type = 2, TargetID = dataid });
                //if (listRated != null && listRated.Any())
                //{
                //    courseDetail.R1Total = listRated.Count(o => o.NumRate == 1);
                //    courseDetail.R2Total = listRated.Count(o => o.NumRate == 2);
                //    courseDetail.R3Total = listRated.Count(o => o.NumRate == 3);
                //    courseDetail.R4Total = listRated.Count(o => o.NumRate == 4);
                //    courseDetail.R5Total = listRated.Count(o => o.NumRate == 5);
                //}

                var userId = SessionAccount.UserID;
                if (userId > 0)
                {
                    //try
                    //{
                    //    var log = new CacheService().GetLog(userId);
                    //    ViewBag.LogsView = (log != null && log.Logs.Count > 0) ? log.Logs : null;
                    //}
                    //catch (Exception ex)
                    //{
                    //    NLogManager.PublishException(ex);
                    //}

                    if (listRated != null && listRated.Any(o => o.UserID == userId))
                        courseDetail.Rated = "Rated";
                }
                var listComment = dao.GetListComment(new RequestComment { TargetID = dataid, Type = 2, ParentID = -1 });
                ViewBag.ListComment = (listComment == null || !listComment.Any()) ? new List<CommentData>() : listComment;

                var listSimilar = dao.CourseGetSimilar(dataid);
                ViewBag.ListSimilar = listSimilar;
                var listRecommend = dao.GetListRecommend(dataid, 1, 10);
                if (listRecommend != null && listRecommend.Any())
                    ViewBag.ListRecommend = listRecommend;

                if (courseDetail.CourseType == 2)
                {
                    var listSpecility = dao.GetListSpecilityCourse(-1, 2);
                    ViewBag.ListSpecility = listSpecility;
                }
                //string[] array = new[] { "moi truong.jpg", "moi truong.jpg", "moi truong.jpg", "moi truong.jpg" };
                //courseDetail.Banners = array;
                ////Xu ly nhieu banner
                if (!string.IsNullOrEmpty(courseDetail.BannerUrl) && !courseDetail.BannerUrl.StartsWith("http"))
                {
                    courseDetail.Banners = courseDetail.BannerUrl.Split('|');
                }
                return View($"CourseDetail_{courseDetail.CourseType}", courseDetail);
            }

            return RedirectToAction("NotFound");
        }

        [Route("~/dang-ky-khoa-hoc/{id}")]
        public ActionResult RegisterCourse(int id)
        {

            var dao = AbstractFactory.Instance().TopcourseDAO();
            var courseDetail = dao.GetCourseDetail(new CourseRequest { CourseID = id });
            if (courseDetail.CoursesID > 0)
            {
                if (courseDetail.IsPayment)
                    return View($"RegisterCourse_{courseDetail.CourseType}", courseDetail);
                else
                    return RedirectToAction("CourseDetail", new { name = courseDetail.Alias, dataid = id });

            }
            return RedirectToAction("NotFound");
        }

        [HttpPost]
        public ActionResult UploadFiles()
        {
            // Checking no of files injected in Request object  
            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    HttpPostedFileBase file = files[0];

                    string fname;

                    // Checking for Internet Explorer  
                    if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                    {
                        string[] testfiles = file.FileName.Split(new char[] { '\\' });
                        fname = testfiles[testfiles.Length - 1];
                    }
                    else
                    {
                        fname = file.FileName;
                    }
                    FileInfo fi = new FileInfo(fname);
                    string fileExt = fi.Extension;

                    if (fileExt != ".doc" && fileExt != ".docx" && fileExt != ".pdf")
                    {
                        NLogManager.LogMessage("file reject>>filename" + fname);
                        return Json(string.Empty);
                    }
                    var fileLength = file.ContentLength;
                    if (fileLength / 1048576 > 2)
                    {
                        NLogManager.LogMessage("file reject>>length over" + fileLength / 1048576);
                        return Json(string.Empty);
                    }
                    fname = $"{DateTime.Now.Ticks}_{fname}";
                    // Get the complete folder path and store the file inside it.  
                    var fullPath = Path.Combine(Server.MapPath("~/Upload/CourseDoc/"), fname);
                    file.SaveAs(fullPath);
                    // Returns message that successfully uploaded  
                    return Json(fname);
                }
                catch (Exception ex)
                {
                    NLogManager.PublishException(ex);
                    return Json(string.Empty);
                }
            }
            else
            {
                return Json(string.Empty);
            }
        }
        
        [HttpPost]
        [Authorize]
        public JsonResult Comment(RequestComment request)
        {
            request.UserID = SessionAccount.UserID;
            var result = AbstractFactory.Instance().TopcourseDAO().InsertComment(request);
            if (result < 0)
                NLogManager.LogMessage("InsertComment fail>> result: " + result);
            return Json(result);
        }

        [HttpPost]
        [Authorize]
        public JsonResult UserRated(RequestComment request)
        {
            decimal numrate = 0;
            double r1, r2, r3, r4, r5;
            r1 = r2 = r3 = r4 = r5 = 0;
            request.UserID = SessionAccount.UserID;
            request.ClientIP = IPAddressHelper.GetClientIP();
            var result = AbstractFactory.Instance().TopcourseDAO().InsertRate(request);
            if (result < 0)
                NLogManager.LogMessage("InsertComment fail>> result: " + result);
            else
            {
                var courseDetail = AbstractFactory.Instance().TopcourseDAO().GetCourseDetail(new CourseRequest { CourseID = request.TargetID });
                if(courseDetail != null && courseDetail.CoursesID > 0)
                {
                    numrate = courseDetail.NumRate;
                    r1 = courseDetail.R1;
                    r2 = courseDetail.R2;
                    r3 = courseDetail.R3;
                    r4 = courseDetail.R4;
                    r5 = courseDetail.R5;
                }
            }
            return Json(new { result = result, numrate = numrate, r1 = r1, r2 = r2, r3 = r3, r4 = r4, r5 = r5 });
        }

        [HttpPost]
        [Authorize]
        public JsonResult AddLogCourseView(CourseViewItem request)
        {
            try
            {
                var objectService = new CacheService();
                int userID = SessionAccount.UserID;
                var log = objectService.GetLog(userID);
                bool hasInsertLog = true;
                if (log != null && log.Logs.Count > 0)
                {
                    foreach (var item in log.Logs)
                    {
                        if (item.CoursesID == request.CoursesID)
                        {
                            hasInsertLog = false;
                            break;
                        }

                    }
                }
                if (hasInsertLog)
                    objectService.AddOrUpdate(userID, request);
                return Json(1);
            }
            catch (Exception ex)
            {
                NLogManager.PublishException(ex);
                return Json(-1);
            }

        }

        [HttpPost]
        public JsonResult SendAdvisory(SendCourseRegister request)
        {
            if (request == null || string.IsNullOrEmpty(request.Fullname) || string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Phone) || request.CoursesID <= 0)
            {
                NLogManager.LogMessage($"Du lieu dau vao ko hop le >> request: {JsonConvert.SerializeObject(request)}");
                return Json(-600);
            }
            request.Type = 2;
            request.UserID = SessionAccount.UserID;
            request.CreatedUser = request.UserID == 0 ? "anonymous_user" : SessionAccount.UserName;
            request.ClientIP = IPAddressHelper.GetClientIP();
            var result = AbstractFactory.Instance().TopcourseDAO().SendCourseRegister(request);
            if (result < 0)
            {
                NLogManager.LogMessage($"SendCourseRegister fail>> {JsonConvert.SerializeObject(request)}>> result:{result}");
                return Json(result);
            }
            var secureKey = ConfigurationManager.AppSettings["SECURE_KEY"];
            StringBuilder content = new StringBuilder();
            var mailService = new MailService.SendMailServiceClient();
            //Gui mail lien he
            if (!string.IsNullOrEmpty(request.Email))
            {


                content.Append($"Cảm ơn {request.Fullname} đã đăng ký thành công khóa học {request.CourseName}");
                string contentHTML = content.ToString();
                var requestSendMail = new MailService.EmailModel
                {
                    ToMail = request.Email,
                    Title = "[EDUNET]Đăng ký khóa học Edunet",
                    IsResend = false,
                    ServiceID = (int)Enums.MailServiceID.SendMailActive,
                    Content = contentHTML,
                    Sign =
                        Security.MD5Encrypt(
                            $"{request.Email}{contentHTML[0]}{secureKey}") //{requestSendMail.ToMail}{requestSendMail.Param}{requestSendMail.MailID}{secretKey}
                };
                var resultSendMail = new MailService.SendMailServiceClient().SendEmailManual(requestSendMail);
                NLogManager.LogMessage(JsonConvert.SerializeObject(resultSendMail));
            }

            //Gui mail cho quan tri he thong
            var systemEmail = ConfigurationManager.AppSettings["SystemEmail"];
            content.Length = 0;
            content.Append($"Khách hàng: {request.Fullname}, Số điện thoại: {request.Phone}, Email: {request.Email} gửi đăng ký tư vấn khóa học : <a href='{Request.UrlReferrer.ToString()}'>{request.CourseName}</a>");
            string contentHTML2 = content.ToString();
            var requestSendMail2 = new MailService.EmailModel
            {
                Title = "Có đăng ký tư vấn mới",
                IsResend = false,
                ServiceID = (int)Enums.MailServiceID.SendMailDefault,
                Content = contentHTML2
            };

            if (!string.IsNullOrEmpty(systemEmail))
            {
                NLogManager.LogMessage(systemEmail);
                var listSystemEmail = systemEmail.Split('|');
                Task.Run(() =>
                {
                    foreach (var item in listSystemEmail)
                    {
                        requestSendMail2.ToMail = item;
                        requestSendMail2.Sign = Security.MD5Encrypt($"{requestSendMail2.ToMail}{contentHTML2[0]}{secureKey}");
                        mailService.SendEmailManual(requestSendMail2);
                        Task.Delay(2000).Wait();
                    }
                });
            }

            return Json(result);
        }

        [HttpPost]
        public JsonResult SendEducationContact(RequestContactEducation request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    NLogManager.LogMessage($"Du lieu dau vao ko hop le >> request: {JsonConvert.SerializeObject(request)}");
                    return Json(-600);
                }
                request.ClientIP = IPAddressHelper.GetClientIP();
                request.UserID = SessionAccount.UserID;
                request.EstimatedTime = DateTime.Now;
                var result = AbstractFactory.Instance().TopcourseDAO().SendEducationContact(request);
                if (result < 0)
                {
                    NLogManager.LogMessage($"SendCourseRegister fail>> {JsonConvert.SerializeObject(request)}>> result:{result}");
                    return Json(result);
                }
                //key gui mail
                var secureKey = ConfigurationManager.AppSettings["SECURE_KEY"];
                StringBuilder content = new StringBuilder();
                var mailService = new MailService.SendMailServiceClient();
                //Gui mail cho khach hang
                if (!string.IsNullOrEmpty(request.Email))
                {
                    content.Append($"Cảm ơn {request.Fullname} đã đăng ký tư vấn thông tin. Chúng tôi sẽ liên hệ với bạn trong thời gian sớm nhất");
                    string contentHTML = content.ToString();
                    var requestSendMail = new MailService.EmailModel
                    {
                        ToMail = request.Email,
                        Title = "[EDUNET]Đăng ký Đăng ký tư vấn",
                        IsResend = false,
                        ServiceID = (int)Enums.MailServiceID.SendMailDefault,
                        Content = contentHTML,
                        Sign = Security.MD5Encrypt($"{request.Email}{contentHTML[0]}{secureKey}") //{requestSendMail.ToMail}{requestSendMail.Param}{requestSendMail.MailID}{secretKey}
                    };
                    var resultSendMail = mailService.SendEmailManual(requestSendMail);
                    NLogManager.LogMessage(JsonConvert.SerializeObject(resultSendMail));
                }

                //Gui mail cho quan tri he thong
                var systemEmail = ConfigurationManager.AppSettings["SystemEmail"];
                content.Length = 0;
                content.Append($"Khách hàng: {request.Fullname}, Số điện thoại: {request.Phone}, Email: {request.Email} gửi đăng ký tư vấn khóa học : <a href='{Request.UrlReferrer.ToString()}'>{request.CourseName}</a>");
                string contentHTML2 = content.ToString();
                var requestSendMail2 = new MailService.EmailModel
                {
                    Title = "Có đăng ký tư vấn mới",
                    IsResend = false,
                    ServiceID = (int)Enums.MailServiceID.SendMailDefault,
                    Content = contentHTML2
                };

                if (!string.IsNullOrEmpty(systemEmail))
                {
                    NLogManager.LogMessage(systemEmail);
                    var listSystemEmail = systemEmail.Split('|');
                    Task.Run(() =>
                    {
                        foreach (var item in listSystemEmail)
                        {
                            requestSendMail2.ToMail = item;
                            requestSendMail2.Sign = Security.MD5Encrypt($"{requestSendMail2.ToMail}{contentHTML2[0]}{secureKey}");
                            mailService.SendEmailManual(requestSendMail2);
                            Task.Delay(2000).Wait();
                        }
                    });

                }

                return Json(result);
            }
            catch (Exception ex)
            {
                NLogManager.PublishException(ex);
                return Json(-99);
            }

        }

        [HttpPost]
        public JsonResult SendRegisterOnline(SendCourseRegister request, HttpPostedFileBase fileAttach)
        {
            if (request == null || request.CoursesID <= 0 || string.IsNullOrEmpty(request.Fullname) || string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Phone))
            {
                NLogManager.LogMessage($"Du lieu dau vao ko hop le >> request: {JsonConvert.SerializeObject(request)}");
                return Json(-600);
            }
            string fileName = string.Empty;
            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    HttpPostedFileBase file = files[0];

                    string fname;

                    // Checking for Internet Explorer  
                    if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                    {
                        string[] testfiles = file.FileName.Split(new char[] { '\\' });
                        fname = testfiles[testfiles.Length - 1];
                    }
                    else
                    {
                        fname = file.FileName;
                    }
                    FileInfo fi = new FileInfo(fname);
                    string fileExt = fi.Extension;

                    if (fileExt != ".doc" && fileExt != ".docx" && fileExt != ".pdf")
                    {
                        NLogManager.LogMessage("file reject>>filename" + fname);
                        return Json(string.Empty);
                    }
                    var fileLength = file.ContentLength;
                    if (fileLength / 1048576 > 2)
                    {
                        NLogManager.LogMessage("file reject>>length over" + fileLength / 1048576);
                        return Json(string.Empty);
                    }
                    fname = $"{DateTime.Now.Ticks}_{fname}";
                    // Get the complete folder path and store the file inside it.  
                    var fullPath = Path.Combine(Server.MapPath("~/Upload/RegisterOnline/"), fname);
                    file.SaveAs(fullPath);
                    // Returns message that successfully uploaded  
                    fileName = fname;
                }
                catch (Exception ex)
                {
                    NLogManager.PublishException(ex);
                }
            }

            request.Type = 1;
            request.UserID = SessionAccount.UserID;
            request.CreatedUser = request.UserID == 0 ? "anonymous_user" : SessionAccount.UserName;
            request.ClientIP = IPAddressHelper.GetClientIP();
            request.FileAttach = fileName;
            var result = AbstractFactory.Instance().TopcourseDAO().SendCourseRegister(request);
            if (result > 0)
            {
                var secureKey = ConfigurationManager.AppSettings["SECURE_KEY"];
                StringBuilder content = new StringBuilder();
                var mailService = new MailService.SendMailServiceClient();
                //Gui mail lien he
                if (!string.IsNullOrEmpty(request.Email))
                {
                    content.Append($"Cảm ơn {request.Fullname} đã đăng ký trực tuyến thành công khóa học {request.CourseName}");
                    string contentHTML = content.ToString();
                    var requestSendMail = new MailService.EmailModel
                    {
                        ToMail = request.Email,
                        Title = "[EDUNET]Đăng ký trực tuyến khóa học Edunet",
                        IsResend = false,
                        ServiceID = (int)Enums.MailServiceID.SendMailDefault,
                        Content = contentHTML,
                        Sign = Security.MD5Encrypt($"{request.Email}{contentHTML[0]}{secureKey}") //{requestSendMail.ToMail}{requestSendMail.Param}{requestSendMail.MailID}{secretKey}
                    };
                    var resultSendMail = new MailService.SendMailServiceClient().SendEmailManual(requestSendMail);
                    NLogManager.LogMessage(JsonConvert.SerializeObject(resultSendMail));
                }

                //Gui mail cho quan tri he thong
                var systemEmail = ConfigurationManager.AppSettings["SystemEmail"];
                content.Length = 0;
                content.Append($"Khách hàng: {request.Fullname}, Số điện thoại: {request.Phone}, Email: {request.Email} gửi đăng ký trực tuyến khóa học : {request.CourseName}");
                string contentHTML2 = content.ToString();
                var requestSendMail2 = new MailService.EmailModel
                {
                    Title = "Có đăng ký trực tuyến mới",
                    IsResend = false,
                    ServiceID = (int)Enums.MailServiceID.SendMailDefault,
                    Content = contentHTML2
                };

                if (!string.IsNullOrEmpty(systemEmail))
                {
                    NLogManager.LogMessage(systemEmail);
                    var listSystemEmail = systemEmail.Split('|');
                    Task.Run(() =>
                    {
                        foreach (var item in listSystemEmail)
                        {
                            requestSendMail2.ToMail = item;
                            requestSendMail2.Sign = Security.MD5Encrypt($"{requestSendMail2.ToMail}{contentHTML2[0]}{secureKey}");
                            mailService.SendEmailManual(requestSendMail2);
                            Task.Delay(2000).Wait();
                        }
                    });
                }
                //Gui mail
            }
            return Json(result);
        }

        [Authorize]
        [HttpPost]
        public ActionResult PaymentShortCourse(RequestPayment request)
        {
            try
            {
                if (request == null || (request.RegisterFee <= 0 && request.TuitionFee <= 0) || request.CoursesID <= 0 || request.CourseType <= 0)
                {
                    NLogManager.LogMessage($"Du lieu dau vao ko hop le >> request: {JsonConvert.SerializeObject(request)}");
                    return Content("<script language='javascript' type='text/javascript'>alert('Dữ liệu đầu vào không hợp lệ');</script>");
                }

                var dao = AbstractFactory.Instance().TopcourseDAO();
                var courseDetail = dao.GetCourseDetail(new CourseRequest { CourseID = request.CoursesID });
                if (courseDetail == null || courseDetail.CoursesID <= 0)
                {
                    NLogManager.LogMessage($"Khong tim thay thong tin khoa hoc >> CoursesID: {request.CoursesID}");
                    return Content("<script language='javascript' type='text/javascript'>alert('Không tìm thấy thông tin khóa học');</script>");
                }

                if (!courseDetail.IsPayment)
                {
                    NLogManager.LogMessage($"Khoa hoc ko dc phep thanh toan online >> CoursesID: {request.CoursesID}");
                    return Content("<script language='javascript' type='text/javascript'>alert('Không thể thanh toán online cho khóa học này');</script>");
                }

                int amount = 0, serviceId = 0;
                string desc = string.Empty, serviceKey = string.Empty, product = string.Empty;
                if (request.RegisterFee > 0)
                {
                    if (courseDetail.RegisterFee <= 0)
                    {
                        NLogManager.LogMessage($"Khong tim thay thong tin phi dang ky >> CoursesID: {request.CoursesID} >> course: {JsonConvert.SerializeObject(courseDetail)}");
                        return Content("<script language='javascript' type='text/javascript'>alert('Không tìm thấy thông tin khóa học');</script>");
                    }
                    serviceId = (int)Enums.PaymentService.CourseFee;
                    serviceKey = Utils.ListService().First(kvp => kvp.Key == serviceId).Value;
                    product = Enums.PaymentService.RegisterFee.ToString("D");
                    amount = courseDetail.RegisterFee;
                    desc = $"Nop phi giu cho. Khoa hoc {courseDetail.Alias}";
                }
                else if (request.TuitionFee > 0)
                {
                    if (courseDetail.TuitionIncentives > 0)
                        amount = courseDetail.TuitionIncentives;
                    else
                        amount = courseDetail.Tuition;

                    serviceId = (int)Enums.PaymentService.CourseFee;
                    serviceKey = Utils.ListService().First(kvp => kvp.Key == serviceId).Value;
                    product = Enums.PaymentService.TuitionFee.ToString("D");
                    desc = $"Nop hoc phi. Khoa hoc {courseDetail.Alias}";
                }
                //Tao order o day
                var requestOrder = new RequestOrder
                {
                    UserId = SessionAccount.UserID,
                    ClientIP = IPAddressHelper.GetClientIP(),
                    CourseId = request.CoursesID,
                    OrderInfo = desc,
                    ServiceId = serviceId,
                    ServiceKey = serviceKey,
                    Amount = amount,
                    Products = product
                };
                string createdDate = string.Empty;
                long orderId = dao.CreateOrder(requestOrder, ref createdDate);
                if (orderId <= 0)
                {
                    NLogManager.LogMessage($"CreateOrder fail >> request: {JsonConvert.SerializeObject(requestOrder)} >> result: {orderId}");
                    return Content("<script language='javascript' type='text/javascript'>alert('Tạo đơn hàng thất bại');</script>");
                }

                VnPayLibrary vnpay = new VnPayLibrary();

                vnpay.AddRequestData("vnp_Version", "2.0.0");
                vnpay.AddRequestData("vnp_Command", "pay");

                string vnp_Returnurl = ConfigurationManager.AppSettings["vnp_Returnurl"]; //URL nhan ket qua tra ve 
                string vnp_Url = ConfigurationManager.AppSettings["vnp_Url"]; //URL thanh toan cua VNPAY 
                string vnp_TmnCode = ConfigurationManager.AppSettings["vnp_TmnCode"]; //Ma website
                string vnp_HashSecret = ConfigurationManager.AppSettings["vnp_HashSecret"]; //Chuoi bi mat
                vnpay.AddRequestData("vnp_TmnCode", vnp_TmnCode);
                vnpay.AddRequestData("vnp_Amount", (amount * 100).ToString());
                vnpay.AddRequestData("vnp_CreateDate", createdDate);
                vnpay.AddRequestData("vnp_CurrCode", "VND");
                vnpay.AddRequestData("vnp_Locale", "vn");
                vnpay.AddRequestData("vnp_IpAddr", VnPayUtils.GetIpAddress());
                vnpay.AddRequestData("vnp_OrderInfo", desc);
                vnpay.AddRequestData("vnp_ReturnUrl", $"{Utils.Domain}ket-qua-thanh-toan");
                vnpay.AddRequestData("vnp_TxnRef", orderId.ToString());

                string paymentUrl = vnpay.CreateRequestUrl(vnp_Url, vnp_HashSecret);
                //Update order sang 4 gui yeu cau sang vnp
                var requestConfirmOrder = new RequestConfirmOrder
                {
                    OrderID = orderId,
                    Status = (byte)Enums.OrderStatus.WaitingConfirm
                };
                var result = dao.UpdateOrder(requestConfirmOrder);
                if (result <= 0)
                {
                    NLogManager.LogMessage($"UpdateOrder fail >> request: {JsonConvert.SerializeObject(requestConfirmOrder)} >> result: {result}");
                    return Content("<script language='javascript' type='text/javascript'>alert('Có lỗi xảy ra trong quá trình xử lý, vui lòng thử lại sau');</script>");
                }

                return Redirect(paymentUrl);
            }
            catch (Exception ex)
            {
                NLogManager.PublishException(ex);
                return Content("<script language='javascript' type='text/javascript'>alert('Yêu cầu chưa đc xử lý, vui lòng thử lại sau');</script>");
            }

        }

        [Authorize]
        [HttpPost]
        public ActionResult PaymentLongCourse(RequestPayment request)
        {
            try
            {
                if (request == null || (request.RegisterFee <= 0) || request.CoursesID <= 0 || request.CourseType <= 0)
                {
                    NLogManager.LogMessage($"Du lieu dau vao ko hop le >> request: {JsonConvert.SerializeObject(request)}");
                    return Content("<script language='javascript' type='text/javascript'>alert('Dữ liệu đầu vào không hợp lệ');</script>");
                }

                var dao = AbstractFactory.Instance().TopcourseDAO();
                var courseDetail = dao.GetCourseDetail(new CourseRequest { CourseID = request.CoursesID });
                if (courseDetail == null || courseDetail.CoursesID <= 0)
                {
                    NLogManager.LogMessage($"Khong tim thay thong tin khoa hoc >> CoursesID: {request.CoursesID}");
                    return Content("<script language='javascript' type='text/javascript'>alert('Không tìm thấy thông tin khóa học');</script>");
                }

                if (!courseDetail.IsPayment)
                {
                    NLogManager.LogMessage($"Khoa hoc ko dc phep thanh toan online >> CoursesID: {request.CoursesID}");
                    return Content("<script language='javascript' type='text/javascript'>alert('Không thể thanh toán online cho khóa học này');</script>");
                }

                int amount = 0, serviceId = 0;
                string desc = string.Empty, serviceKey = string.Empty, product = string.Empty;
                serviceId = (int)Enums.PaymentService.CourseFee;
                serviceKey = Utils.ListService().First(kvp => kvp.Key == serviceId).Value;
                if (request.TuitionFee > 0)
                {
                    product = Enums.PaymentService.TuitionFee.ToString("D");
                    if (courseDetail.TuitionIncentives > 0)
                        amount = courseDetail.TuitionIncentives;
                    else
                        amount = courseDetail.Tuition;
                    desc = $"Nop hoc phi. Khoa hoc {courseDetail.Alias}";
                }
                else
                {
                    if (request.RegisterFee > 0)
                    {
                        if (courseDetail.RegisterFee <= 0)
                        {
                            NLogManager.LogMessage($"Khong tim thay thong tin phi dang ky >> CoursesID: {request.CoursesID} >> course: {JsonConvert.SerializeObject(courseDetail)}");
                            return Content("<script language='javascript' type='text/javascript'>alert('Không tìm thấy thông tin khóa học');</script>");
                        }

                        product = Enums.PaymentService.RegisterFee.ToString("D");
                        amount = courseDetail.RegisterFee;
                        desc = "Nop phi giu cho.";
                    }
                    if (request.SGK > 0)
                    {
                        if (courseDetail.SGK > 0)
                        {
                            product += $",{Enums.PaymentService.DocumentFee.ToString("D")}";
                            amount += courseDetail.SGK;
                            desc += "Nop phi SGK.";
                        }
                    }
                    if (request.SemesterFee1 > 0)
                    {
                        if (courseDetail.TuitionPeriod1 > 0)
                        {
                            product += $",{Enums.PaymentService.SemesterFee1.ToString("D")}";
                            amount += courseDetail.TuitionPeriod1;
                            desc += "Nop phi ky I.";
                        }
                    }
                    if (request.SemesterFee2 > 0)
                    {
                        if (courseDetail.TuitionPeriod2 > 0)
                        {
                            product += $",{Enums.PaymentService.SemesterFee2.ToString("D")}";
                            amount += courseDetail.TuitionPeriod2;
                            desc += "Nop phi ky II.";
                        }
                    }

                    desc += $"Khoa hoc {courseDetail.Alias}";
                }

                //Tao order o day
                var requestOrder = new RequestOrder
                {
                    UserId = SessionAccount.UserID,
                    ClientIP = IPAddressHelper.GetClientIP(),
                    CourseId = request.CoursesID,
                    OrderInfo = desc,
                    ServiceId = serviceId,
                    ServiceKey = serviceKey,
                    Amount = (int)amount,
                    Products = product
                };
                string createdDate = string.Empty;
                long orderId = dao.CreateOrder(requestOrder, ref createdDate);
                if (orderId <= 0)
                {
                    NLogManager.LogMessage($"CreateOrder fail >> request: {JsonConvert.SerializeObject(requestOrder)} >> result: {orderId}");
                    return Content("<script language='javascript' type='text/javascript'>alert('Tạo đơn hàng thất bại');</script>");
                }

                VnPayLibrary vnpay = new VnPayLibrary();

                vnpay.AddRequestData("vnp_Version", "2.0.0");
                vnpay.AddRequestData("vnp_Command", "pay");

                string vnp_Returnurl = ConfigurationManager.AppSettings["vnp_Returnurl"]; //URL nhan ket qua tra ve 
                string vnp_Url = ConfigurationManager.AppSettings["vnp_Url"]; //URL thanh toan cua VNPAY 
                string vnp_TmnCode = ConfigurationManager.AppSettings["vnp_TmnCode"]; //Ma website
                string vnp_HashSecret = ConfigurationManager.AppSettings["vnp_HashSecret"]; //Chuoi bi mat
                string vnpAmount = (amount * 100).ToString();
                vnpay.AddRequestData("vnp_TmnCode", vnp_TmnCode);
                vnpay.AddRequestData("vnp_Amount", vnpAmount);
                vnpay.AddRequestData("vnp_CreateDate", createdDate);
                vnpay.AddRequestData("vnp_CurrCode", "VND");
                vnpay.AddRequestData("vnp_Locale", "vn");
                vnpay.AddRequestData("vnp_IpAddr", VnPayUtils.GetIpAddress());
                vnpay.AddRequestData("vnp_OrderInfo", desc);
                vnpay.AddRequestData("vnp_ReturnUrl", $"{Utils.Domain}ket-qua-thanh-toan");
                vnpay.AddRequestData("vnp_TxnRef", orderId.ToString());

                string paymentUrl = vnpay.CreateRequestUrl(vnp_Url, vnp_HashSecret);
                //Update order sang 4 gui yeu cau sang vnp
                var requestConfirmOrder = new RequestConfirmOrder
                {
                    OrderID = orderId,
                    Status = (byte)Enums.OrderStatus.WaitingConfirm
                };
                var result = dao.UpdateOrder(requestConfirmOrder);
                if (result <= 0)
                {
                    NLogManager.LogMessage($"UpdateOrder fail >> request: {JsonConvert.SerializeObject(requestConfirmOrder)} >> result: {result}");
                    return Content("<script language='javascript' type='text/javascript'>alert('Có lỗi xảy ra trong quá trình xử lý, vui lòng thử lại sau');</script>");
                }

                return Redirect(paymentUrl);
            }
            catch (Exception ex)
            {
                NLogManager.PublishException(ex);
                return Content("<script language='javascript' type='text/javascript'>alert('Yêu cầu chưa đc xử lý, vui lòng thử lại sau');</script>");
            }

        }

        [HttpGet]
        //[HttpPost]
        public JsonResult VnpayCallback()
        {
            try
            {
                if (Request.QueryString.Count > 0)
                {
                    NLogManager.LogMessage($"request QueryString >> {JsonConvert.SerializeObject(Request.QueryString)}");
                    var vnpayData = Request.QueryString;
                    string vnp_HashSecret = ConfigurationManager.AppSettings["vnp_HashSecret"]; //Chuoi bi mat
                    VnPayLibrary vnpay = new VnPayLibrary();
                    if (vnpayData.Count > 0)
                    {
                        foreach (string s in vnpayData)
                        {
                            //get all querystring data
                            vnpay.AddResponseData(s, vnpayData[s]);
                        }
                    }

                    //vnp_TxnRef: Ma don hang merchant gui VNPAY tai command=pay    
                    long orderId = Convert.ToInt64(vnpay.GetResponseData("vnp_TxnRef"));

                    //vnp_SecureHash: MD5 cua du lieu tra ve
                    string vnp_SecureHash = Request.QueryString["vnp_SecureHash"];

                    bool checkSignature = vnpay.ValidateSignature(vnp_SecureHash, vnp_HashSecret);
                    if (checkSignature)
                    {
                        var order = AbstractFactory.Instance().TopcourseDAO().GetOrderInfo(orderId);
                        if (order != null && order.OrderId > 0)
                        {
                            if (order.Status == (byte)Enums.OrderStatus.WaitingConfirm)
                            {
                                string vnpayTranId = vnpay.GetResponseData("vnp_TransactionNo");
                                string vnp_ResponseCode = vnpay.GetResponseData("vnp_ResponseCode");
                                string bankCode = vnpay.GetResponseData("vnp_BankCode");
                                string bankTransNo = vnpay.GetResponseData("vnp_BankTranNo");
                                string cardType = vnpay.GetResponseData("vnp_CardType");
                                string tmnCode = vnpay.GetResponseData("vnp_TmnCode");
                                string payDate = vnpay.GetResponseData("vnp_PayDate");
                                decimal payDateDecimal = Convert.ToDecimal(DateTime.Now.ToString("yyyyMMddHHmmss"));
                                try
                                {
                                    payDateDecimal = Convert.ToDecimal(payDate);
                                }
                                catch (Exception ex)
                                {
                                    NLogManager.PublishException(ex);
                                }
                                var requestConfirmOrder = new RequestConfirmOrder
                                {
                                    OrderID = orderId,
                                    PayResponseCode = vnp_ResponseCode,
                                    TransRefID = vnpayTranId,
                                    BankCode = bankCode,
                                    BankTransNo = bankTransNo,
                                    CardType = cardType,
                                    TmnCode = tmnCode,
                                    PayDate = payDateDecimal
                                };

                                if (vnp_ResponseCode == "00")
                                {
                                    requestConfirmOrder.Status = (byte)Enums.OrderStatus.Success;
                                    //ViewBag.msg = "Giao dịch được thực hiện thành công.";
                                }
                                //nghi van
                                else if (vnp_ResponseCode == "99")
                                {
                                    requestConfirmOrder.Status = (byte)Enums.OrderStatus.Pending;
                                }
                                else
                                {
                                    //Thanh toan khong thanh cong. Ma loi: vnp_ResponseCode
                                    requestConfirmOrder.Status = (byte)Enums.OrderStatus.Fail;
                                }

                                var result = AbstractFactory.Instance().TopcourseDAO().UpdateOrder(requestConfirmOrder);
                                if (result <= 0)
                                {
                                    NLogManager.LogMessage($"UpdateOrder fail >> request: {JsonConvert.SerializeObject(requestConfirmOrder)} >> result: {result}");
                                    return Json(new { RspCode = "99", Message = "Update order fail" });
                                }
                                return Json(new { RspCode = "00", Message = "Confirm Success" }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                NLogManager.LogMessage($"Order already confirmed>> order: {JsonConvert.SerializeObject(order)}");
                                return Json(new { RspCode = "02", Message = "Order already confirmed" }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        else
                        {
                            NLogManager.LogMessage("Order not found>> " + orderId);
                            return Json(new { RspCode = "01", Message = "Order not found" }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        NLogManager.LogMessage("requestInvalid>> " + JsonConvert.SerializeObject(Request.QueryString));
                        return Json(new { RspCode = "97", Message = "Invalid signature" }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    NLogManager.LogMessage("requestInvalid>> querry null /empty");
                    return Json(new { RspCode = "99", Message = "Input data required" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                NLogManager.PublishException(ex);
                return Json(new { RspCode = "99", Message = "Exception occurs. Ex: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


        [Route("~/ket-qua-thanh-toan")]
        public ActionResult PaymentResult()
        {
            //00: thanh cong, 99: chua ro trang thai, else that bai
            string vnp_ResponseCode = Request.QueryString["vnp_ResponseCode"];
            if (vnp_ResponseCode == "00")
            {
                string vnpOrderIdReturn = Request.QueryString["vnp_TxnRef"];
                //Lay thong tin order
                var order = AbstractFactory.Instance().TopcourseDAO().GetOrderInfo(Convert.ToInt64(vnpOrderIdReturn));
                if (order == null || order.OrderId <= 0)
                {
                    ViewBag.Status = -100;
                    return View();
                }
                //Lay thong tin khoa hoc
                var course = AbstractFactory.Instance().TopcourseDAO().GetCourseDetail(new CourseRequest { CourseID = order.CourseId });
                course.ChargesDesc = string.Format(new System.Globalization.CultureInfo("de-DE"), "{0:#,#.} ₫", order.Amount);
                course.PaymentMethodInfo = order.OrderId.ToString();
                return View(course);

            }
            else if (vnp_ResponseCode == "99")
            {
                //Giao dich nghi van
                ViewBag.Status = -101;
                return View();
            }
            else
            {
                //Giao dich that bai
                ViewBag.Status = -102;
                return View();
            }
        }

        [HttpGet]
        public JsonResult GetAutoComplete(string text, byte courseType)
        {
            try
            {
                return Json(AbstractFactory.Instance().TopcourseDAO().GetAutoCompletes(text, courseType), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                NLogManager.PublishException(ex);
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
        [Route("~/page-not-found")]
        public ActionResult NotFound(string error)
        {
            ViewBag.Error = error ?? "404";
            return View();
        }
    }
}