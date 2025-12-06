using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using QuanLySinhVien_Nhom2.Models;

namespace Nhom2_QuanLySinhVien.Services
{
    public class DiemService
    {
        private static DiemService instance;

        // --- CẦU NỐI DỮ LIỆU (Giữ tương thích Form cũ) ---
        public static Diem TempData { get; set; } = new Diem();

        public string Masv { get => TempData.MaSv; set => TempData.MaSv = value; }
        public string Mamh { get => TempData.MaMh; set => TempData.MaMh = value; }

        // Ép kiểu an toàn (Database có thể null, Code cũ dùng float)
        public float Diemcc { get => (float)(TempData.DiemCc ?? 0); set => TempData.DiemCc = value; }
        public float Diemhs1 { get => (float)(TempData.DiemHs1 ?? 0); set => TempData.DiemHs1 = value; }
        public float Diemhs2l1 { get => (float)(TempData.DiemHs2l1 ?? 0); set => TempData.DiemHs2l1 = value; }
        public float Diemhs2l2 { get => (float)(TempData.DiemHs2l2 ?? 0); set => TempData.DiemHs2l2 = value; }
        public float Diemthi { get => (float)(TempData.DiemThi ?? 0); set => TempData.DiemThi = value; }
        public float Diemquatrinh { get => (float)(TempData.DiemQuaTrinh ?? 0); set => TempData.DiemQuaTrinh = value; }
        public float Diemhocphan { get => (float)(TempData.DiemHocPhan ?? 0); set => TempData.DiemHocPhan = value; }
        public float Diemtbhk { get => (float)(TempData.DiemTbhk ?? 0); set => TempData.DiemTbhk = value; }

        private DiemService() { }

        public static DiemService DIEM
        {
            get
            {
                if (instance == null) instance = new DiemService();
                return instance;
            }
        }

        // --- CÁC HÀM HIỂN THỊ (Dùng JOIN thay vì Navigation) ---

        public object LayDuLieuDiem_Monhoc_SinhVien()
        {
            try
            {
                using (var db = new MyDbContext())
                {
                    // JOIN thủ công 3 bảng: Diem - SinhVien - MonHoc
                    var query = from d in db.Diem
                                join sv in db.SinhVien on d.MaSv equals sv.MaSv
                                join mh in db.MonHoc on d.MaMh equals mh.MaMh
                                select new
                                {
                                    MaSV = d.MaSv,
                                    HoDem = sv.HoDem,
                                    Ten = sv.Ten,
                                    MaMH = d.MaMh,
                                    TenMH = mh.TenMh,
                                    SoTC = mh.SoTc,
                                    DiemHocPhan = d.DiemHocPhan,
                                    DiemTBHK = d.DiemTbhk
                                };
                    return query.ToList();
                }
            }
            catch { return null; }
        }

        public object LayDuLieuDiem_Monhoc_SinhVien1()
        {
            try
            {
                using (var db = new MyDbContext())
                {
                    // Lấy chi tiết tất cả điểm
                    var query = from d in db.Diem
                                join sv in db.SinhVien on d.MaSv equals sv.MaSv
                                join mh in db.MonHoc on d.MaMh equals mh.MaMh
                                select new
                                {
                                    MaSV = d.MaSv,
                                    HoDem = sv.HoDem,
                                    Ten = sv.Ten,
                                    MaMH = d.MaMh,
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
                    return query.ToList();
                }
            }
            catch { return null; }
        }

        public object LayDuLieuDiem_Monhoc_SinhVien_Cbo(string hocky)
        {
            try
            {
                using (var db = new MyDbContext())
                {
                    string keyword = "MH" + hocky; // Logic cũ của bạn: %MH{hocky}%
                    var query = from d in db.Diem
                                join sv in db.SinhVien on d.MaSv equals sv.MaSv
                                join mh in db.MonHoc on d.MaMh equals mh.MaMh
                                where d.MaMh.Contains(keyword)
                                select new
                                {
                                    MaSV = d.MaSv,
                                    HoDem = sv.HoDem,
                                    Ten = sv.Ten,
                                    MaMH = d.MaMh,
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
                    return query.ToList();
                }
            }
            catch { return null; }
        }

        public void HienThiLenDataGridView(DataGridView dgv)
        {
            dgv.DataSource = LayDuLieuDiem_Monhoc_SinhVien1();
        }

        // --- CÁC HÀM HỖ TRỢ ---

        public List<SinhVien> LayDuLieuSV()
        {
            using (var db = new MyDbContext())
            {
                return db.SinhVien.ToList();
            }
        }

        public void HienThiLenComboboxSV(ComboBox cbo)
        {
            cbo.Items.Clear();
            var list = LayDuLieuSV();
            foreach (var sv in list)
            {
                cbo.Items.Add($"{sv.MaSv} -{sv.HoDem} {sv.Ten}");
            }
        }

        // --- CÁC HÀM TÍNH TOÁN & LOGIC NGHIỆP VỤ ---

        public object HienThiDiemLenCBO(string hocky)
        {
            // Logic cũ: JOIN Diem -> LopHocPhan -> MonHoc để lọc học kỳ
            try
            {
                using (var db = new MyDbContext())
                {
                    var query = from d in db.Diem
                                join lhp in db.LopHocPhan on d.MaMh equals lhp.MaMh
                                where lhp.HocKy.ToString() == hocky
                                select d.MaSv;
                    return query.Distinct().ToList();
                }
            }
            catch { return new List<string>(); }
        }

