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
    * 创建时间：2017/07/17 9:34:29
    * 说明：
    * ==========================================================
    * */
    public class ModContactList
    {
        public string UserName { get; set; }
        public string NickName { get; set; }
        public int Sex { get; set; }
        public int HeadImgUpdateFlag { get; set; }
        public int ContactType { get; set; }
        public string Alias { get; set; }
        public string ChatRoomOwner { get; set; }
        public string HeadImgUrl { get; set; }
        public int ContactFlag { get; set; }
        public int MemberCount { get; set; }
        public List<MemberList> MemberList { get; set; }
    }
}
