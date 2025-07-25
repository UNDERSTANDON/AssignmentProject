namespace ASMDB.Order
{
    partial class ShippingFRM
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.DataGridView dataGridOrders;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProductId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProductName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn colShipping;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStatus;

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
            this.dataGridOrders = new System.Windows.Forms.DataGridView();
            this.colProductId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colProductName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colShipping = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BtnShippingInfo = new System.Windows.Forms.DataGridViewButtonColumn();
            this.btnBackToShopping = new System.Windows.Forms.Button();
            this.BtShipped = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridOrders)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridOrders
            // 
            this.dataGridOrders.AllowUserToAddRows = false;
            this.dataGridOrders.AllowUserToDeleteRows = false;
            this.dataGridOrders.AllowUserToResizeRows = false;
            this.dataGridOrders.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridOrders.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dataGridOrders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridOrders.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colProductId,
            this.colProductName,
            this.colPrice,
            this.colShipping,
            this.colStatus,
            this.BtnShippingInfo});
            this.dataGridOrders.Dock = System.Windows.Forms.DockStyle.Top;
            this.dataGridOrders.Location = new System.Drawing.Point(0, 0);
            this.dataGridOrders.Name = "dataGridOrders";
            this.dataGridOrders.ReadOnly = true;
            this.dataGridOrders.RowHeadersVisible = false;
            this.dataGridOrders.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridOrders.Size = new System.Drawing.Size(800, 350);
            this.dataGridOrders.TabIndex = 0;
            // 
            // colProductId
            // 
            this.colProductId.HeaderText = "Product ID";
            this.colProductId.Name = "colProductId";
            this.colProductId.ReadOnly = true;
            // 
            // colProductName
            // 
            this.colProductName.HeaderText = "Product Name";
            this.colProductName.Name = "colProductName";
            this.colProductName.ReadOnly = true;
            // 
            // colPrice
            // 
            this.colPrice.HeaderText = "Price";
            this.colPrice.Name = "colPrice";
            this.colPrice.ReadOnly = true;
            // 
            // colShipping
            // 
            this.colShipping.HeaderText = "Shipping";
            this.colShipping.Name = "colShipping";
            this.colShipping.ReadOnly = true;
            // 
            // colStatus
            // 
            this.colStatus.HeaderText = "Status";
            this.colStatus.Name = "colStatus";
            this.colStatus.ReadOnly = true;
            // 
            // BtnShippingInfo
            // 
            this.BtnShippingInfo.HeaderText = "Detail Shipping";
            this.BtnShippingInfo.Name = "BtnShippingInfo";
            this.BtnShippingInfo.ReadOnly = true;
            // 
            // btnBackToShopping
            // 
            this.btnBackToShopping.BackColor = System.Drawing.Color.LightGray;
            this.btnBackToShopping.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnBackToShopping.ForeColor = System.Drawing.Color.Black;
            this.btnBackToShopping.Location = new System.Drawing.Point(174, 372);
            this.btnBackToShopping.Name = "btnBackToShopping";
            this.btnBackToShopping.Size = new System.Drawing.Size(180, 40);
            this.btnBackToShopping.TabIndex = 1;
            this.btnBackToShopping.Text = "Back to shopping";
            this.btnBackToShopping.UseVisualStyleBackColor = false;
            // 
            // BtShipped
            // 
            this.BtShipped.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.BtShipped.Location = new System.Drawing.Point(494, 372);
            this.BtShipped.Name = "BtShipped";
            this.BtShipped.Size = new System.Drawing.Size(171, 40);
            this.BtShipped.TabIndex = 2;
            this.BtShipped.Text = "Shipping complete";
            this.BtShipped.UseVisualStyleBackColor = true;
            this.BtShipped.Click += new System.EventHandler(this.BtShipped_Click);
            // 
            // ShippingFRM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.BtShipped);
            this.Controls.Add(this.btnBackToShopping);
            this.Controls.Add(this.dataGridOrders);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "ShippingFRM";
            this.Text = "Shipping";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridOrders)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridViewButtonColumn BtnShippingInfo;
        private System.Windows.Forms.Button BtShipped;
    }
}