using ClothesShopManagement.Bill;
using ClothesShopManagement.ImportBill;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClothesShopManagement.Statistics
{
    public partial class ViewingStatictis : Form
    {
        private string currentDgv = "";
        public ViewingStatictis()
        {
            InitializeComponent();

            LoadStatisticsType();
            dateTimePicker1.Visible = false;
            btnFilter.Visible = false;
            labelInfo.Visible = false;

            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "MM/yyyy";

            dateTimePicker1.ShowUpDown = true;

        }

        public void LoadStatisticsType()
        {
            comboBox1.Items.Add("Các sản phẩm bán chạy");
            comboBox1.Items.Add("Hóa đơn bán");
            comboBox1.Items.Add("Hóa đơn nhập");
        }

        public void LoadDGV()
        {
            StyleSet.DataGridViewStyle(dataGridView1);
            dataGridView1.Columns["ProductName"].HeaderText = "Tên quần áo";
            dataGridView1.Columns["TotalSold"].HeaderText = "Số lượng đã bán";
        }

        //Hóa đơn bán
        private void Load_Bill()
        {
            string query = @"
        SELECT b.Bill_Id, 
               s.Name AS StaffName, 
               c.Name AS CustomerName, 
               b.SalesPercent, 
               b.CreatedDate, 
               b.Total
        FROM Bill b
        JOIN Staff s ON b.Staff_Id = s.StaffId
        JOIN Customer c ON b.Customer_Id = c.CustomerId";

            dataGridView1.DataSource = CRUD_Data.GetData(query);
        }

        public void DisplayBill()
        {
            StyleSet.DataGridViewStyle(dataGridView1);
            Load_Bill();

            if (dataGridView1.Columns["Bill_Id"] != null) dataGridView1.Columns["Bill_Id"].HeaderText = "Mã hóa đơn";
            if (dataGridView1.Columns["StaffName"] != null) dataGridView1.Columns["StaffName"].HeaderText = "Tên nhân viên";
            if (dataGridView1.Columns["CustomerName"] != null) dataGridView1.Columns["CustomerName"].HeaderText = "Tên khách hàng";
            if (dataGridView1.Columns["SalesPercent"] != null) dataGridView1.Columns["SalesPercent"].HeaderText = "Chiết khấu";
            if (dataGridView1.Columns["CreatedDate"] != null) dataGridView1.Columns["CreatedDate"].HeaderText = "Ngày tạo";
            if (dataGridView1.Columns["Total"] != null) dataGridView1.Columns["Total"].HeaderText = "Tổng hóa đơn";
        }
        //Load label hóa đơn bán
        public void LoadLabelBill()
        {

            string query = "SELECT COUNT(DISTINCT Bill_Id) AS TotalBills, COUNT(DISTINCT Customer_Id) AS TotalCustomers, SUM(Total) AS TotalRevenue From Bill";

            using (SqlConnection connection = CRUD_Data.Connection())
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int totalBills = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
                            int totalCus = reader.IsDBNull(1) ? 0 : reader.GetInt32(1);
                            double totalRevenue = reader.IsDBNull(2) ? 0 : reader.GetDouble(2);

                            labelSection1.Text = totalBills.ToString();
                            labelSection2.Text = totalCus.ToString();
                            labelSection3.Text = totalRevenue.ToString("N0");
                        }
                        else
                        {
                            MessageBox.Show("Không có dữ liệu thống kê.");
                        }
                    }

                }
            }
        }

        //END hóa đơn bán


        //Hóa đơn nhập
        private void Load_ImportBill()
        {
            string query = "select ImportBill.ImportBill_Id, Brand.Name, ImportBill.Total_Payment, ImportBill.ImportDate from ImportBill" +
                " inner join Brand on ImportBill.Brand_Id = Brand.Brand_Id";

            dataGridView1.DataSource = CRUD_Data.GetData(query);
        }

        public void DisplayImportBill()
        {
            StyleSet.DataGridViewStyle(dataGridView1);
            Load_ImportBill();

            dataGridView1.Columns["ImportBill_Id"].HeaderText = "Mã hóa đơn nhập";
            dataGridView1.Columns["Name"].HeaderText = "Nhà cung cấp";
            dataGridView1.Columns["Total_Payment"].HeaderText = "Tổng hóa đơn";
            dataGridView1.Columns["ImportDate"].HeaderText = "Ngày nhập";
        }

        //Load label hóa đơn nhập
        public void LoadLabelImportBill()
        {

            string query = "SELECT COUNT(DISTINCT ImportBill_Id) AS TotalImportBills, SUM(Quantity) AS TotalQuantity, SUM(Total) AS Total_Payment From ImportBillDetail";

            using (SqlConnection connection = CRUD_Data.Connection())
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int totalImportBills = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
                            int totalQuantity = reader.IsDBNull(1) ? 0 : reader.GetInt32(1);
                            double total_Payment = reader.IsDBNull(2) ? 0 : reader.GetDouble(2);

                            labelSection1.Text = totalImportBills.ToString();
                            labelSection2.Text = totalQuantity.ToString();
                            labelSection3.Text = total_Payment.ToString("N0");
                        }
                        else
                        {
                            MessageBox.Show("Không có dữ liệu thống kê.");
                        }
                    }

                }
            }
        }

 
        //END hóa đơn nhập

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
            {
                if (comboBox1.SelectedIndex == 0)
                {
                    labelSection1.Text = "0";
                    labelSection2.Text = "0";
                    labelSection3.Text = "0";

                    labelInfo.Visible = false;
                    dateTimePicker1.Visible = false;
                    btnFilter.Visible = false;

                    using (SqlConnection connection = CRUD_Data.Connection())
                    {
                        connection.Open();

                        string query = "SELECT p.ProductName, SUM(bd.Quantity) AS TotalSold " +
                                       "FROM BillDetail bd " +
                                       "JOIN Product p ON bd.ProductId = p.ProductId " +
                                       "GROUP BY p.ProductName " +
                                       "ORDER BY TotalSold DESC";

                        SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        dataGridView1.DataSource = dataTable;
                    }

                    LoadDGV();
                }
                //Thống kê hóa đơn bán
                else if (comboBox1.SelectedIndex == 1)
                {
                    currentDgv = "bill";

                    //string projectRoot = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\"));
                    //string imagePath = Path.Combine(projectRoot, "images", "icon-user.png");
                    //picB_Section2.Image = Image.FromFile(imagePath);

                    labelInfo.Visible = true;
                    dateTimePicker1.Visible = true;
                    btnFilter.Visible = true;

                    labelWrapper2.Text = "KHÁCH HÀNG";
                    labelWrapper3.Text = "DOANH THU";

                    DisplayBill();
                    LoadLabelBill();
                }
                //Thong ke hoas don nhap
                else if((comboBox1.SelectedIndex == 2)) {

                    currentDgv = "importbill";

                    //string projectRoot = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\"));
                    //string imagePath = Path.Combine(projectRoot, "images", "icon-cart.png");
                    //picB_Section2.Image = Image.FromFile(imagePath);  

                    labelInfo.Visible = true;
                    dateTimePicker1.Visible = true;
                    btnFilter.Visible = true;

                    labelWrapper2.Text = "TỔNG SỐ LƯỢNG HÀNG";
                    labelWrapper3.Text = "TỔNG TIỀN NHẬP";
                
                    DisplayImportBill();
                    LoadLabelImportBill();
                }
            }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            int selectedMonth = dateTimePicker1.Value.Month;
            int selectedYear = dateTimePicker1.Value.Year;

            if (comboBox1.SelectedIndex == 1)
            {
                GetFilteredBillStatistics(selectedMonth, selectedYear);
            }
            else if (comboBox1.SelectedIndex == 2) {
                GetFilteredImportBillStatistics(selectedMonth, selectedYear);
            }
        }

        public void GetFilteredBillStatistics(int month, int year)
        {
            string query = @"
                SELECT 
                    COUNT(DISTINCT Bill_Id) AS TotalBills, 
                    COUNT(DISTINCT Customer_Id) AS TotalCustomers, 
                    SUM(Total) AS TotalRevenue
                FROM Bill
                WHERE MONTH(CreatedDate) = @selectedMonth
                    AND YEAR(CreatedDate) = @selectedYear";

            string filteredQuery = @"
                SELECT b.Bill_Id, 
               s.Name AS StaffName, 
               c.Name AS CustomerName, 
               b.SalesPercent, 
               b.CreatedDate, 
               b.Total
                FROM Bill b
                JOIN Staff s ON b.Staff_Id = s.StaffId
                JOIN Customer c ON b.Customer_Id = c.CustomerId
                WHERE MONTH(b.CreatedDate) = @selectedMonth
                AND YEAR(b.CreatedDate) = @selectedYear";

            using (SqlConnection connection = CRUD_Data.Connection())
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@selectedMonth", month);
                    cmd.Parameters.AddWithValue("@selectedYear", year);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int totalBills = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
                            int totalCus = reader.IsDBNull(1) ? 0 : reader.GetInt32(1);
                            double totalRevenue = reader.IsDBNull(2) ? 0 : reader.GetDouble(2);

                            labelSection1.Text = totalBills.ToString();
                            labelSection2.Text = totalCus.ToString();
                            labelSection3.Text = totalRevenue.ToString("N0");
                        }
                        else
                        {
                            MessageBox.Show("Không có dữ liệu!");
                        }
                    }

                    using (SqlCommand cmdFilter = new SqlCommand(filteredQuery, connection))
                    {
                        cmdFilter.Parameters.AddWithValue("@selectedMonth", month);
                        cmdFilter.Parameters.AddWithValue("@selectedYear", year);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmdFilter))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);

                            dataGridView1.DataSource = dt;

                            StyleSet.DataGridViewStyle(dataGridView1);

                            if (dataGridView1.Columns["Bill_Id"] != null) dataGridView1.Columns["Bill_Id"].HeaderText = "Mã hóa đơn";
                            if (dataGridView1.Columns["StaffName"] != null) dataGridView1.Columns["StaffName"].HeaderText = "Tên nhân viên";
                            if (dataGridView1.Columns["CustomerName"] != null) dataGridView1.Columns["CustomerName"].HeaderText = "Tên khách hàng";
                            if (dataGridView1.Columns["SalesPercent"] != null) dataGridView1.Columns["SalesPercent"].HeaderText = "Chiết khấu";
                            if (dataGridView1.Columns["CreatedDate"] != null) dataGridView1.Columns["CreatedDate"].HeaderText = "Ngày tạo";
                            if (dataGridView1.Columns["Total"] != null) dataGridView1.Columns["Total"].HeaderText = "Tổng hóa đơn";
                        }
                    }
                }
            }
        }

        //Filter import bill
        public void GetFilteredImportBillStatistics(int month, int year)
        {
            string query = @"
                    SELECT 
                        COUNT(DISTINCT ib.ImportBill_Id) AS TotalBills, 
                        SUM(ibd.Quantity) AS TotalQuantity, 
                        SUM(ibd.Total) AS Total_Payment
                    FROM ImportBillDetail ibd
                    INNER JOIN ImportBill ib ON ibd.ImportBill_Id = ib.ImportBill_Id
                    WHERE MONTH(ib.ImportDate) = @selectedMonth
                        AND YEAR(ib.ImportDate) = @selectedYear";

            string filteredQuery = @"
                     select ImportBill.ImportBill_Id, Brand.Name, ImportBill.Total_Payment, ImportBill.ImportDate 
                    from ImportBill
                    inner join Brand on ImportBill.Brand_Id = Brand.Brand_Id
                 WHERE MONTH(ImportDate) = @selectedMonth
                AND YEAR(ImportDate) = @selectedYear";

            using (SqlConnection connection = CRUD_Data.Connection())
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@selectedMonth", month);
                    cmd.Parameters.AddWithValue("@selectedYear", year);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int totalImportBills = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
                            int totalQuantity = reader.IsDBNull(1) ? 0 : reader.GetInt32(1);
                            double total_Payment = reader.IsDBNull(2) ? 0 : reader.GetDouble(2);

                            labelSection1.Text = totalImportBills.ToString();
                            labelSection2.Text = totalQuantity.ToString();
                            labelSection3.Text = total_Payment.ToString("N0");
                        }
                        else
                        {
                            MessageBox.Show("Không có dữ liệu!");
                        }
                    }
                }

                //Lọc
                using (SqlCommand cmdFilter = new SqlCommand(filteredQuery, connection))
                {
                    cmdFilter.Parameters.AddWithValue("@selectedMonth", month);
                    cmdFilter.Parameters.AddWithValue("@selectedYear", year);  

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmdFilter))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt); 

                        dataGridView1.DataSource = dt;
                        StyleSet.DataGridViewStyle(dataGridView1);

                        dataGridView1.Columns["ImportBill_Id"].HeaderText = "Mã hóa đơn nhập";
                        dataGridView1.Columns["Name"].HeaderText = "Nhà cung cấp";
                        dataGridView1.Columns["Total_Payment"].HeaderText = "Tổng hóa đơn";
                        dataGridView1.Columns["ImportDate"].HeaderText = "Ngày nhập";
                    }
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {

                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];
                int id = Convert.ToInt32(selectedRow.Cells[0].Value);
                if(currentDgv == "bill")
                {
                    new ViewingBillDetail(id).ShowDialog();
                } else if(currentDgv == "importbill")
                {
                    new ViewingImportBillDetail(id).ShowDialog();
                }
            }
        }
    }
}
