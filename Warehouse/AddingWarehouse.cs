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

namespace ClothesShopManagement.Warehouse
{
    public partial class AddingWarehouse : Form
    {
        public AddingWarehouse()
        {
            InitializeComponent();
            textBox1.ReadOnly = true; 
            textBox1.Enabled = false; // Alternatively, disable the textbox completely
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Retrieve values from the textboxes
            string name = textBox2.Text.Trim();    // Assuming textBox2 is for Warehouse Name
            string address = textBox3.Text.Trim(); // Assuming textBox3 is for Warehouse Address
            string phoneNumber = textBox4.Text.Trim(); // Assuming textBox4 is for Warehouse Phone Number
            string email = textBox5.Text.Trim();

            // Default stock value is 0
            int stock = 0; // Default value for stock

            // Validate that the name, address, phone number, and email are not empty
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(address) || string.IsNullOrEmpty(phoneNumber) || string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Please fill in all the fields.");
                return;
            }

            // Try parsing the stock value entered by the user
            if (!int.TryParse(textBox6.Text.Trim(), out stock))
            {
                // If parsing fails, stock remains 0. You could also prompt the user to enter a valid number.
                MessageBox.Show("Invalid stock value. Stock will be set to 0.");
            }

            // SQL command to insert a new warehouse without specifying Warehouse_Id (auto-generated)
            string sql = "INSERT INTO ClothesShopManagement.dbo.Warehouse (Name, Address, Phone, Email, Stock) " +
                         "VALUES (@Name, @Address, @Phone, @Email, @Stock)";

            // Create parameters for the SQL command
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Name", name),
                new SqlParameter("@Address", address),
                new SqlParameter("@Phone", phoneNumber),
                new SqlParameter("@Email", email),
                new SqlParameter("@Stock", stock)
            };

            try
            {
                // Execute the command
                int rowsAffected = CRUD_Data.ExecuteNonQuery(sql, parameters);
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Warehouse added successfully!");
                    this.DialogResult = DialogResult.OK; // Set the dialog result to OK
                    this.Close(); // Close the form
                }
                else
                {
                    MessageBox.Show("Error adding warehouse. Please try again.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }
    }
}
