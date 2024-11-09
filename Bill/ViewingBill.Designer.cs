namespace ClothesShopManagement.Bill
{
    partial class ViewingBill
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
            this.label1 = new System.Windows.Forms.Label();
            this.btn_DeleteBill = new System.Windows.Forms.Button();
            this.btn_UpdateBill = new System.Windows.Forms.Button();
            this.btn_AddBill = new System.Windows.Forms.Button();
            this.dgv_Bill = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Bill)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(125)))), ((int)(((byte)(255)))));
            this.label1.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(-7, -2);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(904, 41);
            this.label1.TabIndex = 15;
            this.label1.Text = "Hóa đơn bán";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btn_DeleteBill
            // 
            this.btn_DeleteBill.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(125)))), ((int)(((byte)(255)))));
            this.btn_DeleteBill.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_DeleteBill.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_DeleteBill.ForeColor = System.Drawing.Color.White;
            this.btn_DeleteBill.Location = new System.Drawing.Point(569, 240);
            this.btn_DeleteBill.Margin = new System.Windows.Forms.Padding(2);
            this.btn_DeleteBill.Name = "btn_DeleteBill";
            this.btn_DeleteBill.Size = new System.Drawing.Size(289, 39);
            this.btn_DeleteBill.TabIndex = 14;
            this.btn_DeleteBill.Text = "Xóa Hóa đơn bán";
            this.btn_DeleteBill.UseVisualStyleBackColor = false;
            this.btn_DeleteBill.Click += new System.EventHandler(this.btn_DeleteBill_Click);
            // 
            // btn_UpdateBill
            // 
            this.btn_UpdateBill.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(125)))), ((int)(((byte)(255)))));
            this.btn_UpdateBill.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_UpdateBill.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_UpdateBill.ForeColor = System.Drawing.Color.White;
            this.btn_UpdateBill.Location = new System.Drawing.Point(569, 161);
            this.btn_UpdateBill.Margin = new System.Windows.Forms.Padding(2);
            this.btn_UpdateBill.Name = "btn_UpdateBill";
            this.btn_UpdateBill.Size = new System.Drawing.Size(289, 39);
            this.btn_UpdateBill.TabIndex = 13;
            this.btn_UpdateBill.Text = "Sửa Hóa đơn bán";
            this.btn_UpdateBill.UseVisualStyleBackColor = false;
            // 
            // btn_AddBill
            // 
            this.btn_AddBill.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(125)))), ((int)(((byte)(255)))));
            this.btn_AddBill.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_AddBill.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_AddBill.ForeColor = System.Drawing.Color.White;
            this.btn_AddBill.Location = new System.Drawing.Point(569, 83);
            this.btn_AddBill.Margin = new System.Windows.Forms.Padding(2);
            this.btn_AddBill.Name = "btn_AddBill";
            this.btn_AddBill.Size = new System.Drawing.Size(289, 39);
            this.btn_AddBill.TabIndex = 12;
            this.btn_AddBill.Text = "Thêm Hóa đơn bán";
            this.btn_AddBill.UseVisualStyleBackColor = false;
            // 
            // dgv_Bill
            // 
            this.dgv_Bill.BackgroundColor = System.Drawing.Color.White;
            this.dgv_Bill.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_Bill.Location = new System.Drawing.Point(9, 409);
            this.dgv_Bill.Margin = new System.Windows.Forms.Padding(2);
            this.dgv_Bill.Name = "dgv_Bill";
            this.dgv_Bill.RowHeadersWidth = 74;
            this.dgv_Bill.RowTemplate.Height = 31;
            this.dgv_Bill.Size = new System.Drawing.Size(874, 327);
            this.dgv_Bill.TabIndex = 11;
            // 
            // ViewingBill
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(893, 772);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_DeleteBill);
            this.Controls.Add(this.btn_UpdateBill);
            this.Controls.Add(this.btn_AddBill);
            this.Controls.Add(this.dgv_Bill);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ViewingBill";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.ViewingBill_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Bill)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_DeleteBill;
        private System.Windows.Forms.Button btn_UpdateBill;
        private System.Windows.Forms.Button btn_AddBill;
        private System.Windows.Forms.DataGridView dgv_Bill;
    }
}