        public List<DiemDTO> TinhDiemTBHK(string masv, string hocky)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(masv) || string.IsNullOrWhiteSpace(hocky))
                {
                    throw new ArgumentException("Mã sinh viên và học kỳ không được để trống");
                }

                using (var db = new MyDbContext())
                {
                    string keyword = "MH" + hocky;

                    // Query dữ liệu và đổ vào class DiemDTO
                    var query = from d in db.Diem
                                join mh in db.MonHoc on d.MaMh equals mh.MaMh
                                where d.MaSv == masv && d.MaMh.Contains(keyword)
                                select new DiemDTO
                                {
                                    MaSV = d.MaSv,
                                    MaMH = d.MaMh,
                                    TenMH = mh.TenMh,
                                    SoTC = mh.SoTc ?? 0,
                                    DiemHocPhan = (double)(d.DiemHocPhan ?? 0)
                                };

                    return query.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi tính điểm TBHK cho sinh viên {masv}: {ex.Message}", ex);
            }
        }

        public bool UpdateDiemTBHK(double diem, string masv)
        {
            try
            {
                // Validate input
                if (string.IsNullOrWhiteSpace(masv))
                {
                    return false;
                }

                using (var db = new MyDbContext())
                {
                    // Cập nhật điểm TBHK cho sinh viên
                    var listDiem = db.Diem.Where(d => d.MaSv == masv).ToList();

                    if (listDiem == null || listDiem.Count == 0)
                    {
                        return false; // Không tìm thấy điểm của sinh viên
                    }

                    foreach (var item in listDiem)
                    {
                        item.DiemTbhk = diem;
                    }

                    db.SaveChanges();
                    return true; // Cập nhật thành công
                }
            }
            catch (Exception ex)
            {
                // Log error instead of showing MessageBox
                System.Diagnostics.Debug.WriteLine($"Error updating DiemTBHK: {ex.Message}");
                return false; // Cập nhật thất bại
            }
        }

        public object XetHocBong(string hocky)
        {
            try
            {
                using (var db = new MyDbContext())
                {
                    string keyword = "MH" + hocky;

                    var query = from d in db.Diem
                                join sv in db.SinhVien on d.MaSv equals sv.MaSv
                                join mh in db.MonHoc on d.MaMh equals mh.MaMh
                                where d.DiemTbhk >= 7 && d.MaMh.Contains(keyword)
                                orderby d.DiemTbhk descending
                                select new
                                {
                                    MaSV = d.MaSv,
                                    HoDem = sv.HoDem,
                                    Ten = sv.Ten,
                                    MaMH = d.MaMh,
                                    TenMH = mh.TenMh,
                                    SoTC = mh.SoTc,
                                    DiemHocPhan = d.DiemHocPhan,
                                    DiemTBHK = d.DiemTbhk
                                };

                    // Logic lấy Top 10%
                    int total = query.Count();
                    int take = (int)Math.Ceiling(total * 0.1);
                    if (take < 1 && total > 0) take = 1;

                    return query.Take(take).ToList();
                }
            }
            catch { return null; }
        }

        public object TimKiemDiemMonHoc(string diem)
        {
            try
            {
                using (var db = new MyDbContext())
                {
                    // Tìm kiếm gần đúng theo điểm
                    // Lưu ý: So sánh chuỗi trên server SQL có thể hạn chế, ta kéo về client rồi lọc
                    var query = from d in db.Diem
                                join sv in db.SinhVien on d.MaSv equals sv.MaSv
                                join mh in db.MonHoc on d.MaMh equals mh.MaMh
                                select new
                                {
                                    MaSV = d.MaSv,
                                    HoDem = sv.HoDem,
                                    Ten = sv.Ten,
                                    MaMH = d.MaMh,
                                    TenMH = mh.TenMh,
                                    SoTC = mh.SoTc,
                                    DiemHocPhan = d.DiemHocPhan,
                                    DiemTBHK = d.DiemTbhk
                                };

                    // Lọc client side để dùng .ToString().Contains()
                    return query.AsEnumerable()
                                .Where(x => x.DiemTBHK.ToString().Contains(diem))
                                .ToList();
                }
            }
            catch { return null; }
        }

        public void UpdateDiemQuaTrinh(string maSV, string maMH, float diemCC, float diemHS1, float diemHS2L1, float diemHS2L2, float diemQuaTrinh)
        {
            try
            {
                using (var db = new MyDbContext())
                {
                    var diem = db.Diem.FirstOrDefault(d => d.MaSv == maSV && d.MaMh == maMH);
                    if (diem != null)
                    {
                        diem.DiemCc = diemCC;
                        diem.DiemHs1 = diemHS1;
                        diem.DiemHs2l1 = diemHS2L1;
                        diem.DiemHs2l2 = diemHS2L2;
                        diem.DiemQuaTrinh = diemQuaTrinh;

                        db.SaveChanges();
                        MessageBox.Show("Cập nhật điểm thành công.");
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy điểm để cập nhật.");
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }

        public int LaySoTinChi(string maMH)
        {
            using (var db = new MyDbContext())
            {
                var mh = db.MonHoc.Find(maMH);
                return mh?.SoTc ?? 0;
            }
        }
    }
    public class DiemDTO
    {
        public string MaSV { get; set; }
        public string MaMH { get; set; }
        public string TenMH { get; set; }
        public int SoTC { get; set; }
        public double DiemHocPhan { get; set; }
    }
}