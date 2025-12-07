using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using QuanLySinhVien_Nhom2.Models; // Gọi thư mục chứa Model EF Core

namespace Nhom2_QuanLySinhVien.Services
{
    public class LopHocService
    {
        private static LopHocService instance;

        // --- CẦU NỐI DỮ LIỆU (TempData) ---
        // Giúp các Form cũ binding dữ liệu không bị lỗi
        public static LopHoc TempData { get; set; } = new LopHoc();

        public string MaLop { get => TempData.MaLop; set => TempData.MaLop = value; }
        public string TenLop { get => TempData.TenLop; set => TempData.TenLop = value; }
        public string KhoaHoc { get => TempData.KhoaHoc; set => TempData.KhoaHoc = value; }
        public string HeDaoTao { get => TempData.HeDaoTao; set => TempData.HeDaoTao = value; }
        public string NamNhapHoc { get => TempData.NamNhapHoc; set => TempData.NamNhapHoc = value; }
        public string MaKhoa { get => TempData.MaKhoa; set => TempData.MaKhoa = value; }

        private LopHocService() { }

        public static LopHocService Instance
        {
            get
            {
                if (instance == null) instance = new LopHocService();
                return instance;
            }
        }

        // --- 1. LẤY DỮ LIỆU HIỂN THỊ ---

