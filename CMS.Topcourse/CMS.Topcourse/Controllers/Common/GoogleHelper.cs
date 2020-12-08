using System;
using System.Web.Script.Serialization;
using Topcourse.Utility;

namespace CMS.Topcourse.Controllers.Common
{
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
                var result = WebPost.SendPost(postdata, "https://accounts.google.com/o/oauth2/token");
                var tokenInfo = new JavaScriptSerializer().Deserialize<TokenInfo>(result);
                Log4Net.LogInfo("tokenInfo:" + result);
                var access_token = tokenInfo.access_token;
                var userInfo = WebPost.GetHttpResponse(string.Format("https://www.googleapis.com/oauth2/v1/userinfo?access_token={0}", access_token));
                Log4Net.LogInfo("GetUserInfo:" + userInfo);
                googleUserInfo = new JavaScriptSerializer().Deserialize<GoogleUserInfo>(userInfo.ToString());
                googleUserInfo.access_token = access_token;
            }
            catch (Exception ex)
            {
                Log4Net.PublishException(ex);
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