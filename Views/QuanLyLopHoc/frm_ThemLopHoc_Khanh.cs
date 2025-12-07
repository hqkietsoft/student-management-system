using System;
using System.Windows.Forms;
using Nhom2_QuanLySinhVien.Services;

namespace Nhom2_QuanLySinhVien
{
    public partial class frm_ThemLopHoc_Khanh : Form
    {
        public frm_ThemLopHoc_Khanh()
        {
            InitializeComponent();
        }

        private void frm_Them_Khanh_Load(object sender, EventArgs e)
        {
            // Load danh sách Khoa
            LopHocService.Instance.HienThiKhoaLenComboBox(cb_MaKhoa_Khanh);
        }

        private void btn_Them_Khanh_Click(object sender, EventArgs e)
        {
            // Kiểm tra nhập liệu cơ bản
            if (string.IsNullOrEmpty(tb_MaLop_Khanh.Text))
            {
                MessageBox.Show("Vui lòng nhập mã lớp.", "Thông báo");
                tb_MaLop_Khanh.Focus();
                return;
            }
            if (string.IsNullOrEmpty(cb_MaKhoa_Khanh.Text))
            {
                MessageBox.Show("Vui lòng chọn khoa.", "Thông báo");
                return;
            }

            // Gọi Service để Thêm
            LopHocService.Instance.Them(
                tb_MaLop_Khanh.Text.Trim(),
                tb_TenLop_Khanh.Text.Trim(),
                tb_KhoaHoc_Khanh.Text.Trim(),
                tb_HeDaoTao_Khanh.Text.Trim(),
                tb_NamNhapHoc_Khanh.Text.Trim(),
                cb_MaKhoa_Khanh.Text.Trim()
            );

            // Không cần Close ngay để người dùng có thể thêm tiếp, 
            // hoặc Close tùy ý bạn. Ở đây tôi giữ form để thêm tiếp.
            // this.Close(); 
        }

        private void btn_Huy_Khanh_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}