using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WeChat.Business.Net
{
    /***
    * ===========================================================
    * 创建人：袁建廷
    * 创建时间：2017/06/29 14:47:35
    * 说明：
    * ==========================================================
    * */
    public class Imageloader
    {
        private HttpTools http;
        private TaskFactory AsyncTask;

        public Imageloader(HttpTools http) 
        {
            this.http = http;
            AsyncTask = new TaskFactory();
        }

        public void Add(PictureBox pb, string url)
        {
            ImageHolder Holder = new ImageHolder();
            Holder.pb = pb;
            Holder.Url = url;
            AsyncTask.StartNew(AsynLoadImage, Holder);

        }

        private void AsynLoadImage(object arg)
        {
            ImageHolder Holder = arg as ImageHolder;
            Image image= http.GetResponseImage(Holder.Url);
            if (image != null)
            {
                Holder.pb.Invoke((EventHandler)delegate {
                    Holder.pb.Image = image;
                });
            }
        }


        class ImageHolder
        {
            public PictureBox pb;
            public string Url;
            public string FilePath;
        }


    }
}
