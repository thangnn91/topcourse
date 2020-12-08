using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Topcourse.Utility
{
    public static class WebPost
    {
        /// <summary>
        /// Check Validate khi xác minh website tích hợp trong chức năng tích hợp website Paygate 3 - Comment by: NhatND 2011/11/25
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        static public bool CheckValidUrl(string url)
        {
            try
            {
                //Creating the HttpWebRequest
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                //Setting the Request method HEAD, you can also use GET too.
                request.Method = "GET";
                //Getting the Web Response.
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                //Returns TURE if the Status code == 200
                return (response.StatusCode != HttpStatusCode.RequestTimeout &&
                   response.StatusCode != HttpStatusCode.BadGateway &&
                   response.StatusCode != HttpStatusCode.GatewayTimeout);
            }
            catch (WebException webEx)
            {
                if (webEx.Response != null)
                {
                    using (HttpWebResponse exResponse = (HttpWebResponse)webEx.Response)
                    {
                        return (exResponse.StatusCode != HttpStatusCode.RequestTimeout &&
                          exResponse.StatusCode != HttpStatusCode.BadGateway &&
                          exResponse.StatusCode != HttpStatusCode.GatewayTimeout);
                    }
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                Log4Net.LogInfo(url + "\t" + ex);
                return false;
            }
        }

        // Post ket qua ve cho doi tac tich hop cong
        static public bool PostToMerchant(string data, string sign, string url)
        {
            bool SendResult = false;
            string contents = string.Empty;
            try
            {
                contents = "urlpost=" + HttpUtility.UrlEncode(url) + "&data=" + HttpUtility.UrlEncode(data);
                contents += "&sign=" + HttpUtility.UrlEncode(sign);
                //byte[] contentPost = System.Text.Encoding.UTF8.GetBytes(contents);
                string urlPost = System.Configuration.ConfigurationManager.AppSettings["VTCApp_PaymentDone_PostNotify"];

                string resultPost = SendPostNotifyMerchant(contents, urlPost, ref SendResult);
                Log4Net.LogInfo("SendPostNotifyMerchant > content:" + contents + "|SendResult:" + SendResult + "|resultPost:" + resultPost);
                //SendResult = SendNotifyStateChangeOrderNew(url, contentPost);

            }
            catch (Exception ex)
            {
                Log4Net.LogInfo("Error post.Data: " + contents + "|sign: " + sign
                 + Environment.NewLine + "url: " + url
                 + Environment.NewLine + "Exception detail: " + ex);
            }

            if (!SendResult)
            {
                Log4Net.LogInfo("Cannot post. Data: " + data + "|sign: " + sign
                    + Environment.NewLine + "url: " + url);
            }
            return SendResult;
        }

        // Post ket qua ve cho doi tac tich hop cong
        static public bool PostToMerchantV2(string data, string sign, string url, ref string contents)
        {
            contents = string.Empty;
            bool SendResult = false;

            try
            {
                contents = "urlpost=" + HttpUtility.UrlEncode(url);
                contents += "&data=" + HttpUtility.UrlEncode(data);
                contents += "&signature=" + HttpUtility.UrlEncode(sign);
                //byte[] contentPost = System.Text.Encoding.UTF8.GetBytes(contents);
                string urlPost = System.Configuration.ConfigurationManager.AppSettings["VTCApp_PaymentDone_PostNotify"];

                string resultPost = SendPostNotifyMerchant(contents, urlPost, ref SendResult);
                Log4Net.LogInfo("SendPostNotifyMerchant > content:" + contents + "|SendResult:" + SendResult + "|resultPost:" + resultPost);
                //SendResult = SendNotifyStateChangeOrderNew(url, contentPost);
            }
            catch (Exception ex)
            {
                Log4Net.LogInfo("Error post.Data: " + data + "|sign: " + sign
                 + Environment.NewLine + "url: " + url
                 + Environment.NewLine + "Exception detail: " + ex);
            }

            return SendResult;
        }

        static public bool SendNotifyStateChangeOrder(string _NotifyURL, byte[] contentPost)
        {
            bool success = false;

            try
            {
                CookieContainer cookie = new CookieContainer();

                System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate (object sender, X509Certificate certificate, X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors) { return true; };
                System.Net.ServicePointManager.Expect100Continue = false;
                // Gan tap hop cac security protocal se ho tro ( ssl3, tsl, tsl11, tsl 12) . Toan tu | tra ra mot enum 
                // Neu khong gan mac dinh se chi co SSL3, TSL (voi framswork 4.5)
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(_NotifyURL);
                myRequest.Method = "POST";
                myRequest.ContentLength = contentPost.Length;
                myRequest.CookieContainer = cookie;
                myRequest.ContentType = "application/x-www-form-urlencoded";
                myRequest.KeepAlive = false;

                using (Stream requestStream = myRequest.GetRequestStream())
                {
                    requestStream.Write(contentPost, 0, contentPost.Length);
                }

                string responseXml = string.Empty;
                using (HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse())
                {
                    if (myResponse.StatusCode != HttpStatusCode.OK)
                    {
                        success = false;
                    }
                    else
                    {
                        success = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Log4Net.LogInfo(string.Format("UrpPost: {0}, DataPost:{1}", _NotifyURL, System.Text.Encoding.ASCII.GetString(contentPost))
                    + Environment.NewLine + "Exception:" + ex);
            }
            return success;
        }
        static public bool SendNotifyStateChangeOrderNew(string _NotifyURL, byte[] bytes)
        {
            try
            {
                System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate (object sender, X509Certificate certificate, X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors) { return true; };
                System.Net.ServicePointManager.Expect100Continue = false;
                // Gan tap hop cac security protocal se ho tro ( ssl3, tsl, tsl11, tsl 12) . Toan tu | tra ra mot enum 
                // Neu khong gan mac dinh se chi co SSL3, TSL (voi framswork 4.5)
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(_NotifyURL);
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = bytes.Length;
                request.Method = "POST";
                Stream requestStream = request.GetRequestStream();
                requestStream.Write(bytes, 0, bytes.Length);
                requestStream.Close();
                HttpWebResponse response;
                response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                Log4Net.LogInfo("Excetpion post. Url: " + _NotifyURL
                    + Environment.NewLine + ex);
            }

            return false;
        }
        /// <summary>Send the Message to PalPal Checkout</summary>
        public static string SendPost(string postData, string url)
        {
            bool success = false;
            string resp;
            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
            byte[] data = encoding.GetBytes(postData);

            System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate (object sender, X509Certificate certificate, X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors) { return true; };
            System.Net.ServicePointManager.Expect100Continue = false;
            // Gan tap hop cac security protocal se ho tro ( ssl3, tsl, tsl11, tsl 12) . Toan tu | tra ra mot enum 
            // Neu khong gan mac dinh se chi co SSL3, TSL (voi framswork 4.5)
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

            CookieContainer cookie = new CookieContainer();
            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(url);
            myRequest.Method = "POST";
            myRequest.ContentLength = data.Length;
            myRequest.ContentType = "application/x-www-form-urlencoded";
            myRequest.KeepAlive = false;
            myRequest.CookieContainer = cookie;

            myRequest.AllowAutoRedirect = false;

            using (Stream requestStream = myRequest.GetRequestStream())
            {
                requestStream.Write(data, 0, data.Length);
            }


            string responseXml = string.Empty;
            try
            {
                using (HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse())
                {
                    if (myResponse.StatusCode != HttpStatusCode.OK)
                        success = false;
                    else
                        success = true;
                    using (Stream respStream = myResponse.GetResponseStream())
                    {
                        using (StreamReader respReader = new StreamReader(respStream))
                        {
                            responseXml = respReader.ReadToEnd();
                        }
                    }
                }
            }
            catch (WebException webEx)
            {
                if (webEx.Response != null)
                {
                    using (HttpWebResponse exResponse = (HttpWebResponse)webEx.Response)
                    {
                        using (StreamReader sr = new StreamReader(exResponse.GetResponseStream()))
                        {
                            responseXml = sr.ReadToEnd();
                        }
                    }
                }
            }



            if (success)
            {
                resp = responseXml;
            }
            else
            {
                resp = responseXml;

            }

            return resp;
        }
        public static string SendPostNotifyMerchant(string postData, string url, ref bool success)
        {
            success = false;
            string resp;
            var keyApp = System.Configuration.ConfigurationManager.AppSettings["PostNotifySecretKey"];
            var AuthorizeSecret = Security.Encrypt.Md5(string.Format("{0}{1}", postData, keyApp));
            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
            byte[] data = encoding.GetBytes(postData);

            System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate (object sender, X509Certificate certificate, X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors) { return true; };
            System.Net.ServicePointManager.Expect100Continue = false;
            // Gan tap hop cac security protocal se ho tro ( ssl3, tsl, tsl11, tsl 12) . Toan tu | tra ra mot enum 
            // Neu khong gan mac dinh se chi co SSL3, TSL (voi framswork 4.5)
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

            CookieContainer cookie = new CookieContainer();
            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(url);
            myRequest.Method = "POST";
            myRequest.ContentLength = data.Length;
            myRequest.ContentType = "application/x-www-form-urlencoded";
            myRequest.KeepAlive = false;
            myRequest.CookieContainer = cookie;
            myRequest.Headers.Add("VerifyToken", AuthorizeSecret);
            myRequest.AllowAutoRedirect = false;

            using (Stream requestStream = myRequest.GetRequestStream())
            {
                requestStream.Write(data, 0, data.Length);
            }


            string responseXml = string.Empty;
            try
            {
                using (HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse())
                {
                    if (myResponse.StatusCode != HttpStatusCode.OK)
                        success = false;
                    else
                        success = true;
                    using (Stream respStream = myResponse.GetResponseStream())
                    {
                        using (StreamReader respReader = new StreamReader(respStream))
                        {
                            responseXml = respReader.ReadToEnd();
                        }
                    }
                }
            }
            catch (WebException webEx)
            {
                Log4Net.PublishException(webEx);
                if (webEx.Response != null)
                {
                    using (HttpWebResponse exResponse = (HttpWebResponse)webEx.Response)
                    {
                        using (StreamReader sr = new StreamReader(exResponse.GetResponseStream()))
                        {
                            responseXml = sr.ReadToEnd();
                        }
                    }
                }
            }



            if (success)
            {
                resp = responseXml;
            }
            else
            {
                resp = responseXml;

            }

            return resp;
        }
        public static string SendGet(string url)
        {
            string response = string.Empty;
            try
            {
                System.Net.ServicePointManager.CertificatePolicy = new TrustAllCertificatePolicy();
                HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(url);

                myRequest.Method = "GET";
                //myRequest.ContentLength = data.Length;
                myRequest.CookieContainer = new CookieContainer();
                //myRequest.ContentType = "application/x-www-form-urlencoded";
                myRequest.ContentType = "application/json; charset=UTF-8";
                myRequest.KeepAlive = false;

                using (HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse())
                {
                    using (var reader = new StreamReader(myResponse.GetResponseStream()))
                    {
                        if (reader != null)
                        {
                            response = reader.ReadToEnd();
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                // Graph API Errors or general web exceptions 

                response = ex.Message;

            }
            catch (Exception)
            { }

            return response;
        }
        public static object GetHttpResponse(string url)
        {
            object response = null;
            try
            {
                System.Net.ServicePointManager.CertificatePolicy = new TrustAllCertificatePolicy();
                HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(url);

                myRequest.Method = "GET";
                //myRequest.ContentLength = data.Length;
                myRequest.CookieContainer = new CookieContainer();
                //myRequest.ContentType = "application/x-www-form-urlencoded";
                myRequest.ContentType = "text/xml; encoding='utf-8'";
                myRequest.KeepAlive = false;

                using (HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse())
                {
                    using (var reader = new StreamReader(myResponse.GetResponseStream()))
                    {
                        if (reader != null)
                        {
                            response = reader.ReadToEnd();
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                // Graph API Errors or general web exceptions 

                response = ex.Message;

            }
            catch (Exception)
            { }

            return response;
        }


        /// <summary>
        ///    Classed used to bypass self-signed server certificate
        /// </summary>
        /// <remarks>
        ///     To be used in development only.
        /// </remarks>
        public class TrustAllCertificatePolicy : System.Net.ICertificatePolicy
        {
            public TrustAllCertificatePolicy()
            { }

            public bool CheckValidationResult(ServicePoint sp,
              X509Certificate cert, WebRequest req, int problem)
            {
                return true;
            }
        }
    }


    public class ParamBuilder
    {
        System.Text.StringBuilder paramString;

        public ParamBuilder()
        {
            paramString = new System.Text.StringBuilder();
        }

        public void AddParam(string Name, string Value)
        {
            if (paramString.Length > 0)
                paramString.Append("&");

            paramString.Append(Name);
            paramString.Append("=");
            paramString.Append(HttpUtility.UrlEncode(Value));
        }

        public string GetParamString()
        {
            return paramString.ToString();
        }

        public string BuildRequestData(NameValueCollection paramCollection)
        {
            foreach (string name in paramCollection.AllKeys)
            {
                this.AddParam(name, paramCollection[name]);
            }
            return this.GetParamString();
        }
    }
}
