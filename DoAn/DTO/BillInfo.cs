using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn.DTO
{

    public class BillInfro
    {
        private int ID;
        private int idBill;
        private int idFood;
        private int count;

        public BillInfro(int iD, int idBill, int idFood, int count)
        {
            this.ID = iD;
            this.IdBill = idBill;
            this.IdFood = idFood;
            this.Count = count;

        }
        public BillInfro() { }
        public BillInfro(DataRow row)
        {
            this.ID = (int)row["id"];
            this.idBill = (int)row["idBill"];
            this.idFood = (int)row["idFood"];
            this.count = (int)row["count"];
        }
        public int ID1 { get => ID; set => ID = value; }
        public int IdBill { get => idBill; set => idBill = value; }
        public int IdFood { get => idFood; set => idFood = value; }
        public int Count { get => count; set => count = value; }
    }
}