        // Lấy tất cả lớp học để đổ vào DataGridView
        public object LayTatCaLopHoc()
        {
            try
            {
                using (var db = new MyDbContext())
                {
                    // Select ra đối tượng ẩn danh để đặt tên cột tiếng Việt luôn
                    return db.LopHoc.Select(l => new
                    {
                        MaLop = l.MaLop,
                        TenLop = l.TenLop,
                        KhoaHoc = l.KhoaHoc,
                        HeDaoTao = l.HeDaoTao,
                        NamNhapHoc = l.NamNhapHoc,
                        MaKhoa = l.MaKhoa,
                        TenKhoa = l.MaKhoaNavigation.TenKhoa // Join tự động lấy tên Khoa
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lấy dữ liệu: " + ex.Message);
                return null;
            }
        }

        // --- 2. XỬ LÝ KHOA (Thay thế LayDuLieuKhoa) ---

        public List<Khoa> LayDanhSachKhoa()
        {
            using (var db = new MyDbContext())
            {
                return db.Khoa.ToList();
            }
        }

        public void HienThiKhoaLenComboBox(ComboBox cbo)
        {
            cbo.Items.Clear();
            var listKhoa = LayDanhSachKhoa();
            foreach (var k in listKhoa)
            {
                cbo.Items.Add(k.MaKhoa); // Hoặc k.TenKhoa tùy logic cũ
            }
        }

        // --- 3. THÊM - SỬA - XÓA ---

        public void Them(string ma, string ten, string khoaHoc, string heDT, string namNH, string maKhoa)
        {
            using (var db = new MyDbContext())
            {
                // Kiểm tra trùng mã (Thay thế hàm KiemTra cũ)
                if (db.LopHoc.Any(x => x.MaLop == ma))
                {
                    MessageBox.Show("Mã lớp đã tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                try
                {
                    var lop = new LopHoc()
                    {
                        MaLop = ma,
                        TenLop = ten,
                        KhoaHoc = khoaHoc,
                        HeDaoTao = heDT,
                        NamNhapHoc = namNH,
                        MaKhoa = maKhoa
                    };

                    db.LopHoc.Add(lop);
                    db.SaveChanges();
                    MessageBox.Show($"Thêm thành công lớp: {ten}", "Thông báo");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi thêm: " + ex.Message);
                }
            }
        }

        public void Sua(string ma, string ten, string khoaHoc, string heDT, string namNH, string maKhoa)
        {
            using (var db = new MyDbContext())
            {
                try
                {
                    var lop = db.LopHoc.Find(ma);
                    if (lop != null)
                    {
                        lop.TenLop = ten;
                        lop.KhoaHoc = khoaHoc;
                        lop.HeDaoTao = heDT;
                        lop.NamNhapHoc = namNH;
                        lop.MaKhoa = maKhoa;

                        db.SaveChanges();
                        MessageBox.Show($"Sửa thành công lớp: {ten}", "Thông báo");
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy lớp này để sửa.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi sửa: " + ex.Message);
                }
            }
        }

        public void Xoa(string maLop)
        {
            using (var db = new MyDbContext())
            {
                try
                {
                    // Kiểm tra xem lớp có sinh viên không trước khi xóa (Ràng buộc khóa ngoại)
                    var lop = db.LopHoc.Include(l => l.SinhVien).FirstOrDefault(l => l.MaLop == maLop);

                    if (lop == null)
                    {
                        MessageBox.Show("Không tìm thấy mã lớp.");
                        return;
                    }

                    if (lop.SinhVien.Count > 0) // Lưu ý: Cần kiểm tra SinhVien trong Model LopHoc
                    {
                        MessageBox.Show($"Lớp {maLop} đang có {lop.SinhVien.Count} sinh viên. Không thể xóa!", "Cảnh báo");
                        return;
                    }

                    db.LopHoc.Remove(lop);
                    db.SaveChanges();
                    MessageBox.Show("Xóa thành công!", "Thông báo");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi xóa: " + ex.Message);
                }
            }
        }

        // --- 4. TÌM KIẾM ---

        public object TimKiemLop(string tuKhoa, string maKhoa)
        {
            using (var db = new MyDbContext())
            {
                var query = db.LopHoc.AsQueryable();

                // Lọc theo Khoa nếu có chọn
                if (!string.IsNullOrEmpty(maKhoa) && maKhoa != "Chọn Khoa")
                {
                    query = query.Where(l => l.MaKhoa == maKhoa);
                }

                // Lọc theo từ khóa (Mã, Tên, Hệ ĐT...) - Logic giống SQL cũ
                if (!string.IsNullOrEmpty(tuKhoa))
                {
                    query = query.Where(l => l.MaLop.Contains(tuKhoa) ||
                                             l.TenLop.Contains(tuKhoa) ||
                                             l.HeDaoTao.Contains(tuKhoa) ||
                                             l.KhoaHoc.Contains(tuKhoa));
                }

                return query.Select(l => new
                {
                    Mã_Lớp = l.MaLop,
                    Tên_Lớp = l.TenLop,
                    Khóa_Học = l.KhoaHoc,
                    Hệ_ĐT = l.HeDaoTao,
                    Năm_NH = l.NamNhapHoc,
                    Mã_Khoa = l.MaKhoa,
                    Tên_Khoa = l.MaKhoaNavigation.TenKhoa
                }).ToList();
            }
        }

        // --- 5. CÁC HÀM HỖ TRỢ KHÁC (Thay thế LayThongTinLop) ---

        public void LayThongTinLopVaoTextBox(string maLop, TextBox txtTen, TextBox txtKhoaHoc)
        {
            using (var db = new MyDbContext())
            {
                var lop = db.LopHoc.Find(maLop);
                if (lop != null)
                {
                    txtTen.Text = lop.TenLop;
                    txtKhoaHoc.Text = lop.KhoaHoc;
                    // ... gán thêm các textbox khác nếu cần

                    // Cập nhật vào TempData luôn
                    TempData = lop;
                }
            }
        }

        // Thêm vào Services/LopHocService.cs
        public object SapXepLopHocTheoTen()
        {
            try
            {
                using (var db = new MyDbContext())
                {
                    // Lấy danh sách và sắp xếp tăng dần theo Tên Lớp
                    return db.LopHoc.OrderBy(l => l.TenLop).Select(l => new
                    {
                        Mã_Lớp = l.MaLop,
                        Tên_Lớp = l.TenLop,
                        Khóa_Học = l.KhoaHoc,
                        Hệ_ĐT = l.HeDaoTao,
                        Năm_NH = l.NamNhapHoc,
                        Mã_Khoa = l.MaKhoa,
                        Tên_Khoa = l.MaKhoaNavigation.TenKhoa
                    }).ToList();
                }
            }
            catch { return null; }
        }
    }
}