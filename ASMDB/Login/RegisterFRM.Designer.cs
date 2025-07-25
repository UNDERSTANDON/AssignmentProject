namespace ASMDB.Login
{
    partial class RegisterFRM
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
            this.panelMain = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.picConfirmPass = new System.Windows.Forms.PictureBox();
            this.picPass = new System.Windows.Forms.PictureBox();
            this.picUser = new System.Windows.Forms.PictureBox();
            this.txtConfirmPass = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.hlbBacktoLogin = new System.Windows.Forms.LinkLabel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnRegister = new System.Windows.Forms.Button();
            this.panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picConfirmPass)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPass)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picUser)).BeginInit();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelMain.Controls.Add(this.splitContainer1);
            this.panelMain.Controls.Add(this.label1);
            this.panelMain.Controls.Add(this.hlbBacktoLogin);
            this.panelMain.Controls.Add(this.btnCancel);
            this.panelMain.Controls.Add(this.btnRegister);
            this.panelMain.Location = new System.Drawing.Point(40, 40);
            this.panelMain.Name = "panelMain";
            this.panelMain.Padding = new System.Windows.Forms.Padding(20);
            this.panelMain.Size = new System.Drawing.Size(530, 390);
            this.panelMain.TabIndex = 20;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Font = new System.Drawing.Font("Segoe UI", 12.25F);
            this.splitContainer1.Location = new System.Drawing.Point(20, 80);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.label5);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.picConfirmPass);
            this.splitContainer1.Panel2.Controls.Add(this.picPass);
            this.splitContainer1.Panel2.Controls.Add(this.picUser);
            this.splitContainer1.Panel2.Controls.Add(this.txtConfirmPass);
            this.splitContainer1.Panel2.Controls.Add(this.txtPassword);
            this.splitContainer1.Panel2.Controls.Add(this.txtUser);
            this.splitContainer1.Size = new System.Drawing.Size(490, 210);
            this.splitContainer1.SplitterDistance = 150;
            this.splitContainer1.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 133);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(106, 23);
            this.label5.TabIndex = 2;
            this.label5.Text = "Re-Password";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 83);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 23);
            this.label3.TabIndex = 1;
            this.label3.Text = "Password";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 23);
            this.label2.TabIndex = 0;
            this.label2.Text = "Username";
            // 
            // picConfirmPass
            // 
            this.picConfirmPass.Image = global::ASMDB.Properties.Resources.Password;
            this.picConfirmPass.Location = new System.Drawing.Point(5, 130);
            this.picConfirmPass.Name = "picConfirmPass";
            this.picConfirmPass.Size = new System.Drawing.Size(30, 30);
            this.picConfirmPass.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picConfirmPass.TabIndex = 0;
            this.picConfirmPass.TabStop = false;
            // 
            // picPass
            // 
            this.picPass.Image = global::ASMDB.Properties.Resources.Password;
            this.picPass.Location = new System.Drawing.Point(5, 80);
            this.picPass.Name = "picPass";
            this.picPass.Size = new System.Drawing.Size(30, 30);
            this.picPass.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picPass.TabIndex = 1;
            this.picPass.TabStop = false;
            // 
            // picUser
            // 
            this.picUser.Image = global::ASMDB.Properties.Resources.UserProfile;
            this.picUser.Location = new System.Drawing.Point(5, 30);
            this.picUser.Name = "picUser";
            this.picUser.Size = new System.Drawing.Size(30, 30);
            this.picUser.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picUser.TabIndex = 2;
            this.picUser.TabStop = false;
            // 
            // txtConfirmPass
            // 
            this.txtConfirmPass.BackColor = System.Drawing.Color.White;
            this.txtConfirmPass.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtConfirmPass.Font = new System.Drawing.Font("Segoe UI", 12.25F);
            this.txtConfirmPass.Location = new System.Drawing.Point(40, 130);
            this.txtConfirmPass.Name = "txtConfirmPass";
            this.txtConfirmPass.Size = new System.Drawing.Size(250, 29);
            this.txtConfirmPass.TabIndex = 2;
            this.txtConfirmPass.UseSystemPasswordChar = true;
            // 
            // txtPassword
            // 
            this.txtPassword.BackColor = System.Drawing.Color.White;
            this.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPassword.Font = new System.Drawing.Font("Segoe UI", 12.25F);
            this.txtPassword.Location = new System.Drawing.Point(40, 80);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(250, 29);
            this.txtPassword.TabIndex = 1;
            this.txtPassword.UseSystemPasswordChar = true;
            // 
            // txtUser
            // 
            this.txtUser.BackColor = System.Drawing.Color.White;
            this.txtUser.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUser.Font = new System.Drawing.Font("Segoe UI", 12.25F);
            this.txtUser.Location = new System.Drawing.Point(40, 30);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(250, 29);
            this.txtUser.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 24.25F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(180, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(147, 45);
            this.label1.TabIndex = 8;
            this.label1.Text = "Register";
            // 
            // hlbBacktoLogin
            // 
            this.hlbBacktoLogin.ActiveLinkColor = System.Drawing.Color.DodgerBlue;
            this.hlbBacktoLogin.AutoSize = true;
            this.hlbBacktoLogin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.hlbBacktoLogin.Font = new System.Drawing.Font("Segoe UI", 12.25F, System.Drawing.FontStyle.Underline);
            this.hlbBacktoLogin.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.hlbBacktoLogin.Location = new System.Drawing.Point(151, 363);
            this.hlbBacktoLogin.Name = "hlbBacktoLogin";
            this.hlbBacktoLogin.Size = new System.Drawing.Size(206, 23);
            this.hlbBacktoLogin.TabIndex = 7;
            this.hlbBacktoLogin.TabStop = true;
            this.hlbBacktoLogin.Text = "Already have an account?";
            this.hlbBacktoLogin.VisitedLinkColor = System.Drawing.Color.MediumSlateBlue;
            this.hlbBacktoLogin.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.hlbBacktoLogin_LinkClicked);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.Gainsboro;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 14.25F);
            this.btnCancel.ForeColor = System.Drawing.Color.Black;
            this.btnCancel.Location = new System.Drawing.Point(40, 320);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(120, 40);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            // 
            // btnRegister
            // 
            this.btnRegister.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.btnRegister.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRegister.FlatAppearance.BorderSize = 0;
            this.btnRegister.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRegister.Font = new System.Drawing.Font("Segoe UI", 14.25F);
            this.btnRegister.ForeColor = System.Drawing.Color.White;
            this.btnRegister.Location = new System.Drawing.Point(350, 320);
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Size = new System.Drawing.Size(140, 40);
            this.btnRegister.TabIndex = 5;
            this.btnRegister.Text = "Register";
            this.btnRegister.UseVisualStyleBackColor = false;
            this.btnRegister.Click += new System.EventHandler(this.btnRegister_Click);
            // 
            // RegisterFRM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(610, 470);
            this.Controls.Add(this.panelMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "RegisterFRM";
            this.Text = "Register";
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picConfirmPass)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPass)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picUser)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtConfirmPass;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnRegister;
        private System.Windows.Forms.LinkLabel hlbBacktoLogin;
        private System.Windows.Forms.PictureBox picUser;
        private System.Windows.Forms.PictureBox picPass;
        private System.Windows.Forms.PictureBox picConfirmPass;
    }
}