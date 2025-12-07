using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using QuanLySinhVien_Nhom2.Models;

namespace Nhom2_QuanLySinhVien.Services
{
    public class AuthService
    {
        private static AuthService instance;

        // --- CẦU NỐI DỮ LIỆU (Giữ tương thích Form cũ) ---
        // Dùng biến tĩnh để lưu thông tin người đang đăng nhập
        public static NguoiDung CurrentUser { get; set; } = new NguoiDung();

        public string Username { get => CurrentUser.TenDn; set => CurrentUser.TenDn = value; }
        public string Password { get => CurrentUser.MatKhau; set => CurrentUser.MatKhau = value; }

        // Fix lỗi CS0019: Ép kiểu an toàn cho LoaiND
        public string LoaiND
        {
            get => CurrentUser.LoaiNd.ToString();
            set
            {
                if (int.TryParse(value, out int v)) CurrentUser.LoaiNd = v;
            }
        }

        //private AuthService() { }

        public static AuthService NguoiDung
        {
            get
            {
                if (instance == null)
                    instance = new AuthService();
                return instance;
            }
        }

        // --- CÁC HÀM XỬ LÝ (EF CORE) ---

        public int checkLogin(string tendn, string matkhau)
        {
            try
            {
                using (var db = new MyDbContext())
                {
                    var user = db.NguoiDung.FirstOrDefault(u => u.TenDn == tendn && u.MatKhau == matkhau);
                    if (user != null)
                    {
                        // Fix lỗi CS0019 (Operator ??): 
                        // Nếu trong Model 'LoaiNd' là int (không phải int?) thì không cần ?? 0
                        // Nếu là int? thì dùng ?? 0. Ở đây mình viết an toàn nhất:
                        Program.loaiND = user.LoaiNd;

                        // Lưu lại thông tin người dùng hiện tại để dùng cho các form khác
                        CurrentUser = user;
                        return Program.loaiND;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi đăng nhập: " + ex.Message);
            }
            return 0;
        }

        public void UserInfo(int loaind, TextBox username, TextBox password, TextBox TypeOfUser)
        {
            try
            {
                // Logic mới: Hiển thị thông tin người VỪA ĐĂNG NHẬP (CurrentUser)
                // Thay vì query lại DB dựa trên loaiND (logic cũ dễ sai nếu có nhiều user cùng quyền)
                if (CurrentUser != null && !string.IsNullOrEmpty(CurrentUser.TenDn))
                {
                    username.Text = CurrentUser.TenDn;
                    password.Text = CurrentUser.MatKhau;
                    TypeOfUser.Text = CurrentUser.LoaiNd.ToString();
                }
                else
                {
                    // Fallback: Tìm trong DB nếu CurrentUser rỗng
                    using (var db = new MyDbContext())
                    {
                        var u = db.NguoiDung.FirstOrDefault(x => x.LoaiNd == loaind);
                        if (u != null)
                        {
                            username.Text = u.TenDn;
                            password.Text = u.MatKhau;
                            TypeOfUser.Text = u.LoaiNd.ToString();
                        }
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Chưa có thông tin đăng nhập.");
            }
        }

        public void ChagePassword(string user, string pass)
        {
            try
            {
                using (var db = new MyDbContext())
                {
                    var u = db.NguoiDung.Find(user);
                    if (u != null)
                    {
                        u.MatKhau = pass;
                        db.SaveChanges();
                        MessageBox.Show("Đổi mật khẩu thành công!", "Thông báo");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi đổi mật khẩu: " + ex.Message);
            }
        }

        // Hàm hỗ trợ Form Quản lý người dùng
        public void ShowOnComboBox(ComboBox cbo)
        {
            cbo.Items.Clear();
            using (var db = new MyDbContext())
            {
                var list = db.NguoiDung.ToList();
                foreach (var item in list) cbo.Items.Add(item.TenDn);
            }
        }

        public void GetAllByUsername(ComboBox user, TextBox tb)
        {
            using (var db = new MyDbContext())
            {
                var u = db.NguoiDung.Find(user.Text);
                if (u != null) tb.Text = u.MatKhau;
            }
        }
    }
}