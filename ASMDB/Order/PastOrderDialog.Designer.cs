namespace ASMDB.Order
{
    partial class PastOrderDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dataGridPastOrders;
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
            this.components = new System.ComponentModel.Container();
            this.dataGridPastOrders = new System.Windows.Forms.DataGridView();
            this.btnClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridPastOrders)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridPastOrders
            // 
            this.dataGridPastOrders.AllowUserToAddRows = false;
            this.dataGridPastOrders.AllowUserToDeleteRows = false;
            this.dataGridPastOrders.AllowUserToResizeRows = false;
            this.dataGridPastOrders.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridPastOrders.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dataGridPastOrders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridPastOrders.Location = new System.Drawing.Point(12, 12);
            this.dataGridPastOrders.Name = "dataGridPastOrders";
            this.dataGridPastOrders.ReadOnly = true;
            this.dataGridPastOrders.RowHeadersVisible = false;
            this.dataGridPastOrders.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridPastOrders.Size = new System.Drawing.Size(760, 350);
            this.dataGridPastOrders.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnClose.Location = new System.Drawing.Point(340, 380);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(120, 40);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // PastOrderDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 441);
            this.Controls.Add(this.dataGridPastOrders);
            this.Controls.Add(this.btnClose);
            this.Name = "PastOrderDialog";
            this.Text = "Past Orders";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridPastOrders)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion
    }
}