using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace QuanLySinhVien_Nhom2.Models
{
    public partial class NguoiDung
    {
        public NguoiDung()
        {
            GiaoVien = new HashSet<GiaoVien>();
            SinhVien = new HashSet<SinhVien>();
        }

        public string TenDn { get; set; }
        public string MatKhau { get; set; }
        public int LoaiNd { get; set; }

        public virtual ICollection<GiaoVien> GiaoVien { get; set; }
        public virtual ICollection<SinhVien> SinhVien { get; set; }
    }
}
