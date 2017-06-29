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

namespace WeChat
{
    public partial class LoginForm : FormSkin
    {

        private TaskFactory AsyncTask;
        /// <summary>
        /// UI线程的同步上下文
        /// </summary>
        private SynchronizationContext m_SyncContext = null;
        public API api;
        private UserManager UserManager;


        public LoginForm()
        {
            InitializeComponent();
            //获取UI线程同步上下文
            m_SyncContext = SynchronizationContext.Current;
            AsyncTask = new TaskFactory();
            api = new API();
            UserManager = api.UserManager;
            UserManager.m_SyncContext = m_SyncContext;
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            AsyncTask.StartNew(() =>
            {
                bool result = UserManager.GetUIN();
                if (result)
                {
                    string url = "https://login.weixin.qq.com/l/" + Context.uuid;
                    Bitmap bitmap = UserManager.GetQRCode(url);
                    m_SyncContext.Post(Update, bitmap);
                    return;
                }
                m_SyncContext.Post(Update, result);
            });
        }


        private void Update(object obj)
        {
            if (obj is Bitmap)
            {
                Bitmap bitmap = obj as Bitmap;
                this.pictureBox1.Image = bitmap;
                AsynLogin.RunWorkerAsync();
            }
            else if (obj is bool && !((bool)obj))
            {
                Toast.Maketext(this, "获取二维码失败,请稍后重试").Show();
                UpdateInfo("获取二维码失败,请稍后重试");
            }
        }




        private void UpdateInfo(string message)
        {
            lblMessageInfo.Text = message;
        }



        private void AsynLogin_DoWork(object sender, DoWorkEventArgs e)
        {
            int code;

            while (true)
            {
                UserManager.WaitForLogin(out code);
                switch (code)
                {
                    case 201://扫描成功
                        this.AsynLogin.ReportProgress(1);
                        break;
                    case 200:
                        //等待用户确定
                        this.AsynLogin.ReportProgress(2);
                        bool Result = UserManager.Login();
                        if (Result)
                        {
                            this.AsynLogin.ReportProgress(3);
                            e.Result = true;
                            return;
                        }
                        else
                        {
                            this.AsynLogin.ReportProgress(98);
                            e.Result = false;
                            return;
                        }
                        break;

                    case 408://登陆超时
                        this.AsynLogin.ReportProgress(0);
                        e.Result = false;
                        return;
                    default:
                        this.AsynLogin.ReportProgress(98);
                        e.Result = false;
                        return;
                }
            }


        }

        private void AsynLogin_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            switch (e.ProgressPercentage)
            {
                case 1:
                    UpdateInfo("扫描成功");
                    break;
                case 2:
                    UpdateInfo("等待用户确定");
                    break;
                case 3:
                    UpdateInfo("登录成功");
                    break;
                case 0:
                    UpdateInfo("登陆超时");
                    break;
                default:
                    UpdateInfo("登陆异常");
                    break;
            }
        }

        private void AsynLogin_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            bool result =Convert.ToBoolean( e.Result);
            if (result)
            {
                MainForm form = new MainForm(this);
                form.Show();
                this.Hide();
                return;
            }
            Toast.Maketext(this, "登录失败！").Show();

        }

    }
}
