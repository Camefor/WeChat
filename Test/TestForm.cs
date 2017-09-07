using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test
{
    public partial class TestForm : Form
    {
        public TestForm()
        {
            InitializeComponent();
        }

        private void TestForm_Load(object sender, EventArgs e)
        {
            string json= File.ReadAllText(Application.StartupPath + "//emoji.json",Encoding.UTF8);
            List<Emoji_g> emoji_list = JsonConvert.DeserializeObject<List<Emoji_g>>(json);

            int x, y = 0;
            int t = 0;
            for (int i = 0; i < 15; i++)
            {
                y = i * 28;
                for (int j = 0; j < 7; j++)
                {
                    x = j * 28;
                    Emoji_g gg= emoji_list[t];
                    gg.y = y;
                    Createlbl(new Point(x, y), gg.keywords);
                    t++;
                }
            }

            string objJson= JsonConvert.SerializeObject(emoji_list);
            File.AppendAllText(Application.StartupPath+"//emoji1.json",objJson,Encoding.UTF8);

            //string test_data = "哈哈哈啊哈[微笑]！！！好好的好的[撇嘴]啊可是懒得看书的话卷卡式带科技时代撒快点回答后开始的哈萨克接电话卡号地块撒旦";

            //string json = "[{\"keywords\":\"[微笑]\",\"x\":0,\"y\":0},{\"keywords\":\"[撇嘴]\",\"x\":29,\"y\":0}]";
            //List<Emoji_g> emoji_list = JsonConvert.DeserializeObject<List<Emoji_g>>(json);


            //foreach (Emoji_g item in emoji_list)
            //{
            //    test_data = test_data.Replace(item.keywords, item.x + "," + item.y);
            //}
        }

        private void Createlbl(Point point,string text) 
        {
            Label lbl = new Label();
            lbl.AutoSize = false;
            lbl.BackColor = Color.Transparent;
            lbl.BorderStyle = BorderStyle.FixedSingle;
            lbl.Size = new Size(29, 29);
            lbl.Location = point;
            lbl.Margin = new Padding(0,0,0,0);
            lbl.Padding = new Padding(0,0,0,0);
            toolTip1.SetToolTip(lbl, text);
            this.flowLayoutPanel1.Controls.Add(lbl);
        }




        /// <summary>
        /// 截取图片方法
        /// </summary>
        /// <param name="url">图片地址</param>
        /// <param name="beginX">开始位置-X</param>
        /// <param name="beginY">开始位置-Y</param>
        /// <param name="getX">截取宽度</param>
        /// <param name="getY">截取长度</param>
        /// <param name="fileName">文件名称</param>
        /// <param name="savePath">保存路径</param>
        /// <param name="fileExt">后缀名</param>
        public static string CutImage(string url, int beginX, int beginY, int getX, int getY, string fileName, string savePath, string fileExt)
        {
            if ((beginX < getX) && (beginY < getY))
            {
                Bitmap bitmap = new Bitmap(url);//原图
                if (((beginX + getX) <= bitmap.Width) && ((beginY + getY) <= bitmap.Height))
                {
                    Bitmap destBitmap = new Bitmap(getX, getY);//目标图
                    Rectangle destRect = new Rectangle(0, 0, getX, getY);//矩形容器
                    Rectangle srcRect = new Rectangle(beginX, beginY, getX, getY);

                    Graphics g= Graphics.FromImage(destBitmap);
                    g.DrawImage(bitmap, destRect, srcRect, GraphicsUnit.Pixel);

                    ImageFormat format = ImageFormat.Png;
                    switch (fileExt.ToLower())
                    {
                        case "png":
                            format = ImageFormat.Png;
                            break;
                        case "bmp":
                            format = ImageFormat.Bmp;
                            break;
                        case "gif":
                            format = ImageFormat.Gif;
                            break;
                    }
                    destBitmap.Save(savePath + "//" + fileName, format);
                    return savePath + "\\" + "*" + fileName.Split('.')[0] + "." + fileExt;
                }
                else
                {
                    return "截取范围超出图片范围";
                }
            }
            else
            {
                return "请确认(beginX < getX)&&(beginY < getY)";
            }
        }

    }
}
