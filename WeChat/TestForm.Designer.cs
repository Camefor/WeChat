namespace WeChat
{
    partial class TestForm
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
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.fListView1 = new WinForm.UI.Controls.FListView();
            this.SuspendLayout();
            // 
            // txtMessage
            // 
            this.txtMessage.Location = new System.Drawing.Point(13, 498);
            this.txtMessage.Multiline = true;
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(542, 79);
            this.txtMessage.TabIndex = 1;
            this.txtMessage.Text = "呵呵哒[微笑]。。。";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(472, 584);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(83, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // fListView1
            // 
            this.fListView1.Adapter = null;
            this.fListView1.ItemDivider = 10;
            this.fListView1.Location = new System.Drawing.Point(12, 12);
            this.fListView1.Name = "fListView1";
            this.fListView1.Size = new System.Drawing.Size(544, 479);
            this.fListView1.TabIndex = 0;
            this.fListView1.Text = "fListView1";
            // 
            // TestForm
            // 
            this.AcceptButton = this.button1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(567, 621);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtMessage);
            this.Controls.Add(this.fListView1);
            this.Name = "TestForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TestForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private WinForm.UI.Controls.FListView fListView1;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.Button button1;
    }
}