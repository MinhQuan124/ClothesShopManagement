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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btn_AddStaff = new System.Windows.Forms.Button();
            this.btn_UpdateStaff = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.btn_SearchStaff = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_ViewStaff)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv_ViewStaff
            // 
            this.dgv_ViewStaff.BackgroundColor = System.Drawing.Color.Gainsboro;
            this.dgv_ViewStaff.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_ViewStaff.Location = new System.Drawing.Point(0, 239);
            this.dgv_ViewStaff.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dgv_ViewStaff.Name = "dgv_ViewStaff";
            this.dgv_ViewStaff.RowHeadersWidth = 74;
            this.dgv_ViewStaff.RowTemplate.Height = 31;
            this.dgv_ViewStaff.Size = new System.Drawing.Size(765, 295);
            this.dgv_ViewStaff.TabIndex = 0;
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Microsoft YaHei UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(11, 6);
            this.textBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(188, 34);
            this.textBox1.TabIndex = 1;
            // 
            // btn_AddStaff
            // 
            this.btn_AddStaff.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(102)))), ((int)(((byte)(255)))));
            this.btn_AddStaff.Font = new System.Drawing.Font("Microsoft YaHei UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_AddStaff.ForeColor = System.Drawing.Color.White;
            this.btn_AddStaff.Location = new System.Drawing.Point(578, 11);
            this.btn_AddStaff.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btn_AddStaff.Name = "btn_AddStaff";
            this.btn_AddStaff.Size = new System.Drawing.Size(139, 39);
            this.btn_AddStaff.TabIndex = 2;
            this.btn_AddStaff.Text = "Thêm";
            this.btn_AddStaff.UseVisualStyleBackColor = false;
            // 
            // btn_UpdateStaff
            // 
            this.btn_UpdateStaff.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(102)))), ((int)(((byte)(255)))));
            this.btn_UpdateStaff.Font = new System.Drawing.Font("Microsoft YaHei UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_UpdateStaff.ForeColor = System.Drawing.Color.White;
            this.btn_UpdateStaff.Location = new System.Drawing.Point(578, 54);
            this.btn_UpdateStaff.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btn_UpdateStaff.Name = "btn_UpdateStaff";
            this.btn_UpdateStaff.Size = new System.Drawing.Size(139, 39);
            this.btn_UpdateStaff.TabIndex = 3;
            this.btn_UpdateStaff.Text = "Sửa";
            this.btn_UpdateStaff.UseVisualStyleBackColor = false;
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Microsoft YaHei UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.Location = new System.Drawing.Point(578, 97);
            this.button3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(139, 39);
            this.button3.TabIndex = 4;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // btn_SearchStaff
            // 
            this.btn_SearchStaff.BackgroundImage = global::ClothesShopManagement.Properties.Resources.IconSearch;
            this.btn_SearchStaff.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_SearchStaff.Location = new System.Drawing.Point(203, 6);
            this.btn_SearchStaff.Margin = new System.Windows.Forms.Padding(2);
            this.btn_SearchStaff.Name = "btn_SearchStaff";
            this.btn_SearchStaff.Size = new System.Drawing.Size(40, 36);
            this.btn_SearchStaff.TabIndex = 6;
            this.btn_SearchStaff.UseVisualStyleBackColor = true;
            // 
            // ViewingStaff
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(1147, 799);
            this.Controls.Add(this.btn_SearchStaff);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.btn_UpdateStaff);
            this.Controls.Add(this.btn_AddStaff);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.dgv_ViewStaff);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "ViewingStaff";
            this.Text = "ViewingStaff";
            this.Load += new System.EventHandler(this.ViewingStaff_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_ViewStaff)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv_ViewStaff;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btn_AddStaff;
        private System.Windows.Forms.Button btn_UpdateStaff;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button btn_SearchStaff;
    }
}