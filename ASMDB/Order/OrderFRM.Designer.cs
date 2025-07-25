namespace ASMDB.Order
{
    partial class OrderFRM
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.DataGridView dataGridOrderProducts;
        private System.Windows.Forms.Button btnPurchase;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colSelect;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProductId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProductName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProductPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn colQuantity;
        private System.Windows.Forms.DataGridViewComboBoxColumn colShippingType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colShippingFee;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblTotalPrices;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblVoucher;

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
            this.dataGridOrderProducts = new System.Windows.Forms.DataGridView();
            this.colSelect = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colProductId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colProductName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colProductPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colQuantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colShippingType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.colShippingFee = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnPurchase = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lblTotalPrices = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblVoucher = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblSeclectAll = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridOrderProducts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridOrderProducts
            // 
            this.dataGridOrderProducts.AllowUserToAddRows = false;
            this.dataGridOrderProducts.AllowUserToDeleteRows = false;
            this.dataGridOrderProducts.AllowUserToResizeRows = false;
            this.dataGridOrderProducts.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridOrderProducts.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dataGridOrderProducts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridOrderProducts.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colSelect,
            this.colProductId,
            this.colProductName,
            this.colProductPrice,
            this.colQuantity,
            this.colShippingType,
            this.colShippingFee});
            this.dataGridOrderProducts.Dock = System.Windows.Forms.DockStyle.Top;
            this.dataGridOrderProducts.Location = new System.Drawing.Point(0, 0);
            this.dataGridOrderProducts.Name = "dataGridOrderProducts";
            this.dataGridOrderProducts.RowHeadersVisible = false;
            this.dataGridOrderProducts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridOrderProducts.Size = new System.Drawing.Size(900, 350);
            this.dataGridOrderProducts.TabIndex = 0;
            // 
            // colSelect
            // 
            this.colSelect.HeaderText = "Select";
            this.colSelect.Name = "colSelect";
            // 
            // colProductId
            // 
            this.colProductId.HeaderText = "Product ID";
            this.colProductId.Name = "colProductId";
            // 
            // colProductName
            // 
            this.colProductName.HeaderText = "Product Name";
            this.colProductName.Name = "colProductName";
            // 
            // colProductPrice
            // 
            this.colProductPrice.HeaderText = "Product Price";
            this.colProductPrice.Name = "colProductPrice";
            // 
            // colQuantity
            // 
            this.colQuantity.HeaderText = "Quantity";
            this.colQuantity.Name = "colQuantity";
            // 
            // colShippingType
            // 
            this.colShippingType.HeaderText = "Shipping Type";
            this.colShippingType.Items.AddRange(new object[] {
            "Standard",
            "Express"});
            this.colShippingType.Name = "colShippingType";
            // 
            // colShippingFee
            // 
            this.colShippingFee.HeaderText = "Shipping Fee";
            this.colShippingFee.Name = "colShippingFee";
            this.colShippingFee.ReadOnly = true;
            // 
            // btnPurchase
            // 
            this.btnPurchase.BackColor = System.Drawing.Color.Red;
            this.btnPurchase.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnPurchase.ForeColor = System.Drawing.Color.White;
            this.btnPurchase.Location = new System.Drawing.Point(220, 410);
            this.btnPurchase.Name = "btnPurchase";
            this.btnPurchase.Size = new System.Drawing.Size(140, 40);
            this.btnPurchase.TabIndex = 1;
            this.btnPurchase.Text = "Purchase";
            this.btnPurchase.UseVisualStyleBackColor = false;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.LightGray;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnCancel.ForeColor = System.Drawing.Color.Black;
            this.btnCancel.Location = new System.Drawing.Point(440, 410);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(140, 40);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Location = new System.Drawing.Point(171, 356);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.lblTotalPrices);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.lblVoucher);
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Size = new System.Drawing.Size(685, 36);
            this.splitContainer1.SplitterDistance = 300;
            this.splitContainer1.TabIndex = 3;
            // 
            // lblTotalPrices
            // 
            this.lblTotalPrices.AutoSize = true;
            this.lblTotalPrices.Font = new System.Drawing.Font("Microsoft Sans Serif", 18.25F);
            this.lblTotalPrices.Location = new System.Drawing.Point(151, 4);
            this.lblTotalPrices.Name = "lblTotalPrices";
            this.lblTotalPrices.Size = new System.Drawing.Size(95, 29);
            this.lblTotalPrices.TabIndex = 1;
            this.lblTotalPrices.Text = "% VND";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18.25F);
            this.label1.Location = new System.Drawing.Point(4, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(141, 29);
            this.label1.TabIndex = 0;
            this.label1.Text = "Total Price:";
            // 
            // lblVoucher
            // 
            this.lblVoucher.AutoSize = true;
            this.lblVoucher.Font = new System.Drawing.Font("Microsoft Sans Serif", 18.25F);
            this.lblVoucher.Location = new System.Drawing.Point(123, 4);
            this.lblVoucher.Name = "lblVoucher";
            this.lblVoucher.Size = new System.Drawing.Size(163, 29);
            this.lblVoucher.TabIndex = 1;
            this.lblVoucher.Text = "Not Available";
            this.lblVoucher.Click += new System.EventHandler(this.lblVoucher_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 18.25F);
            this.label2.Location = new System.Drawing.Point(3, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(114, 29);
            this.label2.TabIndex = 0;
            this.label2.Text = "Voucher:";
            // 
            // lblSeclectAll
            // 
            this.lblSeclectAll.AutoSize = true;
            this.lblSeclectAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 18.25F);
            this.lblSeclectAll.ForeColor = System.Drawing.Color.Blue;
            this.lblSeclectAll.Location = new System.Drawing.Point(24, 360);
            this.lblSeclectAll.Name = "lblSeclectAll";
            this.lblSeclectAll.Size = new System.Drawing.Size(117, 29);
            this.lblSeclectAll.TabIndex = 4;
            this.lblSeclectAll.Text = "Select all";
            this.lblSeclectAll.Click += new System.EventHandler(this.lblSeclectAll_Click);
            // 
            // OrderFRM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 470);
            this.Controls.Add(this.lblSeclectAll);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.btnPurchase);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.dataGridOrderProducts);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "OrderFRM";
            this.Text = "Order Products";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridOrderProducts)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblSeclectAll;
    }
}