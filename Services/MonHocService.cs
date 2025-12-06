using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using QuanLySinhVien_Nhom2.Models; // Gọi thư mục chứa Model EF Core

namespace Nhom2_QuanLySinhVien.Services // Đổi namespace về Services
{
    public class MonHocService
    {
        private static MonHocService instance;

        // --- CẦU NỐI DỮ LIỆU (Giữ tương thích với code cũ) ---
        // Tạo một biến tạm để hứng dữ liệu từ các property cũ
        public static MonHoc TempData { get; set; } = new MonHoc();

        public string MaMH { get => TempData.MaMh; set => TempData.MaMh = value; }
        public string TenMH { get => TempData.TenMh; set => TempData.TenMh = value; }

        // Code cũ dùng int, Model mới có thể là int hoặc int? (nullable). 
        // Ta ép kiểu an toàn để tránh lỗi.
        public int SoTC
        {
            get => TempData.SoTc ?? 0;
            set => TempData.SoTc = value;
        }

        private MonHocService() { }

        public static MonHocService Subject
        {
            get
            {
                if (instance == null)
                    instance = new MonHocService();
                return instance;
            }
        }

        #region Hiển thị lên DataGridView

        // Đổi kiểu trả về từ DataTable sang object (List)
        public object SelectAllSubject()
        {
            try
            {
                using (var db = new MyDbContext())
                {
                    return db.MonHoc.Select(mh => new
                    {
                        MaMH = mh.MaMh,
                        TenMH = mh.TenMh,
                        SoTC = mh.SoTc
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hiển thị: " + ex.Message);
                return null;
            }
        }

        public void ShowDataOnTable(DataGridView dgv)
        {
            dgv.DataSource = SelectAllSubject();
        }
        #endregion

        // --- CÁC HÀM HỖ TRỢ LOGIC CŨ ---

        // Hàm kiểm tra tồn tại (Viết lại bằng LINQ)
        private bool checkExistingByID(string NameOrID)
        {
            using (var db = new MyDbContext())
            {
                return db.MonHoc.Any(x => x.MaMh == NameOrID || x.TenMh == NameOrID);
            }
        }

        // Hàm hỗ trợ điền dữ liệu vào ComboBox/TextBox khi chọn (Logic cũ hơi phức tạp, tôi đã chuyển sang EF)
        public void GetAllByNameOrID(ComboBox nameOrId, ComboBox cbo, TextBox tb, int i, int j)
        {
            try
            {
                using (var db = new MyDbContext())
                {
                    string keyword = nameOrId.Text;
                    var mh = db.MonHoc.FirstOrDefault(x => x.MaMh.Contains(keyword) || x.TenMh.Contains(keyword));

                    if (mh != null)
                    {
                        // Logic cũ dùng index i, j để lấy cột. Ở đây ta map cứng luôn cho an toàn
                        // Giả định: i=0 là Mã, j=1 là Tên (hoặc ngược lại tùy form bạn gọi)
                        // Nếu form lỗi, bạn hãy kiểm tra lại xem i, j đại diện cho cột nào

                        // Cách an toàn nhất: Chỉ lấy dữ liệu đầu tiên tìm thấy
                        if (i == 0) cbo.SelectedItem = mh.MaMh;
                        else cbo.SelectedItem = mh.TenMh;

                        if (j == 1) tb.Text = mh.TenMh;
                        else if (j == 2) tb.Text = mh.SoTc.ToString();
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Lỗi: " + e.Message);
            }
        }

        public void GetAllByID_SuaThongTin(ComboBox ID, TextBox tb, TextBox tb1, int i, int j)
        {
            try
            {
                using (var db = new MyDbContext())
                {
                    var mh = db.MonHoc.Find(ID.Text);
                    if (mh != null)
                    {
                        // Map dữ liệu vào TextBox (Giả định vị trí cột như logic cũ)
                        // Tốt nhất bạn nên sửa Form để gán trực tiếp: txtTen.Text = mh.TenMh
                        // Nhưng để giữ code cũ chạy được, tôi gán tạm thế này:
                        if (i == 1) tb.Text = mh.TenMh;
                        if (j == 2) tb1.Text = mh.SoTc.ToString();
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Lỗi: " + e.Message);
            }
        }

        public void ShowOnComboBox(ComboBox cbo, int i)
        {
            cbo.Items.Clear();
            using (var db = new MyDbContext())
            {
                var list = db.MonHoc.ToList();
                foreach (var item in list)
                {
                    // i = 0 -> Mã, i = 1 -> Tên
                    if (i == 0) cbo.Items.Add(item.MaMh);
                    else if (i == 1) cbo.Items.Add(item.TenMh);
                }
            }
        }

        // --- THÊM - SỬA - XÓA ---

        public void InsertData(TextBox ma, TextBox ten, ComboBox sotc)
        {
            using (var db = new MyDbContext())
            {
                if (db.MonHoc.Any(x => x.MaMh == ma.Text))
                {
                    MessageBox.Show("Đã tồn tại mã môn học", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (db.MonHoc.Any(x => x.TenMh == ten.Text))
                {
                    MessageBox.Show("Đã tồn tại tên môn học", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                try
                {
                    var mh = new MonHoc()
                    {
                        MaMh = ma.Text,
                        TenMh = ten.Text,
                        SoTc = int.Parse(sotc.Text) // Ép kiểu số tín chỉ
                    };

                    db.MonHoc.Add(mh);
                    db.SaveChanges();
                    MessageBox.Show($"Thêm thành công: {ten.Text}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi thêm: " + ex.Message);
                }
            }
        }

        public void DeleleSubjectByID(ComboBox ma, ComboBox ten) // Code cũ bạn truyền ComboBox vào
        {
            using (var db = new MyDbContext())
            {
                try
                {
                    var mh = db.MonHoc.Find(ma.Text);
                    if (mh == null)
                    {
                        MessageBox.Show("Không tồn tại mã môn học này");
                        return;
                    }

                    db.MonHoc.Remove(mh);
                    db.SaveChanges();
                    MessageBox.Show($"Đã xóa môn học: {ten.Text}", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi xóa (Có thể do ràng buộc dữ liệu): " + ex.Message);
                }
            }
        }

        public void UpdateSubjectById(string ma, string ten, string sotc)
        {
            using (var db = new MyDbContext())
            {
                try
                {
                    var mh = db.MonHoc.Find(ma);
                    if (mh != null)
                    {
                        mh.TenMh = ten;
                        mh.SoTc = int.Parse(sotc);
                        db.SaveChanges();
                        MessageBox.Show($"Đã sửa môn học: {ten}", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("Lỗi sửa: " + e.Message);
                }
            }
        }

        // --- TÌM KIẾM & SẮP XẾP ---

        public object SeachingAllByAll(string SearchingInfo)
        {
            using (var db = new MyDbContext())
            {
                // Tìm theo Mã, Tên hoặc Số TC
                return db.MonHoc.Where(x => x.MaMh.Contains(SearchingInfo) ||
                                             x.TenMh.Contains(SearchingInfo) ||
                                             x.SoTc.ToString().Contains(SearchingInfo))
                                 .Select(mh => new {
                                     Mã_MH = mh.MaMh,
                                     Tên_MH = mh.TenMh,
                                     Số_TC = mh.SoTc
                                 })
                                 .ToList();
            }
        }

        public object SortingByIndexOfCombobox(ComboBox info)
        {
            if (info.SelectedIndex < 0 || info.Text.Contains("Chọn thông tin"))
            {
                MessageBox.Show("Vui lòng chọn thông tin sắp xếp", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }

            using (var db = new MyDbContext())
            {
                var query = db.MonHoc.AsQueryable();

                // Logic sắp xếp (0: Mã, 1: Tên, 2: Số TC)
                if (info.SelectedIndex == 0)
                    query = query.OrderBy(x => x.MaMh);
                else if (info.SelectedIndex == 1)
                    query = query.OrderBy(x => x.TenMh);
                else if (info.SelectedIndex == 2)
                    query = query.OrderBy(x => x.SoTc);

                return query.Select(mh => new {
                    Mã_MH = mh.MaMh,
                    Tên_MH = mh.TenMh,
                    Số_TC = mh.SoTc
                }).ToList();
            }
        }
    }
}