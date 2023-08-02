using DoAn.DAO;
using DoAn.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAn
{
    public partial class QuanLy : Form
    {
        private Account loginaccount;

       
        public QuanLy(Account acc)
        {

            InitializeComponent();
            this.Loginaccount = acc;
            LoadTable();
            loadCategory();
            
        }

        // sau khi lấy được thông tin tài khoản từ username đăng nhập  thì sẽ set đưa giá trị type của tài khoản vào ChangeAccount
        public Account Loginaccount
        {
            get { return loginaccount; }
            set { loginaccount = value; ChangeAccount(loginaccount.Type); } 
        }
        void ChangeAccount(int type)
        {
            adminToolStripMenuItem.Enabled = type ==1; // kiem tra nếu như type bằng 1 thì sẽ bật admin còn không thì sẽ tắt
        }


        void loadCategory() // 
        {
            List<Category> list = CategoryDAO.Instance.getlistCategory();
            comboBox_loaidoan.DataSource = list;
            comboBox_loaidoan.DisplayMember = "Name";
         
        }
        void loadDsCategoryId(int id)
        {

            List<Food> foods = FoodDAO.Instance.GetFoodByCategoryID(id);
            comboBox_monan.DataSource = foods;
            comboBox_monan.DisplayMember = "name";

        }


        void LoadTable()
        {
            fLP_banan.Controls.Clear();
            List<Table> tablelist = TableDAO.Instance.LoadTableList();
            foreach (Table table in tablelist)
            {
                System.Windows.Forms.Button btn = new System.Windows.Forms.Button() { Width = TableDAO.chieurong, Height = TableDAO.chieudai };
                btn.Text = table.Name + Environment.NewLine + table.Status;
                btn.Click += Btn_Click;
                btn.Tag = table;


                switch (table.Status)
                {
                    case "Trống":
                        btn.BackColor = Color.White;
                        break;
                    default:
                        btn.BackColor = Color.Red;
                        break;
                }

                fLP_banan.Controls.Add(btn);

            }
        }

        public void showBill(int id)
        {
            lv_showbill.Items.Clear();
            List<DTO.Menu> List = MenuDAO.Instance.GetListMenu(id); // lấy danh sách từ Menu           
            float sum = 0;
            foreach (DTO.Menu item in List)
            {
                ListViewItem listViewItem = new ListViewItem(item.FoodName.ToString());
                listViewItem.SubItems.Add(item.Count.ToString());
                listViewItem.SubItems.Add(item.Price.ToString());
                listViewItem.SubItems.Add(item.TotalPrice.ToString());
                sum += item.TotalPrice;
                lv_showbill.Items.Add(listViewItem);

            }
            
            textBox_giatien.Text = sum.ToString();
        }
        private void Btn_Click(object sender, EventArgs e)
        {
            int tableID = ((sender as System.Windows.Forms.Button).Tag as Table).ID;
            lv_showbill.Tag = (sender as System.Windows.Forms.Button).Tag;
            showBill(tableID);
        }

        private void adminToolStripMenuItem_Click(object sender, EventArgs e)
        {   
            

            Admin f = new Admin();
            f.loginAccount = loginaccount;
            this.Hide();
            f.ShowDialog();
            this.Show();
            LoadTable();

        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            int id = 0;
            ComboBox cb = sender as ComboBox;
            if (cb.SelectedItem == null || !(cb.SelectedItem is Category))
                return;
            Category select = cb.SelectedItem as Category;
            id = select.Id;
            loadDsCategoryId(id);
        }

        private void button1_Click(object sender, EventArgs e)// Chức năng thêm món vào bill
        {

            Table table = lv_showbill.Tag as Table;
            if (table == null)
            {
                MessageBox.Show("Hãy chọn bàn");
                return;
            }
            int idBill = BillDAO.Instance.GetBillByTableID(table.ID);    
            int foodID = (comboBox_monan.SelectedItem as Food).Id;
            int count = (int)numUD_slmon.Value;

            if (lv_showbill.Items.Count == 0)
            {
                if(count < 0)
                {
                    MessageBox.Show("Có lỗi !!");
                    return;
                }
            }

            

            if (idBill == -1)
            {
                BillDAO.Instance.insertBill(table.ID);
                
                BillInfoDAO.Instance.InsertBillInfo(BillDAO.Instance.GetMaxBill(), foodID, count);
            }
            else
            {
                BillInfoDAO.Instance.InsertBillInfo(idBill, foodID, count);

            }

            showBill(table.ID);
            LoadTable();
            numUD_slmon.Value = 1;
        }


        private void button2_Click(object sender, EventArgs e)// chức năng thanh toán
        {
            Table table = lv_showbill.Tag as Table;
            int idBIll = BillDAO.Instance.GetBillByTableID(table.ID);
            int discount = (int)numUD_giamgia.Value;
            double totalPrice = Convert.ToDouble(textBox_giatien.Text);
            double finalTotal = totalPrice - (totalPrice * discount / 100);
            if (idBIll != -1)
            {
                if (discount > 0)
                {
                    if (MessageBox.Show(string.Format("Bạn có chắc muốn thanh toán bàn {0} ?\n Số tiền sau khi giảm giá là {1}", table.Name, finalTotal), "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                    {
                        BillDAO.Instance.checkout(idBIll, discount, (float)finalTotal);
                        showBill(table.ID);

                        LoadTable();
                        numUD_giamgia.Value = 0;
                    }
                }

                else
                {
                    if (MessageBox.Show(string.Format("Bạn có chắc muốn thanh toán bàn {0} ?", table.Name), "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                    {
                        BillDAO.Instance.checkout(idBIll, discount, (float)finalTotal);
                        showBill(table.ID);

                        LoadTable();
                    }

                }



            }
        }

        private void QuanLy_Load_1(object sender, EventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lv_showbill_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void xToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Menu t = new Menu();

            t.Show();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        
    }
}
