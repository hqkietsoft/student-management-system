using System;
using System.Windows.Forms;
using Nhom2_QuanLySinhVien.Services; // Gọi Service

namespace Nhom2_QuanLySinhVien
{
    public partial class frm_Sua_Khanh : Form
    {
        // Các thuộc tính nhận dữ liệu từ Form cha (Giữ nguyên)
        public string MaLop { get; set; }
        public string TenLop { get; set; }
        public string KhoaHoc { get; set; }
        public string HeDaoTao { get; set; }
        public string NamNhapHoc { get; set; }
        public string MaKhoa { get; set; }

        public frm_Sua_Khanh()
        {
            InitializeComponent();
        }

        private void frm_Sua_Khanh_Load(object sender, EventArgs e)
        {
            // 1. Load danh sách Khoa từ Service
            LopHocService.Instance.HienThiKhoaLenComboBox(cb_MaKhoa_Khanh);

            // 2. Hiển thị dữ liệu cũ lên TextBox
            tb_MaLop_Khanh.Text = MaLop;
            tb_TenLop_Khanh.Text = TenLop;
            tb_KhoaHoc_Khanh.Text = KhoaHoc;
            tb_HeDaoTao_Khanh.Text = HeDaoTao;
            tb_NamNhapHoc_Khanh.Text = NamNhapHoc;
            cb_MaKhoa_Khanh.Text = MaKhoa;

            // Khóa mã lớp (không cho sửa khóa chính)
            tb_MaLop_Khanh.Enabled = false;
        }

        private void btn_Sua_Khanh_Click(object sender, EventArgs e)
        {
            // Kiểm tra dữ liệu (Validation)
            if (string.IsNullOrEmpty(tb_TenLop_Khanh.Text) || string.IsNullOrEmpty(cb_MaKhoa_Khanh.Text))
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin.", "Thông báo");
                return;
            }

            // Gọi Service để Sửa
            LopHocService.Instance.Sua(
                tb_MaLop_Khanh.Text,
                tb_TenLop_Khanh.Text,
                tb_KhoaHoc_Khanh.Text,
                tb_HeDaoTao_Khanh.Text,
                tb_NamNhapHoc_Khanh.Text,
                cb_MaKhoa_Khanh.Text
            );

            this.Close(); // Đóng form sau khi sửa
        }

        private void btn_Huy_Khanh_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}