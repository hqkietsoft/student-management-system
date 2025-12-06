using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore; // Thư viện EF Core
using QuanLySinhVien_Nhom2.Models;   // Gọi namespace chứa Model mới (class SinhVien, MyDbContext)

namespace Nhom2_QuanLySinhVien.Model // Đổi namespace cho gọn
{
    public class SinhVienService
    {
        public static SinhVien SinhVienDangChon { get; set; } = new SinhVien();
        // 1. Giữ nguyên Singleton Pattern để các Form không phải sửa code gọi
        private static SinhVienService instance;
        public static SinhVienService SV
        {
            get
            {
                if (instance == null)
                    instance = new SinhVienService();
                return instance;
            }
        }

        private SinhVienService() { }

        // =========================================================================
        // PHẦN CẦU NỐI (QUAN TRỌNG): Giúp Form cũ chạy được mà không cần sửa code
        // =========================================================================

        // 1. Tạo một biến lưu trữ tạm thời (để hứng dữ liệu từ Form)
        public static SinhVien TempData { get; set; } = new SinhVien();

        // 2. Tạo các Property ảo trỏ vào biến tạm kia (Tên y hệt code cũ của bạn)
        public string MaSV1 { get => TempData.MaSv; set => TempData.MaSv = value; }
        public string HoDem1 { get => TempData.HoDem; set => TempData.HoDem = value; }
        public string Ten1 { get => TempData.Ten; set => TempData.Ten = value; }

        // Lưu ý: Code cũ bạn dùng string cho NgaySinh, nhưng DB là DateTime
        // Ta cần chuyển đổi qua lại để không lỗi Form
        public string NgaySinh1
        {
            get => TempData.NgaySinh?.ToString("dd/MM/yyyy");
            set
            {
                if (DateTime.TryParse(value, out DateTime dt)) TempData.NgaySinh = dt;
            }
        }

        public string GioiTinh1 { get => TempData.GioiTinh; set => TempData.GioiTinh = value; }
        public string QueQuan1 { get => TempData.QueQuan; set => TempData.QueQuan = value; }
        public string SoDT1 { get => TempData.SoDt; set => TempData.SoDt = value; }
        public string MaLop1 { get => TempData.MaLop; set => TempData.MaLop = value; }
        public string TenDN1 { get => TempData.TenDn; set => TempData.TenDn = value; }

        // =========================================================================
        // HẾT PHẦN CẦU NỐI - Dưới đây là logic xử lý EF Core
        // =========================================================================

        // --- PHẦN 1: HIỂN THỊ DỮ LIỆU ---

        // Hàm này trả về List thay vì DataTable (DataGridView vẫn hiểu tốt)
        public object HienThiDuLieu_sinhvien()
        {
            using (var db = new MyDbContext())
            {
                // Dùng LINQ để lấy dữ liệu. 
                // .Select() giúp chọn những cột cần thiết và đặt tên tiếng Việt cho tiêu đề cột
                return db.SinhVien
                         .Select(sv => new
                         {
                             MaSv = sv.MaSv,
                             HoDem = sv.HoDem,
                             Ten = sv.Ten,
                             NgaySinh = sv.NgaySinh,
                             GioiTinh = sv.GioiTinh,
                             QueQuan = sv.QueQuan,
                             MaLop = sv.MaLop,
                             SoDT = sv.SoDt,
                             TenDN = sv.TenDn
                         })
                         .ToList();
            }
        }

        public void Show_All(DataGridView dgv)

        {

            dgv.DataSource = HienThiDuLieu_sinhvien();



        }

        // --- PHẦN 2: CÁC HÀM HỖ TRỢ COMBOBOX ---

        // Lấy danh sách Lớp học để đổ vào ComboBox
        public List<LopHoc> LayDanhSachLop()
        {
            using (var db = new MyDbContext())
            {
                return db.LopHoc.ToList();
            }
        }

        // Hỗ trợ hàm cũ: ShowOnComboBox (Sửa lại logic vòng lặp)
        public void ShowOnComboBox(ComboBox cbo)
        {
            cbo.Items.Clear();
            var listLop = LayDanhSachLop();
            foreach (var lop in listLop)
            {
                cbo.Items.Add(lop.MaLop); // Hoặc lop.TenLop tùy bạn
            }
        }

        // Hỗ trợ hàm cũ: ShowOnComboBox1 (Cho Người dùng)
        public void ShowOnComboBox1(ComboBox cbo)
        {
            cbo.Items.Clear();
            using (var db = new MyDbContext())
            {
                var listUser = db.NguoiDung.ToList();
                foreach (var u in listUser)
                {
                    cbo.Items.Add(u.TenDn);
                }
            }
        }

        // --- PHẦN 3: THÊM - SỬA - XÓA (QUAN TRỌNG NHẤT) ---

