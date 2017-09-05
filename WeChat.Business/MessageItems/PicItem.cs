using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using WeChat.Business.APP;
using WeChat.Business.Net;

namespace WeChat.Business.MessageItems
{
    public class PicItem
    {
        private HttpTools http;

        public PicItem(HttpTools http)
        {
            this.http = http;
        }


        public string AnalysisImageMessage(string Content)
        {
            string url = GetImageUrl(Content);
            if (string.IsNullOrWhiteSpace(url))
                return null;

            try
            {
                string sevePath = Constants.PATH_CACHE + "/" + Path.GetRandomFileName();
                Image image = http.GetResponseImage(url);
                if (image != null)
                    image.Save(sevePath);
                return sevePath;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public string GetImageUrl(string Content)
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
