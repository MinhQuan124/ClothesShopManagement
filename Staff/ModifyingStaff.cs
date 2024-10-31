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
    
    public partial class ModifyingStaff : Form
    {
        public event Action StaffUpdated;
        private int StaffId;
        public ModifyingStaff()
        {
            InitializeComponent();
            LoadRoles();
        }
        private void LoadRoles()
        {
            DataTable rolesTable = CRUD_Data.GetData("SELECT RoleId, Name FROM Role");

            cmbRole.DataSource = rolesTable;
            cmbRole.DisplayMember = "Name";
            cmbRole.ValueMember = "RoleId";
        }
        public void LoadStaffData(int staffId)
        {
            StaffId = staffId; // Lưu StaffId để sử dụng khi cập nhật

            // Truy vấn để lấy thông tin nhân viên dựa trên StaffId
            DataTable staffData = CRUD_Data.GetData($"SELECT * FROM Staff WHERE StaffId = {StaffId}");

            if (staffData.Rows.Count > 0)
            {
                DataRow row = staffData.Rows[0];

                // Điền dữ liệu vào các TextBox
                txtName.Text = row["Name"].ToString();
                txtPhoneNumber.Text = row["PhoneNumber"].ToString();
                txtEmail.Text = row["Email"].ToString();
                txtAddress.Text = row["Address"].ToString();
                cmbRole.SelectedValue = row["RoleId"];
                txtUsername.Text = row["Username"].ToString();
                txtPassword.Text = row["Password"].ToString();
            }
        }
        private void btn_Save_Click(object sender, EventArgs e)
        {
            string name = txtName.Text.Trim();
            string phoneNumber = txtPhoneNumber.Text.Trim();
            string email = txtEmail.Text.Trim();
            string address = txtAddress.Text.Trim();
            int roleId = (int)cmbRole.SelectedValue;
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (StaffId > 0)
            {
                // Truy vấn cập nhật nhân viên
                string query = $"UPDATE Staff SET Name = N'{name}', PhoneNumber = '{phoneNumber}', " +
                               $"Email = '{email}', Address = N'{address}', RoleId = {roleId}, " +
                               $"Username = '{username}', Password = '{password}' " +
                               $"WHERE StaffId = {StaffId}";

                CRUD_Data.GetData(query);

                // Kích hoạt sự kiện StaffUpdated để cập nhật DataGridView trên form chính
                StaffUpdated?.Invoke();
                MessageBox.Show("Đã cập nhật nhân viên thành công.");

                // Đóng form nếu sửa thành công
                this.Close();
            }
        }

        private void ModifyingStaff_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
