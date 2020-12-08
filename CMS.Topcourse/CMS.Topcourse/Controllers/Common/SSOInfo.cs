using System;
using System.Web;
using System.Web.Security;
using Topcourse.Utility;
using Topcourse.Utility.Security;

namespace CMS.Topcourse.Controllers.Common
{
    public class SSOInfo
    {
        private const string _CookieUserInfo = "eb4a3c8426d5a29404e4e148cc92af13";
        private const string _NewsletterCookiesInfo = "sfg8sugij54ol98guifjg98";
        private readonly string _SSODomain = Config.SSODomain;

        private readonly RijndaelEnhanced rijndaelKey;
        private int _LifeTime = 30;     //15 phut
        private int _UserId;
        private string _UserName = "";
        private bool _IsAdministrator = false;
        private string _SessionID = "";
        private string _ClientIP = "";
        private string _AccessToken = "";
        private bool _IsExpires = true;
        private bool _NewsletterIsExpires = true;

        public int UserId
        {
            get { return _UserId; }
            set { _UserId = value; }
        }
        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }
        public string SessionID
        {
            get { return _SessionID; }
            set { _SessionID = value; }
        }
        public bool IsAdministrator
        {
            get { return _IsAdministrator; }
            set { _IsAdministrator = value; }
        }

        public string ClientIP
        {
            get { return _ClientIP; }
            set { _ClientIP = value; }
        }

        public string AccessToken // dùng cho đồng bộ hệ thống core Pay
        {
            get { return _AccessToken; }
            set { _AccessToken = value; }
        }

        public void LifeTimeSetting(int TimeExpiresSetting)
        {
            _LifeTime = TimeExpiresSetting;
        }

        public SSOInfo()
        {
            _LifeTime = int.Parse(Config.GetAppsetting("CookieTimeout") == "" ? "30" : Config.GetAppsetting("CookieTimeout"));
            rijndaelKey = new RijndaelEnhanced(Key.sKey, "@1B2c3D4e5F6g7H8");
        }

        public SSOInfo(string key)
        {
            _LifeTime = int.Parse(Config.GetAppsetting("CookieTimeout") == "" ? "30" : Config.GetAppsetting("CookieTimeout"));
            rijndaelKey = new RijndaelEnhanced(key, "@1B2c3D4e5F6g7H8");
        }
        public virtual bool IsSigned()
        {
            try
            {
                //this.Get();
                //Log4Net.LogInfo(this._UserName + " - " + this._ClientIP + " - " + this._IsExpires);
                bool bLoged = (this._UserName.Length > 0 && this._ClientIP.Length > 0 && !this._IsExpires);
                if (bLoged)
                {
                    this.SetCookieUserInfo(this._UserId, this._UserName, this._IsAdministrator, this._SessionID, this._AccessToken);
                }
                return bLoged;
            }
            catch (Exception ex)
            {
                Log4Net.PublishException(ex);
                SignOut();

                return false;
            }
        }
        // ReSharper disable once ParameterHidesMember
        public void SignIn(string _UserName)
        {
            SetCookieUserInfo(_UserName, HttpContext.Current.Session.SessionID);
        }
        // ReSharper disable once ParameterHidesMember
        public void SignIn(string _UserName, string _SesionID)
        {
            SetCookieUserInfo(_UserName, _SesionID);
        }
        // ReSharper disable once ParameterHidesMember
        public void SignIn(int _UserId, string _UserName, bool _IsAdministrator, string _SesionID, string accessToken)
        {
            SetCookieUserInfo(_UserId, _UserName, _IsAdministrator, _SesionID, accessToken);
        }
        public virtual void SignOut()
        {
            DelCookieUserInfo();
        }
        public SSOInfo Get()
        {
            try
            {
                string[] CookieValueArray = GetCookieUserInfo();
                int.TryParse(CookieValueArray[0], out _UserId);
                _UserName = CookieValueArray[1];
                bool.TryParse(CookieValueArray[2], out _IsAdministrator); //_IsAdministrator = Convert.ToBoolean();
                _SessionID = CookieValueArray[3];
                _ClientIP = CookieValueArray[4];
                _AccessToken = CookieValueArray[5];
                if (!String.IsNullOrEmpty(CookieValueArray[6]) && DateTime.Parse(CookieValueArray[6]) > DateTime.Now)
                {
                    _IsExpires = false;

                }
            }
            catch (Exception ex)
            {
                Log4Net.PublishException(ex);
                throw;
            }

            return this;
        }

