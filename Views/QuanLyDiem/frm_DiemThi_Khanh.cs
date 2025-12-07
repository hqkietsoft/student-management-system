using System;
using System.Data;
using System.Windows.Forms;
using Nhom2_QuanLySinhVien.Services; // Gọi Service

namespace Nhom2_QuanLySinhVien
{
    public partial class frm_DiemThi_Khanh : Form
    {
        public frm_DiemThi_Khanh()
        {
            InitializeComponent();
            LoadData(); // Gọi hàm load dữ liệu ban đầu
        }

        private void frm_DiemThi_Khanh_Load(object sender, EventArgs e)
        {
            // Có thể bỏ trống nếu đã gọi ở Constructor
        }

        // --- 1. LOAD DANH SÁCH MÔN HỌC ---
        private void LoadData()
        {
            try
            {
                // Gọi Service lấy danh sách môn học
                var listMon = DiemService.DIEM.LayDuLieuMonHoc();

                cb_Mon_Khanh.DataSource = listMon;
                cb_Mon_Khanh.DisplayMember = "TenMH";
                cb_Mon_Khanh.ValueMember = "MaMH";

                // Gán sự kiện sau khi load để tránh lỗi trigger sớm
                cb_Mon_Khanh.SelectedIndexChanged -= cb_Mon_Khanh_SelectedIndexChanged;
                cb_Mon_Khanh.SelectedIndexChanged += cb_Mon_Khanh_SelectedIndexChanged;

                // Chọn môn đầu tiên (nếu có)
                if (cb_Mon_Khanh.Items.Count > 0)
                {
                    cb_Mon_Khanh.SelectedIndex = 0;
                    LoadDiemSinhVien(); // Load bảng điểm luôn
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải môn học: " + ex.Message);
            }
        }

        // --- 2. LOAD BẢNG ĐIỂM THEO MÔN ---
        private void LoadDiemSinhVien()
        {
            if (cb_Mon_Khanh.SelectedValue == null) return;

            string maMH = cb_Mon_Khanh.SelectedValue.ToString();

            // Gọi Service lấy dữ liệu -> Trả về List object
            var data = DiemService.DIEM.LayThongTinSinhVienTheoMonHoc(maMH);

            // Gán vào GridView (List object gán datasource bình thường)
            dgv_DiemThi_Khanh.DataSource = data;

            // Thêm cột nút Xóa nếu chưa có
            AddDeleteButtonColumn();
        }

        private void AddDeleteButtonColumn()
        {
            if (dgv_DiemThi_Khanh.Columns["Xoa"] == null)
            {
                DataGridViewButtonColumn btnDelete = new DataGridViewButtonColumn();
                btnDelete.Name = "Xoa";
                btnDelete.HeaderText = "Xóa";
                btnDelete.Text = "Xóa";
                btnDelete.UseColumnTextForButtonValue = true;
                dgv_DiemThi_Khanh.Columns.Add(btnDelete);
            }
            // Đẩy cột xóa xuống cuối cùng
            dgv_DiemThi_Khanh.Columns["Xoa"].DisplayIndex = dgv_DiemThi_Khanh.Columns.Count - 1;
        }

        private void cb_Mon_Khanh_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDiemSinhVien();
        }

        // --- 3. TÍNH ĐIỂM TỰ ĐỘNG (KHI SỬA TRÊN LƯỚI) ---
        private void dgv_DiemThi_Khanh_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // Lấy dữ liệu dòng hiện tại
                string maSV = dgv_DiemThi_Khanh.Rows[e.RowIndex].Cells["MaSV"].Value?.ToString();
                string maMH = cb_Mon_Khanh.SelectedValue.ToString(); // Lấy từ combo box chuẩn hơn

                if (string.IsNullOrEmpty(maSV)) return;

                // Lấy điểm QT và Thi
                string diemQTStr = dgv_DiemThi_Khanh.Rows[e.RowIndex].Cells["DiemQuaTrinh"].Value?.ToString();
                string diemThiStr = dgv_DiemThi_Khanh.Rows[e.RowIndex].Cells["DiemThi"].Value?.ToString();

