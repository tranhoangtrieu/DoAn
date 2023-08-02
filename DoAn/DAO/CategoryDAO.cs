using DoAn.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn.DAO
{
    public class CategoryDAO
    {
        private static CategoryDAO instance;
        public static CategoryDAO Instance
        {
            get { if (CategoryDAO.instance == null) instance = new CategoryDAO(); return instance; }
            private set { CategoryDAO.instance = value; }
        }

        private CategoryDAO() { }

        public List<Category> getlistCategory()
        {
            List<Category> list = new List<Category>();
            string query = "select * from \"FoodCategory\"";
            DataTable dt = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow dr in dt.Rows)
            {
                Category category = new Category(dr);
                list.Add(category);
            }
            return list;
        }

        public Category GetID(int id) 
        {   
            Category category = null;
            string query = " select * from \"FoodCategory\" where id =" + id;
            DataTable dt = DataProvider.Instance.ExecuteQuery(query);
            foreach(DataRow dr in dt.Rows)
            {
                category = new Category(dr);
                return category;
            }
            return category;
        }

        public bool InsertCategory(string name)
        {
            string query = "INSERT INTO \"FoodCategory\" (\"id\",\"name\") VALUES((SELECT max(\"id\") FROM \"FoodCategory\") +1,'"+name+"')";
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
            
        }

        public bool DeleteCategory(int id)
        {
            string query = "DELETE FROM \"FoodCategory\" WHERE id=" + id;
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }
        public bool UpdateCategory(int id, string name)
        {
            string query = "UPDATE \"FoodCategory\" SET \"name\"='"+name+"' WHERE id =" + id;
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }
    }
}
