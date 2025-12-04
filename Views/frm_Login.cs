using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace Nhom2_QuanLySinhVien
{
    public partial class frm_Login : Form
    {
        Login login;
        public frm_Login()
        {
            InitializeComponent();
            login = new Login();
            label3.Visible = false;
            this.AcceptButton = btn_Login;
        }

        private void frm_Login_Load(object sender, EventArgs e)
        {

        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void guna2TextBox2_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void sw_HideShow_CheckedChanged(object sender, EventArgs e)
        {
            txtPass.UseSystemPasswordChar = !sw_HideShow.Checked;
            if (sw_HideShow.Checked)
            {
                txtPass.UseSystemPasswordChar = false;
                txtPass.PasswordChar = '\0';
            }
            else
            {
                txtPass.UseSystemPasswordChar = true;
                txtPass.PasswordChar = '●';
            }
        }

        private void sw_HideShow_Click(object sender, EventArgs e)
        {
        }

        private void btn_Login_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUser.Text.Trim()))
            {
                label3.Text = "Bạn chưa nhập tên đăng nhập!";
                label3.Visible = true;
                label3.Location = new Point(497,293);
                txtUser.Focus();
            }
            else if (string.IsNullOrEmpty(txtPass.Text.Trim()))
            {
                label3.Text = "Bạn chưa nhập mật khẩu!";
                label3.Visible = true;
                label3.Location = new Point(514, 293);
                txtPass.Focus();
            }
            else if (login.checkLogin(txtUser.Text, txtPass.Text) == 0)
            {
                label3.Text = "Tên đăng nhập và mật khẩu không đúng!";
                label3.Location = new Point(470, 293);
                label3.Visible = true;
            }
            else
            {
                label3.Visible = false;
                TrangChu f = new TrangChu();
                this.Hide();
                f.ShowDialog();
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("Liên hệ Admin để cấp lại mật khẩu!", "Quên mật khẩu", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
