using ClothesShopManagement.Brand;
using ClothesShopManagement.WareHouseProduct;
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
    public partial class ViewingWarehouse : Form
    {
        public ViewingWarehouse()
        {
            InitializeComponent();
        }
        private void LoadWareHouse()
        {
            string sql = "SELECT Warehouse_Id, Name, Address, Phone, Email, Stock FROM ClothesShopManagement.dbo.Warehouse";
            dataGridView1.DataSource = CRUD_Data.GetData(sql);
            StyleSet.DataGridViewStyle(dataGridView1);

            // Customize DataGridView (optional)
            // CustomizeDataGridView();

            dataGridView1.Columns["Warehouse_Id"].HeaderText = "Mã kho";
            dataGridView1.Columns["Name"].HeaderText = "Tên kho";
            dataGridView1.Columns["Phone"].HeaderText = "Số điện thoại";
            dataGridView1.Columns["Address"].HeaderText = "Địa chỉ";
            dataGridView1.Columns["Email"].HeaderText = "Email";
            dataGridView1.Columns["Stock"].HeaderText = "Số lượng";
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
        private void ViewingWarehouse_Load(object sender, EventArgs e)
        {
            LoadWareHouse();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (AddingWarehouse addBrandForm = new AddingWarehouse())
            {
                if (addBrandForm.ShowDialog() == DialogResult.OK)
                {
                    LoadWareHouse();
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string searchValue = textBox1.Text.Trim();

            if (string.IsNullOrEmpty(searchValue))
            {
                LoadWareHouse();
            }

            else
            {
                string sql = "SELECT Warehouse_Id, Name, Address, Phone, Email, Stock FROM ClothesShopManagement.dbo.Warehouse WHERE Name LIKE @searchValue";

                SqlParameter param = new SqlParameter("@searchValue", "%" + searchValue + "%");

                dataGridView1.DataSource = CRUD_Data.GetDataWithParameter(sql, param);
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Get the selected row
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                string warehouseId = selectedRow.Cells["Warehouse_Id"].Value.ToString();

                // Confirm deletion
                DialogResult confirmResult = MessageBox.Show($"Are you sure you want to delete warehouse {warehouseId}?", "Confirm Delete", MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    // SQL query to delete the selected warehouse from the database
                    string sql = "DELETE FROM ClothesShopManagement.dbo.Warehouse WHERE Warehouse_Id = @WarehouseId";
                    SqlParameter[] parameters = new SqlParameter[]
                    {
            new SqlParameter("@WarehouseId", warehouseId)
                    };

                    // Execute the delete operation
                    int rowsAffected = CRUD_Data.ExecuteNonQuery(sql, parameters);
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Warehouse deleted successfully!");
                        LoadWareHouse(); // Refresh the data in DataGridView
                    }
                    else
                    {
                        MessageBox.Show("Error deleting warehouse. Please try again.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a warehouse to delete.");
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Get the selected row
                var selectedRow = dataGridView1.SelectedRows[0];
                var warehouse = new ModelClass.Warehouse
                {
                    WarehouseId = Convert.ToInt32(selectedRow.Cells["Warehouse_Id"].Value.ToString()),
                    Name = selectedRow.Cells["Name"].Value.ToString(),
                    Address = selectedRow.Cells["Address"].Value.ToString(),
                    Phone = selectedRow.Cells["Phone"].Value.ToString(),
                    Email = selectedRow.Cells["Email"].Value.ToString(),
                    Stock = Convert.ToInt32(selectedRow.Cells["Stock"].Value)
                };

                // Open the EditWarehouse form
                using (var editWarehouseForm = new EditingWarehouse(warehouse))
                {
                    if (editWarehouseForm.ShowDialog() == DialogResult.OK)
                    {
                        LoadWareHouse(); // Refresh the DataGridView after editing
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a warehouse to edit.");
            }

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)  // Kiểm tra nếu người dùng double-click vào một dòng hợp lệ
            {
                // Lấy giá trị ID của nhà kho từ dòng đã chọn
                var selectedRow = dataGridView1.Rows[e.RowIndex];
                string warehouseId = selectedRow.Cells["Warehouse_Id"].Value.ToString();

                // Mở form DetailWarehouse và truyền ID nhà kho vào TextBox
                using (var detailWarehouseForm = new ViewingWarehoueProduct())
                {
                    detailWarehouseForm.SetWarehouseId(warehouseId);  // Truyền ID nhà kho vào form chi tiết
                    detailWarehouseForm.ShowDialog();  // Hiển thị form chi tiết
                }
            }
        }
    }
}
