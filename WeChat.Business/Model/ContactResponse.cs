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
    * 创建时间：2017/07/03 11:49:05
    * 说明：
    * ==========================================================
    * */
    public class ContactResponse
    {
        public Response BaseResponse { get; set; }
        public int MemberCount { get; set; }
        public List<RContact> MemberList { get; set; }
        public int Seq { get; set; }
    }
}
