namespace ClothesShopManagement.Staff
{
    partial class ViewingStaff
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
            this.dgv_ViewStaff = new System.Windows.Forms.DataGridView();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btn_AddStaff = new System.Windows.Forms.Button();
            this.btn_UpdateStaff = new System.Windows.Forms.Button();
            this.btn_Delete = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_ViewStaff)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv_ViewStaff
            // 
            this.dgv_ViewStaff.BackgroundColor = System.Drawing.Color.White;
            this.dgv_ViewStaff.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_ViewStaff.Location = new System.Drawing.Point(0, 378);
            this.dgv_ViewStaff.Name = "dgv_ViewStaff";
            this.dgv_ViewStaff.RowHeadersWidth = 74;
            this.dgv_ViewStaff.RowTemplate.Height = 31;
            this.dgv_ViewStaff.Size = new System.Drawing.Size(1147, 454);
            this.dgv_ViewStaff.TabIndex = 0;
            // 
            // txtSearch
            // 
            this.txtSearch.BackColor = System.Drawing.Color.White;
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearch.Font = new System.Drawing.Font("Microsoft YaHei UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.ForeColor = System.Drawing.Color.Black;
            this.txtSearch.HideSelection = false;
            this.txtSearch.Location = new System.Drawing.Point(87, 80);
            this.txtSearch.Multiline = true;
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(280, 55);
            this.txtSearch.TabIndex = 1;
            this.txtSearch.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyUp);
            // 
            // btn_AddStaff
            // 
            this.btn_AddStaff.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(125)))), ((int)(((byte)(255)))));
            this.btn_AddStaff.Font = new System.Drawing.Font("Microsoft YaHei UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_AddStaff.ForeColor = System.Drawing.Color.White;
            this.btn_AddStaff.Location = new System.Drawing.Point(927, 80);
            this.btn_AddStaff.Name = "btn_AddStaff";
            this.btn_AddStaff.Size = new System.Drawing.Size(208, 60);
            this.btn_AddStaff.TabIndex = 2;
            this.btn_AddStaff.Text = "Thêm";
            this.btn_AddStaff.UseVisualStyleBackColor = false;
            this.btn_AddStaff.Click += new System.EventHandler(this.btn_AddStaff_Click);
            // 
            // btn_UpdateStaff
            // 
            this.btn_UpdateStaff.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(125)))), ((int)(((byte)(255)))));
            this.btn_UpdateStaff.Font = new System.Drawing.Font("Microsoft YaHei UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_UpdateStaff.ForeColor = System.Drawing.Color.White;
            this.btn_UpdateStaff.Location = new System.Drawing.Point(927, 146);
            this.btn_UpdateStaff.Name = "btn_UpdateStaff";
            this.btn_UpdateStaff.Size = new System.Drawing.Size(208, 60);
            this.btn_UpdateStaff.TabIndex = 3;
            this.btn_UpdateStaff.Text = "Sửa";
            this.btn_UpdateStaff.UseVisualStyleBackColor = false;
            this.btn_UpdateStaff.Click += new System.EventHandler(this.btn_UpdateStaff_Click);
            // 
            // btn_Delete
            // 
            this.btn_Delete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(125)))), ((int)(((byte)(255)))));
            this.btn_Delete.Font = new System.Drawing.Font("Microsoft YaHei UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Delete.ForeColor = System.Drawing.Color.White;
            this.btn_Delete.Location = new System.Drawing.Point(927, 212);
            this.btn_Delete.Name = "btn_Delete";
            this.btn_Delete.Size = new System.Drawing.Size(208, 60);
            this.btn_Delete.TabIndex = 4;
            this.btn_Delete.Text = "Xóa";
            this.btn_Delete.UseVisualStyleBackColor = false;
            this.btn_Delete.Click += new System.EventHandler(this.btn_Delete_Click);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(125)))), ((int)(((byte)(255)))));
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, -2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1148, 63);
            this.label1.TabIndex = 7;
            this.label1.Text = "Danh sách nhân viên";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Image = global::ClothesShopManagement.Properties.Resources.IconSearch;
            this.pictureBox1.Location = new System.Drawing.Point(26, 80);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(64, 55);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            // 
            // ViewingStaff
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1147, 824);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_Delete);
            this.Controls.Add(this.btn_UpdateStaff);
            this.Controls.Add(this.btn_AddStaff);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.dgv_ViewStaff);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ViewingStaff";
            this.Text = "ViewingStaff";
            this.Load += new System.EventHandler(this.ViewingStaff_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_ViewStaff)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv_ViewStaff;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btn_AddStaff;
        private System.Windows.Forms.Button btn_UpdateStaff;
        private System.Windows.Forms.Button btn_Delete;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}