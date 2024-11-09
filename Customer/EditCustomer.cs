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

namespace ClothesShopManagement.Customer
{
    public partial class EditCustomer : Form
    {
        private int customerId;
        public EditCustomer(int customerId)
        {
            InitializeComponent();
            this.customerId = customerId;
            LoadCustomerData();
        }
        private void LoadCustomerData()
        {
            // Query to select the customer by ID
            string query = "SELECT Name, PhoneNumber, Email, CreatedDate FROM Customer WHERE CustomerId = @CustomerId";
            SqlParameter parameter = new SqlParameter("@CustomerId", customerId);

            DataTable customerData = CRUD_Data.GetDataWithParameter(query, parameter);

            if (customerData.Rows.Count > 0)
            {
                // Fill form fields with the customer data
                txtName.Text = customerData.Rows[0]["Name"].ToString();
                txtPhoneNumber.Text = customerData.Rows[0]["PhoneNumber"].ToString();
                txtEmail.Text = customerData.Rows[0]["Email"].ToString();
                dtp_CreateDate.Value = Convert.ToDateTime(customerData.Rows[0]["CreatedDate"]);
            }
        }

        private void EditCustomer_Load(object sender, EventArgs e)
        {

        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            string updateQuery = "UPDATE Customer SET Name = @Name, PhoneNumber = @PhoneNumber, Email = @Email, CreatedDate = @CreatedDate WHERE CustomerId = @CustomerId";

            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@Name", txtName.Text),
        new SqlParameter("@PhoneNumber", txtPhoneNumber.Text),
        new SqlParameter("@Email", txtEmail.Text),
        new SqlParameter("@CreatedDate", dtp_CreateDate.Value),
        new SqlParameter("@CustomerId", customerId)
            };

            // Execute the update query
            int rowsAffected = CRUD_Data.ExecuteNonQuery(updateQuery, parameters);

            if (rowsAffected > 0)
            {
                MessageBox.Show("Customer updated successfully.");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("An error occurred while updating the customer.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
