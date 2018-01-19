using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeChat.API.Dao
{
    /***
    * ===========================================================
    * 创建人：yuanj
    * 创建时间：2018/01/17 16:27:14
    * 说明：
    * ==========================================================
    * */
    public class DaoMaster
    {
        /// <summary>
        /// 数据库版本
        /// </summary>
        public const int SCHEMA_VERSION = 3;

        public static string dbName;

        public static DaoSession session;

        public static DaoSession newSession(string name)
        {
            dbName = "Data Source=" + name + ";Pooling=true;FailIfMissing=false";
            session = new DaoSession(dbName, SCHEMA_VERSION);
            return session;
        }

        public static DaoSession GetSession()
        {
            if (session == null)
                return null;
            return session;
        }

        public static void Close()
        {
            if (session != null)
                session.Close();
        }
    }
}
