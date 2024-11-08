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

namespace ClothesShopManagement.Customer
{
    public partial class AddCustomer : Form
    {
        public AddCustomer()
        {
            InitializeComponent();
        }

        private void AddCustomer_Load(object sender, EventArgs e)
        {

        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            string sql = "INSERT INTO Customer (Name, PhoneNumber, Email, CreatedDate) VALUES (@Name, @PhoneNumber, @Email, @CreatedDate)";
            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@Name", txtName.Text),
        new SqlParameter("@PhoneNumber", txtPhoneNumber.Text),
        new SqlParameter("@Email", txtEmail.Text),
        new SqlParameter("@CreatedDate", dtp_CreateDate.Value)
            };

            CRUD_Data.ExecuteNonQuery(sql, parameters);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
