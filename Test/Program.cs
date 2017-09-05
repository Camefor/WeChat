using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {

            string str = "&lt;msg&gt;&lt;emoji fromusername = \"wxid_80rwnom0v2ft21\" tousername = \"gh_486994bdc191\" type=\"1\" idbuffer=\"media:0_0\" md5 =\"2a51fc08dffa0056de8c24db69892d8e\" len = \"253442\" productid=\"\" androidmd5 =\"2a51fc08dffa0056de8c24db69892d8e\" androidlen=\"253442\" s60v3md5 = \"2a51fc08dffa0056de8c24db69892d8e\" s60v3len=\"253442\" s60v5md5 = \"2a51fc08dffa0056de8c24db69892d8e\" s60v5len=\"253442\" cdnurl = \"http://emoji.qpic.cn/wx_emoji/GxrhxTTwRIYQ4yv0mtjENCEswIKmxF8UvFfaMUDHb3jCjblP7CQDUA/\" designerid = \"\" thumburl = \"\" encrypturl = \"http://emoji.qpic.cn/wx_emoji/GxrhxTTwRIYQ4yv0mtjENCEswIKmxF8UcVC8YJMruRO7j6H5iaCfzjA/\" aeskey = \"0e416de55ceb9f324a8fdeab8cc7da62\" externurl = \"http://emoji.qpic.cn/wx_emoji/nichWHQufHGIzROo6Pz0BwCic9ssRo5HJtTyCiaAc7NdLtkibXUmM2xCu6QskZZ1eefC/\" externmd5 = \"e2d23a41a3005c6890878216c8394e50\" width= \"558\" height= \"630\" &gt;&lt;/emoji&gt; &lt;/msg &gt;";


            string url= GetImageUrl(str);

            Console.WriteLine(url);
            Console.Read();

        }

        public static string GetImageUrl(string Content)
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


    }
}
