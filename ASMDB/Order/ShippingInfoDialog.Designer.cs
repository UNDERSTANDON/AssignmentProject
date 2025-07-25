namespace ASMDB.Order
{
    partial class ShippingInfoDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dgvShippingInfo;
        private System.Windows.Forms.Button btnClose;

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
            this.dgvShippingInfo = new System.Windows.Forms.DataGridView();
            this.btnClose = new System.Windows.Forms.Button();
            this.Ship_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Estimate_Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Ship_Type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Shipping_Cost = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Current_Location = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Shipping_Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvShippingInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvShippingInfo
            // 
            this.dgvShippingInfo.AllowUserToAddRows = false;
            this.dgvShippingInfo.AllowUserToDeleteRows = false;
            this.dgvShippingInfo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvShippingInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvShippingInfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Ship_ID,
            this.Estimate_Date,
            this.Ship_Type,
            this.Shipping_Cost,
            this.Current_Location,
            this.Shipping_Status});
            this.dgvShippingInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.dgvShippingInfo.Location = new System.Drawing.Point(0, 0);
            this.dgvShippingInfo.Margin = new System.Windows.Forms.Padding(2);
            this.dgvShippingInfo.Name = "dgvShippingInfo";
            this.dgvShippingInfo.ReadOnly = true;
            this.dgvShippingInfo.Size = new System.Drawing.Size(600, 325);
            this.dgvShippingInfo.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(525, 333);
            this.btnClose.Margin = new System.Windows.Forms.Padding(2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(56, 24);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // Ship_ID
            // 
            this.Ship_ID.HeaderText = "ID Shipping";
            this.Ship_ID.Name = "Ship_ID";
            this.Ship_ID.ReadOnly = true;
            // 
            // Estimate_Date
            // 
            this.Estimate_Date.HeaderText = "Estimate Date";
            this.Estimate_Date.Name = "Estimate_Date";
            this.Estimate_Date.ReadOnly = true;
            // 
            // Ship_Type
            // 
            this.Ship_Type.HeaderText = "Type";
            this.Ship_Type.Name = "Ship_Type";
            this.Ship_Type.ReadOnly = true;
            // 
            // Shipping_Cost
            // 
            this.Shipping_Cost.HeaderText = "Cost";
            this.Shipping_Cost.Name = "Shipping_Cost";
            this.Shipping_Cost.ReadOnly = true;
            // 
            // Current_Location
            // 
            this.Current_Location.HeaderText = "Currently at";
            this.Current_Location.Name = "Current_Location";
            this.Current_Location.ReadOnly = true;
            // 
            // Shipping_Status
            // 
            this.Shipping_Status.HeaderText = "Status";
            this.Shipping_Status.Name = "Shipping_Status";
            this.Shipping_Status.ReadOnly = true;
            // 
            // ShippingInfoDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 366);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.dgvShippingInfo);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "ShippingInfoDialog";
            this.Text = "Shipping Information";
            ((System.ComponentModel.ISupportInitialize)(this.dgvShippingInfo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridViewTextBoxColumn Ship_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Estimate_Date;
        private System.Windows.Forms.DataGridViewTextBoxColumn Ship_Type;
        private System.Windows.Forms.DataGridViewTextBoxColumn Shipping_Cost;
        private System.Windows.Forms.DataGridViewTextBoxColumn Current_Location;
        private System.Windows.Forms.DataGridViewTextBoxColumn Shipping_Status;
    }
}