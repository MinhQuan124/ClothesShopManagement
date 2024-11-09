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
    public partial class ViewingStaff : Form
    {
        public ViewingStaff()
        {
            InitializeComponent();
        }

        private void ViewingStaff_Load(object sender, EventArgs e)
        {
            StyleSet.DataGridViewStyle(dgv_ViewStaff);
            Load_Staff();
            dgv_ViewStaff.Columns["StaffId"].HeaderText = "Mã nhân viên";
            dgv_ViewStaff.Columns["Name"].HeaderText = " Họ Tên";
            dgv_ViewStaff.Columns["PhoneNumber"].HeaderText = "Số điện thoại";
            dgv_ViewStaff.Columns["Email"].HeaderText = "Email";
            dgv_ViewStaff.Columns["Address"].HeaderText = "Địa chỉ";
            dgv_ViewStaff.Columns["Role"].HeaderText = "Vai trò";
            dgv_ViewStaff.Columns["Username"].HeaderText = "Tên đăng nhập";
            dgv_ViewStaff.Columns["Password"].HeaderText = "Mật khẩu";

        }

        private void Load_Staff()
        {
            string query = "SELECT s.StaffId, s.Name, s.PhoneNumber, s.Email, s.Address, " +
                           "r.Name AS Role, s.Username, s.Password " +
                           "FROM Staff s " +
                           "JOIN Role r ON s.RoleId = r.RoleId";
            dgv_ViewStaff.DataSource = CRUD_Data.GetData(query);
        }
        private void Search_Staff()
        {
            string keyword = txtSearch.Text.Trim();
            string query = $"SELECT s.StaffId, s.Name, s.PhoneNumber, s.Email, s.Address, " +
                   $"r.Name AS Role, s.Username, s.Password " +
                   $"FROM Staff s " +
                   $"JOIN Role r ON s.RoleId = r.RoleId " +
                   $"WHERE s.Name LIKE N'%{keyword}%' " +
                   $"OR s.PhoneNumber LIKE '%{keyword}%' " +
                   $"OR r.Name LIKE N'%{keyword}%'";

            dgv_ViewStaff.DataSource = CRUD_Data.GetData(query);
        }
        private void txtSearch_KeyUp(object sender, KeyEventArgs e)
        {
            Search_Staff();
        }

        private void btn_AddStaff_Click(object sender, EventArgs e)
        {
            AddingStaff addingStaff = new AddingStaff();
            addingStaff.StaffAdded += () =>
            {
                Load_Staff();
            };

            addingStaff.Show();
        }

        private void btn_UpdateStaff_Click(object sender, EventArgs e)
        {
            // Kiểm tra nếu có dòng nào được chọn trong DataGridView
            if (dgv_ViewStaff.SelectedRows.Count > 0)
            {
                // Lấy StaffId của nhân viên từ cột đầu tiên
                int staffId = Convert.ToInt32(dgv_ViewStaff.SelectedRows[0].Cells["StaffId"].Value);

                // Tạo instance của ModifyingStaff và truyền StaffId
                ModifyingStaff modifyForm = new ModifyingStaff();
                modifyForm.LoadStaffData(staffId);

                // Đăng ký sự kiện để cập nhật lại DataGridView sau khi sửa
                modifyForm.StaffUpdated += () =>
                {
                    Load_Staff();
                };

                modifyForm.ShowDialog(); // Hiển thị form sửa nhân viên
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một nhân viên để sửa.");
            }
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            if (dgv_ViewStaff.SelectedRows.Count > 0)
            {
                int staffId = Convert.ToInt32(dgv_ViewStaff.SelectedRows[0].Cells["StaffId"].Value);

                var confirmResult = MessageBox.Show("Bạn có chắc chắn muốn xóa nhân viên này?","Xác nhận xóa",MessageBoxButtons.YesNo,MessageBoxIcon.Question);

                if (confirmResult == DialogResult.Yes)
                {
                    string query = $"DELETE FROM Staff WHERE StaffId = {staffId}";
                    CRUD_Data.CUD(query);
                    Load_Staff();
                    MessageBox.Show("Đã xóa nhân viên thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một nhân viên để xóa.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

    }
}
