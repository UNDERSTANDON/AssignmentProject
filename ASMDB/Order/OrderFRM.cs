using ASMDB.Customer;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ASMDB.Order
{
    public partial class OrderFRM : Form
    {
        private List<OrderItem> orderItems;
        private int _cusId;
        private int _employeeId;

        public OrderFRM(List<OrderItem> items = null, int cusId = 0, int employeeId = 0)
        {
            InitializeComponent();
            orderItems = items ?? new List<OrderItem>();
            _cusId = cusId;
            _employeeId = employeeId;
            LoadOrderProductsGrid();
            btnPurchase.Click += BtnPurchase_Click;
            btnCancel.Click += (s, e) => this.Close();
            dataGridOrderProducts.CellValueChanged += DataGridOrderProducts_CellValueChanged;
            dataGridOrderProducts.CurrentCellDirtyStateChanged += DataGridOrderProducts_CurrentCellDirtyStateChanged;
            dataGridOrderProducts.CellEndEdit += DataGridOrderProducts_CellEndEdit;
            dataGridOrderProducts.RowsAdded += (s, e) => UpdateAllShippingFeesAndTotal();
            UpdateAllShippingFeesAndTotal();
        }

        private void LoadOrderProductsGrid()
        {
            dataGridOrderProducts.AutoGenerateColumns = false;
            dataGridOrderProducts.Rows.Clear();
            foreach (var item in orderItems)
            {
                dataGridOrderProducts.Rows.Add(
                    false, // Select
                    item.Prod_ID,
                    item.Prod_Name,
                    item.Price,
                    item.Quantity,
                    "Standard", // Default shipping type
                    0.00m // Shipping fee, will be calculated
                );
            }
        }

        private void BtnPurchase_Click(object sender, EventArgs e)
        {
            var selected = new List<OrderItem>();
            foreach (DataGridViewRow row in dataGridOrderProducts.Rows)
            {
                bool isChecked = false;
                if (row.Cells["colSelect"] is DataGridViewCheckBoxCell chkCell && chkCell.Value is bool b && b)
                    isChecked = true;
                if (isChecked)
                {
                    int prodId = Convert.ToInt32(row.Cells["colProductId"].Value);
                    string prodName = row.Cells["colProductName"].Value.ToString();
                    decimal price = Convert.ToDecimal(row.Cells["colProductPrice"].Value);
                    int quantity = row.Cells["colQuantity"].Value != null ? Convert.ToInt32(row.Cells["colQuantity"].Value) : 1;
                    string shipping = row.Cells["colShippingType"].Value != null ? row.Cells["colShippingType"].Value.ToString() : "Standard";
                    selected.Add(new OrderItem
                    {
                        Prod_ID = prodId,
                        Prod_Name = prodName,
                        Price = price,
                        Quantity = quantity,
                        Shipping = shipping,
                        OrderDate = DateTime.Now.ToString("yyyy-MM-dd")
                    });
                    // Clear cart item
                    UserFRM user = (UserFRM)Application.OpenForms["UserFRM"];
                    user.ClearCart();
                }
            }
            if (selected.Count == 0)
            {
                MessageBox.Show("Please select at least one item to purchase.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            MessageBox.Show($"Purchased {selected.Count} item(s)!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            var shippingForm = new ShippingFRM(selected, _cusId, _employeeId);
            this.Hide();
            shippingForm.ShowDialog();
            this.Close();
        }

        private void lblVoucher_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Voucher functionality is not implemented yet.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void DataGridOrderProducts_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dataGridOrderProducts.IsCurrentCellDirty)
            {
                dataGridOrderProducts.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void DataGridOrderProducts_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && (e.ColumnIndex == dataGridOrderProducts.Columns["colQuantity"].Index ||
                                    e.ColumnIndex == dataGridOrderProducts.Columns["colShippingType"].Index ||
                                    e.ColumnIndex == dataGridOrderProducts.Columns["colSelect"].Index))
            {
                UpdateShippingFeeForRow(e.RowIndex);
                UpdateAllShippingFeesAndTotal();
            }
        }

        private void DataGridOrderProducts_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == dataGridOrderProducts.Columns["colQuantity"].Index)
            {
                UpdateShippingFeeForRow(e.RowIndex);
                UpdateAllShippingFeesAndTotal();
            }
        }

        private void UpdateShippingFeeForRow(int rowIndex)
        {
            var row = dataGridOrderProducts.Rows[rowIndex];
            int quantity = 1;
            decimal price = 0;
            string shippingType = "Standard";
            if (row.Cells["colQuantity"].Value != null && int.TryParse(row.Cells["colQuantity"].Value.ToString(), out int q))
                quantity = q;
            if (row.Cells["colProductPrice"].Value != null && decimal.TryParse(row.Cells["colProductPrice"].Value.ToString(), out decimal p))
                price = p;
            if (row.Cells["colShippingType"].Value != null)
                shippingType = row.Cells["colShippingType"].Value.ToString();
            int n = shippingType == "Express" ? 3 : 1;
            decimal shippingFee = ((quantity * price) / 10m) * n;
            row.Cells["colShippingFee"].Value = shippingFee.ToString("F2");
        }

        private void UpdateAllShippingFeesAndTotal()
        {
            decimal total = 0;
            foreach (DataGridViewRow row in dataGridOrderProducts.Rows)
            {
                bool isChecked = row.Cells["colSelect"].Value is bool b && b;
                if (!isChecked) continue;
                int quantity = 1;
                decimal price = 0;
                decimal shippingFee = 0;
                if (row.Cells["colQuantity"].Value != null && int.TryParse(row.Cells["colQuantity"].Value.ToString(), out int q))
                    quantity = q;
                if (row.Cells["colProductPrice"].Value != null && decimal.TryParse(row.Cells["colProductPrice"].Value.ToString(), out decimal p))
                    price = p;
                if (row.Cells["colShippingFee"].Value != null && decimal.TryParse(row.Cells["colShippingFee"].Value.ToString(), out decimal s))
                    shippingFee = s;
                total += (quantity * price) + shippingFee;
            }
            lblTotalPrices.Text = total.ToString("C2");
        }

        private void lblSeclectAll_Click(object sender, EventArgs e)
        {
            bool selectAll = true;
            // If any row is unchecked, select all; otherwise, deselect all
            foreach (DataGridViewRow row in dataGridOrderProducts.Rows)
            {
                if (!(row.Cells["colSelect"].Value is bool b) || !b)
                {
                    selectAll = true;
                    // check the box
                    row.Cells["colSelect"].Value = true;
                    break;
                }
                selectAll = false;
            }
            foreach (DataGridViewRow row in dataGridOrderProducts.Rows)
            {
                row.Cells["colSelect"].Value = selectAll;
            }
            UpdateAllShippingFeesAndTotal();
        }
    }

    public class OrderItem
    {
        public int Prod_ID { get; set; }
        public string Prod_Name { get; set; }
        public decimal Price { get; set; }
        public string Shipping { get; set; }
        public int Quantity { get; set; }
        public string OrderDate { get; set; }
    }
}
