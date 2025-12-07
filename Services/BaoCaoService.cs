using System;
using System.Data; // Cần dùng DataTable để xuất Excel dễ dàng
using System.Linq;
using Microsoft.EntityFrameworkCore;
using QuanLySinhVien_Nhom2.Models;

namespace Nhom2_QuanLySinhVien.Services
{
    public class BaoCaoService
    {
        private static BaoCaoService instance;
        public static BaoCaoService Instance
        {
            get { if (instance == null) instance = new BaoCaoService(); return instance; }
        }
        private BaoCaoService() { }

        // --- CÁC HÀM LẤY DỮ LIỆU CHO COMBOBOX ---

        public object LayDanhSachLop()
        {
            using (var db = new MyDbContext())
            {
                return db.LopHoc.Select(l => l.MaLop).Distinct().ToList();
            }
        }

        public object LayDanhSachMonHoc()
        {
            using (var db = new MyDbContext())
            {
                return db.MonHoc.Select(m => new { m.MaMh, m.TenMh }).ToList();
            }
        }

        public object LayDanhSachSinhVien()
        {
            using (var db = new MyDbContext())
            {
                return db.SinhVien.Select(s => s.MaSv).ToList();
            }
        }

        // --- CÁC HÀM LẤY DỮ LIỆU BÁO CÁO (TRẢ VỀ DATATABLE) ---
        // Lưu ý: EPPlus hỗ trợ LoadFromDataTable rất tốt, nên ta convert List sang DataTable 
        // hoặc trả về List object rồi dùng thư viện convert sau. 
        // Ở đây tôi trả về List Object để chuẩn EF Core, việc convert sẽ làm ở Form.

        public object LayDanhSachSinhVienTheoLop(string maLop)
        {
            using (var db = new MyDbContext())
            {
                return db.SinhVien
                         .Where(s => s.MaLop == maLop)
                         .Select(s => new
                         {
                             Mã_SV = s.MaSv,
                             Họ_Đệm = s.HoDem,
                             Tên = s.Ten,
                             Ngày_Sinh = s.NgaySinh,
                             Giới_Tính = s.GioiTinh,
                             Quê_Quán = s.QueQuan,
                             Số_ĐT = s.SoDt,
                             Tên_ĐN = s.TenDn
                         }).ToList();
            }
        }

        public object LayBangDiemMonHocCuaLop(string maLop, string maMH)
        {
            using (var db = new MyDbContext())
            {
                // Join 3 bảng: SinhVien - Diem - MonHoc
                // Logic cũ: SELECT ... FROM SinhVien JOIN Diem ... WHERE MaLop... AND MaMH...
                var query = from s in db.SinhVien
                            join d in db.Diem on s.MaSv equals d.MaSv
                            where s.MaLop == maLop && d.MaMh == maMH
                            select new
                            {
                                Mã_Lớp = s.MaLop,
                                Mã_MH = d.MaMh,
                                Mã_SV = s.MaSv,
                                Họ_Đệm = s.HoDem,
                                Tên = s.Ten,
                                Điểm_CC = d.DiemCc,
                                Điểm_HS1 = d.DiemHs1,
                                Điểm_HS2_L1 = d.DiemHs2l1,
                                Điểm_HS2_L2 = d.DiemHs2l2,
                                Điểm_QT = d.DiemQuaTrinh,
                                Điểm_Thi = d.DiemThi,
                                Điểm_HP = d.DiemHocPhan,
                                Điểm_TBHK = d.DiemTbhk
                            };
                return query.ToList();
            }
        }
    }
}