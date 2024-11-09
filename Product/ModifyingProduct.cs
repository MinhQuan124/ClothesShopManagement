using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClothesShopManagement.Product
{
    public partial class ModifyingProduct : Form
    {
        private int importBill_Id;
        private int productId;
        public ModifyingProduct(int idImportBill, int product_id)
        {
            InitializeComponent();

            importBill_Id = idImportBill;
            productId = product_id;

            LoadData();
        }
        public void LoadData()
        {
            string query = "SELECT ProductName, Size, Price, Material, Quantity FROM Product Where ProductId = @ProductId";

            using (SqlConnection connection = CRUD_Data.Connection())
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ProductId", productId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string name = reader.GetString(0);
                            string size = reader.GetString(1);
                            double price = reader.GetDouble(2);
                            string material = reader.GetString(3);
                            int quantity = reader.GetInt32(4);

                            txtName.Text = name;
                            txtSize.Text = size;
                            txtPrice.Text = price.ToString();
                            txtMaterial.Text = material;
                            txtQuantity.Text = quantity.ToString();
                        }
                    }
                }
            }
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            using(SqlConnection connection = CRUD_Data.Connection())
            {
                connection.Open();

                //Transaction để đảm báo tất cả các dữ liệu được cập nhật cùng nhau ( một cách an toàn )
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        string updateProductQuery = "UPDATE Product SET ProductName = @ProductName, Size = @Size, Price = @Price, Material = @Material, Quantity = @Quantity WHERE ProductId = @ProductId";
                        using (SqlCommand command = new SqlCommand(updateProductQuery, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@ProductId", productId);
                            command.Parameters.AddWithValue("@ProductName", txtName.Text);
                            command.Parameters.AddWithValue("@Size", txtSize.Text);
                            command.Parameters.AddWithValue("@Price", Convert.ToDouble(txtPrice.Text));
                            command.Parameters.AddWithValue("@Material", txtMaterial.Text);
                            command.Parameters.AddWithValue("@Quantity", Convert.ToInt32(txtQuantity.Text));
                            command.ExecuteNonQuery();
                        }

                        // cập nhật ImportBillDetail dựa trên ProductId
                        string updateDetailQuery = "UPDATE ImportBillDetail SET Quantity = @Quantity, UnitPrice = @UnitPrice WHERE ProductId = @ProductId";
                        using (SqlCommand detailCommand = new SqlCommand(updateDetailQuery, connection, transaction))
                        {
                            detailCommand.Parameters.AddWithValue("@ProductId", productId);
                            detailCommand.Parameters.AddWithValue("@Quantity", Convert.ToInt32(txtQuantity.Text));
                            detailCommand.Parameters.AddWithValue("@UnitPrice", Convert.ToDouble(txtPrice.Text));
                            detailCommand.ExecuteNonQuery();
                        }

                        //Cập nhật importBill
                        string updateImportBill = " UPDATE ImportBill SET Total_Payment = " +
                            "(SELECT SUM(Quantity * UnitPrice) FROM ImportBillDetail WHERE ImportBill_Id = @ImportBill_Id )" +
                            " WHERE ImportBill_Id = @ImportBill_Id";
                        using (SqlCommand importBillCommand = new SqlCommand(updateImportBill, connection, transaction))
                        {
                            importBillCommand.Parameters.AddWithValue("@ImportBill_Id", importBill_Id);
                            importBillCommand.ExecuteNonQuery();
                        }

                        transaction.Commit();
                        MessageBox.Show("Cập nhật sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }    
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string deleteDetailQuery = "DELETE FROM ImportBillDetail WHERE ProductId = @ProductId";
            string deleteProductQuery = "DELETE FROM Product WHERE ProductId = @ProductId";

            using (SqlConnection connection = CRUD_Data.Connection())
            {
                connection.Open();

                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand detailCommand = new SqlCommand(deleteDetailQuery, connection, transaction))
                        {
                            detailCommand.Parameters.AddWithValue("@ProductId", productId);
                            detailCommand.ExecuteNonQuery();
                        }

                        using (SqlCommand productCommand = new SqlCommand(deleteProductQuery, connection, transaction))
                        {
                            productCommand.Parameters.AddWithValue("@ProductId", productId);
                            productCommand.ExecuteNonQuery();
                        }

                        //Cập nhật importBill
                        string updateImportBill = " UPDATE ImportBill SET Total_Payment = " +
                            "(SELECT SUM(Quantity * UnitPrice) FROM ImportBillDetail WHERE ImportBill_Id = @ImportBill_Id )" +
                            " WHERE ImportBill_Id = @ImportBill_Id";
                        using (SqlCommand importBillCommand = new SqlCommand(updateImportBill, connection, transaction))
                        {
                            importBillCommand.Parameters.AddWithValue("@ImportBill_Id", importBill_Id);
                            importBillCommand.ExecuteNonQuery();
                        }

                        transaction.Commit();
                        MessageBox.Show("Xóa sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show("Lỗi khi xóa sản phẩm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

        }
    }
}
