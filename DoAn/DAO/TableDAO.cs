using DoAn.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn.DAO
{
    public class TableDAO
    {
        private static TableDAO instance;
        public static TableDAO Instance
        {
            get { if (TableDAO.instance == null) instance = new TableDAO(); return TableDAO.instance; }
            private set { TableDAO.instance = value; }

        }


        public static int chieudai = 70;
        public static int chieurong = 70;

        private TableDAO() { }
        public List<Table> LoadTableList()
        {
            List<Table> tableList = new List<Table>();

            DataTable dataTable = DataProvider.Instance.ExecuteQuery("select * from public.\"TableFood\" order by \"id\" ");
            foreach (DataRow item in dataTable.Rows)
            {
                Table table = new Table(item);
                tableList.Add(table);
            }
            return tableList;
        }

        public bool FirstInsertTable()
        {
            string query = "INSERT INTO \"TableFood\" (\"name\", \"id\") VALUES ('Bàn 1',1)";
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }

        public bool InsertTable()
        {
            string query = "INSERT INTO \"TableFood\" (\"name\", \"id\") SELECT 'Bàn ' || (max(\"id\")), (max(\"id\") + 1) FROM \"TableFood\"";
            int result =  DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }
        public bool DeleteTable(int id)
        {
            string query = "DELETE FROM \"TableFood\" WHERE \"id\"=" + id;
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }
    }
}

