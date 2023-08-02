using DoAn.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn.DAO
{
    public class BillInfoDAO
    {
        public static BillInfoDAO instance;
        public static BillInfoDAO Instance
        {
            get { if (instance == null) instance = new BillInfoDAO(); return BillInfoDAO.instance; }
            private set { BillInfoDAO.instance = value; }
        }
        public BillInfoDAO() { }
        public List<BillInfro> GetListBillInfor(int id)
        {
            List<BillInfro> list = new List<BillInfro>();
            DataTable data = DataProvider.Instance.ExecuteQuery("select * from \"BillInfo\" where \"idBill\"= " + id);
            foreach (DataRow row in data.Rows)
            {
                BillInfro info = new BillInfro(row);
                list.Add(info);
            }

            return list;
        }

        public void InsertBillInfo(int idBill, int idFood, int count)
        {
            DataProvider.Instance.ExecuteNonQuery("call public.usp_insertbillinfo("+ idBill+", "+ idFood + ","+ count+") ") ;
        }
    }
}
