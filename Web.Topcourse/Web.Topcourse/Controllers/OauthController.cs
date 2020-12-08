using Common.Model;
using Common.Utilities.IpAddress;
using Common.Utilities.Log;
using Common.Utilities.Security;
using DataAccess.Topcourse.Factory;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Security;
using Utilities.Http;
using Web.Topcourse.Helper;

namespace Web.Topcourse.Controllers
{
    public class OauthController : ApiController
    {
        // GET: UtilityApi
        [HttpGet]
        [ActionName("google_authen")]
        public IHttpActionResult GoogleAuthen()
        {
            var data = new { AuthenUrl = $"{Utils.Domain}api/oauth/request_auth?login_type=google&redirect_uri={HttpUtility.UrlEncode(Utils.Domain)}" };
            return Ok(data);
        }


        [HttpGet]
        [HttpPost]
        [ActionName("request_auth")]
        public HttpResponseMessage RequestAuth()
        {
            var outputResponse = Request.CreateResponse(HttpStatusCode.Found); // Redirect
            // var redirect_uri = HttpContext.Current.Request["redirect_uri"] ?? System.Configuration.ConfigurationManager.AppSettings["DOMAIN"];
            var redirect_uri = HttpContext.Current.Request["redirect_uri"] ?? Utils.Domain;
            var actionType = HttpContext.Current.Request["actionType"] ?? "1"; //
            var login_type = HttpContext.Current.Request["login_type"] ?? "google";
            try
            {
                switch (login_type)
                {
                    case "google":
                        var googlelogin_callback_url = ConfigurationManager.AppSettings["GOOGLE_APP_REDIRECT_URIS"];
                        var googlelogin_client_id = ConfigurationManager.AppSettings["GOOGLE_APP_ID"];

                        var urlRedirects = "https://accounts.google.com/o/oauth2/auth?scope=https%3A%2F%2Fwww.googleapis.com%2Fauth%2Fuserinfo.email+https%3A%2F%2Fwww.googleapis.com%2Fauth%2Fuserinfo.profile&redirect_uri="
                                           + HttpUtility.UrlEncode(googlelogin_callback_url)
                                           + "&response_type=code&client_id=" + googlelogin_client_id;
                        outputResponse.Headers.Location = new Uri(urlRedirects);
                        return outputResponse;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                NLogManager.PublishException(ex);
            }
            outputResponse.Headers.Location = new Uri(redirect_uri);
            return outputResponse;
        }

        [HttpGet]
        [ActionName("googlelogincallback")]
        public HttpResponseMessage googlelogincallback()
        {
            var outputResponse = Request.CreateResponse(HttpStatusCode.Found); // Redirect
            var error = HttpContext.Current.Request["error"] ?? "";

            if (!string.IsNullOrEmpty(error))
            {
                outputResponse.Headers.Location = new Uri(string.Format("{0}?messeger={1}", Utils.Domain, "Đăng nhập thất bại"));
                return outputResponse;
            }

            var code = HttpContext.Current.Request["code"] ?? "";

            var userInfo = GoogleHelpers.GetGoogleUserInfo(code);
            //Lay thong tin gan ket
            var requestGetAssociateUser = new OAuthenAccount
            {
                OAuthAccount = userInfo.id,
                OAuthSystemID = (int)Enums.OAuthSystemID.Google
            };
            var oathAccount = AbstractFactory.Instance().TopcourseDAO().GetAssociateUser(requestGetAssociateUser);
            if (oathAccount!=null && oathAccount.OAuthID == -969)
            {
                NLogManager.LogMessage($"Exception, ko xu ly gi ca >> request: {JsonConvert.SerializeObject(requestGetAssociateUser)} >> response:{JsonConvert.SerializeObject(oathAccount)}");
                outputResponse.Headers.Location = new Uri(string.Format("{0}?messeger={1}", Utils.Domain, "Đăng nhập thất bại"));
                return outputResponse;
            }
            //Chua gan ket
            if (oathAccount == null || oathAccount.OAuthID <= 0)
            {
                int userID = 0;
                string userName = string.Empty;
                string fullName = string.Empty;
                string clientIP = IPAddressHelper.GetClientIP();
                var basicInfo = AbstractFactory.Instance().TopcourseDAO().GetBasicInfo(userInfo.email, 0);
                
                //Goi dang ky
                if (basicInfo == null || basicInfo.UserID <= 0)
                {
                    string randomPassword = Guid.NewGuid().ToString();
                    string userProfile = string.Empty;
                    if (!string.IsNullOrEmpty(userInfo.family_name))
                        userProfile += $"Firstname#0#{Utils.DbFormatString(userInfo.family_name)}";
                    if (!string.IsNullOrEmpty(userInfo.given_name))
                        userProfile += (string.IsNullOrEmpty(userProfile) ? $"Lastname#0#{Utils.DbFormatString(userInfo.given_name)}" : $"|Lastname#0#{Utils.DbFormatString(userInfo.given_name)}");

                    var requestRegStudent = new ProfileStudent
                    {
                        Username = userInfo.email,
                        Password = Security.MD5Encrypt($"{userInfo.email}{randomPassword}"),
                        ClientIP = clientIP,
                        RegisterType = (int)Enums.RegisterType.OAuthen,
                        FullName = userInfo.name,
                        UserProfileInfo = userProfile
                    };
                    var resultReg = AbstractFactory.Instance().TopcourseDAO().RegisterStudent(requestRegStudent);
                    if (resultReg < 0)
                    {
                        NLogManager.LogMessage($"Tu dong dang ky oauth that bai>>request:{JsonConvert.SerializeObject(requestRegStudent)} >> result: {resultReg}");
                        outputResponse.Headers.Location = new Uri(string.Format("{0}?messeger={1}", Utils.Domain, "Đăng nhập thất bại"));
                        return outputResponse;
                    }
                    ThreadPool.QueueUserWorkItem(_ =>
                    {
                        //Gui email pass word
                        StringBuilder content = new StringBuilder();

                        content.Append("Hệ thống tự động đăng ký tài khoản khi bạn đăng nhập bằng google:<br/><br/>");
                        content.Append($"Mật khẩu của bạn là: {randomPassword}");
                        string contentHTML = content.ToString();

                        var requestSendMail = new MailService.EmailModel
                        {
                            ToMail = userInfo.email,
                            Title = "[EDUNET]Hệ thống tự động đăng ký tài khoản",
                            IsResend = false,
                            ServiceID = (int)Enums.MailServiceID.SendMailDefault,
                            Content = contentHTML,
                            Sign = Security.MD5Encrypt($"{userInfo.email}{contentHTML[0]}{ConfigurationManager.AppSettings["SECURE_KEY"]}") //{requestSendMail.ToMail}{requestSendMail.Param}{requestSendMail.MailID}{secretKey}
                        };
                        var resultSendMail = new MailService.SendMailServiceClient().SendEmailManual(requestSendMail);
                    });                    
                    userID = resultReg;
                    userName = userInfo.email;
                    fullName = userInfo.name;
                }
                else
                {
                    userID = basicInfo.UserID;
                    userName = basicInfo.Username;
                    fullName = basicInfo.Fullname;
                }
                //Goi gan ket
                requestGetAssociateUser.UserID = userID;
                requestGetAssociateUser.ClientIP = clientIP;
                var resultAssociateAccount = AbstractFactory.Instance().TopcourseDAO().CreateAssociateUser(requestGetAssociateUser);
                if(resultAssociateAccount < 0)
                {
                    NLogManager.LogMessage($"CreateAssociateUser that bai>>request:{JsonConvert.SerializeObject(requestGetAssociateUser)} >> result: {resultAssociateAccount}");
                    outputResponse.Headers.Location = new Uri(string.Format("{0}?messeger={1}", Utils.Domain, "Đăng nhập thất bại"));
                    return outputResponse;
                }
                string cookieUsername = SessionAccount.SessionName(userID, userName, clientIP, fullName, 1, DateTime.Now.Ticks);
                FormsAuthentication.SetAuthCookie(cookieUsername, true);
                outputResponse.Headers.Location = new Uri(Utils.Domain);
                return outputResponse;
            }
            //Da gan ket
            else
            {
                //Lay basic info theo userid
                var basicInfo = AbstractFactory.Instance().TopcourseDAO().GetBasicInfo(string.Empty, oathAccount.UserID);
                if(basicInfo == null || basicInfo.UserID <= 0)
                {
                    NLogManager.LogMessage($"GetBasicInfo da gan ket that bai>>UserID:{oathAccount.UserID} >> response: {basicInfo}");
                    outputResponse.Headers.Location = new Uri(string.Format("{0}?messeger={1}", Utils.Domain, "Đăng nhập thất bại"));
                    return outputResponse;
                }
                string cookieUsername = SessionAccount.SessionName(basicInfo.UserID, basicInfo.Username, IPAddressHelper.GetClientIP(), basicInfo.Fullname, 1, DateTime.Now.Ticks);
                FormsAuthentication.SetAuthCookie(cookieUsername, true);
                outputResponse.Headers.Location = new Uri(Utils.Domain);
                return outputResponse;
            }                    
        }
       
    }

