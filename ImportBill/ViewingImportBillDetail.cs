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
    public partial class ViewingImportBillDetail : Form
    {
        int id_ImportBill;
        public ViewingImportBillDetail(int importBill_Id)
        {
            InitializeComponent();

            id_ImportBill = importBill_Id;

            LoadImportedProduct();
            LoadDataRelated();
        }

        public void LoadImportedProduct()
        {
            StyleSet.DataGridViewStyle(dgvImportedProduct);
            string query = "SELECT p.ProductId, p.ProductName, p.Size, p.Price, p.Material, p.Quantity " +
                $"FROM ImportBillDetail ibd JOIN Product p ON ibd.ProductId = p.ProductId WHERE ibd.ImportBill_Id = {id_ImportBill}";

            dgvImportedProduct.DataSource = CRUD_Data.GetData(query);
            dgvImportedProduct.Columns["ProductId"].HeaderText = "Mã quần áo";
            dgvImportedProduct.Columns["ProductName"].HeaderText = "Tên quần áo";
            dgvImportedProduct.Columns["Size"].HeaderText = "Kích cỡ";
            dgvImportedProduct.Columns["Price"].HeaderText = "Đơn giá";
            dgvImportedProduct.Columns["Material"].HeaderText = "Chất liệu";
            dgvImportedProduct.Columns["Quantity"].HeaderText = "Số lượng";

            dgvImportedProduct.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        public void LoadDataRelated()
        {
            string query = "SELECT b.Name, ib.Total_Payment, ib.ImportDate " +
                   "FROM ImportBill ib " +
                   "JOIN Brand b ON ib.Brand_Id = b.Brand_Id " +
                   "WHERE ib.ImportBill_Id = @ImportBill_Id";

            string queryQuantity = "SELECT SUM(Quantity) FROM ImportBillDetail WHERE ImportBill_Id = @ImportBill_Id";

            using (SqlConnection connection = CRUD_Data.Connection())
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ImportBill_Id", id_ImportBill);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string brandName = reader.GetString(0);
                            double totalPayment = reader.GetDouble(1);
                            DateTime importedDate = reader.GetDateTime(2);

                            lblBrand.Text = brandName;
                            lblTotal.Text = totalPayment.ToString("N2") + " VND";
                            lblImportDate.Text = importedDate.ToString("yyyy-MM-dd HH:mm:ss");
                        }
                    }
                }


                using (SqlCommand quantityCommand = new SqlCommand(queryQuantity, connection))
                {
                    quantityCommand.Parameters.AddWithValue("@ImportBill_Id", id_ImportBill);

                    using (SqlDataReader reader = quantityCommand.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int totalQuantity = reader.GetInt32(0);
                            lblQuantity.Text = totalQuantity.ToString();
                        }
                    }
                }
            }
        }
    }
}