        #region ghi thong tin UserInfo
        public void SetCookie(string CookieName, string CookieValue)
        {
            HttpContext.Current.Response.Cookies.Set(HttpContext.Current.Request.Cookies[CookieName] ?? new HttpCookie(CookieName, ""));
            //HttpContext.Current.Response.Cookies[CookieName].Value = rijndaelKey.Encrypt(CookieValue);
            //if (!_SSODomain.Equals(""))
            //{
            //    System.Web.HttpContext.Current.Response.Cookies[CookieName].Path = "/";
            //    System.Web.HttpContext.Current.Response.Cookies[CookieName].Domain = _SSODomain;
            //}
            //System.Web.HttpContext.Current.Response.Cookies[CookieName].Expires = DateTime.Now.AddMinutes(_LifeTime);
            //System.Web.HttpContext.Current.Response.Cookies[CookieName].HttpOnly = true;
            FormsAuthentication.SetAuthCookie(rijndaelKey.Encrypt(CookieValue), false);
        }
        public string GetCookie(string CookieName)
        {
            if (System.Web.HttpContext.Current.Request.Cookies[CookieName] != null)
            {
                return rijndaelKey.Decrypt(System.Web.HttpContext.Current.Request.Cookies[CookieName].Value.ToString());
            }
            else
                return "";
        }
        private void SetCookieUserInfo(string UserName, string SessionID)
        {

            string CookieValue = "";
            CookieValue += UserName;
            CookieValue += "|" + SessionID;
            CookieValue += "|" + Config.GetIP();
            CookieValue += "|" + DateTime.Now.AddMinutes(_LifeTime);



            if (System.Web.HttpContext.Current.Request.Cookies[_CookieUserInfo] != null)
                System.Web.HttpContext.Current.Response.Cookies.Set(System.Web.HttpContext.Current.Request.Cookies[_CookieUserInfo]);
            else
                System.Web.HttpContext.Current.Response.Cookies.Set(new HttpCookie(_CookieUserInfo, ""));

            System.Web.HttpContext.Current.Response.Cookies[_CookieUserInfo].Value = rijndaelKey.Encrypt(CookieValue);
            if (!_SSODomain.Equals(""))
            {
                System.Web.HttpContext.Current.Response.Cookies[_CookieUserInfo].Path = "/";
                System.Web.HttpContext.Current.Response.Cookies[_CookieUserInfo].Domain = _SSODomain;
            }
            System.Web.HttpContext.Current.Response.Cookies[_CookieUserInfo].Expires = DateTime.Now.AddMinutes(_LifeTime);
            System.Web.HttpContext.Current.Response.Cookies[_CookieUserInfo].HttpOnly = true;
            //DBUsers m_Users = new DBUsers();
            //m_Users.UpdateLog(UserName);
        }
        private void SetCookieUserInfo(int UserId, string UserName, bool IsAdministrator, string SessionID, string accesstoken)
        {
            string CookieValue = "";
            CookieValue += UserId;
            CookieValue += "|" + UserName;
            CookieValue += "|" + IsAdministrator;
            CookieValue += "|" + SessionID;
            CookieValue += "|" + Config.GetIP();
            CookieValue += "|" + accesstoken;
            CookieValue += "|" + DateTime.Now.AddMinutes(_LifeTime);

            //if (System.Web.HttpContext.Current.Request.Cookies[_CookieUserInfo] != null)
            //    System.Web.HttpContext.Current.Response.Cookies.Set(System.Web.HttpContext.Current.Request.Cookies[_CookieUserInfo]);
            //else
            //    System.Web.HttpContext.Current.Response.Cookies.Set(new HttpCookie(_CookieUserInfo, ""));

            //System.Web.HttpContext.Current.Response.Cookies[_CookieUserInfo].Value = rijndaelKey.Encrypt(CookieValue);
            //if (!_SSODomain.Equals(""))
            //{
            //    System.Web.HttpContext.Current.Response.Cookies[_CookieUserInfo].Path = "/";
            //    System.Web.HttpContext.Current.Response.Cookies[_CookieUserInfo].Domain = _SSODomain;
            //}
            //System.Web.HttpContext.Current.Response.Cookies[_CookieUserInfo].Expires = DateTime.Now.AddMinutes(_LifeTime);
            //System.Web.HttpContext.Current.Response.Cookies[_CookieUserInfo].HttpOnly = true;
            FormsAuthentication.SetAuthCookie(rijndaelKey.Encrypt(CookieValue), false);

        }
        private string[] GetCookieUserInfo()
        {
            string[] CookieValue = new string[0];
            CookieValue = "||||||".Split(new char[] { '|' });
            try
            {
                if (System.Web.HttpContext.Current.Request.Cookies[_CookieUserInfo] != null)
                {
                    //CookieValue = rijndaelKey.Decrypt(System.Web.HttpContext.Current.Request.Cookies[_CookieUserInfo].Value.ToString()).Split(new char[] { '|' });
                    return CookieValue;
                }
                //else
                //    CookieValue = "|||||".Split(new char[] { '|' });
                var current = new object();
                var isLogin = HttpContext.Current.User.Identity.Name;
                if (string.IsNullOrEmpty(isLogin)) return CookieValue;
                current = isLogin;
                if (current == null) return CookieValue;
                CookieValue = rijndaelKey.Decrypt(current.ToString()).Split(new char[] { '|' });
            }
            catch (Exception ex)
            {
                Log4Net.LogInfo("_CookieUserInfo Exception null");
                CookieValue = "||||||".Split(new char[] { '|' });
                Log4Net.PublishException(ex);
            }
            return CookieValue;
        }
        private void DelCookieUserInfo()
        {
            System.Web.Security.FormsAuthentication.SignOut();
            for (var i = 0; i < HttpContext.Current.Request.Cookies.Count; i++)
            {
                var cookies = HttpContext.Current.Request.Cookies.Get(i);
                if (cookies.Name == FormsAuthentication.FormsCookieName) continue;
                cookies.Expires = DateTime.Now.AddDays(-1);
                cookies.Value = string.Empty;
                HttpContext.Current.Response.Cookies.Set(cookies);
            }
            foreach (string cookie in HttpContext.Current.Request.Cookies.AllKeys)
            {
                if (cookie == System.Web.Security.FormsAuthentication.FormsCookieName) continue;
                HttpContext.Current.Response.Cookies.Add(new HttpCookie(cookie) { Expires = DateTime.Now.AddMonths(-1), Path = "/" });
            }
            HttpContext.Current.Session.RemoveAll();
        }
        #endregion

