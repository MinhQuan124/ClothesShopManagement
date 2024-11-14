using ClothesShopManagement.ModelClass;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Xml.Linq;
using ClothesShopManagement.Customer;

namespace ClothesShopManagement.Bill
{
    public partial class AddingBill : Form
    {
        private int StaffId;
        public List<ModelClass.AddProduct> addProducts { get; private set; } = new List<ModelClass.AddProduct>();
        public AddingBill(int staffid)
        {
            StaffId = staffid;
            InitializeComponent();
            
            
        }
        private void AddingBill_Load(object sender, EventArgs e)
        {
            LoadCmbCustomer();
            StyleSet.DataGridViewStyle(dgvBillDetail);
            LoadDGV();
        }

        private void LoadDGV() 
        {
            if (dgvBillDetail.Columns["WarehouseProduct_Id"] != null) dgvBillDetail.Columns["WarehouseProduct_Id"].Visible = false;
            if (dgvBillDetail.Columns["ProductName"] != null) dgvBillDetail.Columns["ProductName"].HeaderText = "Tên sản phẩm";
            if (dgvBillDetail.Columns["Size"] != null) dgvBillDetail.Columns["Size"].HeaderText = "Size";
            if (dgvBillDetail.Columns["Price"] != null) dgvBillDetail.Columns["Price"].HeaderText = "Giá";
            if (dgvBillDetail.Columns["Quantity"] != null) dgvBillDetail.Columns["Quantity"].HeaderText = "Số lượng";
            if (dgvBillDetail.Columns["Total"] != null) dgvBillDetail.Columns["Total"].HeaderText = "Tổng";
        }

        private void LoadCmbCustomer()
        {
            DataTable CustomerTable = CRUD_Data.GetData("SELECT CustomerId, Name FROM Customer");
            cmbCustomer.DataSource = CustomerTable;
            cmbCustomer.DisplayMember = "Name";
            cmbCustomer.ValueMember = "CustomerId";
            cmbCustomer.SelectedIndex = CustomerTable.Rows.Count - 1; 
            CalculationDiscount();
        }

        private void LoadBillDetail()
        {
            dgvBillDetail.DataSource = null;  
            dgvBillDetail.DataSource = addProducts;
            LoadDGV();
        }
       

        private void btnAddProductToBill_Click(object sender, EventArgs e)
        {
            AddProductToBill addProductToBill = new AddProductToBill();
            var result = addProductToBill.ShowDialog();
            if (result == DialogResult.OK)
            {
                addProducts.AddRange(addProductToBill.addProducts);
                MessageBox.Show("Đã thêm sản phẩm vào danh sách hóa đơn.");
                LoadBillDetail();
                CalculateTotalAmount();
            }
        }

        private void CalculationDiscount()
        {
            string customerIDstring = cmbCustomer.SelectedValue.ToString();
            int customerID;
            if(int.TryParse(customerIDstring, out customerID))
            {

            }
            string query = "SELECT COUNT(*) FROM Bill WHERE Customer_Id = @CustomerID";
            SqlParameter[] parameters = new SqlParameter[]
            {
              new SqlParameter("@CustomerID", customerID),
            };

            // Thực thi truy vấn và lấy kết quả một cách an toàn
            object result = CRUD_Data.ExecuteScalar(query, parameters);
            int count = result != null && result != DBNull.Value ? Convert.ToInt32(result) : 0;

            // Nếu khách hàng có ít nhất một hóa đơn trước đó, giảm giá 20%, nếu không thì là 0%
            txtSale.Text = count > 0 ? "20" : "0";

        }

        private void CalculateTotalAmount()
        {
            // Tính tổng các giá trị Total trong danh sách addProducts
            decimal totalAmount = addProducts.Sum(item => item.Total);

            //Tính chiết khấu nếu có
            int sale;
            if (int.TryParse(txtSale.Text, out sale))
            {
                totalAmount = totalAmount*(1-(sale/100.0m));
            }
            // Hiển thị tổng vào txtTong
            txtTotal.Text = totalAmount.ToString("N2"); // Định dạng số thập phân nếu cần
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //Save Bill
            int Staff_Id = StaffId;
            int Customer_Id = (int)cmbCustomer.SelectedValue;
            float SalesPercent = Convert.ToSingle (txtSale.Text.Trim());
            DateTime CreatedDate = dateTimePicker1.Value;
            float Total = Convert.ToSingle(txtTotal.Text.Trim());

            // Kiểm tra dữ liệu hợp lệ
            //|| SalesPercent == 0
            if (Customer_Id == 0  || Total == 0 || CreatedDate == DateTime.MinValue)
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin.");
                return;
            }
            string query = "INSERT INTO Bill (Staff_Id, Customer_Id, SalesPercent, CreatedDate, Total) " +
               "OUTPUT INSERTED.Bill_Id " +
               "VALUES (@staff_Id, @customer_Id, @salesPercent, @createdDate, @total)";
            SqlParameter[] parameters = new SqlParameter[]
           {
                new SqlParameter("@staff_Id", Staff_Id),
                new SqlParameter("@customer_Id", Customer_Id),
                new SqlParameter("@salesPercent", SalesPercent),
                new SqlParameter("@createdDate", CreatedDate),
                new SqlParameter("@total", Total)
           };

            object result = CRUD_Data.ExecuteScalar(query, parameters); 
            if (result != null)
            {
                int newBillId = Convert.ToInt32(result);
                foreach (var product in addProducts)
                {
                    string query_2 = "INSERT INTO Billdetail (Bill_Id, WarehouseProduct_Id, Quantity, Price,Total) " +
                                   "VALUES (@billId, @warehouseProduct_Id, @quantity, @price, @total)";
                    SqlParameter[] parameters_2 = new SqlParameter[]
                    {
                     new SqlParameter("@billId", newBillId),
                     new SqlParameter("@warehouseProduct_Id", product.WarehouseProduct_Id),
                     new SqlParameter("@quantity", product.Quanity),
                     new SqlParameter("@price", product.Price),
                     new SqlParameter("@total", product.Total)
                    };
                    //
                    CRUD_Data.ExecuteNonQuery(query_2, parameters_2);
                  
                    // 
                    string queryUpdate = "UPDATE WarehouseProduct " +
                                         "SET Quantity = Quantity - @quantity " +
                                         "WHERE WarehouseProduct_Id = @warehouseProduct_Id";

                    SqlParameter[] parametersUpdate = new SqlParameter[]
                    {
                      new SqlParameter("@quantity", product.Quanity),
                      new SqlParameter("@warehouseProduct_Id", product.WarehouseProduct_Id)
                    };

                    // 
                    CRUD_Data.ExecuteNonQuery(queryUpdate, parametersUpdate);
                }
                MessageBox.Show("Đã thêm hóa đơn thành công.");
                DialogResult = DialogResult.OK;
                this.Close();

            }
            else
            {
                MessageBox.Show("Không thể thêm hóa đơn.");
            };

            //Save BillDetail
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmbCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
          CalculationDiscount();
        }

        private void btnAddCustomer_Click(object sender, EventArgs e)
        {
            AddCustomer addCustomer = new AddCustomer();

            var result = addCustomer.ShowDialog();
            if (result == DialogResult.OK)
            {
                LoadCmbCustomer();
            }
        }
    }
}
