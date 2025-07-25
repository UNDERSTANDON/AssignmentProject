namespace ASMDB.Employee
{
    partial class SalesmanFRM
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.DataGridView dataGridProducts;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnHide;
        private System.Windows.Forms.Button btnMarkDown;
        private System.Windows.Forms.PictureBox picPendingOrders;
        private System.Windows.Forms.TextBox txtSearch;

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
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnHide = new System.Windows.Forms.Button();
            this.btnMarkDown = new System.Windows.Forms.Button();
            this.picPendingOrders = new System.Windows.Forms.PictureBox();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.cbSearchType = new System.Windows.Forms.ComboBox();
            this.picEmployeeProfile = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridProducts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPendingOrders)).BeginInit();
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
            // btnUpdate
            // 
            this.btnUpdate.Font = new System.Drawing.Font("Segoe UI", 10.25F);
            this.btnUpdate.Location = new System.Drawing.Point(313, 17);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(120, 32);
            this.btnUpdate.TabIndex = 1;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            // 
            // btnHide
            // 
            this.btnHide.Font = new System.Drawing.Font("Segoe UI", 10.25F);
            this.btnHide.Location = new System.Drawing.Point(439, 16);
            this.btnHide.Name = "btnHide";
            this.btnHide.Size = new System.Drawing.Size(120, 32);
            this.btnHide.TabIndex = 2;
            this.btnHide.Text = "Hide";
            this.btnHide.UseVisualStyleBackColor = true;
            // 
            // btnMarkDown
            // 
            this.btnMarkDown.Font = new System.Drawing.Font("Segoe UI", 10.25F);
            this.btnMarkDown.Location = new System.Drawing.Point(565, 17);
            this.btnMarkDown.Name = "btnMarkDown";
            this.btnMarkDown.Size = new System.Drawing.Size(120, 32);
            this.btnMarkDown.TabIndex = 3;
            this.btnMarkDown.Text = "Mark Down";
            this.btnMarkDown.UseVisualStyleBackColor = true;
            // 
            // picPendingOrders
            // 
            this.picPendingOrders.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picPendingOrders.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picPendingOrders.Image = global::ASMDB.Properties.Resources.PendingOrder;
            this.picPendingOrders.Location = new System.Drawing.Point(691, 9);
            this.picPendingOrders.Name = "picPendingOrders";
            this.picPendingOrders.Size = new System.Drawing.Size(40, 40);
            this.picPendingOrders.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picPendingOrders.TabIndex = 10;
            this.picPendingOrders.TabStop = false;
            // 
            // txtSearch
            // 
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtSearch.Location = new System.Drawing.Point(20, 20);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(200, 27);
            this.txtSearch.TabIndex = 4;
            // 
            // cbSearchType
            // 
            this.cbSearchType.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.cbSearchType.FormattingEnabled = true;
            this.cbSearchType.Items.AddRange(new object[] {
            "ID",
            "Name"});
            this.cbSearchType.Location = new System.Drawing.Point(231, 21);
            this.cbSearchType.Name = "cbSearchType";
            this.cbSearchType.Size = new System.Drawing.Size(76, 25);
            this.cbSearchType.TabIndex = 11;
            // 
            // picEmployeeProfile
            // 
            this.picEmployeeProfile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picEmployeeProfile.Image = global::ASMDB.Properties.Resources.UserProfile;
            this.picEmployeeProfile.Location = new System.Drawing.Point(748, 9);
            this.picEmployeeProfile.Name = "picEmployeeProfile";
            this.picEmployeeProfile.Size = new System.Drawing.Size(40, 40);
            this.picEmployeeProfile.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picEmployeeProfile.TabIndex = 12;
            this.picEmployeeProfile.TabStop = false;
            this.picEmployeeProfile.Click += new System.EventHandler(this.picEmployeeProfile_Click);
            // 
            // SalesmanFRM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.picEmployeeProfile);
            this.Controls.Add(this.cbSearchType);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnHide);
            this.Controls.Add(this.btnMarkDown);
            this.Controls.Add(this.dataGridProducts);
            this.Controls.Add(this.picPendingOrders);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "SalesmanFRM";
            this.Text = "Product Manager";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SalesmanFRM_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridProducts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPendingOrders)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picEmployeeProfile)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbSearchType;
        private System.Windows.Forms.PictureBox picEmployeeProfile;
    }
}