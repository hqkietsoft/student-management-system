using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace QuanLySinhVien_Nhom2.Models
{
    public partial class SinhVien
    {
        public SinhVien()
        {
            Diem = new HashSet<Diem>();
        }

        public string MaSv { get; set; }
        public string HoDem { get; set; }
        public string Ten { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string GioiTinh { get; set; }
        public string QueQuan { get; set; }
        public string SoDt { get; set; }
        public string MaLop { get; set; }
        public string TenDn { get; set; }

        public virtual LopHoc MaLopNavigation { get; set; }
        public virtual NguoiDung TenDnNavigation { get; set; }
        public virtual ICollection<Diem> Diem { get; set; }
    }
}
