using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
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
    public class WeTextBox: RichTextBox
    {
        private List<Bitmap> pic = new List<Bitmap>();

        public WeTextBox()
        {
        }

        public int FileCount { get { return pic.Count; } }


        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (e.Control && e.KeyCode == Keys.V)
            {
                IDataObject data = Clipboard.GetDataObject();
                if (data.GetDataPresent(DataFormats.Bitmap))
                {
                    Bitmap bit = (Bitmap)data.GetData(DataFormats.Bitmap, true);
                    pic.Add(bit);
                }
            }
        }

    }
}
