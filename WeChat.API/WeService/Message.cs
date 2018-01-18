using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeChat.API.Dao;
using WeChat.API.Utils;
using WeChat.API.Wx;

namespace WeChat.API
{
    /***
    * ===========================================================
    * 创建人：yuanj
    * 创建时间：2018/01/17 11:10:07
    * 说明：
    * ==========================================================
    * */
    public class Message
    {
        public string FromContactID { get; set; }
        public string ToContactD;
        public string Content { get; set; }
        public int MsgType { get; set; }
        public bool IsSend { get; set; }
        public string fileName { get; set; }
        public string FileSize { get; set; }
        public Contact Remote { get; set; }
        public Contact Mime { get; set; }

    }

    public class MessageFactory
    {

        public static WechatAPIService m_Service;

        public static void Init(WechatAPIService Service)
        {
            m_Service = Service;
        }

        public static bool IsSend(string FromUserName)
        {
            if (FromUserName == WechatAPIService.Self.ID)
                return true;
            return false;
        }


        public static Message CreateMessage(AddMsg msg)
        {
            //MsgType
            //1   文本消息
            //3   图片消息
            //34  语音消息
            //37  VERIFYMSG
            //40  POSSIBLEFRIEND_MSG
            //42  共享名片
            //43  视频通话消息
            //47  动画表情
            //48  位置消息
            //49  分享链接
            //50  VOIPMSG
            //51  微信初始化消息
            //52  VOIPNOTIFY
            //53  VOIPINVITE
            //62  小视频
            //9999    SYSNOTICE
            //10000   系统消息
            //10002   撤回消息

            bool isSend = IsSend(msg.FromUserName);
            Contact remote = (isSend) ? WechatAPIService.GetContact(msg.ToUserName) : WechatAPIService.GetContact(msg.FromUserName);

            Message ret = new Message();
            switch (msg.MsgType)
            {
                case 1:
                    ret.Content = msg.Content;
                    break;
                case 47:
                    //下载图片
                    string sevePath = Path.Combine(m_Service.CachePath, Path.GetRandomFileName());
                    ret.fileName = sevePath;
                    ret.FileSize = msg.FileSize;
                    if (string.IsNullOrEmpty(msg.Content))
                        break;

                    string url = XMLTools.GetImageUrl(msg.Content);
                    if (string.IsNullOrEmpty(url))
                        break;
                    Image image = m_Service.GetImage(url);
                    if (image == null)
                        break;
                    image.Save(sevePath);
                    break;
                default:
                    break;
            }
            if (ret == null)
                ret = new Message();
            ret.MsgType = msg.MsgType;
            ret.IsSend = isSend;
            ret.Remote = remote;
            ret.FromContactID = msg.FromUserName;
            ret.ToContactD = msg.ToUserName;
            ret.Mime = WechatAPIService.Self;
            if (msg.MsgType != 51)
                DaoMaster.GetSession().GetMessageDao().InsertMessage(ret, remote.Seq);

            return ret;
        }
    }
}
