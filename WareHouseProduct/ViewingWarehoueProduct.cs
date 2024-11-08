using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClothesShopManagement.WareHouseProduct
{
    public partial class ViewingWarehoueProduct : Form
    {
        public ViewingWarehoueProduct()
        {
            InitializeComponent();
        }
       

       
        public void SetWarehouseId(int warehouseId)
        {
            textBox1.Text = warehouseId.ToString();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int warehouseId = Convert.ToInt32(textBox1.Text.Trim()); // Assuming textBox1 contains the Warehouse_Id

            using (var addProductForm = new AddingWarehouseProduct(warehouseId))
            {
                if (addProductForm.ShowDialog() == DialogResult.OK)
                {
                    load(); // Refresh the DataGridView after adding a product
                }
            }
        }
        private void load()
        {
            string sql = "SELECT WarehouseProduct_Id, Warehouse_Id, ProductId, Quantity FROM ClothesShopManagement.dbo.WarehouseProduct";
            dataGridView1.DataSource = CRUD_Data.GetData(sql);
        }

        private void ViewingWarehoueProduct_Load(object sender, EventArgs e)
        {
            load();
        }
    }
}
