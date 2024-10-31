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
    public partial class AddingStaff : Form
    {
        public event Action StaffAdded;
        public AddingStaff()
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
        private void AddingStaff_Load(object sender, EventArgs e)
        {

        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            // Lấy dữ liệu từ các trường nhập liệu
            string name = txtName.Text.Trim();
            string phoneNumber = txtPhoneNumber.Text.Trim();
            string email = txtEmail.Text.Trim();
            string address = txtAddress.Text.Trim();
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();
            int roleId = (int)cmbRole.SelectedValue;

            // Kiểm tra dữ liệu hợp lệ
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(phoneNumber) || string.IsNullOrEmpty(email) ||
                string.IsNullOrEmpty(address) || string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin.");
                return;
            }

            // Thực hiện lệnh SQL để thêm dữ liệu vào bảng Staff
            string query = $"INSERT INTO Staff (Name, PhoneNumber, Email, Address, RoleId, Username, Password) " +
                           $"VALUES (N'{name}', '{phoneNumber}', '{email}', N'{address}', {roleId}, '{username}', '{password}')";
            CRUD_Data.GetData(query);

            // Kích hoạt sự kiện StaffAdded để cập nhật DataGridView trên form chính
            StaffAdded?.Invoke();

            // Xóa các trường nhập liệu để chuẩn bị cho lần nhập tiếp theo
            txtName.Clear();
            txtPhoneNumber.Clear();
            txtEmail.Clear();
            txtAddress.Clear();
            txtUsername.Clear();
            txtPassword.Clear();
            cmbRole.SelectedIndex = -1;
            txtName.Focus();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
