namespace ASMDB.Admin.Statistic
{
    partial class EmplStatFRM
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
            this.BtnRefresh = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.BtnASCDESC = new System.Windows.Forms.Button();
            this.cbSearchPast = new System.Windows.Forms.ComboBox();
            this.cbSearchType = new System.Windows.Forms.ComboBox();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.dgvStat = new System.Windows.Forms.DataGridView();
            this.Employee_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Employee_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SoldWeek = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SoldMonth = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SoldYear = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SoldAllTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStat)).BeginInit();
            this.SuspendLayout();
            // 
            // BtnRefresh
            // 
            this.BtnRefresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.BtnRefresh.ForeColor = System.Drawing.SystemColors.ControlText;
            this.BtnRefresh.Location = new System.Drawing.Point(468, 10);
            this.BtnRefresh.Name = "BtnRefresh";
            this.BtnRefresh.Size = new System.Drawing.Size(75, 27);
            this.BtnRefresh.TabIndex = 27;
            this.BtnRefresh.Text = "Refresh";
            this.BtnRefresh.UseVisualStyleBackColor = true;
            // 
            // btnBack
            // 
            this.btnBack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnBack.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnBack.Location = new System.Drawing.Point(12, 406);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(80, 32);
            this.btnBack.TabIndex = 25;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = true;
            // 
            // BtnASCDESC
            // 
            this.BtnASCDESC.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.BtnASCDESC.ForeColor = System.Drawing.SystemColors.ControlText;
            this.BtnASCDESC.Location = new System.Drawing.Point(387, 11);
            this.BtnASCDESC.Name = "BtnASCDESC";
            this.BtnASCDESC.Size = new System.Drawing.Size(75, 27);
            this.BtnASCDESC.TabIndex = 24;
            this.BtnASCDESC.Text = "ASC";
            this.BtnASCDESC.UseVisualStyleBackColor = true;
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
            this.cbSearchPast.Location = new System.Drawing.Point(305, 11);
            this.cbSearchPast.Name = "cbSearchPast";
            this.cbSearchPast.Size = new System.Drawing.Size(76, 25);
            this.cbSearchPast.TabIndex = 23;
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
            this.cbSearchType.Location = new System.Drawing.Point(223, 12);
            this.cbSearchType.Name = "cbSearchType";
            this.cbSearchType.Size = new System.Drawing.Size(76, 25);
            this.cbSearchType.TabIndex = 22;
            // 
            // txtSearch
            // 
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtSearch.Location = new System.Drawing.Point(12, 11);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(200, 27);
            this.txtSearch.TabIndex = 21;
            // 
            // dgvStat
            // 
            this.dgvStat.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvStat.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Employee_ID,
            this.Employee_Name,
            this.SoldWeek,
            this.SoldMonth,
            this.SoldYear,
            this.SoldAllTime});
            this.dgvStat.Location = new System.Drawing.Point(12, 55);
            this.dgvStat.Name = "dgvStat";
            this.dgvStat.Size = new System.Drawing.Size(645, 383);
            this.dgvStat.TabIndex = 26;
            // 
            // Employee_ID
            // 
            this.Employee_ID.HeaderText = "Employee ID";
            this.Employee_ID.Name = "Employee_ID";
            // 
            // Employee_Name
            // 
            this.Employee_Name.HeaderText = "Name";
            this.Employee_Name.Name = "Employee_Name";
            // 
            // SoldWeek
            // 
            this.SoldWeek.HeaderText = "Sold Last Week";
            this.SoldWeek.Name = "SoldWeek";
            // 
            // SoldMonth
            // 
            this.SoldMonth.HeaderText = "Sold Last Month";
            this.SoldMonth.Name = "SoldMonth";
            // 
            // SoldYear
            // 
            this.SoldYear.HeaderText = "Sold Last Year";
            this.SoldYear.Name = "SoldYear";
            // 
            // SoldAllTime
            // 
            this.SoldAllTime.HeaderText = "Sold All Time";
            this.SoldAllTime.Name = "SoldAllTime";
            // 
            // EmplStatFRM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(668, 450);
            this.Controls.Add(this.BtnRefresh);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.BtnASCDESC);
            this.Controls.Add(this.cbSearchPast);
            this.Controls.Add(this.cbSearchType);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.dgvStat);
            this.Name = "EmplStatFRM";
            this.Text = "EmplStatFRM";
            ((System.ComponentModel.ISupportInitialize)(this.dgvStat)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtnRefresh;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button BtnASCDESC;
        private System.Windows.Forms.ComboBox cbSearchPast;
        private System.Windows.Forms.ComboBox cbSearchType;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.DataGridView dgvStat;
        private System.Windows.Forms.DataGridViewTextBoxColumn Employee_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Employee_Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn SoldWeek;
        private System.Windows.Forms.DataGridViewTextBoxColumn SoldMonth;
        private System.Windows.Forms.DataGridViewTextBoxColumn SoldYear;
        private System.Windows.Forms.DataGridViewTextBoxColumn SoldAllTime;
    }
}