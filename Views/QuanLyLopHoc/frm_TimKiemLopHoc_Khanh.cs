using System;
using System.Windows.Forms;
using Nhom2_QuanLySinhVien.Services;

namespace Nhom2_QuanLySinhVien
{
    public partial class frm_TimKiemLopHoc_Khanh : Form
    {
        // 1. Xóa constructor cũ có tham số DataTable
        public frm_TimKiemLopHoc_Khanh()
        {
            InitializeComponent();
        }

        private void frm_TimKiem_Khanh_Load(object sender, EventArgs e)
        {
            // Load danh sách Khoa để chọn tìm kiếm
            LopHocService.Instance.HienThiKhoaLenComboBox(cb_Khoa_Khanh);

            // Load toàn bộ danh sách ban đầu
            dgv_TimKiem_Khanh.DataSource = LopHocService.Instance.LayTatCaLopHoc();
        }

        private void btn_TimKiem_Khanh_Click(object sender, EventArgs e)
        {
            string tuKhoa = tb_TuKhoa_Khanh.Text.Trim();
            string maKhoa = cb_Khoa_Khanh.Text;

            // Gọi Service tìm kiếm
            var ketQua = LopHocService.Instance.TimKiemLop(tuKhoa, maKhoa);

            dgv_TimKiem_Khanh.DataSource = ketQua;

            // Kiểm tra kết quả (List thì dùng Count)
            // Ép kiểu về List dynamic hoặc kiểm tra null
            if (dgv_TimKiem_Khanh.RowCount == 0)
            {
                MessageBox.Show("Không tìm thấy lớp học phù hợp.", "Thông báo");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}