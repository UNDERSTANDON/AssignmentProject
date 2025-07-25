using ASMDB.Connection;
using ASMDB.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ASMDB.Warehouse
{
    /// <summary>
    /// Warehouse management dashboard. Manages products and categories.
    /// </summary>
    public partial class WarehouseFRM : Form
    {
        private List<ProductWithQuantity> products;
        private List<Category> categories;
        private DAL_Warehouse dalWarehouse;
        private Timer searchTimer;
        private int employeeId;

        /// <summary>
        /// Initializes the WarehouseFRM and wires up button events.
        /// </summary>
        public WarehouseFRM(int employeeId)
        {
            InitializeComponent();
            this.employeeId = employeeId;
            cbSearchType.SelectedIndex = 1; // Default to 'Name'
            dalWarehouse = new DAL_Warehouse();
            InitializeSearchTimer();
            InitializeSearchEvents();
            button1.Click += BtnAddItem_Click;
            button2.Click += BtnRemoveItem_Click;
            button3.Click += BtnUpdateItem_Click;
            cbSearchType.SelectedIndexChanged += (s, e) => SearchTimer_Tick(null, null);
            LoadData();
        }

        private void InitializeSearchTimer()
        {
            searchTimer = new Timer();
            searchTimer.Interval = 200; // 0.2 seconds
            searchTimer.Tick += SearchTimer_Tick;
        }

        private void InitializeSearchEvents()
        {
            // Set up the search textbox from designer
            textBox1.Text = "Search products...";
            textBox1.ForeColor = Color.Gray;
            textBox1.TextChanged += TxtSearch_TextChanged;
            textBox1.LostFocus += TxtSearch_LostFocus;
            textBox1.GotFocus += TxtSearch_GotFocus;
        }

        private void TxtSearch_GotFocus(object sender, EventArgs e)
        {
            if (textBox1.Text == "Search products...")
            {
                textBox1.Text = "";
                textBox1.ForeColor = Color.Black;
            }
        }

        private void TxtSearch_LostFocus(object sender, EventArgs e)
        {
            searchTimer.Stop();
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                textBox1.Text = "Search products...";
                textBox1.ForeColor = Color.Gray;
            }
        }

        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            // Don't trigger search if it's the placeholder text
            if (textBox1.Text == "Search products...")
                return;

            searchTimer.Stop();
            searchTimer.Start();
        }

        private void SearchTimer_Tick(object sender, EventArgs e)
        {
            searchTimer.Stop();
            string searchTerm = textBox1.Text.Trim();
            string searchType = cbSearchType.SelectedItem?.ToString() ?? "Name";
            // Don't search if it's the placeholder text
            if (string.IsNullOrEmpty(searchTerm) || searchTerm == "Search products...")
            {
                LoadData(); // Load all products
            }
            else
            {
                products = dalWarehouse.SearchProducts(searchTerm, searchType);
                LoadProductGrid();
            }
        }

        /// <summary>
        /// Loads data from the database.
        /// </summary>
        private void LoadData()
        {
            products = dalWarehouse.GetAllProductsWithQuantity();
            categories = dalWarehouse.GetAllCategories();
            LoadProductGrid();
        }

        /// <summary>
        /// Loads product data into the grid.
        /// </summary>
        private void LoadProductGrid()
        {
            var table = new DataTable();
            table.Columns.Add("Product ID", typeof(int));
            table.Columns.Add("Product Name", typeof(string));
            table.Columns.Add("Product Price", typeof(decimal));
            table.Columns.Add("Category ID", typeof(int));
            table.Columns.Add("Category Name", typeof(string));
            table.Columns.Add("Quantity", typeof(int));
            foreach (var p in products)
            {
                table.Rows.Add(p.Prod_ID, p.Prod_Name, p.Prod_Price, p.Category_ID, p.Category_Name, p.Quantity);
            }
            dataGridProducts.DataSource = table;
        }

        /// <summary>
        /// Opens the dialog to add a new product.
        /// </summary>
        private void BtnAddItem_Click(object sender, EventArgs e)
        {
            using (var dlg = new ProductDetailsDialog("Add Product"))
            {
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    int newProdId = dalWarehouse.AddProduct(dlg.ProductName, dlg.ProductPrice, dlg.CategoryID, dlg.Quantity);
                    if (newProdId > 0)
                    {
                        MessageBox.Show("Product added successfully!");
                        LoadData(); // Reload data from database
                    }
                    else
                    {
                        MessageBox.Show("Failed to add product.");
                    }
                }
            }
        }

        /// <summary>
        /// Removes the selected product.
        /// </summary>
        private void BtnRemoveItem_Click(object sender, EventArgs e)
        {
            if (dataGridProducts.SelectedRows.Count == 0)
            {
                MessageBox.Show("Select a product to remove.");
                return;
            }
            var id = (int)dataGridProducts.SelectedRows[0].Cells["Product ID"].Value;
            var prod = products.FirstOrDefault(p => p.Prod_ID == id);
            if (prod != null)
            {
                if (MessageBox.Show($"Remove '{prod.Prod_Name}'?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    bool success = dalWarehouse.DeleteProduct(id);
                    if (success)
                    {
                        MessageBox.Show("Product removed successfully!");
                        LoadData(); // Reload data from database
                    }
                    else
                    {
                        MessageBox.Show("Failed to remove product.");
                    }
                }
            }
        }

        /// <summary>
        /// Opens the dialog to update the selected product.
        /// </summary>
        private void BtnUpdateItem_Click(object sender, EventArgs e)
        {
            if (dataGridProducts.SelectedRows.Count == 0)
            {
                MessageBox.Show("Select a product to update.");
                return;
            }
            var id = (int)dataGridProducts.SelectedRows[0].Cells["Product ID"].Value;
            var prod = products.FirstOrDefault(p => p.Prod_ID == id);
            if (prod != null)
            {
                using (var dlg = new ProductDetailsDialog("Update Product", prod.Prod_Name, prod.Prod_Price, prod.Category_ID, prod.Quantity))
                {
                    if (dlg.ShowDialog(this) == DialogResult.OK)
                    {
                        bool success = dalWarehouse.UpdateProduct(id, dlg.ProductName, dlg.ProductPrice, dlg.CategoryID, dlg.Quantity);
                        if (success)
                        {
                            MessageBox.Show("Product updated successfully!");
                            LoadData(); // Reload data from database
                        }
                        else
                        {
                            MessageBox.Show("Failed to update product.");
                        }
                    }
                }
            }
        }

        private void WarehouseFRM_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
            Application.Exit(); // Ensure application exits when warehouse form is closed
        }

        private void picEmployeeProfile_Click(object sender, EventArgs e)
        {
            var profileForm = new Profile_Icons.Profile_EmployeeFRM(employeeId);
            profileForm.ShowDialog();
        }
    }
}
