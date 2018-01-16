using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeChat.Business.Model;
using WeChat.ListAdapter;

namespace WeChat
{
    public partial class RContactForm : Form
    {
        //private LastRContactAdapter adapter;
        private RContactAdapter adapter;
        private TaskFactory AsyncTask;
        /// <summary>
        /// UI线程的同步上下文
        /// </summary>
        private SynchronizationContext m_SyncContext = null;
        public RContactForm()
        {
            InitializeComponent();
            //获取UI线程同步上下文
            m_SyncContext = SynchronizationContext.Current;
            AsyncTask = new TaskFactory();
        }

        private void RContactForm_Load(object sender, EventArgs e)
        {
            //adapter = new LastRContactAdapter();
            adapter = new RContactAdapter();
            this.ContartList.Adapter = adapter;
            AsyncTask.StartNew(()=> {
                loadData();
            });

        }

        private void loadData()
        {
            string json = File.ReadAllText("E:\\test.json");
            List<RContact> MemberList = JsonConvert.DeserializeObject<List<RContact>>(json);
            m_SyncContext.Post(UpdateList, MemberList);
        }

        private void UpdateList(object state)
        {
            List<RContact> MemberList = state as List<RContact>;
            adapter.AddItems(MemberList);
        }
    }
}
