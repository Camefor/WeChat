namespace WeChat
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.pictureBoxSkin1 = new formSkin.Controls.PictureBoxSkin();
            this.pictureBoxSkin2 = new formSkin.Controls.PictureBoxSkin();
            this.pictureBoxSkin3 = new formSkin.Controls.PictureBoxSkin();
            this.pictureBoxSkin4 = new formSkin.Controls.PictureBoxSkin();
            this.pbHead = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.panel6 = new System.Windows.Forms.Panel();
            this.ContartList = new formSkin.Controls._List.FListView();
            this.LastList = new formSkin.Controls._List.FListView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.textBoxSkin1 = new formSkin.Controls.TextBoxSkin();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSkin1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSkin2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSkin3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSkin4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbHead)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.panel6.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(52)))));
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Controls.Add(this.pictureBoxSkin4);
            this.panel1.Controls.Add(this.pbHead);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(60, 640);
            this.panel1.TabIndex = 0;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.pictureBoxSkin1);
            this.panel5.Controls.Add(this.pictureBoxSkin2);
            this.panel5.Controls.Add(this.pictureBoxSkin3);
            this.panel5.Location = new System.Drawing.Point(0, 70);
            this.panel5.Margin = new System.Windows.Forms.Padding(0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(58, 171);
            this.panel5.TabIndex = 5;
            // 
            // pictureBoxSkin1
            // 
            this.pictureBoxSkin1.Image = global::WeChat.Properties.Resources.img_last_tab_no;
            this.pictureBoxSkin1.Location = new System.Drawing.Point(17, 15);
            this.pictureBoxSkin1.Name = "pictureBoxSkin1";
            this.pictureBoxSkin1.SelectedImage = global::WeChat.Properties.Resources.img_last_tab;
            this.pictureBoxSkin1.Size = new System.Drawing.Size(23, 23);
            this.pictureBoxSkin1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBoxSkin1.TabIndex = 1;
            this.pictureBoxSkin1.TabStop = false;
            this.pictureBoxSkin1.Tag = "0";
            this.pictureBoxSkin1.SelectedItem += new formSkin.Controls.PictureBoxSkin.PictureBoxSkinEventHandler(this.pictureBoxSkin1_SelectedItem);
            this.pictureBoxSkin1.Click += new System.EventHandler(this.pictureBoxSkin1_Click);
            // 
            // pictureBoxSkin2
            // 
            this.pictureBoxSkin2.Image = global::WeChat.Properties.Resources.img_Friends_tab_no;
            this.pictureBoxSkin2.Location = new System.Drawing.Point(17, 69);
            this.pictureBoxSkin2.Name = "pictureBoxSkin2";
            this.pictureBoxSkin2.SelectedImage = global::WeChat.Properties.Resources.img_Friends_tab;
            this.pictureBoxSkin2.Size = new System.Drawing.Size(23, 23);
            this.pictureBoxSkin2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBoxSkin2.TabIndex = 2;
            this.pictureBoxSkin2.TabStop = false;
            this.pictureBoxSkin2.Tag = "1";
            this.pictureBoxSkin2.SelectedItem += new formSkin.Controls.PictureBoxSkin.PictureBoxSkinEventHandler(this.pictureBoxSkin1_SelectedItem);
            this.pictureBoxSkin2.Click += new System.EventHandler(this.pictureBoxSkin1_Click);
            // 
            // pictureBoxSkin3
            // 
            this.pictureBoxSkin3.Image = global::WeChat.Properties.Resources.img_public_tab_no;
            this.pictureBoxSkin3.Location = new System.Drawing.Point(17, 118);
            this.pictureBoxSkin3.Name = "pictureBoxSkin3";
            this.pictureBoxSkin3.SelectedImage = global::WeChat.Properties.Resources.img_public_tab;
            this.pictureBoxSkin3.Size = new System.Drawing.Size(23, 23);
            this.pictureBoxSkin3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBoxSkin3.TabIndex = 3;
            this.pictureBoxSkin3.TabStop = false;
            this.pictureBoxSkin3.Tag = "2";
            this.pictureBoxSkin3.SelectedItem += new formSkin.Controls.PictureBoxSkin.PictureBoxSkinEventHandler(this.pictureBoxSkin1_SelectedItem);
            this.pictureBoxSkin3.Click += new System.EventHandler(this.pictureBoxSkin1_Click);
            // 
            // pictureBoxSkin4
            // 
            this.pictureBoxSkin4.Image = global::WeChat.Properties.Resources.img_menu_no;
            this.pictureBoxSkin4.Location = new System.Drawing.Point(17, 588);
            this.pictureBoxSkin4.Name = "pictureBoxSkin4";
            this.pictureBoxSkin4.Size = new System.Drawing.Size(23, 23);
            this.pictureBoxSkin4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBoxSkin4.TabIndex = 4;
            this.pictureBoxSkin4.TabStop = false;
            // 
            // pbHead
            // 
            this.pbHead.Image = global::WeChat.Properties.Resources.hh;
            this.pbHead.Location = new System.Drawing.Point(12, 17);
            this.pbHead.Name = "pbHead";
            this.pbHead.Size = new System.Drawing.Size(35, 35);
            this.pbHead.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbHead.TabIndex = 0;
            this.pbHead.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(231)))));
            this.panel2.Controls.Add(this.pictureBox3);
            this.panel2.Controls.Add(this.panel6);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Location = new System.Drawing.Point(60, 1);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(250, 639);
            this.panel2.TabIndex = 1;
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.pictureBox3.Image = global::WeChat.Properties.Resources.img_add;
            this.pictureBox3.Location = new System.Drawing.Point(206, 21);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(25, 25);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox3.TabIndex = 4;
            this.pictureBox3.TabStop = false;
            // 
            // panel6
            // 
            this.panel6.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel6.Controls.Add(this.ContartList);
            this.panel6.Controls.Add(this.LastList);
            this.panel6.Location = new System.Drawing.Point(1, 61);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(247, 577);
            this.panel6.TabIndex = 0;
            // 
            // ContartList
            // 
            this.ContartList.Adapter = null;
            this.ContartList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ContartList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ContartList.IntervalHeight = 1;
            this.ContartList.Location = new System.Drawing.Point(0, 148);
            this.ContartList.Name = "ContartList";
            this.ContartList.ScrollBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(231)))));
            this.ContartList.ScrollSliderDefaultColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(200)))), ((int)(((byte)(198)))));
            this.ContartList.ScrollSliderDownColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(151)))), ((int)(((byte)(151)))));
            this.ContartList.Size = new System.Drawing.Size(247, 77);
            this.ContartList.TabIndex = 6;
            this.ContartList.Text = "fListView1";
            this.ContartList.Visible = false;
            // 
            // LastList
            // 
            this.LastList.Adapter = null;
            this.LastList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LastList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.LastList.IntervalHeight = 1;
            this.LastList.Location = new System.Drawing.Point(0, 23);
            this.LastList.Name = "LastList";
            this.LastList.ScrollBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(231)))));
            this.LastList.ScrollSliderDefaultColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(200)))), ((int)(((byte)(198)))));
            this.LastList.ScrollSliderDownColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(151)))), ((int)(((byte)(151)))));
            this.LastList.Size = new System.Drawing.Size(247, 77);
            this.LastList.TabIndex = 5;
            this.LastList.Text = "fListView1";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.textBoxSkin1);
            this.panel3.Controls.Add(this.pictureBox2);
            this.panel3.Location = new System.Drawing.Point(8, 21);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(182, 25);
            this.panel3.TabIndex = 3;
            // 
            // textBoxSkin1
            // 
            this.textBoxSkin1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.textBoxSkin1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(215)))), ((int)(((byte)(214)))));
            this.textBoxSkin1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxSkin1.Location = new System.Drawing.Point(3, 4);
            this.textBoxSkin1.Name = "textBoxSkin1";
            this.textBoxSkin1.Size = new System.Drawing.Size(148, 16);
            this.textBoxSkin1.TabIndex = 1;
            this.textBoxSkin1.WatermarkText = "搜索";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox2.Image = global::WeChat.Properties.Resources.img_search;
            this.pictureBox2.Location = new System.Drawing.Point(155, 0);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(23, 23);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox2.TabIndex = 2;
            this.pictureBox2.TabStop = false;
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.BackgroundImage = global::WeChat.Properties.Resources.img_bg;
            this.panel4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.panel4.Location = new System.Drawing.Point(310, 27);
            this.panel4.Margin = new System.Windows.Forms.Padding(0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(550, 613);
            this.panel4.TabIndex = 2;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(860, 640);
            this.CloseBoxBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(84)))), ((int)(((byte)(84)))));
            this.CloseBoxFontColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(94)))), ((int)(((byte)(94)))));
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsLogo = false;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaxBoxBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(227)))), ((int)(((byte)(227)))));
            this.MaxBoxFontColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(94)))), ((int)(((byte)(94)))));
            this.MinBoxBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(227)))), ((int)(((byte)(227)))));
            this.MinBoxFontColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(94)))), ((int)(((byte)(94)))));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "微 信";
            this.TitleBackColorColor = System.Drawing.Color.Transparent;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSkin1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSkin2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSkin3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSkin4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbHead)).EndInit();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.panel6.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pbHead;
        private System.Windows.Forms.Panel panel2;
        private formSkin.Controls.TextBoxSkin textBoxSkin1;
        private formSkin.Controls.PictureBoxSkin pictureBoxSkin1;
        private formSkin.Controls.PictureBoxSkin pictureBoxSkin3;
        private formSkin.Controls.PictureBoxSkin pictureBoxSkin2;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Panel panel4;
        private formSkin.Controls._List.FListView LastList;
        private formSkin.Controls.PictureBoxSkin pictureBoxSkin4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel6;
        private formSkin.Controls._List.FListView ContartList;
    }
}

