using System;
using System.Web;

namespace Web.Topcourse.Helper
{
    public class SessionAccount
    {
        public static int UserID
        {
            get
            {
                int userId = 0;

                if (HttpContext.Current != null && HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    var s = HttpContext.Current.User.Identity.Name.Split('|');

                    Int32.TryParse(s[0], out userId);
                }

                return userId;
            }
        }

        public static string UserName
        {
            get
            {
                string userName = string.Empty;

                if (HttpContext.Current != null && HttpContext.Current.User.Identity.IsAuthenticated && !string.IsNullOrEmpty(HttpContext.Current.User.Identity.Name))
                {
                    var s = HttpContext.Current.User.Identity.Name.Split('|');
                    if (s != null && s.Length > 1)
                        userName = s[1];
                }

                return userName;
            }
        }
        public static string IpAddress
        {
            get
            {
                string ipAddress = string.Empty;

                if (HttpContext.Current != null && HttpContext.Current.User.Identity.IsAuthenticated && !string.IsNullOrEmpty(HttpContext.Current.User.Identity.Name))
                {
                    var s = HttpContext.Current.User.Identity.Name.Split('|');
                    if (s != null && s.Length > 2)
                        ipAddress = s[2];
                }

                return ipAddress;
            }
        }

        public static string FullName
        {
            get
            {

                string fullName = string.Empty;

                if (HttpContext.Current != null && HttpContext.Current.User.Identity.IsAuthenticated && !string.IsNullOrEmpty(HttpContext.Current.User.Identity.Name))
                {
                    var s = HttpContext.Current.User.Identity.Name.Split('|');
                    if (s != null && s.Length > 3)
                        fullName = s[3];
                }

                return fullName;
            }
        }
        public static byte UserType
        {
            get
            {

                byte userType = 0;

                if (HttpContext.Current != null && HttpContext.Current.User.Identity.IsAuthenticated && !string.IsNullOrEmpty(HttpContext.Current.User.Identity.Name))
                {
                    var s = HttpContext.Current.User.Identity.Name.Split('|');
                    if (s != null && s.Length > 4)
                        userType = Convert.ToByte(s[4]);
                }
                return userType;
            }
        }
        public static string SessionName(int userID, string userName, string ipAddress = "", string fullName = "", byte userType = 1, long loginTime = 0)
        {
            return $"{userID}|{userName}|{ipAddress}|{fullName}|{userType}|{loginTime}";
        }
    }
}