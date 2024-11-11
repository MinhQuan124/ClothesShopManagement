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
    public partial class AddingWarehouseProduct : Form
    {
        public ViewingWarehouse warehouse;
        public int WarehouseId { get; set; }

        public AddingWarehouseProduct(int warehouseId)
        {
            InitializeComponent();
            WarehouseId = warehouseId;

            LoadProductDataToComboBox();

        }
        private void LoadProductDataToComboBox()
        {
            string sql = "SELECT ImportBill_Id FROM ClothesShopManagement.dbo.ImportBill";
            DataTable importBillsTable = CRUD_Data.GetData(sql);

            comboBox1.DataSource = importBillsTable;
            comboBox1.DisplayMember = "ImportBill_Id";
            comboBox1.ValueMember = "ImportBill_Id";
            LoadUnaddedImportBills();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedImportBillId = 0;

            if (comboBox1.SelectedValue != null)
            {
                if (comboBox1.SelectedValue is DataRowView rowView)
                {
                    selectedImportBillId = Convert.ToInt32(rowView["ImportBill_Id"]);
                }
                else
                {
                    selectedImportBillId = Convert.ToInt32(comboBox1.SelectedValue);
                }

                // Query to get products from ImportBillDetail
                string sql = @"
                    SELECT ProductId, Quantity 
                    FROM ClothesShopManagement.dbo.ImportBillDetail
                    WHERE ImportBill_Id = @ImportBillId";

                SqlParameter param = new SqlParameter("@ImportBillId", selectedImportBillId);
                DataTable productIdsTable = CRUD_Data.GetDataWithParameter(sql, param);

                // Show products in ComboBox2
                if (productIdsTable.Rows.Count > 0)
                {
                    comboBox2.DataSource = productIdsTable;
                    comboBox2.DisplayMember = "ProductId";
                    comboBox2.ValueMember = "ProductId";
                }
                else
                {
                    comboBox2.DataSource = null;
                    MessageBox.Show("Không tìm thấy sản phẩm trong hóa đơn nhập.");
                }
            }
        }

        private void AddingWarehouseProduct_Load(object sender, EventArgs e)
        {
            LoadProductDataToComboBox();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            int selectedImportBillId = Convert.ToInt32(comboBox1.SelectedValue);

            // Kiểm tra xem hóa đơn nhập đã được nhập vào bất kỳ kho nào trước đó chưa
            string checkBillSql = @"
SELECT COUNT(*)
FROM ClothesShopManagement.dbo.WarehouseProduct
WHERE ProductId IN (
    SELECT ProductId 
    FROM ClothesShopManagement.dbo.ImportBillDetail
    WHERE ImportBill_Id = @ImportBillId
)";

            SqlParameter checkBillParam = new SqlParameter("@ImportBillId", selectedImportBillId);
            int existingBillCount = Convert.ToInt32(CRUD_Data.ExecuteScalar(checkBillSql, new SqlParameter[] { checkBillParam }));

            if (existingBillCount > 0)
            {
                MessageBox.Show("Hóa đơn nhập này đã được nhập vào một nhà kho trước đó và không thể nhập thêm vào bất kỳ nhà kho nào khác.");
            }
            else
            {
                // Lấy danh sách sản phẩm và số lượng từ chi tiết hóa đơn nhập
                string sql = @"
    SELECT ProductId, Quantity 
    FROM ClothesShopManagement.dbo.ImportBillDetail
    WHERE ImportBill_Id = @ImportBillId";

                SqlParameter param = new SqlParameter("@ImportBillId", selectedImportBillId);
                DataTable productDetails = CRUD_Data.GetDataWithParameter(sql, param);

                if (productDetails.Rows.Count > 0)
                {
                    int totalQuantityInBill = 0;

                    // Tính tổng số lượng tất cả sản phẩm trong hóa đơn nhập
                    foreach (DataRow row in productDetails.Rows)
                    {
                        int quantity = Convert.ToInt32(row["Quantity"]);
                        totalQuantityInBill += quantity;
                    }

                    // Kiểm tra tồn kho của kho hiện tại
                    string warehouseSql = @"
        SELECT ISNULL(Stock, 0)
        FROM ClothesShopManagement.dbo.Warehouse
        WHERE Warehouse_Id = @WarehouseId";

                    SqlParameter[] warehouseParams = new SqlParameter[]
                    {
            new SqlParameter("@WarehouseId", WarehouseId) // Dùng ID kho hiện tại
                    };

                    // Thực thi để lấy số lượng tồn kho của kho hiện tại
                    int availableStock = Convert.ToInt32(CRUD_Data.ExecuteScalar(warehouseSql, warehouseParams));

                    // Kiểm tra nếu tổng số lượng sản phẩm trong bill vượt quá tồn kho của kho hiện tại
                    if (totalQuantityInBill > availableStock)
                    {
                        MessageBox.Show("Không thể thêm hóa đơn nhập này vào kho. Tổng số lượng sản phẩm vượt quá sức chứa hiện tại.");
                    }
                    else
                    {
                        // Logic thêm sản phẩm vào kho nếu tổng số lượng không vượt quá tồn kho
                        foreach (DataRow row in productDetails.Rows)
                        {
                            int productId = Convert.ToInt32(row["ProductId"]);
                            int quantity = Convert.ToInt32(row["Quantity"]);

                            // Thêm sản phẩm vào WarehouseProduct
                            string insertSql = @"
                INSERT INTO ClothesShopManagement.dbo.WarehouseProduct (Warehouse_Id, ProductId, Quantity)
                VALUES (@WarehouseId, @ProductId, @Quantity)";

                            SqlParameter[] insertParams = new SqlParameter[]
                            {
                    new SqlParameter("@WarehouseId", WarehouseId),
                    new SqlParameter("@ProductId", productId),
                    new SqlParameter("@Quantity", quantity)
                            };

                            int rowsInserted = CRUD_Data.ExecuteNonQuery(insertSql, insertParams);

                            if (rowsInserted > 0)
                            {
                                this.DialogResult = DialogResult.OK;
                                this.Close();

                                MessageBox.Show($"Quần áo có mã {productId} đã được thêm vào Kho {WarehouseId}.");
                            }
                        }
                    }
                }
            }


        }

        private void LoadUnaddedImportBills()
        {
            string sql = @"
        SELECT 
           
            ibd.ImportBill_Id, 
            ibd.ProductId, 
            ibd.Quantity, 
            ibd.UnitPrice, 
            ibd.Total
        FROM 
            ClothesShopManagement.dbo.ImportBillDetail ibd
        WHERE 
            NOT EXISTS (
                SELECT 1
                FROM ClothesShopManagement.dbo.WarehouseProduct wp
                WHERE wp.ProductId = ibd.ProductId
            )";

            DataTable unaddedImportBillDetails = CRUD_Data.GetData(sql);
            dataGridView1.DataSource = unaddedImportBillDetails;

            StyleSet.DataGridViewStyle(dataGridView1);
            dataGridView1.Columns["ImportBill_Id"].HeaderText = "Mã Hóa Đơn Nhập";
            dataGridView1.Columns["ProductId"].HeaderText = "Mã Sản Phẩm";
            dataGridView1.Columns["Quantity"].HeaderText = "Số Lượng";
            dataGridView1.Columns["UnitPrice"].HeaderText = "Đơn Giá";
            dataGridView1.Columns["Total"].HeaderText = "Tổng Giá";

         
        }




        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedValue != null && int.TryParse(comboBox2.SelectedValue.ToString(), out int selectedProductId))
            {
                // Tạo SqlParameter mới cho câu truy vấn đầu tiên
                SqlParameter param1 = new SqlParameter("@ProductId", selectedProductId);

                // Lấy thông tin chi tiết sản phẩm từ ImportBillDetail
                string sql = "SELECT Quantity FROM ClothesShopManagement.dbo.ImportBillDetail WHERE ProductId = @ProductId";
                DataTable productDetails = CRUD_Data.GetDataWithParameter(sql, param1);

                if (productDetails.Rows.Count > 0)
                {
                    DataRow row = productDetails.Rows[0];
                    textBox1.Text = row["Quantity"].ToString();

                    // Tạo SqlParameter mới cho câu truy vấn thứ hai
                    SqlParameter param2 = new SqlParameter("@ProductId", selectedProductId);

                    // Truy vấn để lấy tên sản phẩm từ bảng Product
                    string sqlProductName = "SELECT ProductName FROM ClothesShopManagement.dbo.Product WHERE ProductId = @ProductId";
                    DataTable productName = CRUD_Data.GetDataWithParameter(sqlProductName, param2);

                    if (productName.Rows.Count > 0)
                    {
                        textBox2.Text = productName.Rows[0]["ProductName"].ToString();
                    }
                    else
                    {
                        textBox2.Clear();
                        MessageBox.Show("Không tìm thấy tên sản phẩm.");
                    }
                }
                else
                {
                    textBox1.Clear();
                    textBox2.Clear();
                    MessageBox.Show("Không tìm thấy chi tiết sản phẩm.");
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra xem chỉ số hàng có hợp lệ không
            if (e.RowIndex >= 0)
            {
                // Lấy hàng được chọn
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];

                // Kiểm tra nếu cột "ImportBill_Id" và cột mã sản phẩm tồn tại trong DataGridView
                if (selectedRow.Cells["ImportBill_Id"] != null && selectedRow.Cells["ImportBill_Id"].Value != DBNull.Value)
                {
                    // Gán giá trị ImportBill_Id từ DataGridView vào ComboBox
                    comboBox1.SelectedValue = selectedRow.Cells["ImportBill_Id"].Value;
                }

                if (selectedRow.Cells["ProductId"] != null && selectedRow.Cells["ProductId"].Value != DBNull.Value)
                {
                    // Gán giá trị ProductId vào TextBox hoặc làm bất kỳ xử lý nào cần thiết
                    comboBox2.Text = selectedRow.Cells["ProductId"].Value.ToString();
                }
                if (selectedRow.Cells["ProductId"] != null && selectedRow.Cells["ProductId"].Value != DBNull.Value)
                {
                    // Gán giá trị ProductId vào TextBox hoặc làm bất kỳ xử lý nào cần thiết
                    comboBox2.Text = selectedRow.Cells["ProductId"].Value.ToString();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}


    

        
    

