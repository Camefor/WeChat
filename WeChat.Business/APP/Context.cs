using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeChat.Business.Model;

namespace WeChat.Business.APP
{
    /***
    * ===========================================================
    * 创建人：袁建廷
    * 创建时间：2017/06/29 11:46:04
    * 说明：
    * ==========================================================
    * */
    public class Context
    {
        public const string lang = "zh_CN";
        public const string appid = "wx782c26e4c19acffb";


        public static string uuid;
        public static string root_uri = string.Empty;
        public static string base_uri = string.Empty;
        public static string redirect_uri = string.Empty;
        public static string BaseRequest;
        public static string uin;
        public static string skey;
        public static string sid;
        public static string pass_ticket;
        public static string webwx_data_ticket;
        public static string webwx_auth_ticket;
        public static string lastModifiedDate;

        public static string synckey;
        public static SyncKey SyncKeys;
        public static User user;//当前用户

        //特殊账号
        public static string[] SpecialUsers = {"newsapp", "fmessage", "filehelper", "weibo", "qqmail",
             "tmessage", "qmessage", "qqsync", "floatbottle", "lbsapp", "shakeapp",
            "medianote", "qqfriend", "readerapp", "blogapp", "facebookapp", "masssendapp",
            "meishiapp", "feedsapp","voip", "blogappweixin", "weixin", "brandsessionholder",
            "weixinreminder", "wxid_novlwrv3lqwv11", "gh_22b87fa7cb3c", "officialaccounts",
            "notification_messages", "wxid_novlwrv3lqwv11", "gh_22b87fa7cb3c", "wxitil",
            "userexperience_alarm", "notification_messages" };

        //public static Dictionary<string, RContact> GroupList = new Dictionary<string, RContact>();//群聊

        //public static Dictionary<string, RContact> PublicUsersList = new Dictionary<string, RContact>();//公众号/服务号

        //public static Dictionary<string, RContact> SpecialUsersList = new Dictionary<string, RContact>();//特殊账号

        //public static Dictionary<string, RContact> GroupMemeberList = new Dictionary<string, RContact>();//群友

        //public static Dictionary<string, RContact> ContactList = new Dictionary<string, RContact>();//好友

        public static Dictionary<string, RContact> ContactList = new Dictionary<string, RContact>();//好友


        /// <summary>
        /// 更新username 获取好友、群、。。。
        /// </summary>
        /// <param name="flag">1 好友 、2 群 3 群友、4 公众号</param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static RContact GetRContact(int flag, string userName)
        {
            if (ContactList.ContainsKey(userName))
                return ContactList[userName];
            //switch (flag)
            //{
            //    case 1:
            //        if (ContactList.ContainsKey(userName))
            //            return ContactList[userName];
            //        break;
            //    case 2:
            //        if (GroupList.ContainsKey(userName))
            //            return GroupList[userName];
            //        break;
            //    case 3:
            //        if (GroupMemeberList.ContainsKey(userName))
            //            return GroupMemeberList[userName];
            //        break;
            //    case 4:
            //        if (PublicUsersList.ContainsKey(userName))
            //            return PublicUsersList[userName];
            //        break;
            //    default:
            //        break;
            //}
            return null;
        }

        private static string _DeviceID;
        public static string DeviceID
        {
            get
            {
                _DeviceID = (string.IsNullOrEmpty(_DeviceID)) ? GetDeviceID() : _DeviceID;
                return _DeviceID;
            }
        }

        public static string GetDeviceID()
        {
            string DeviceID = null;
            Random ran = new Random();
            DeviceID = "e58989089" + ran.Next(10000, 99999);
            return DeviceID;
        }

    }
}
