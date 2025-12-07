using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using OfficeOpenXml; // Thư viện EPPlus
using Nhom2_QuanLySinhVien.Services;
using QuanLySinhVien_Nhom2; // Gọi Service

namespace Nhom2_QuanLySinhVien
{
    public partial class BaoCaoThongKe : UserControl
    {
        public BaoCaoThongKe()
        {
            InitializeComponent();

            // Cấu hình License cho EPPlus (Bắt buộc)
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            LoadComboBoxData();
        }

        private void BaoCaoThongKe_Load(object sender, EventArgs e)
        {
        }

        private void LoadComboBoxData()
        {
            try
            {
                // 1. Load Lớp học (Ép kiểu rõ ràng về List<string>)
                var rawLop = BaoCaoService.Instance.LayDanhSachLop();
                List<string> listLop = ((IEnumerable<string>)rawLop).ToList();

                cbbLopHoc.DataSource = listLop;

                // Tạo một list mới cho combobox thứ 2 để tránh xung đột binding
                cbbLopHoc1.DataSource = new List<string>(listLop);

                // 2. Load Môn học (Service trả về List object {MaMh, TenMh})
                // Ta dùng BindingSource để an toàn
                var listMon = BaoCaoService.Instance.LayDanhSachMonHoc();
                BindingSource bsMon = new BindingSource();
                bsMon.DataSource = listMon;

                cbbMonHoc.DataSource = bsMon;
                cbbMonHoc.DisplayMember = "MaMh";
                cbbMonHoc.ValueMember = "MaMh";

                // 3. Load Sinh viên
                var rawSV = BaoCaoService.Instance.LayDanhSachSinhVien();
                List<string> listSV = ((IEnumerable<string>)rawSV).ToList();
                cbbMaSV.DataSource = listSV;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message);
            }
        }

        // --- NÚT 1: Xuất danh sách SV của lớp ---
        private void btnXuatDanhSachSV_Click(object sender, EventArgs e)
        {
            if (cbbLopHoc1.SelectedItem == null) return;

            string maLop = cbbLopHoc1.Text; // Lấy text trực tiếp nếu DataSource là List<string>

            var dataList = BaoCaoService.Instance.LayDanhSachSinhVienTheoLop(maLop);
            DataTable dt = ConvertToDataTable(dataList); // Chuyển sang DataTable

            ExportToExcel(dt);
        }

        // --- NÚT 2: Xuất bảng điểm môn học của lớp ---
        private void btnXuatBaiDiem_Click(object sender, EventArgs e)
        {
            if (cbbLopHoc.SelectedItem == null || cbbMonHoc.SelectedValue == null) return;

            string maLop = cbbLopHoc.Text;
            string maMonHoc = cbbMonHoc.SelectedValue.ToString();

            var dataList = BaoCaoService.Instance.LayBangDiemMonHocCuaLop(maLop, maMonHoc);
            DataTable dt = ConvertToDataTable(dataList);

            ExportToExcel(dt);
        }

        // --- NÚT 3: Xuất báo cáo cá nhân (Mở form Report) ---
        private void btnXuatBaoCaoSV_Click(object sender, EventArgs e)
        {
            if (cbbMaSV.SelectedItem == null) return;

            string maSinhVien = cbbMaSV.Text;

            // Mở form BCTK (Code cũ của bạn)
            // Lưu ý: Form BCTK cũng cần được sửa để dùng EF Core nếu nó đang dùng SQL
            BCTK reportForm = new BCTK(maSinhVien);
            reportForm.ShowDialog();
        }

        // --- HÀM HỖ TRỢ XUẤT EXCEL (Giữ nguyên logic EPPlus) ---
        private void ExportToExcel(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var package = new ExcelPackage())
                {
                    var worksheet = package.Workbook.Worksheets.Add("Sheet1");
                    worksheet.Cells["A1"].LoadFromDataTable(dt, true);

                    // Format ngày tháng (Tìm cột có tên chứa chữ "Ngày" hoặc "Date")
                    for (int col = 1; col <= dt.Columns.Count; col++)
                    {
                        if (dt.Columns[col - 1].ColumnName.ToLower().Contains("ngày") ||
                            dt.Columns[col - 1].ColumnName.ToLower().Contains("date"))
                        {
                            worksheet.Column(col).Style.Numberformat.Format = "dd/MM/yyyy";
                        }
                        worksheet.Column(col).AutoFit();
                    }

                    using (var saveFileDialog = new SaveFileDialog())
                    {
                        saveFileDialog.Filter = "Excel Files|*.xlsx";
                        if (saveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            File.WriteAllBytes(saveFileDialog.FileName, package.GetAsByteArray());
                            MessageBox.Show("Xuất Excel thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi xuất Excel: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // --- HÀM HỖ TRỢ: CHUYỂN LIST SANG DATATABLE (Generic) ---
        // Giúp tương thích giữa EF Core (List) và EPPlus (DataTable)
        // Hàm mới: Không dùng <T> nữa mà dùng object để nhận Anonymous Type
        public DataTable ConvertToDataTable(object source)
        {
            DataTable table = new DataTable();

            // Ép kiểu sang danh sách các đối tượng
            var list = source as System.Collections.IEnumerable;
            if (list == null) return table;

            // Lấy phần tử đầu tiên để soi cấu trúc cột
            var enumerator = list.GetEnumerator();
            if (!enumerator.MoveNext()) return table; // Danh sách rỗng

            var firstItem = enumerator.Current;

            // Dùng Reflection để lấy tên cột từ đối tượng đầu tiên
            var properties = firstItem.GetType().GetProperties();

            foreach (var prop in properties)
            {
                // Xử lý kiểu Nullable (ví dụ int? -> int) để DataTable không lỗi
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }

            // Reset lại và duyệt từ đầu để đổ dữ liệu
            foreach (var item in list)
            {
                DataRow row = table.NewRow();
                foreach (var prop in properties)
                {
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                }
                table.Rows.Add(row);
            }

            return table;
        }

        private void guna2ControlBox2_Click(object sender, EventArgs e)
        {
            if (this.Parent != null)
            {
                this.Parent.Controls.Remove(this);
                this.Dispose();
            }
        }

        private void label1_Click(object sender, EventArgs e) { }
    }
}