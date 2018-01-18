using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeChat.API
{
    /***
    * ===========================================================
    * 创建人：yuanj
    * 创建时间：2018/01/17 10:35:34
    * 说明：
    * ==========================================================
    * */
    public enum ClientStatusType
    {
        GetUUID,
        GetQRCode,
        Login,
        QRCodeScaned,
        WeixinInit,
        SyncCheck,
        WeixinSync,
        None,
    }
}
