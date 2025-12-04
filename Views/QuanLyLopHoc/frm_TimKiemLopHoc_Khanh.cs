using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nhom2_QuanLySinhVien
{
    public partial class frm_TimKiemLopHoc_Khanh : Form
    {
        public frm_TimKiemLopHoc_Khanh()
        {
            InitializeComponent();
        }
        private DataTable danhSachLop;

        public frm_TimKiemLopHoc_Khanh(DataTable dt)
        {
            InitializeComponent();
            danhSachLop = dt;
        }
        private void HienThiDanhSachLop()
        {
            dgv_TimKiem_Khanh.DataSource = danhSachLop;
        }
        private void frm_TimKiem_Khanh_Load(object sender, EventArgs e)
        {
            LoadDanhSachKhoa();
            HienThiDanhSachLop(); 
        }
        private void LoadDanhSachKhoa()
        {
            try
            {
                cb_Khoa_Khanh.DataSource = TruyVan.LayDuLieuKhoa();
                cb_Khoa_Khanh.DisplayMember = "TenKhoa";
                cb_Khoa_Khanh.ValueMember = "MaKhoa";
                cb_Khoa_Khanh.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách khoa: " + ex.Message, "Lỗi");
            }
        }
        private void btn_TimKiem_Khanh_Click(object sender, EventArgs e)
        {
            string tuKhoa = tb_TuKhoa_Khanh.Text.Trim();
            string maKhoa = cb_Khoa_Khanh.SelectedValue?.ToString();

            if (string.IsNullOrEmpty(tuKhoa) && string.IsNullOrEmpty(maKhoa))
            {
                MessageBox.Show("Không tìm thấy lớp học phù hợp với từ khóa và khoa đã chọn.", "Thông báo");
                return;
            }

            try
            {
                DataTable dt = TruyVan.TimKiemLop(tuKhoa, maKhoa);
                dgv_TimKiem_Khanh.DataSource = dt;

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy lớp học phù hợp với từ khóa và khoa đã chọn.", "Thông báo");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm lớp học: " + ex.Message, "Lỗi");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
