namespace ClothesShopManagement.Bill
{
    partial class ViewingBillDetail
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblStaffname = new System.Windows.Forms.Label();
            this.lblCustomerName = new System.Windows.Forms.Label();
            this.lblSalesPercent = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.lblCreatedDate = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgvBillDetail = new System.Windows.Forms.DataGridView();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBillDetail)).BeginInit();
            this.SuspendLayout();
            // 
            // lblStaffname
            // 
            this.lblStaffname.AutoSize = true;
            this.lblStaffname.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStaffname.Location = new System.Drawing.Point(53, 58);
            this.lblStaffname.Name = "lblStaffname";
            this.lblStaffname.Size = new System.Drawing.Size(112, 25);
            this.lblStaffname.TabIndex = 0;
            this.lblStaffname.Text = "Nhân viên : ";
            // 
            // lblCustomerName
            // 
            this.lblCustomerName.AutoSize = true;
            this.lblCustomerName.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCustomerName.Location = new System.Drawing.Point(53, 102);
            this.lblCustomerName.Name = "lblCustomerName";
            this.lblCustomerName.Size = new System.Drawing.Size(126, 25);
            this.lblCustomerName.TabIndex = 1;
            this.lblCustomerName.Text = "Khách hàng : ";
            // 
            // lblSalesPercent
            // 
            this.lblSalesPercent.AutoSize = true;
            this.lblSalesPercent.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSalesPercent.Location = new System.Drawing.Point(612, 58);
            this.lblSalesPercent.Name = "lblSalesPercent";
            this.lblSalesPercent.Size = new System.Drawing.Size(116, 25);
            this.lblSalesPercent.TabIndex = 2;
            this.lblSalesPercent.Text = "Chiết khấu : ";
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.Location = new System.Drawing.Point(899, 58);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(100, 25);
            this.lblTotal.TabIndex = 3;
            this.lblTotal.Text = "Tổng giá : ";
            // 
            // lblCreatedDate
            // 
            this.lblCreatedDate.AutoSize = true;
            this.lblCreatedDate.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCreatedDate.Location = new System.Drawing.Point(351, 58);
            this.lblCreatedDate.Name = "lblCreatedDate";
            this.lblCreatedDate.Size = new System.Drawing.Size(102, 25);
            this.lblCreatedDate.TabIndex = 4;
            this.lblCreatedDate.Text = "Ngày tạo : ";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgvBillDetail);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 266);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(1160, 354);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Danh sách các sản phẩm đã mua";
            // 
            // dgvBillDetail
            // 
            this.dgvBillDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBillDetail.Location = new System.Drawing.Point(6, 29);
            this.dgvBillDetail.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvBillDetail.Name = "dgvBillDetail";
            this.dgvBillDetail.RowHeadersWidth = 62;
            this.dgvBillDetail.RowTemplate.Height = 28;
            this.dgvBillDetail.Size = new System.Drawing.Size(1148, 325);
            this.dgvBillDetail.TabIndex = 0;
            // 
            // ViewingBillDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1182, 646);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblCreatedDate);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.lblSalesPercent);
            this.Controls.Add(this.lblCustomerName);
            this.Controls.Add(this.lblStaffname);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "ViewingBillDetail";
            this.Text = "Xem chi tiết hóa đơn";
            this.Load += new System.EventHandler(this.ViewingBillDetail_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBillDetail)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblStaffname;
        private System.Windows.Forms.Label lblCustomerName;
        private System.Windows.Forms.Label lblSalesPercent;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label lblCreatedDate;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgvBillDetail;
    }
}