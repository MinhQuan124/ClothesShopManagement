using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClothesShopManagement.Product
{
    public partial class ViewingProduct : Form
    {
        public ViewingProduct()
        {
            InitializeComponent();
            LoadProduct();
        }

        public void LoadProduct()
        {
            StyleSet.DataGridViewStyle(dgv_ViewProduct);
            string query = "SELECT p.ProductId, p.ProductName, p.Size, p.Price, p.Material, p.Quantity, b.Name " +
                "From Product p Inner join Brand b ON p.Brand_Id = b.Brand_Id ";

            dgv_ViewProduct.DataSource = CRUD_Data.GetData(query);
            dgv_ViewProduct.Columns["ProductId"].HeaderText = "Mã quần áo";
            dgv_ViewProduct.Columns["ProductName"].HeaderText = "Tên quần áo";
            dgv_ViewProduct.Columns["Size"].HeaderText = "Kích cỡ";
            dgv_ViewProduct.Columns["Price"].HeaderText = "Đơn giá";
            dgv_ViewProduct.Columns["Material"].HeaderText = "Chất liệu";
            dgv_ViewProduct.Columns["Quantity"].HeaderText = "Số lượng";
            dgv_ViewProduct.Columns["Name"].HeaderText = "Thương hiệu";

            dgv_ViewProduct.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void btn_AddProduct_Click(object sender, EventArgs e)
        {
            new ImportBill.btnAddBrand().ShowDialog(); //Lỗi hiển thị đúng tên form
            LoadProduct();
        }

        private void Search_Product()
        {
            string keyword = txtSearch.Text.Trim();
            string query = $"SELECT p.ProductId, p.ProductName, p.Size, p.Price, p.Material, p.Quantity, b.Name " +
                   $"FROM Product p " +
                   $"JOIN Brand b ON p.Brand_Id = b.Brand_Id " +
                   $"WHERE p.ProductName LIKE N'%{keyword}%' ";

            dgv_ViewProduct.DataSource = CRUD_Data.GetData(query);
        }
        private void txtSearch_KeyUp(object sender, KeyEventArgs e)
        {
            Search_Product();
        }
    }
}
