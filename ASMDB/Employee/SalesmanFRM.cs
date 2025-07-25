using ASMDB.Connection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace ASMDB.Employee
{
    /// <summary>
    /// Employee (salesman) dashboard. Manages products and pending orders.
    /// </summary>
    public partial class SalesmanFRM : Form
    {
        private DAL_Salesman dal_Salesman;
        private Timer searchTimer;
        private string currentSearchTerm = "";
        private int employeeId;

        /// <summary>
        /// Initializes the SalesmanFRM and wires up button events.
        /// </summary>
        public SalesmanFRM(int employeeId)
        {
            InitializeComponent();
            this.employeeId = employeeId;
            cbSearchType.SelectedIndex = 1; // Default to 'Name'
            dal_Salesman = new DAL_Salesman();
            InitializeSearchTimer();
            btnUpdate.Click += BtnUpdate_Click;
            btnHide.Click += BtnHide_Click;
            btnMarkDown.Click += BtnMarkDown_Click;
            picPendingOrders.Click += PicPendingOrders_Click;
            txtSearch.TextChanged += TxtSearch_TextChanged;
            txtSearch.Leave += TxtSearch_Leave;
            cbSearchType.SelectedIndexChanged += (s, e) => LoadProductGrid();
            LoadProductGrid();
            searchTimer.Start(); // Start the timer
        }

        private void InitializeSearchTimer()
        {
            searchTimer = new Timer();
            searchTimer.Interval = 200; // 0.2 seconds
            searchTimer.Tick += SearchTimer_Tick;
        }

        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            // Reset the timer when text changes
            searchTimer.Stop();
            searchTimer.Start();
        }

        private void SearchTimer_Tick(object sender, EventArgs e)
        {
            if (txtSearch.Text.Trim() != currentSearchTerm)
            {
                currentSearchTerm = txtSearch.Text.Trim();
                LoadProductGrid();
            }
        }

        private void TxtSearch_Leave(object sender, EventArgs e)
        {
            // Stop the timer when user exits the search textbox
            searchTimer.Stop();
        }

        /// <summary>
        /// Loads product data from database into the grid.
        /// </summary>
        private void LoadProductGrid()
        {
            try
            {
                List<ProductWithCategory> products;
                string searchType = cbSearchType.SelectedItem?.ToString() ?? "Name";
                if (string.IsNullOrWhiteSpace(currentSearchTerm))
                {
                    products = dal_Salesman.GetAllProductsWithCategory();
                }
                else
                {
                    products = dal_Salesman.SearchProducts(currentSearchTerm, searchType);
                }

                var table = new DataTable();
                table.Columns.Add("Product ID", typeof(int));
                table.Columns.Add("Product Name", typeof(string));
                table.Columns.Add("Product Price", typeof(decimal));
                table.Columns.Add("Category", typeof(string));
                table.Columns.Add("Status", typeof(string));
                table.Columns.Add("MarkDown", typeof(string));

                foreach (var p in products)
                {
                    table.Rows.Add(p.Prod_ID, p.Prod_Name, p.Prod_Price, p.Category_Name, p.Status, p.MarkDown);
                }

                dataGridProducts.DataSource = table;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading products: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Opens the dialog to update the selected product.
        /// </summary>
        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridProducts.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a product to update.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            var row = dataGridProducts.SelectedRows[0];
            int prodId = Convert.ToInt32(row.Cells["Product ID"].Value);
            string prodName = row.Cells["Product Name"].Value.ToString();
            decimal prodPrice = Convert.ToDecimal(row.Cells["Product Price"].Value);
            string category = row.Cells["Category"].Value.ToString();
            string status = row.Cells["Status"].Value.ToString();
            string markDown = row.Cells["MarkDown"].Value.ToString();
            string imagePath = dal_Salesman.GetProductImagePath(prodId);

            using (var dlg = new SalesmanUpdateDialog(prodId, prodName, prodPrice, category, status, markDown, dal_Salesman, imagePath))
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    // Update the database
                    bool success = dal_Salesman.UpdateProduct(prodId, dlg.ProdName, dlg.ProdPrice, dlg.Category_ID, dlg.ProductImagePath);
                    if (success)
                    {
                        MessageBox.Show("Product updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadProductGrid(); // Refresh the grid
                    }
                    else
                    {
                        MessageBox.Show("Failed to update product.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        /// <summary>
        /// Toggles the visibility of the selected product.
        /// </summary>
        private void BtnHide_Click(object sender, EventArgs e)
        {
            if (dataGridProducts.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a product to hide/show.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            var row = dataGridProducts.SelectedRows[0];
            string currentStatus = row.Cells["Status"].Value.ToString();
            string newStatus = currentStatus == "Hidden" ? "Visible" : "Hidden";
            row.Cells["Status"].Value = newStatus;

            // Note: This is currently just updating the UI. To persist to database, 
            // you would need to add a Status field to the Products table and update it here.
            MessageBox.Show($"Product status changed to {newStatus}", "Status Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Toggles the markdown status of the selected product.
        /// </summary>
        private void BtnMarkDown_Click(object sender, EventArgs e)
        {
            if (dataGridProducts.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a product to mark down/unmark.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            var row = dataGridProducts.SelectedRows[0];
            string currentMarkDown = row.Cells["MarkDown"].Value.ToString();
            string newMarkDown = currentMarkDown == "Yes" ? "No" : "Yes";
            row.Cells["MarkDown"].Value = newMarkDown;

            // Note: This is currently just updating the UI. To persist to database, 
            // you would need to add a MarkDown field to the Products table and update it here.
            MessageBox.Show($"Product markdown status changed to {newMarkDown}", "Markdown Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Opens the pending orders dialog.
        /// </summary>
        private void PicPendingOrders_Click(object sender, EventArgs e)
        {
            var dalOrders = new ASMDB.Connection.DAL_Orders();
            var pendingOrders = dalOrders.GetPendingOrdersForAcceptance();
            if (pendingOrders.Count == 0)
            {
                MessageBox.Show("There are no pending orders to accept.", "No Orders", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            using (var dlg = new Order.OrderAcceptingDialog(employeeId))
            {
                dlg.ShowDialog();
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (searchTimer != null)
            {
                searchTimer.Stop();
                searchTimer.Dispose();
            }
            base.OnFormClosing(e);
        }

        private void SalesmanFRM_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
            Application.Exit(); // Ensure application exits when salesman form is closed
        }

        private void picEmployeeProfile_Click(object sender, EventArgs e)
        {
            var profileForm = new Profile_Icons.Profile_EmployeeFRM(employeeId);
            profileForm.ShowDialog();
        }
    }

    public class PendingOrdersDialog : Form
    {
        public PendingOrdersDialog()
        {
            this.Text = "Pending Orders";
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ClientSize = new Size(400, 300);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.WhiteSmoke;

            var lbl = new Label
            {
                Text = "Pending orders feature coming soon!",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                AutoSize = false,
                Width = 380,
                Height = 40,
                Top = 120,
                Left = 10,
                TextAlign = ContentAlignment.MiddleCenter
            };
            this.Controls.Add(lbl);
        }
    }
}
