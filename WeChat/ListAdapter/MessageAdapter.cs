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
                        string message = obj.Content;
                        Rectangle Rectangle = DrawText(obj.IsSend, obj.Content, item, g, br);
                        item.Height = Rectangle.Height;
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

        private Rectangle DrawText(bool IsSend, string str, FListItem item, Graphics g, SolidBrush br)
        {
            List<Object> array = EmojiLib.Emoji.ConvertToEmojiAndString(str);
            List<Object> Newarray = null;
            Rectangle Rectangle = ReckonRec(array, font, g, out Newarray);
            Rectangle.X = (IsSend) ? item.Width - Rectangle.Width - 10 : item.Rectangle.X;
            Rectangle.Y = item.Rectangle.Y;
            GraphicsUtils.FillRoundRectangle(g, br, Rectangle, 10);//绘制背景
            br.Color = Color.Black;
            int x = (IsSend) ? item.Width - Rectangle.Width + 5 : 10;
            int y = Rectangle.Y + 10;
            foreach (Object obj in Newarray)
            {
                if (obj is Bitmap)
                {
                    Bitmap bit = obj as Bitmap;
                    g.DrawImage(bit, new Point(x, y));
                    x += 29;
                }
                else
                {
                    g.DrawString(obj.ToString(), font, br, new Point(x, y));
                    x += Convert.ToInt32(g.MeasureString(obj.ToString(), font).Width);
                }
            }
            return Rectangle;
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

        /// <summary>
        /// 预估 文本所占用的区域大小
        /// </summary>
        /// <param name="array"></param>
        /// <param name="font"></param>
        /// <returns></returns>
        private Rectangle ReckonRec(List<Object> array, Font font, Graphics g, out List<Object> Newarray)
        {
            Newarray = new List<object>();
            Rectangle rect = Rectangle.Empty;
            int row = 1;
            float Width = 0;
            int maxWidth = 355;
            float tempWidth = 0;

            foreach (Object item in array)
            {
                if (item is Bitmap)
                {
                    tempWidth += 29;
                    Newarray.Add(item);
                }
                else
                {
                    string message = item.ToString();
                    //tempWidth += g.MeasureString(message, font).Width;   //无法换行
                    StringBuilder sb = new StringBuilder();
                    foreach (char str in message)
                    {
                        tempWidth += CalTextWidth(g, font, str + "");
                        sb.Append(str);
                        if (tempWidth > maxWidth)
                        {
                            sb.Append("\n");
                            row++;
                            Width += tempWidth;
                            tempWidth = 0;
                        }
                    }
                    if (row == 1)
                        Width = tempWidth;
                    Newarray.Add(sb.ToString());
                }
            }
            int w = Convert.ToInt32(Width)+20;
            int h = row * 30 + 10;
            rect.Width = w;
            rect.Height = h;
            return rect;
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

        /// <summary>
        /// 测量文字的宽
        /// </summary>
        /// <param name="g"></param>
        /// <param name="font"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public float CalTextWidth(Graphics g, Font font, string text)
        {
            StringFormat sf = StringFormat.GenericTypographic;
            sf.FormatFlags |= StringFormatFlags.MeasureTrailingSpaces;
            SizeF s = g.MeasureString(text, font, 0, sf);
            return s.Width;
        }
    }
}
