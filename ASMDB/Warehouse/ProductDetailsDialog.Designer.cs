namespace ASMDB.Warehouse
{
    partial class ProductDetailsDialog
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox txtProductName;
        private System.Windows.Forms.TextBox txtProductPrice;
        private System.Windows.Forms.ComboBox cmbCategory;
        private System.Windows.Forms.NumericUpDown numQuantity;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.Label lblQuantity;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.txtProductName = new System.Windows.Forms.TextBox();
            this.txtProductPrice = new System.Windows.Forms.TextBox();
            this.cmbCategory = new System.Windows.Forms.ComboBox();
            this.numQuantity = new System.Windows.Forms.NumericUpDown();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblName = new System.Windows.Forms.Label();
            this.lblPrice = new System.Windows.Forms.Label();
            this.lblCategory = new System.Windows.Forms.Label();
            this.lblQuantity = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numQuantity)).BeginInit();
            this.SuspendLayout();
            // 
            // lblName
            // 
            this.lblName.Text = "Product Name:";
            this.lblName.Location = new System.Drawing.Point(24, 24);
            this.lblName.Size = new System.Drawing.Size(100, 23);
            // 
            // txtProductName
            // 
            this.txtProductName.Location = new System.Drawing.Point(140, 24);
            this.txtProductName.Size = new System.Drawing.Size(200, 23);
            this.txtProductName.Font = new System.Drawing.Font("Segoe UI", 10F);
            // 
            // lblPrice
            // 
            this.lblPrice.Text = "Price:";
            this.lblPrice.Location = new System.Drawing.Point(24, 64);
            this.lblPrice.Size = new System.Drawing.Size(100, 23);
            // 
            // txtProductPrice
            // 
            this.txtProductPrice.Location = new System.Drawing.Point(140, 64);
            this.txtProductPrice.Size = new System.Drawing.Size(200, 23);
            this.txtProductPrice.Font = new System.Drawing.Font("Segoe UI", 10F);
            // 
            // lblCategory
            // 
            this.lblCategory.Text = "Category:";
            this.lblCategory.Location = new System.Drawing.Point(24, 104);
            this.lblCategory.Size = new System.Drawing.Size(100, 23);
            // 
            // cmbCategory
            // 
            this.cmbCategory.Location = new System.Drawing.Point(140, 104);
            this.cmbCategory.Size = new System.Drawing.Size(200, 23);
            this.cmbCategory.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            // 
            // lblQuantity
            // 
            this.lblQuantity.Text = "Quantity:";
            this.lblQuantity.Location = new System.Drawing.Point(24, 144);
            this.lblQuantity.Size = new System.Drawing.Size(100, 23);
            // 
            // numQuantity
            // 
            this.numQuantity.Location = new System.Drawing.Point(140, 144);
            this.numQuantity.Size = new System.Drawing.Size(200, 23);
            this.numQuantity.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.numQuantity.Minimum = 1;
            this.numQuantity.Maximum = 10000;
            // 
            // btnOK
            // 
            this.btnOK.Text = "OK";
            this.btnOK.Location = new System.Drawing.Point(140, 200);
            this.btnOK.Size = new System.Drawing.Size(90, 32);
            this.btnOK.Font = new System.Drawing.Font("Segoe UI", 10F);
            // 
            // btnCancel
            // 
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Location = new System.Drawing.Point(250, 200);
            this.btnCancel.Size = new System.Drawing.Size(90, 32);
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 10F);
            // 
            // ProductDetailsDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(380, 260);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.txtProductName);
            this.Controls.Add(this.lblPrice);
            this.Controls.Add(this.txtProductPrice);
            this.Controls.Add(this.lblCategory);
            this.Controls.Add(this.cmbCategory);
            this.Controls.Add(this.lblQuantity);
            this.Controls.Add(this.numQuantity);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProductDetailsDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Product Details";
            ((System.ComponentModel.ISupportInitialize)(this.numQuantity)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
} 