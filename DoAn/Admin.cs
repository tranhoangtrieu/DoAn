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
    public partial class Admin : Form
    {
        BindingSource FoodList = new BindingSource();
        BindingSource CategoryList = new BindingSource();
        BindingSource AccountList = new BindingSource();
        BindingSource TableList = new BindingSource();
        private bool isButtonOn = true;
        public Account loginAccount;

         


        public Admin()
        {
            
            InitializeComponent();
            datagrv_thucan.DataSource = FoodList;
            datagrv_danhmuc.DataSource = CategoryList;
            datagrv_Account.DataSource = AccountList;
            datagrv_banan.DataSource = TableList;
            loadAccount();
            loadFood();
            AddFoodBinding();
            loadcbCategory(comboBox1);
            loadCategory();
            AddCategoryBinding();
            loadTableFood();
            AddTableFoodBinding();
            AddAcountBinding();
            panel15.Hide();
            button4.Hide();
            button6.Hide();
            button7.Hide();
            button8.Hide();
        }
        void loadAccount()
        {
            string query = "select * from \"Account\"";
            DataProvider dataProvider = new DataProvider();
            AccountList.DataSource = DataProvider.Instance.ExecuteQuery(query);
        }
        
        public void loadFood()
        {
            string query = "select * from \"Food\" order by \"id\"";
             DataTable dt = DataProvider.Instance.ExecuteQuery(query);
             FoodList.DataSource = dt;


        }
 
        void loadCategory()
        {
            string query = "select * from \"FoodCategory\"";
            DataTable dt = DataProvider.Instance.ExecuteQuery(query);
            CategoryList.DataSource = dt;    
        }

        void AddFoodBinding()
        {
            txt_idFood.DataBindings.Add(new Binding("Text",datagrv_thucan.DataSource, "id",true, DataSourceUpdateMode.Never));
            txt_nameFood.DataBindings.Add(new Binding("Text", datagrv_thucan.DataSource, "Name",true, DataSourceUpdateMode.Never));
            txt_price.DataBindings.Add(new Binding("Text", datagrv_thucan.DataSource, "price",true, DataSourceUpdateMode.Never));
        }

        void AddCategoryBinding()
        {
            txt_IdCategory.DataBindings.Add(new Binding("Text", datagrv_danhmuc.DataSource, "id", true, DataSourceUpdateMode.Never));
            txt_nameCategory.DataBindings.Add(new Binding("Text", datagrv_danhmuc.DataSource, "name", true, DataSourceUpdateMode.Never));
        }

        void loadcbCategory(ComboBox cb)
        {
            cb.DataSource = CategoryDAO.Instance.getlistCategory();
            comboBox1.DisplayMember = "Name";
        }
        
        void loadTableFood()
        {
            string query = "select * from \"TableFood\" order by id";
            DataTable dt = DataProvider.Instance.ExecuteQuery(query);
            TableList.DataSource = dt;
        }

        void AddTableFoodBinding()
        {
            txt_idTable.DataBindings.Add(new Binding("Text", datagrv_banan.DataSource, "id", true, DataSourceUpdateMode.Never));
            txt_nameTable.DataBindings.Add(new Binding("Text", datagrv_banan.DataSource, "name", true, DataSourceUpdateMode.Never));

        }

        void AddAcountBinding()
        {
            txt_displayname.DataBindings.Add(new Binding("Text", datagrv_Account.DataSource, "DisplayName", true, DataSourceUpdateMode.Never));
            txt_username.DataBindings.Add(new Binding("Text", datagrv_Account.DataSource, "UserName", true, DataSourceUpdateMode.Never));
            nb_typeAccount.DataBindings.Add(new Binding("Value", datagrv_Account.DataSource, "Type", true, DataSourceUpdateMode.Never));
            txt_password.DataBindings.Add(new Binding("Text", datagrv_Account.DataSource, "PassWord", true, DataSourceUpdateMode.Never));
        }

        void TableBinding()
        {
            txt_idTable.DataBindings.Add(new Binding("Text", datagrv_banan.DataSource, "id", true, DataSourceUpdateMode.Never));
            txt_nameTable.DataBindings.Add(new Binding("Text", datagrv_banan.DataSource, "name", true, DataSourceUpdateMode.Never));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadListBillDate(dateTimePicker1.Value, dateTimePicker2.Value);
            
        }// thông kê

        void LoadListBillDate(DateTime? chekIn, DateTime? chekOut)
        {
            datagrv_doanhthu.DataSource = BillDAO.Instance.GetBillListDate(chekIn, chekOut);

        }

        private void Admin_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView5_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void txt_idFood_TextChanged(object sender , EventArgs e)
        {
             
            try
            {
                if(datagrv_thucan.SelectedCells.Count > 0)
                {
                    int id = (int)datagrv_thucan.SelectedCells[0].OwningRow.Cells["colIdCategory"].Value;

                    Category category = CategoryDAO.Instance.GetID(id);
                    comboBox1.SelectedItem = category;
                    int intdex = -1;
                    int i = 0;
                    foreach (Category item in comboBox1.Items)
                    {
                        if (item.Id == category.Id)
                        {
                            intdex = i; break;
                        }
                        i++;
                    }
                    comboBox1.SelectedIndex = intdex;
                }
                
            }
            catch
            {
                
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            loadFood();
        }

       

        private void button_sua_Click(object sender, EventArgs e)
        {
            if (isButtonOn)
            {
                button8.Show();
            }
            else
            {
                button8.Hide();
            }
            isButtonOn = !isButtonOn;
        }

        //------------------------------------------------------------------------------------------------- 


        // Danh mục món ăn

        private void button6_Click(object sender, EventArgs e)// chức năng tìm kiếm món ăm trong Food
        {
            string Search = textBox_search.Text;
            string query = "SELECT * FROM \"Food\" WHERE unaccent(lower(\"name\")) ILIKE '%"+Search+ "%' ORDER BY \"id\"";
            DataTable dt = DataProvider.Instance.ExecuteQuery(query);
            datagrv_thucan.DataSource = dt;
            textBox_search.Clear();
            if(datagrv_thucan.RowCount < 1)
            {
                query = "SELECT * FROM \"Food\" where \"name\" ILIKE '%" + Search + "%'  ORDER BY \"id\"";
                DataTable newdt = DataProvider.Instance.ExecuteQuery(query);
                datagrv_thucan.DataSource = newdt;
                textBox_search.Clear();
            }
        }


        //------------------------------------------------------------------------------------------------- 

        private void button3_Click(object sender, EventArgs e)// xóa món ăn
        {
            DialogResult result = MessageBox.Show("Bạn có chắc muốn xóa?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if( result == DialogResult.Yes)
            {
                try
                {
                    int id = Convert.ToInt32(txt_idFood.Text);
                    if (FoodDAO.Instance.DeleteFood(id))
                    {
                        MessageBox.Show("Xóa món thành công ");
                        loadFood();

                    }
                    else
                    {
                        MessageBox.Show("Có lỗi khi xóa món");
                    }
                }
                catch
                {
                    MessageBox.Show("Bạn không thể xóa món ăn bởi vì \n Món ăn này đã nằm trong Danh mục doanh thu");
                }
            }
            else
            {
                MessageBox.Show("Không có thay đổi.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }


            
        }
        private void button8_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txt_idFood.Text);
            string name = txt_nameFood.Text;
            int idCategory = (comboBox1.SelectedItem as Category).Id;
            float price = (float.Parse(txt_price.Text));
            if (FoodDAO.Instance.UpdateFood(name, id, price, idCategory))
            {
                MessageBox.Show("Sửa món thành công");
                loadFood();
                button8.Hide();
                isButtonOn = true;
            }
            else
            {
                MessageBox.Show("Có lỗi khi sửa món !");
            }
        }//sửa món ăn

        private void datagrv_thucan_KeyDown(object sender, KeyEventArgs e)// xóa món ăn bằng nút delete
        {
            if (e.KeyCode == Keys.Delete)
            {
                DialogResult result = MessageBox.Show("Bạn có chắc muốn xóa?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        int id = Convert.ToInt32(txt_idFood.Text);
                        if (FoodDAO.Instance.DeleteFood(id))
                        {
                            MessageBox.Show("Xóa món thành công ");
                            loadFood();

                        }
                        else
                        {
                            MessageBox.Show("Có lỗi khi xóa món");
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Bạn không thể xóa món ăn bởi vì \n Món ăn này đã nằm trong Danh mục doanh thu");
                    }
                }
                else
                {
                    MessageBox.Show("Không có thay đổi.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void label16_Click(object sender, EventArgs e)
        {

        }
        private void button2_Click(object sender, EventArgs e)
        {
            int count = datagrv_thucan.RowCount;
            AddFood food = new AddFood(count);

            food.Show();


            


        }// Thêm món ăn (trên Form AddFood )


        //-------------------------------------------------------------------------------------------------

        // Danh mục Tài khoản

        private void btn_addAccount_Click(object sender, EventArgs e)
        {
            if (isButtonOn)
            {
                button4.Show();
                txt_username.Clear();
                txt_displayname.Clear();
                txt_password.Clear();

            }
            else
            {
                button4.Hide();
            }
            isButtonOn = !isButtonOn;
           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string UserName = txt_username.Text;
            string DisplayName = txt_displayname.Text;
            string PassWord = txt_password.Text;
            int Type = (int)nb_typeAccount.Value;
            if (AccountDAO.Instance.InsertAccount(DisplayName, UserName, PassWord, Type))
            {
                MessageBox.Show("Thêm tài khoản thành công");
                loadAccount();
                button4.Hide();
                isButtonOn = true;
            }
            else
            {
                MessageBox.Show("Có lỗi khi thêm tài khoản");
            }

            //-------------------------------------------------------------------------------------------------

        }// thêm tài khoản

        private void btn_deleteAccont_Click(object sender, EventArgs e)
        {
            string UserName = txt_username.Text;
            DialogResult result = MessageBox.Show("Bạn có chắc muốn xóa?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                if (loginAccount.UserName1.Equals(UserName))
                {
                    MessageBox.Show("Bạn không thể xóa tài khoản đang sủ dụng");
                    return;
                }

                if (AccountDAO.Instance.DeleteAccount(UserName))
                {
                    MessageBox.Show("Xóa tài khoản thành công");
                    loadAccount();
                }
                else
                {
                    MessageBox.Show("Có lỗi khi xóa tài khoản");
                }

            }
            else
            {
                MessageBox.Show("Không có thay đổi.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            
            
        }// xóa tài khoản

        private void btn_updateAccount_Click(object sender, EventArgs e)
        {
            if (isButtonOn)
            {
                button6.Show();
                txt_username.Enabled = false;

            }
            else
            {
                button6.Hide();
                txt_username.Enabled = true;
            }
            isButtonOn = !isButtonOn;


        }
        private void button6_Click_1(object sender, EventArgs e)
        {
            string UserName = txt_username.Text;
            string DisplayName = txt_displayname.Text;
            string PassWord = txt_password.Text;
            int Type = (int)nb_typeAccount.Value;

            txt_username.Enabled = true;
            if (AccountDAO.Instance.UpdateAccount(DisplayName, UserName, PassWord, Type))
            {
                MessageBox.Show("Sửa tài khoản thành công");
                loadAccount();
                button6.Hide();
                isButtonOn = true;
            }
            else
            {
                MessageBox.Show("Có lỗi khi sửa tài khoản");
            }
        }// sửa thông tin tài khoản
        private void datagrv_Account_KeyDown(object sender, KeyEventArgs e)// xóa tài khoản bằng nút delete
        {
            if(e.KeyCode == Keys.Delete)
            {
                string UserName = txt_username.Text;
                DialogResult result = MessageBox.Show("Bạn có chắc muốn xóa?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    if (loginAccount.UserName1.Equals(UserName))
                    {
                        MessageBox.Show("Bạn không thể xóa tài khoản đang sủ dụng");
                        return;
                    }

                    if (AccountDAO.Instance.DeleteAccount(UserName))
                    {
                        MessageBox.Show("Xóa tài khoản thành công");
                        loadAccount();
                    }
                    else
                    {
                        MessageBox.Show("Có lỗi khi xóa tài khoản");
                    }

                }
                else
                {
                    MessageBox.Show("Không có thay đổi.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }


            }

        }

        //-------------------------------------------------------------------------------------------------


        // Danh mục bàn ăn

        private void button14_Click(object sender, EventArgs e)
        {
            if(datagrv_banan.RowCount < 1)
            {
                TableDAO.Instance.FirstInsertTable();
                MessageBox.Show("Thêm bàn thành công");
                loadTableFood();
                return;

            }
            
            
            if (TableDAO.Instance.InsertTable())
            {
                MessageBox.Show("Thêm bàn thành công");
                loadTableFood();
            }
        }// chức năng thêm bàn
        private void button13_Click(object sender, EventArgs e)
        {

            try
            {
                int id = Convert.ToInt32(txt_idTable.Text);
                if (TableDAO.Instance.DeleteTable(id))
                {
                    MessageBox.Show("Xóa bàn thành công");
                    loadTableFood();
                }
            }
            catch { MessageBox.Show("      Bạn không thể xóa bàn này \n Bởi vì bàn này đã nằm trong Danh mục doanh thu"); }

        }// xóa bàn thành công
        private void datagrv_banan_KeyDown(object sender, KeyEventArgs e)// xóa món ăn nút delete
        {
            if(e.KeyCode == Keys.Delete)
            {
                try
                {
                    int id = Convert.ToInt32(txt_idTable.Text);
                    if (TableDAO.Instance.DeleteTable(id))
                    {
                        MessageBox.Show("Xóa bàn thành công");
                        loadTableFood();
                    }
                }
                catch { MessageBox.Show("      Bạn không thể xóa bàn này \n Bởi vì bàn này đã nằm trong Danh mục doanh thu"); }
            }
        }



        //-------------------------------------------------------------------------------------------------

        //Danh mục loại đồ ăn
        private void btb_deleteCategory_Click(object sender, EventArgs e)// xóa loại đồ ăn
        {

            int id = Convert.ToInt32(txt_IdCategory.Text);
            DialogResult result = MessageBox.Show("Bạn có chắc muốn xóa?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(result == DialogResult.Yes)
            {
                try
                {
                    if (CategoryDAO.Instance.DeleteCategory(id))
                    {
                        MessageBox.Show("Xóa loại đồ ăn thành công");
                        loadCategory();
                        loadcbCategory(comboBox1);


                    }
                    else
                    {
                        MessageBox.Show("Có lỗi khi xóa loại đồ ăn");
                    }
                }
                catch {
                    MessageBox.Show("                   Bạn không thể xóa loại đồ ăn này \n Bởi vì món ăn của loại đồ ăn đã nằm trong Danh mục doanh thu");
                }
            }
            else
            {
                MessageBox.Show("Không có thay đổi.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }



        }
        private void button7_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txt_IdCategory.Text);
            string name = txt_nameCategory.Text;
            if (CategoryDAO.Instance.UpdateCategory(id, name))
            {
                MessageBox.Show("Sửa loại đồ ăn thành công");
                loadCategory();
                loadcbCategory(comboBox1);
                button7.Hide();
                isButtonOn = true;

            }
            else
            {
                MessageBox.Show("Có lỗi khi Sửa loại đồ ăn");
            }
        }// sửa loại đồ ăn
        private void button3_Click_1(object sender, EventArgs e)// thêm loại đồ ăn
        {
            string name = textBox1.Text;

            if (datagrv_danhmuc.RowCount < 1)
            {
                CategoryDAO.Instance.FirstInsertCategory(name);
                MessageBox.Show("Thêm loại đồ ăn thành công");
                loadCategory();
                loadcbCategory(comboBox1);
                textBox1.Clear();
                panel15.Hide();
                return;
                
            }


            
            if (CategoryDAO.Instance.InsertCategory(name))
            {
                MessageBox.Show("Thêm loại đồ ăn thành công");
                loadCategory();
                loadcbCategory(comboBox1);
                textBox1.Clear();
                panel15.Hide();
                isButtonOn = true;
            }
            else
            {
                MessageBox.Show("Có lỗi khi thêm loại đồ ăn");
            }
        }
        private void button11_Click(object sender, EventArgs e)
        {
            if (isButtonOn)
            {
                panel15.Show();

            }
            else
            {
                panel15.Hide();
            }
            isButtonOn = !isButtonOn;


        }
        private void btn_updateCategory_Click(object sender, EventArgs e)
        {
            if (isButtonOn)
            {
                button7.Show();
            }
            else
            {
                button7.Hide();
            }
            isButtonOn = !isButtonOn;

        }

        private void datagrv_danhmuc_KeyDown(object sender, KeyEventArgs e)// xóa loại đồ ăn bằng nút delete
        {
            if(e.KeyCode == Keys.Delete) 
            {
                int id = Convert.ToInt32(txt_IdCategory.Text);
                DialogResult result = MessageBox.Show("Bạn có chắc muốn xóa?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        if (CategoryDAO.Instance.DeleteCategory(id))
                        {
                            MessageBox.Show("Xóa loại đồ ăn thành công");
                            loadCategory();
                            loadcbCategory(comboBox1);


                        }
                        else
                        {
                            MessageBox.Show("Có lỗi khi xóa loại đồ ăn");
                        }
                    }
                    catch
                    {
                        MessageBox.Show("                   Bạn không thể xóa loại đồ ăn này \n Bởi vì món ăn của loại đồ ăn đã nằm trong Danh mục doanh thu");
                    }
                }
                else
                {
                    MessageBox.Show("Không có thay đổi.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
                else
                {
                    MessageBox.Show("Không có thay đổi.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }

        }

        





        //-------------------------------------------------------------------------------------------------



    }

