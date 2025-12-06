using System;
using System.Linq;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using Nhom2_QuanLySinhVien.Services;

namespace Nhom2_QuanLySinhVien
{
    public partial class LopHocPhan : UserControl
    {
        public LopHocPhan()
        {
            InitializeComponent();
        }

        private void LopHocPhan_Load_1(object sender, EventArgs e)
        {
            LoadData();
            PhanQuyen();
        }

        // --- HÀM LOAD DỮ LIỆU (Dùng EF Core) ---
        private void LoadData()
        {
            // Tắt tự sinh cột để dùng cột bạn đã thiết kế sẵn
            guna2DataGridView1.AutoGenerateColumns = false;

            // Lấy dữ liệu từ Service gán vào bảng
            guna2DataGridView1.DataSource = LopHocPhanService.Instance.LayTatCaLopHocPhan();

            MaLopCount();
        }

        // --- PHÂN QUYỀN ---
        public void PhanQuyen()
        {
            if (Program.loaiND == 3 || Program.loaiND == 4)
            {
                btnXoa.Enabled = false;
                guna2Button4.Enabled = false; // Nút Sửa
                btnThem.Enabled = false;
            }
        }

        // --- ĐẾM SỐ LƯỢNG ---
        private void MaLopCount()
        {
            // Đếm trực tiếp từ số dòng của bảng (Nhanh gọn)
            label3.Text = $"{guna2DataGridView1.RowCount}";
        }

        // --- TÌM KIẾM ---
        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {
            string keyword = guna2TextBox1.Text.Trim();
            if (string.IsNullOrEmpty(keyword))
            {
                LoadData();
            }
            else
            {
                // Gọi hàm tìm kiếm từ Service
                guna2DataGridView1.DataSource = LopHocPhanService.Instance.TimKiemLopHocPhan(keyword);
                MaLopCount();
            }
        }

        // --- NÚT XÓA ---
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (guna2DataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một dòng để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Lấy Mã lớp từ dòng đang chọn
            string maLop = guna2DataGridView1.SelectedRows[0].Cells["MaLop"].Value.ToString();

            if (MessageBox.Show($"Bạn có chắc chắn muốn xóa lớp học phần: {maLop}?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                // Gọi Service xóa
                LopHocPhanService.Instance.Xoa(maLop);
                LoadData(); // Load lại bảng
            }
        }

        // --- NÚT SỬA ---
        private void guna2Button4_Click(object sender, EventArgs e)
        {
            if (guna2DataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một dòng để sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                DataGridViewRow row = guna2DataGridView1.SelectedRows[0];

                // Mở UserControl Sửa (Giữ nguyên logic cũ của bạn)
                SuaLopHocPhan sua = new SuaLopHocPhan(
                    row.Cells["MaLop"].Value.ToString(),
                    row.Cells["MaMH"].Value.ToString(),
                    row.Cells["MaGV"].Value.ToString(),
                    row.Cells["HocKy"].Value.ToString(),
                    row.Cells["Nam"].Value.ToString()
                );
                addUserControl(sua);
            }
        }

        // --- NÚT THÊM ---
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            ThemLopHocPhan them = new ThemLopHocPhan();
            addUserControl(them);
        }

        // Hàm hỗ trợ chuyển UserControl (Giữ nguyên)
        private void addUserControl(UserControl function)
        {
            function.Dock = DockStyle.Fill;
            panel4.Controls.Clear();
            panel4.Controls.Add(function);
            function.BringToFront();
        }

        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {
            if (this.Parent != null)
            {
                this.Parent.Controls.Remove(this);
                this.Dispose();
            }
        }

        // Xử lý Double Click để sửa (Giữ nguyên logic cũ)
        private void guna2DataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Gọi lại hàm nút sửa cho gọn code
                guna2Button4_Click(sender, e);
            }
        }

        // Xử lý Resize (Giữ nguyên code giao diện của bạn)
        private void panel3_Resize(object sender, EventArgs e)
        {
            int buttonCount = panel3.Controls.OfType<Button>().Count();
            if (buttonCount == 0) return;

            int buttonWidth = 120;
            int buttonHeight = 45;
            int space = 150;

            int totalWidth = buttonCount * buttonWidth + (buttonCount - 1) * space;
            int startX = (panel3.Width - totalWidth) / 2;
            int y = (panel3.Height - buttonHeight) / 2;

            int x = startX;

            foreach (Button btn in panel3.Controls.OfType<Button>())
            {
                btn.SetBounds(x, y, buttonWidth, buttonHeight);
                x += buttonWidth + space;
            }
        }

        // Các event thừa
        private void guna2Button1_CheckedChanged(object sender, EventArgs e) { }
        private void panel4_Paint(object sender, PaintEventArgs e) { }
        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void LopHocPhan_Load(object sender, EventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }
        private void guna2Panel1_Paint(object sender, PaintEventArgs e) { }
        private void guna2Panel1_Paint_1(object sender, PaintEventArgs e) { }
    }
}