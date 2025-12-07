using System;
using System.Windows.Forms;
using Nhom2_QuanLySinhVien.Services; // Gọi thư mục Service

namespace Nhom2_QuanLySinhVien
{
    public partial class frm_QuanLyLopHoc_Khanh : Form
    {
        public frm_QuanLyLopHoc_Khanh()
        {
            InitializeComponent();
            PhanQuyen();
        }

        // 1. Phân quyền (Giữ nguyên logic cũ)
        public void PhanQuyen()
        {
            // Nếu là Sinh viên (3, 4) thì khóa các nút chức năng
            if (Program.loaiND == 3 || Program.loaiND == 4)
            {
                btn_Them.Enabled = false;
                btn_Sua.Enabled = false;
                btn_Xoa.Enabled = false;
            }
        }

        // 2. Form Load
        private void frm_QuanLyLopHoc_Khanh_Load(object sender, EventArgs e)
        {
            HienThi();
        }

        // 3. Hàm Hiển thị (Dùng EF Core thay vì SQL)
        public void HienThi()
        {
            // Lấy dữ liệu từ Service và đổ vào Grid
            dgv_QuanLyLopHoc_Khanh.DataSource = LopHocService.Instance.LayTatCaLopHoc();
        }

        #region NÚT THÊM
        private void btn_Them_Click(object sender, EventArgs e)
        {
            frm_ThemLopHoc_Khanh them = new frm_ThemLopHoc_Khanh();
            this.Hide();
            them.ShowDialog();
            HienThi(); // Load lại dữ liệu sau khi thêm xong
            this.Show();
        }
        #endregion

        #region NÚT SỬA
        private void btn_Sua_Click(object sender, EventArgs e)
        {
            if (dgv_QuanLyLopHoc_Khanh.CurrentRow != null)
            {
                // Lấy dữ liệu từ dòng đang chọn
                // Lưu ý: Đảm bảo thứ tự cột hoặc dùng tên cột ["TenCot"] để chính xác hơn
                string MaLop = dgv_QuanLyLopHoc_Khanh.CurrentRow.Cells[0].Value.ToString();
                string TenLop = dgv_QuanLyLopHoc_Khanh.CurrentRow.Cells[1].Value.ToString();
                string KhoaHoc = dgv_QuanLyLopHoc_Khanh.CurrentRow.Cells[2].Value.ToString();
                string HeDaoTao = dgv_QuanLyLopHoc_Khanh.CurrentRow.Cells[3].Value.ToString();
                string NamNhapHoc = dgv_QuanLyLopHoc_Khanh.CurrentRow.Cells[4].Value.ToString();
                string MaKhoa = dgv_QuanLyLopHoc_Khanh.CurrentRow.Cells[5].Value.ToString();

                // Cập nhật vào Bridge (TempData) để Form Sửa có thể dùng (Nếu bạn refactor Form Sửa)
                LopHocService.TempData.MaLop = MaLop;
                LopHocService.TempData.TenLop = TenLop;

                // Mở Form sửa (Giữ logic truyền tham số cũ để form con không bị lỗi)
                frm_Sua_Khanh sua = new frm_Sua_Khanh
                {
                    MaLop = MaLop,
                    TenLop = TenLop,
                    KhoaHoc = KhoaHoc,
                    HeDaoTao = HeDaoTao,
                    NamNhapHoc = NamNhapHoc,
                    MaKhoa = MaKhoa
                };

                this.Hide();
                sua.ShowDialog();
                HienThi(); // Load lại sau khi sửa
                this.Show();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một dòng trước khi sửa.", "Thông báo");
            }
        }
        #endregion

        #region NÚT XOÁ
        private void btn_Xoa_Click(object sender, EventArgs e)
        {
            // Logic cũ: Mở form xóa riêng
            frm_XoaLopHoc_Khanh xoa = new frm_XoaLopHoc_Khanh();
            this.Hide();
            xoa.ShowDialog();
            HienThi();
            this.Show();
        }
        #endregion

        #region NÚT TÌM KIẾM
        private void btn_TimKiem_Click(object sender, EventArgs e)
        {
            // Logic cũ: Truyền DataTable vào constructor
            // Logic mới: Vì Service trả về List, ta không truyền DataTable nữa.
            // Bạn cần vào frm_TimKiemLopHoc_Khanh xóa tham số ở Constructor đi.

            // frm_TimKiemLopHoc_Khanh timkiem = new frm_TimKiemLopHoc_Khanh(); 
            // this.Hide();
            // timkiem.ShowDialog();
            // this.Show();

            MessageBox.Show("Chức năng tìm kiếm đang được cập nhật sang EF Core. Vui lòng sửa form Tìm kiếm sau.");
        }
        #endregion

        #region NÚT SẮP XẾP
        private void btn_SapXep_Khanh_Click(object sender, EventArgs e)
        {
            // Gọi hàm sắp xếp từ Service
            dgv_QuanLyLopHoc_Khanh.DataSource = LopHocService.Instance.SapXepLopHocTheoTen();
            MessageBox.Show("Đã sắp xếp danh sách lớp học theo tên lớp", "Sắp xếp", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion

        #region NÚT THOÁT
        private void btn_Thoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        // Các sự kiện click thừa (để trống)
        private void label1_Click(object sender, EventArgs e) { }
        private void dgv_QuanLyLopHoc_Khanh_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
    }
}