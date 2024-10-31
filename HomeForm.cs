using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClothesShopManagement
{
    public partial class HomeForm : Form
    {
        private bool isAdmin;
        public HomeForm(bool isAdmin)
        {
            InitializeComponent();
            this.isAdmin = isAdmin;
            btnStaff.Visible = isAdmin;
        }

        private void ShowFormInPanel(Form form)
        {
            // Xóa nội dung hiện tại của Panel
            PanelForm.Controls.Clear();    
            // Thiết lập các thuộc tính của form con
            form.TopLevel = false;      // Không cho form con là TopLevel (form độc lập)
            form.Dock = DockStyle.Fill; // Cho phép form lấp đầy panel
            PanelForm.Controls.Add(form);  // Thêm form vào Panel
            form.Show();                // Hiển thị form con
           
        }



        private void button1_Click_1(object sender, EventArgs e)
        {
            ShowFormInPanel(new Brand.ViewingBrand());
        }

        private void btnCustomers_Click(object sender, EventArgs e)
        {
            ShowFormInPanel(new Customer.Form1());
        }

        private void btnBill_Click(object sender, EventArgs e)
        {
            ShowFormInPanel(new Bill.Form1());
        }

        private void btnStatistic_Click(object sender, EventArgs e)
        {
            ShowFormInPanel(new Statistics.ViewingStatictis());
        }

        private void btnWarehouse_Click(object sender, EventArgs e)
        {
            ShowFormInPanel(new Warehouse.ViewingWarehouse());
        }

        private void btnStaff_Click(object sender, EventArgs e)
        {
            ShowFormInPanel(new Staff.ViewingStaff());
        }

        private void HomeForm_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
