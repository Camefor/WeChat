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
using WinForm.UI.Controls;
using WinForm.UI.Forms;

namespace WeChat
{
    public partial class MainForm : BaseForm, IMessageCallBack
    {

        private RContactManager RContactManager;
        private TaskFactory AsyncTask;
        /// <summary>
        /// UI线程的同步上下文
        /// </summary>
        private SynchronizationContext m_SyncContext = null;


        private LastRContactAdapter LastRContactAdapter;
        private RContactAdapter RContactAdapter;
        private LoginForm loginForm;
        private API api;
        /// <summary>
        /// 当前打开的好友信息
        /// </summary>
        private RContact rContact;
        private MessageAdapter adapter;

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
            adapter = new MessageAdapter();
            this.fListView1.Adapter = adapter;
            txtMessage.ImeMode = ImeMode.OnHalf;

        }


        private void MainForm_Load(object sender, EventArgs e)
        {
            LastRContactAdapter = new LastRContactAdapter();
            this.LastList.Adapter = LastRContactAdapter;

            HideTable();
            LastList.Dock = DockStyle.Fill;
            LastList.Visible = true;
            pictureBoxSkin1.IsSelected = true;

            //获取用户信息
            AsyncTask.StartNew(() =>
            {
                RContactManager.Webwxinit(UpdateUser);
            });
            this.LastList.ItemClick += LastList_ItemClick;
            this.ContartList.ItemClick += LastList_ItemClick;
        }


        /// <summary>
        /// listView 向单机事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void LastList_ItemClick(object sender, WinForm.UI.Events.ItemClickEventArgs e)
        {
            RContact rContact = e.ViewHolder.UserData as RContact;
            if (this.rContact == rContact)
                return;
            adapter.Clear();
            this.rContact = rContact;
            this.lblOpUser.Text = rContact.NickName;
            this.lblOpUser.Visible = true;
            this.MessageContext.Visible = true;

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
            string str = Context.root_uri + user.HeadImgUrl;
            //string str = Context.base_uri + "/webwxgeticon?seq=0&username=" + user.UserName + "&skey=" + Context.skey;
            api.Imageloader.Add(this.pbHead, str);
            List<RContact> List = new List<RContact>(response.ContactList);
            LastRContactAdapter.SetItems(List);

            RContactAdapter = new ListAdapter.RContactAdapter();
            this.ContartList.Adapter = RContactAdapter;
            //加载好友信息
            AsyncTask.StartNew(() =>
            {
                RContactManager.GetRContact(UpdateRContact);
            });
        }

        private void UpdateRContact(object state)
        {
            ContactResponse response = state as ContactResponse;
            if (response == null)
            {
                ShowToast("获取好友数据失败！");
                LogHandler.e("UpdateRContact ================>response==null");
                return;
            }

            List<RContact> List = new List<RContact>(response.MemberList);
            RContactAdapter.SetItems(List);
            //获取消息
            AsyncTask.StartNew(LoadMessage);
        }


        public void LoadMessage()
        {
            api.MessageManager.GetMessage(this);
        }


        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Hide();
            AsyncTask.StartNew(()=> {
                api.MessageManager.Online = false;
                if (api != null)
                {
                    api.UserManager.webwxlogout();
                }
            });
            

