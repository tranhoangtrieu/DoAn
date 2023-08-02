using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn.DAO
{
    public class MenuDAO
    {
        public static MenuDAO instance;
        public static MenuDAO Instance
        {
            get { if (MenuDAO.instance == null) instance = new MenuDAO(); return MenuDAO.instance; }
            private set { MenuDAO.instance = value; }
        }
        public MenuDAO()
        {
        }
        public List<DTO.Menu> GetListMenu(int id)
        {
            List<DTO.Menu> list = new List<DTO.Menu>();
            string query = ("SELECT f.name, bi.count, f.price, f.price*bi.count AS \"totalPrice\"\r\nFROM public.\"BillInfo\" AS bi\r\nINNER JOIN public.\"Bill\" AS b ON bi.\"idBill\" = b.\"id\"\r\nINNER JOIN public.\"Food\" AS f ON bi.\"idFood\" = f.\"id\"\r\nWHERE b.\"status\" = 0 AND b.\"idTable\" ="+id);
            DataTable dataTable = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow dr in dataTable.Rows)
            {
                DTO.Menu memu = new DTO.Menu(dr);
                list.Add(memu);
            }
            return list;
        }
    }
}
