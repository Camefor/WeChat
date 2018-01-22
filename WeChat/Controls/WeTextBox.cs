using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WeChat.Controls
{
    /***
    * ===========================================================
    * 创建人：yuanj
    * 创建时间：2018/01/19 17:16:17
    * 说明：
    * ==========================================================
    * */
    public class WeTextBox : RichTextBox
    {

        public WeTextBox()
        {
            base.ImeMode= ImeMode.OnHalf; 
        }


        public List<Image> GetImages()
        {
            string rtf = Rtf;
            if (!string.IsNullOrEmpty(rtf))
            {
                List<Image> ImageList = GetImagesByRtf(rtf);
                return ImageList;
            }
            return null;
        }

        private List<Image> GetImagesByRtf(string rtfText)
        {
            var imageList = new List<Image>();
            int width;
            int height;
            int.TryParse(Regex.Match(rtfText, @"(?<=picw(-)?)[\d]+(?=\\pich)").Value, out width);
            if (width != 0)
                width = width / 26;

            int.TryParse(Regex.Match(rtfText, @"(?<=pich)[\d]+(?=\\picwgoal)").Value, out height);
            if (height != 0)
                height = height / 26;
            while (true)
            {
                var index = rtfText.IndexOf("pichgoal", StringComparison.Ordinal);
                if (index == -1) break;
                try
                {
                    rtfText = rtfText.Remove(0, index + 8);
                    index = rtfText.IndexOf("\r\n", StringComparison.Ordinal);
                    rtfText = rtfText.Remove(0, index);
                    index = rtfText.IndexOf("}", StringComparison.Ordinal);
                    var imgByteStr = rtfText.Substring(0, index).Replace("\r\n", "");
                    rtfText = rtfText.Remove(0, index);
                    var count = imgByteStr.Length / 2;
                    var bts = new byte[count];
                    for (var i = 0; i != count; i++)
                    {
                        var tempText = imgByteStr[i * 2] + imgByteStr[(i * 2) + 1].ToString();
                        bts[i] = Convert.ToByte(tempText, 16);
                    }
                    var ms = new MemoryStream(bts);
                    var img = Image.FromStream(ms);
                    using (var oriBmp = new Bitmap(img, width, height))
                    {
                        img = (Image)oriBmp.Clone();
                        imageList.Add(img);
                    }

                }
                catch
                {
                    continue;
                }
            }
            return imageList;
        }
    }
}
