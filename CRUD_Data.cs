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
        private static string sqlPath = "Data Source=DINHQUAN1243\\SQLEXPRESS;Initial Catalog=ClothesShopManagement;Integrated Security=True";

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
    }
}
