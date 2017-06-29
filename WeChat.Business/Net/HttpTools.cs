using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace WeChat.Business.Net
{
    /***
    * ===========================================================
    * 创建人：袁建廷
    * 创建时间：2017/06/28 16:08:02
    * 说明：
    * ==========================================================
    * */
    public class HttpTools
    {
        public CookieContainer Cookie = new CookieContainer();



        private string webwx_data_ticket;

        private string _UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/53.0.2785.104 Safari/537.36 Core/1.53.2372.400 QQBrowser/9.5.10548.400";
        public string UserAgent
        {
            set { _UserAgent = value; }
        }

        public CookieCollection GetCookie(string url)
        {
            return Cookie.GetCookies(new Uri(url));
        }


        bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors) { return true; }

        private HttpWebRequest GetRequest(string url, string Method = "POST", string strArgs = null, string contentType = null, FileInfo fileInfo = null, Dictionary<string, string> param = null, int TimeOut = 60002)
        {
            string strReferer = "https://wx2.qq.com/";
            try
            {
                string p = @"(http|https)://(?<domain>[^(:|/]*)";
                Regex reg = new Regex(p, RegexOptions.IgnoreCase);
                Match m = reg.Match(url);
                strReferer = "https://" + m.Groups["domain"].Value + "/";
            }
            catch (Exception)
            {
            }
            FileStream fs = null;
            Stream postStream = null;
            Uri uri = new Uri(url);
            HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.CreateDefault(uri);
            try
            {

                myHttpWebRequest.ProtocolVersion = HttpVersion.Version11;
                //myHttpWebRequest.Proxy = WebProxy;
                //使用回调的方法进行证书验证。
                ServicePointManager.ServerCertificateValidationCallback = CheckValidationResult;

                myHttpWebRequest.Method = Method;
                myHttpWebRequest.ServicePoint.ConnectionLimit = 1024;

                myHttpWebRequest.AllowAutoRedirect = true;
                myHttpWebRequest.KeepAlive = true;
                myHttpWebRequest.Accept = "application/json,text/plain,image/gif, image/x-xbitmap, image/jpeg, image/pjpeg, application/vnd.ms-excel, application/msword, application/x-shockwave-flash, */*";
                myHttpWebRequest.Referer = strReferer;
                myHttpWebRequest.Timeout = TimeOut;
                myHttpWebRequest.AllowWriteStreamBuffering = true;
                myHttpWebRequest.UserAgent = _UserAgent;
                myHttpWebRequest.Headers.Add("Accept-Encoding", "gzip, deflate, sdch, br");
                myHttpWebRequest.Headers.Add("Accept-Language", "zh-CN,zh;q=0.8");

                myHttpWebRequest.Headers.Set("Origin", "XMLHttpRequest");
                myHttpWebRequest.CookieContainer = Cookie;
                myHttpWebRequest.ContentType = (string.IsNullOrEmpty(contentType)) ? "application/x-www-form-urlencoded; charset=UTF-8" : contentType;
                if (fileInfo != null)
                {
                    string boundary = "----" + DateTime.Now.Ticks.ToString("X"); // 随机分隔线
                    myHttpWebRequest.ContentType = "multipart/form-data;charset=UTF-8;boundary=" + boundary;

                    //文件数据模板  
                    string fileFormdataTemplate =
                        "\r\n--" + boundary +
                        "\r\nContent-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"" +
                        "\r\nContent-Type: application/octet-stream" +
                        "\r\n\r\n";

                    string fileData = string.Format(fileFormdataTemplate, "file", fileInfo.Name);
                    byte[] postHeaderBytes = Encoding.UTF8.GetBytes(fileData);

                    //文本数据模板  
                    string dataFormdataTemplate =
                        "\r\n--" + boundary +
                        "\r\nContent-Disposition: form-data; name=\"{0}\"" +
                        "\r\n\r\n{1}";
                    byte[] endBoundaryBytes = Encoding.UTF8.GetBytes("\r\n--" + boundary + "--\r\n");


                    fs = new FileStream(fileInfo.FullName, FileMode.Open, FileAccess.Read);
                    byte[] bArr = new byte[fs.Length];
                    fs.Read(bArr, 0, bArr.Length);
                    fs.Close();
                    postStream = myHttpWebRequest.GetRequestStream();

                    if (param != null)
                    {
                        foreach (string item in param.Keys)
                        {
                            string paramStr = string.Format(dataFormdataTemplate, item, param[item]);
                            byte[] formitembytes = Encoding.UTF8.GetBytes(paramStr);
                            postStream.Write(formitembytes, 0, formitembytes.Length);
                        }
                    }

                    postStream.Write(postHeaderBytes, 0, postHeaderBytes.Length);
                    postStream.Write(bArr, 0, bArr.Length);

                    postStream.Write(endBoundaryBytes, 0, endBoundaryBytes.Length);
                    postStream.Close();

                    return myHttpWebRequest;
                }

                if (strArgs != null)
                {
                    byte[] postData = Encoding.UTF8.GetBytes(strArgs);
                    myHttpWebRequest.ContentLength = postData.Length;
                    System.IO.Stream PostStream = myHttpWebRequest.GetRequestStream();
                    PostStream.Write(postData, 0, postData.Length);
                    PostStream.Close();
                }

                return myHttpWebRequest;
            }
            catch (Exception)
            {
                if (fs != null) fs.Close();
                if (postStream != null) postStream.Close();
                if (myHttpWebRequest != null) myHttpWebRequest.Abort();
                return null;
            }
        }


        /**/
        /// <summary> 
        /// 功能描述：模拟POST，提交数据，并记录Header中的cookie 
        /// </summary> 
        /// <param name="strURL">数据提交的页面地址</param> 
        /// <param name="strArgs">参数</param> 
        /// <param name="strReferer">引用地址</param> 
        /// <param name="code">网站编码</param> 
        /// <returns>可以返回页面内容或不返回</returns> 
        public string PostData(string strURL, string strArgs, string contentType = null)
        {
            //strArgs = HttpUtility.UrlEncode(strArgs);

            HttpWebResponse response = null;
            System.IO.StreamReader sr = null;
            HttpWebRequest myHttpWebRequest = null;
            try
            {
                myHttpWebRequest = GetRequest(strURL, "POST", strArgs, contentType);
                string strResult = "";
                if (myHttpWebRequest == null) return string.Empty;
                response = (HttpWebResponse)myHttpWebRequest.GetResponse();
                //判断响应的信息是否为压缩信息 若为压缩信息解压后返回
                if (response.ContentEncoding == "gzip")
                {
                    MemoryStream ms = new MemoryStream();
                    GZipStream zip = new GZipStream(response.GetResponseStream(), CompressionMode.Decompress);
                    byte[] buffer = new byte[1024];
                    int l = zip.Read(buffer, 0, buffer.Length);
                    while (l > 0)
                    {
                        ms.Write(buffer, 0, l);
                        l = zip.Read(buffer, 0, buffer.Length);
                    }
                    ms.Dispose();
                    zip.Dispose();
                    strResult = Encoding.UTF8.GetString(ms.ToArray());
                    response.Close();
                    return strResult;

                }


                sr = new System.IO.StreamReader(response.GetResponseStream(), Encoding.UTF8);
                strResult = sr.ReadToEnd();
                sr.Close();
                response.Close();
                return strResult;
            }
            catch (Exception ex)
            {
                if (myHttpWebRequest != null) myHttpWebRequest.Abort();
                if (sr != null) sr.Close();
                if (response != null) response.Close();
            }
            return string.Empty;
        }


        /**/
        /// <summary> 
        /// 功能描述：GET 获取数据
        /// </summary> 
        /// <param name="strURL">获取网站的某页面的地址</param> 
        /// <param name="strReferer">引用的地址</param> 
        /// <returns>返回页面内容</returns> 
        public string GetPage(string strURL)
        {
            return GetPage(strURL, 60002, null);
        }

        public string GetQrCode(string strURL)
        {
            return GetPage(strURL, 3000, null);
        }


        public string GetPage(string strURL, int TimeOut = 60002, string contentType = null)
        {
            HttpWebResponse response = null;
            System.IO.StreamReader sr = null;
            HttpWebRequest myHttpWebRequest = GetRequest(strURL, "GET", null, contentType, null, null, TimeOut);
            Stream streamReceive = null;
            try
            {
                string strResult = string.Empty;
                if (myHttpWebRequest == null)
                {
                    return "";
                }

                response = (HttpWebResponse)myHttpWebRequest.GetResponse();
                if (response.Cookies != null && response.Cookies.Count > 0)
                {
                    foreach (System.Net.Cookie item in response.Cookies)
                    {
                        if (item.Name == "webwx_data_ticket")
                        {
                            webwx_data_ticket = item.Value;
                            break;
                        }
                    }
                    this.Cookie.Add(myHttpWebRequest.RequestUri, response.Cookies);
                }

                string gzip = response.ContentEncoding;
                if (string.IsNullOrEmpty(gzip) || gzip.ToLower() != "gzip")
                {
                    streamReceive = response.GetResponseStream();
                }
                else
                {
                    streamReceive = new System.IO.Compression.GZipStream(response.GetResponseStream(), System.IO.Compression.CompressionMode.Decompress);
                }

                sr = new System.IO.StreamReader(streamReceive, Encoding.UTF8);

                if (response.ContentLength > 1)
                {
                    strResult = sr.ReadToEnd();
                }
                else
                {
                    char[] buffer = new char[256];
                    int count = 0;
                    StringBuilder sb = new StringBuilder();
                    while ((count = sr.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        sb.Append(new string(buffer));
                    }
                    strResult = sb.ToString();
                    sb.Clear();
                }
                streamReceive.Close();
                sr.Close();
                response.Close();
                return strResult;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + "======================>");
                if (streamReceive != null) streamReceive.Close();
                if (sr != null) sr.Close();
                if (response != null) response.Close();
                if (myHttpWebRequest != null) myHttpWebRequest.Abort();
                return "";
            }
        }

        /// <summary>
        /// 获取文件
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public bool GetFile(string url, string SevePath)
        {
            HttpWebRequest myHttpWebRequest = GetRequest(url, "GET");
            HttpWebResponse response = null;
            //System.IO.StreamReader sr = null;
            StreamWriter sw = null;
            response = (HttpWebResponse)myHttpWebRequest.GetResponse();
            Stream streamReceive;
            string gzip = response.ContentEncoding;

            if (string.IsNullOrEmpty(gzip) || gzip.ToLower() != "gzip")
            {
                streamReceive = response.GetResponseStream();
            }
            else
            {
                streamReceive = new System.IO.Compression.GZipStream(response.GetResponseStream(), System.IO.Compression.CompressionMode.Decompress);
            }
            try
            {
                sw = new StreamWriter(SevePath);
                streamReceive.CopyTo(sw.BaseStream);
                return true;
            }
            catch (Exception)
            {
            }
            finally
            {
                if (sw != null)
                {
                    sw.Flush();
                    sw.Close();
                }
                if (streamReceive != null)
                {
                    streamReceive.Close();
                }
                response.Close();
            }
            return false;
        }


        public Image GetResponseImage(string url)
        {
            Image image = null;
            try
            {
                HttpWebRequest myHttpWebRequest = GetRequest(url, "GET", null, "image/jpeg,image/png", null, null, 8000);
                HttpWebResponse response = null;
                if (myHttpWebRequest == null) return null;
                response = (HttpWebResponse)myHttpWebRequest.GetResponse();

                Stream streamReceive;
                string gzip = response.ContentEncoding;

                if (string.IsNullOrEmpty(gzip) || gzip.ToLower() != "gzip")
                {
                    streamReceive = response.GetResponseStream();
                }
                else
                {
                    streamReceive = new System.IO.Compression.GZipStream(response.GetResponseStream(), System.IO.Compression.CompressionMode.Decompress);
                }
                try
                {
                    image = Image.FromStream(streamReceive);
                }
                catch (Exception)
                {
                }
                finally
                {
                    streamReceive.Close();
                    response.Close();
                    myHttpWebRequest.Abort();
                }
                return image;
            }
            catch (Exception e)
            {
                return image;
            }

        }

        #region 微信上传文件
        /// <summary>
        /// 微信上传文件
        /// </summary>
        /// <param name="url"></param>
        /// <param name="fileInfo"></param>
        /// <param name="postStr"></param>
        /// <param name="pass_ticket"></param>
        /// <param name="type">图片为 image/png 文件为：application/octet-stream </param>
        /// <returns></returns>
        public string webwxuploadmedia(string url, FileInfo fileInfo, string type, Dictionary<string, string> param)
        {
            try
            {
                if (!param.ContainsKey("webwx_data_ticket"))
                {
                    param.Add("webwx_data_ticket", webwx_data_ticket);
                }
                Uri uri = new Uri(url);
                HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.CreateDefault(uri);
                myHttpWebRequest.ProtocolVersion = HttpVersion.Version11;
                //使用回调的方法进行证书验证。
                ServicePointManager.ServerCertificateValidationCallback = CheckValidationResult;

                myHttpWebRequest.Method = "POST";
                myHttpWebRequest.ServicePoint.ConnectionLimit = 1024;

                myHttpWebRequest.AllowAutoRedirect = true;
                myHttpWebRequest.KeepAlive = true;
                myHttpWebRequest.Accept = "*/*";
                myHttpWebRequest.Referer = "https://wx2.qq.com/";
                myHttpWebRequest.Timeout = 1000 * 60 + 2;
                myHttpWebRequest.Proxy = null;
                myHttpWebRequest.AllowWriteStreamBuffering = true;
                myHttpWebRequest.UserAgent = _UserAgent;
                myHttpWebRequest.Headers.Add("Origin", "https://wx2.qq.com");
                myHttpWebRequest.Headers.Add("Accept-Encoding", "gzip, deflate, sdch, br");
                myHttpWebRequest.Headers.Add("Accept-Language", "zh-CN,zh;q=0.8");

                StringBuilder postData = new StringBuilder();

                string boundary = DateTime.Now.Ticks.ToString("X"); // 随机分隔线


                string dataFormdataTemplate = "\r\n------" + boundary + "\r\nContent-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}";

                foreach (string item in param.Keys)
                {
                    string paramStr = string.Format(dataFormdataTemplate, item, param[item]);
                    postData.Append(paramStr);
                }
                //文件数据模板  
                string fileFormdataTemplate = "\r\n------" + boundary + "\r\nContent-Disposition:form-data; name=\"filename\"; filename=\"" + fileInfo.Name + "\"";

                postData.Append(fileFormdataTemplate);
                postData.Append("\r\nContent-Type: " + type + "\r\n\r\n");

                //Console.WriteLine(postData.ToString());

                byte[] endBoundaryBytes = Encoding.UTF8.GetBytes("\r\n------" + boundary + "--\r\n");
                byte[] buff = Encoding.UTF8.GetBytes(postData.ToString());
                FileStream fs = new FileStream(fileInfo.FullName, FileMode.Open, FileAccess.Read);
                byte[] bArr = new byte[fs.Length];
                fs.Read(bArr, 0, bArr.Length);
                fs.Close();


                myHttpWebRequest.ContentType = "multipart/form-data;boundary=----" + boundary;
                myHttpWebRequest.ContentLength = buff.Length + bArr.Length + endBoundaryBytes.Length;

                Stream postStream = myHttpWebRequest.GetRequestStream();

                postStream.Write(buff, 0, buff.Length);
                postStream.Write(bArr, 0, bArr.Length);
                postStream.Write(endBoundaryBytes, 0, endBoundaryBytes.Length);
                postStream.Close();


                string strResult = "";
                System.IO.StreamReader sr = null;
                HttpWebResponse response = (HttpWebResponse)myHttpWebRequest.GetResponse();
                //判断响应的信息是否为压缩信息 若为压缩信息解压后返回
                if (response.ContentEncoding == "gzip")
                {
                    MemoryStream ms = new MemoryStream();
                    GZipStream zip = new GZipStream(response.GetResponseStream(), CompressionMode.Decompress);
                    byte[] buffer = new byte[1024];
                    int l = zip.Read(buffer, 0, buffer.Length);
                    while (l > 0)
                    {
                        ms.Write(buffer, 0, l);
                        l = zip.Read(buffer, 0, buffer.Length);
                    }
                    ms.Dispose();
                    zip.Dispose();
                    strResult = Encoding.UTF8.GetString(ms.ToArray());
                    response.Close();
                    return strResult;

                }
                sr = new System.IO.StreamReader(response.GetResponseStream(), Encoding.UTF8);
                strResult = sr.ReadToEnd();
                sr.Close();
                response.Close();
                return strResult;
            }
            catch (Exception e)
            {
            }
            return string.Empty;
        }
        #endregion


        public string GetParameter(Dictionary<string, object> param)
        {
            StringBuilder sb = new StringBuilder();
            int i = 0;
            foreach (string item in param.Keys)
            {
                sb.Append(item + "=" + param[item]);
                if (i < param.Count - 1)
                    sb.Append("&");
                i++;
            }

            return HttpUtility.UrlEncode(sb.ToString());
        }

    }
}
