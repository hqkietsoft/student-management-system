using Nhom2_QuanLySinhVien.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLySinhVien_Nhom2.QuanLyDiem
{
	public partial class frm_TinhDiemTBHK_Bac : Form
	{
		private readonly DiemService diem;
		string ma;
		public frm_TinhDiemTBHK_Bac()
		{
			InitializeComponent();
			diem = DiemService.DIEM;
			dgv_DiemTBHK.DataSource = diem.LayDuLieuDiem_Monhoc_SinhVien();
			cbo_ThongTinSV.DropDownHeight = 300;
			cbo_ThongTinSV.DropDownWidth = 30;
			

		}

		private void cbo_ThongTinSV_SelectedIndexChanged(object sender, EventArgs e)
		{
			//dgv_DiemTBHK.DataSource = diem.TinhDiemTBHK(cbo_ThongTinSV.Text.Substring(0,5));
			dgv_DiemTBHK.DataSource = diem.TinhDiemTBHK(cbo_ThongTinSV.Text.Trim(), cbo_hocky_diem.Text.Trim());

		}

        private void btnTinhDiem_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra đầu vào
            if (cbo_hocky_diem.Text == "Chọn học kỳ" || cbo_ThongTinSV.Text == "Chọn Mã SV")
            {
                MessageBox.Show("Vui lòng chọn học kỳ và mã sinh viên", "Thông báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string maSV = cbo_ThongTinSV.Text.Trim();
                string hocKy = cbo_hocky_diem.Text.Trim();

                // 2. Lấy danh sách điểm từ Service
                List<DiemDTO> danhSachDiem = diem.TinhDiemTBHK(maSV, hocKy);

                if (danhSachDiem == null || danhSachDiem.Count == 0)
                {
                    MessageBox.Show("Sinh viên này chưa có điểm môn nào trong học kỳ này!",
                                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // 3. Tính toán điểm trung bình
                double tongDiemTichLuy = danhSachDiem.Sum(item => item.DiemHocPhan * item.SoTC);
                int tongSoTinChi = danhSachDiem.Sum(item => item.SoTC);

                if (tongSoTinChi == 0)
                {
                    MessageBox.Show("Không thể tính điểm: Tổng số tín chỉ bằng 0!",
                                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                double diemTBHK = Math.Round(tongDiemTichLuy / tongSoTinChi, 2);

                // 4. Cập nhật vào Database
                bool updateSuccess = diem.UpdateDiemTBHK(diemTBHK, maSV);

                if (!updateSuccess)
                {
                    MessageBox.Show("Cập nhật điểm thất bại!", "Lỗi",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // 5. Hiển thị lại lên lưới
                dgv_DiemTBHK.DataSource = null; // Clear first for proper refresh
                dgv_DiemTBHK.DataSource = danhSachDiem;

                // Hiển thị kết quả
                MessageBox.Show($"Đã tính xong! Điểm TBHK của sinh viên {maSV} là: {diemTBHK:F2}",
                                "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tính điểm: {ex.Message}\n\nChi tiết: {ex.StackTrace}",
                                "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXetHocBong_Click(object sender, EventArgs e)
		{
			if(cbo_hocky.Text == "Chọn học kỳ")
			{
				MessageBox.Show("Vui lòng chọn học kỳ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
			else
			{
				dgv_DiemTBHK.DataSource = diem.XetHocBong(cbo_hocky.Text);
			}
		}

		private void txt_TTTimKiem_TextChanged(object sender, EventArgs e)
		{
			dgv_DiemTBHK.DataSource = diem.TimKiemDiemMonHoc(txt_TTTimKiem.Text);
		}

		private void cbo_hocky_diem_SelectedIndexChanged(object sender, EventArgs e)
		{
            // 1. Khai báo List<string> thay vì DataTable
            List<string> dsMaSV = (List<string>)diem.HienThiDiemLenCBO(cbo_hocky_diem.Text.Trim());

            // 2. Xóa dữ liệu cũ
            cbo_ThongTinSV.Items.Clear();
            cbo_ThongTinSV.Text = "Chọn Mã SV";

            // 3. Duyệt List để thêm vào ComboBox (Code gọn hơn cũ nhiều)
            foreach (string maSV in dsMaSV)
            {
                cbo_ThongTinSV.Items.Add(maSV);
            }
        }

		private void groupBox1_Enter(object sender, EventArgs e)
		{

		}

		private void frm_TinhDiemTBHK_Bac_Load(object sender, EventArgs e)
		{
			cbo_hocky.Text = "Chọn học kỳ";
			cbo_hocky_diem.Text = "Chọn học kỳ";
		}

		private void guna2Button1_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}
