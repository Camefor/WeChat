namespace WeChat
{
    partial class RContactForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ContartList = new WinForm.UI.Controls.FListView();
            this.SuspendLayout();
            // 
            // ContartList
            // 
            this.ContartList.Adapter = null;
            this.ContartList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ContartList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(232)))));
            this.ContartList.Location = new System.Drawing.Point(176, 12);
            this.ContartList.Name = "ContartList";
            this.ContartList.ScrollBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(231)))));
            this.ContartList.Size = new System.Drawing.Size(274, 559);
            this.ContartList.TabIndex = 7;
            this.ContartList.Text = "fListView1";
            // 
            // RContactForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(598, 650);
            this.Controls.Add(this.ContartList);
            this.Name = "RContactForm";
            this.Text = "RContactForm";
            this.Load += new System.EventHandler(this.RContactForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private WinForm.UI.Controls.FListView ContartList;
    }
}