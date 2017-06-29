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
    * 创建时间：2017/06/29 14:32:07
    * 说明：
    * ==========================================================
    * */
    public class MPSubscribeMsg
    {
        public string UserName { get; set; }
        public int MPArticleCount { get; set; }
        public long Time { get; set; }
        public string NickName { get; set; }
    }
}
