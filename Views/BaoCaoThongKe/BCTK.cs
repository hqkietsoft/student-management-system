using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Nhom2_QuanLySinhVien.Services;
using QuanLySinhVien_Nhom2.Models;

namespace QuanLySinhVien_Nhom2
{
    public partial class BCTK : Form
    {
        private string maSinhVien;

        public BCTK(string maSinhVien)
        {
            InitializeComponent();
            this.maSinhVien = maSinhVien;
            this.StartPosition = FormStartPosition.CenterScreen;

            // Thiết lập Dock & Zoom
            reportViewer1.Dock = DockStyle.Fill;
            reportViewer1.ZoomMode = ZoomMode.PageWidth;
        }

        private void BCTK_Load(object sender, EventArgs e)
        {
            this.reportViewer1.RefreshReport();
            try
            {
                LoadReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải báo cáo: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // --- HÀM LẤY DỮ LIỆU (Dùng EF Core thay vì SQL) ---
        private DataTable GetDiemSinhVien()
        {
            using (var db = new MyDbContext())
            {
                // Query JOIN phức tạp tương tự câu SQL cũ
                var query = from d in db.Diem
                            join mh in db.MonHoc on d.MaMh equals mh.MaMh
                            join sv in db.SinhVien on d.MaSv equals sv.MaSv
                            join lhp in db.LopHocPhan on mh.MaMh equals lhp.MaMh 
                            where sv.MaSv == maSinhVien
                            select new
                            {
                                // Đặt tên property giống hệt cột trong DataSet của Report (RDLC)
                                MaSV1 = d.MaSv,
                                MaMH1 = d.MaMh,
                                HoDem = sv.HoDem,
                                Ten = sv.Ten,
                                TenMH = mh.TenMh,
                                SoTC = mh.SoTc,
                                DiemCC = d.DiemCc,
                                DiemHS1 = d.DiemHs1,
                                DiemHS2L1 = d.DiemHs2l1,
                                DiemHS2L2 = d.DiemHs2l2,
                                DiemQuaTrinh = d.DiemQuaTrinh,
                                DiemThi = d.DiemThi,
                                DiemHocPhan = d.DiemHocPhan,
                                DiemTBHK = d.DiemTbhk
                            };

                var listResult = query.Distinct().ToList(); // Distinct nếu cần thiết

                // Chuyển List sang DataTable để ReportViewer hiểu
                // Gọi hàm Convert từ BaoCaoThongKe hoặc viết lại hàm Convert đơn giản ở đây
                return ConvertListToDataTable(listResult);
            }
        }

        // Hàm hỗ trợ convert nhanh (Copy từ BaoCaoThongKe sang đây để form độc lập)
        private DataTable ConvertListToDataTable<T>(IEnumerable<T> data)
        {
            DataTable table = new DataTable();
            if (data == null || !data.Any()) return table;

            var props = typeof(T).GetProperties(); // Dùng Reflection với Anonymous Type thì hơi khác
            // Với Anonymous Type, ta lấy type từ phần tử đầu tiên
            var firstItem = data.First();
            var properties = firstItem.GetType().GetProperties();

            foreach (var prop in properties)
            {
                // Xử lý Nullable
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }

            foreach (var item in data)
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

        private void LoadReport()
        {
            try
            {
                string reportPath = FindReportPath("Report.rdlc");

                if (string.IsNullOrEmpty(reportPath))
                {
                    MessageBox.Show("Không tìm thấy file Report.rdlc!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                reportViewer1.LocalReport.ReportPath = reportPath;

                // Lấy dữ liệu từ EF Core
                DataTable dtDiem = GetDiemSinhVien();

                reportViewer1.LocalReport.DataSources.Clear();
                // "DataSet1" là tên Dataset bạn đặt trong file RDLC (Check kỹ file Report để đặt đúng)
                reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dtDiem));

                reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải báo cáo: {ex.Message}", "Lỗi");
            }
        }

        private string FindReportPath(string reportFileName)
        {
            string[] possiblePaths = new string[]
            {
                Path.Combine(Application.StartupPath, reportFileName),
                Path.Combine(Directory.GetCurrentDirectory(), reportFileName),
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, reportFileName), // Thêm path này
                Path.GetFullPath(reportFileName),
                // Lên 2-3 cấp để tìm nếu đang chạy Debug
                Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, reportFileName)
            };

            foreach (var path in possiblePaths)
            {
                if (File.Exists(path)) return path;
            }
            return null;
        }

        // Chức năng Export PDF (Giữ nguyên logic cũ)
        private void ExportReport(string format = "PDF")
        {
            try
            {
                string deviceInfo = null;
                string mimeType;
                string encoding;
                string fileNameExtension;
                string[] streams;
                Warning[] warnings;

                byte[] bytes = reportViewer1.LocalReport.Render(
                    format, deviceInfo, out mimeType, out encoding, out fileNameExtension,
                    out streams, out warnings);

                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "PDF files (*.pdf)|*.pdf|All files (*.*)|*.*";
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        File.WriteAllBytes(saveFileDialog.FileName, bytes);
                        MessageBox.Show("Xuất báo cáo thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi xuất báo cáo: {ex.Message}");
            }
        }

        private void reportViewer1_Load(object sender, EventArgs e) { }
    }
}