using ASMDB.Connection;
using System;
using System.Windows.Forms;

namespace ASMDB.Warehouse
{
    /// <summary>
    /// Modal dialog for adding or updating a product. Handles product data entry and validation.
    /// </summary>
    public partial class ProductDetailsDialog : Form
    {
        private DAL_Warehouse dalWarehouse;

        /// <summary>
        /// Gets the entered product name.
        /// </summary>
        public new string ProductName => txtProductName.Text.Trim();
        /// <summary>
        /// Gets the entered product price.
        /// </summary>
        public decimal ProductPrice => decimal.TryParse(txtProductPrice.Text, out var p) ? p : 0;
        /// <summary>
        /// Gets the selected category ID.
        /// </summary>
        public int CategoryID => (cmbCategory.SelectedItem as CategoryItem)?.ID ?? 0;
        /// <summary>
        /// Gets the selected category name.
        /// </summary>
        public string CategoryName => (cmbCategory.SelectedItem as CategoryItem)?.Name ?? string.Empty;
        /// <summary>
        /// Gets the entered quantity.
        /// </summary>
        public int Quantity => (int)numQuantity.Value;

        /// <summary>
        /// Initializes the dialog with optional product data.
        /// </summary>
        public ProductDetailsDialog(string title = "Add Product", string name = "", decimal price = 0, int categoryId = 0, int quantity = 1)
        {
            InitializeComponent();
            dalWarehouse = new DAL_Warehouse();
            this.Text = title;
            txtProductName.Text = name;
            txtProductPrice.Text = price > 0 ? price.ToString() : "";
            numQuantity.Value = quantity > 0 ? quantity : 1;

            // Populate categories from database
            LoadCategories(categoryId);

            btnOK.Click += BtnOK_Click;
            btnCancel.Click += (s, e) => this.DialogResult = DialogResult.Cancel;
        }

        private void LoadCategories(int selectedCategoryId = 0)
        {
            cmbCategory.Items.Clear();
            var categories = dalWarehouse.GetAllCategories();

            foreach (var category in categories)
            {
                var item = new CategoryItem(category.Category_ID, category.Category_Name);
                cmbCategory.Items.Add(item);

                if (category.Category_ID == selectedCategoryId)
                {
                    cmbCategory.SelectedItem = item;
                }
            }

            // If no category is selected and we have categories, select the first one
            if (cmbCategory.SelectedItem == null && cmbCategory.Items.Count > 0)
            {
                cmbCategory.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// Handles the OK button click event and validates input.
        /// </summary>
        private void BtnOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtProductName.Text))
            {
                MessageBox.Show("Product name is required.");
                return;
            }
            if (!decimal.TryParse(txtProductPrice.Text, out var price) || price < 0)
            {
                MessageBox.Show("Enter a valid price.");
                return;
            }
            if (cmbCategory.SelectedItem == null)
            {
                MessageBox.Show("Select a category.");
                return;
            }
            this.DialogResult = DialogResult.OK;
        }

        private class CategoryItem
        {
            public int ID { get; }
            public string Name { get; }
            public CategoryItem(int id, string name) { ID = id; Name = name; }
            public override string ToString() => Name;
        }
    }
}