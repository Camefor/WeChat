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
    * 创建时间：2017/07/17 8:59:39
    * 说明：
    * ==========================================================
    * */
    public class BatchgetContactResponse
    {
        public Response BaseResponse { get; set; }
        public int Count { get; set; }
        public List<BatchgetContact> ContactList { get; set; }
    }
}
