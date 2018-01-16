using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinForm.UI.Controls;

namespace WeChat.ListAdapter
{
    /***
    * ===========================================================
    * 创建人：yuanj
    * 创建时间：2018/01/16 15:58:00
    * 说明：表情包
    * ==========================================================
    * */
    public class ClownHolder
    {
        internal static void DrawItem(string path, ViewHolder holder, Graphics g, bool IsSend)
        {
            if (!File.Exists(path))
            {
                TextHolder.DrawItem("获取表情失败",holder,g,IsSend);
                return;
            }
            Image image = Image.FromFile(path);
           
            Point point= holder.bounds.Location;
            Rectangle rec = new Rectangle();
            rec.Width = image.Width;
            rec.Height = image.Height;
            if (image.Width > 300)
            {
                rec.Width = 300;
            }
            if (image.Height > 200)
            {
                rec.Height = 200;
            }
            rec.Y = point.Y + 10;
            if (IsSend)
            {
                rec.X = holder.bounds.Width - 95 - rec.Width;
            }
            else
            {
                rec.X = 70;
            }
            g.DrawImage(image, rec, new Rectangle(0, 0, image.Width, image.Height), GraphicsUnit.Pixel);
            holder.bounds.Height = rec.Height + 10;
        }

    }
}
