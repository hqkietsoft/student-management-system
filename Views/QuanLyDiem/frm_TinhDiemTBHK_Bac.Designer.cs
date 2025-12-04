namespace QuanLySinhVien_Nhom2.QuanLyDiem
{
	partial class frm_TinhDiemTBHK_Bac
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.cbo_ThongTinSV = new System.Windows.Forms.ComboBox();
            this.btnTinhDiem = new System.Windows.Forms.Button();
            this.btnXetHocBong = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_TTTimKiem = new System.Windows.Forms.TextBox();
            this.cbo_hocky = new System.Windows.Forms.ComboBox();
            this.cbo_hocky_diem = new System.Windows.Forms.ComboBox();
            this.dgv_DiemTBHK = new Guna.UI2.WinForms.Guna2DataGridView();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column16 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.guna2GroupBox1 = new Guna.UI2.WinForms.Guna2GroupBox();
            this.guna2GroupBox2 = new Guna.UI2.WinForms.Guna2GroupBox();
            this.guna2Button1 = new Guna.UI2.WinForms.Guna2Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_DiemTBHK)).BeginInit();
            this.guna2GroupBox1.SuspendLayout();
            this.guna2GroupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbo_ThongTinSV
            // 
            this.cbo_ThongTinSV.DisplayMember = "MaSV";
            this.cbo_ThongTinSV.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbo_ThongTinSV.FormattingEnabled = true;
            this.cbo_ThongTinSV.Location = new System.Drawing.Point(212, 42);
            this.cbo_ThongTinSV.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbo_ThongTinSV.Name = "cbo_ThongTinSV";
            this.cbo_ThongTinSV.Size = new System.Drawing.Size(167, 33);
            this.cbo_ThongTinSV.TabIndex = 7;
            this.cbo_ThongTinSV.SelectedIndexChanged += new System.EventHandler(this.cbo_ThongTinSV_SelectedIndexChanged);
            // 
            // btnTinhDiem
            // 
            this.btnTinhDiem.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTinhDiem.Location = new System.Drawing.Point(405, 42);
            this.btnTinhDiem.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnTinhDiem.Name = "btnTinhDiem";
            this.btnTinhDiem.Size = new System.Drawing.Size(112, 31);
            this.btnTinhDiem.TabIndex = 9;
            this.btnTinhDiem.Text = "Tính điểm";
            this.btnTinhDiem.UseVisualStyleBackColor = true;
            this.btnTinhDiem.Click += new System.EventHandler(this.btnTinhDiem_Click);
            // 
            // btnXetHocBong
            // 
            this.btnXetHocBong.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXetHocBong.Location = new System.Drawing.Point(143, 45);
            this.btnXetHocBong.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnXetHocBong.Name = "btnXetHocBong";
            this.btnXetHocBong.Size = new System.Drawing.Size(129, 31);
            this.btnXetHocBong.TabIndex = 10;
            this.btnXetHocBong.Text = "Xét học bổng";
            this.btnXetHocBong.UseVisualStyleBackColor = true;
            this.btnXetHocBong.Click += new System.EventHandler(this.btnXetHocBong_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 14F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(175, 92);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(300, 25);
            this.label2.TabIndex = 12;
            this.label2.Text = "Tìm kiếm theo điểm trung bình : ";
            // 
            // txt_TTTimKiem
            // 
            this.txt_TTTimKiem.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_TTTimKiem.Location = new System.Drawing.Point(483, 92);
            this.txt_TTTimKiem.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txt_TTTimKiem.Name = "txt_TTTimKiem";
            this.txt_TTTimKiem.Size = new System.Drawing.Size(153, 29);
            this.txt_TTTimKiem.TabIndex = 13;
            this.txt_TTTimKiem.TextChanged += new System.EventHandler(this.txt_TTTimKiem_TextChanged);
            // 
            // cbo_hocky
            // 
            this.cbo_hocky.Font = new System.Drawing.Font("Times New Roman", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbo_hocky.FormattingEnabled = true;
            this.cbo_hocky.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4"});
            this.cbo_hocky.Location = new System.Drawing.Point(4, 46);
            this.cbo_hocky.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbo_hocky.Name = "cbo_hocky";
            this.cbo_hocky.Size = new System.Drawing.Size(136, 31);
            this.cbo_hocky.TabIndex = 0;
            // 
            // cbo_hocky_diem
            // 
            this.cbo_hocky_diem.Font = new System.Drawing.Font("Times New Roman", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbo_hocky_diem.FormattingEnabled = true;
            this.cbo_hocky_diem.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4"});
            this.cbo_hocky_diem.Location = new System.Drawing.Point(19, 44);
            this.cbo_hocky_diem.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbo_hocky_diem.Name = "cbo_hocky_diem";
            this.cbo_hocky_diem.Size = new System.Drawing.Size(167, 31);
            this.cbo_hocky_diem.TabIndex = 11;
            this.cbo_hocky_diem.SelectedIndexChanged += new System.EventHandler(this.cbo_hocky_diem_SelectedIndexChanged);
            // 
            // dgv_DiemTBHK
            // 
            this.dgv_DiemTBHK.AllowUserToAddRows = false;
            this.dgv_DiemTBHK.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.dgv_DiemTBHK.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.RoyalBlue;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(254)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.RoyalBlue;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_DiemTBHK.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_DiemTBHK.ColumnHeadersHeight = 42;
            this.dgv_DiemTBHK.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.dgv_DiemTBHK.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column9,
            this.Column10,
            this.Column11,
            this.Column12,
            this.Column13,
            this.Column14,
            this.Column15,
            this.Column16});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(254)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_DiemTBHK.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgv_DiemTBHK.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgv_DiemTBHK.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgv_DiemTBHK.Location = new System.Drawing.Point(0, 122);
            this.dgv_DiemTBHK.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dgv_DiemTBHK.Name = "dgv_DiemTBHK";
            this.dgv_DiemTBHK.ReadOnly = true;
            this.dgv_DiemTBHK.RowHeadersVisible = false;
            this.dgv_DiemTBHK.RowHeadersWidth = 62;
            this.dgv_DiemTBHK.RowTemplate.Height = 28;
            this.dgv_DiemTBHK.Size = new System.Drawing.Size(870, 246);
            this.dgv_DiemTBHK.TabIndex = 16;
            this.dgv_DiemTBHK.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.dgv_DiemTBHK.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.dgv_DiemTBHK.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.dgv_DiemTBHK.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.dgv_DiemTBHK.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.dgv_DiemTBHK.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.dgv_DiemTBHK.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgv_DiemTBHK.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.dgv_DiemTBHK.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgv_DiemTBHK.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(254)));
            this.dgv_DiemTBHK.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.dgv_DiemTBHK.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.dgv_DiemTBHK.ThemeStyle.HeaderStyle.Height = 42;
            this.dgv_DiemTBHK.ThemeStyle.ReadOnly = true;
            this.dgv_DiemTBHK.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.dgv_DiemTBHK.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgv_DiemTBHK.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(254)));
            this.dgv_DiemTBHK.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.dgv_DiemTBHK.ThemeStyle.RowsStyle.Height = 28;
            this.dgv_DiemTBHK.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgv_DiemTBHK.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            // 
            // Column9
            // 
            this.Column9.DataPropertyName = "MaSV";
            this.Column9.HeaderText = "Mã sinh viên";
            this.Column9.MinimumWidth = 8;
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            // 
            // Column10
            // 
            this.Column10.DataPropertyName = "HoDem";
            this.Column10.HeaderText = "Họ đệm";
            this.Column10.MinimumWidth = 8;
            this.Column10.Name = "Column10";
            this.Column10.ReadOnly = true;
            // 
            // Column11
            // 
            this.Column11.DataPropertyName = "Ten";
            this.Column11.HeaderText = "Tên";
            this.Column11.MinimumWidth = 8;
            this.Column11.Name = "Column11";
            this.Column11.ReadOnly = true;
            // 
            // Column12
            // 
            this.Column12.DataPropertyName = "MaMH";
            this.Column12.HeaderText = "Mã môn học";
            this.Column12.MinimumWidth = 8;
            this.Column12.Name = "Column12";
            this.Column12.ReadOnly = true;
            // 
            // Column13
            // 
            this.Column13.DataPropertyName = "TenMH";
            this.Column13.HeaderText = "Tên môn học";
            this.Column13.MinimumWidth = 8;
            this.Column13.Name = "Column13";
            this.Column13.ReadOnly = true;
            // 
            // Column14
            // 
            this.Column14.DataPropertyName = "SoTC";
            this.Column14.HeaderText = "STC";
            this.Column14.MinimumWidth = 8;
            this.Column14.Name = "Column14";
            this.Column14.ReadOnly = true;
            // 
            // Column15
            // 
            this.Column15.DataPropertyName = "DiemHocPhan";
            this.Column15.HeaderText = "Điểm HP";
            this.Column15.MinimumWidth = 8;
            this.Column15.Name = "Column15";
            this.Column15.ReadOnly = true;
            // 
            // Column16
            // 
            this.Column16.DataPropertyName = "DiemTBHK";
            this.Column16.HeaderText = "Điểm TBHK";
            this.Column16.MinimumWidth = 8;
            this.Column16.Name = "Column16";
            this.Column16.ReadOnly = true;
            // 
            // guna2GroupBox1
            // 
            this.guna2GroupBox1.Controls.Add(this.btnXetHocBong);
            this.guna2GroupBox1.Controls.Add(this.cbo_hocky);
            this.guna2GroupBox1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2GroupBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.guna2GroupBox1.Location = new System.Drawing.Point(0, 1);
            this.guna2GroupBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.guna2GroupBox1.Name = "guna2GroupBox1";
            this.guna2GroupBox1.Size = new System.Drawing.Size(301, 82);
            this.guna2GroupBox1.TabIndex = 17;
            this.guna2GroupBox1.Text = "Xét học bổng";
            this.guna2GroupBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // guna2GroupBox2
            // 
            this.guna2GroupBox2.Controls.Add(this.guna2Button1);
            this.guna2GroupBox2.Controls.Add(this.cbo_hocky_diem);
            this.guna2GroupBox2.Controls.Add(this.btnTinhDiem);
            this.guna2GroupBox2.Controls.Add(this.cbo_ThongTinSV);
            this.guna2GroupBox2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2GroupBox2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.guna2GroupBox2.Location = new System.Drawing.Point(299, 1);
            this.guna2GroupBox2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.guna2GroupBox2.Name = "guna2GroupBox2";
            this.guna2GroupBox2.Size = new System.Drawing.Size(571, 82);
            this.guna2GroupBox2.TabIndex = 18;
            this.guna2GroupBox2.Text = "TÍNH ĐIỂM TRUNG BÌNH CHUNG THEO HỌC KỲ CỦA TỪNG SINH VIÊN";
            this.guna2GroupBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // guna2Button1
            // 
            this.guna2Button1.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button1.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button1.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.guna2Button1.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.guna2Button1.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2Button1.ForeColor = System.Drawing.Color.White;
            this.guna2Button1.Location = new System.Drawing.Point(533, 1);
            this.guna2Button1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.guna2Button1.Name = "guna2Button1";
            this.guna2Button1.Size = new System.Drawing.Size(37, 31);
            this.guna2Button1.TabIndex = 19;
            this.guna2Button1.Text = "X";
            this.guna2Button1.Click += new System.EventHandler(this.guna2Button1_Click);
            // 
            // frm_TinhDiemTBHK_Bac
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.ForestGreen;
            this.ClientSize = new System.Drawing.Size(870, 368);
            this.Controls.Add(this.guna2GroupBox2);
            this.Controls.Add(this.guna2GroupBox1);
            this.Controls.Add(this.dgv_DiemTBHK);
            this.Controls.Add(this.txt_TTTimKiem);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "frm_TinhDiemTBHK_Bac";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.frm_TinhDiemTBHK_Bac_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_DiemTBHK)).EndInit();
            this.guna2GroupBox1.ResumeLayout(false);
            this.guna2GroupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.ComboBox cbo_ThongTinSV;
		private System.Windows.Forms.Button btnTinhDiem;
		private System.Windows.Forms.Button btnXetHocBong;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txt_TTTimKiem;
		private System.Windows.Forms.ComboBox cbo_hocky;
		private System.Windows.Forms.ComboBox cbo_hocky_diem;
		private Guna.UI2.WinForms.Guna2DataGridView dgv_DiemTBHK;
		private Guna.UI2.WinForms.Guna2GroupBox guna2GroupBox1;
		private Guna.UI2.WinForms.Guna2GroupBox guna2GroupBox2;
		private Guna.UI2.WinForms.Guna2Button guna2Button1;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column12;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column13;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column14;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column15;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column16;
	}
}