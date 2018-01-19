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
using WeChat.API;
using WinForm.UI.Forms;

namespace WeChat
{
    public partial class LoginForm : BaseForm
    {
        private WechatAPIService api;
        /// <summary>
        /// UI线程的同步上下文
        /// </summary>
        private SynchronizationContext m_SyncContext = null;
        public LoginForm()
        {
            InitializeComponent();
            //获取UI线程同步上下文
            m_SyncContext = SynchronizationContext.Current;
            api = new WechatAPIService();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            Init();
            api.CachePath = App.PATH_CACHE;
            api.OnGetQRCodeImage += Api_OnGetQRCodeImage;
            api.OnUserScanQRCode += Api_OnUserScanQRCode;
            api.OnStatusChanged += Api_OnStatusChanged;
            api.OnLoginSucess += Api_OnLoginSucess;
            api.Start();
        }

        private void Api_OnLoginSucess(WechatAPIService sender, LoginSucessEvent e)
        {
            m_SyncContext.Post(ShowMain,null);
        }

        private void ShowMain(object state)
        {
            MainForm main = new MainForm(api);
            main.Show();
            main.FormClosed += (obj, e) => {
                this.Close();
            };
            this.Hide();
        }

        private void Api_OnStatusChanged(WechatAPIService sender, StatusChangedEvent e)
        {
            switch (sender.CurrentStatus)
            {
                case ClientStatusType.GetUUID:
                    break;
                case ClientStatusType.GetQRCode:
                    break;
                case ClientStatusType.Login:
                    break;
                case ClientStatusType.QRCodeScaned:
                    m_SyncContext.Post(UpdateInfo, "请在手机上点击\"登陆\"按钮");
                    break;
                case ClientStatusType.WeixinInit:
                    break;
                case ClientStatusType.SyncCheck:
                    break;
                case ClientStatusType.WeixinSync:
                    break;
                case ClientStatusType.None:
                    break;
                default:
                    break;
            }
        }

        private void Api_OnUserScanQRCode(WechatAPIService sender, UserScanQRCodeEvent e)
        {
            this.pictureBox1.Image = e.UserAvatarImage;
            //UpdateInfo("");
        }

        private void Api_OnGetQRCodeImage(WechatAPIService sender, GetQRCodeImageEvent e)
        {
            this.pictureBox1.Image = e.QRImage;
        }


        private void Init()
        {
            App.PATH_INSTALL = Application.StartupPath;
            App.PATH_CACHE = App.PATH_INSTALL + "/cache/";
            App.PATH_DATA = App.PATH_INSTALL + "/data/";
            if (!Directory.Exists(App.PATH_CACHE))
            {
                Directory.CreateDirectory(App.PATH_CACHE);
            }
            if (!Directory.Exists(App.PATH_DATA))
            {
                Directory.CreateDirectory(App.PATH_DATA);
            }
        }


        private void UpdateInfo(object message)
        {
            lblMessageInfo.Text = message.ToString();
        }

        private void LoginForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (api != null)
            {
                api.Quit(true);
            }
        }
    }
}
