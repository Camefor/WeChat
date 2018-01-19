using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeChat.API.Tools;
using WinForm.UI;

namespace WeChat
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            #region 异常捕获
            Application.ThreadException += new ThreadExceptionEventHandler(Program.UIThreadException);
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(Program.CurrentDomain_UnhandledException);
            #endregion


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Style style = FormsManager.Style;
            string path = Path.Combine(Application.StartupPath, "logo.ico");
            if (File.Exists(path))
                style.Icon = new Icon(path);
            style.TitleBackColor = Color.Transparent;
            style.MinBoxBackColor = Color.FromArgb(70, Color.White);
            style.MaxBoxBackColor = Color.FromArgb(70, Color.White);

            //Application.Run(new LoginForm());
            Application.Run(new TestForm1());
        }


        /// <summary>
        /// 主线程异常
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void UIThreadException(object sender, ThreadExceptionEventArgs e)
        {
            try
            {
                LogHandler.e(e.Exception);
            }
            catch
            {
            }
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            try
            {

                Exception ex = (Exception)e.ExceptionObject;
                LogHandler.e(ex);
                //string text = "应用程序发生严重错误，给您带来的不便我们深表抱歉，程序即将关闭请联系管理员报告下列错误信息：" + ex.Message;
                //MessageBox.Show(text, "错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            catch (Exception message)
            {
                LogHandler.e(message);
            }
            finally
            {
                //Application.Exit();
            }
        }

    }
}
