using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeChat.Business.Model;

namespace WeChat.Business.BLL
{
    public interface IMessageCallBack
    {
        /// <summary>
        /// 新消息
        /// </summary>
        void OnMessage(WMessage item);
        /// <summary>
        /// 新好友
        /// </summary>
        void OnNewRContact(RContact Contact);
        /// <summary>
        /// 退出
        /// </summary>
        void OnWeChatOut(string message);

    }
}
