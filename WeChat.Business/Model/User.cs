using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WeChat.Business.Model
{
    /***
    * ===========================================================
    * 创建人：袁建廷
    * 创建时间：2017/06/29 14:31:31
    * 说明：
    * ==========================================================
    * */
    public class User
    {
        public long Uin { get; set; }
        public string UserName { get; set; }
        string NiName;
        public string NickName
        {
            get
            {

                NiName = StripTagsRegex(NiName);
                return NiName;
            }
            set
            {
                NiName = value;
            }

        }


        public string StripTagsRegex(string source)
        {
            return Regex.Replace(source, "<.*?>", "😃");
        }
        public string HeadImgUrl { get; set; }
        public string RemarkName { get; set; }
        public string PYInitial { get; set; }
        public string PYQuanPin { get; set; }
        public string RemarkPYInitial { get; set; }
        public string RemarkPYQuanPin { get; set; }
        public int HideInputBarFlag { get; set; }
        public int StarFriend { get; set; }
        public int Sex { get; set; }
        public string Signature { get; set; }
        public int AppAccountFlag { get; set; }
        public int VerifyFlag { get; set; }
        public int ContactFlag { get; set; }
        public int WebWxPluginSwitch { get; set; }
        public int HeadImgFlag { get; set; }
        public int SnsFlag { get; set; }


    }
}
