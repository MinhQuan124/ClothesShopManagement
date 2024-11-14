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

namespace ClothesShopManagement.Bill
{
    public partial class ViewingBill : Form
    {
        private int StaffId;
        public ViewingBill(int staffid)
        {
            StaffId = staffid;
            InitializeComponent();
        }

        private void ViewingBill_Load(object sender, EventArgs e)
        {
            StyleSet.DataGridViewStyle(dgv_Bill);
            Load_Staff();

            // Check if DataGridView has been populated with data to avoid NullReferenceException
            if (dgv_Bill.Columns["Bill_Id"] != null) dgv_Bill.Columns["Bill_Id"].HeaderText = "Mã hóa đơn";
            if (dgv_Bill.Columns["StaffName"] != null) dgv_Bill.Columns["StaffName"].HeaderText = "Tên nhân viên";
            if (dgv_Bill.Columns["CustomerName"] != null) dgv_Bill.Columns["CustomerName"].HeaderText = "Tên khách hàng";
            if (dgv_Bill.Columns["SalesPercent"] != null) dgv_Bill.Columns["SalesPercent"].HeaderText = "Chiết khấu";
            if (dgv_Bill.Columns["CreatedDate"] != null) dgv_Bill.Columns["CreatedDate"].HeaderText = "Ngày tạo";
            if (dgv_Bill.Columns["Total"] != null) dgv_Bill.Columns["Total"].HeaderText = "Tổng hóa đơn";
        }

        private void Load_Staff()
        {
            string query = @"
        SELECT b.Bill_Id, 
               s.Name AS StaffName, 
               c.Name AS CustomerName, 
               b.SalesPercent, 
               b.CreatedDate, 
               b.Total
        FROM Bill b
        JOIN Staff s ON b.Staff_Id = s.StaffId
        JOIN Customer c ON b.Customer_Id = c.CustomerId";

            dgv_Bill.DataSource = CRUD_Data.GetData(query);
        }

        private void btn_DeleteBill_Click(object sender, EventArgs e)
        {
            if (dgv_Bill.CurrentRow == null) return;

            int billId = Convert.ToInt32(dgv_Bill.CurrentRow.Cells["Bill_Id"].Value);
            int customerId = Convert.ToInt32(dgv_Bill.CurrentRow.Cells["Customer_Id"].Value);
            DateTime billCreatedDate = Convert.ToDateTime(dgv_Bill.CurrentRow.Cells["CreatedDate"].Value);

            string customerDateQuery = "SELECT CreatedDate FROM Customer WHERE CustomerId = @CustomerId";
            SqlParameter param = new SqlParameter("@CustomerId", customerId);

            // Lấy thông tin ngày tạo khách hàng
            DataTable customerData = CRUD_Data.GetDataWithParameter(customerDateQuery, param);

            if (customerData.Rows.Count > 0)
            {
                DateTime customerCreatedDate = Convert.ToDateTime(customerData.Rows[0]["CreatedDate"]);

                if (customerCreatedDate.Date == billCreatedDate.Date)
                {
                    // Tạo tham số mới cho câu lệnh DELETE
                    SqlParameter deleteBillParam = new SqlParameter("@CustomerId", customerId);
                    string deleteBillQuery = "DELETE FROM Bill WHERE Customer_Id = @CustomerId";
                    CRUD_Data.ExecuteNonQuery(deleteBillQuery, new SqlParameter[] { deleteBillParam });

                    // Tạo tham số mới cho câu lệnh DELETE Customer
                    SqlParameter deleteCustomerParam = new SqlParameter("@CustomerId", customerId);
                    string deleteCustomerQuery = "DELETE FROM Customer WHERE CustomerId = @CustomerId";
                    CRUD_Data.ExecuteNonQuery(deleteCustomerQuery, new SqlParameter[] { deleteCustomerParam });

                    MessageBox.Show("Khách hàng và hóa đơn liên quan đã bị xóa.");
                }
                else
                {
                    // Tạo tham số mới cho câu lệnh DELETE Bill
                    SqlParameter deleteBillParam = new SqlParameter("@BillId", billId);
                    string deleteBillQuery = "DELETE FROM Bill WHERE Bill_Id = @BillId";
                    CRUD_Data.ExecuteNonQuery(deleteBillQuery, new SqlParameter[] { deleteBillParam });
                    MessageBox.Show("Chỉ hóa đơn đã bị xóa.");
                }
            }
            else
            {
                MessageBox.Show("Không tìm thấy thông tin khách hàng.");
            }

            Load_Staff();
        }

        private void btn_AddBill_Click(object sender, EventArgs e)
        {
            AddingBill addingBill = new AddingBill(StaffId);
            addingBill.ShowDialog();
        }

        private void dgv_Bill_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = dgv_Bill.Rows[e.RowIndex];
                int billId = Convert.ToInt32(selectedRow.Cells[0].Value);

                new ViewingBillDetail(billId).ShowDialog();


            }
        }
    }
}
