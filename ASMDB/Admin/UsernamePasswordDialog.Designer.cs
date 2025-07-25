namespace ASMDB.Admin
{
    partial class UsernamePasswordDialog
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.Label lblPassword;

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
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblUsername = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblUsername
            // 
            this.lblUsername.Text = "Username:";
            this.lblUsername.Location = new System.Drawing.Point(20, 20);
            this.lblUsername.Size = new System.Drawing.Size(80, 20);
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(110, 18);
            this.txtUsername.Size = new System.Drawing.Size(170, 20);
            // 
            // lblPassword
            // 
            this.lblPassword.Text = "Password:";
            this.lblPassword.Location = new System.Drawing.Point(20, 60);
            this.lblPassword.Size = new System.Drawing.Size(80, 20);
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(110, 58);
            this.txtPassword.Size = new System.Drawing.Size(170, 20);
            this.txtPassword.UseSystemPasswordChar = true;
            // 
            // btnOK
            // 
            this.btnOK.Text = "OK";
            this.btnOK.Location = new System.Drawing.Point(110, 100);
            this.btnOK.Size = new System.Drawing.Size(80, 30);
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Location = new System.Drawing.Point(200, 100);
            this.btnCancel.Size = new System.Drawing.Size(80, 30);
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // UsernamePasswordDialog
            // 
            this.AcceptButton = this.btnOK;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(320, 160);
            this.Controls.Add(this.lblUsername);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Name = "UsernamePasswordDialog";
            this.Text = "Enter Credentials";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
} 