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
        public HomeForm()
        {
            InitializeComponent();
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

        private void button1_Click(object sender, EventArgs e)
        {
            ShowFormInPanel(new Brand.ViewingBrand());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ShowFormInPanel(new Staff.ViewingStaff());
        }

       
    }
}
