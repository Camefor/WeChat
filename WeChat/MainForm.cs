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
using WeChat.Adapter;
using WeChat.API;
using WeChat.API.Dao;
using WeChat.API.RPC;
using WinForm.UI.Controls;
using WinForm.UI.Forms;

namespace WeChat
{
    public partial class MainForm : BaseForm
    {
        private WechatAPIService api;
        private LastRContactAdapter LastRContactAdapter;
        private RContactAdapter RContactAdapter;
        private MessageAdapter adapter;
        private Contact openContact;
        private TaskFactory AsyncTask;
        private MusicForm musicForm;
        /// <summary>
        /// UI线程的同步上下文
        /// </summary>
        private SynchronizationContext m_SyncContext = null;
        public MainForm()
        {
            InitializeComponent();
            //获取UI线程同步上下文
            m_SyncContext = SynchronizationContext.Current;
            AsyncTask = new TaskFactory();
            LastRContactAdapter = new LastRContactAdapter();
            this.LastList.Adapter = LastRContactAdapter;
            RContactAdapter = new RContactAdapter();
            this.ContartList.Adapter = RContactAdapter;
            adapter = new MessageAdapter();
            this.fListView1.Adapter = adapter;
            txtMessage.ImeMode = ImeMode.OnHalf;
            this.fListView1.IsMouseFeedBack = false;
            musicForm = new MusicForm();
        }
        public MainForm(WechatAPIService api) : this()
        {
            this.api = api;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            loadingView1.BringToFront();
            loadingView1.Start();
            api.OnGetUser += Api_OnGetUser;
            api.OnInited += Api_OnInited;
            api.OnAddMessage += Api_OnAddMessage;

            HideTable();
            LastList.Dock = DockStyle.Fill;
            LastList.Visible = true;
            pictureBoxSkin1.IsSelected = true;

        }



        /// <summary>
        /// listView 事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void LastList_ItemClick(object sender, WinForm.UI.Events.ItemClickEventArgs e)
        {
            Contact rContact = e.ViewHolder.UserData as Contact;
            if (this.openContact == rContact)
                return;
            adapter.Clear();
            this.openContact = rContact;
            this.lblOpUser.Text = rContact.NickName;
            this.lblOpUser.Visible = true;
            this.MessageContext.Visible = true;
            //加载聊天记录
            AsyncTask.StartNew(LoadMessageHistory);

        }

        private void LoadMessageHistory()
        {
            if (openContact == null)
                return;
            string Seq = openContact.Seq;
            MessageDao MsgDao = DaoMaster.GetSession().GetMessageDao();
            List<API.Message> message = MsgDao.GetMessage(Seq);
            m_SyncContext.Post(UpdateMessageList, message);
        }

        private void UpdateMessageList(object state)
        {
            if (state is API.Message)
            {
                adapter.Add((API.Message)state);
            }
            else
            {
                List<API.Message> message = state as List<API.Message>;

                adapter.AddItems(message);
            }
            this.fListView1.ScrollBottom();
        }

        private void Api_OnGetUser(WechatAPIService sender, GetUserEvent e)
        {
            m_SyncContext.Post(UpdateUser, WechatAPIService.Self);
            string Uin = WechatAPIService.Self.Uin + ".db";
            string path = Path.Combine(App.PATH_DATA, Uin);
            DaoMaster.newSession(path);
        }
        private void Api_OnInited(WechatAPIService sender, InitedEvent e)
        {
            LastRContactAdapter.AddItems(e.LastContact);
            RContactAdapter.AddItems(sender.Contacts);
            m_SyncContext.Post((obj) => loadingView1.Stop(), null);

        }
        /// <summary>
        /// 更新当前用户信息
        /// </summary>
        /// <param name="state"></param>
        private void UpdateUser(object state)
        {
            Contact Self = state as Contact;
            pbHead.Image = Self.HeadImage;
        }

