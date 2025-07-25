using ASMDB.Connection;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace ASMDB.Login
{
    public partial class Password_ResetFRM : Form
    {
        private int employeeId;
        private string username;
        public Password_ResetFRM(int employeeId, string username)
        {
            InitializeComponent();
            this.employeeId = employeeId;
            this.username = username;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            string password = txtPassword.Text;
            string confirmPassword = txtConfirmPassword.Text;

            if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword))
            {
                MessageBox.Show("Please fill in all fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (password != confirmPassword)
            {
                MessageBox.Show("Passwords do not match.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string passwordHash = ComputeSha256Hash(password);
            DAL_Login dal = new DAL_Login();
            bool success = dal.UpdateEmployeePasswordAndActivate(employeeId, passwordHash);
            if (success)
            {
                MessageBox.Show("Password reset successful! Please log in again.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Password reset failed. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string ComputeSha256Hash(string rawData)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}