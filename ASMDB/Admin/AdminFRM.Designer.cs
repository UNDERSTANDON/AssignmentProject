namespace ASMDB.Admin
{
    partial class AdminFRM
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
            this.btnCustomerManager = new System.Windows.Forms.Button();
            this.btnEmployeeManager = new System.Windows.Forms.Button();
            this.BtnStatSales = new System.Windows.Forms.Button();
            this.BtnEmplStat = new System.Windows.Forms.Button();
            this.BtnBackup = new System.Windows.Forms.Button();
            this.BtnQuit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnCustomerManager
            // 
            this.btnCustomerManager.Font = new System.Drawing.Font("Microsoft Sans Serif", 18.25F);
            this.btnCustomerManager.Location = new System.Drawing.Point(12, 12);
            this.btnCustomerManager.Name = "btnCustomerManager";
            this.btnCustomerManager.Size = new System.Drawing.Size(167, 68);
            this.btnCustomerManager.TabIndex = 0;
            this.btnCustomerManager.Text = "Manage Customers";
            this.btnCustomerManager.UseVisualStyleBackColor = true;
            // 
            // btnEmployeeManager
            // 
            this.btnEmployeeManager.Font = new System.Drawing.Font("Microsoft Sans Serif", 18.25F);
            this.btnEmployeeManager.Location = new System.Drawing.Point(185, 12);
            this.btnEmployeeManager.Name = "btnEmployeeManager";
            this.btnEmployeeManager.Size = new System.Drawing.Size(167, 68);
            this.btnEmployeeManager.TabIndex = 1;
            this.btnEmployeeManager.Text = "Manage Employees";
            this.btnEmployeeManager.UseVisualStyleBackColor = true;
            // 
            // BtnStatSales
            // 
            this.BtnStatSales.Font = new System.Drawing.Font("Microsoft Sans Serif", 18.25F);
            this.BtnStatSales.Location = new System.Drawing.Point(12, 86);
            this.BtnStatSales.Name = "BtnStatSales";
            this.BtnStatSales.Size = new System.Drawing.Size(167, 68);
            this.BtnStatSales.TabIndex = 2;
            this.BtnStatSales.Text = "Statistic of Sales";
            this.BtnStatSales.UseVisualStyleBackColor = true;
            this.BtnStatSales.Click += new System.EventHandler(this.BtnStatSales_Click);
            // 
            // BtnEmplStat
            // 
            this.BtnEmplStat.Font = new System.Drawing.Font("Microsoft Sans Serif", 18.25F);
            this.BtnEmplStat.Location = new System.Drawing.Point(185, 86);
            this.BtnEmplStat.Name = "BtnEmplStat";
            this.BtnEmplStat.Size = new System.Drawing.Size(167, 68);
            this.BtnEmplStat.TabIndex = 3;
            this.BtnEmplStat.Text = "Employee Stat";
            this.BtnEmplStat.UseVisualStyleBackColor = true;
            this.BtnEmplStat.Click += new System.EventHandler(this.BtnEmplStat_Click);
            // 
            // BtnBackup
            // 
            this.BtnBackup.Font = new System.Drawing.Font("Microsoft Sans Serif", 18.25F);
            this.BtnBackup.Location = new System.Drawing.Point(358, 12);
            this.BtnBackup.Name = "BtnBackup";
            this.BtnBackup.Size = new System.Drawing.Size(167, 68);
            this.BtnBackup.TabIndex = 4;
            this.BtnBackup.Text = "Backup Database";
            this.BtnBackup.UseVisualStyleBackColor = true;
            this.BtnBackup.Click += new System.EventHandler(this.BtnBackup_Click);
            // 
            // BtnQuit
            // 
            this.BtnQuit.Font = new System.Drawing.Font("Microsoft Sans Serif", 18.25F);
            this.BtnQuit.Location = new System.Drawing.Point(358, 86);
            this.BtnQuit.Name = "BtnQuit";
            this.BtnQuit.Size = new System.Drawing.Size(167, 68);
            this.BtnQuit.TabIndex = 5;
            this.BtnQuit.Text = "Quit Program";
            this.BtnQuit.UseVisualStyleBackColor = true;
            this.BtnQuit.Click += new System.EventHandler(this.BtnQuit_Click);
            // 
            // AdminFRM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(533, 171);
            this.Controls.Add(this.BtnQuit);
            this.Controls.Add(this.BtnBackup);
            this.Controls.Add(this.BtnEmplStat);
            this.Controls.Add(this.BtnStatSales);
            this.Controls.Add(this.btnEmployeeManager);
            this.Controls.Add(this.btnCustomerManager);
            this.Name = "AdminFRM";
            this.Text = "AdminFRM";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AdminFRM_FormClosed);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCustomerManager;
        private System.Windows.Forms.Button btnEmployeeManager;
        private System.Windows.Forms.Button BtnStatSales;
        private System.Windows.Forms.Button BtnEmplStat;
        private System.Windows.Forms.Button BtnBackup;
        private System.Windows.Forms.Button BtnQuit;
    }
}