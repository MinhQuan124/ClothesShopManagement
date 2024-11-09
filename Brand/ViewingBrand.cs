using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace ClothesShopManagement.Brand
{
    public partial class ViewingBrand : Form
    {
        public ViewingBrand()
        {
            InitializeComponent();
        }

        private void ViewingBrand_Load(object sender, EventArgs e)
        {
            LoadBrandData();
        }

        private void LoadBrandData()
        {
            string sql = "SELECT Brand_Id, Name, Address, PhoneNumber FROM ClothesShopManagement.dbo.Brand";
            dataGridView1.DataSource = CRUD_Data.GetData(sql);
            StyleSet.DataGridViewStyle(dataGridView1);
            //CustomizeDataGridView();
            dataGridView1.Columns["Brand_Id"].HeaderText = "Mã thương hiệu";
            dataGridView1.Columns["Name"].HeaderText = "Tên thương hiệu";
            dataGridView1.Columns["PhoneNumber"].HeaderText = "Số điện thoại";
            dataGridView1.Columns["Address"].HeaderText = "Địa chỉ";
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        }

        private void CustomizeDataGridView()
        {
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                Font = new Font("Arial", 10, FontStyle.Bold),
                BackColor = Color.LightGray
            };
            dataGridView1.DefaultCellStyle = new DataGridViewCellStyle
            {
                Font = new Font("Arial", 9),
                BackColor = Color.Beige
            };
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.LightYellow;
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView1.GridColor = Color.LightGray;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.ReadOnly = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string searchValue = textBox1.Text.Trim();

            if (string.IsNullOrEmpty(searchValue))
            {
                LoadBrandData();
            }
            else
            {
                string sql = "SELECT Brand_Id, Name, Address, PhoneNumber FROM ClothesShopManagement.dbo.Brand WHERE Name LIKE @searchValue";
                SqlParameter param = new SqlParameter("@searchValue", "%" + searchValue + "%");

                dataGridView1.DataSource = CRUD_Data.GetDataWithParameter(sql, param);
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var selectedRow = dataGridView1.Rows[e.RowIndex];
                var brand = new ModelClass.Brand
                {
                    BrandId = selectedRow.Cells["Brand_Id"].Value.ToString(),
                    Name = selectedRow.Cells["Name"].Value.ToString(),
                    Address = selectedRow.Cells["Address"].Value.ToString(),
                    PhoneNumber = selectedRow.Cells["PhoneNumber"].Value.ToString()
                };

                using (var editBrandForm = new EditBrand(brand))
                {
                    if (editBrandForm.ShowDialog() == DialogResult.OK)
                    {
                        LoadBrandData();
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (AddingBrand addBrandForm = new AddingBrand())
            {
                if (addBrandForm.ShowDialog() == DialogResult.OK)
                {
                    LoadBrandData();
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Get the selected row
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                string brandId = selectedRow.Cells["Brand_Id"].Value.ToString();

                // Confirm deletion
                DialogResult confirmResult = MessageBox.Show($"Bạn có chắc muốn xóa thương hiệu {brandId}?", "Xác nhận xóa", MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    // SQL query to delete the selected brand from the database
                    string sql = "DELETE FROM ClothesShopManagement.dbo.Brand WHERE Brand_Id = @BrandId";
                    SqlParameter[] parameters = new SqlParameter[]
                    {
                new SqlParameter("@BrandId", brandId)
                    };

                    // Execute the delete
                    int rowsAffected = CRUD_Data.ExecuteNonQuery(sql, parameters);
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Xóa thương hiệu thành công");
                        LoadBrandData(); // Refresh the DataGridView
                    }
                    else
                    {
                        MessageBox.Show("Lỗi. Vui lòng thử lại");
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn thương hiệu muốn xóa");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Check if a row is selected
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Get the selected row
                var selectedRow = dataGridView1.SelectedRows[0];
                var brand = new ModelClass.Brand
                {
                    BrandId = selectedRow.Cells["Brand_Id"].Value.ToString(),
                    Name = selectedRow.Cells["Name"].Value.ToString(),
                    Address = selectedRow.Cells["Address"].Value.ToString(),
                    PhoneNumber = selectedRow.Cells["PhoneNumber"].Value.ToString()
                };

                // Open the EditBrand form
                using (var editBrandForm = new EditBrand(brand))
                {
                    if (editBrandForm.ShowDialog() == DialogResult.OK)
                    {
                        LoadBrandData(); // Refresh the DataGridView after editing
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn thương hiệu để chỉnh sửa.");
            }
        }
    }
}
    

