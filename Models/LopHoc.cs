using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace QuanLySinhVien_Nhom2.Models
{
    public partial class LopHoc
    {
        public LopHoc()
        {
            SinhVien = new HashSet<SinhVien>();
        }

        public string MaLop { get; set; }
        public string TenLop { get; set; }
        public string KhoaHoc { get; set; }
        public string HeDaoTao { get; set; }
        public string NamNhapHoc { get; set; }
        public string MaKhoa { get; set; }

        public virtual Khoa MaKhoaNavigation { get; set; }
        public virtual ICollection<SinhVien> SinhVien { get; set; }
    }
}
