namespace ClothesShopManagement.Customer
{
    partial class ViewCustomer
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
            this.btn_DeleteCus = new System.Windows.Forms.Button();
            this.btn_UpdateCus = new System.Windows.Forms.Button();
            this.btn_AddCus = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.dgv_ViewCus = new System.Windows.Forms.DataGridView();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_Search = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_ViewCus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_DeleteCus
            // 
            this.btn_DeleteCus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(125)))), ((int)(((byte)(255)))));
            this.btn_DeleteCus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_DeleteCus.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_DeleteCus.ForeColor = System.Drawing.Color.White;
            this.btn_DeleteCus.Location = new System.Drawing.Point(648, 201);
            this.btn_DeleteCus.Margin = new System.Windows.Forms.Padding(2);
            this.btn_DeleteCus.Name = "btn_DeleteCus";
            this.btn_DeleteCus.Size = new System.Drawing.Size(210, 39);
            this.btn_DeleteCus.TabIndex = 9;
            this.btn_DeleteCus.Text = "Xóa";
            this.btn_DeleteCus.UseVisualStyleBackColor = false;
            this.btn_DeleteCus.Click += new System.EventHandler(this.btn_DeleteCus_Click);
            // 
            // btn_UpdateCus
            // 
            this.btn_UpdateCus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(125)))), ((int)(((byte)(255)))));
            this.btn_UpdateCus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_UpdateCus.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_UpdateCus.ForeColor = System.Drawing.Color.White;
            this.btn_UpdateCus.Location = new System.Drawing.Point(648, 137);
            this.btn_UpdateCus.Margin = new System.Windows.Forms.Padding(2);
            this.btn_UpdateCus.Name = "btn_UpdateCus";
            this.btn_UpdateCus.Size = new System.Drawing.Size(210, 39);
            this.btn_UpdateCus.TabIndex = 8;
            this.btn_UpdateCus.Text = "Sửa";
            this.btn_UpdateCus.UseVisualStyleBackColor = false;
            this.btn_UpdateCus.Click += new System.EventHandler(this.btn_UpdateCus_Click);
            // 
            // btn_AddCus
            // 
            this.btn_AddCus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(125)))), ((int)(((byte)(255)))));
            this.btn_AddCus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_AddCus.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_AddCus.ForeColor = System.Drawing.Color.White;
            this.btn_AddCus.Location = new System.Drawing.Point(648, 75);
            this.btn_AddCus.Margin = new System.Windows.Forms.Padding(2);
            this.btn_AddCus.Name = "btn_AddCus";
            this.btn_AddCus.Size = new System.Drawing.Size(210, 39);
            this.btn_AddCus.TabIndex = 7;
            this.btn_AddCus.Text = "Thêm";
            this.btn_AddCus.UseVisualStyleBackColor = false;
            this.btn_AddCus.Click += new System.EventHandler(this.btn_AddCus_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.ForeColor = System.Drawing.Color.Black;
            this.txtSearch.HideSelection = false;
            this.txtSearch.Location = new System.Drawing.Point(78, 78);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(2);
            this.txtSearch.Multiline = true;
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(265, 46);
            this.txtSearch.TabIndex = 6;
            // 
            // dgv_ViewCus
            // 
            this.dgv_ViewCus.BackgroundColor = System.Drawing.Color.White;
            this.dgv_ViewCus.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_ViewCus.Location = new System.Drawing.Point(9, 370);
            this.dgv_ViewCus.Margin = new System.Windows.Forms.Padding(2);
            this.dgv_ViewCus.Name = "dgv_ViewCus";
            this.dgv_ViewCus.RowHeadersWidth = 74;
            this.dgv_ViewCus.RowTemplate.Height = 31;
            this.dgv_ViewCus.Size = new System.Drawing.Size(874, 327);
            this.dgv_ViewCus.TabIndex = 5;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Image = global::ClothesShopManagement.Properties.Resources.IconSearch;
            this.pictureBox1.Location = new System.Drawing.Point(11, 77);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(59, 47);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 11;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(125)))), ((int)(((byte)(255)))));
            this.label1.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(-6, -2);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(904, 41);
            this.label1.TabIndex = 10;
            this.label1.Text = "Danh sách nhân viên";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btn_Search
            // 
            this.btn_Search.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(125)))), ((int)(((byte)(255)))));
            this.btn_Search.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Search.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Search.ForeColor = System.Drawing.Color.White;
            this.btn_Search.Location = new System.Drawing.Point(347, 78);
            this.btn_Search.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Search.Name = "btn_Search";
            this.btn_Search.Size = new System.Drawing.Size(147, 46);
            this.btn_Search.TabIndex = 12;
            this.btn_Search.Text = "Tìm kiếm";
            this.btn_Search.UseVisualStyleBackColor = false;
            this.btn_Search.Click += new System.EventHandler(this.btn_Search_Click);
            // 
            // ViewCustomer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(893, 772);
            this.Controls.Add(this.btn_Search);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_DeleteCus);
            this.Controls.Add(this.btn_UpdateCus);
            this.Controls.Add(this.btn_AddCus);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.dgv_ViewCus);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ViewCustomer";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.ViewCustomer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_ViewCus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_DeleteCus;
        private System.Windows.Forms.Button btn_UpdateCus;
        private System.Windows.Forms.Button btn_AddCus;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.DataGridView dgv_ViewCus;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_Search;
    }
}