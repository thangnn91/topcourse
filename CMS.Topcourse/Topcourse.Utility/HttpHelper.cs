﻿using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;

namespace Topcourse.Utility
{
    public static class HttpUtils
    {
        public static HttpResponseMessage CreateResponse(HttpResponseMessage response)
        {
            response.Headers.CacheControl = new CacheControlHeaderValue()
            {
                Public = true,
                MaxAge = new TimeSpan(0, 0, 1, 0)
            };

            return response;
        }
        public static HttpResponseMessage CreateResponse(HttpStatusCode statusCode, string content, string reason)
        {
            return new HttpResponseMessage()
            {
                StatusCode = statusCode,
                Content = new StringContent(content),
                ReasonPhrase = reason
            };
        }

        public static string SendPost(string postData, string url)
        {
            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
            byte[] data = encoding.GetBytes(postData);
            System.Net.ServicePointManager.CertificatePolicy = new TrustAllCertificatePolicy();
            System.Net.ServicePointManager.Expect100Continue = false;
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
            return responseXml;
        }

        /// <summary>Send the Message to Merchant</summary>
        public static string GetStringHttpResponse(string url)
        {
            string response = null;
            try
            {
                System.Net.ServicePointManager.CertificatePolicy = new TrustAllCertificatePolicy();
                HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(url);
                myRequest.Method = "GET";
                //myRequest.ContentLength = data.Length;
                myRequest.CookieContainer = new CookieContainer();
                //myRequest.ContentType = "application/x-www-form-urlencoded";
                myRequest.ContentType = "text/xml; encoding='utf-8'";
                //myRequest.ContentType = "application/x-www-form-urlencoded";
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
                throw ex;
            }
            catch (Exception)
            {
            }

            return response;
        }

        /// <summary>Send the Message to Merchant</summary>
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
            }
            catch (Exception)
            {
            }

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
            {
            }

            public bool CheckValidationResult(ServicePoint sp,
                X509Certificate cert, WebRequest req, int problem)
            {
                return true;
            }
        }

        public static HttpResponseMessage PostProxy(object model, string api)
        {
            HttpClientHandler handler = new HttpClientHandler();

            var Client = new HttpClient(handler);
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = Client.PostAsJsonAsync(api, model).Result;
            return response;
        }

        public static HttpResponseMessage PostProxy(object model, string api, string accessToken, string authenType = "Bearer")
        {

            HttpClientHandler handler = new HttpClientHandler();

            var client = new HttpClient(handler);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(authenType, accessToken);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.PostAsJsonAsync(api, model).Result;
            return response;
        }

        public static string GetStringHttpResponse(string url, string accessToken)
        {
            string response = null;
            try
            {
                System.Net.ServicePointManager.CertificatePolicy = new TrustAllCertificatePolicy();
                HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(url);

                myRequest.Method = "GET";
                //myRequest.ContentLength = data.Length;
                myRequest.CookieContainer = new CookieContainer();
                //myRequest.ContentType = "application/x-www-form-urlencoded";
                //myRequest.ContentType = "text/xml; encoding='utf-8'";
                //myRequest.ContentType = "application/x-www-form-urlencoded";
                myRequest.ContentType = "application/json; encoding='utf-8'";
                myRequest.KeepAlive = false;
                myRequest.Headers.Add("Authorization", "Bearer " + accessToken);
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
                Log4Net.LogInfo($"WebException>> url: {url} ||accessToken: {accessToken} ");
                using (WebResponse responseFail = ex.Response)
                {
                    HttpWebResponse httpResponse = (HttpWebResponse)responseFail;
                    using (Stream data = responseFail.GetResponseStream())
                    using (var reader = new StreamReader(data))
                    {
                        string responseString = reader.ReadToEnd();
                        return responseString;
                    }
                }
            }
            catch (Exception) { Log4Net.LogInfo($"Exception>> url: {url} ||accessToken: {accessToken} "); }
            return response;
        }

        public static HttpResponseMessage PutProxy(object model, string api, string accessToken, string authenType = "Bearer")
        {
            HttpClientHandler handler = new HttpClientHandler();
            var client = new HttpClient(handler);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(authenType, accessToken);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.PutAsJsonAsync(api, model).Result;
            return response;
        }

    }
}