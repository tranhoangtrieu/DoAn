using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn.DTO
{
    public class Bill
    {
        private int id;
        private DateTime? datecheckin;
        private DateTime? datecheckout;
        private int status;
        private int discount = 0;
        public Bill(int id, DateTime? datecheckin, DateTime? datecheckout, int status, int discount)
        {
            this.id = id;
            this.datecheckin = datecheckin;
            this.datecheckout = datecheckout;
            this.status = status;
            this.discount = discount;

        }
        public Bill() { }
        public Bill(DataRow row)
        {
            this.id = (int)row["id"];
            this.datecheckin = (DateTime?)row["datecheckin"];
            var datecheckoutTemp = row["datecheckout"];
            if (datecheckoutTemp.ToString() != "")
            {
                this.datecheckout = (DateTime?)datecheckoutTemp;
            }
            this.status = (int)row["status"];
            this.discount = (int)row["discount"];
        }


        public int Id { get => id; set => id = value; }
        public DateTime? Datecheckin { get => datecheckin; set => datecheckin = value; }
        public DateTime? Datecheckout { get => datecheckout; set => datecheckout = value; }
        public int Status { get => status; set => status = value; }
        public int Discount { get => discount; set => discount = value; }
    }
}
