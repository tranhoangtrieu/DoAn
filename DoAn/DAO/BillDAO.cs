using DoAn.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn.DAO
{
    public class BillDAO
    {
        private static BillDAO instance;


        public static BillDAO Instance
        {
            get { if (instance == null) instance = new BillDAO(); return BillDAO.instance; }
            private set { BillDAO.instance = value; }
        }
        public int GetBillByTableID(int id)
        {
            DataTable data = DataProvider.Instance.ExecuteQuery("select * from \"Bill\" where status =0 and \"idTable\" ="+id);
            if (data.Rows.Count > 0)
            {
                Bill bill = new Bill(data.Rows[0]);
                return bill.Id;
            }
            return -1;
        }
        public void insertBill(int id)
        {
            DataProvider.Instance.ExecuteNonQuery("call public.usp_insertbill("+ id+")");
        }




        public int GetMaxBill()
        {
            try
            {
                return (int)DataProvider.Instance.ExecuteScalar("select MAX(id) from \"Bill\"");
            }
            catch
            {
                return -1;
            }
        }
        // select MAX(id) from "Bill"
        // UPDATE "Bill" SET status = 1, DateCheckOut = CURRENT_TIMESTAMP,   discount = 20,  TotalPrice = 10000  WHERE id = 3;

        public void checkout(int id, int discount, float totalprice)
        {
            
            string query = ("UPDATE \"Bill\" SET \"status\" = 1,\"datecheckout\" = CURRENT_DATE,\"discount\" =" + discount+ " ,\"totalprice\" = " + totalprice+"  WHERE \"id\" = "+id+";\r\n");

            DataProvider.Instance.ExecuteNonQuery(query);
        }
        
        public DataTable GetBillListDate(DateTime? checkIn, DateTime? checkOut)
        {
            return DataProvider.Instance.ExecuteQuery(" select \"name\" as \"Tên bàn\",\"datecheckin\" as \"ngày nhận bàn\", \"datecheckout\" as \"ngày thanh toán\", \"totalprice\" as \"Tổng tiền\", \"discount\" as \"giảm giá\" from USP_GetListBillDate('" + checkIn+"','"+checkOut+"')");

        }

        


    }
}