        // Giữ nguyên tham số đầu vào y hệt cũ để Form không bị lỗi
        public void Them(string masv, string hodem, string ten, DateTimePicker ngaysinh, string gioitinh, string quequan, string sdt, string malop, ComboBox cbbTenDN)
        {
            string tendn = cbbTenDN.Text;

            using (var db = new MyDbContext())
            {
                // 1. Kiểm tra trùng (Thay thế Check_Ma_TenDN)
                if (db.SinhVien.Any(x => x.MaSv == masv))
                {
                    MessageBox.Show("Đã tồn tại mã sinh viên!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


                try
                {
                    // 2. Thêm NguoiDung trước (Logic cũ của bạn có đoạn INSERT INTO NGUOIDUNG)
                    var userExit = db.NguoiDung.Find(tendn);
                    if (userExit == null)
                    {
                        var newUser = new NguoiDung()
                        {
                            TenDn = tendn,
                            MatKhau = "1111", // Mật khẩu mặc định như code cũ
                            LoaiNd = 4         // Quyền mặc định như code cũ
                        };
                        db.NguoiDung.Add(newUser);
                        db.SaveChanges(); // Lưu User trước để tránh lỗi khóa ngoại
                    }

                    // 3. Thêm SinhVien (Map tham số vào Object mới)
                    var svMoi = new SinhVien()
                    {
                        MaSv = masv,
                        HoDem = hodem,
                        Ten = ten,
                        NgaySinh = ngaysinh.Value,
                        GioiTinh = gioitinh,
                        QueQuan = quequan,
                        SoDt = sdt,
                        MaLop = malop,
                        TenDn = tendn
                    };

                    db.SinhVien.Add(svMoi);
                    db.SaveChanges(); // Lưu Sinh viên xuống SQL

                    MessageBox.Show($"Thêm thành công sinh viên {ten}.\nMật khẩu mặc định: 1111", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    // Hiển thị lỗi chi tiết (bao gồm cả lỗi bên trong InnerException)
                    MessageBox.Show("Lỗi: " + (ex.InnerException?.Message ?? ex.Message));
                }
            }
        }

        public void Sua(string masv, string hodem, string ten, DateTimePicker ngaysinh, string gioitinh, string quequan, string sdt, string malop, string tendn)
        {
            using (var db = new MyDbContext())
            {
                try
                {
                    // 1. Tìm sinh viên cần sửa
                    var svCanSua = db.SinhVien.Find(masv);

                    if (svCanSua != null)
                    {
                        // 2. Cập nhật thông tin mới
                        svCanSua.HoDem = hodem;
                        svCanSua.Ten = ten;
                        svCanSua.NgaySinh = ngaysinh.Value;
                        svCanSua.GioiTinh = gioitinh;
                        svCanSua.QueQuan = quequan;
                        svCanSua.SoDt = sdt;
                        svCanSua.MaLop = malop;
                        svCanSua.TenDn = tendn;

                        // 3. Lưu lại
                        db.SaveChanges();
                        MessageBox.Show($"Đã sửa thông tin: {ten}", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy sinh viên này!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi sửa: " + ex.Message);
                }
            }
        }

        public void Xoa(string masv, string tendn)
        {
            using (var db = new MyDbContext())
            {
                try
                {
                    // 1. Tìm sinh viên
                    var svXoa = db.SinhVien.Find(masv);
                    if (svXoa != null)
                    {
                        // 2. Xóa
                        db.SinhVien.Remove(svXoa);
                        db.SaveChanges();
                        MessageBox.Show($"Đã xóa sinh viên mã: {masv}", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi xóa (Có thể do ràng buộc điểm số): " + ex.Message);
                }
            }
        }

        // --- PHẦN 4: TÌM KIẾM ---

        // Trả về List object để gán vào DataGridView
        public object TimKiem(string keyword)
        {
            using (var db = new MyDbContext())
            {
                // SELECT * FROM SinhVien WHERE ... LIKE ...
                var ketQua = db.SinhVien
                               .Where(sv => sv.MaSv.Contains(keyword) ||
                                            sv.Ten.Contains(keyword) ||
                                            sv.MaLop.Contains(keyword))
                               .Select(sv => new {
                                   Mã_SV = sv.MaSv,
                                   Họ_tên = sv.HoDem + " " + sv.Ten,
                                   Ngày_Sinh = sv.NgaySinh,
                                   Lớp = sv.MaLop,
                                   // ... thêm các cột khác nếu muốn hiện thị
                               })
                               .ToList();
                return ketQua;
            }
        }

        // Hàm sắp xếp
        public object SapXep()
        {
            using (var db = new MyDbContext())
            {
                return db.SinhVien.OrderBy(sv => sv.Ten).ToList();
            }
        }
    }
}