using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace WeChat.API.Utils
{
    /***
    * ===========================================================
    * 创建人：yuanj
    * 创建时间：2018/01/17 15:33:19
    * 说明：
    * ==========================================================
    * */
    public class XMLTools
    {
        /// <summary>
        /// 获取表情图片消息中的图片UTL
        /// </summary>
        /// <param name="Content"></param>
        /// <returns></returns>
        public static string GetImageUrl(string Content)
        {
            try
            {
                Content = Content.Replace("&lt;", "<");
                Content = Content.Replace("&gt;", ">");

                XmlDocument doc = new XmlDocument();
                doc.LoadXml(Content);
                XmlNode xn = doc.SelectSingleNode("msg");
                xn = xn.SelectSingleNode("emoji");
                XmlElement xe = (XmlElement)xn;
                string cdnurl = xe.GetAttribute("cdnurl").ToString();
                return cdnurl;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
