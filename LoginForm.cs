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

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text; 
            string password = txtPassword.Text; 

            if (AuthenticateUser(username, password, out bool isAdmin))
            {
                HomeForm homeForm = new HomeForm(isAdmin,"quan");
                homeForm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Sai tài khoản hoặc mật khẩu. Vui lòng thử lại.");
            }
        }
        private bool AuthenticateUser(string username, string password, out bool isAdmin)
        {
            isAdmin = false;
            string sql = "SELECT RoleId FROM Staff WHERE Username = @Username AND Password = @Password";
            string sqlgetName = "SELECT Name FROM Staff WHERE Username = @Username AND Password = @Password";
            try
            {
                using (SqlConnection conn = CRUD_Data.Connection())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@Username", username);
                        cmd.Parameters.AddWithValue("@Password", password);

                        var result = cmd.ExecuteScalar();

                        if (result != null)
                        {
                            int roleId = (int)result; 
                            isAdmin = roleId == 0; 
                            return true; 
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
