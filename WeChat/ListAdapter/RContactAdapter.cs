using formSkin.Controls._List;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WeChat.Business.APP;
using WeChat.Business.Base;
using WeChat.Business.Model;
using WeChat.Properties;

namespace WeChat.ListAdapter
{
    /***
    * ===========================================================
    * 创建人：袁建廷
    * 创建时间：2017/07/03 11:56:28
    * 说明：
    * ==========================================================
    * */
    public class RContactAdapter : Adapter
    {

        private List<RContact> items;
        private Font font;
        private Color SubItemSelectColor = Color.FromArgb(198, 197, 197);
        private Color ItemMouseOnColor = Color.FromArgb(205, 209, 216);
        private Font LastFont;

        private API api;


        public RContactAdapter(API api)
        {
            this.api = api;
            font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            LastFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
        }

        public void SetItems(List<RContact> items)
        {
            this.items = items;
            notifyDataSetChanged();
        }

        public void Add(RContact item)
        {
            if (items == null)
                items = new List<RContact>();
            items.Add(item);
            notifyDataSetChanged();
        }

        public void AddAll(List<RContact> items)
        {
            if (this.items == null)
                this.items = new List<RContact>();
            this.items.AddRange(items);
            notifyDataSetChanged();
        }

        public void Clear()
        {
            if (this.items != null)
                items.Clear();
            notifyDataSetChanged();
        }


        public override int GetCount()
        {
            return (items == null) ? 0 : items.Count;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override object GetItem(int position)
        {
            return items[position];
        }

        public override void GetView(int position, FListItem item, System.Drawing.Graphics g)
        {
            item.Height = 65;
            RContact bean = items[position];
            SolidBrush sb = new SolidBrush(Color.FromArgb(230, 229, 229));
            if (item.IsSelected)
            {        //判断改子项是否被选中
                sb.Color = SubItemSelectColor;
            }
            else if (item.IsMouseOnItem)
            {
                sb.Color = ItemMouseOnColor;
            }
            else
                sb.Color = Color.FromArgb(230, 229, 229);


            g.FillRectangle(sb, item.Rectangle);

            DrawHeadImage(g, bean, item.Rectangle);


            int x = 10 + 50;
            int y = item.Rectangle.Bottom - (item.Rectangle.Height / 2 + 10);
            if (!string.IsNullOrWhiteSpace(bean.LastMessage))
                y = item.Rectangle.Top + 12;
            using (Brush brush = new SolidBrush(Color.Black))
            {
                g.DrawString(bean.NickName, font, brush, new PointF(x, y));
            }

            if (!string.IsNullOrWhiteSpace(bean.LastMessage))
            {
                y += 24;
                using (Brush brush = new SolidBrush(Color.FromArgb(153, 153, 153)))
                {
                    g.DrawString(bean.LastMessage, LastFont, brush, new PointF(x, y));
                }
            }

            if (bean.LastMessageTime != null)
            {
                string time = ((DateTime)bean.LastMessageTime).ToString("HH:mm");
                x = item.Rectangle.Width - 60;
                y = item.Rectangle.Bottom - (item.Rectangle.Height / 2 + 15);
                using (Brush brush = new SolidBrush(Color.FromArgb(153, 153, 153)))
                {
                    g.DrawString(time, LastFont, brush, new PointF(x, y));
                }
            }

        }

        /// <summary>
        /// 绘制列表子项的头像
        /// </summary>
        /// <param name="g">绘图表面</param>
        /// <param name="subItem">要绘制头像的子项</param>
        /// <param name="rectSubItem">该子项的区域</param>
        private void DrawHeadImage(Graphics g, RContact subItem, Rectangle rectSubItem)
        {
            Image HeadImage = subItem.HeadImage;
            if (subItem.HeadImage == null)                 //如果头像位空 用默认资源给定的头像
            {
                HeadImage = Resources.default_head;
                Thread mythread = new Thread(AsynLoadImage);
                mythread.Start(subItem);
            }

            int x = 10;
            int y = rectSubItem.Bottom - (rectSubItem.Height / 2 + 20);
            Rectangle rec = new Rectangle(x, y, 40, 40);

            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.DrawImage(HeadImage, rec, new Rectangle(0, 0, HeadImage.Width, HeadImage.Height), GraphicsUnit.Pixel);

        }


        private void AsynLoadImage(Object arg)
        {
            RContact item = arg as RContact;
            string url = Context.root_uri + item.HeadImgUrl;
            Image image = api.HttpTools.GetResponseImage(url);
            item.HeadImage = image;
        }


    }
}
