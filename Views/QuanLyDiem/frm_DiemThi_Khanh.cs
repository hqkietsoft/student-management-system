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
    public partial class frm_DiemThi_Khanh : Form
    {
        public frm_DiemThi_Khanh()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            cb_Mon_Khanh.DataSource = TruyVan.LayDuLieuMonHoc(); 
            cb_Mon_Khanh.DisplayMember = "TenMH";  
            cb_Mon_Khanh.ValueMember = "MaMH";
            
            cb_Mon_Khanh.SelectedIndexChanged += cb_Mon_Khanh_SelectedIndexChanged;

        }

        private void cb_Mon_Khanh_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_Mon_Khanh.SelectedIndex != -1)
            {
                string maMH = cb_Mon_Khanh.SelectedValue.ToString();
                DataTable dtSinhVien = TruyVan.LayThongTinSinhVienTheoMonHoc(maMH);
                if (dtSinhVien.Rows.Count >= 0)
                {
                    dgv_DiemThi_Khanh.DataSource = dtSinhVien;

                    if (dgv_DiemThi_Khanh.Columns["Xoa"] != null)
                    {
                        dgv_DiemThi_Khanh.Columns.Remove("Xoa");
                    }

                    DataGridViewButtonColumn btnDelete = new DataGridViewButtonColumn();
                    btnDelete.Name = "Xoa";
                    btnDelete.HeaderText = "Xóa";
                    btnDelete.Text = "Xóa";
                    btnDelete.UseColumnTextForButtonValue = true;
                    dgv_DiemThi_Khanh.Columns.Add(btnDelete);

                    btnDelete.DisplayIndex = dgv_DiemThi_Khanh.Columns.Count - 1;
                }
                else
                {
                    MessageBox.Show("Không có dữ liệu sinh viên cho môn học này.");
                    dgv_DiemThi_Khanh.DataSource = null;
                }
            }
            else
            {
                dgv_DiemThi_Khanh.DataSource = null;
            }
        }

        private void dgv_DiemThi_Khanh_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            string maSV = dgv_DiemThi_Khanh.Rows[e.RowIndex].Cells["MaSV"].Value?.ToString();
            string maMonHoc = dgv_DiemThi_Khanh.Rows[e.RowIndex].Cells["MaMH"].Value?.ToString();
            if (string.IsNullOrEmpty(maSV) || string.IsNullOrEmpty(maMonHoc))
                return;

            string diemQuaTrinhStr = dgv_DiemThi_Khanh.Rows[e.RowIndex].Cells["DiemQuaTrinh"].Value?.ToString();
            string diemThiStr = dgv_DiemThi_Khanh.Rows[e.RowIndex].Cells["DiemThi"].Value?.ToString();

            if (string.IsNullOrEmpty(diemQuaTrinhStr) || string.IsNullOrEmpty(diemThiStr))
                return;

            if (float.TryParse(diemQuaTrinhStr, out float diemQuaTrinh) && float.TryParse(diemThiStr, out float diemThi))
            {
                if (diemQuaTrinh >= 0 && diemQuaTrinh <= 10 && diemThi >= 0 && diemThi <= 10)
                {
                    float diemHocPhan = (diemQuaTrinh * 0.4f) + (diemThi * 0.6f);
                    dgv_DiemThi_Khanh.Rows[e.RowIndex].Cells["DiemHocPhan"].Value = diemHocPhan.ToString("0.00");

                    TruyVan.CapNhatDiem(maSV, maMonHoc, diemQuaTrinh, diemThi, diemHocPhan);
                }
            }
        }

        private void btn_LuuTatCa_Khanh_Click(object sender, EventArgs e)
        {
            bool hasInvalidData = false;

            for (int i = 0; i < dgv_DiemThi_Khanh.Rows.Count; i++)
            {
                string maSV = dgv_DiemThi_Khanh.Rows[i].Cells["MaSV"].Value?.ToString();
                string maMonHoc = dgv_DiemThi_Khanh.Rows[i].Cells["MaMH"].Value?.ToString();
                if (string.IsNullOrEmpty(maSV) || string.IsNullOrEmpty(maMonHoc))
                    continue;

                string diemQuaTrinhStr = dgv_DiemThi_Khanh.Rows[i].Cells["DiemQuaTrinh"].Value?.ToString();
                string diemThiStr = dgv_DiemThi_Khanh.Rows[i].Cells["DiemThi"].Value?.ToString();

                if (float.TryParse(diemQuaTrinhStr, out float diemQuaTrinh) && float.TryParse(diemThiStr, out float diemThi))
                {
                    if (diemQuaTrinh >= 0 && diemQuaTrinh <= 10 && diemThi >= 0 && diemThi <= 10)
                    {
                        float diemHocPhan = (diemQuaTrinh * 0.4f) + (diemThi * 0.6f);
                        TruyVan.CapNhatDiem(maSV, maMonHoc, diemQuaTrinh, diemThi, diemHocPhan);
                    }
                    else
                    {
                        hasInvalidData = true;
                    }
                }
            }

            if (hasInvalidData)
            {
                MessageBox.Show("Một số điểm không hợp lệ đã bị bỏ qua. Vui lòng kiểm tra lại.");
            }
            else
            {
                MessageBox.Show("Cập nhật điểm thành công!");
            }
        }

        private void dgv_DiemThi_Khanh_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (dgv_DiemThi_Khanh.Columns[e.ColumnIndex].Name == "DiemQuaTrinh" || dgv_DiemThi_Khanh.Columns[e.ColumnIndex].Name == "DiemThi")
            {
                if (!string.IsNullOrEmpty(e.FormattedValue.ToString()))
                {
                    if (!float.TryParse(e.FormattedValue.ToString(), out float diem) || diem < 0 || diem > 10)
                    {
                        MessageBox.Show("Điểm phải nằm trong khoảng từ 0 đến 10.");
                        e.Cancel = true;
                    }
                }
            }
        }

        private void dgv_DiemThi_Khanh_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgv_DiemThi_Khanh.Columns["Xoa"].Index && e.RowIndex >= 0)
            {
                var result = MessageBox.Show("Bạn có chắc chắn muốn xóa điểm thi này không?", "Xác nhận", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    string maSV = dgv_DiemThi_Khanh.Rows[e.RowIndex].Cells["MaSV"].Value?.ToString();
                    string maMonHoc = dgv_DiemThi_Khanh.Rows[e.RowIndex].Cells["MaMH"].Value?.ToString();

                    try
                    {
                        if (TruyVan.XoaDiem(maSV, maMonHoc))
                        {
                            dgv_DiemThi_Khanh.Rows[e.RowIndex].Cells["DiemThi"].Value = DBNull.Value;
                            dgv_DiemThi_Khanh.Rows[e.RowIndex].Cells["DiemHocPhan"].Value = DBNull.Value;
                            MessageBox.Show("Điểm thi đã được xóa thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Đã xảy ra lỗi khi xóa điểm thi.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void frm_DiemThi_Khanh_Load(object sender, EventArgs e)
        {
            cb_Mon_Khanh.SelectedIndex = 1;
        }
    }
}
