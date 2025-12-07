using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace QuanLySinhVien_Nhom2.Models
{
    public partial class GiaoVien
    {
        public string MaGv { get; set; }
        public string HoTen { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string MaKhoa { get; set; }
        public string TenDn { get; set; }

        public virtual Khoa MaKhoaNavigation { get; set; }
        public virtual NguoiDung TenDnNavigation { get; set; }
    }
}
