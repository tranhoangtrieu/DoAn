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
        int countRow;
        public AddFood(int count)
        {
            InitializeComponent();
            countRow = count;
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



            float price;
            if (!float.TryParse(txt_price.Text, out price))
            {
                MessageBox.Show("Bảng giá vui lòng chỉ nhập số");
                return;
            }



            if (countRow < 1)
            {
                FoodDAO.Instance.FirstInsertFood(name, idCategory, price);
                MessageBox.Show(" Thêm món thành công \n Vui lòng ấn \"Xem\" để tải lại danh mục !");
                return;
                this.Close();
            }

            try
            {
                if (FoodDAO.Instance.InsertFood(name, idCategory, price))
                {
                    MessageBox.Show(" Thêm món thành công \n Vui lòng ấn \"Xem\" để tải lại danh mục !");
                    this.Close();
                }
                else
                {
                    MessageBox.Show(" Có lỗi khi thêm thức ăn !");
                    return;
                }
            }
            catch
            {
                MessageBox.Show(" Có lỗi khi thêm thức ăn !");
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
