using System;
using System.Windows.Forms;
using Nhom2_QuanLySinhVien.Services; // Gọi Service

namespace Nhom2_QuanLySinhVien
{
    public partial class SuaLopHocPhan : UserControl
    {
        private string originalMaLop; // Lưu mã gốc để so sánh khi sửa

        public SuaLopHocPhan()
        {
            InitializeComponent();
        }

        // Constructor nhận dữ liệu từ Form cha truyền sang
        public SuaLopHocPhan(string maLop, string maMH, string maGV, string hocKy, string nam)
        {
            InitializeComponent();

            originalMaLop = maLop;

            // 1. Load dữ liệu ComboBox trước
            LoadComboBoxData();

            // 2. Gán dữ liệu cũ lên giao diện
            txtMaLHP.Text = maLop;
            cbbHocKy.Text = hocKy;

            // Xử lý giá trị Năm (Decimal -> Int)
            if (decimal.TryParse(nam, out decimal namVal))
            {
                nbrNam.Value = namVal;
            }

            // 3. Gán giá trị chọn cho ComboBox
            // WinForms sẽ tự động chọn item có ValueMember khớp với giá trị này
            cbbMaMon.SelectedValue = maMH;
            cbbMaGV.SelectedValue = maGV;
        }

        private void LoadComboBoxData()
        {
            try
            {
                // Load Môn học từ Service
                cbbMaMon.DataSource = LopHocPhanService.Instance.LayDanhSachMonHoc();
                cbbMaMon.DisplayMember = "Display"; // Tên hiển thị (VD: MH01 - Toán)
                cbbMaMon.ValueMember = "Value";     // Giá trị ngầm (VD: MH01)

                // Load Giáo viên từ Service
                cbbMaGV.DataSource = LopHocPhanService.Instance.LayDanhSachGiaoVien();
                cbbMaGV.DisplayMember = "Display";
                cbbMaGV.ValueMember = "Value";

                // Load Học kỳ (Hardcode như cũ)
                cbbHocKy.Items.Clear();
                cbbHocKy.Items.AddRange(new object[] { "1", "2", "3" });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message);
            }
        }

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            // Kiểm tra nhập liệu (Validation)
            if (string.IsNullOrWhiteSpace(txtMaLHP.Text) ||
                cbbMaMon.SelectedValue == null ||
                cbbMaGV.SelectedValue == null ||
                string.IsNullOrWhiteSpace(cbbHocKy.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Gọi Service để thực hiện Sửa
            // Hàm Sua trong Service sẽ tự lo logic: 
            // - Nếu đổi mã -> Kiểm tra trùng, Xóa cũ, Thêm mới.
            // - Nếu không đổi mã -> Update bình thường.
            bool ketQua = LopHocPhanService.Instance.Sua(
                originalMaLop,                  // Mã cũ (để tìm bản ghi gốc)
                txtMaLHP.Text.Trim(),           // Mã mới (người dùng nhập)
                cbbMaMon.SelectedValue.ToString(),
                cbbMaGV.SelectedValue.ToString(),
                int.Parse(cbbHocKy.Text.Trim()),
                nbrNam.Value
            );

            if (ketQua)
            {
                MessageBox.Show("Cập nhật lớp học phần thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                BackToHome(); // Quay về
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            BackToHome();
        }

        // Hàm quay về màn hình chính (Giữ nguyên logic điều hướng của bạn)
        private void BackToHome()
        {
            if (this.Parent != null)
            {
                // Refresh lại dữ liệu ở form cha (LopHocPhan)
                // Cách đơn giản nhất theo logic cũ:
                TrangChu trangChu = new TrangChu();
                trangChu.Show();
                trangChu.LoadLopHocPhanUserControl();

                // Ẩn form hiện tại đi
                this.FindForm()?.Hide();
            }
        }
    }
}