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
    * 创建时间：2017/07/17 9:33:29
    * 说明：
    * ==========================================================
    * */
    public class RecommendInfo
    {
        public string UserName { get; set; }
        public string NickName { get; set; }
        public long QQNum { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string Content { get; set; }
        public string Signature { get; set; }
        public string Alias { get; set; }
        public int Scene { get; set; }
        public int VerifyFlag { get; set; }
        public string AttrStatus { get; set; }
        public int Sex { get; set; }
        public string Ticket { get; set; }
        public int OpCode { get; set; }
    }
}
