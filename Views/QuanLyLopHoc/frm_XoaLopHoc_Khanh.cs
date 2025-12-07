using System;
using System.Windows.Forms;
using Nhom2_QuanLySinhVien.Services;

namespace Nhom2_QuanLySinhVien
{
    public partial class frm_XoaLopHoc_Khanh : Form
    {
        public frm_XoaLopHoc_Khanh()
        {
            InitializeComponent();
        }

        private void frm_Xoa_Khanh_Load(object sender, EventArgs e)
        {
            LoadMaLop();
        }

        private void LoadMaLop()
        {
            // Load danh sách Mã lớp vào ComboBox
            // Vì hàm LayTatCaLopHoc trả về List Object, ta gán trực tiếp
            // và chọn ValueMember là tên thuộc tính "Mã_Lớp" (như trong Service định nghĩa)
            cb_MaLop_Khanh.DataSource = LopHocService.Instance.LayTatCaLopHoc();
            cb_MaLop_Khanh.DisplayMember = "Mã_Lớp";
            cb_MaLop_Khanh.ValueMember = "Mã_Lớp";
        }

        // Nút Tìm (Để hiện thông tin trước khi xóa)
        private void btn_Tim_Khanh_Click(object sender, EventArgs e)
        {
            string maLop = cb_MaLop_Khanh.Text;

            // Gọi hàm hỗ trợ lấy thông tin (Cần cập nhật Service hoặc viết lại logic ở đây)
            // Để nhanh gọn, tôi gọi hàm lấy thông tin của Service và tận dụng biến TempData
            LopHocService.Instance.LayThongTinLopVaoTextBox(maLop, tb_TenLop_Khanh, tb_KhoaHoc_Khanh);

            // Lấy tiếp các trường còn lại từ TempData (Cầu nối)
            if (LopHocService.TempData != null && LopHocService.TempData.MaLop == maLop)
            {
                tb_HeDaoTao_Khanh.Text = LopHocService.TempData.HeDaoTao;
                tb_NamNhapHoc_Khanh.Text = LopHocService.TempData.NamNhapHoc;
            }
            else
            {
                MessageBox.Show("Không tìm thấy thông tin lớp.");
                ClearTextBoxes();
            }
        }

        private void btn_Xoa_Khanh_Click(object sender, EventArgs e)
        {
            string maLop = cb_MaLop_Khanh.Text;
            if (string.IsNullOrEmpty(maLop)) return;

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                // Gọi Service Xóa
                LopHocService.Instance.Xoa(maLop);

                // Refresh lại
                LoadMaLop();
                ClearTextBoxes();
            }
        }

        private void ClearTextBoxes()
        {
            tb_TenLop_Khanh.Clear();
            tb_KhoaHoc_Khanh.Clear();
            tb_HeDaoTao_Khanh.Clear();
            tb_NamNhapHoc_Khanh.Clear();
        }

        private void btn_Huy_Khanh_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}