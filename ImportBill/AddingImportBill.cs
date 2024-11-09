using ClothesShopManagement.Brand;
using ClothesShopManagement.ModelClass;
using ClothesShopManagement.Product;
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

namespace ClothesShopManagement.ImportBill
{
    public partial class btnAddBrand : Form
    {
        private List<ModelClass.Product> newProducts = new List<ModelClass.Product>();
        private float totalPayment = 0;
        private int totalAmount = 0;

        public btnAddBrand()
        {
            InitializeComponent();
            LoadBrand();
        }

        private void LoadBrand()
        {
            DataTable brandTable = CRUD_Data.GetData("SELECT Brand_Id, Name FROM Brand");
            cmbBrand.DataSource = brandTable;
            cmbBrand.DisplayMember = "Name";
            cmbBrand.ValueMember = "Brand_Id";
        }

        //Adding Product form
        private void btn_AddProduct_Click(object sender, EventArgs e)
        {
           if(cmbBrand.Items.Count != 0)
            {
                //Lay id da chon trong comboBox Brand
                int selectedBrand_Id = Convert.ToInt32(cmbBrand.SelectedValue);


                // Tạo instance của form AddingProduct
                var addingProductForm = new AddingProduct(selectedBrand_Id);

                // Đăng ký sự kiện ProductsAdded để nhận danh sách sản phẩm khi form đóng
                addingProductForm.ProductsAdded += products =>
                {
                    newProducts.AddRange(products); // Thêm danh sách sản phẩm vào newProduct Lít
                    UpdateDataGridView();
                };

                //Đăng ký sự kiện nhân tổng số lượng
                addingProductForm.QuantityUpdated += UpdateQuantityStatus;

                //Đămng ký sự kiện lấy tổng số tiền
                addingProductForm.TotalPriceUpdated += UpdateTotalPrice;

                addingProductForm.ShowDialog();
                UpdateBrandSelection();
            }
            else
            {
                MessageBox.Show("Không có thương hiệu(nhãn hàng)! Vui lòng thêm thương hiệu.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void UpdateDataGridView()
        {
            dgvProduct.DataSource = null;
            dgvProduct.DataSource = newProducts;

            StyleSet.DataGridViewStyle(dgvProduct);

            dgvProduct.Columns["productName"].HeaderText = "Tên Sản Phẩm";
            dgvProduct.Columns["size"].HeaderText = "Kích Thước";
            dgvProduct.Columns["price"].HeaderText = "Đơn giá";
            dgvProduct.Columns["material"].HeaderText = "Chất Liệu";
            dgvProduct.Columns["quantity"].HeaderText = "Số Lượng";
            dgvProduct.Columns["brandName"].HeaderText = "Nhãn hàng/Thương hiệu";


            dgvProduct.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgvProduct.Columns["price"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgvProduct.Columns["quantity"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvProduct.Columns["productName"].MinimumWidth = 150;
            dgvProduct.Columns["size"].MinimumWidth = 30;
            dgvProduct.Columns["price"].MinimumWidth = 80;
            dgvProduct.Columns["material"].MinimumWidth = 100;
            dgvProduct.Columns["quantity"].MinimumWidth = 80;
            dgvProduct.Columns["brandName"].MinimumWidth = 80;

            dgvProduct.Columns["productId"].Visible = false;
            dgvProduct.Columns["brandId"].Visible = false;
        }
        private void UpdateQuantityStatus(int totalQuantity)
        {
            totalAmount = totalQuantity;
            txtQuantity.Text = totalQuantity.ToString(); 
        }
        private void UpdateTotalPrice(float totalPrice)
        {
            totalPayment = totalPrice;
            labelTotal.Text = totalPrice.ToString("C", new System.Globalization.CultureInfo("vi-VN"));
        }

        public void UpdateBrandSelection()
        {
            cmbBrand.Enabled = false;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSubmitBill_Click(object sender, EventArgs e)
        {
            if (dgvProduct.Rows.Count > 0)
            {
                using (SqlConnection connection = CRUD_Data.Connection())
                {
                    connection.Open();

                    // Thêm hóa đơn nhập
                    string insertBillQuery = "INSERT INTO ImportBill (Brand_Id, Total_Payment, ImportDate) " +
                                              "VALUES (@Brand_Id, @Total_Payment, @ImportDate); " +
                                              "SELECT CAST(scope_identity() AS int);"; // Lấy ID của hóa đơn mới tạo

                    int importBillId;

                    int brandId = Convert.ToInt32(cmbBrand.SelectedValue);

                    DateTime importDate = DateTime.Now;
                    string date = importDate.ToString("yyyy-MM-dd HH:mm:ss");

                    using (SqlCommand command = new SqlCommand(insertBillQuery, connection))
                    {
                        command.Parameters.AddWithValue("@Brand_Id", brandId);
                        command.Parameters.AddWithValue("@Total_Payment", totalPayment);
                        command.Parameters.AddWithValue("@ImportDate", date);

                        importBillId = (int)command.ExecuteScalar(); 
                    }

                    string insertProductQuery = "INSERT INTO Product (ProductName, Size, Price, Material, Quantity, Brand_Id) " +
                                  "VALUES (@ProductName, @Size, @Price, @Material, @Quantity, @Brand_Id); " +
                                  "SELECT CAST(scope_identity() AS int);"; // Lấy ID của sản phẩm mới tạo

                    // Thêm chi tiết hóa đơn cho nhiều sản phẩm
                    string insertImportBillDetailQuery = "INSERT INTO ImportBillDetail (ImportBill_Id, ProductId, Quantity, UnitPrice) " +
                                                "VALUES (@ImportBill_Id, @ProductId, @Quantity, @UnitPrice);" +
                                                "SELECT CAST(scope_identity() AS int);";

                    foreach (var product in newProducts)
                    {
                        // Thêm sản phẩm vào bảng Product và lấy ProductId
                        int productId;
                        using (SqlCommand productCmd = new SqlCommand(insertProductQuery, connection))
                        {
                            productCmd.Parameters.AddWithValue("@ProductName",product.productName);
                            productCmd.Parameters.AddWithValue("@Size", product.size);
                            productCmd.Parameters.AddWithValue("@Price", product.price);
                            productCmd.Parameters.AddWithValue("@Material", product.material);
                            productCmd.Parameters.AddWithValue("@Quantity", product.quantity);
                            productCmd.Parameters.AddWithValue("@Brand_Id", product.brandId);

                            productId = (int)productCmd.ExecuteScalar(); // Nhận ID của sản phẩm mới tạo
                        }

                        // Thêm sản phẩm vào ImportBillDetail với ProductId
                        using (SqlCommand detailCommand = new SqlCommand(insertImportBillDetailQuery, connection))
                        {
                            detailCommand.Parameters.AddWithValue("@ImportBill_Id", importBillId);
                            detailCommand.Parameters.AddWithValue("@ProductId", productId);
                            detailCommand.Parameters.AddWithValue("@Quantity", product.quantity);
                            detailCommand.Parameters.AddWithValue("@UnitPrice", product.price);

                            detailCommand.ExecuteNonQuery(); 
                        }
                    }

                    MessageBox.Show("Hàng đã nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng nhập sản phẩm trước!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btn_DeleteImportBill_Click(object sender, EventArgs e)
        {
            new AddingBrand().ShowDialog();
            LoadBrand();
        }
    }
}
