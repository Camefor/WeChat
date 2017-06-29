using formSkin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeChat.Business.APP;
using WeChat.Business.Base;
using WeChat.Business.BLL;
using WeChat.Business.Model;
using WeChat.Business.Utils;
using WeChat.ListAdapter;

namespace WeChat
{
    public partial class MainForm : FormSkin
    {

        private RContactManager RContactManager;
        private TaskFactory AsyncTask;
        /// <summary>
        /// UI线程的同步上下文
        /// </summary>
        private SynchronizationContext m_SyncContext = null;


        private RContactAdapter RContactAdapter;
        private LoginForm loginForm;
        private API api;

        public MainForm(LoginForm loginForm)
        {
            InitializeComponent();
            //获取UI线程同步上下文
            m_SyncContext = SynchronizationContext.Current;
            AsyncTask = new TaskFactory();
            api = loginForm.api;
            
            RContactManager = api.RContactManager;
            RContactManager.m_SyncContext = m_SyncContext;
            this.loginForm = loginForm;

        }


        private void MainForm_Load(object sender, EventArgs e)
        {
            RContactAdapter = new RContactAdapter(api);
            this.fListView1.Adapter = RContactAdapter;

            //获取用户信息
            AsyncTask.StartNew(() => {
                RContactManager.Webwxinit(UpdateUser);
            });

        }

      

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="obj"></param>
        public void UpdateUser(Object obj)
        {
            UserResponse response = obj as UserResponse;
            if (response == null)
            {
                ShowToast("获取用户数据失败！");
                LogHandler.e("UpdateUser ================>response==null");
                return;
            }
            User user = response.User;
            string str = Context.base_uri + "/webwxgeticon?seq=0&username=" + user.UserName + "&skey=" + Context.skey;
            api.Imageloader.Add(this.pbHead, str);
            List<RContact> List = new List<RContact>(response.ContactList);
            RContactAdapter.SetItems(List);


        }


        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (loginForm != null)
                loginForm.Close();
        }

        private void ShowToast(string message) 
        {
            Toast.Maketext(this,message).Show();
        }

    }
}
