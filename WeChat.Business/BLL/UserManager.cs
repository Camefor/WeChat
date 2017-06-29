using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using ThoughtWorks.QRCode.Codec;
using WeChat.Business.APP;
using WeChat.Business.Base;
using WeChat.Business.Net;
using WeChat.Business.Utils;

namespace WeChat.Business.BLL
{
    /***
    * ===========================================================
    * 创建人：袁建廷
    * 创建时间：2017/06/29 11:40:54
    * 说明：
    * ==========================================================
    * */
    public class UserManager : BaseManager
    {
        public UserManager(HttpTools http)
            : base(http) 
        {
            
        }

        /// <summary>
        /// 获取UUID
        /// </summary>
        /// <returns></returns>
        public bool GetUIN() 
        {
            try
            {

                string Time = Tools.GetTimeStamp();
                string url = "https://login.wx.qq.com/jslogin?";
                string param = "appid=" + Context.appid + "&redirect_uri=https%3A%2F%2Fwx.qq.com%2Fcgi-bin%2Fmmwebwx-bin%2Fwebwxnewloginpage&fun=new&lang=" + Context.lang + "&_=" + Time;
                string json = "";

                json = http.GetQrCode(url + param);
                string pattern = "window.QRLogin.code = (\\d+); window.QRLogin.uuid = \"(\\S+?)\"";
                Match match = Tools.search(pattern, json);
                if (match.Groups.Count > 1)
                {
                    int code = Convert.ToInt32(match.Groups[1].Value);
                    Context.uuid = Convert.ToString(match.Groups[2].Value);
                    return code == 200;
                }
                return false;
            }
            catch (Exception)
            {

                return false;
            }
        
        }


        /// <summary>
        /// 生成二维码
        /// </summary>
        /// <returns></returns>
        public Bitmap GetQRCode(string enCodeString)
        {
            Bitmap bt;
            QRCodeEncoder endocder = new QRCodeEncoder();
            //二维码背景颜色
            endocder.QRCodeBackgroundColor = System.Drawing.Color.White;
            //二维码编码方式
            endocder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
            //每个小方格的宽度
            endocder.QRCodeScale = 5;
            //二维码版本号
            endocder.QRCodeVersion = 5;
            //纠错等级
            endocder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;


            bt = endocder.Encode(enCodeString, Encoding.UTF8);

            return bt;
        }


        /// <summary>
        /// 等待登录扫描（get轮询）：
        /// </summary>
        public void WaitForLogin(out int code)
        {
            string param = "loginicon=true&tip=0&uuid=" + Context.uuid +"&_=" + Tools.GetTimeStamp();

            string json = http.PostData("https://login.wx.qq.com/cgi-bin/mmwebwx-bin/login", param);
            string pattern = "window.code=(\\d+);";
            Match match = Tools.search(pattern, json);
            if (match.Groups.Count > 1)
            {
                code = Convert.ToInt32(match.Groups[1].Value);
                if (code == 200)
                {
                    match = Tools.search("window.redirect_uri=\"(\\S+?)\";", json);
                    string r_uri = match.Groups[1].Value;
                    Context.redirect_uri = r_uri + "&fun=new&version=v2&lang=zh_CN";
                    Context.base_uri = r_uri.Substring(0, r_uri.LastIndexOf('/'));
                }
                return;
            }
            code = 0;
        }


        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        public bool Login()
        {
            try
            {
                string url = Context.redirect_uri;
                string xml = http.GetPage(url);
                XmlDocument doc = new XmlDocument();
                foreach (Cookie ck in http.GetCookie(url))
                {

                    if (ck.Name == "webwx_data_ticket")
                    {
                        Context.webwx_data_ticket = ck.Value;
                    }
                    if (ck.Name == "webwx_auth_ticket")
                    {
                        Context.webwx_auth_ticket = ck.Value;
                    }
                }
                doc.LoadXml(xml);
                // 得到根节点bookstor
                XmlNode xn = doc.SelectSingleNode("error");
                // 得到根节点的所有子节点
                XmlNodeList xnl = xn.ChildNodes;
                foreach (XmlNode item in xnl)
                {
                    string key = item.Name;
                    string value = item.InnerText;
                    switch (key)
                    {
                        case "ret":
                            break;
                        case "message":
                            break;
                        case "skey":
                            Context.skey = value;
                            break;
                        case "wxsid":
                            Context.sid = value;
                            break;
                        case "wxuin":
                            Context.uin = value;
                            break;
                        case "pass_ticket":
                            Context.pass_ticket = value;
                            break;
                        case "isgrayscale":
                            break;

                    }
                }
            }
            catch (Exception e)
            {
                return false;
            }

            var BaseRequest = new
            {
                DeviceID = Context.DeviceID,
                Sid = Context.sid,
                Skey = Context.skey,
                Uin = Context.uin

            };
            string param = JsonConvert.SerializeObject(new { BaseRequest });
            Context.BaseRequest = param;
            return true;
        }

    }
}
