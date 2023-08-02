using DoAn.DAO;
using DoAn.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAn
{
    public partial class DangNhap : Form
    {
        public DangNhap()
        {
            InitializeComponent();
        }

        
        private void button1_Click(object sender, EventArgs e)
        {
             string userName = this.txt_taikhoan.Text;
            string passWord = this.txt_matkhau.Text;
            if(userName =="" || passWord == "")
            {
                MessageBox.Show("Chưa nhập tài khoản hoặc mật khẩu");
            }
            else 
            
            if (Login(userName, passWord))
            {   
                Account login = AccountDAO.Instance.GetAccount(userName);
                QuanLy f = new QuanLy(login);  // Chuyền thông tin tài khoản form quanly
                this.Hide();
                f.ShowDialog();
                this.Show();

            }
            else
            {
                MessageBox.Show("Sai tên tài khoản hoặc mật khẩu !");
            }

        }
        bool Login(string userName, string PassWord)
        {
            return AccountDAO.Instance.Login(userName, PassWord);
        }

        private void txt_taikhoan_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void DangNhap_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(MessageBox.Show("Bạn có thực sự muốn thoát ?", " Thông báo", MessageBoxButtons.OKCancel) != System.Windows.Forms.DialogResult.OK)
            {
                e.Cancel = true;
            }
        }

        private void button_Thoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
