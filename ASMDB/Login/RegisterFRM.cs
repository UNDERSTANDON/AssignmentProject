using ASMDB.Connection;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace ASMDB.Login
{
    /// <summary>
    /// Registration form for new users.
    /// </summary>
    public partial class RegisterFRM : Form
    {
        /// <summary>
        /// Initializes the RegisterFRM.
        /// </summary>
        public RegisterFRM()
        {
            InitializeComponent();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            string username = txtUser.Text.Trim();
            string password = txtPassword.Text;
            string confirmPassword = txtConfirmPass.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword))
            {
                MessageBox.Show("Please fill in all fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (password != confirmPassword)
            {
                MessageBox.Show("Passwords do not match.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Hash the password
            string passwordHash = ComputeSha256Hash(password);

            DAL_Login dal = new DAL_Login();
            // Insert customer first (using username as name, phone=0, email="")
            int cusId = dal.InsertCustomer(username, 0, "", "");
            if (cusId == -1)
            {
                MessageBox.Show("Failed to create customer record.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // Insert user account linked to customer
            bool success = dal.InsertUserAccountWithCustomer(username, passwordHash, cusId);

            if (success)
            {
                MessageBox.Show("Registration successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Registration failed. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void hlbBacktoLogin_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Close the registration form and return to the login form
            this.Close();
        }
    }
}
