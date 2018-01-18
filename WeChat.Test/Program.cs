using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeChat.API.Tools;

namespace WeChat.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            string url = "/cgi-bin/mmwebwx-bin/webwxgeticon?seq=721650572&username=@96511ed4972c718e354ebaddab1080718e209c3cddab496da5ecb35944f1fa4c&skey=@crypt_1560af10_a69df2a158a9e51683d04329d6d515cc";
            string uu= StringUtils.GetSeq(url);
            Console.WriteLine(uu);
            Console.Read();
        }
    }
}
