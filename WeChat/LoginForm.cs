using formSkin;
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
            Init();


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

        private void Init()
        {
            Constants.PATH_INSTALL = Application.StartupPath;
            Constants.PATH_CACHE = Constants.PATH_INSTALL + "/cache/";
            Constants.PATH_DATA = Constants.PATH_INSTALL + "/data/";
            if (!Directory.Exists(Constants.PATH_CACHE))
            {
                Directory.CreateDirectory(Constants.PATH_CACHE);
            }
            if (!Directory.Exists(Constants.PATH_DATA))
            {
                Directory.CreateDirectory(Constants.PATH_DATA);
            }

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
                Toast.MakeText(this, "获取二维码失败,请稍后重试").Show();
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
                        string Result = UserManager.Login();
                        if ("success"==Result)
                        {
                            this.AsynLogin.ReportProgress(3);
                            e.Result = "success";
                            return;
                        }
                        else
                        {
                            this.AsynLogin.ReportProgress(98);
                            e.Result = Result;
                            return;
                        }
                        break;

                    case 408://登陆超时
                        this.AsynLogin.ReportProgress(0);
                        e.Result = "登陆超时";
                        return;
                    default:
                        this.AsynLogin.ReportProgress(98);
                        e.Result = "登陆异常";
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
            string result = e.Result.ToString();
            if ("success" == result)
            {
                MainForm form = new MainForm(this);
                form.Show();
                this.Hide();
                return;
            }
            else 
            {
                Toast.MakeText(this, result,7000).Show();
            }
            

        }

    }
}
