namespace ASMDB.Admin
{
    partial class EmployeeManager
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.DataGridView dataGridEmployees;
        private System.Windows.Forms.Button btnAddEmployee;
        private System.Windows.Forms.Button btnRemoveEmployee;
        private System.Windows.Forms.Button btnUpdateEmployee;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.ComboBox cbSearchType;
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
            this.dataGridEmployees = new System.Windows.Forms.DataGridView();
            this.btnAddEmployee = new System.Windows.Forms.Button();
            this.btnRemoveEmployee = new System.Windows.Forms.Button();
            this.btnUpdateEmployee = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.cbSearchType = new System.Windows.Forms.ComboBox();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.picEmployeeProfile = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridEmployees)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picEmployeeProfile)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridEmployees
            // 
            this.dataGridEmployees.AllowUserToAddRows = false;
            this.dataGridEmployees.AllowUserToDeleteRows = false;
            this.dataGridEmployees.AllowUserToResizeRows = false;
            this.dataGridEmployees.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridEmployees.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dataGridEmployees.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridEmployees.Location = new System.Drawing.Point(0, 60);
            this.dataGridEmployees.Name = "dataGridEmployees";
            this.dataGridEmployees.ReadOnly = true;
            this.dataGridEmployees.RowHeadersVisible = false;
            this.dataGridEmployees.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridEmployees.Size = new System.Drawing.Size(800, 390);
            this.dataGridEmployees.TabIndex = 0;
            // 
            // btnAddEmployee
            // 
            this.btnAddEmployee.Font = new System.Drawing.Font("Segoe UI", 10.25F);
            this.btnAddEmployee.Location = new System.Drawing.Point(320, 13);
            this.btnAddEmployee.Name = "btnAddEmployee";
            this.btnAddEmployee.Size = new System.Drawing.Size(120, 32);
            this.btnAddEmployee.TabIndex = 1;
            this.btnAddEmployee.Text = "Add";
            this.btnAddEmployee.UseVisualStyleBackColor = true;
            // 
            // btnRemoveEmployee
            // 
            this.btnRemoveEmployee.Font = new System.Drawing.Font("Segoe UI", 10.25F);
            this.btnRemoveEmployee.Location = new System.Drawing.Point(450, 13);
            this.btnRemoveEmployee.Name = "btnRemoveEmployee";
            this.btnRemoveEmployee.Size = new System.Drawing.Size(120, 32);
            this.btnRemoveEmployee.TabIndex = 2;
            this.btnRemoveEmployee.Text = "Remove";
            this.btnRemoveEmployee.UseVisualStyleBackColor = true;
            // 
            // btnUpdateEmployee
            // 
            this.btnUpdateEmployee.Font = new System.Drawing.Font("Segoe UI", 10.25F);
            this.btnUpdateEmployee.Location = new System.Drawing.Point(580, 13);
            this.btnUpdateEmployee.Name = "btnUpdateEmployee";
            this.btnUpdateEmployee.Size = new System.Drawing.Size(120, 32);
            this.btnUpdateEmployee.TabIndex = 3;
            this.btnUpdateEmployee.Text = "Update";
            this.btnUpdateEmployee.UseVisualStyleBackColor = true;
            // 
            // btnBack
            // 
            this.btnBack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnBack.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnBack.Location = new System.Drawing.Point(12, 406);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(80, 32);
            this.btnBack.TabIndex = 4;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = true;
            // 
            // cbSearchType
            // 
            this.cbSearchType.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.cbSearchType.FormattingEnabled = true;
            this.cbSearchType.Items.AddRange(new object[] {
            "ID",
            "Name"});
            this.cbSearchType.Location = new System.Drawing.Point(223, 19);
            this.cbSearchType.Name = "cbSearchType";
            this.cbSearchType.Size = new System.Drawing.Size(76, 25);
            this.cbSearchType.TabIndex = 13;
            // 
            // txtSearch
            // 
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtSearch.Location = new System.Drawing.Point(12, 18);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(200, 27);
            this.txtSearch.TabIndex = 12;
            // 
            // picEmployeeProfile
            // 
            this.picEmployeeProfile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picEmployeeProfile.Image = global::ASMDB.Properties.Resources.UserProfile;
            this.picEmployeeProfile.Location = new System.Drawing.Point(748, 12);
            this.picEmployeeProfile.Name = "picEmployeeProfile";
            this.picEmployeeProfile.Size = new System.Drawing.Size(40, 40);
            this.picEmployeeProfile.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picEmployeeProfile.TabIndex = 14;
            this.picEmployeeProfile.TabStop = false;
            this.picEmployeeProfile.Click += new System.EventHandler(this.picEmployeeProfile_Click);
            // 
            // EmployeeManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.picEmployeeProfile);
            this.Controls.Add(this.cbSearchType);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnAddEmployee);
            this.Controls.Add(this.btnRemoveEmployee);
            this.Controls.Add(this.btnUpdateEmployee);
            this.Controls.Add(this.dataGridEmployees);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "EmployeeManager";
            this.Text = "Employee Manager";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridEmployees)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picEmployeeProfile)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picEmployeeProfile;
    }
}