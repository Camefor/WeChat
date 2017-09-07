using formSkin.Controls._List;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeChat.Business.Model;
using WeChat.Business.Utils;

namespace WeChat.ListAdapter
{
    /***
    * ===========================================================
    * 创建人：yuanj
    * 创建时间：2017/08/19 17:34:39
    * 说明：
    * ==========================================================
    * */
    public class MessageAdapter : Adapter
    {
        private List<WMessage> items;
        private Font font;

        public MessageAdapter()
        {
            items = new List<WMessage>();
            font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
        }


        public override int GetCount()
        {
            return items.Count;
        }

        public override object GetItem(int position)
        {
            return items[position];
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override void GetView(int position, FListItem item, Graphics g)
        {
            int x, y = 0, Height = 0;
            WMessage obj = items[position];


            using (SolidBrush br = new SolidBrush(Color.White))
            {
                Rectangle msgRec = Rectangle.Empty;
                switch (obj.MsgType)
                {
                    case 1://文本消息
                        Size TextSize = GraphicsUtils.GetStringWidth(obj.Content, g, font);
                        //大小
                        int row = (obj.Content.Length % 20 == 0) ? obj.Content.Length / 20 : obj.Content.Length / 20+1;//行数
                        int mw = (row <= 1) ? TextSize.Width+15 : 250;
                        int mh = (row <= 1) ? TextSize.Height + 15 : row * 250;

                        Height = TextSize.Height + 15;

                        if (obj.IsSend)
                        {
                            x = item.Width - TextSize.Width - 20;
                            y = item.Height * position + 5;
                        }
                        else
                        {
                            x = 5;
                            y = item.Height * position + 5;
                        }
                        msgRec = new Rectangle(x, y, TextSize.Width + 5, Height);
                        GraphicsUtils.FillRoundRectangle(g, br, msgRec, 10);
                        DrawText(obj.Content, g, msgRec, br, TextSize);
                        item.Height = Height + 15;
                        break;
                    case 47://收藏表情
                        Height = 120;
                        if (obj.IsSend)
                        {
                            x = item.Width - 120;
                            y = item.Rectangle.Y + 20;
                        }
                        else
                        {
                            x = 5;
                            y = item.Rectangle.Y + 20;
                        }
                        msgRec = new Rectangle(x, y, 105, 105);
                        GraphicsUtils.FillRoundRectangle(g, br, msgRec, 10);
                        DrawImage(obj.FileContent, g, msgRec);
                        item.Height = Height + 15;
                        break;
                    case 49:
                        obj.Content = "连接分享";
                        goto case 1;
                        //DrawText("连接分享", g, msgRec, br, TextSize);
                        break;
                    default:
                        obj.Content = "消息类型" + obj.MsgType;
                        goto case 1;
                        //DrawText("消息类型"+obj.MsgType, g, msgRec, br, TextSize);
                        break;
                }



            }

        }

        private void DrawText(string str, Graphics g, SolidBrush br);


        //绘制文字消息
        private void DrawText(string str, Graphics g, Rectangle msgRec, SolidBrush br, Size TextSize)
        {
            br.Color = Color.Black;

            char sign = '▓';
            string message = EmojiLib.Emoji.FilterEmoji(str, sign);
            List<string> array = EmojiLib.Emoji.DivisionString(str, sign);
            foreach (string item in array)
            {
                if (string.IsNullOrWhiteSpace(item))
                    continue;
                if (item.StartsWith("\\") && item.IndexOf("emoji") != -1)
                {
                    //获取图片
                    string emojiName = item.Substring(1,item.LastIndexOf('\\')-1);
                    Bitmap bit= EmojiLib.Emoji.GetImageByName(emojiName);
                    g.DrawImage(bit,)
                }
                else
                    g.DrawString(item, font, br, new PointF(msgRec.X + 4, msgRec.Y + (msgRec.Height / 2 - TextSize.Height / 2)));
            }

            //g.DrawString(str, font, br, new PointF(msgRec.X + 4, msgRec.Y + (msgRec.Height / 2 - TextSize.Height / 2)));
        }

        private void DrawImage(string path, Graphics g, Rectangle msgRec)
        {
            if (string.IsNullOrWhiteSpace(path) || !File.Exists(path))
            {
                //绘制默认图片
                return;
            }

            Rectangle rec = new Rectangle(msgRec.X + 2, msgRec.Y + 2, 100, 100);
            Image img = Image.FromFile(path);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.DrawImage(img, rec, new Rectangle(0, 0, img.Width, img.Height), GraphicsUnit.Pixel);

        }


        public void Add(WMessage item)
        {
            items.Add(item);
            notifyDataSetChanged();
        }

        public void AddAll(List<WMessage> items)
        {
            this.items.AddRange(items);
            notifyDataSetChanged();
        }
    }
}
