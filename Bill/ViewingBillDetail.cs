using ClothesShopManagement.ModelClass;
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
    public partial class ViewingBillDetail : Form
    {
        private int BillId;
        public ViewingBillDetail(int billid)
        {
            BillId = billid;
            InitializeComponent();
        }
        private void LoadBillDetail()
        {
            string sql1 = @"
             SELECT 
                  s.Name AS StaffName,
                  c.Name AS CustomerName,
                  b.CreatedDate,
                  b.SalesPercent,
                  b.Total
             FROM 
                  ClothesShopManagement.dbo.Bill b
             JOIN
                  ClothesShopManagement.dbo.Staff s ON b.Staff_Id = s.StaffId
             JOIN 
                  ClothesShopManagement.dbo.Customer c ON b.Customer_Id = c.CustomerId
             WHERE 
                  b.Bill_Id = @BillId";

            // Define parameter for the Bill_Id
            SqlParameter[] parameters1 = new SqlParameter[]
            {
                new SqlParameter("@BillId", BillId)
            };

            // Retrieve data
            DataTable billDetailsTable = CRUD_Data.GetDataWithParameter(sql1, parameters1);

            // Populate text boxes if data is available
            if (billDetailsTable.Rows.Count > 0)
            {
                DataRow row = billDetailsTable.Rows[0];
                lblStaffname.Text += row["StaffName"].ToString();
                lblCustomerName.Text += row["CustomerName"].ToString();
                lblCreatedDate.Text += Convert.ToDateTime(row["CreatedDate"]).ToString("dd/MM/yyyy"); // Format as needed
                lblSalesPercent.Text += row["SalesPercent"].ToString();
                lblTotal.Text += row["Total"].ToString();
            }
            //
            string sql = @"
            SELECT 
               p.ProductName,
               bd.Quantity,
               p.Size,
               bd.Price,
               bd.Total
             FROM 
               ClothesShopManagement.dbo.BillDetail bd
             JOIN
               ClothesShopManagement.dbo.WarehouseProduct wp ON bd.WarehouseProduct_Id = wp.WarehouseProduct_Id
             JOIN 
               ClothesShopManagement.dbo.Product p ON wp.ProductId = p.ProductId
             WHERE 
               bd.Bill_Id = @BillId";

            // Tạo tham số cho truy vấn
            SqlParameter[] parameters = new SqlParameter[]
            {
               new SqlParameter("@BillId", BillId)
            };
            // Đổ dữ liệu vào dataGridView
            dgvBillDetail.DataSource = CRUD_Data.GetDataWithParameter(sql, parameters);
            StyleSet.DataGridViewStyle(dgvBillDetail);


        }

        private void ViewingBillDetail_Load(object sender, EventArgs e)
        {
            LoadBillDetail();
            LoadDGV();
        }
        private void LoadDGV()
        {
            if (dgvBillDetail.Columns["ProductName"] != null) dgvBillDetail.Columns["ProductName"].HeaderText = "Tên sản phẩm";
            if (dgvBillDetail.Columns["Quantity"] != null) dgvBillDetail.Columns["Quantity"].HeaderText = "Số lượng";
            if (dgvBillDetail.Columns["Size"] != null) dgvBillDetail.Columns["Size"].HeaderText = "Size";
            if (dgvBillDetail.Columns["Price"] != null) dgvBillDetail.Columns["Price"].HeaderText = "Giá";
            if (dgvBillDetail.Columns["Total"] != null) dgvBillDetail.Columns["Total"].HeaderText = "Tổng";           
        }
    }
}