        #region Newsletter Cookies Info
        public void NewsletterSignIn(string Username)
        {
            this.SetNewsletterCookiesInfo(Username);
        }
        public virtual void NewsletterSignOut()
        {
            DelNewsletterCookiesInfo();
        }
        public virtual bool NewsletterIsSigned()
        {
            this.NewsletterGet();
            bool bLoged = (this._UserName.Length > 0 && this._ClientIP.Length > 0 && !this._NewsletterIsExpires);
            if (bLoged)
            {
                this.SetNewsletterCookiesInfo(this._UserName);
            }
            return bLoged;
        }
        private void SetNewsletterCookiesInfo(string Username)
        {
            string CookieValue = "";
            CookieValue += Username;
            CookieValue += "|" + Config.GetIP();
            CookieValue += "|" + DateTime.Now.AddMinutes(_LifeTime);

            if (System.Web.HttpContext.Current.Request.Cookies[_NewsletterCookiesInfo] != null)
                System.Web.HttpContext.Current.Response.Cookies.Set(System.Web.HttpContext.Current.Request.Cookies[_NewsletterCookiesInfo]);
            else
                System.Web.HttpContext.Current.Response.Cookies.Set(new HttpCookie(_NewsletterCookiesInfo, ""));

            System.Web.HttpContext.Current.Response.Cookies[_NewsletterCookiesInfo].Value = rijndaelKey.Encrypt(CookieValue);
            if (!_SSODomain.Equals(""))
            {
                System.Web.HttpContext.Current.Response.Cookies[_NewsletterCookiesInfo].Path = "/";
                System.Web.HttpContext.Current.Response.Cookies[_NewsletterCookiesInfo].Domain = _SSODomain;
            }
            System.Web.HttpContext.Current.Response.Cookies[_NewsletterCookiesInfo].Expires = DateTime.Now.AddMinutes(_LifeTime);
            System.Web.HttpContext.Current.Response.Cookies[_NewsletterCookiesInfo].HttpOnly = true;
        }
        private string[] GetNewsletterCookiesInfo()
        {
            string[] CookieValue = new string[0];
            if (System.Web.HttpContext.Current.Request.Cookies[_NewsletterCookiesInfo] != null)
            {
                try
                {
                    CookieValue = rijndaelKey.Decrypt(System.Web.HttpContext.Current.Request.Cookies[_NewsletterCookiesInfo].Value.ToString()).Split(new char[] { '|' });
                }
                catch
                {
                    CookieValue = "|||".Split(new char[] { '|' });
                }
            }
            else
                CookieValue = "|||".Split(new char[] { '|' });
            return CookieValue;
        }
        private void DelNewsletterCookiesInfo()
        {
            if (System.Web.HttpContext.Current.Request.Cookies[_NewsletterCookiesInfo] != null)
            {
                if (!_SSODomain.Equals(""))
                {
                    System.Web.HttpContext.Current.Response.Cookies[_NewsletterCookiesInfo].Domain = _SSODomain;
                }
                System.Web.HttpContext.Current.Response.Cookies[_NewsletterCookiesInfo].Expires = DateTime.Now.AddMonths(-1);
            }
        }
        public SSOInfo NewsletterGet()
        {
            string[] CookieValueArray = GetNewsletterCookiesInfo();
            this._UserName = CookieValueArray[0];
            this._ClientIP = CookieValueArray[1];
            if (!String.IsNullOrEmpty(CookieValueArray[2]) && DateTime.Parse(CookieValueArray[2]) > DateTime.Now)
            {
                this._NewsletterIsExpires = false;
            }
            return this;
        }
        #endregion
    }
}