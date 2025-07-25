using ASMDB.Connection;
using System;
using System.Windows.Forms;

namespace ASMDB.Order
{
    public partial class OrderAcceptingDialog : Form
    {
        private int _employeeId;
        public OrderAcceptingDialog(int employeeId)
        {
            InitializeComponent();
            _employeeId = employeeId;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            LoadPendingOrders();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dataGridView1.Columns[e.ColumnIndex].Name == "BtnAcceptOrder")
            {
                int orderId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["OrderID"].Value);
                var dalOrders = new DAL_Orders();
                var dalShipping = new DAL_Shipping();
                var order = dalOrders.GetOrderById(orderId);
                if (order == null)
                {
                    MessageBox.Show("Order not found.");
                    return;
                }
                var dalCustomers = new DAL_Customers();
                var customer = dalCustomers.GetCustomerById(order.Cus_ID);
                string customerAddress = customer?.Address ?? "Unknown";
                bool orderOk = dalOrders.UpdateOrderStatusAndEmployee(orderId, "Item Prepared", _employeeId);
                bool shipOk = dalShipping.UpdateShippingStatusAndLocation(order.Ship_ID, "Delivering", customerAddress);
                if (orderOk && shipOk)
                {
                    // Update Responsible column with salesman's name
                    var dalLogin = new DAL_Login();
                    var employee = dalLogin.GetEmployeeById(_employeeId);
                    string salesmanName = employee?.Employee_Name ?? _employeeId.ToString();
                    dataGridView1.Rows[e.RowIndex].Cells["Responsible"].Value = salesmanName;
                    MessageBox.Show("Order accepted and updated successfully.");
                }
                else
                {
                    MessageBox.Show("Failed to update order or shipping status.");
                }
            }
        }

        private void LoadPendingOrders()
        {
            var dalOrders = new DAL_Orders();
            var dalCustomers = new DAL_Customers();
            var dalProducts = new DAL_Products();
            var pendingOrders = dalOrders.GetPendingOrdersForAcceptance();
            dataGridView1.Rows.Clear();
            foreach (var order in pendingOrders)
            {
                // Get customer name
                var customer = dalCustomers.GetCustomerById(order.Cus_ID);
                string customerName = customer?.Cus_Name ?? "Unknown";
                // Get product info (assuming 1 product per order)
                var productId = 0;
                var productName = "";
                var orderProducts = dalOrders.GetOrderProducts(order.Order_ID);
                if (orderProducts.Count > 0)
                {
                    productId = orderProducts[0].Prod_ID;
                    var product = dalProducts.GetProductById(productId);
                    productName = product?.Prod_Name ?? "Unknown";
                }
                dataGridView1.Rows.Add(
                    order.Order_ID,
                    productId,
                    productName,
                    order.Quantity,
                    order.Cus_ID,
                    customerName,
                    order.Employee_ID == 0 ? "Unassigned" : order.Employee_ID.ToString(),
                    "Accept"
                );
            }
        }
    }
}
