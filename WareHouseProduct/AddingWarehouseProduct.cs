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
        public int WarehouseId { get; set; }
        public AddingWarehouseProduct()
        {
            InitializeComponent();
        }
        public AddingWarehouseProduct(int warehouseId)
        {
            InitializeComponent();
            WarehouseId = warehouseId;
            LoadProductDataToComboBox();
           
        }
        private void LoadProductDataToComboBox()
        {
            string sql = "SELECT ProductId, ProductName, Quantity FROM ClothesShopManagement.dbo.Product";
            DataTable productsTable = CRUD_Data.GetData(sql);

            comboBox1.DataSource = productsTable;
            comboBox1.DisplayMember = "ProductId";
            comboBox1.ValueMember = "ProductId";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedValue != null)
            {
                // Try to safely convert the SelectedValue to an integer
                if (int.TryParse(comboBox1.SelectedValue.ToString(), out int selectedProductId))
                {
                    // SQL query to get product details by ProductId
                    string sql = "SELECT ProductName, Quantity FROM ClothesShopManagement.dbo.Product WHERE ProductId = @ProductId";
                    SqlParameter param = new SqlParameter("@ProductId", selectedProductId);

                    // Execute the query to get product details
                    DataTable productDetails = CRUD_Data.GetDataWithParameter(sql, param);

                    // Check if any product details were returned
                    if (productDetails.Rows.Count > 0)
                    {
                        DataRow row = productDetails.Rows[0];
                        textBox2.Text = row["ProductName"].ToString();  // Set product name
                        textBox1.Text = row["Quantity"].ToString();    // Set quantity
                    }
                    else
                    {
                        // Clear textboxes if no product is found
                        textBox2.Clear();
                        textBox1.Clear();
                        MessageBox.Show("Product not found.");
                    }
                }
                else
                {
                    MessageBox.Show("Invalid product ID. Please select a valid product.");
                }
            }
            else
            {
                MessageBox.Show("Please select a product.");
            }

        }

        private void AddingWarehouseProduct_Load(object sender, EventArgs e)
        {
            LoadProductDataToComboBox();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int productId = Convert.ToInt32(comboBox1.SelectedValue.ToString());
            string quantity = textBox1.Text.Trim();

            // Include Warehouse_Id in the SQL query for insertion
            string sql = "INSERT INTO ClothesShopManagement.dbo.WarehouseProduct (Warehouse_Id, ProductId, Quantity) VALUES (@WarehouseId, @ProductId, @Quantity)";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@WarehouseId", WarehouseId),
                new SqlParameter("@ProductId", productId),
                new SqlParameter("@Quantity", quantity)
            };

            int rowsAffected = CRUD_Data.ExecuteNonQuery(sql, parameters);
            if (rowsAffected > 0)
            {
                MessageBox.Show("Product added to Warehouse successfully!");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Error adding product. Please try again.");
            }
        
    }
    }
    }
