using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeChat.Business.Utils
{
    /***
    * ===========================================================
    * 创建人：yuanj
    * 创建时间：2017/08/19 17:34:04
    * 说明：
    * ==========================================================
    * */
    public class GraphicsUtils
    {
        /// <summary>
        /// 获取字符串的长度
        /// </summary>
        /// <param name="text"></param>
        /// <param name="g"></param>
        /// <param name="font"></param>
        /// <returns></returns>
        public static Size GetStringWidth(string text, Graphics g, Font font)
        {
            SizeF size = g.MeasureString(text, font);
            return size.ToSize();
        }

        /// <summary>
        /// 绘制圆角矩形
        /// </summary>
        /// <param name="rectangle"></param>
        /// <param name="g"></param>
        /// <param name="_radius">圆的度数</param>
        /// <param name="cusp">画不画尖角</param>
        /// <param name="back_color">渐变色的起始</param>
        public static void Draw(Rectangle rectangle, Graphics g, int _radius, bool cusp, Color back_color)
        {
            int span = 2;
            //抗锯齿
            g.SmoothingMode = SmoothingMode.AntiAlias;
            using (Brush hrush = new SolidBrush(back_color))
            {
                //画尖角
                if (cusp)
                {
                    span = 10;
                    PointF p1 = new PointF(rectangle.Width - 12, rectangle.Y + 10);
                    PointF p2 = new PointF(rectangle.Width - 12, rectangle.Y + 30);
                    PointF p3 = new PointF(rectangle.Width, rectangle.Y + 20);
                    PointF[] ptsArray = { p1, p2, p3 };
                    g.FillPolygon(hrush, ptsArray);
                }
                //填充
                g.FillPath(hrush, DrawRoundRect(rectangle.X, rectangle.Y, rectangle.Width - span, rectangle.Height - 1, _radius));
            }
        }


        public static GraphicsPath DrawRoundRect(int x, int y, int width, int height, int radius)
        {
            //四边圆角
            GraphicsPath gp = new GraphicsPath();
            gp.AddArc(x, y, radius, radius, 180, 90);
            gp.AddArc(width - radius, y, radius, radius, 270, 90);
            gp.AddArc(width - radius, height - radius, radius, radius, 0, 90);
            gp.AddArc(x, height - radius, radius, radius, 90, 90);
            gp.CloseAllFigures();
            return gp;
        }

        /// <summary>
        /// 圆角矩形
        /// </summary>
        /// <param name="g"></param>
        /// <param name="brush"></param>
        /// <param name="rect"></param>
        /// <param name="cornerRadius"></param>
        public static void FillRoundRectangle(Graphics g, Brush brush, Rectangle rect, int cornerRadius)
        {
            using (GraphicsPath path = CreateRoundedRectanglePath(rect, cornerRadius))
            {
                g.FillPath(brush, path);
            }
        }
        internal static GraphicsPath CreateRoundedRectanglePath(Rectangle rect, int cornerRadius)
        {
            GraphicsPath roundedRect = new GraphicsPath();
            roundedRect.AddArc(rect.X, rect.Y, cornerRadius * 2, cornerRadius * 2, 180, 90);
            roundedRect.AddLine(rect.X + cornerRadius, rect.Y, rect.Right - cornerRadius * 2, rect.Y);
            roundedRect.AddArc(rect.X + rect.Width - cornerRadius * 2, rect.Y, cornerRadius * 2, cornerRadius * 2, 270, 90);
            roundedRect.AddLine(rect.Right, rect.Y + cornerRadius * 2, rect.Right, rect.Y + rect.Height - cornerRadius * 2);
            roundedRect.AddArc(rect.X + rect.Width - cornerRadius * 2, rect.Y + rect.Height - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 0, 90);
            roundedRect.AddLine(rect.Right - cornerRadius * 2, rect.Bottom, rect.X + cornerRadius * 2, rect.Bottom);
            roundedRect.AddArc(rect.X, rect.Bottom - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 90, 90);
            roundedRect.AddLine(rect.X, rect.Bottom - cornerRadius * 2, rect.X, rect.Y + cornerRadius * 2);
            roundedRect.CloseFigure();
            return roundedRect;
        }
    }
}
