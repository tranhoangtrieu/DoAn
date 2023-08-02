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
    public partial class AddFood : Form
    {
        public Admin admin;
        public AddFood()
        {
            InitializeComponent();
            loadCategory();
            
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if(txt_nameFood.Text == "" || txt_price.Text == "")
            {
                MessageBox.Show("Vui lòng không để trống thông tin món ăn");
                return;
            }
            string name = txt_nameFood.Text;
            int idCategory = (comboBox1.SelectedItem as Category).Id;           
            float price = float.Parse(txt_price.Text);
            if (FoodDAO.Instance.InsertFood( name, idCategory, price))
            {
                MessageBox.Show(" Thêm món thành công");
                
            }
            else
            {
                MessageBox.Show(" Có lỗi khi thêm thức ăn !");
                return;
            }
        }
        void loadCategory() // 
        {
            List<Category> list = CategoryDAO.Instance.getlistCategory();
            comboBox1.DataSource = list;
            comboBox1.DisplayMember = "Name";

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
