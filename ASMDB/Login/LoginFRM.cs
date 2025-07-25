using ASMDB.Connection;
using ASMDB.Models;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace ASMDB.Login
{
    /// <summary>
    /// Login form for all user roles. Handles authentication and navigation to dashboards.
    /// </summary>
    public partial class LoginFRM : Form
    {
        /// <summary>
        /// Initializes the LoginFRM.
        /// </summary>
        public LoginFRM()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles customer login logic and navigation.
        /// </summary>
        private void LoginAsCustomer(int cusId)
        {
            MessageBox.Show("Welcome Customer!");
            Customer.UserFRM userForm = new Customer.UserFRM(cusId);
            userForm.Show();
            this.Hide();
        }

        /// <summary>
        /// Handles admin login logic and navigation.
        /// </summary>
        private void LoginAsAdmin(int employeeId)
        {
            MessageBox.Show("Welcome Admin!");
            Admin.AdminFRM adminForm = new Admin.AdminFRM(employeeId);
            adminForm.Show();
            this.Hide();
        }

        /// <summary>
        /// Handles employee login logic and navigation.
        /// </summary>
        private void LoginAsEmployee(int employeeId)
        {
            MessageBox.Show("Welcome Employee!");
            Employee.SalesmanFRM employeeForm = new Employee.SalesmanFRM(employeeId);
            employeeForm.Show();
            this.Hide();
        }

        /// <summary>
        /// Handles warehouse login logic and navigation.
        /// </summary>
        private void LoginAsWareHouse(int employeeId)
        {
            MessageBox.Show("Welcome Warehouse Staff!");
            Warehouse.WarehouseFRM warehouseForm = new Warehouse.WarehouseFRM(employeeId);
            warehouseForm.Show();
            this.Hide();
        }

        /// <summary>
        /// Handles the login button click event and routes to the correct dashboard.
        /// </summary>
        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUserName.Text.Trim();
            string password = txtPassword.Text;
            string passwordHash = ComputeSha256Hash(password);

            DAL_Login dal = new DAL_Login();
            UserAccounts user = dal.GetUserAccount(username, passwordHash);

            if (user == null)
            {
                MessageBox.Show("Invalid username or password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.Beep(37, 1);
                return;
            }

            if (user.Employee_ID.HasValue)
            {
                if (!user.IsActive)
                {
                    // Show password reset form for first login
                    Password_ResetFRM resetForm = new Password_ResetFRM(user.Employee_ID.Value, user.Username);
                    resetForm.ShowDialog();
                    return;
                }
                Employees emp = GetEmployeeById(user.Employee_ID.Value);
                if (emp != null)
                {
                    switch (emp.Role_ID)
                    {
                        case 1: // Admin
                            LoginAsAdmin(user.Employee_ID.Value);
                            break;
                        case 2: // Sales
                            LoginAsEmployee(user.Employee_ID.Value);
                            break;
                        case 3: // Warehouse
                            LoginAsWareHouse(user.Employee_ID.Value);
                            break;
                        default:
                            MessageBox.Show("Unknown employee role.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                    }
                }
                else
                {
                    MessageBox.Show("Employee not found.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (user.Cus_ID.HasValue)
            {
                LoginAsCustomer(user.Cus_ID.Value);
            }
            else
            {
                MessageBox.Show("User account is not assigned to a valid role.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            user = null; // Clear user data after login
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

        private Employees GetEmployeeById(int employeeId)
        {
            DAL_Login dal = new DAL_Login();
            return dal.GetEmployeeById(employeeId);
        }

        private void hlbRegister_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            // Open the registration form
            Login.RegisterFRM res = new Login.RegisterFRM();
            res.Show();
            res.FormClosed += (s, args) => this.Show(); // Show this form again when registration is closed
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            return;
        }

        private void txtUserName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true; // Prevent the beep sound on Enter key
                txtPassword.Focus(); // Move focus to the password field
            }
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true; // Prevent the beep sound on Enter key
                btnLogin.PerformClick(); // Trigger the login button click event
            }
        }
    }
}
