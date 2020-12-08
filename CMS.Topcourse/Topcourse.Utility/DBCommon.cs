using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Web.Configuration;
using System.Web;
using System.Web.Security;
using System.Text.RegularExpressions;
using Topcourse.Utility;
using Topcourse.Utility;


namespace Topcourse.Utility
{
    public sealed class DBCommon
    {
        static bool ValidateIP(string strIPAddress)
        {
            string IPTestExp = @"(([1-9]?[0-9]|1[0-9]{2}|2[0-4][0-9]|255[0-5])\.){3}([1-9]?[0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])";
            if (Regex.Match(strIPAddress, IPTestExp).Success)
                return true;
            else
            {
                return false;
            }
        }
        public static string ClientIP
        {
            get
            {
                string IP = "";

                if (HttpContext.Current.Request.ServerVariables["HTTP_CLIENT_IP"] != null)
                {
                    IP = HttpContext.Current.Request.ServerVariables["HTTP_CLIENT_IP"];
                    return IP;
                }

                if (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
                {
                    IP = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                    return IP;
                }

                if (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED"] != null)
                {
                    IP = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED"];
                    return IP;
                }

                if (HttpContext.Current.Request.ServerVariables["HTTP_X_CLUSTER_CLIENT_IP"] != null)
                {
                    IP = HttpContext.Current.Request.ServerVariables["HTTP_X_CLUSTER_CLIENT_IP"];
                    return IP;
                }

                if (HttpContext.Current.Request.ServerVariables["HTTP_FORWARDED_FOR"] != null)
                {
                    IP = HttpContext.Current.Request.ServerVariables["HTTP_FORWARDED_FOR"];
                    return IP;
                }

                if (HttpContext.Current.Request.ServerVariables["HTTP_FORWARDED"] != null)
                {
                    IP = HttpContext.Current.Request.ServerVariables["HTTP_FORWARDED"];
                    return IP;
                }

                if (IP == "")
                {
                    IP = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                }
                return IP;
            }
        }
        public string XSSFilter(string sValue)
        {
            string sTemp = "?=:/._-0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            string sOut = "";
            for (int i = 0; i < sValue.Length; i++)
            {
                if (sTemp.IndexOf(sValue[i]) >= 0)
                {
                    sOut += sValue[i];
                }
            }

            return sOut;
        }
        public static string SiteName
        {
            get
            {
                string sRet = Config.SiteName;
                if (string.IsNullOrEmpty(sRet))
                {
                    string sDomain = HttpContext.Current.Request.Url.Host.Trim().ToLower();
                    if (sDomain.IndexOf("localhost") < 0)
                    {
                        sRet = sDomain.Replace("." + Config.DomainName, "");
                    }
                    else
                    {
                        sRet = "www";
                    }
                }
                return sRet;
            }
        }
        public static String UCS2Convert(String sContent)
        {
            sContent = sContent.Trim();
            String sUTF8Lower = "a|á|à|ả|ã|ạ|ă|ắ|ằ|ẳ|ẵ|ặ|â|ấ|ầ|ẩ|ẫ|ậ|đ|e|é|è|ẻ|ẽ|ẹ|ê|ế|ề|ể|ễ|ệ|i|í|ì|ỉ|ĩ|ị|o|ó|ò|ỏ|õ|ọ|ô|ố|ồ|ổ|ỗ|ộ|ơ|ớ|ờ|ở|ỡ|ợ|u|ú|ù|ủ|ũ|ụ|ư|ứ|ừ|ử|ữ|ự|y|ý|ỳ|ỷ|ỹ|ỵ";

            String sUTF8Upper = "A|Á|À|Ả|Ã|Ạ|Ă|Ắ|Ằ|Ẳ|Ẵ|Ặ|Â|Ấ|Ầ|Ẩ|Ẫ|Ậ|Đ|E|É|È|Ẻ|Ẽ|Ẹ|Ê|Ế|Ề|Ể|Ễ|Ệ|I|Í|Ì|Ỉ|Ĩ|Ị|O|Ó|Ò|Ỏ|Õ|Ọ|Ô|Ố|Ồ|Ổ|Ỗ|Ộ|Ơ|Ớ|Ờ|Ở|Ỡ|Ợ|U|Ú|Ù|Ủ|Ũ|Ụ|Ư|Ứ|Ừ|Ử|Ữ|Ự|Y|Ý|Ỳ|Ỷ|Ỹ|Ỵ";

            String sUCS2Lower = "a|a|a|a|a|a|a|a|a|a|a|a|a|a|a|a|a|a|d|e|e|e|e|e|e|e|e|e|e|e|e|i|i|i|i|i|i|o|o|o|o|o|o|o|o|o|o|o|o|o|o|o|o|o|o|u|u|u|u|u|u|u|u|u|u|u|u|y|y|y|y|y|y";

            String sUCS2Upper = "A|A|A|A|A|A|A|A|A|A|A|A|A|A|A|A|A|A|D|E|E|E|E|E|E|E|E|E|E|E|E|I|I|I|I|I|I|O|O|O|O|O|O|O|O|O|O|O|O|O|O|O|O|O|O|U|U|U|U|U|U|U|U|U|U|U|U|Y|Y|Y|Y|Y|Y";

            String[] aUTF8Lower = sUTF8Lower.Split(new Char[] { '|' });

            String[] aUTF8Upper = sUTF8Upper.Split(new Char[] { '|' });

            String[] aUCS2Lower = sUCS2Lower.Split(new Char[] { '|' });

            String[] aUCS2Upper = sUCS2Upper.Split(new Char[] { '|' });

            Int32 nLimitChar;

            nLimitChar = aUTF8Lower.GetUpperBound(0);

            for (int i = 1; i <= nLimitChar; i++)
            {

                sContent = sContent.Replace(aUTF8Lower[i], aUCS2Lower[i]);

                sContent = sContent.Replace(aUTF8Upper[i], aUCS2Upper[i]);

            }
            string sUCS2regex = @"[A-Za-z0-9- ]";
            string sEscaped = new Regex(sUCS2regex, RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.ExplicitCapture).Replace(sContent, string.Empty);
            if (string.IsNullOrEmpty(sEscaped))
                return sContent;
            sEscaped = sEscaped.Replace("[", "\\[");
            sEscaped = sEscaped.Replace("]", "\\]");
            sEscaped = sEscaped.Replace("^", "\\^");
            string sEscapedregex = @"[" + sEscaped + "]";
            return new Regex(sEscapedregex, RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.ExplicitCapture).Replace(sContent, string.Empty);
        }
        public static string StripHtml(string Html)
        {
            //Stripts the <script> tags from the Html
            string scriptregex = @"<scr" + @"ipt[^>.]*>[\s\S]*?</sc" + @"ript>";
            System.Text.RegularExpressions.Regex scripts = new System.Text.RegularExpressions.Regex(scriptregex, RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.ExplicitCapture);
            string scriptless = scripts.Replace(Html, " ");

            //Stripts the <style> tags from the Html
            string styleregex = @"<style[^>.]*>[\s\S]*?</style>";
            System.Text.RegularExpressions.Regex styles = new System.Text.RegularExpressions.Regex(styleregex, RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.ExplicitCapture);
            string styleless = styles.Replace(scriptless, " ");
            //Strips the HTML tags from the Html
            System.Text.RegularExpressions.Regex objRegExp = new System.Text.RegularExpressions.Regex("<(.|\n)+?>", RegexOptions.IgnoreCase);

            //Replace all HTML tag matches with the empty string
            string strOutput = objRegExp.Replace(styleless, " ");

            // Convert &&amp;amp;eacute; to &amp;eacute; (e') so French words are indexable
            // ## UNDOCUMENTED ## this line is new in Version 2, but was not documented
            // in the article... I may explain it when writing about Version 3...
            ExtendedHtmlUtility ExtHtml = new ExtendedHtmlUtility();
            strOutput = ExtHtml.HtmlEntityDecode(strOutput, false);
            // The above line can be safely commented out on most English pages
            // since it's unlikely any 'important' characters would be HtmlEncoded

            //Replace all < and > with &lt; and &gt;
            strOutput = strOutput.Replace("<", "&lt;");
            strOutput = strOutput.Replace(">", "&gt;");

            objRegExp = null;
            return strOutput;
        }
        public static string StripHtml(string Html, string tagHtml)
        {
            //Stripts the <tagHtml> tags from the Html
            string tagBegin = @"<" + tagHtml + @"[\s\S]*?>";
            System.Text.RegularExpressions.Regex tagStripBegin = new System.Text.RegularExpressions.Regex(tagBegin, RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.ExplicitCapture);
            string strBegin = tagStripBegin.Replace(Html, string.Empty);

            string tagEnd = @"</" + tagHtml + ">";
            System.Text.RegularExpressions.Regex tagStripEnd = new System.Text.RegularExpressions.Regex(tagEnd, RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.ExplicitCapture);
            string strEnd = tagStripEnd.Replace(strBegin, string.Empty);

            return strEnd;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Html"></param>
        /// <param name="tagHtmls">If length == 0 is default tags</param>
        /// <returns></returns>
        public static string StripHtml(string Html, string[] tagHtmls)
        {
            //Stripts the <tagHtml> tags from the Html
            string tagBegin = "";
            string tagEnd = "";
            string strBegin = "";
            if (tagHtmls.Length == 0)
            {
                tagHtmls = new string[] { "input", "form", "script", "div", "table", "p", "hr", "ul", "li", "img", "table", "tbody", "tr", "td", "th" };
            }
            System.Text.RegularExpressions.Regex tagStripEnd;
            foreach (string tagHtml in tagHtmls)
            {
                tagBegin = @"<" + tagHtml + @"[\s\S]*?>";
                System.Text.RegularExpressions.Regex tagStripBegin = new System.Text.RegularExpressions.Regex(tagBegin, RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.ExplicitCapture);
                strBegin = tagStripBegin.Replace(Html, string.Empty);

                tagEnd = @"</" + tagHtml + ">";
                tagStripEnd = new System.Text.RegularExpressions.Regex(tagEnd, RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.ExplicitCapture);
                Html = tagStripEnd.Replace(strBegin, string.Empty);
            }
            return Html;
        }

        public static string FixHtml(string Html)
        {
            string strOutput = Html;
            //Stripts the <p> tags from the Html
            string pregex = @"<p[^>.]*>&nbsp;</p>";
            System.Text.RegularExpressions.Regex p = new System.Text.RegularExpressions.Regex(pregex, RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.ExplicitCapture);
            strOutput = p.Replace(strOutput, "<br>");

            //Stripts the <link> tags from the Html
            string linkregex = @"<link[\s\S]*?>";
            System.Text.RegularExpressions.Regex link = new System.Text.RegularExpressions.Regex(linkregex, RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.ExplicitCapture);
            strOutput = link.Replace(strOutput, string.Empty);

            //Stripts the <style> tags from the Html
            string styleregex = @"<style[^>.]*>[\s\S]*?</style>";
            System.Text.RegularExpressions.Regex styles = new System.Text.RegularExpressions.Regex(styleregex, RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.ExplicitCapture);
            strOutput = styles.Replace(strOutput, string.Empty);

            //Stripts the [if tags from the Html
            string ifregex = @"<!--[^>.]*>[\s\S]*?<![^>.]*-->";
            System.Text.RegularExpressions.Regex iftag = new System.Text.RegularExpressions.Regex(ifregex, RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.ExplicitCapture);
            strOutput = iftag.Replace(strOutput, string.Empty);
            return strOutput;
        }
        public static string AutoTagHTML(string source)
        {
            try
            {
                string result;
                result = StripHtml(source.ToLower()).Replace("\"", ",");
                result = result.Replace("“", ",");
                result = result.Replace("”", ",");

                result = result.Replace("'", ",");
                result = result.Replace(".", ",");
                result = result.Replace("?", ",");
                result = result.Replace("!", ",");

                do
                {
                    result = result.Replace("  ", " ");
                } while (result.IndexOf("  ") > 0);
                string[] aresult = result.Split(" ".ToCharArray());
                string sDon_Am = "cần|về|quá|vì|bị|do|làm|nhưng|cùng|một|hai|ba|như|sau|không|mà|các|lên|hoặc|giành|này|nhận|ngày|từ|thay|đều|vừa|gì|theo|cho|mới|của|sẽ|trên|và|đang|theo|của|rất|muốn|có|được|với|cả|đến|những|tại|ở|là|của|khi|còn|cũng|vì|có|trong|theo|tại|vào|";
                for (int i = 0; i < aresult.Length; i++)
                {
                    if ((sDon_Am.IndexOf(aresult[i] + "|") >= 0))
                    {
                        aresult[i] = ",";
                    }
                }

                result = "";
                for (int i = 0; i < aresult.Length; i++)
                {
                    result = result + " " + aresult[i];
                }
                aresult = result.Split(",".ToCharArray());

                result = "";
                string sTmp = "";
                for (int i = aresult.Length - 1; i > 0; i--)
                {
                    sTmp = aresult[i].Trim();
                    while (sTmp.StartsWith(","))
                    {
                        sTmp = sTmp.Remove(0, 1);
                    }
                    while (sTmp.EndsWith(","))
                    {
                        sTmp = sTmp.Remove(sTmp.Length - 1, 1);
                    }
                    if (sTmp.Trim().Length > 2) result = result + ", " + sTmp.Trim();
                }
                while (result.StartsWith(","))
                {
                    result = result.Remove(0, 1);
                }
                while (result.EndsWith(","))
                {
                    result = result.Remove(result.Length - 1, 1);
                }
                return result.Trim();
            }
            catch
            {
                return source;
            }
        }
        public static string SubString(string sSource, int length)
        {
            if (string.IsNullOrEmpty(sSource))
                return string.Empty;
            if (sSource.Length <= length)
                return sSource;

            string mSource = sSource;
            int nLength = length;

            int m = mSource.Length;
            while (nLength > 0 && mSource[nLength].ToString() != " ")
            {
                nLength--;
            }
            mSource = mSource.Substring(0, nLength);
            return mSource + "...";
        }
        public static string MakeRewriteUrl(string rewriteUrl, int cateId)
        {
            while (rewriteUrl.StartsWith("/"))
            {
                rewriteUrl = rewriteUrl.Remove(0, 1);
            }
            while (rewriteUrl.EndsWith("/"))
            {
                rewriteUrl = rewriteUrl.Remove(rewriteUrl.Length - 1, 1);
            }
            UrlUtils m_UrlUtils = new UrlUtils(cateId);
            return Config.UrlRoot + m_UrlUtils.Encode() + "/" + rewriteUrl + "/index.htm";
        }
        public static string MakeRewriteUrl(string rewriteUrl, int cateId, long itemId)
        {
            if (rewriteUrl.StartsWith("/"))
            {
                rewriteUrl = rewriteUrl.Remove(0, 1);
            }
            if (rewriteUrl.EndsWith("/"))
            {
                rewriteUrl = rewriteUrl.Remove(rewriteUrl.Length - 1, 1);
            }
            UrlUtils m_UrlUtils = new UrlUtils(cateId, itemId);
            return Config.UrlRoot + m_UrlUtils.Encode() + "/" + rewriteUrl + ".htm";
        }
        public static string MakeRewriteUrl_Support(string rewriteUrl, int cateId, long itemId)
        {
            if (rewriteUrl.StartsWith("/"))
            {
                rewriteUrl = rewriteUrl.Remove(0, 1);
            }
            if (rewriteUrl.EndsWith("/"))
            {
                rewriteUrl = rewriteUrl.Remove(rewriteUrl.Length - 1, 1);
            }
            UrlUtils m_UrlUtils = new UrlUtils(cateId, itemId);
            return m_UrlUtils.Encode() + "/" + rewriteUrl + ".htm";
        }
        public static string MakeLauncherRewriteUrl(string rewriteUrl, int cateId, long itemId)
        {
            if (rewriteUrl.StartsWith("/"))
            {
                rewriteUrl = rewriteUrl.Remove(0, 1);
            }
            if (rewriteUrl.EndsWith("/"))
            {
                rewriteUrl = rewriteUrl.Remove(rewriteUrl.Length - 1, 1);
            }
            UrlUtils m_UrlUtils = new UrlUtils(cateId, itemId);
            return "http://boomspeed.zooz.vn/news/" + m_UrlUtils.Encode() + "/" + rewriteUrl + ".htm";
        }
        public static string MakeNextPageRewriteUrl(string rewriteUrl, int cateId, int CurrPage)
        {
            while (rewriteUrl.StartsWith("/"))
            {
                rewriteUrl = rewriteUrl.Remove(0, 1);
            }
            while (rewriteUrl.EndsWith("/"))
            {
                rewriteUrl = rewriteUrl.Remove(rewriteUrl.Length - 1, 1);
            }
            UrlUtils m_UrlUtils = new UrlUtils(cateId, CurrPage);
            return Config.UrlRoot + m_UrlUtils.Encode() + "/" + rewriteUrl + "/index.htm";
        }


        public static string MakeRewriteUrlForArticle(string rewriteUrl, long itemId)
        {
            while (rewriteUrl.StartsWith("/"))
            {
                rewriteUrl = rewriteUrl.Remove(0, 1);
            }
            while (rewriteUrl.EndsWith("/"))
            {
                rewriteUrl = rewriteUrl.Remove(rewriteUrl.Length - 1, 1);
            }
            return rewriteUrl + "/t" + itemId + ".htm";
        }
    }
}
