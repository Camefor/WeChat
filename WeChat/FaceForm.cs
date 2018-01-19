using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeChat.Utils;
using WinForm.UI.Forms;

namespace WeChat
{
    public partial class FaceForm : BaseForm
    {
        private static FaceForm form;

        public static FaceForm GetFaceForm()
        {
            if (form == null)
                form = new FaceForm();
            return form;
        }

        private FaceForm()
        {
            InitializeComponent();
        }

        private void FaceForm_Load(object sender, EventArgs e)
        {
            string[] faces = EmojiTools.face.Split(',');
            string[] location= EmojiTools.location;
            for (int i = 0; i < location.Length; i++)
            {
                Label lbl = new Label();
                lbl.Margin = new Padding(0,0,0,0);
                lbl.AutoSize = false;
                lbl.Size = new Size(29, 29);
                toolTip1.SetToolTip(lbl, faces[i]);
                lbl.Click += Lbl_Click;
                lbl.Name = faces[i];
                flowLayoutPanel1.Controls.Add(lbl);
            }

        }

        private Point point;
        private Action<DialogResult, string> action;


        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.Location = point;
        }

        public void Show(Point Location,Action<DialogResult,string> action)
        {
            this.action = action;
            this.point = Location;
            point.Y = point.Y - this.Height - 20;
            point.X = point.X - 100;
            this.Location = point;
            this.Focus();
            base.Show();
            Deactivate += FaceForm_Deactivate;
        }

        public string Result { get; private set; }

        private void Lbl_Click(object sender, EventArgs e)
        {
            Label lbl = sender as Label;
            Result = lbl.Name;
            this.DialogResult = DialogResult.OK;
            Hide();
            OnResultEvent();
        }

        private void FaceForm_Deactivate(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Hide();
            OnResultEvent();
        }

        private new void Hide()
        {
            Deactivate -= FaceForm_Deactivate;
            base.Hide();
            action?.Invoke(DialogResult, Result);
        }

        public delegate void ResultHandler(FaceForm sender, EventArgs e);
        public event ResultHandler ResultEvent;
        public virtual void OnResultEvent()
        {
            ResultEvent?.Invoke(this, EventArgs.Empty);
        }
    }
}
