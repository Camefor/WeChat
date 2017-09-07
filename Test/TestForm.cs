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
            //string json = File.ReadAllText(Application.StartupPath + "//emoji.json", Encoding.UTF8);
            //string test_data = "哈哈哈啊哈[微笑]！！！好好的好的[撇嘴]啊可是懒得看书的话卷卡式带[发呆]科技时代撒快[流泪]点回答后开始的哈[流泪]萨克接电话卡号地块撒旦";
            //List<Emoji_g> emoji_list = JsonConvert.DeserializeObject<List<Emoji_g>>(json);

            //foreach (Emoji_g item in emoji_list)
            //{
            //    test_data = test_data.Replace(item.keywords, "\\" + item.x + "," + item.y + "\\");
            //    Createlbl(item.keywords);
            //}
            //label1.Text = test_data;
        }

        private void Createlbl(string text)
        {
            Label lbl = new Label();
            lbl.AutoSize = false;
            lbl.BackColor = Color.Transparent;
            lbl.BorderStyle = BorderStyle.None;
            lbl.Size = new Size(29, 29);
            lbl.Margin = new Padding(0, 0, 0, 0);
            lbl.Padding = new Padding(0, 0, 0, 0);
            //toolTip1.SetToolTip(lbl, text);
            lbl.Text = text;
            this.flowLayoutPanel1.Controls.Add(lbl);
        }


        private void button1_Click(object sender, EventArgs e)
        {
            string json = File.ReadAllText(Application.StartupPath + "//emoji.json", Encoding.UTF8);
            List<Emoji_g> emoji_list = JsonConvert.DeserializeObject<List<Emoji_g>>(json);
            Bitmap bit = new Bitmap(@"C:\Users\2017-07\Documents\GitHub\emojiImage.png");
            //惊讶 406
            //90

            for (int i =0; i < 90; i++)
            {
                Emoji_g emoji = emoji_list[i];
                if (i != 0 && (i+1) % 15 == 0)
                {
                    emoji.x = emoji.x - 1;
                }
                //emoji.y = emoji.y - 1;
                string ResouresName = "emoji_" + i + ".png";
                //SeveImage(bit, emoji.x, emoji.y, ResouresName);
                emoji.ResouresName = ResouresName;
            }
            bit.Dispose();
            string json1 = JsonConvert.SerializeObject(emoji_list);
            File.WriteAllText(Application.StartupPath + "//jj.json", json1, Encoding.UTF8);



            //Emoji_g emoji = emoji_list[14];
            //SeveImage(bit, emoji.x, emoji.y, "ddd5.png");

            
            MessageBox.Show("Test");
        }

        private static void SeveImage(Bitmap bit, int x, int y, string ResouresName)
        {
            using (Bitmap NewBitmap = bit.Clone(new Rectangle(x, y, 29, 29), bit.PixelFormat))
            {
                NewBitmap.Save(@"C:\Users\2017-07\Documents\GitHub\emojis\" + ResouresName);
            }
            //NewBitmap.Save(@"C:\Users\2017-07\Documents\GitHub\emojis\" + ResouresName);
            //NewBitmap.Dispose();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<Emoji_g> list = new List<Emoji_g>();
            foreach (Control item in this.flowLayoutPanel1.Controls)
            {
                Label lbl = item as Label;
                Emoji_g em = new Emoji_g();
                em.keywords = lbl.Text;
                em.x = lbl.Location.X;
                em.y = lbl.Location.Y;
                list.Add(em);
            }

            string json = JsonConvert.SerializeObject(list);
            File.WriteAllText(Application.StartupPath + "//jj.json", json, Encoding.UTF8);

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string test_data = "哈哈哈啊哈[微笑]！！！好好的好的[撇嘴]啊可是懒得。\n看书的话卷卡式带[发呆]科技时代撒快[流泪]点回答后开始的哈[流泪]萨克接电话卡号地块撒旦";
            richTextBox1.AppendText("原字符串：\n");
            richTextBox1.AppendText(test_data+"\n");

            char sign = '▓';
            string str = EmojiLib.Emoji.FilterEmoji(test_data, sign);

            List<string> array= EmojiLib.Emoji.DivisionString(str,sign);

            richTextBox1.AppendText("替换后：\n");
            foreach (string item in array)
            {
                if (item.StartsWith("\\") && item.IndexOf("emoji") != -1)
                {
                    //获取图片
                    string emojiName = item.Substring(1, item.LastIndexOf('\\')-1);

                }
                richTextBox1.AppendText(item + "\n");
            }
        }



    }
}
