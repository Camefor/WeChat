using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WeChat.Business.BLL;
using WeChat.Business.Net;

namespace WeChat.Business.Base
{
    /***
    * ===========================================================
    * 创建人：袁建廷
    * 创建时间：2017/06/29 11:31:13
    * 说明：
    * ==========================================================
    * */
    public class API
    {
        private HttpTools http;

        private RContactManager rContactManager;
        private UserManager userManager;
        private Imageloader imageloader;
        private MessageManager messageManager;


        public API()
        {
            http = new HttpTools();
            rContactManager = new RContactManager(http);
            userManager = new UserManager(http);
            imageloader = new Imageloader(http);
            messageManager = new MessageManager(http);
        }

        public HttpTools HttpTools { get { return http; } }
             

        public RContactManager RContactManager { get { return rContactManager; } }

        public UserManager UserManager { get { return userManager; } }

        public Imageloader Imageloader { get { return imageloader; } }

        public MessageManager MessageManager { get { return messageManager; } }

    }
}
