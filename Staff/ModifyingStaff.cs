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
            // Kiểm tra dữ liệu hợp lệ
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(phoneNumber) || string.IsNullOrEmpty(email) ||
                string.IsNullOrEmpty(address) || string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin.");
                return;
            }

            if (StaffId > 0)
            {
                // Truy vấn cập nhật nhân viên
                string query = "UPDATE Staff SET Name = @name, PhoneNumber = @phoneNumber, " +
                               "Email = @email, Address = @address, RoleId = @roleId, " +
                               "Username = @username, Password = @password " +
                               "WHERE StaffId = @staffId";

                // Tạo mảng tham số
                SqlParameter[] parameters = new SqlParameter[]
                {
                  new SqlParameter("@name", name),
                  new SqlParameter("@phoneNumber", phoneNumber),
                  new SqlParameter("@email", email),
                  new SqlParameter("@address", address),
                  new SqlParameter("@roleId", roleId),
                  new SqlParameter("@username", username),
                  new SqlParameter("@password", password),
                  new SqlParameter("@staffId", StaffId)
                };

                // Gọi hàm ExecuteNonQuery với câu truy vấn và mảng tham số
                int rowsAffected = CRUD_Data.ExecuteNonQuery(query, parameters);

                // Kiểm tra kết quả
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Đã cập nhật nhân viên thành công.");
                    // Kích hoạt sự kiện StaffUpdated để cập nhật DataGridView trên form chính
                    StaffUpdated?.Invoke();
                }
                else
                {
                    MessageBox.Show("Không thể cập nhật nhân viên.");
                }
                // Đóng form nếu sửa thành công
                this.Close();
            }
        }

        private void ModifyingStaff_Load(object sender, EventArgs e)
        {
            LoadStaffData(StaffId);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
