using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using WeChat.Business.APP;
using WeChat.Business.Base;

namespace WeChat.Business.Model
{
    /***
    * ===========================================================
    * 创建人：袁建廷
    * 创建时间：2017/06/28 16:06:10
    * 说明：
    * ==========================================================
    * */
    public class RContact
    {

        public string Uin { get; set; }
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
            if (string.IsNullOrWhiteSpace(source))
                return source;
            return Regex.Replace(source, "<.*?>", "😃");
        }
        private string headImgUrl;
        public string HeadImgUrl
        {
            get { return headImgUrl; }
            set { if (headImgUrl == value) return; headImgUrl = value; loadImage(headImgUrl);  }
        }
        public int ContactFlag { get; set; }
        public int MemberCount { get; set; }
        public string RemarkName { get; set; }
        public int HideInputBarFlag { get; set; }
        public int Sex { get; set; }
        public string Signature { get; set; }
        public int VerifyFlag { get; set; }

        //public string Seq
        //{

        //    get
        //    {
        //        return UrlTools.AnalysisUrl("https://wx2.qq.com" + HeadImgUrl, "Seq");

        //    }
        //}//唯一标识
        public int OwnerUin { get; set; }

        string PYInitial_;
        public string PYInitial
        {

            set
            {
                PYInitial_ = value;
            }
            get
            {
                if (RemarkPYInitial + "" != "")
                {
                    return RemarkPYInitial;
                }
                else
                {
                    return PYInitial_;
                }
            }


        }
        public string PYQuanPin { get; set; }
        public string RemarkPYInitial { get; set; }
        public string RemarkPYQuanPin { get; set; }
        public int StarFriend { get; set; }
        public int AppAccountFlag { get; set; }
        public int Statues { get; set; }
        public string AttrStatus { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string Alias { get; set; }
        public int SnsFlag { get; set; }
        public int UniFriend { get; set; }
        public string DisplayName { get; set; }
        public int ChatRoomId { get; set; }
        public string KeyWord { get; set; }
        public string EncryChatRoomId { get; set; }
        public int IsOwner { get; set; }


        public string LastMessage { get; set; }
        public DateTime? LastMessageTime { get; set; }

        public Image HeadImage { get; set; }

        public void loadImage(string img)
        {
            if ( API.HttpTools == null)
                return;
            string url = Context.root_uri + img;
            Image image = API.HttpTools.GetResponseImage(url);
            HeadImage = image;
        }

    }
}
