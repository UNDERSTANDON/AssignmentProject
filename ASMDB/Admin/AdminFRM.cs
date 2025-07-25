using System;
using System.Drawing.Text;
using System.Windows.Forms;
using Connection;
using System.Configuration;

namespace ASMDB.Admin
{
    /// <summary>
    /// Main admin dashboard form. Allows navigation to employee and customer management.
    /// </summary>
    public partial class AdminFRM : Form
    {
        private int employeeId;
        /// <summary>
        /// Initializes the AdminFRM and wires up button events.
        /// </summary>
        public AdminFRM(int employeeId)
        {
            InitializeComponent();
            btnCustomerManager.Click += BtnCustomerManager_Click;
            btnEmployeeManager.Click += BtnEmployeeManager_Click;
            this.employeeId = employeeId;
        }

        /// <summary>
        /// Opens the CustomerManager form as a modal dialog.
        /// </summary>
        private void BtnCustomerManager_Click(object sender, EventArgs e)
        {
            var frm = new CustomerManager(employeeId);
            this.Hide();
            frm.ShowDialog();
            this.Show();
        }

        /// <summary>
        /// Opens the EmployeeManager form as a modal dialog.
        /// </summary>
        private void BtnEmployeeManager_Click(object sender, EventArgs e)
        {
            var frm = new EmployeeManager(employeeId);
            this.Hide();
            frm.ShowDialog();
            this.Show();
        }

        /// <summary>
        /// Closes the AdminFRM.
        /// </summary>
        private void BtnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AdminFRM_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit(); // Ensure application exits when admin form is closed
        }

        private void BtnStatSales_Click(object sender, EventArgs e)
        {
            var frm = new Statistic.StatSalesFRM();
            this.Hide();
            frm.ShowDialog();
            this.Show();
        }

        private void BtnEmplStat_Click(object sender, EventArgs e)
        {
            var frm = new Statistic.EmplStatFRM();
            this.Hide();
            frm.ShowDialog();
            this.Show();
        }

        private void BtnQuit_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to quit?", "Confirm Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void BtnBackup_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to backup the database?", "Confirm Backup", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                try
                {
                    string connStr = ConfigurationManager.ConnectionStrings["ASMDB"].ConnectionString;
                    DAL_Backup backup = new DAL_Backup(connStr);
                    backup.CreateBackup();
                    MessageBox.Show("Database backup completed successfully.", "Backup Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while backing up the database: {ex.Message}", "Backup Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }   
            }   
        }
    }
}

