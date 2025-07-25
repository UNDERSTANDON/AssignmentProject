using ASMDB.Models;
using Connection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace ASMDB.Admin
{
    /// <summary>
    /// Form for managing employees. Allows adding, updating, and removing employees.
    /// </summary>
    public partial class EmployeeManager : Form
    {
        private Connection.DAL_Employees dalEmployees = new Connection.DAL_Employees();
        private Timer searchTimer = new Timer();
        private string currentSearchTerm = "";
        private int employeeId;

        /// <summary>
        /// Initializes the EmployeeManager and wires up button events.
        /// </summary>
        public EmployeeManager(int employeeId)
        {
            InitializeComponent();
            this.employeeId = employeeId;
            btnAddEmployee.Click += BtnAddEmployee_Click;
            btnRemoveEmployee.Click += BtnRemoveEmployee_Click;
            btnUpdateEmployee.Click += BtnUpdateEmployee_Click;
            btnBack.Click += BtnBack_Click;
            txtSearch.TextChanged += TxtSearch_TextChanged;
            cbSearchType.SelectedIndexChanged += (s, e) => SearchTimer_Tick(null, null);
            cbSearchType.SelectedIndex = 1; // Default to 'Name'
            searchTimer.Interval = 200;
            searchTimer.Tick += SearchTimer_Tick;
            LoadEmployeeGrid();
        }

        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            searchTimer.Stop();
            searchTimer.Start();
        }

        private void SearchTimer_Tick(object sender, EventArgs e)
        {
            searchTimer.Stop();
            if (txtSearch.Text.Trim() != currentSearchTerm)
            {
                currentSearchTerm = txtSearch.Text.Trim();
                LoadEmployeeGrid();
            }
        }

        /// <summary>
        /// Loads employee data into the grid.
        /// </summary>
        private void LoadEmployeeGrid()
        {
            List<Employees> employees;
            string searchType = cbSearchType.SelectedItem?.ToString() ?? "Name";
            if (string.IsNullOrWhiteSpace(currentSearchTerm))
                employees = dalEmployees.GetAllEmployees();
            else
                employees = dalEmployees.SearchEmployees(currentSearchTerm, searchType);
            var table = new DataTable();
            table.Columns.Add("Employee ID", typeof(int));
            table.Columns.Add("Name", typeof(string));
            table.Columns.Add("Position", typeof(string));
            table.Columns.Add("Phone", typeof(string));
            table.Columns.Add("Address", typeof(string));
            table.Columns.Add("Email", typeof(string));
            table.Columns.Add("Role ID", typeof(int));
            table.Columns.Add("Role Name", typeof(string));

            var roles = new[]
            {
                new { Role_ID = 1, Role_Name = "Admin" },
                new { Role_ID = 2, Role_Name = "Sales" },
                new { Role_ID = 3, Role_Name = "Warehouse" }
            };

            foreach (var e in employees)
            {
                var role = Array.Find(roles, r => r.Role_ID == e.Role_ID);
                table.Rows.Add(e.Employee_ID, e.Employee_Name, e.Position, e.Phone, e.Address, e.Email, e.Role_ID, role?.Role_Name ?? "");
            }

            dataGridEmployees.DataSource = table;
        }

        /// <summary>
        /// Opens the dialog to add a new employee.
        /// </summary>
        private void BtnAddEmployee_Click(object sender, EventArgs e)
        {
            using (var dlg = new EmployeeDetailsDialog())
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    using (var credDlg = new UsernamePasswordDialog())
                    {
                        if (credDlg.ShowDialog() == DialogResult.OK)
                        {
                            string username = credDlg.Username;
                            string password = credDlg.Password;
                            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                            {
                                MessageBox.Show("Username and password are required.");
                                return;
                            }
                            string passwordHash = ASMDB.Connection.DAL_Login.HashPassword(password);
                            int roleId = dlg.Role == "Admin" ? 1 : dlg.Role == "Sales" ? 2 : 3;
                            bool success = dalEmployees.AddEmployee(dlg.EmployeeName, dlg.Position, dlg.Phone, dlg.Address, dlg.Email, roleId, username, passwordHash);
                            if (success)
                                MessageBox.Show("Employee added. Account is inactive until first login.");
                            else
                                MessageBox.Show("Failed to add employee.");
                            LoadEmployeeGrid();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Removes the selected employee.
        /// </summary>
        private void BtnRemoveEmployee_Click(object sender, EventArgs e)
        {
            if (dataGridEmployees.SelectedRows.Count == 0)
            {
                MessageBox.Show("Select an employee to remove.");
                return;
            }
            int empId = Convert.ToInt32(dataGridEmployees.SelectedRows[0].Cells["Employee ID"].Value);
            if (MessageBox.Show("Are you sure you want to delete this employee?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                bool success = dalEmployees.DeleteEmployee(empId);
                if (success)
                    MessageBox.Show("Employee deleted.");
                else
                    MessageBox.Show("Failed to delete employee.");
                LoadEmployeeGrid();
            }
        }

        /// <summary>
        /// Opens the dialog to update the selected employee.
        /// </summary>
        private void BtnUpdateEmployee_Click(object sender, EventArgs e)
        {
            if (dataGridEmployees.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an employee to update.", "Update Employee", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            var row = dataGridEmployees.SelectedRows[0];
            int empId = Convert.ToInt32(row.Cells["Employee ID"].Value);
            // Get image path from DAL_EmployeeProfile
            var profileDal = new DAL_EmployeeProfile(System.Configuration.ConfigurationManager.ConnectionStrings["ASMDB"].ConnectionString);
            var profile = profileDal.GetEmployeeProfile(empId);
            string imagePath = profile.Item3;
            var dlg = new EmployeeDetailsDialog(
                row.Cells[1].Value?.ToString(),
                row.Cells[2].Value?.ToString(),
                row.Cells[3].Value?.ToString(), // Phone
                row.Cells[4].Value?.ToString(), // Address
                row.Cells[5].Value?.ToString(), // Email
                row.Cells[7].Value?.ToString(),  // Role Name
                imagePath // pass image path
            );
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                int roleId = dlg.Role == "Admin" ? 1 : dlg.Role == "Sales" ? 2 : 3;
                bool success = dalEmployees.UpdateEmployee(empId, dlg.EmployeeName, dlg.Position, dlg.Phone, dlg.Address, dlg.Email, roleId, dlg.EmployeeImagePath);
                if (success)
                    MessageBox.Show("Employee updated.");
                else
                    MessageBox.Show("Failed to update employee.");
                LoadEmployeeGrid();
            }
        }

        /// <summary>
        /// Closes the EmployeeManager form.
        /// </summary>
        private void BtnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void picEmployeeProfile_Click(object sender, EventArgs e)
        {
            var profileForm = new Profile_Icons.Profile_EmployeeFRM(employeeId);
            profileForm.ShowDialog();
        }
    }
}
