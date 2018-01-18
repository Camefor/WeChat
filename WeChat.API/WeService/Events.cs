using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeChat.API
{
    /***
    * ===========================================================
    * 创建人：yuanj
    * 创建时间：2018/01/17 10:39:48
    * 说明：
    * ==========================================================
    * */
    public class WeChatClientEvent : EventArgs { }


    public class GetQRCodeImageEvent : WeChatClientEvent
    {
        public Image QRImage;
    }

    public class UserScanQRCodeEvent : WeChatClientEvent
    {
        public Image UserAvatarImage;
    }

    public class LoginSucessEvent : WeChatClientEvent
    {

    }
    public class GetUserEvent : WeChatClientEvent
    {
        public Contact Self;
    }
    public class InitedEvent : WeChatClientEvent
    {
        public List<Contact> LastContact;
    }

    public class AddMessageEvent : WeChatClientEvent
    {
        public Message Msg;
    }

    public class StatusChangedEvent : WeChatClientEvent
    {
        public ClientStatusType FromStatus;
        public ClientStatusType ToStatus;
    }
}
