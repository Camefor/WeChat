using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeChat.Adapter;
using WeChat.API.Dao;
using WeChat.Controls;

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
            //DaoMaster.newSession(@"C:\Users\yuanj\Source\Repos\WeChat\WeChat\bin\Debug\data\1344662178.db");
            //MessageDao messageDao = DaoMaster.GetSession().GetMessageDao();
            //List<API.Message> list = messageDao.GetMessage("665699749");
            //adapter.AddItems(list);
            //fListView1.ScrollBottom();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            API.Message msg = new API.Message() {
                IsSend=false,
                MsgType=34,
                VoiceLength=152000
            };
            adapter.Add(msg);




            //List<Image> imageList = weTextBox1.GetImages();
            //if (imageList != null && imageList.Count > 0)
            //{
            //    //int i = 0;
            //    //foreach (Image item in imageList)
            //    //{
            //    //    item.Save("image" + i + ".bmp");
            //    //    i++;
            //    //}
            //}

            //string rtf = weTextBox1.Rtf;
            //File.WriteAllText("D:\\rtf2.rtf",rtf);


            //MessageBox.Show("11");


        }

        private void fListView1_ItemClick(object sender, WinForm.UI.Events.ItemClickEventArgs e)
        {
            adapter.Play();
        }
    }
}
