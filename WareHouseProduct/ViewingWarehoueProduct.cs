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
       

        // Phương thức để nhận ID nhà kho từ form Warehouse
        public void SetWarehouseId(string warehouseId)
        {
            textBox1.Text = warehouseId;  
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
