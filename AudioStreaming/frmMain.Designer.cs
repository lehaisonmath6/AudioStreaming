namespace AudioStreaming
{
    partial class frmMain
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
            this.txtIPServer = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnKetNoi = new System.Windows.Forms.Button();
            this.txtNoiDung = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lsvDanhSachDangPhat = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btnMoNhac = new System.Windows.Forms.Button();
            this.btnPause = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtIPServer
            // 
            this.txtIPServer.Location = new System.Drawing.Point(86, 38);
            this.txtIPServer.Name = "txtIPServer";
            this.txtIPServer.Size = new System.Drawing.Size(165, 20);
            this.txtIPServer.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "IP Server";
            // 
            // btnKetNoi
            // 
            this.btnKetNoi.Location = new System.Drawing.Point(257, 36);
            this.btnKetNoi.Name = "btnKetNoi";
            this.btnKetNoi.Size = new System.Drawing.Size(102, 23);
            this.btnKetNoi.TabIndex = 2;
            this.btnKetNoi.Text = "Kết nối";
            this.btnKetNoi.UseVisualStyleBackColor = true;
            this.btnKetNoi.Click += new System.EventHandler(this.btnKetNoi_Click);
            // 
            // txtNoiDung
            // 
            this.txtNoiDung.Location = new System.Drawing.Point(86, 88);
            this.txtNoiDung.Name = "txtNoiDung";
            this.txtNoiDung.Size = new System.Drawing.Size(165, 20);
            this.txtNoiDung.TabIndex = 4;
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(86, 126);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(84, 23);
            this.btnSend.TabIndex = 5;
            this.btnSend.Text = "Play";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lsvDanhSachDangPhat);
            this.groupBox1.Location = new System.Drawing.Point(32, 255);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(327, 163);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Danh sách đang phát";
            // 
            // lsvDanhSachDangPhat
            // 
            this.lsvDanhSachDangPhat.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.lsvDanhSachDangPhat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lsvDanhSachDangPhat.FullRowSelect = true;
            this.lsvDanhSachDangPhat.Location = new System.Drawing.Point(3, 16);
            this.lsvDanhSachDangPhat.Name = "lsvDanhSachDangPhat";
            this.lsvDanhSachDangPhat.Size = new System.Drawing.Size(321, 144);
            this.lsvDanhSachDangPhat.TabIndex = 0;
            this.lsvDanhSachDangPhat.UseCompatibleStateImageBehavior = false;
            this.lsvDanhSachDangPhat.View = System.Windows.Forms.View.Details;
            this.lsvDanhSachDangPhat.SelectedIndexChanged += new System.EventHandler(this.lsvDanhSachDangPhat_SelectedIndexChanged);
            this.lsvDanhSachDangPhat.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lsvDanhSachDangPhat_MouseDoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Nguồn phát";
            this.columnHeader1.Width = 142;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Tên bài hát";
            this.columnHeader2.Width = 181;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // btnMoNhac
            // 
            this.btnMoNhac.Location = new System.Drawing.Point(257, 86);
            this.btnMoNhac.Name = "btnMoNhac";
            this.btnMoNhac.Size = new System.Drawing.Size(102, 23);
            this.btnMoNhac.TabIndex = 7;
            this.btnMoNhac.Text = "Mở nhạc";
            this.btnMoNhac.UseVisualStyleBackColor = true;
            this.btnMoNhac.Click += new System.EventHandler(this.btnMoNhac_Click);
            // 
            // btnPause
            // 
            this.btnPause.Location = new System.Drawing.Point(176, 126);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(75, 23);
            this.btnPause.TabIndex = 8;
            this.btnPause.Text = "Stop";
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(83, 192);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(35, 13);
            this.lblStatus.TabIndex = 9;
            this.lblStatus.Text = "label2";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(371, 430);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnPause);
            this.Controls.Add(this.btnMoNhac);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.txtNoiDung);
            this.Controls.Add(this.btnKetNoi);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtIPServer);
            this.Name = "frmMain";
            this.Text = "Trình nghe nhạc mạng lan";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtIPServer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnKetNoi;
        private System.Windows.Forms.TextBox txtNoiDung;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListView lsvDanhSachDangPhat;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btnMoNhac;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Label lblStatus;
    }
}

