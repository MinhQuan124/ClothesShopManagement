using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClothesShopManagement
{
    static class StyleSet
    {
        public static void DataGridViewStyle (DataGridView dataGridView1 ) 
        {

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.Height = 30; 
            }
            
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.BackgroundColor = Color.White;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.Navy;
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            dataGridView1.DefaultCellStyle.Font = new Font("Segoe UI", 11);
            dataGridView1.AllowUserToResizeRows = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ClearSelection();
            dataGridView1.CurrentCell = null;
            dataGridView1.ReadOnly = true;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }


    }
}
