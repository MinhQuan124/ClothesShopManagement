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
            string query = "INSERT INTO Staff (Name, PhoneNumber, Email, Address, RoleId, Username, Password) " +
               "VALUES (@name, @phoneNumber, @email, @address, @roleId, @username, @password)";

            // Tạo mảng tham số
            SqlParameter[] parameters = new SqlParameter[]
            {
              new SqlParameter("@name", name),
              new SqlParameter("@phoneNumber", phoneNumber),
              new SqlParameter("@email", email),
              new SqlParameter("@address", address),
              new SqlParameter("@roleId", roleId),
              new SqlParameter("@username", username),
              new SqlParameter("@password", password)
            };

            // Gọi hàm ExecuteNonQuery với câu truy vấn và mảng tham số
            int rowsAffected = CRUD_Data.ExecuteNonQuery(query, parameters);

            // Kiểm tra kết quả (có thể hiển thị thông báo nếu cần)
            if (rowsAffected > 0)
            {
                MessageBox.Show("Đã thêm nhân viên thành công.");
                // Kích hoạt sự kiện StaffAdded để cập nhật DataGridView trên form chính
                StaffAdded?.Invoke();

            }
            else
            {
                MessageBox.Show("Không thể thêm nhân viên.");
            };

            

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