            if (loginForm != null)
                loginForm.Close();
        }

        private void ShowToast(string message)
        {
            Toast.MakeText(this, message).Show();
        }

        #region table
        /// <summary>
        /// table切换事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBoxSkin1_Click(object sender, EventArgs e)
        {
            if (((CirclePictureBox)sender).IsSelected)
                return;

            foreach (Control item in panel5.Controls)
            {
                if (item is CirclePictureBox)
                {
                    ((CirclePictureBox)item).IsSelected = false;
                }
            }
            ((CirclePictureBox)sender).IsSelected = true;
        }

        private void HideTable()
        {
            foreach (Control item in panel6.Controls)
            {
                if (item is FListView)
                {
                    ((FListView)item).Visible = false;
                }
            }
        }
        /// <summary>
        /// table选中改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBoxSkin1_SelectedItem(object sender, EventArgs e)
        {
            CirclePictureBox view = sender as CirclePictureBox;
            int stap = Convert.ToInt32(view.Tag);
            HideTable();
            switch (stap)
            {
                case 0:
                    this.LastList.Visible = true;
                    this.LastList.Dock = DockStyle.Fill;
                    break;
                case 1:
                    this.ContartList.Visible = true;
                    this.ContartList.Dock = DockStyle.Fill;
                    break;
                case 2:
                    //this.Collection.Visible = true;
                    break;
                default:
                    break;
            }
        }
        #endregion



        public void OnMessage(WMessage item)
        {
            if (item.MsgType == 51)
                return;

            m_SyncContext.Post(UpdateMessage, item);
            //保存消息好本地数据库
        }

        private void UpdateMessage(object state)
        {
            WMessage item = state as WMessage;
            int flag = (item.isGroup) ? 2 : 1;
            if (item.IsSend)
            {
                RContact ToUser = Context.GetRContact(flag, item.ToUserName);
                item.SendHeadImage = this.pbHead.Image;
            }else
            {
                RContact FromUser = Context.GetRContact(flag, item.FromUserName);
                item.SendHeadImage = FromUser.HeadImage;
                //判断是否为当前打开好友消息
                if (this.rContact==null|| item.FromUserName != this.rContact.UserName)
                {
                    SetNotify(FromUser.NickName, item.Content);
                }
            }

            adapter.Add(item);
            fListView1.ScrollBottom();

        }

        public void OnNewRContact(RContact Contact)
        {

        }

        public void OnWeChatOut(string message)
        {
            this.Invoke((EventHandler)delegate
            {
                ShowToast(message);
            });
        }

        //最后通知时间
        DateTime LastNotifyTime = DateTime.Now;
        /// <summary>
        ///  //消息气泡 提示
        /// </summary>
        /// <param name="title"></param>
        /// <param name="message"></param>
        public void SetNotify(string title, string message)
        {
            //通知间隔为3秒
            DateTime time = DateTime.Now;
            TimeSpan span = time - LastNotifyTime;
            if (span.Hours > 0 || span.Minutes > 0 || span.Seconds > 3)
            {
                if (!string.IsNullOrEmpty(message))
                {
                    //notifyIcon1.BalloonTipText ="【"+title+"】："+ message;
                    notifyIcon1.ShowBalloonTip(3000, title, message, ToolTipIcon.Info);
                    LastNotifyTime = DateTime.Now;
                }
            }

        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            string message = txtMessage.Text;
            if (string.IsNullOrWhiteSpace(message))
                return;
            WMessage item = new WMessage();
            item.MsgType = 1;
            item.IsSend = true;
            item.Content = message;
            item.SendHeadImage = this.pbHead.Image;//自己的头像
            adapter.Add(item);
            txtMessage.Text = "";
            this.fListView1.ScrollBottom();
            SendTextMessage(message, this.rContact.UserName);
        }

        #region 发送文本消息
        /// <summary>
        /// 发现消息
        /// </summary>
        /// <param name="message"></param>
        /// <param name="toUserName"></param>
        /// <returns></returns>
        public void SendTextMessage(string message, string toUserName)
        {
            AsyncTask.StartNew(() =>
            {
                bool Result = false;
                Result = api.MessageManager.SendTextMessage(Context.user.UserName, toUserName, message);
            });
        }

        /// <summary>
        /// 发现消息
        /// </summary>
        /// <param name="message"></param>
        /// <param name="toUserName"></param>
        /// <returns></returns>
        public void SendTextMessage(string message, List<string> toUserNames)
        {
            AsyncTask.StartNew(() =>
            {
                bool Result = false;
                foreach (string item in toUserNames)
                {
                    if (string.IsNullOrWhiteSpace(item))
                        continue;
                    Result = api.MessageManager.SendTextMessage(Context.user.UserName, item, message);
                }
            });
        }

        #endregion

      
    }
}
