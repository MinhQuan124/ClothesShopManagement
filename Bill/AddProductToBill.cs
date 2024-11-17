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
            DataTable warehouses = CRUD_Data.GetData("SELECT Warehouse_Id, Name FROM Warehouse");
            cmbWarehouse.DataSource = warehouses;
            cmbWarehouse.DisplayMember = "Name";
            cmbWarehouse.ValueMember = "Warehouse_Id"; 
        }
        private void AddProductToBill_Load(object sender, EventArgs e)
        {
            StyleSet.DataGridViewStyle(dgvProducts);
            LoadWarehouses();
            int warehouseId = 0;
            if (cmbWarehouse.SelectedValue != null && int.TryParse(cmbWarehouse.SelectedValue.ToString(), out warehouseId))
            {
                LoadProduct(warehouseId);
            }
            LoadDGV();
        }

        private void LoadDGV()
        {
            if (dgvProducts.Columns["WarehouseProduct_Id"] != null) dgvProducts.Columns["WarehouseProduct_Id"].Visible = false;
            if (dgvProducts.Columns["ProductId"] != null) dgvProducts.Columns["ProductId"].Visible = false;
            if (dgvProducts.Columns["ProductName"] != null) dgvProducts.Columns["ProductName"].HeaderText = "Tên quần áo";
            if (dgvProducts.Columns["Size"] != null) dgvProducts.Columns["Size"].HeaderText = "Kích cỡ";
            if (dgvProducts.Columns["Price"] != null) dgvProducts.Columns["Price"].HeaderText = "Đơn giá";
            if (dgvProducts.Columns["Quantity"] != null) dgvProducts.Columns["Quantity"].HeaderText = "Số lượng";
        }

        private void LoadProduct(int warehouseId)
        {
            string sql = @"
            SELECT 
               wp.WarehouseProduct_Id,
               p.ProductId,
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
                LoadDGV();
            }
        }

        public List<ModelClass.AddProduct> addProducts { get; private set; } = new List<ModelClass.AddProduct>();

        private void btn_Save_Click(object sender, EventArgs e)
        {
            if (dgvProducts.SelectedRows.Count > 0)
            {
                if (string.IsNullOrWhiteSpace(txtQuanity.Text))
                {
                    MessageBox.Show("Vui lòng nhập số lượng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtQuanity.Focus();
                    return;
                }

                if (!int.TryParse(txtQuanity.Text, out int checkQuanity) || checkQuanity <= 0)
                {
                    MessageBox.Show("Số lượng phải là số nguyên dương.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtQuanity.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtSellingPrice.Text))
                {
                    MessageBox.Show("Vui lòng nhập giá bán.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtSellingPrice.Focus();
                    return;
                }

                if (!float.TryParse(txtSellingPrice.Text, out float sellingPrice) || sellingPrice <= 0)
                {
                    MessageBox.Show("Giá bán phải lớn hơn 0.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtSellingPrice.Focus();
                    return;
                }

                int checkQuanity_2 = Convert.ToInt32(dgvProducts.SelectedRows[0].Cells["Quantity"].Value);

                if (checkQuanity > checkQuanity_2)
                {
                    MessageBox.Show("Không thể bán số lượng lớn hơn số lượng trong kho.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                float importPrice = Convert.ToSingle(dgvProducts.SelectedRows[0].Cells["Price"].Value);

                if (sellingPrice <= importPrice)
                {
                    MessageBox.Show("Giá bán phải lớn hơn giá nhập.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtSellingPrice.Focus();
                    return;
                }

                var selectedRow = dgvProducts.SelectedRows[0];

                AddProduct addProduct = new AddProduct
                {
                    WarehouseProduct_Id = Convert.ToInt32(selectedRow.Cells["WarehouseProduct_Id"].Value),
                    ProductId = Convert.ToInt32(selectedRow.Cells["ProductId"].Value),
                    ProductName = selectedRow.Cells["ProductName"].Value.ToString(),
                    Size = selectedRow.Cells["Size"].Value.ToString(),
                    Price = sellingPrice,
                    Quanity = checkQuanity, 
                };

                addProducts.Add(addProduct);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một sản phẩm để thêm.");
            }
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
