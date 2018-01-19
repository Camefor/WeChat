namespace WeChat
{
    partial class TestForm1
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
            this.fListView1 = new WinForm.UI.Controls.FListView();
            this.weTextBox1 = new WeChat.Controls.WeTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // fListView1
            // 
            this.fListView1.Adapter = null;
            this.fListView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fListView1.BackColor = System.Drawing.Color.White;
            this.fListView1.ItemDivider = 10;
            this.fListView1.Location = new System.Drawing.Point(110, 15);
            this.fListView1.Margin = new System.Windows.Forms.Padding(0);
            this.fListView1.MouseHolder = null;
            this.fListView1.Name = "fListView1";
            this.fListView1.ScrollBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(201)))), ((int)(((byte)(198)))));
            this.fListView1.SelectHolder = null;
            this.fListView1.Size = new System.Drawing.Size(544, 460);
            this.fListView1.TabIndex = 1;
            this.fListView1.Text = "fListView1";
            // 
            // weTextBox1
            // 
            this.weTextBox1.Location = new System.Drawing.Point(110, 478);
            this.weTextBox1.Name = "weTextBox1";
            this.weTextBox1.Size = new System.Drawing.Size(544, 80);
            this.weTextBox1.TabIndex = 2;
            this.weTextBox1.Text = "";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(660, 535);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // TestForm1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(764, 570);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.weTextBox1);
            this.Controls.Add(this.fListView1);
            this.Name = "TestForm1";
            this.Text = "TestForm1";
            this.Load += new System.EventHandler(this.TestForm1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private WinForm.UI.Controls.FListView fListView1;
        private Controls.WeTextBox weTextBox1;
        private System.Windows.Forms.Button button1;
    }
}