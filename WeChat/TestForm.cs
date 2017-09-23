using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeChat.Business.Model;
using WeChat.ListAdapter;

namespace WeChat
{
    public partial class TestForm : Form
    {
        private MessageAdapter adapter;

        public TestForm()
        {
            InitializeComponent();
            adapter = new MessageAdapter();
            this.fListView1.Adapter = adapter;
            txtMessage.ImeMode = ImeMode.OnHalf;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string message = txtMessage.Text;
            if (string.IsNullOrWhiteSpace(message))
                return;
            WMessage item = new WMessage();
            item.MsgType = 1;
            item.IsSend =(new Random().Next()%2==0);
            item.Content = message;
            adapter.Add(item);
            txtMessage.Text = "";
            this.fListView1.ScrollBottom();
        }
    }
}