                if (float.TryParse(diemQTStr, out float diemQT) && float.TryParse(diemThiStr, out float diemThi))
                {
                    if (IsScoreValid(diemQT) && IsScoreValid(diemThi))
                    {
                        // Tính điểm HP
                        float diemHP = (float)Math.Round((diemQT * 0.4f) + (diemThi * 0.6f), 2);

                        // Cập nhật lên lưới để user thấy
                        dgv_DiemThi_Khanh.Rows[e.RowIndex].Cells["DiemHocPhan"].Value = diemHP;

                        // Gọi Service lưu vào DB ngay
                        DiemService.DIEM.CapNhatDiem(maSV, maMH, diemQT, diemThi, diemHP);
                    }
                    else
                    {
                        MessageBox.Show("Điểm phải từ 0-10.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi cập nhật: " + ex.Message);
            }
        }

        // --- 4. NÚT LƯU TẤT CẢ ---
        private void btn_LuuTatCa_Khanh_Click(object sender, EventArgs e)
        {
            bool hasInvalidData = false;
            string maMH = cb_Mon_Khanh.SelectedValue.ToString();

            // Duyệt từng dòng trên lưới
            for (int i = 0; i < dgv_DiemThi_Khanh.Rows.Count; i++)
            {
                string maSV = dgv_DiemThi_Khanh.Rows[i].Cells["MaSV"].Value?.ToString();
                if (string.IsNullOrEmpty(maSV)) continue;

                string diemQTStr = dgv_DiemThi_Khanh.Rows[i].Cells["DiemQuaTrinh"].Value?.ToString();
                string diemThiStr = dgv_DiemThi_Khanh.Rows[i].Cells["DiemThi"].Value?.ToString();

                if (float.TryParse(diemQTStr, out float diemQT) && float.TryParse(diemThiStr, out float diemThi))
                {
                    if (IsScoreValid(diemQT) && IsScoreValid(diemThi))
                    {
                        float diemHP = (float)Math.Round((diemQT * 0.4f) + (diemThi * 0.6f), 2);
                        // Cập nhật từng sinh viên
                        DiemService.DIEM.CapNhatDiem(maSV, maMH, diemQT, diemThi, diemHP);
                    }
                    else hasInvalidData = true;
                }
            }

            if (hasInvalidData) MessageBox.Show("Một số điểm không hợp lệ bị bỏ qua.");
            else MessageBox.Show("Cập nhật tất cả thành công!");

            LoadDiemSinhVien(); // Refresh lại
        }

        // --- 5. NÚT XÓA TRÊN GRID ---
        private void dgv_DiemThi_Khanh_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra nếu click vào cột Xóa (và không phải tiêu đề)
            if (e.RowIndex >= 0 && dgv_DiemThi_Khanh.Columns[e.ColumnIndex].Name == "Xoa")
            {
                var result = MessageBox.Show("Bạn có chắc chắn muốn xóa điểm này?", "Xác nhận", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    string maSV = dgv_DiemThi_Khanh.Rows[e.RowIndex].Cells["MaSV"].Value?.ToString();
                    string maMH = cb_Mon_Khanh.SelectedValue.ToString();

                    if (DiemService.DIEM.XoaDiem(maSV, maMH))
                    {
                        MessageBox.Show("Đã xóa điểm thi!");

                        // Xóa trên lưới (Visual update)
                        dgv_DiemThi_Khanh.Rows[e.RowIndex].Cells["DiemThi"].Value = DBNull.Value;
                        dgv_DiemThi_Khanh.Rows[e.RowIndex].Cells["DiemHocPhan"].Value = DBNull.Value;
                    }
                    else
                    {
                        MessageBox.Show("Lỗi xóa điểm.");
                    }
                }
            }
        }

        // --- VALIDATION ---
        private void dgv_DiemThi_Khanh_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            string colName = dgv_DiemThi_Khanh.Columns[e.ColumnIndex].Name;
            if (colName == "DiemQuaTrinh" || colName == "DiemThi")
            {
                string val = e.FormattedValue.ToString();
                if (!string.IsNullOrEmpty(val))
                {
                    if (!float.TryParse(val, out float diem) || !IsScoreValid(diem))
                    {
                        MessageBox.Show("Điểm phải là số từ 0 đến 10.");
                        e.Cancel = true; // Chặn không cho rời ô
                    }
                }
            }
        }

        private bool IsScoreValid(float score)
        {
            return score >= 0 && score <= 10;
        }
    }
}