using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeChat.Business.Model;
using WeChat.Business.Utils;
using WinForm.UI.Controls;

namespace WeChat.ListAdapter
{
    /***
    * ===========================================================
    * 创建人：yuanj
    * 创建时间：2018/01/16 10:34:29
    * 说明：
    * ==========================================================
    * */
    public class TextHolder
    {
        private static Font font;
        private static Color backColor=Color.White;
        private static Color TriangleColor = Color.FromArgb(158, 234, 106);
        private static Color TextColor = Color.FromArgb(35, 35, 35);
        private static StringFormat StringFormat;


        static TextHolder()
        {
            font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            StringFormat = new StringFormat()
            {
                Alignment = StringAlignment.Center,
                LineAlignment= StringAlignment.Center

            };

        }

        internal static void DrawItem(string content, ViewHolder holder, Graphics g,bool IsSend)
        {
            Size size= GraphicsUtils.GetStringWidth(content, g, font);
            holder.bounds.Height = size.Height + 20;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            Rectangle point = holder.bounds;
            Point[] points = GetPolygon( point, IsSend);
            using (SolidBrush brushes = new SolidBrush(TriangleColor))
            {
                g.FillPolygon(brushes, points);
                Rectangle rect = Rectangle.Empty;
                if (IsSend)
                {
                    rect = new Rectangle(holder.bounds.Width - 95 - size.Width, point.Y + 10, size.Width + 15, size.Height + 15);
                }
                else
                {
                    rect = new Rectangle(70, point.Y + 10, size.Width + 15, size.Height + 15);
                }
                GraphicsUtils.FillRoundRectangle(g, brushes, rect, 4);
                brushes.Color = TextColor;
                g.DrawString(content, font, brushes, rect, StringFormat);

            }
            g.SmoothingMode = SmoothingMode.None;
        }


        private static Point[] GetPolygon( Rectangle point,bool IsSend)
        {
            Point[] points = new Point[3];
            int x = 0, y = 0;
            if (IsSend)
            {
                x = point.Width-80;
                y =point.Y+15;
                points[0] = new Point(x, y);

                x = point.Width-75;
                y = y +3;
                points[1] = new Point(x, y);

                x = point.Width- 80;
                y = y+3;
                points[2] = new Point(x, y);
            }else
            {
                x = 70;
                y = point.Y + 15;
                points[0] = new Point(x, y);

                x = 65;
                y = y + 3;
                points[1] = new Point(x, y);

                x = 70;
                y = y + 3;
                points[2] = new Point(x, y);
            }
            return points;
        }

    }
}
