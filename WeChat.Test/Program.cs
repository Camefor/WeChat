using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WeChat.API.Result;
using WeChat.API.Tools;

namespace WeChat.Test
{
    class Program
    {
        static void Main(string[] args)
        {


            FileInfo file = new FileInfo(@"C:\Users\yuanj\Source\Repos\WeChat\WeChat\bin\Debug\cache\1nq11m3n.l5t.bmp");


            var streamContent = new StreamContent();
            //var streamContent = new StreamContent(File.Open(file.FullName, FileMode.Open));
            Console.WriteLine(11);






            LoginResult gg = new LoginResult();
            gg.code = 101;
            gg.redirect_uri = "https://www.baidu.com";
            gg.UserAvatar = "ddddddddddddddddddd";
            var loginResult = gg;
            LogHandler.i("WechatAPIService", "HandleQRCodeScaned()", loginResult);



            string url = "/cgi-bin/mmwebwx-bin/webwxgeticon?seq=721650572&username=@96511ed4972c718e354ebaddab1080718e209c3cddab496da5ecb35944f1fa4c&skey=@crypt_1560af10_a69df2a158a9e51683d04329d6d515cc";
            string uu= StringUtils.GetSeq(url);
            Console.WriteLine(uu);
            Console.Read();
        }
    }
}
