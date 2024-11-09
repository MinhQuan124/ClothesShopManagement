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
    public partial class ViewingImportBill : Form
    {
        public ViewingImportBill()
        {
            InitializeComponent();
            LoadImportBill();
        }

        public void LoadImportBill()
        {
            StyleSet.DataGridViewStyle(dgv_ViewImportBill);
            string query = "select ImportBill.ImportBill_Id, Brand.Name, ImportBill.Total_Payment, ImportBill.ImportDate from ImportBill" +
                " inner join Brand on ImportBill.Brand_Id = Brand.Brand_Id";

            dgv_ViewImportBill.DataSource = CRUD_Data.GetData(query);
            dgv_ViewImportBill.Columns["ImportBill_Id"].HeaderText = "Mã hóa đơn nhập";
            dgv_ViewImportBill.Columns["Name"].HeaderText = "Nhà cung cấp";
            dgv_ViewImportBill.Columns["Total_Payment"].HeaderText = "Tổng hóa đơn";
            dgv_ViewImportBill.Columns["ImportDate"].HeaderText = "Ngày nhập";


            dgv_ViewImportBill.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void btn_AddImportBill_Click(object sender, EventArgs e)
        {
            new btnAddBrand().ShowDialog();
            LoadImportBill();
        }

        private void btn_DeleteImportBill_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem có hóa đơn nào được chọn không
            if (dgv_ViewImportBill.SelectedRows.Count > 0)
            {
                // Lấy ID của hóa đơn đã chọn
                int idImportBill = Convert.ToInt32(dgv_ViewImportBill.SelectedRows[0].Cells["ImportBill_Id"].Value);

                using (SqlConnection connection = CRUD_Data.Connection())
                {
                    connection.Open();

                    //SqlTransaction: để đảm bảo tính nhất quán dữ liệu khi thực hiện các thao tác thêm, sửa hoặc xóa trên nhiều bảng
                    using (SqlTransaction transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            // Lấy danh sách sản phẩm
                            string productIdsQuery = "SELECT ProductId FROM ImportBillDetail WHERE ImportBill_Id = @ImportBill_Id;";
                            List<int> productIds = new List<int>();
                            using (SqlCommand productIdCommand = new SqlCommand(productIdsQuery, connection, transaction))
                            {
                                productIdCommand.Parameters.AddWithValue("@ImportBill_Id", idImportBill);
                                using (SqlDataReader reader = productIdCommand.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        productIds.Add(reader.GetInt32(0));
                                    }
                                }
                            }

                            // Xóa các chi tiết hóa đơn
                            string deleteDetailQuery = "DELETE FROM ImportBillDetail WHERE ImportBill_Id = @ImportBill_Id;";
                            using (SqlCommand detailCommand = new SqlCommand(deleteDetailQuery, connection, transaction))
                            {
                                detailCommand.Parameters.AddWithValue("@ImportBill_Id", idImportBill);
                                detailCommand.ExecuteNonQuery();
                            }

                            // Xóa các sản phẩm liên quan
                            if (productIds.Count > 0)
                            {
                                string productIdList = string.Join(",", productIds);
                                string deleteProductsQuery = $"DELETE FROM Product WHERE ProductId IN ({productIdList});";
                                using (SqlCommand productCommand = new SqlCommand(deleteProductsQuery, connection, transaction))
                                {
                                    productCommand.ExecuteNonQuery();
                                }
                            }

                            // Xóa hóa đơn nhập
                            string deleteBillQuery = "DELETE FROM ImportBill WHERE ImportBill_Id = @ImportBill_Id;";
                            using (SqlCommand deleteCmd = new SqlCommand(deleteBillQuery, connection, transaction))
                            {
                                deleteCmd.Parameters.AddWithValue("@ImportBill_Id", idImportBill);
                                deleteCmd.ExecuteNonQuery();
                            }

                            transaction.Commit();
                            MessageBox.Show("Hóa đơn và các sản phẩm liên quan đã được xóa thành công.");
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
                        }
                    }
                }

                // Cập nhật lại DataGridView
                LoadImportBill();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một hóa đơn để xóa.");
            }

        }

        private void dgv_ViewImportBill_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0)
            {
                //Click to get id
                int importBillId = Convert.ToInt32(dgv_ViewImportBill.Rows[e.RowIndex].Cells[0].Value);

                new ViewingImportBillDetail(importBillId).ShowDialog();
            }
        }

        private void btn_UpdateImportBill_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem có hóa đơn nào được chọn không
            if (dgv_ViewImportBill.SelectedRows.Count > 0)
            {
                int idImportBill = Convert.ToInt32(dgv_ViewImportBill.SelectedRows[0].Cells["ImportBill_Id"].Value);

                new EditingImportBill(idImportBill).ShowDialog();
                LoadImportBill();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn hóa đơn để sửa.");
            }
        }
    }
}
