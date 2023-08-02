using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn.DTO
{
    public class Food
    {
        private string name;
        private int id;
        private int idCategory;
        private float price;

        public Food(string name, int id, int idCategory, float price)
        {
            Name = name;
            Id = id;
            IdCategory = idCategory;
            Price = price;

        }

        public Food(DataRow row)
        {
            this.id = (int)row["id"];
            this.name = row["name"].ToString();
            this.idCategory = (int)row["idCategory"];
            this.price = (float)Convert.ToDouble(row["price"].ToString());
        }
        public Food() { }   
        public string Name { get => name; set => name = value; }
        public int Id { get => id; set => id = value; }
        public int IdCategory { get => idCategory; set => idCategory = value; }
        public float Price { get => price; set => price = value; }
    }
}
