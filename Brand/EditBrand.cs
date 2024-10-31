using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ClothesShopManagement.Brand
{
    public partial class EditBrand : Form
    {
        public ModelClass.Brand Brand { get; set; }

        public EditBrand(ModelClass.Brand brand)
        {
            InitializeComponent();
            Brand = brand;

            textBox1.Text = Brand.BrandId;
            textBox2.Text = Brand.Name;
            textBox3.Text = Brand.Address;
            textBox4.Text = Brand.PhoneNumber;

            // Make Brand_Id textbox read-only
            textBox1.ReadOnly = true; // Or you can disable the textbox if you don't want it visible
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            UpdateBrand();
        }

        private void UpdateBrand()
        {
            // No need to set Brand.BrandId since it's read-only
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Brand.Name = textBox2.Text;
            Brand.Address = textBox3.Text;
            Brand.PhoneNumber = textBox4.Text;

            string sql = "UPDATE ClothesShopManagement.dbo.Brand SET Name = @Name, Address = @Address, PhoneNumber = @PhoneNumber WHERE Brand_Id = @BrandId";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Name", Brand.Name),
                new SqlParameter("@Address", Brand.Address),
                new SqlParameter("@PhoneNumber", Brand.PhoneNumber),
                new SqlParameter("@BrandId", Brand.BrandId) // Ensure the Brand_Id is passed for the WHERE clause
            };

            int rowsAffected = CRUD_Data.ExecuteNonQuery(sql, parameters);
            if (rowsAffected > 0)
            {
                MessageBox.Show("Brand updated successfully!");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Error updating brand. Please try again.");
            }
        }
    }
}
