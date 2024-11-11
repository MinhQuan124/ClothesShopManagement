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
    public partial class EditingWarehouse : Form
    {
        public ModelClass.WareHouse WareHouse { get; set; }
        public EditingWarehouse(ModelClass.WareHouse warehouse)
        {
            InitializeComponent();
            WareHouse = warehouse;

            // Populate the textboxes with the current warehouse data
            textBox1.Text = WareHouse.Warehouse_Id.ToString(); // WarehouseId is likely an INT
            textBox2.Text = WareHouse.Name;
            textBox3.Text = WareHouse.Address;
            textBox4.Text = WareHouse.Phone;
            textBox5.Text = WareHouse.Email;
            textBox6.Text = WareHouse.Stock.ToString(); // Assuming Stock is an INT

            // Make Warehouse_Id textbox read-only (since it's auto-generated)
            textBox1.ReadOnly = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Update the Warehouse object with values from the textboxes
            WareHouse.Name = textBox2.Text;
            WareHouse.Address = textBox3.Text;
            WareHouse.Phone = textBox4.Text;
            WareHouse.Email = textBox5.Text;
            WareHouse.Stock = Convert.ToInt32(textBox6.Text); // Assuming Stock is an INT

            // SQL query to update the warehouse in the database
            string sql = "UPDATE ClothesShopManagement.dbo.Warehouse SET Name = @Name, Address = @Address, Phone = @Phone, Email = @Email, Stock = @Stock WHERE Warehouse_Id = @WarehouseId";

            // SQL parameters
            SqlParameter[] parameters = new SqlParameter[]
            {
            new SqlParameter("@Name", WareHouse.Name),
            new SqlParameter("@Address", WareHouse.Address),
            new SqlParameter("@Phone", WareHouse.Phone),
            new SqlParameter("@Email", WareHouse.Email),
            new SqlParameter("@Stock", WareHouse.Stock),
            new SqlParameter("@WarehouseId", WareHouse.Warehouse_Id) // Pass the WarehouseId in the WHERE clause
            };

            // Execute the query
            int rowsAffected = CRUD_Data.ExecuteNonQuery(sql, parameters);

            // Check if the update was successful
            if (rowsAffected > 0)
            {
                MessageBox.Show("Warehouse updated successfully!");
                this.DialogResult = DialogResult.OK; // Indicate success and close the form
                this.Close();
            }
            else
            {
                MessageBox.Show("Error updating warehouse. Please try again.");
            }
        }
    }
}
