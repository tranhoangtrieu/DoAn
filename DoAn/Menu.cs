using DoAn.DAO;
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
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
            loadMenu();
        }

        void loadMenu()
        {
            string query = "select a.\"id\" as \"Mã\",a.\"name\" as \"Tên đồ ăn\", b.\"name\" as \"Loại đồ ăn\" , \"price\" as \"giá\" from \"Food\" a,\"FoodCategory\" b\r\nwhere \"idCategory\" = b.id";
            DataTable dt =DataProvider.Instance.ExecuteQuery(query);
            dataGridView1.DataSource = dt;
        }
    }
}
