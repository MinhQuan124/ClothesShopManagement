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
    public partial class AddProductToBill : Form
    {
        
        public AddProductToBill()
        {
            InitializeComponent();
        }
        private void LoadWarehouses()
        {
            StyleSet.DataGridViewStyle(dgvProducts);
            DataTable warehouses = CRUD_Data.GetData("SELECT Warehouse_Id, Name FROM Warehouse");
            cmbWarehouse.DataSource = warehouses;
            cmbWarehouse.DisplayMember = "Name";
            cmbWarehouse.ValueMember = "Warehouse_Id";
            //

            if (dgvProducts.Columns["WarehouseProduct_Id"] != null) dgvProducts.Columns["WarehouseProduct_Id"].HeaderText = "Mã sản phẩm trong kho";
            if (dgvProducts.Columns["ProductName"] != null) dgvProducts.Columns["ProductName"].HeaderText = "Tên sản phẩm";
            if (dgvProducts.Columns["Size"] != null) dgvProducts.Columns["Size"].HeaderText = "Size";
            if (dgvProducts.Columns["Price"] != null) dgvProducts.Columns["Price"].HeaderText = "Giá";
            if (dgvProducts.Columns["Quantity"] != null) dgvProducts.Columns["Quantity"].HeaderText = "Số lượng";
        }
        private void AddProductToBill_Load(object sender, EventArgs e)
        {
            LoadWarehouses();
        }
        private void LoadProduct(int warehouseId)
        {
            string sql = @"
            SELECT 
               wp.WarehouseProduct_Id,
               p.ProductName,
               p.Size,
               p.Price,
               wp.Quantity
             FROM 
               ClothesShopManagement.dbo.WarehouseProduct wp
             JOIN 
               ClothesShopManagement.dbo.Product p ON wp.ProductId = p.ProductId
             WHERE 
               wp.Warehouse_Id = @WarehouseId";

            // Tạo tham số cho truy vấn
            SqlParameter[] parameters = new SqlParameter[]
            {
               new SqlParameter("@WarehouseId", warehouseId)
            };

            // Đổ dữ liệu vào dataGridView
            dgvProducts.DataSource = CRUD_Data.GetDataWithParameter(sql, parameters);
            StyleSet.DataGridViewStyle(dgvProducts);
        }

        private void cmbWarehouse_SelectedIndexChanged(object sender, EventArgs e)
        {
            int warehouseId = 0;
            if (cmbWarehouse.SelectedValue != null && int.TryParse(cmbWarehouse.SelectedValue.ToString(), out warehouseId))
            {
                LoadProduct(warehouseId);
            }
        }

        public List<ModelClass.AddProduct> addProducts { get; private set; } = new List<ModelClass.AddProduct>();

        private void btn_Save_Click(object sender, EventArgs e)
        {
            if (dgvProducts.SelectedRows.Count > 0)
            {
                if(txtQuanity.Text == "")
                {
                    MessageBox.Show("Vui lòng nhập số lượng.");
                }
                else
                {
                    // Giả sử `Staff` là lớp chứa các thuộc tính của nhân viên
                    var selectedRow = dgvProducts.SelectedRows[0];

                    // Lấy dữ liệu từ các ô trong dòng được chọn
                    AddProduct addProduct = new AddProduct
                    {
                        WarehouseProduct_Id = Convert.ToInt32(selectedRow.Cells["WarehouseProduct_Id"].Value),
                        ProductName = selectedRow.Cells["ProductName"].Value.ToString(),
                        Price = Convert.ToSingle(selectedRow.Cells["Price"].Value),
                        Quanity = Convert.ToInt32(txtQuanity.Text),
                        // Lấy các thuộc tính khác tương tự
                    };
                    // Thêm vào danh sách
                    addProducts.Add(addProduct);

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một dòng để thêm.");
            }
           
        }
    }
}
