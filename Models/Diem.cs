using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace QuanLySinhVien_Nhom2.Models
{
    public partial class Diem
    {
        public string MaSv { get; set; }
        public string MaMh { get; set; }
        public string TenDn { get; set; }
        public double? DiemCc { get; set; }
        public double? DiemHs1 { get; set; }
        public double? DiemHs2l1 { get; set; }
        public double? DiemHs2l2 { get; set; }
        public double? DiemQuaTrinh { get; set; }
        public double? DiemThi { get; set; }
        public double? DiemHocPhan { get; set; }
        public double? DiemTbhk { get; set; }

        public virtual MonHoc MaMhNavigation { get; set; }
        public virtual SinhVien SinhVien { get; set; }
    }
}
