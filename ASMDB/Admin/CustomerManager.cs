using ASMDB.Connection;
using ASMDB.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace ASMDB.Admin
{
    /// <summary>
    /// Form for managing customers. Allows adding, updating, and removing customers.
    /// </summary>
    public partial class CustomerManager : Form
    {
        private DAL_Customers dalCustomers = new DAL_Customers();
        private Timer searchTimer = new Timer();
        private string currentSearchTerm = "";
        private int employeeId;

        /// <summary>
        /// Initializes the CustomerManager and wires up button events.
        /// </summary>
        public CustomerManager(int employeeId)
        {
            InitializeComponent();
            btnAddCustomer.Click += BtnAddCustomer_Click;
            btnRemoveCustomer.Click += BtnRemoveCustomer_Click;
            btnUpdateCustomer.Click += BtnUpdateCustomer_Click;
            btnBack.Click += BtnBack_Click;
            txtSearch.TextChanged += TxtSearch_TextChanged;
            cbSearchType.SelectedIndexChanged += (s, e) => SearchTimer_Tick(null, null);
            cbSearchType.SelectedIndex = 1; // Default to 'Name'
            searchTimer.Interval = 200;
            searchTimer.Tick += SearchTimer_Tick;
            LoadCustomerGrid();
            this.employeeId = employeeId;
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
                LoadCustomerGrid();
            }
        }

        /// <summary>
        /// Loads customer data into the grid.
        /// </summary>
        private void LoadCustomerGrid()
        {
            List<Customers> customers;
            string searchType = cbSearchType.SelectedItem?.ToString() ?? "Name";
            if (string.IsNullOrWhiteSpace(currentSearchTerm))
                customers = dalCustomers.GetAllCustomers();
            else
                customers = dalCustomers.SearchCustomers(currentSearchTerm, searchType);
            var table = new DataTable();
            table.Columns.Add("Customer ID", typeof(int));
            table.Columns.Add("Name", typeof(string));
            table.Columns.Add("Phone", typeof(string));
            table.Columns.Add("Address", typeof(string));
            table.Columns.Add("Email", typeof(string));
            foreach (var c in customers)
                table.Rows.Add(c.Cus_ID, c.Cus_Name, c.Phone, c.Address, c.Email);
            dataGridCustomers.DataSource = table;
        }

        /// <summary>
        /// Opens the dialog to add a new customer.
        /// </summary>
        private void BtnAddCustomer_Click(object sender, EventArgs e)
        {
            using (var dlg = new CustomerDetailsDialog("", "", "", ""))
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    bool success = dalCustomers.AddCustomer(dlg.CustomerName, int.Parse(dlg.Phone), dlg.Address, dlg.Email);
                    if (success)
                        MessageBox.Show("Customer added.");
                    else
                        MessageBox.Show("Failed to add customer.");
                    LoadCustomerGrid();
                }
            }
        }

        /// <summary>
        /// Removes the selected customer.
        /// </summary>
        private void BtnRemoveCustomer_Click(object sender, EventArgs e)
        {
            if (dataGridCustomers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Select a customer to remove.");
                return;
            }
            int cusId = Convert.ToInt32(dataGridCustomers.SelectedRows[0].Cells["Customer ID"].Value);
            if (MessageBox.Show("Are you sure you want to delete this customer?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                bool success = dalCustomers.DeleteCustomer(cusId);
                if (success)
                    MessageBox.Show("Customer deleted.");
                else
                    MessageBox.Show("Failed to delete customer.");
                LoadCustomerGrid();
            }
        }

        /// <summary>
        /// Opens the dialog to update the selected customer.
        /// </summary>
        private void BtnUpdateCustomer_Click(object sender, EventArgs e)
        {
            if (dataGridCustomers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a customer to update.", "Update Customer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            var row = dataGridCustomers.SelectedRows[0];
            int cusId = Convert.ToInt32(row.Cells["Customer ID"].Value);
            var dlg = new CustomerDetailsDialog(
                row.Cells["Name"].Value?.ToString(),
                row.Cells["Phone"].Value?.ToString(),
                row.Cells["Address"].Value?.ToString(),
                row.Cells["Email"].Value?.ToString(),
                null // Assuming the image path is optional and not provided here
            );
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                if (int.TryParse(dlg.Phone, out int phone))
                {
                    bool success = dalCustomers.UpdateCustomer(cusId, dlg.CustomerName, phone, dlg.Address, dlg.Email, dlg.CustomerImagePath);
                    if (success)
                        MessageBox.Show("Customer updated.");
                    else
                        MessageBox.Show("Failed to update customer.");
                }
                else
                {
                    MessageBox.Show("Invalid phone number format. Please enter a valid number.");
                }
                LoadCustomerGrid();
            }
        }

        /// <summary>
        /// Closes the CustomerManager form.
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
