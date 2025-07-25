namespace ASMDB.Admin.Statistic
{
    partial class StatSalesFRM
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
            this.cbSearchType = new System.Windows.Forms.ComboBox();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.cbSearchPast = new System.Windows.Forms.ComboBox();
            this.BtnASCDESC = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.dgvStat = new System.Windows.Forms.DataGridView();
            this.Product_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Product_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalSell = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BtnRefresh = new System.Windows.Forms.Button();
            this.BtnChooseDate = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStat)).BeginInit();
            this.SuspendLayout();
            // 
            // cbSearchType
            // 
            this.cbSearchType.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.cbSearchType.FormattingEnabled = true;
            this.cbSearchType.Items.AddRange(new object[] {
            "ID",
            "Name",
            "Sold",
            "Highest Profit",
            ""});
            this.cbSearchType.Location = new System.Drawing.Point(230, 12);
            this.cbSearchType.Name = "cbSearchType";
            this.cbSearchType.Size = new System.Drawing.Size(76, 25);
            this.cbSearchType.TabIndex = 15;
            // 
            // txtSearch
            // 
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtSearch.Location = new System.Drawing.Point(19, 11);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(200, 27);
            this.txtSearch.TabIndex = 14;
            // 
            // cbSearchPast
            // 
            this.cbSearchPast.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.cbSearchPast.FormattingEnabled = true;
            this.cbSearchPast.Items.AddRange(new object[] {
            "Last Week",
            "Last Month",
            "Last Year",
            "All Time"});
            this.cbSearchPast.Location = new System.Drawing.Point(312, 11);
            this.cbSearchPast.Name = "cbSearchPast";
            this.cbSearchPast.Size = new System.Drawing.Size(76, 25);
            this.cbSearchPast.TabIndex = 16;
            // 
            // BtnASCDESC
            // 
            this.BtnASCDESC.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.BtnASCDESC.ForeColor = System.Drawing.SystemColors.ControlText;
            this.BtnASCDESC.Location = new System.Drawing.Point(475, 10);
            this.BtnASCDESC.Name = "BtnASCDESC";
            this.BtnASCDESC.Size = new System.Drawing.Size(75, 27);
            this.BtnASCDESC.TabIndex = 17;
            this.BtnASCDESC.Text = "ASC";
            this.BtnASCDESC.UseVisualStyleBackColor = true;
            this.BtnASCDESC.Click += new System.EventHandler(this.BtnASCDESC_Click);
            // 
            // btnBack
            // 
            this.btnBack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnBack.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnBack.Location = new System.Drawing.Point(19, 406);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(80, 32);
            this.btnBack.TabIndex = 18;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // dgvStat
            // 
            this.dgvStat.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvStat.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Product_ID,
            this.Product_Name,
            this.Price,
            this.TotalSell,
            this.TotalPrice});
            this.dgvStat.Location = new System.Drawing.Point(19, 55);
            this.dgvStat.Name = "dgvStat";
            this.dgvStat.Size = new System.Drawing.Size(649, 383);
            this.dgvStat.TabIndex = 19;
            // 
            // Product_ID
            // 
            this.Product_ID.HeaderText = "Product ID";
            this.Product_ID.Name = "Product_ID";
            // 
            // Product_Name
            // 
            this.Product_Name.HeaderText = "Product_Name";
            this.Product_Name.Name = "Product_Name";
            // 
            // Price
            // 
            this.Price.HeaderText = "Price";
            this.Price.Name = "Price";
            // 
            // TotalSell
            // 
            this.TotalSell.HeaderText = "Sold";
            this.TotalSell.Name = "TotalSell";
            // 
            // TotalPrice
            // 
            this.TotalPrice.HeaderText = "Total Price";
            this.TotalPrice.Name = "TotalPrice";
            // 
            // BtnRefresh
            // 
            this.BtnRefresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.BtnRefresh.ForeColor = System.Drawing.SystemColors.ControlText;
            this.BtnRefresh.Location = new System.Drawing.Point(556, 10);
            this.BtnRefresh.Name = "BtnRefresh";
            this.BtnRefresh.Size = new System.Drawing.Size(75, 27);
            this.BtnRefresh.TabIndex = 20;
            this.BtnRefresh.Text = "Refresh";
            this.BtnRefresh.UseVisualStyleBackColor = true;
            this.BtnRefresh.Click += new System.EventHandler(this.BtnRefresh_Click);
            // 
            // BtnChooseDate
            // 
            this.BtnChooseDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.BtnChooseDate.ForeColor = System.Drawing.SystemColors.ControlText;
            this.BtnChooseDate.Location = new System.Drawing.Point(394, 10);
            this.BtnChooseDate.Name = "BtnChooseDate";
            this.BtnChooseDate.Size = new System.Drawing.Size(75, 27);
            this.BtnChooseDate.TabIndex = 23;
            this.BtnChooseDate.Text = "Choose Date";
            this.BtnChooseDate.UseVisualStyleBackColor = true;
            this.BtnChooseDate.Click += new System.EventHandler(this.BtnChooseDate_Click);
            // 
            // StatSalesFRM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(680, 450);
            this.Controls.Add(this.BtnChooseDate);
            this.Controls.Add(this.BtnRefresh);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.BtnASCDESC);
            this.Controls.Add(this.cbSearchPast);
            this.Controls.Add(this.cbSearchType);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.dgvStat);
            this.Name = "StatSalesFRM";
            this.Text = "StatSalesFRM";
            this.Load += new System.EventHandler(this.StatSalesFRM_Load_1);
            ((System.ComponentModel.ISupportInitialize)(this.dgvStat)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.ComboBox cbSearchPast;
        private System.Windows.Forms.Button BtnASCDESC;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.DataGridView dgvStat;
        private System.Windows.Forms.DataGridViewTextBoxColumn Product_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Product_Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn Price;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalSell;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalPrice;
        private System.Windows.Forms.Button BtnRefresh;
        private System.Windows.Forms.ComboBox cbSearchType;
        private System.Windows.Forms.Button BtnChooseDate;
    }
}