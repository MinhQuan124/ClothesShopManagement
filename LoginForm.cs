using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClothesShopManagement
{
    public partial class LoginForm : Form
    {
        public string usernameCurrently = "";
        public LoginForm()
        {
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text; 
            string password = txtPassword.Text; 

            if (AuthenticateUser(username, password, out bool isAdmin, out string usernameCurrently,out int staffid))
            {
                HomeForm homeForm = new HomeForm(isAdmin, usernameCurrently,staffid);
                homeForm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Sai tài khoản hoặc mật khẩu. Vui lòng thử lại.");
            }
        }
        private bool AuthenticateUser(string username, string password, out bool isAdmin, out string usernameCurrently,out int staffid)
        {
            isAdmin = false;
            usernameCurrently = string.Empty; // Khởi tạo biến cho tên
            staffid = 0;
            string sql = "SELECT StaffId, RoleId, Name FROM Staff WHERE Username = @Username AND Password = @Password";

            try
            {
                using (SqlConnection conn = CRUD_Data.Connection())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@Username", username);
                        cmd.Parameters.AddWithValue("@Password", password);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int roleId = reader.GetInt32(1);
                                usernameCurrently = reader.GetString(2);
                                isAdmin = roleId == 0;
                                staffid = reader.GetInt32(0);
                                return true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi kết nối cơ sở dữ liệu: {ex.Message}");
            }
            return false;
        }

    }
}
