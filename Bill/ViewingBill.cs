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
        private List<(int ProductId, int Quantity)> billDetailsList = new List<(int ProductId, int Quantity)>();

        private int StaffId;
        public ViewingBill(int staffid)
        {
            StaffId = staffid;
            InitializeComponent();
        }

        private void ViewingBill_Load(object sender, EventArgs e)
        {
            StyleSet.DataGridViewStyle(dgv_Bill);
            Load_Bill();


            if (dgv_Bill.Columns["Bill_Id"] != null) dgv_Bill.Columns["Bill_Id"].HeaderText = "Mã hóa đơn";
            if (dgv_Bill.Columns["StaffName"] != null) dgv_Bill.Columns["StaffName"].HeaderText = "Tên nhân viên";
            if (dgv_Bill.Columns["CustomerName"] != null) dgv_Bill.Columns["CustomerName"].HeaderText = "Tên khách hàng";
            if (dgv_Bill.Columns["SalesPercent"] != null) dgv_Bill.Columns["SalesPercent"].HeaderText = "Chiết khấu";
            if (dgv_Bill.Columns["CreatedDate"] != null) dgv_Bill.Columns["CreatedDate"].HeaderText = "Ngày tạo";
            if (dgv_Bill.Columns["Total"] != null) dgv_Bill.Columns["Total"].HeaderText = "Tổng hóa đơn";
        }

        private void Load_Bill()
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
        private int GetCustomerIdByName(string customerName)
        {
            string query = "SELECT CustomerId FROM Customer WHERE Name = @CustomerName";
            SqlParameter param = new SqlParameter("@CustomerName", customerName);
            object result = CRUD_Data.ExecuteScalar(query, new SqlParameter[] { param });

            return result != null ? Convert.ToInt32(result) : -1; 
        }

        private void btn_DeleteBill_Click(object sender, EventArgs e)
        {
            if (dgv_Bill.CurrentRow == null) return;

            int billId = Convert.ToInt32(dgv_Bill.CurrentRow.Cells["Bill_Id"].Value);
            string customerName = dgv_Bill.CurrentRow.Cells["CustomerName"].Value.ToString();

            int customerId = GetCustomerIdByName(customerName);
            if (customerId == -1)
            {
                MessageBox.Show("Không thể tìm thấy thông tin khách hàng.");
                return;
            }

            string checkBillCountQuery = "SELECT COUNT(*) FROM Bill WHERE Customer_Id = @CustomerId AND Bill_Id != @BillId";
            SqlParameter[] checkBillCountParams = {
        new SqlParameter("@CustomerId", customerId),
        new SqlParameter("@BillId", billId)
    };
            int billCount = (int)CRUD_Data.ExecuteScalar(checkBillCountQuery, checkBillCountParams);

            if (billCount > 0)
            {
                DeleteBillAndDetails(billId);
                MessageBox.Show("Chỉ hóa đơn đã bị xóa.");
            }
            else
            {
                DeleteBillAndDetails(billId);
                string deleteCustomerQuery = "DELETE FROM Customer WHERE CustomerId = @CustomerId";
                SqlParameter deleteCustomerParam = new SqlParameter("@CustomerId", customerId);
                CRUD_Data.ExecuteNonQuery(deleteCustomerQuery, new SqlParameter[] { deleteCustomerParam });
                MessageBox.Show("Khách hàng và các dữ liệu liên quan đã bị xóa.");
            }


            currentBillId = -1;

            Load_Bill();

        }
        private void DeleteBillAndDetails(int billId)
        {
            // Kiểm tra nếu có sản phẩm liên quan đến hóa đơn này trong billDetailsList
            var billDetails = billDetailsList.Where(b => b.ProductId != 0).ToList(); // Lọc các sản phẩm hợp lệ

            if (billDetails.Count > 0)
            {
                // Hiển thị danh sách sản phẩm trong MessageBox (giữ nguyên)
                StringBuilder productList = new StringBuilder();
                foreach (var detail in billDetails)
                {
                    productList.AppendLine($"ProductId: {detail.ProductId}, Quantity: {detail.Quantity}");
                }
                MessageBox.Show("Các ProductId trong BillDetail:\n" + productList.ToString(), "Thông tin sản phẩm");

                // Cập nhật số lượng sản phẩm trong bảng Product và WarehouseProduct
                foreach (var detail in billDetails)
                {
                    int productId = detail.ProductId;
                    int quantity = detail.Quantity;

                    // Cập nhật số lượng sản phẩm trong bảng Product
                    string updateProductQuery = @"
            UPDATE Product 
            SET Quantity = Quantity + @Quantity 
            WHERE ProductId = @ProductId";

                    SqlParameter[] updateProductParams = {
                new SqlParameter("@Quantity", quantity),
                new SqlParameter("@ProductId", productId)
            };

                    CRUD_Data.ExecuteNonQuery(updateProductQuery, updateProductParams);

                    // === Thêm phần cập nhật số lượng trong WarehouseProduct ===
                    // Kiểm tra nếu ProductId tồn tại trong WarehouseProduct
                    string checkWarehouseQuery = @"
            SELECT COUNT(*) 
            FROM WarehouseProduct 
            WHERE ProductId = @ProductId";
                    SqlParameter[] checkParams = {
                new SqlParameter("@ProductId", productId)
            };

                    int warehouseCount = (int)CRUD_Data.ExecuteScalar(checkWarehouseQuery, checkParams);

                    if (warehouseCount > 0)
                    {
                        // Cập nhật số lượng trong WarehouseProduct
                        string updateWarehouseQuery = @"
                UPDATE WarehouseProduct 
                SET Quantity = Quantity + @Quantity 
                WHERE ProductId = @ProductId";

                        SqlParameter[] updateWarehouseParams = {
                    new SqlParameter("@Quantity", quantity),
                    new SqlParameter("@ProductId", productId)
                };

                        CRUD_Data.ExecuteNonQuery(updateWarehouseQuery, updateWarehouseParams);
                    }
                }

                // Xóa các chi tiết hóa đơn liên quan đến Bill_Id
                string deleteBillDetailsQuery = "DELETE FROM BillDetail WHERE Bill_Id = @BillId";
                SqlParameter deleteBillDetailsParam = new SqlParameter("@BillId", billId);
                CRUD_Data.ExecuteNonQuery(deleteBillDetailsQuery, new SqlParameter[] { deleteBillDetailsParam });

                // Xóa hóa đơn nếu không có lỗi
                string deleteBillQuery = "DELETE FROM Bill WHERE Bill_Id = @BillId";
                SqlParameter deleteBillParam = new SqlParameter("@BillId", billId);
                CRUD_Data.ExecuteNonQuery(deleteBillQuery, new SqlParameter[] { deleteBillParam });

                MessageBox.Show("Hóa đơn và các chi tiết đã bị xóa, số lượng sản phẩm đã được cập nhật lại.");
            }
            else
            {
                MessageBox.Show("Không tìm thấy sản phẩm trong BillDetail của hóa đơn này.");
            }
        }

        private void btn_AddBill_Click(object sender, EventArgs e)
        {
            AddingBill addingBill = new AddingBill(StaffId);
            var result =  addingBill.ShowDialog();
            if (result == DialogResult.OK)
            {
                Load_Bill();
            }
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
        private int currentBillId = -1;
        private void LoadBillDetails(int billId)
        {
            // Tạo danh sách chứa thông tin ProductId và Quantity của BillDetail tương ứng với BillId
            billDetailsList.Clear();  // Xóa danh sách trước khi cập nhật

            string query = @"
        SELECT ProductId, Quantity 
        FROM BillDetail 
        WHERE Bill_Id = @BillId";

            SqlParameter param = new SqlParameter("@BillId", billId);
            DataTable billDetails = CRUD_Data.GetDataWithParameter(query, new SqlParameter[] { param });

            foreach (DataRow row in billDetails.Rows)
            {
                int productId = Convert.ToInt32(row["ProductId"]);
                int quantity = Convert.ToInt32(row["Quantity"]);

                // Thêm thông tin vào danh sách
                billDetailsList.Add((productId, quantity));
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (billDetailsList.Count > 0)
            {
                StringBuilder productList = new StringBuilder();

                // Duyệt qua danh sách billDetailsList để tạo chuỗi thông tin sản phẩm
                foreach (var detail in billDetailsList)
                {
                    productList.AppendLine($"ProductId: {detail.ProductId}, Quantity: {detail.Quantity}");
                }

                // Hiển thị danh sách sản phẩm trong MessageBox
                MessageBox.Show("Danh sách sản phẩm trong BillDetail:\n" + productList.ToString(), "Thông tin BillDetail");
            }
            else
            {
                MessageBox.Show("Không tìm thấy sản phẩm cho BillId đã chọn.");
            }
        }

        private void dgv_Bill_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Lấy BillId từ cột "Bill_Id" trong DataGridView
                currentBillId = Convert.ToInt32(dgv_Bill.Rows[e.RowIndex].Cells["Bill_Id"].Value);


                // Gọi hàm để lấy thông tin BillDetail tương ứng với BillId đã chọn
                LoadBillDetails(currentBillId);
            }
        }
    }
}
