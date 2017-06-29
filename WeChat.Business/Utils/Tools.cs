using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WeChat.Business.Utils
{
    /***
    * ===========================================================
    * 创建人：袁建廷
    * 创建时间：2017/06/29 11:46:34
    * 说明：
    * ==========================================================
    * */
    public class Tools
    {

        /// <summary>  
        /// 获取时间戳  
        /// </summary>  
        /// <returns></returns>  
        public static string GetTimeStamp()
        {
            DateTime time = DateTime.Now;

            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1, 0, 0, 0, 0));
            long t = (time.Ticks - startTime.Ticks) / 10000;   //除10000调整为13位      
            return t + "";

        }


        public static Match search(string pattern, string input)
        {
            Match match = Regex.Match(input, pattern);
            return match;
        }

    }
}
