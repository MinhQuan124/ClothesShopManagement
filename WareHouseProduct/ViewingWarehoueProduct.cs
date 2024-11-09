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

namespace ClothesShopManagement.WareHouseProduct
{
    public partial class ViewingWarehoueProduct : Form
    {
        private HomeForm hp;
        public ViewingWarehoueProduct()
        {
            InitializeComponent();
        }
        public ViewingWarehoueProduct(HomeForm formA)
        {
            InitializeComponent();
            hp = formA; 
        }


        public void SetWarehouseId(int warehouseId)
        {
            textBox1.Text = warehouseId.ToString();
            load();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int warehouseId = Convert.ToInt32(textBox1.Text.Trim());  // Get the WarehouseId from textBox1

            // Create an instance of the AddingWarehouseProduct form and pass the WarehouseId
            using (var addingProductForm = new AddingWarehouseProduct(warehouseId))
            {
                // Show the form as a dialog
                if (addingProductForm.ShowDialog() == DialogResult.OK)
                {
                    load();
                 
                    // Refresh the DataGridView after adding a product
                }
            }

        }
     
        private void load()
        {
            int ma = Convert.ToInt32(textBox1.Text);  // Get the WarehouseId from textBox1

            // Use a parameterized query to prevent SQL injection and group by ProductId
            string sql = @"  SELECT Warehouse_Id, ProductId, SUM(Quantity) AS TotalQuantity  FROM ClothesShopManagement.dbo.WarehouseProduct     WHERE Warehouse_Id = '" + ma+"'   GROUP BY Warehouse_Id, ProductId";

            dataGridView1.DataSource = CRUD_Data.GetData(sql);
            StyleSet.DataGridViewStyle(dataGridView1);
            dataGridView1.Columns["Warehouse_Id"].HeaderText = "Mã kho";
            dataGridView1.Columns["ProductId"].HeaderText = "Mã sản phẩm";
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        }

        private void ViewingWarehoueProduct_Load(object sender, EventArgs e)
        {
            load();
        }
    }
}
