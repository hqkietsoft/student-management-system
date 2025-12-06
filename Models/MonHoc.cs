using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace QuanLySinhVien_Nhom2.Models
{
    public partial class MonHoc
    {
        public MonHoc()
        {
            Diem = new HashSet<Diem>();
            LopHocPhan = new HashSet<LopHocPhan>();
        }

        public string MaMh { get; set; }
        public string TenMh { get; set; }
        public int? SoTc { get; set; }

        public virtual ICollection<Diem> Diem { get; set; }
        public virtual ICollection<LopHocPhan> LopHocPhan { get; set; }
    }
}
