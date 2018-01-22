using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeChat.API.Tools;

namespace WeChat.API
{
    /***
    * ===========================================================
    * 创建人：yuanj
    * 创建时间：2018/01/17 10:53:01
    * 说明：
    * ==========================================================
    * */
    public class Contact
    {
        public string Seq
        {
            get
            {
                return StringUtils.GetSeq( HeadImgUrl);

            }
        }//唯一标识

        internal Client client { get; set; }

        public virtual string NickName { get; set; }
        public string ID { get; set; }
        public string RemarkName { get; set; }

        private string headImgUrl;
        public string HeadImgUrl
        {
            get { return headImgUrl; }
            set { if (headImgUrl == value) return; headImgUrl = value; loadImage(headImgUrl); }
        }
        public string LastMessage { get; set; }
        public DateTime? LastMessageTime { get; set; }

        public Image HeadImage { get; set; }

        public void loadImage(string img)
        {
            if (client == null)
                return;
            Image image = client.GetImage(img);
            HeadImage = image;
        }

        public string Uin { get; set; }
        public string DisplayName { get; set; }
    }
}
