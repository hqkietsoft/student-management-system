using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace QuanLySinhVien_Nhom2.Models
{
    public partial class LopHocPhan
    {
        public string MaLop { get; set; }
        public string MaMh { get; set; }
        public string MaGv { get; set; }
        public int? HocKy { get; set; }
        public int? Nam { get; set; }

        public virtual MonHoc MaMhNavigation { get; set; }
    }
}
