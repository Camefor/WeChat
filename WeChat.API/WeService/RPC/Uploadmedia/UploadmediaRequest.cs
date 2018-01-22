using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeChat.API.RPC
{
    public class UploadmediaRequest
    {
        public int UploadType;
        public BaseRequest BaseRequest;
        public long ClientMediaId;
        public long TotalLen;
        public int StartPos;
        public long DataLen;
        public int MediaType;
        public string FromUserName;
        public string ToUserName;
        public string FileMd5;
    }
}
