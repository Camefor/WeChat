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
   * 创建时间：2017/09/06 15:53:44
   * 说明：
   * ==========================================================
   * */
    public class StatusNotifyResponse
    {
        public Response BaseResponse { get; set; }
        public string MsgID { get; set; }
        public string LocalID { get; set; }
    }
}
