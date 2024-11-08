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
    public partial class ViewCustomer : Form
    {
        public ViewCustomer()
        {
            InitializeComponent();
        }

        private void ViewCustomer_Load(object sender, EventArgs e)
        {
            StyleSet.DataGridViewStyle(dgv_ViewCus);
            Load_Staff();
            dgv_ViewCus.Columns["CustomerId"].HeaderText = "Mã khách hàng";
            dgv_ViewCus.Columns["Name"].HeaderText = " Họ Tên";
            dgv_ViewCus.Columns["PhoneNumber"].HeaderText = "Số điện thoại";
            dgv_ViewCus.Columns["Email"].HeaderText = "Email";
            dgv_ViewCus.Columns["CreatedDate"].HeaderText = "Ngày tạo";
        }
        private void Load_Staff()
        {
            string query = "SELECT CustomerId, Name, PhoneNumber, Email, CreatedDate FROM Customer";
            dgv_ViewCus.DataSource = CRUD_Data.GetData(query);
        }


        private void btn_AddCus_Click(object sender, EventArgs e)
        {
            AddCustomer addCustomerForm = new AddCustomer();
            if (addCustomerForm.ShowDialog() == DialogResult.OK)
            {
                // Refresh data grid view after adding a new customer
                Load_Staff();
            }
        }

        private void btn_UpdateCus_Click(object sender, EventArgs e)
        {
            if (dgv_ViewCus.CurrentRow == null) return;

            // Get selected customer ID
            int customerId = Convert.ToInt32(dgv_ViewCus.CurrentRow.Cells["CustomerId"].Value);

            EditCustomer editForm = new EditCustomer(customerId);
            if (editForm.ShowDialog() == DialogResult.OK)
            {
                // Refresh data grid view after update
                Load_Staff();
            }
            //


        }

        private void btn_DeleteCus_Click(object sender, EventArgs e)
        {
            if (dgv_ViewCus.CurrentRow == null) return;

            int customerId = Convert.ToInt32(dgv_ViewCus.CurrentRow.Cells["CustomerId"].Value);
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete this customer?", "Delete Confirmation", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                string sql = "DELETE FROM Customer WHERE CustomerId = @CustomerId";
                SqlParameter[] parameters = new SqlParameter[]
                {
            new SqlParameter("@CustomerId", customerId)
                };

                CRUD_Data.ExecuteNonQuery(sql, parameters);
                Load_Staff();
            }
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {

            string phoneNumber = txtSearch.Text.Trim();

            // Build query based on whether a phone number is provided
            string query = string.IsNullOrEmpty(phoneNumber)
                ? "SELECT CustomerId, Name, PhoneNumber, Email, CreatedDate FROM Customer"
                : $"SELECT CustomerId, Name, PhoneNumber, Email, CreatedDate FROM Customer WHERE PhoneNumber = '{phoneNumber}'";

            // Retrieve data and bind it to the DataGridView
            DataTable customerData = CRUD_Data.GetData(query);
            dgv_ViewCus.DataSource = customerData;

            if (customerData.Rows.Count == 0)
            {
                MessageBox.Show("No customer found with the provided phone number.");
            }
        }
    }
}
