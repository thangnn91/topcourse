using System.Linq;
using System.Web;
using System.Collections.Generic;
using System;

namespace Common.Utilities.IpAddress
{
    public static class IPAddressHelper
    {
        static string HTTP_X_FORWARDED_FOR_ALLOW = "";
        static string HTTP_X_FORWARDED_FOR = "";

        static string WhiteListIPs = "";
        static string BlackListIPs = "";
        static List<string> WHITE_LIST_IPS = new List<string>();
        static List<string> BLACK_LIST_IPS = new List<string>();

        static IPAddressHelper()
        {
            if (!string.IsNullOrEmpty(WhiteListIPs))
                WHITE_LIST_IPS = WhiteListIPs.Split(',').ToList<string>();
            if (!string.IsNullOrEmpty(BlackListIPs))
                BLACK_LIST_IPS = BlackListIPs.Split(',').ToList<string>();
        }
        public static string GetIp()
        {
            var ip = string.Empty;
            try
            {
                if (string.IsNullOrEmpty(ip))
                {
                    if (HttpContext.Current.Request.Headers["HTTP_X_FORWARDED_FOR"] != null)
                    {
                        ip = HttpContext.Current.Request.Headers["HTTP_X_FORWARDED_FOR"];
                        if (!string.IsNullOrEmpty(ip))
                            ip = ip.Split(',').Last().Trim();
                    }
                    if (ip == "")
                    {
                        ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                    }
                }
            }
            catch
            {
                return string.Empty;
            }

            return ip.Replace('|', '#').Trim();
        }
        //public static string GetClientIP()
        //{
        //    string IP = string.Empty;

        //    try
        //    {
        //        if (!string.IsNullOrEmpty(HTTP_X_FORWARDED_FOR) && HTTP_X_FORWARDED_FOR.ToLower().Equals("true"))
        //        {
        //            IP = HttpContext.Current.Request.Headers["HTTP_X_FORWARDED_FOR"];
        //            //      IP = HttpContext.Current.Request.Headers["HTTP_X_FORWARDED_FOR"];
        //            if (!string.IsNullOrEmpty(IP))
        //            {
        //                IP = IP.Split(',').Last().Trim();
        //                return IP.Replace('|', '#').Trim();
        //            }
        //            if (string.IsNullOrEmpty(IP) && HttpContext.Current.Request.Url.Host.IndexOf("localhost") >= 0)
        //            {
        //                IP = "127.0.0.1";
        //            }

        //            if (string.IsNullOrEmpty(IP) && HttpContext.Current.Request.Headers["HTTP_CITRIX"] != null)
        //            {
        //                IP = HttpContext.Current.Request.Headers["HTTP_CITRIX"];
        //            }

        //            if (string.IsNullOrEmpty(IP) && HttpContext.Current.Request.Headers["CITRIX_CLIENT_HEADER"] != null)
        //            {
        //                IP = HttpContext.Current.Request.Headers["CITRIX_CLIENT_HEADER"];
        //            }
        //        }

        //        if (string.IsNullOrEmpty(IP) && HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"] != null)
        //        {
        //            IP = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        //        }

        //    }
        //    catch
        //    {
        //    }

        //    var ips = IP.Split(',', ':', ';');
        //    if (ips.Length > 1)
        //        IP = ips[ips.Length - 1];

        //    return IP.Replace('|', '#').Trim();
        //}
        //public static string GetClientIP()
        //{
        //    string str = string.Empty;
        //    try
        //    {
        //        str = HttpContext.Current.Request.Headers["HTTP_X_FORWARDED_FOR"];
        //        if (!string.IsNullOrEmpty(str))
        //        {
        //            char[] separator = new char[] { ',' };
        //            str = str.Split(separator).Last<string>().Trim();
        //        }
        //        if (string.IsNullOrEmpty(str) && (HttpContext.Current.Request.Url.Host.IndexOf("localhost") >= 0))
        //        {
        //            str = "127.0.0.1";
        //        }
        //        if (string.IsNullOrEmpty(str) && HttpContext.Current.Request.Headers["HTTP_CITRIX"] != null)
        //        {
        //            str = HttpContext.Current.Request.Headers["HTTP_CITRIX"];
        //        }
        //        if (string.IsNullOrEmpty(str) && HttpContext.Current.Request.Headers["CITRIX_CLIENT_HEADER"] != null)
        //        {
        //            str = HttpContext.Current.Request.Headers["CITRIX_CLIENT_HEADER"];
        //        }
        //        if (string.IsNullOrEmpty(str) && HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"] != null)
        //        {
        //            str = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        //        }
        //    }
        //    catch
        //    {
        //    }
        //    string[] strArray = str.Split(new char[] { ',', ':', ';' });
        //    if (strArray.Length > 1)
        //    {
        //        str = strArray[0];
        //    }
        //    return str.Replace('|', '#').Trim();
        //}
        public static string GetClientIP()
        {
            string IP = string.Empty;
            try
            {
                if (!string.IsNullOrEmpty(HTTP_X_FORWARDED_FOR_ALLOW) && HTTP_X_FORWARDED_FOR_ALLOW.Equals("true"))
                {
                    if (string.IsNullOrEmpty(IP) && HttpContext.Current.Request.Headers["HTTP_X_FORWARDED_FOR"] != null)
                        IP = HttpContext.Current.Request.Headers["HTTP_X_FORWARDED_FOR"];

                    if (string.IsNullOrEmpty(IP) && HttpContext.Current.Request.Headers["HTTP_CITRIX"] != null)
                        IP = HttpContext.Current.Request.Headers["HTTP_CITRIX"];

                    if (string.IsNullOrEmpty(IP) && HttpContext.Current.Request.Headers["CITRIX_CLIENT_HEADER"] != null)
                        IP = HttpContext.Current.Request.Headers["CITRIX_CLIENT_HEADER"];

                    if (!string.IsNullOrEmpty(IP))
                    {
                        var ips = IP.Split(',', ':', ';');
                        if (ips.Length > 0)
                            IP = ips[ips.Length - 1];
                    }
                }

                if (string.IsNullOrEmpty(IP) && HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"] != null)
                    IP = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                if (string.IsNullOrEmpty(IP))
                    IP = HttpContext.Current.Request.UserHostAddress;
            }
            catch (Exception ex)
            {
            }
            return IP.Replace('|', '#').Trim();
        }


        public static bool IsWhiteListIP(string clientIP)
        {
            if (WHITE_LIST_IPS.Count == 0)
                return false;

            if (WHITE_LIST_IPS.Any((ip) => { return ip.Equals("*"); }))
                return true;

            return WHITE_LIST_IPS.Any((ip) => { return ip.Equals(clientIP); });
        }

        public static bool IsBlackListIP(string clientIP)
        {
            if (BLACK_LIST_IPS.Count == 0)
                return false;

            if (BLACK_LIST_IPS.Any((ip) => { return ip.Equals("*"); }))
                return true;

            return BLACK_LIST_IPS.Any((ip) => { return ip.Equals(clientIP); });
        }

        public static List<string> GetWhiteListIP()
        {
            return WHITE_LIST_IPS;
        }
    }
}
