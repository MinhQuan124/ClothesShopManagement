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
            string query = "SELECT * FROM Staff";
            dgv_ViewStaff.DataSource = CRUD_Data.GetData(query);
        }

        private void txtSearch_KeyUp(object sender, KeyEventArgs e)
        {
            string keyword = txtSearch.Text.Trim();

            if (string.IsNullOrEmpty(keyword))
            {
                dgv_ViewStaff.DataSource = CRUD_Data.GetData("SELECT * FROM Staff");
            }
            else
            {
                string query = $"SELECT * FROM Staff WHERE Name LIKE '%{keyword}%'";
                dgv_ViewStaff.DataSource = CRUD_Data.GetData(query);
            }
        }

        private void btn_AddStaff_Click(object sender, EventArgs e)
        {
            AddingStaff addingStaff = new AddingStaff();
            addingStaff.StaffAdded += () =>
            {
                dgv_ViewStaff.DataSource = CRUD_Data.GetData("SELECT * FROM Staff");
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
                    dgv_ViewStaff.DataSource = CRUD_Data.GetData("SELECT * FROM Staff");
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

                var confirmResult = MessageBox.Show("Bạn có chắc chắn muốn xóa nhân viên này?", "Xác nhận xóa", MessageBoxButtons.YesNo);

                if (confirmResult == DialogResult.Yes)
                {
                    string query = $"DELETE FROM Staff WHERE StaffId = {staffId}";
                    CRUD_Data.CUD(query);
                    dgv_ViewStaff.DataSource = CRUD_Data.GetData("SELECT * FROM Staff");
                    MessageBox.Show("Đã xóa nhân viên thành công.");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một nhân viên để xóa.");
            }
        }
    }
}
