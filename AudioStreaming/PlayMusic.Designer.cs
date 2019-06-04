namespace AudioStreaming
{
    partial class PlayMusic
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
            this.label1 = new System.Windows.Forms.Label();
            this.lblTenBaiHat = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(45, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(122, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Bạn đang nghe bài hát :";
            // 
            // lblTenBaiHat
            // 
            this.lblTenBaiHat.AutoSize = true;
            this.lblTenBaiHat.Location = new System.Drawing.Point(184, 42);
            this.lblTenBaiHat.Name = "lblTenBaiHat";
            this.lblTenBaiHat.Size = new System.Drawing.Size(68, 13);
            this.lblTenBaiHat.TabIndex = 1;
            this.lblTenBaiHat.Text = "lblTenBaiHat";
            // 
            // PlayMusic
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(605, 92);
            this.Controls.Add(this.lblTenBaiHat);
            this.Controls.Add(this.label1);
            this.Name = "PlayMusic";
            this.Text = "PlayMusic";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PlayMusic_FormClosing);
            this.Load += new System.EventHandler(this.PlayMusic_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblTenBaiHat;
    }
}