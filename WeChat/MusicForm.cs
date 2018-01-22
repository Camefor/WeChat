using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WeChat
{
    public partial class MusicForm : Form
    {
        public MusicForm()
        {
            InitializeComponent();
            axWindowsMediaPlayer1.settings.volume = 100;//音量
        }

        /// <summary>
        /// 播放
        /// </summary>
        /// <param name="path"></param>
        public void Player(string path)
        {
            axWindowsMediaPlayer1.Ctlcontrols.stop();
            axWindowsMediaPlayer1.URL = path;
            axWindowsMediaPlayer1.Ctlcontrols.play();//播放文件
        }

        public void Stop()
        {
            axWindowsMediaPlayer1.Ctlcontrols.stop();
        }

    }
}
