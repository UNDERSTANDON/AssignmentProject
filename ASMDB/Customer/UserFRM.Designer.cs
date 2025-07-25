namespace ASMDB.Customer
{
    partial class UserFRM
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.FlowLayoutPanel itemListPanel;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.PictureBox picCustomerProfile;
        private System.Windows.Forms.PictureBox picCartIcon;
        private System.Windows.Forms.Label lblCartCount;
        private System.Windows.Forms.PictureBox picShippingIcon;
        private System.Windows.Forms.Button btnPastOrders;

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
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.lblCartCount = new System.Windows.Forms.Label();
            this.itemListPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.btnPastOrders = new System.Windows.Forms.Button();
            this.picCartIcon = new System.Windows.Forms.PictureBox();
            this.picCustomerProfile = new System.Windows.Forms.PictureBox();
            this.picShippingIcon = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picCartIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCustomerProfile)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picShippingIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // txtSearch
            // 
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtSearch.Location = new System.Drawing.Point(16, 14);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(240, 27);
            this.txtSearch.TabIndex = 2;
            // 
            // btnSearch
            // 
            this.btnSearch.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.btnSearch.Location = new System.Drawing.Point(266, 13);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(80, 29);
            this.btnSearch.TabIndex = 3;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            // 
            // lblCartCount
            // 
            this.lblCartCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCartCount.AutoSize = true;
            this.lblCartCount.BackColor = System.Drawing.Color.Red;
            this.lblCartCount.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblCartCount.ForeColor = System.Drawing.Color.White;
            this.lblCartCount.Location = new System.Drawing.Point(891, 2);
            this.lblCartCount.MinimumSize = new System.Drawing.Size(18, 18);
            this.lblCartCount.Name = "lblCartCount";
            this.lblCartCount.Size = new System.Drawing.Size(18, 18);
            this.lblCartCount.TabIndex = 6;
            this.lblCartCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // itemListPanel
            // 
            this.itemListPanel.AutoScroll = true;
            this.itemListPanel.BackColor = System.Drawing.Color.WhiteSmoke;
            this.itemListPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.itemListPanel.Location = new System.Drawing.Point(0, 50);
            this.itemListPanel.Name = "itemListPanel";
            this.itemListPanel.Padding = new System.Windows.Forms.Padding(16);
            this.itemListPanel.Size = new System.Drawing.Size(966, 552);
            this.itemListPanel.TabIndex = 4;
            // 
            // btnPastOrders
            // 
            this.btnPastOrders.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPastOrders.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnPastOrders.Location = new System.Drawing.Point(716, 10);
            this.btnPastOrders.Name = "btnPastOrders";
            this.btnPastOrders.Size = new System.Drawing.Size(90, 28);
            this.btnPastOrders.TabIndex = 8;
            this.btnPastOrders.Text = "Past Orders";
            this.btnPastOrders.UseVisualStyleBackColor = true;
            // 
            // picCartIcon
            // 
            this.picCartIcon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picCartIcon.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picCartIcon.Image = global::ASMDB.Properties.Resources.ShoppingCart;
            this.picCartIcon.Location = new System.Drawing.Point(866, 2);
            this.picCartIcon.Name = "picCartIcon";
            this.picCartIcon.Size = new System.Drawing.Size(40, 40);
            this.picCartIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picCartIcon.TabIndex = 5;
            this.picCartIcon.TabStop = false;
            this.picCartIcon.Click += new System.EventHandler(this.picCartIcon_Click);
            // 
            // picCustomerProfile
            // 
            this.picCustomerProfile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picCustomerProfile.Image = global::ASMDB.Properties.Resources.UserProfile;
            this.picCustomerProfile.Location = new System.Drawing.Point(916, 2);
            this.picCustomerProfile.Name = "picCustomerProfile";
            this.picCustomerProfile.Size = new System.Drawing.Size(40, 40);
            this.picCustomerProfile.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picCustomerProfile.TabIndex = 1;
            this.picCustomerProfile.TabStop = false;
            this.picCustomerProfile.Click += new System.EventHandler(this.picCustomerProfile_Click);
            // 
            // picShippingIcon
            // 
            this.picShippingIcon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picShippingIcon.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picShippingIcon.Image = global::ASMDB.Properties.Resources.ShippingIcon;
            this.picShippingIcon.Location = new System.Drawing.Point(816, 2);
            this.picShippingIcon.Name = "picShippingIcon";
            this.picShippingIcon.Size = new System.Drawing.Size(40, 40);
            this.picShippingIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picShippingIcon.TabIndex = 7;
            this.picShippingIcon.TabStop = false;
            // 
            // UserFRM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(966, 602);
            this.Controls.Add(this.lblCartCount);
            this.Controls.Add(this.picCartIcon);
            this.Controls.Add(this.itemListPanel);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.picCustomerProfile);
            this.Controls.Add(this.picShippingIcon);
            this.Controls.Add(this.btnPastOrders);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "UserFRM";
            this.Text = "Customer";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.UserFRM_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.picCartIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCustomerProfile)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picShippingIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}