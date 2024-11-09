using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ClothesShopManagement.Brand
{
    public partial class AddingBrand : Form
    {
        public AddingBrand()
        {
            InitializeComponent();
            textBox1.ReadOnly = true;
            textBox1.Enabled = false; 
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Remove the brandId from this method since it's auto-generated
            string name = textBox2.Text.Trim();     // Assuming textBox2 is for Name
            string address = textBox3.Text.Trim();  // Assuming textBox3 is for Address
            string phoneNumber = textBox4.Text.Trim(); // Assuming textBox4 is for PhoneNumber

            // SQL command to insert a new brand without specifying the Brand_Id
            string sql = "INSERT INTO ClothesShopManagement.dbo.Brand (Name, Address, PhoneNumber) VALUES (@Name, @Address, @PhoneNumber)";

            // Create parameters for the SQL command
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Name", name),
                new SqlParameter("@Address", address),
                new SqlParameter("@PhoneNumber", phoneNumber)
            };

            // Execute the command
            int rowsAffected = CRUD_Data.ExecuteNonQuery(sql, parameters);
            if (rowsAffected > 0)
            {
                MessageBox.Show("Thêm thương hiệu thành công!");
                this.DialogResult = DialogResult.OK; // Set the dialog result to OK
                this.Close(); // Close the form
            }
            else
            {
                MessageBox.Show("Lỗi. Vui lòng thử lại.");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
