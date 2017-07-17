using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeChat.Business.Model
{
    /***
    * ===========================================================
    * 创建人：袁建廷
    * 创建时间：2017/07/17 9:05:36
    * 说明：
    * ==========================================================
    * */
    public class WMessage
    {
        public WMessage() { }

        public WMessage(int MsgType, string Content, bool IsSend)
        {
            this.MsgType = MsgType;
            this.Content = Content;
            this.IsSend = IsSend;
        }



        public string MsgId { get; set; }
        public string FromUserName { get; set; }
        public string ToUserName { get; set; }
        public int MsgType { get; set; }
        public string Content { get; set; }
        public int Status { get; set; }
        public int ImgStatus { get; set; }
        public long CreateTime { get; set; }
        public long VoiceLength { get; set; }
        public long PlayLength { get; set; }
        public string FileName { get; set; }
        public string FileSize { get; set; }
        public string MediaId { get; set; }
        public string Url { get; set; }
        public int AppMsgType { get; set; }
        public int StatusNotifyCode { get; set; }
        public string StatusNotifyUserName { get; set; }
        public RecommendInfo RecommendInfo { get; set; }
        public int ForwardFlag { get; set; }
        public AppInfo AppInfo { get; set; }
        public int HasProductId { get; set; }
        public string Ticket { get; set; }
        public int ImgHeight { get; set; }
        public int ImgWidth { get; set; }
        public int SubMsgType { get; set; }
        public long NewMsgId { get; set; }
        public string OriContent { get; set; }

        public bool IsSend { get; set; }
        public string GroupFId { get; set; }

        public bool isGroup { get; set; }
        //文件xml信息
        public string FileContent { get; set; }

        public string LocalID { get; set; }
    }
}
