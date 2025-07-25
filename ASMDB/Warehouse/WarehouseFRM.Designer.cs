namespace ASMDB.Warehouse
{
    partial class WarehouseFRM
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.DataGridView dataGridProducts;

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
            this.dataGridProducts = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.cbSearchType = new System.Windows.Forms.ComboBox();
            this.picEmployeeProfile = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridProducts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picEmployeeProfile)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridProducts
            // 
            this.dataGridProducts.AllowUserToAddRows = false;
            this.dataGridProducts.AllowUserToDeleteRows = false;
            this.dataGridProducts.AllowUserToResizeRows = false;
            this.dataGridProducts.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridProducts.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dataGridProducts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridProducts.Location = new System.Drawing.Point(0, 60);
            this.dataGridProducts.Name = "dataGridProducts";
            this.dataGridProducts.ReadOnly = true;
            this.dataGridProducts.RowHeadersVisible = false;
            this.dataGridProducts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridProducts.Size = new System.Drawing.Size(800, 390);
            this.dataGridProducts.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Segoe UI", 10.25F);
            this.button1.Location = new System.Drawing.Point(314, 16);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(120, 32);
            this.button1.TabIndex = 4;
            this.button1.Text = "Add New Item";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Segoe UI", 10.25F);
            this.button2.Location = new System.Drawing.Point(440, 16);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(120, 32);
            this.button2.TabIndex = 5;
            this.button2.Text = "Remove Item";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Segoe UI", 10.25F);
            this.button3.Location = new System.Drawing.Point(566, 16);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(120, 32);
            this.button3.TabIndex = 6;
            this.button3.Text = "Update Item";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.textBox1.Location = new System.Drawing.Point(12, 21);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(193, 23);
            this.textBox1.TabIndex = 7;
            // 
            // cbSearchType
            // 
            this.cbSearchType.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.cbSearchType.FormattingEnabled = true;
            this.cbSearchType.Items.AddRange(new object[] {
            "ID",
            "Name"});
            this.cbSearchType.Location = new System.Drawing.Point(216, 21);
            this.cbSearchType.Name = "cbSearchType";
            this.cbSearchType.Size = new System.Drawing.Size(76, 25);
            this.cbSearchType.TabIndex = 8;
            // 
            // picEmployeeProfile
            // 
            this.picEmployeeProfile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picEmployeeProfile.Image = global::ASMDB.Properties.Resources.UserProfile;
            this.picEmployeeProfile.Location = new System.Drawing.Point(748, 12);
            this.picEmployeeProfile.Name = "picEmployeeProfile";
            this.picEmployeeProfile.Size = new System.Drawing.Size(40, 40);
            this.picEmployeeProfile.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picEmployeeProfile.TabIndex = 13;
            this.picEmployeeProfile.TabStop = false;
            this.picEmployeeProfile.Click += new System.EventHandler(this.picEmployeeProfile_Click);
            // 
            // WarehouseFRM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.picEmployeeProfile);
            this.Controls.Add(this.cbSearchType);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.dataGridProducts);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "WarehouseFRM";
            this.Text = "Warehouse";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.WarehouseFRM_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridProducts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picEmployeeProfile)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ComboBox cbSearchType;
        private System.Windows.Forms.PictureBox picEmployeeProfile;
    }
}