        private void Api_OnAddMessage(WechatAPIService sender, AddMessageEvent e)
        {
            API.Message msg = e.Msg;
            if (msg.MsgType == 51)
                return;
            //判断发送消息人是否为当前聊天用户
            if (this.openContact != null && openContact.ID == msg.Remote.ID)
            {
                m_SyncContext.Post(UpdateMessageList, msg);

            }
            if (msg.MsgType == 3)
            {
                AsyncTask.StartNew(() => DownloadImage(msg));
            }

        }
        /// <summary>
        /// 下载图片
        /// </summary>
        private void DownloadImage(API.Message msg)
        {
            string url = string.Format("/cgi-bin/mmwebwx-bin/webwxgetmsgimg?MsgID={0}", msg.MsgId);
            Image image = api.GetMsgImage(url);
            string fileName = msg.MsgId + ".bmp";
            string path = Path.Combine(App.PATH_CACHE, fileName);
            image.Save(path);
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            string message = txtMessage.Text;
            string rtf = txtMessage.Rtf;
            if (string.IsNullOrWhiteSpace(message) && string.IsNullOrEmpty(rtf))
                return;
            this.btnSend.Enabled = false;
            if (!string.IsNullOrEmpty(message))
            {
                SendMsg(this.openContact.ID, message);
                API.Message msg = new API.Message()
                {
                    MsgType = 1,
                    Content = message,
                    IsSend = true,
                    Remote = openContact,
                    Mime = WechatAPIService.Self
                };
                adapter.Add(msg);
            }
            List<Image> imageList = txtMessage.GetImages();
            if (imageList != null && imageList.Count > 0)
            {
                List<API.Message> messageList = new List<API.Message>();
                foreach (Image item in imageList)
                {
                    string filePath = Path.Combine(App.PATH_CACHE, Path.GetRandomFileName() + ".bmp");
                    item.Save(filePath);
                    SendImg(this.openContact.ID, filePath);
                    messageList.Add(new API.Message()
                    {
                        MsgType = 3,
                        fileName = filePath,
                        IsSend = true,
                        Remote = openContact,
                        Mime = WechatAPIService.Self
                    });
                }
                adapter.AddItems(messageList);
            }

            txtMessage.Clear();
            this.fListView1.ScrollBottom();
            this.btnSend.Enabled = true;
        }

        #region 发送消息
        /// <summary>
        /// 发送图片消息
        /// </summary>
        /// <param name="ToUserName"></param>
        /// <param name="path"></param>
        public void SendImg(string ToUserName, string filePath)
        {
            Contact ToUser = WechatAPIService.GetContact(ToUserName);
            API.Message msg = new API.Message()
            {
                MsgType = 3,
                fileName = filePath,
                IsSend = true,
                Remote = ToUser,
                Mime = WechatAPIService.Self,
            };
            Task<SendMsgImgResponse> task = api.SendImgAsync(ToUserName, filePath);
            task.ContinueWith((obj) =>
            {
                SendMsgImgResponse res = obj.Result;
                if (res != null && !string.IsNullOrEmpty(res.MsgID))
                {
                    MessageDao MsgDao = DaoMaster.GetSession().GetMessageDao();
                    msg.MsgId = res.MsgID;
                    MsgDao.InsertMessage(msg, openContact.Seq);
                }
            });

        }

        /// <summary>
        /// 发送文本消息
        /// </summary>
        /// <param name="ToUserName"></param>
        /// <param name="Content"></param>
        public void SendMsg(string ToUserName, string Content)
        {
            Contact ToUser = WechatAPIService.GetContact(ToUserName);
            API.Message msg = new API.Message()
            {
                MsgType = 1,
                Content = Content,
                IsSend = true,
                Remote = ToUser,
                Mime = WechatAPIService.Self,
            };
            Task<SendMsgResponse> task = api.SendMsgAsync(ToUserName, Content);
            task.ContinueWith((obj) =>
            {
                SendMsgResponse res = obj.Result;
                if (res != null && !string.IsNullOrEmpty(res.MsgID))
                {
                    MessageDao MsgDao = DaoMaster.GetSession().GetMessageDao();
                    msg.MsgId = res.MsgID;
                    MsgDao.InsertMessage(msg, openContact.Seq);
                }
            });
        }
        #endregion

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

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            notifyIcon1.Dispose();
            this.Hide();
            DaoMaster.Close();
        }
        /// <summary>
        /// 聊天记录点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fListView1_ItemClick(object sender, WinForm.UI.Events.ItemClickEventArgs e)
        {
            API.Message msg = e.ViewHolder.UserData as API.Message;
            if (msg.MsgType == 3)//图片消息
            {
                string path = string.Empty;
                if (!msg.IsSend)
                {
                    string fileName = msg.MsgId + ".bmp";
                    path = Path.Combine(App.PATH_CACHE, fileName);
                }else
                {
                    path = msg.fileName;
                }
                ImageForm form = new ImageForm();
                form.Show(path);
            }else if (msg.MsgType == 34)//语音消息
            {
                string path= msg.fileName;
                musicForm.Player(path);
                adapter.Play();
                AsyncTask.StartNew(() =>
                {
                    Thread.Sleep((int)msg.VoiceLength / 1000);
                    adapter.Stop();
                });
            }
        }

        private void btnFace_Click(object sender, EventArgs e)
        {
            //Point faceLoca= btnFace.Location;
            //Point point= PointToScreen(faceLoca);
            Point point = Control.MousePosition;
            FaceForm.GetFaceForm().Show(point, (DialogResult, obj) =>
            {

                if (DialogResult == DialogResult.OK)
                {
                    this.txtMessage.AppendText(obj);
                }

            });

        }
    }
}
