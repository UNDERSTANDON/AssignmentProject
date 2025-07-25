using ASMDB.Connection;
using System.Windows.Forms;
using System;

namespace ASMDB.Order
{
    public partial class PastOrderDialog : Form
    {
        private int _cusId;
        public PastOrderDialog(int cusId)
        {
            InitializeComponent();
            _cusId = cusId;
            LoadPastOrders();
            btnClose.Click += (s, e) => this.Close();
        }

        private void LoadPastOrders()
        {
            var dal = new DAL_Orders();
            var orders = dal.GetPastOrdersByCustomerId(_cusId);
            var table = new System.Data.DataTable();
            table.Columns.Add("Order ID", typeof(int));
            table.Columns.Add("Product Name", typeof(string));
            table.Columns.Add("Cost", typeof(decimal));
            table.Columns.Add("Shipping Fee", typeof(decimal));
            table.Columns.Add("Order Date", typeof(string));
            foreach (var order in orders)
            {
                table.Rows.Add(order.Order_ID, order.ProductName, order.Cost, order.ShippingFee, string.IsNullOrEmpty(order.OrderDate) ? "" : order.OrderDate);
            }
            dataGridPastOrders.DataSource = table;
        }
    }
}
