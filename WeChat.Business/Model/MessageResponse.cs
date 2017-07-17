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
    * 创建时间：2017/07/17 9:05:21
    * 说明：
    * ==========================================================
    * */
    public class MessageResponse
    {
        public Response BaseResponse { get; set; }
        public int AddMsgCount { get; set; }
        public List<WMessage> AddMsgList { get; set; }
        public int ModContactCount { get; set; }
        public List<ModContactList> ModContactList { get; set; }
        public int DelContactCount { get; set; }
        //public List<> DelContactList{get;set;}
        public int ModChatRoomMemberCount { get; set; }
        //public List<> ModChatRoomMemberList{get;set;}
        //public Profile Profile { get; set; }
        public int ContinueFlag { get; set; }
        public SyncKey SyncKey { get; set; }
    }
}
