using System;
using System.Windows.Forms;
using Nhom2_QuanLySinhVien.Services; // Gọi Service

namespace Nhom2_QuanLySinhVien
{
    public partial class ThemLopHocPhan : UserControl
    {
        public ThemLopHocPhan()
        {
            InitializeComponent();
            LoadComboBoxData();
        }

        private void LoadComboBoxData()
        {
            try
            {
                // Load Môn học từ Service
                cbbMaMon.DataSource = LopHocPhanService.Instance.LayDanhSachMonHoc();
                cbbMaMon.DisplayMember = "Display";
                cbbMaMon.ValueMember = "Value";
                cbbMaMon.SelectedIndex = -1; // Chưa chọn gì

                // Load Giáo viên từ Service
                cbbMaGV.DataSource = LopHocPhanService.Instance.LayDanhSachGiaoVien();
                cbbMaGV.DisplayMember = "Display";
                cbbMaGV.ValueMember = "Value";
                cbbMaGV.SelectedIndex = -1;

                // Load Học kỳ (Hardcode)
                cbbHocKy.Items.Clear();
                cbbHocKy.Items.AddRange(new object[] { "1", "2", "3" });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải dữ liệu: {ex.Message}", "Lỗi");
            }
        }

        private void ThemLopHocPhan_Load(object sender, EventArgs e)
        {
        }

        private void btnXacNhan_Click_1(object sender, EventArgs e)
        {
            // Validation
            if (string.IsNullOrWhiteSpace(txtMaLHP.Text) ||
                cbbMaMon.SelectedValue == null ||
                cbbMaGV.SelectedValue == null ||
                string.IsNullOrWhiteSpace(cbbHocKy.Text) ||
                string.IsNullOrWhiteSpace(nbrNam.Value.ToString()))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Gọi Service để Thêm
            // Logic kiểm tra trùng mã đã nằm trong Service rồi
            bool ketQua = LopHocPhanService.Instance.Them(
                txtMaLHP.Text.Trim(),
                cbbMaMon.SelectedValue.ToString(),
                cbbMaGV.SelectedValue.ToString(),
                int.Parse(cbbHocKy.Text.Trim()),
                nbrNam.Value
            );

            if (ketQua)
            {
                MessageBox.Show("Thêm lớp học phần thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Reset form để nhập tiếp
                txtMaLHP.Clear();
                cbbMaMon.SelectedIndex = -1;
                cbbMaGV.SelectedIndex = -1;
                cbbHocKy.SelectedIndex = -1;
                nbrNam.Value = nbrNam.Minimum;

                // Quay về (Tùy chọn: Nếu muốn thêm xong quay về luôn thì bỏ comment dòng dưới)
                // BackToHome();
            }
        }

        private void btnHuy_Click_1(object sender, EventArgs e)
        {
            BackToHome();
        }

        private void BackToHome()
        {
            if (this.Parent != null)
            {
                TrangChu trangChu = new TrangChu();
                trangChu.Show();
                trangChu.LoadLopHocPhanUserControl();
                
                this.FindForm()?.Hide();
            }
        }
    }
}