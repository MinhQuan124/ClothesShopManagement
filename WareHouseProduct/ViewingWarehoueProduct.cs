using ClothesShopManagement.Warehouse;
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

            // Truyền tham chiếu của form này vào AddingWarehouseProduct
            using (var addingProductForm = new AddingWarehouseProduct(warehouseId))
            {
                // Show the form as a dialog
                if (addingProductForm.ShowDialog() == DialogResult.OK)
                {
                    load(); // Load lại dữ liệu khi form AddingWarehouseProduct đóng và trả về DialogResult.OK
                }
            }
        }
        private void load()
        {
            int ma = Convert.ToInt32(textBox1.Text);  // Get the WarehouseId from textBox1

            // Truy vấn lấy thông tin sản phẩm kèm theo tên sản phẩm
            string sql = @"
        SELECT 
            wp.Warehouse_Id, 
            wp.ProductId, 
            p.ProductName, 
            SUM(wp.Quantity) AS TotalQuantity
        FROM 
            ClothesShopManagement.dbo.WarehouseProduct wp
        JOIN 
            ClothesShopManagement.dbo.Product p ON wp.ProductId = p.ProductId
        WHERE 
            wp.Warehouse_Id = @WarehouseId
        GROUP BY 
            wp.Warehouse_Id, wp.ProductId, p.ProductName";

            // Tạo tham số cho truy vấn
            SqlParameter[] parameters = new SqlParameter[]
            {
               new SqlParameter("@WarehouseId", ma)
            };

            // Đổ dữ liệu vào dataGridView
            dataGridView1.DataSource = CRUD_Data.GetDataWithParameter(sql, parameters);
            StyleSet.DataGridViewStyle(dataGridView1);

            // Đặt tiêu đề cho các cột
            dataGridView1.Columns["Warehouse_Id"].HeaderText = "Mã kho";
            dataGridView1.Columns["ProductId"].HeaderText = "Mã sản phẩm";
            dataGridView1.Columns["ProductName"].HeaderText = "Tên sản phẩm";
            dataGridView1.Columns["TotalQuantity"].HeaderText = "Tổng số lượng";
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        }

        private void ViewingWarehoueProduct_Load(object sender, EventArgs e)
        {
            load();

        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                // Lấy WarehouseId từ textBox1
                int warehouseId = Convert.ToInt32(textBox1.Text);

                // Hiển thị xác nhận trước khi xóa
                DialogResult dialogResult = MessageBox.Show(
                    $"Bạn có chắc chắn muốn xóa tất cả các sản phẩm trong kho có ID {warehouseId}?",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (dialogResult == DialogResult.Yes)
                {
                    // Thực hiện truy vấn để xóa tất cả sản phẩm trong kho
                    string deleteSql = @"
            DELETE FROM ClothesShopManagement.dbo.WarehouseProduct
            WHERE Warehouse_Id = @WarehouseId";

                    SqlParameter[] parameters = new SqlParameter[]
                    {
            new SqlParameter("@WarehouseId", warehouseId)
                    };

                    // Thực thi câu lệnh SQL
                    int rowsAffected = CRUD_Data.ExecuteNonQuery(deleteSql, parameters);

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Tất cả sản phẩm trong kho đã được xóa thành công.");
                        load(); // Tải lại dữ liệu sau khi xóa
                    }
                    else
                    {
                        MessageBox.Show("Không thể xóa sản phẩm. Vui lòng thử lại.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng nhập mã kho cần xóa.");
            }
        }
    }

}
