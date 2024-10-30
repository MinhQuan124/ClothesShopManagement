using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClothesShopManagement.Staff
{
    public partial class ViewingStaff : Form
    {
        public ViewingStaff()
        {
            InitializeComponent();
        }

        private void ViewingStaff_Load(object sender, EventArgs e)
        {
            StyleSet.DataGridViewStyle(dgv_ViewStaff);


            dgv_ViewStaff.DataSource = CRUD_Data.GetData("SELECT * FROM Staff");
        }
    }
}
