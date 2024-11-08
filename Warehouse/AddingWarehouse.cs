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
            string email = textBox5.Text.Trim();   // Assuming textBox5 is for Warehouse Email
            int stock = 0; // Default value for stock
            if (int.TryParse(textBox6.Text.Trim(), out int result)) // Assuming textBox6 is for Stock
            {
                stock = result; // If the value is valid, assign it to stock
            }
            else
            {
                MessageBox.Show("Please enter a valid stock number.");
                return; // Exit if stock is invalid
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
    }
}
