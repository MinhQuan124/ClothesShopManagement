using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClothesShopManagement.ImportBill;
using ClothesShopManagement.ModelClass;

namespace ClothesShopManagement.Product
{
    public partial class AddingProduct : Form
    {
        private int idBrand;
        //Khai bao su kien gui ve import form
        public event Action<List<ModelClass.Product>> ProductsAdded;

        //Khai báo sự kiện gửi số lượng về import form
        public event Action<int> QuantityUpdated;

        //Khai bao su kien gui tong tien ve import form
        public event Action<float> TotalPriceUpdated;

        private List<ModelClass.Product> productList = new List<ModelClass.Product>();
        public List<ModelClass.Product> Products => productList; //Tao thuoc tinh Products lay danh sach san pham
        public AddingProduct(int selectedBrand_Id)
        {
            InitializeComponent();
            idBrand = selectedBrand_Id;
            LoadBrand();

            cmbBrand.SelectedValue = idBrand;
            cmbBrand.Enabled = false;
        }
        private void LoadBrand()
        {
            DataTable brandTable = CRUD_Data.GetData("SELECT Brand_Id, Name FROM Brand");

            cmbBrand.DataSource = brandTable;
            cmbBrand.DisplayMember = "Name";
            cmbBrand.ValueMember = "Brand_Id";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtName.Text) || !string.IsNullOrEmpty(txtMaterial.Text) || !string.IsNullOrEmpty(txtSize.Text))
            {
                int quantity;
                if (int.TryParse(txtQuantity.Text, out quantity) && quantity > 0)
                {
                    float price;
                    if (float.TryParse(txtPrice.Text, out price) && price > 0)
                    {
                        var selectedBrandId = Convert.ToInt32(cmbBrand.SelectedValue);
                        string brandName = cmbBrand.Text;

                        var product = new ModelClass.Product
                        {
                            productName = txtName.Text.Trim(),
                            size = txtSize.Text.Trim(),
                            price = price,
                            material = txtMaterial.Text.Trim(),
                            quantity = quantity,
                            brandId = selectedBrandId,
                            brandName = brandName
                        };

                        productList.Add(product);

                        UpdateTotalQuantity();
                        UpdateTotalPrice();

                        MessageBox.Show("Thêm quần/ áo thành công", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.None);
                        ClearInputs();

                    }
                    else
                    {
                        MessageBox.Show("Đơn giá không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Số lượng không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.");
            }
            
        }
        private void UpdateTotalQuantity()
        {
            int totalQuantity = productList.Sum(p => p.quantity);
            QuantityUpdated?.Invoke(totalQuantity);
        }
        private void UpdateTotalPrice()
        {
            float totalPrice = productList.Sum(p => p.quantity * p.price);
            TotalPriceUpdated?.Invoke(totalPrice);
        }
        private void ClearInputs()
        {
            txtName.Clear();
            txtSize.Clear();
            txtPrice.Clear();
            txtMaterial.Clear();
            txtQuantity.Clear();
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            // Kích hoạt sự kiện và gửi danh sách sản phẩm
            ProductsAdded?.Invoke(productList);

            //Kích hoạt sự kiện update combobox
            if (this.Owner is btnAddBrand addingForm)
            {
                addingForm.UpdateBrandSelection();
            }

            this.Close();
        }
    }
}
