using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace QuanLySinhVien_Nhom2.Models
{
    public partial class Khoa
    {
        public Khoa()
        {
            GiaoVien = new HashSet<GiaoVien>();
            LopHoc = new HashSet<LopHoc>();
        }

        public string MaKhoa { get; set; }
        public string TenKhoa { get; set; }

        public virtual ICollection<GiaoVien> GiaoVien { get; set; }
        public virtual ICollection<LopHoc> LopHoc { get; set; }
    }
}
