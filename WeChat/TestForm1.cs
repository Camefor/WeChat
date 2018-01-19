using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeChat.Adapter;
using WeChat.API.Dao;

namespace WeChat
{
    public partial class TestForm1 : Form
    {
        private MessageAdapter adapter;

        public TestForm1()
        {
            InitializeComponent();
            adapter = new MessageAdapter();
            this.fListView1.Adapter = adapter;
            this.fListView1.IsMouseFeedBack = false;
        }


        private void TestForm1_Load(object sender, EventArgs e)
        {
            DaoMaster.newSession(@"C:\Users\yuanj\Source\Repos\WeChat\WeChat\bin\Debug\data\1344662178.db");
            MessageDao messageDao = DaoMaster.GetSession().GetMessageDao();
            List<API.Message> list = messageDao.GetMessage("665699749");
            adapter.AddItems(list);
            fListView1.ScrollBottom();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int count= weTextBox1.FileCount;
            MessageBox.Show(count+"");
        }
    }
}