    public class GoogleHelpers
    {
        public static GoogleUserInfo GetGoogleUserInfo(string code)
        {
            var googleUserInfo = new GoogleUserInfo();
            try
            {
                var googlelogin_callback_url = System.Configuration.ConfigurationManager.AppSettings["GOOGLE_APP_REDIRECT_URIS"];
                var googlelogin_client_id = System.Configuration.ConfigurationManager.AppSettings["GOOGLE_APP_ID"];
                var googlelogin_client_secret = System.Configuration.ConfigurationManager.AppSettings["GOOGLE_APP_KEY"];

                var postdata = string.Format("code={0}&client_id={1}&client_secret={2}&redirect_uri={3}&grant_type=authorization_code", code, googlelogin_client_id, googlelogin_client_secret, googlelogin_callback_url);
                var result = HttpUtils.SendPost(postdata, "https://accounts.google.com/o/oauth2/token");
                var tokenInfo = JsonConvert.DeserializeObject<TokenInfo>(result);
                //NLogManager.LogMessage("tokenInfo:" + result);
                var access_token = tokenInfo.access_token;
                var userInfo = HttpUtils.GetHttpResponse(string.Format("https://www.googleapis.com/oauth2/v1/userinfo?access_token={0}", access_token));
                //NLogManager.LogMessage("GetUserInfo:" + userInfo);
                googleUserInfo = JsonConvert.DeserializeObject<GoogleUserInfo>(userInfo.ToString());
                googleUserInfo.access_token = access_token;
            }
            catch (Exception ex)
            {
                NLogManager.PublishException(ex);
            }
            return googleUserInfo;
        }

    }

    [Serializable]
    public class GoogleUserInfo
    {
        public string id { get; set; }
        public string email { get; set; }
        public string verified_email { get; set; }
        public string name { get; set; }
        public string given_name { get; set; }
        public string family_name { get; set; }
        public string link { get; set; }
        public string picture { get; set; }
        public string gender { get; set; }
        public string locale { get; set; }
        public string access_token { get; set; }
    }


    [Serializable]
    public class TokenInfo
    {
        public string access_token { get; set; }
        public string expires_in { get; set; }
        public string token_type { get; set; }
        public string refresh_token { get; set; }
        public string xoauth_yahoo_guid { get; set; }
    }
}