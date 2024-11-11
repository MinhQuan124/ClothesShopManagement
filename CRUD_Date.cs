using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace ClothesShopManagement
{
    static class CRUD_Data
    {
        private static string sqlPath = "Data Source=LAPTOP-L8K1U12M\\QUANDOAN;Initial Catalog=ClothesShopManagement;Integrated Security=True";

        //Ham tra ve ket noi
        public static SqlConnection Connection()
        {
            return new SqlConnection(sqlPath);
        }

        //Ham lay du lieu do vao DataSource cua DataGridView
        public static DataTable GetData(string sql)
        {
            // Mo chuoi ket noi
            SqlConnection conn = Connection();
            conn.Open();

            // tao doi tuong adapter de adapt du lieu
            SqlDataAdapter adapt = new SqlDataAdapter(sql, conn);

            //Khoi tao 1 du lieu bang
            DataTable table = new DataTable();
            //adapt du lieu vao bang
            adapt.Fill(table);

            //ngat ket noi sqlConnnection
            conn.Close();
            //Huy bo doi tuong get du lieu sau khi adapt
            adapt.Dispose();
            //Tra ve du lieu bang
            return table;
        }

        //CUD - Create Update Delete
        public static void CUD(string sql)
        {
            //Mo chuoi ket noi
            SqlConnection conn = Connection();
            conn.Open();

            //Thuc hien cau lenh
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.ExecuteNonQuery();

            //dong ket noi
            conn.Close();
            //Huy cau lenh sql
            cmd.Dispose();
        }

        //Phuong thuc do data vao datatable (cach 2)
        public static DataTable GetDataWithParameter(string sql, SqlParameter parameter)
        {
            SqlConnection conn = Connection();
            conn.Open();

            // Create SqlCommand with parameter
            SqlCommand command = new SqlCommand(sql, conn);
            command.Parameters.Add(parameter);

            SqlDataAdapter adapt = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapt.Fill(table);

            conn.Close();
            adapt.Dispose();

            return table;
        }

        public static int ExecuteNonQuery(string sql, SqlParameter[] parameters)
        {
            using (SqlConnection conn = Connection())
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand(sql, conn))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }
                    return command.ExecuteNonQuery();
                }
            }

        }
        public static DataTable GetDataWithParameter(string query, SqlParameter[] parameters)
        {
            // Tạo đối tượng kết nối
            using (SqlConnection conn = new SqlConnection(sqlPath))
            {
                // Tạo đối tượng SqlCommand và chỉ định câu truy vấn và kết nối
                SqlCommand cmd = new SqlCommand(query, conn);

                // Thêm các tham số vào câu lệnh SQL
                if (parameters != null)
                {
                    cmd.Parameters.AddRange(parameters);
                }

                // Tạo một đối tượng DataAdapter để thực thi câu lệnh và nạp dữ liệu vào DataTable
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();             
                conn.Open();
                da.Fill(dt);
                return dt;
            }
        }
           
        public static object ExecuteScalar(string sql, SqlParameter[] parameters = null)
        {
            using (SqlConnection conn = Connection())
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand(sql, conn))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    return command.ExecuteScalar();
                }
            }
        }
       


    }
}
