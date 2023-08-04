using DoAn.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn.DAO
{
    public class FoodDAO
    {
        private static FoodDAO instance;
        public static FoodDAO Instance
        {
            get { if (FoodDAO.instance == null) instance = new FoodDAO(); return instance; }
            private set { FoodDAO.instance = value; }
        }

        public FoodDAO() { }
        public List<Food> GetFoodByCategoryID(int id)
        {
            List<Food> list = new List<Food>();
            string query = "select * from \"Food\" where \"idCategory\" = " + id;
            DataTable dataTable = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow row in dataTable.Rows)
            {
                Food food = new Food(row);
                list.Add(food);
            }
            return list;

        }
        public bool FirstInsertFood(string name, int idCategory, float price)
        {

            string query = "INSERT INTO \"Food\"(\"id\",\"name\",\"idCategory\",\"price\") VALUES (1,'"+name+"',"+idCategory+"  ,"+price+" )";
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }



        public bool InsertFood(string name, int idCategory, float  price)
        {
            


            string query = "INSERT INTO \"Food\" (\"id\", \"name\", \"idCategory\", \"price\")\r\nVALUES ((SELECT max(\"id\") FROM \"Food\") + 1, '"+name+"', "+idCategory+", "+price+")";
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }
        public bool DeleteFood(int id)
        {
            string query = "delete from \"Food\" where id=" + id;
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }

        public bool UpdateFood(string name, int id, float price,int idCategory)
        {
            string query = "UPDATE \"Food\" SET \"name\"='" + name + "',\"idCategory\"=" + idCategory + ",\"price\"=" + price + " WHERE \"id\"= " + id;
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }
        

    }
}
