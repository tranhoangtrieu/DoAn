using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn.DAO
{
    public class DataProvider
    {
       
        
            private static DataProvider instance;

            private string connetionSTR = "Host=localhost;Port=5432;Username=postgres;Password=12345;Database=QuanLyQuanNuoc";


            public static DataProvider Instance
            {
                get { if (instance == null) instance = new DataProvider(); return instance; }
                private set { instance = value; }
            }
            public DataTable ExecuteQuery(string query, object[] parameters = null) // phương thức sẽ trả về là 1 table 
            {
                DataTable data = new DataTable();

                using (NpgsqlConnection connection = new NpgsqlConnection(connetionSTR))
                {
                    connection.Open();

                    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                    {
                        if (parameters != null)
                        {
                            // Sử dụng NpgsqlParameter thay vì AddWithValue để đảm bảo đúng kiểu dữ liệu và tránh lỗi chuyển đổi dữ liệu
                            for (int i = 0; i < parameters.Length; i++)
                            {
                                command.Parameters.AddWithValue($"@param{i}", parameters[i]);
                            }
                        }

                        using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command))
                        {
                            adapter.Fill(data);
                        }
                    }
                }

                return data;
            }

        public int ExecuteNonQuery(string query, object[] parameters = null)// phương thức sẽ trả về là 1 hàng sử dụng khi gặp câu lệnh insert, update,delete
        {
            int affectedRows = 0;

            using (NpgsqlConnection connection = new NpgsqlConnection(connetionSTR))
            {
                connection.Open();

                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    if (parameters != null)
                    {
                        for (int i = 0; i < parameters.Length; i++)
                        {
                            command.Parameters.AddWithValue($"@param{i}", parameters[i]);
                        }
                    }

                    affectedRows = command.ExecuteNonQuery();
                }
                connection.Close();
            }

            return affectedRows;
        }

        public object ExecuteScalar(string query, object[] parameters = null)// phương thức trả về 1 giá trị duy nhất từ câu truy vấn
            {
                object result = null;

                using (NpgsqlConnection connection = new NpgsqlConnection(connetionSTR))
                {
                    connection.Open();

                    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                    {
                        if (parameters != null)
                        {
                            // Sử dụng NpgsqlParameter thay vì AddWithValue để đảm bảo đúng kiểu dữ liệu và tránh lỗi chuyển đổi dữ liệu
                            for (int i = 0; i < parameters.Length; i++)
                            {
                                command.Parameters.AddWithValue($"@param{i}", parameters[i]);
                            }
                        }

                        result = command.ExecuteScalar();
                    }
                }

                return result;
            }
        }
    }

