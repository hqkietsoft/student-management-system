using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Nhom2_QuanLySinhVien.Model;

namespace Nhom2_QuanLySinhVien
{
    public partial class frm_XoaSinhVien_Huyen : Form
    {
        private readonly SinhVienService svXoa;
        private string HoDem, Ten, NgaySinh, GioiTinh, QueQuan, MaLop,SoDT;
        int no0Column = 0;
        
        public frm_XoaSinhVien_Huyen()
        {
            InitializeComponent();
            svXoa = SinhVienService.SV;
            
            svXoa.ShowOnComboBox(cb_MaLop);
            svXoa.ShowOnComboBox1(cb_TenDN);

            txt_MaSV.Text = SinhVienService.SinhVienDangChon.MaSv;
            txt_HoDem.Text = SinhVienService.SinhVienDangChon.HoDem;
            txt_Ten.Text = SinhVienService.SinhVienDangChon.Ten;
            dateNgaySinh.Text = SinhVienService.SinhVienDangChon.NgaySinh.ToString();
            txt_QueQuan.Text = SinhVienService.SinhVienDangChon.QueQuan;
            txt_SoDT.Text = SinhVienService.SinhVienDangChon.SoDt;
            cb_MaLop.SelectedItem = SinhVienService.SinhVienDangChon.MaLop;
            cb_TenDN.SelectedItem = SinhVienService.SinhVienDangChon.TenDn;

            HoDem = SinhVienService.SinhVienDangChon.HoDem;
            Ten = SinhVienService.SinhVienDangChon.Ten;
            NgaySinh = SinhVienService.SinhVienDangChon.NgaySinh.ToString();
            QueQuan = SinhVienService.SinhVienDangChon.QueQuan;
            SoDT = SinhVienService.SinhVienDangChon.SoDt;
            MaLop = SinhVienService.SinhVienDangChon.MaLop;

            if (SinhVienService.SinhVienDangChon.GioiTinh == "Nam")
            {
                rb_Nam.Checked = true;
            }
            else
            {
                rb_Nu.Checked = true;
            }
        }

        private void btn_Huy_Click(object sender, EventArgs e)
        {

            DialogResult dialog = MessageBox.Show("Bạn chắc chắn muốn thoát?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void btn_Xoa_Click(object sender, EventArgs e)
        {
            DialogResult ans = MessageBox.Show($"Bạn có muốn xóa sinh viên {cb_TenDN.Text}", "Xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (ans == DialogResult.Yes)
            {
                if (string.IsNullOrEmpty(txt_MaSV.Text) || string.IsNullOrEmpty(txt_HoDem.Text) ||
                    string.IsNullOrEmpty(txt_Ten.Text) || string.IsNullOrEmpty(dateNgaySinh.Value.ToString()) ||
                    string.IsNullOrEmpty(txt_QueQuan.Text) || string.IsNullOrEmpty(txt_SoDT.Text) ||
                    string.IsNullOrEmpty(cb_MaLop.Text) || string.IsNullOrEmpty(cb_TenDN.Text))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    svXoa.Xoa(txt_MaSV.Text, cb_TenDN.Text);
                    this.Close();
                }
            }
        }
        
    }
}
