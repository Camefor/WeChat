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
    * 创建时间：2017/07/17 9:34:49
    * 说明：
    * ==========================================================
    * */
    public class MemberList
    {
        public int Uin { get; set; }
        public string UserName { get; set; }
        public string AttrStatus { get; set; }
        public string PYInitial { get; set; }
        public string PYQuanPin { get; set; }
        public string RemarkPYInitial { get; set; }
        public string RemarkPYQuanPin { get; set; }
        public int MemberStatus { get; set; }
        public string DisplayName { get; set; }
        public string KeyWord { get; set; }
    }
}
