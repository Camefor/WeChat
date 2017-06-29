namespace WeChat
{
    partial class LoginForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblMessageInfo = new System.Windows.Forms.Label();
            this.AsynLogin = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(74)))), ((int)(((byte)(74)))));
            this.label1.Location = new System.Drawing.Point(12, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "微 信";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(46, 83);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(189, 188);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // lblMessageInfo
            // 
            this.lblMessageInfo.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblMessageInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(132)))), ((int)(((byte)(132)))), ((int)(((byte)(132)))));
            this.lblMessageInfo.Location = new System.Drawing.Point(16, 312);
            this.lblMessageInfo.Name = "lblMessageInfo";
            this.lblMessageInfo.Size = new System.Drawing.Size(252, 20);
            this.lblMessageInfo.TabIndex = 2;
            this.lblMessageInfo.Text = "请使用微信扫一扫以登录";
            this.lblMessageInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // AsynLogin
            // 
            this.AsynLogin.WorkerReportsProgress = true;
            this.AsynLogin.DoWork += new System.ComponentModel.DoWorkEventHandler(this.AsynLogin_DoWork);
            this.AsynLogin.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.AsynLogin_ProgressChanged);
            this.AsynLogin.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.AsynLogin_RunWorkerCompleted);
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(280, 400);
            this.CloseBoxBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(84)))), ((int)(((byte)(84)))));
            this.CloseBoxFontColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(139)))), ((int)(((byte)(139)))));
            this.Controls.Add(this.lblMessageInfo);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsLogo = false;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "登 录";
            this.TitleBackColorColor = System.Drawing.Color.WhiteSmoke;
            this.Load += new System.EventHandler(this.LoginForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblMessageInfo;
        private System.ComponentModel.BackgroundWorker AsynLogin;
    }
}