using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeChat.API.Wx;

namespace WeChat.API.RPC
{
    public class SendMsgRequest
    {
        public BaseRequest BaseRequest;
        public Msg Msg;
        public long rr;
    }
}
