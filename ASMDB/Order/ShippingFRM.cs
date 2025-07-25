using ASMDB.Connection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace ASMDB.Order
{
    public partial class ShippingFRM : Form
    {
        private List<OrderItem> orderItems;
        private Button btnBackToShopping;
        private DAL_Orders dalOrders;
        private int _cusId;
        private int _employeeId;
        private bool _viewOnly = false;

        public ShippingFRM(List<OrderItem> items, int cusId, int employeeId = 0, bool viewOnly = false)
        {
            InitializeComponent();
            orderItems = items ?? new List<OrderItem>();
            _cusId = cusId;
            _employeeId = employeeId;
            _viewOnly = viewOnly;
            dalOrders = new DAL_Orders();

            if (!_viewOnly && orderItems.Count > 0)
                InsertOrderToDatabase();

            var orders = dalOrders.GetPendingOrdersByCustomerId(_cusId);
            orders = orders.Where(o => !o.Is_Done).ToList(); // In-memory filter as extra safeguard
            LoadOrderGrid(orders);
            btnBackToShopping.Click += (s, e) => this.Close();
            dataGridOrders.CellContentClick += dataGridOrders_CellContentClick;
            dataGridOrders.SelectionChanged += dataGridOrders_SelectionChanged;
            BtShipped.Enabled = false;
        }

        private void LoadOrderGrid(List<ASMDB.Models.Orders> orders)
        {
            dataGridOrders.Rows.Clear();
            var dalShipping = new DAL_Shipping();
            foreach (var order in orders)
            {
                decimal totalPrice = order.Cost * order.Quantity;
                var shipping = dalShipping.GetShippingById(order.Ship_ID);
                string shippingStatus = shipping?.Shipping_Status ?? "Unknown";
                dataGridOrders.Rows.Add(
                    order.Order_ID,
                    order.Cus_ID,
                    totalPrice,
                    shippingStatus,
                    order.salesman_Status,
                    "See details" // Set button text for Detail Shipping
                );
            }
        }

        private void InsertOrderToDatabase()
        {
            try
            {
                if (orderItems.Count == 0)
                {
                    MessageBox.Show("No items to process.", "No Items", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Determine the overall shipping method (if all items have same shipping, use that; otherwise use "Mixed")
                string overallShipping = orderItems.All(item => item.Shipping == orderItems[0].Shipping)
                    ? orderItems[0].Shipping
                    : "Mixed";

                // Insert order into database
                bool success = dalOrders.InsertOrderWithProducts("Preparing Items", overallShipping, orderItems, _cusId, _employeeId);

                if (success)
                {
                    MessageBox.Show($"Order successfully created with {orderItems.Count} items!\nShipping: {overallShipping}",
                        "Order Created", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Failed to create order in database.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating order: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridOrders_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dataGridOrders.Columns[e.ColumnIndex].Name == "BtnShippingInfo")
            {
                var dialog = new ShippingInfoDialog(_cusId);
                dialog.ShowDialog();
            }
        }
        public static void ShowForCustomerOrders(int cusId, int employeeId = 0)
        {
            var form = new ShippingFRM(null, cusId, employeeId, viewOnly: true);
            form.ShowDialog();
        }

        private void BtShipped_Click(object sender, EventArgs e)
        {
            if (dataGridOrders.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an order to mark as shipped.");
                return;
            }
            var row = dataGridOrders.SelectedRows[0];
            int orderId = Convert.ToInt32(row.Cells["colProductId"].Value);
            var dalOrders = new DAL_Orders();
            var dalShipping = new DAL_Shipping();
            var (currentLocation, customerAddress, shippingStatus, shipId) = dalOrders.GetOrderShippingAndAddress(orderId);
            if (shippingStatus == "Delivering" && currentLocation == customerAddress)
            {
                bool doneOk = dalOrders.MarkOrderAsDone(orderId);
                dalShipping.UpdateShippingStatusAndLocation(shipId, "Delivered", customerAddress);
                MessageBox.Show(doneOk ? "Order marked as delivered." : "Failed to mark order as delivered in DB.");
                // Refresh the grid to remove completed orders
                var orders = dalOrders.GetPendingOrdersByCustomerId(_cusId);
                orders = orders.Where(o => !o.Is_Done).ToList();
                LoadOrderGrid(orders);
                BtShipped.Enabled = false;
            }
            else
            {
                MessageBox.Show("Order cannot be marked as delivered. Make sure it is in 'Delivering' status and at the customer's address.");
            }
        }

        private void dataGridOrders_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridOrders.SelectedRows.Count == 0)
            {
                BtShipped.Enabled = false;
                return;
            }
            var row = dataGridOrders.SelectedRows[0];
            int orderId = Convert.ToInt32(row.Cells["colProductId"].Value);
            var dalOrders = new DAL_Orders();
            var (currentLocation, customerAddress, shippingStatus, _) = dalOrders.GetOrderShippingAndAddress(orderId);
            BtShipped.Enabled = (shippingStatus == "Delivering" && currentLocation == customerAddress);
        }
    }
}
