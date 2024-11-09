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
    public partial class EditingImportBill : Form
    {
        private int importBill_Id;
        public EditingImportBill(int idImportBill)
        {
            InitializeComponent();
            importBill_Id = idImportBill;

            labelTotal.Text = importBill_Id.ToString();
            LoadData();
            LoadImportedProduct();
        }

        public void LoadImportedProduct()
        {
            StyleSet.DataGridViewStyle(dgvProduct);
            string query = "SELECT p.ProductId, p.ProductName, p.Size, p.Price, p.Material, p.Quantity " +
                $"FROM ImportBillDetail ibd JOIN Product p ON ibd.ProductId = p.ProductId WHERE ibd.ImportBill_Id = {importBill_Id}";

            dgvProduct.DataSource = CRUD_Data.GetData(query);
            dgvProduct.Columns["ProductId"].HeaderText = "Mã quần áo";
            dgvProduct.Columns["ProductName"].HeaderText = "Tên quần áo";
            dgvProduct.Columns["Size"].HeaderText = "Kích cỡ";
            dgvProduct.Columns["Price"].HeaderText = "Đơn giá";
            dgvProduct.Columns["Material"].HeaderText = "Chất liệu";
            dgvProduct.Columns["Quantity"].HeaderText = "Số lượng";

            dgvProduct.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        public void LoadData()
        {
            string query = "SELECT SUM(Quantity) FROM ImportBillDetail WHERE ImportBill_Id = @ImportBill_Id";

            string loadBrandsQuery = "SELECT Brand_Id, Name FROM Brand";

            string queryImportBill = "SELECT b.Name, ib.Total_Payment, ib.ImportDate " +
                          "FROM ImportBill ib " +
                          "INNER JOIN Brand b ON ib.Brand_Id = b.Brand_Id " +
                          "WHERE ib.ImportBill_Id = @ImportBill_Id";

            using (SqlConnection connect = CRUD_Data.Connection())
            {
                connect.Open();

                //Load brand
                using (SqlCommand loadCommand = new SqlCommand(loadBrandsQuery, connect))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(loadCommand);
                    DataTable brandTable = new DataTable();
                    adapter.Fill(brandTable);

                    // Gán dữ liệu vào ComboBox
                    cmbBrand.DataSource = brandTable;
                    cmbBrand.DisplayMember = "Name";
                    cmbBrand.ValueMember = "Brand_Id";
                }

                using (SqlCommand cmd = new SqlCommand(query, connect))
                {
                    cmd.Parameters.AddWithValue("@ImportBill_Id", importBill_Id);

                    //Get sum quantity
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int quantity = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);

                            txtQuantity.Text = quantity.ToString();
                        }
                    }
                }

                //Get brand name, total, import date
                using (SqlCommand cmd = new SqlCommand(queryImportBill, connect))
                {
                    cmd.Parameters.AddWithValue("@ImportBill_Id", importBill_Id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string brandName = reader.GetString(0);
                            cmbBrand.SelectedIndex = cmbBrand.FindStringExact(brandName);

                            double total = reader.IsDBNull(1) ? 0 : reader.GetDouble(1);
                            DateTime importDate = reader.GetDateTime(2);

                            labelTotal.Text = total.ToString("N2") + " VND";
                            dateTimePicker1.Value = importDate;
                        }
                    }
                }
            }

        }

        private void dgvProduct_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (cmbBrand.Items.Count != 0)
                {
                    int idProduct = Convert.ToInt32(dgvProduct.SelectedRows[0].Cells["ProductId"].Value);

                    new ModifyingProduct(importBill_Id,idProduct).ShowDialog();
                    LoadData();
                    LoadImportedProduct();
                }
                else
                {
                    MessageBox.Show("Không có thương hiệu(nhãn hàng)! Vui lòng thêm thương hiệu.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnSubmitBill_Click(object sender, EventArgs e)
        {
            int selectedBrand = (int)cmbBrand.SelectedValue;
            using (SqlConnection connection = CRUD_Data.Connection())
             {
                connection.Open();

                //Update import bill
                string query = "Update ImportBill SET Brand_Id = @Brand_Id, ImportDate = @ImportDate WHERE ImportBill_Id = @ImportBill_Id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    DateTime date = DateTime.Now;

                    command.Parameters.AddWithValue("@Brand_Id", selectedBrand);
                    command.Parameters.AddWithValue("@ImportDate", date.ToString("yyyy-MM-dd HH:mm:ss"));
                    command.Parameters.AddWithValue("@ImportBill_Id", importBill_Id);

                    command.ExecuteNonQuery();
                }

                //Update product
                string productQuery = "UPDATE p SET p.Brand_Id = @NewBrandId FROM Product p " +
                            "INNER JOIN ImportBillDetail ibd ON p.ProductId = ibd.ProductId " +
                            "INNER JOIN ImportBill ib ON ibd.ImportBill_Id = ib.ImportBill_Id " +
                            "WHERE ib.ImportBill_Id = @ImportBillId";

                using (SqlCommand command = new SqlCommand(productQuery, connection))
                {
                    command.Parameters.AddWithValue("@NewBrandId", selectedBrand);
                    command.Parameters.AddWithValue("@ImportBillId", importBill_Id);
                    command.ExecuteNonQuery();
                }
                MessageBox.Show("Cập nhật hóa đơn thành công.");
                this.Close();
            }
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
