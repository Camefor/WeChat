using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeChat.API.Wx;

namespace WeChat.API.RPC
{
    public class SendMsgImgRequest
    {
        public BaseRequest BaseRequest;
        public ImgMsg Msg;
        public int Scene;
    }
}
