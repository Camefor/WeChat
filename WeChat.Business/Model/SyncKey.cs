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
    * 创建时间：2017/06/29 14:31:05
    * 说明：
    * ==========================================================
    * */
    public class SyncKey
    {
        public int Count { get; set; }
        public List<Keys> List { get; set; }
    }
}
