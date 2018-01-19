using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeChat.API.Dao
{
    /***
    * ===========================================================
    * 创建人：yuanj
    * 创建时间：2018/01/17 16:53:11
    * 说明：
    * ==========================================================
    * */
    public class DaoSession
    {
        private MessageDao messageDao;
        private SqLiteHelper db;

        public const string TABLE_NAME = "T_DataBaseVersion";

        public DaoSession(string connectionString, int version)
        {
            db = new SqLiteHelper(connectionString);
            InitDataBase(version);
            messageDao = new MessageDao();
        }

        private void InitDataBase(int version)
        {
            int result = db.ExecuteScalar("SELECT count(*) from sqlite_master where type='table' and name='" + TABLE_NAME + "'");
            if (result == 0)
            {
                //表不存在
                string[] colNames = { "KEY", "VALUE" };
                string[] colTypes = { "TEXT", "TEXT" };
                db.CreateTable(TABLE_NAME, colNames, colTypes);
                db.InsertValues(TABLE_NAME, new string[] { "version", version + "" });
            }
            else
            {
                //判断数据库是否需要升级
                DataTable dt = db.GetTable("select * from " + TABLE_NAME + " where KEY='version'");
                if (dt != null && dt.Rows.Count > 0)
                {
                    int OldVersion = Convert.ToInt32(dt.Rows[0]["VALUE"]);
                    if (OldVersion < version)
                    {
                        UpdateDB();
                        db.UpdateValues(TABLE_NAME, new string[] { "VALUE" }, new string[] { version + "" }, "KEY", "version", "=");
                    }
                }
            }
            CreateAllTables();
        }




        public MessageDao GetMessageDao()
        {
            return messageDao;
        }

        public void UpdateDB()
        {
            MessageDao.UpdateTable(db);
        }

        public void CreateAllTables()
        {
            MessageDao.CreateTable(db);
        }

        public void Close()
        {
            db.CloseConnection();
        }

    }
}
