using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WeChat.Business.Base
{
    /***
    * ===========================================================
    * 创建人：袁建廷
    * 创建时间：2017/06/29 11:41:19
    * 说明：
    * ==========================================================
    * */
    public abstract class BaseManager
    {
        /// <summary>
        /// UI线程的同步上下文
        /// </summary>
        public SynchronizationContext m_SyncContext { set; get; }
        protected Net.HttpTools http;

        public BaseManager(Net.HttpTools http)
        {
            // TODO: Complete member initialization
            this.http = http;
        }

    }
}
