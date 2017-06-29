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
    * 创建时间：2017/06/29 14:30:15
    * 说明：
    * ==========================================================
    * */
    public class UserResponse
    {
        public Response BaseResponse { get; set; }
        public int Count { get; set; }
        public List<RContact> ContactList { get; set; }
        public SyncKey SyncKey { get; set; }
        public User User { get; set; }
        public string ChatSet { get; set; }
        public string SKey { get; set; }
        public int ClientVersion { get; set; }
        public long SystemTime { get; set; }
        public int GrayScale { get; set; }
        public int InviteStartCount { get; set; }
        public int MPSubscribeMsgCount { get; set; }
        public List<MPSubscribeMsg> MPSubscribeMsgList { get; set; }

        public int ClickReportInterval { get; set; }
    }
}
