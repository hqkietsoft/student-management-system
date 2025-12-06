using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using QuanLySinhVien_Nhom2.Models; // Namespace chứa EF Core Models
using LHPModel = QuanLySinhVien_Nhom2.Models.LopHocPhan;

namespace Nhom2_QuanLySinhVien.Services
{
    public class LopHocPhanService
    {
        private static LopHocPhanService instance;
        public static LopHocPhanService Instance
        {
            get
            {
                if (instance == null) instance = new LopHocPhanService();
                return instance;
            }
        }

        private LopHocPhanService() { }

        // --- 1. LẤY DỮ LIỆU HIỂN THỊ ---
        public object LayTatCaLopHocPhan()
        {
            try
            {
                using (var db = new MyDbContext())
                {
                    // Trả về danh sách đối tượng ẩn danh khớp với các cột trên GridView
                    return db.LopHocPhan.Select(l => new
                    {
                        MaLop = l.MaLop,
                        MaMH = l.MaMh,
                        MaGV = l.MaGv,
                        HocKy = l.HocKy,
                        Nam = l.Nam
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message);
                return null;
            }
        }

        // --- 2. TÌM KIẾM ---
        // --- 2. TÌM KIẾM ---
        public object TimKiemLopHocPhan(string keyword)
        {
            try
            {
                using (var db = new MyDbContext())
                {
                    // SỬA LỖI: Thêm .ToString() vào HocKy và Nam
                    return db.LopHocPhan
                                .Where(l => l.MaLop.Contains(keyword) ||
                                            l.MaMh.Contains(keyword) ||
                                            l.MaGv.Contains(keyword) ||
                                            l.HocKy.ToString().Contains(keyword) || // Thêm .ToString()
                                            l.Nam.ToString().Contains(keyword))     // Thêm .ToString()
                                .Select(l => new
                                {
                                    MaLop = l.MaLop,
                                    MaMH = l.MaMh,
                                    MaGV = l.MaGv,
                                    HocKy = l.HocKy,
                                    Nam = l.Nam
                                }).ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tìm kiếm: " + ex.Message);
                return null;
            }
        }

        // --- 3. XÓA ---
        public void Xoa(string maLop)
        {
            using (var db = new MyDbContext())
            {
                try
                {
                    var lhp = db.LopHocPhan.Find(maLop);
                    if (lhp != null)
                    {
                        // Kiểm tra ràng buộc điểm số trước khi xóa (nếu cần)
                        // if (db.Diems.Any(d => d.MaMh == lhp.MaMh)) { ... }

                        db.LopHocPhan.Remove(lhp);
                        db.SaveChanges();
                        MessageBox.Show("Xóa lớp học phần thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy lớp học phần này.", "Lỗi");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi xóa (Có thể do ràng buộc dữ liệu): " + ex.Message, "Lỗi");
                }
            }
        }

        // --- 4. CÁC HÀM HỖ TRỢ COMBOBOX ---

        // Lấy danh sách Môn học (Format: Mã - Tên)
        public object LayDanhSachMonHoc()
        {
            using (var db = new MyDbContext())
            {
                return db.MonHoc.Select(mh => new { Value = mh.MaMh, Display = mh.MaMh + " - " + mh.TenMh }).ToList();
            }
        }

        public object LayDanhSachGiaoVien()
        {
            using (var db = new MyDbContext())
            {
                return db.GiaoVien.Select(gv => new { Value = gv.MaGv, Display = gv.MaGv + " - " + gv.HoTen }).ToList();
            }
        }

        // --- HÀM SỬA (Đã dùng LHPModel) ---
        public bool Sua(string maLopCu, string maLopMoi, string maMH, string maGV, int hocKy, decimal nam)
        {
            using (var db = new MyDbContext())
            {
                try
                {
                    if (maLopCu != maLopMoi && db.LopHocPhan.Any(x => x.MaLop == maLopMoi))
                    {
                        MessageBox.Show($"Mã {maLopMoi} đã tồn tại!", "Cảnh báo");
                        return false;
                    }

                    var lhp = db.LopHocPhan.Find(maLopCu);
                    if (lhp == null) return false;

                    if (maLopCu != maLopMoi) // Đổi mã -> Xóa cũ thêm mới
                    {
                        // SỬA Ở ĐÂY: Dùng LHPModel thay vì LopHocPhan
                        var lhpMoi = new LHPModel()
                        {
                            MaLop = maLopMoi,
                            MaMh = maMH,
                            MaGv = maGV,
                            HocKy = hocKy,
                            Nam = (int)nam // Ép kiểu decimal -> int
                        };
                        db.LopHocPhan.Remove(lhp);
                        db.LopHocPhan.Add(lhpMoi);
                    }
                    else // Cập nhật bình thường
                    {
                        lhp.MaMh = maMH;
                        lhp.MaGv = maGV;
                        lhp.HocKy = hocKy;
                        lhp.Nam = (int)nam;
                    }

                    db.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi cập nhật: " + ex.Message);
                    return false;
                }
            }
        }

        // --- HÀM THÊM (Đã dùng LHPModel) ---
        public bool Them(string maLop, string maMH, string maGV, int hocKy, decimal nam)
        {
            using (var db = new MyDbContext())
            {
                try
                {
                    if (db.LopHocPhan.Any(x => x.MaLop == maLop))
                    {
                        MessageBox.Show($"Mã lớp {maLop} đã tồn tại!", "Cảnh báo");
                        return false;
                    }

                    // SỬA Ở ĐÂY: Dùng LHPModel
                    var lhp = new LHPModel()
                    {
                        MaLop = maLop,
                        MaMh = maMH,
                        MaGv = maGV,
                        HocKy = hocKy,
                        Nam = (int)nam
                    };

                    db.LopHocPhan.Add(lhp);
                    db.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi thêm mới: " + ex.Message);
                    return false;
                }
            }
        }
    }
}