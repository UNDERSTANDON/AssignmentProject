using ASMDB.Models;
using ASMDB.Order;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ASMDB.Connection
{
    /// <summary>
    /// Data access layer for order and order product management.
    /// </summary>
    public class DAL_Orders
    {
        private DAL_Shipping dal_Shipping;

        /// <summary>
        /// Initializes a new instance of the DAL_Orders class.
        /// </summary>
        public DAL_Orders()
        {
            dal_Shipping = new DAL_Shipping();
        }

        /// <summary>
        /// Inserts a new order into the database.
        /// </summary>
        /// <param name="status">Salesman status.</param>
        /// <param name="shipping">Shipping type.</param>
        /// <param name="cusId">Customer ID.</param>
        /// <param name="employeeId">Employee ID.</param>
        /// <param name="quantity">Order quantity.</param>
        /// <param name="cost">Order cost.</param>
        /// <returns>The new order ID, or -1 if insertion failed.</returns>
        public int InsertOrder(string status, string shipping, int cusId, int employeeId, int quantity, decimal cost)
        {
            string connStr = ConfigurationManager.ConnectionStrings["ASMDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    // Find max Order_ID and increment by 1
                    int newOrderId = 1;
                    string getMaxIdQuery = "SELECT ISNULL(MAX(Order_ID), 1000) + 1 FROM Orders";
                    using (SqlCommand getMaxIdCmd = new SqlCommand(getMaxIdQuery, conn))
                    {
                        newOrderId = (int)getMaxIdCmd.ExecuteScalar();
                    }

                    // Create shipping record first
                    decimal shippingCost = shipping == "Express" ? 12.99m : 5.99m;
                    int shipId = dal_Shipping.CreateShippingRecord(shipping, shippingCost);

                    if (shipId == -1)
                    {
                        MessageBox.Show("Failed to create shipping record.");
                        return -1;
                    }

                    string query = "INSERT INTO Orders (Order_ID, salesman_Status, Quantity, Cost, Ship_ID, Employee_ID, Cus_ID, Order_Date) VALUES (@Order_ID, @salesman_Status, @Quantity, @Cost, @Ship_ID, @Employee_ID, @Cus_ID, @Order_Date)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Order_ID", newOrderId);
                        cmd.Parameters.AddWithValue("@salesman_Status", status);
                        cmd.Parameters.AddWithValue("@Quantity", quantity);
                        cmd.Parameters.AddWithValue("@Cost", cost);
                        cmd.Parameters.AddWithValue("@Ship_ID", shipId);
                        cmd.Parameters.AddWithValue("@Employee_ID", employeeId == 0 ? (object)DBNull.Value : employeeId);
                        cmd.Parameters.AddWithValue("@Cus_ID", cusId == 0 ? (object)DBNull.Value : cusId);
                        cmd.Parameters.AddWithValue("@Order_Date", DateTime.Now.ToString("yyyy-MM-dd"));

                        int result = cmd.ExecuteNonQuery();
                        if (result > 0)
                        {
                            return newOrderId;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error inserting order: " + ex.Message);
                }
            }
            return -1;
        }

        /// <summary>
        /// Inserts a product into an order.
        /// </summary>
        /// <param name="orderId">Order ID.</param>
        /// <param name="productId">Product ID.</param>
        /// <returns>True if successful, false otherwise.</returns>
        public bool InsertOrderProduct(int orderId, int productId)
        {
            string connStr = ConfigurationManager.ConnectionStrings["ASMDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    string query = "INSERT INTO Order_Products (Order_ID, Prod_ID) VALUES (@Order_ID, @Prod_ID)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Order_ID", orderId);
                        cmd.Parameters.AddWithValue("@Prod_ID", productId);
                        int result = cmd.ExecuteNonQuery();
                        return result > 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error inserting order product: " + ex.Message);
                }
            }
            return false;
        }

        /// <summary>
        /// Inserts an order with multiple products.
        /// </summary>
        /// <param name="status">Salesman status.</param>
        /// <param name="shipping">Shipping type.</param>
        /// <param name="orderItems">List of order items.</param>
        /// <param name="cusId">Customer ID.</param>
        /// <param name="employeeId">Employee ID.</param>
        /// <returns>True if successful, false otherwise.</returns>
        public bool InsertOrderWithProducts(string status, string shipping, List<OrderItem> orderItems, int cusId, int employeeId)
        {
            try
            {
                if (orderItems == null || orderItems.Count == 0)
                    return false;
                // For each product, insert a separate order (since Orders table is 1 product per row)
                foreach (var item in orderItems)
                {
                    int orderId = InsertOrder(status, shipping, cusId, employeeId, item.Quantity, item.Price);
                    if (orderId == -1)
                    {
                        return false;
                    }
                    bool success = InsertOrderProduct(orderId, item.Prod_ID);
                    if (!success)
                    {
                        MessageBox.Show($"Failed to insert product {item.Prod_Name} into order.");
                        return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error creating order with products: " + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Retrieves a list of past orders.
        /// </summary>
        /// <returns>A list of past orders.</returns>
        public List<Orders> GetPastOrders()
        {
            List<Orders> pastOrders = new List<Orders>();
            string connStr = ConfigurationManager.ConnectionStrings["ASMDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT Order_ID, salesman_Status, Ship_ID, Employee_ID, Cus_ID, Is_Done, Quantity, Cost, Order_Date FROM Orders WHERE Is_Done = 1";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Orders order = new Orders
                                {
                                    Order_ID = reader.GetInt32(0),
                                    salesman_Status = reader.IsDBNull(1) ? null : reader.GetString(1),
                                    Ship_ID = reader.IsDBNull(2) ? 0 : reader.GetInt32(2),
                                    Employee_ID = reader.IsDBNull(3) ? 0 : reader.GetInt32(3),
                                    Cus_ID = reader.IsDBNull(4) ? 0 : reader.GetInt32(4),
                                    Is_Done = reader.GetBoolean(5),
                                    Quantity = reader.IsDBNull(6) ? 1 : reader.GetInt32(6),
                                    Cost = reader.IsDBNull(7) ? 0 : reader.GetDecimal(7),
                                    Order_Date = reader.IsDBNull(8) ? "" : reader.GetDateTime(8).ToString("yyyy-MM-dd")
                                };
                                pastOrders.Add(order);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error retrieving past orders: " + ex.Message);
                }
            }
            return pastOrders;
        }

        /// <summary>
        /// Retrieves a list of orders by customer ID.
        /// </summary>
        /// <param name="cusId">Customer ID.</param>
        /// <returns>A list of orders for the specified customer.</returns>
        public List<Orders> GetOrdersByCustomerId(int cusId)
        {
            List<Orders> orders = new List<Orders>();
            string connStr = ConfigurationManager.ConnectionStrings["ASMDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT Order_ID, salesman_Status, Ship_ID, Employee_ID, Cus_ID, Is_Done, Quantity, Cost, Order_Date FROM Orders WHERE Cus_ID = @Cus_ID";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Cus_ID", cusId);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Orders order = new Orders
                                {
                                    Order_ID = reader.GetInt32(0),
                                    salesman_Status = reader.IsDBNull(1) ? null : reader.GetString(1),
                                    Ship_ID = reader.IsDBNull(2) ? 0 : reader.GetInt32(2),
                                    Employee_ID = reader.IsDBNull(3) ? 0 : reader.GetInt32(3),
                                    Cus_ID = reader.IsDBNull(4) ? 0 : reader.GetInt32(4),
                                    Is_Done = reader.GetBoolean(5),
                                    Quantity = reader.IsDBNull(6) ? 1 : reader.GetInt32(6),
                                    Cost = reader.IsDBNull(7) ? 0 : reader.GetDecimal(7),
                                    Order_Date = reader.IsDBNull(8) ? "" : reader.GetDateTime(8).ToString("yyyy-MM-dd")
                                };
                                orders.Add(order);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error retrieving orders: " + ex.Message);
                }
            }
            return orders;
        }

        /// <summary>
        /// Retrieves a list of past orders by customer ID.
        /// </summary>
        /// <param name="cusId">Customer ID.</param>
        /// <returns>A list of past orders for the specified customer.</returns>
        public List<PastOrderView> GetPastOrdersByCustomerId(int cusId)
        {
            List<PastOrderView> pastOrders = new List<PastOrderView>();
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["ASMDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    string query = @"SELECT o.Order_ID, p.Prod_Name, o.Cost, s.Shipping_Cost, o.Order_Date
                                     FROM Orders o
                                     JOIN Order_Products op ON o.Order_ID = op.Order_ID
                                     JOIN Products p ON op.Prod_ID = p.Prod_ID
                                     JOIN Shipping s ON o.Ship_ID = s.Ship_ID
                                     WHERE o.Cus_ID = @Cus_ID AND o.Is_Done = 1";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Cus_ID", cusId);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                PastOrderView order = new PastOrderView
                                {
                                    Order_ID = reader.GetInt32(0),
                                    ProductName = reader.GetString(1),
                                    Cost = reader.GetDecimal(2),
                                    ShippingFee = reader.GetDecimal(3),
                                    OrderDate = reader.IsDBNull(4) ? "" : reader.GetDateTime(4).ToString("yyyy-MM-dd")
                                };
                                pastOrders.Add(order);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error retrieving past orders: " + ex.Message);
                }
            }
            return pastOrders;
        }

        /// <summary>
        /// Retrieves a list of pending orders by customer ID.
        /// </summary>
        /// <param name="cusId">Customer ID.</param>
        /// <returns>A list of pending orders for the specified customer.</returns>
        public List<Orders> GetPendingOrdersByCustomerId(int cusId)
        {
            List<Orders> orders = new List<Orders>();
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["ASMDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT Order_ID, salesman_Status, Ship_ID, Employee_ID, Cus_ID, Is_Done, Quantity, Cost, Order_Date FROM Orders WHERE Cus_ID = @Cus_ID AND Is_Done = 0";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Cus_ID", cusId);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Orders order = new Orders
                                {
                                    Order_ID = reader.GetInt32(0),
                                    salesman_Status = reader.IsDBNull(1) ? null : reader.GetString(1),
                                    Ship_ID = reader.IsDBNull(2) ? 0 : reader.GetInt32(2),
                                    Employee_ID = reader.IsDBNull(3) ? 0 : reader.GetInt32(3),
                                    Cus_ID = reader.IsDBNull(4) ? 0 : reader.GetInt32(4),
                                    Is_Done = reader.GetBoolean(5),
                                    Quantity = reader.IsDBNull(6) ? 1 : reader.GetInt32(6),
                                    Cost = reader.IsDBNull(7) ? 0 : reader.GetDecimal(7),
                                    Order_Date = reader.IsDBNull(8) ? "" : reader.GetDateTime(8).ToString("yyyy-MM-dd")
                                };
                                orders.Add(order);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error retrieving pending orders: " + ex.Message);
                }
            }
            return orders;
        }

        /// <summary>
        /// Updates the status and employee ID of an order.
        /// </summary>
        /// <param name="orderId">Order ID.</param>
        /// <param name="salesmanStatus">New salesman status.</param>
        /// <param name="employeeId">Employee ID.</param>
        /// <returns>True if successful, false otherwise.</returns>
        public bool UpdateOrderStatusAndEmployee(int orderId, string salesmanStatus, int employeeId)
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["ASMDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    string query = "UPDATE Orders SET salesman_Status = @salesmanStatus, Employee_ID = @employeeId WHERE Order_ID = @orderId";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@salesmanStatus", salesmanStatus);
                        cmd.Parameters.AddWithValue("@employeeId", employeeId);
                        cmd.Parameters.AddWithValue("@orderId", orderId);
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error updating order status: " + ex.Message);
                }
            }
            return false;
        }

        /// <summary>
        /// Retrieves a list of pending orders for acceptance.
        /// </summary>
        /// <returns>A list of pending orders for acceptance.</returns>
        public List<Orders> GetPendingOrdersForAcceptance()
        {
            List<Orders> orders = new List<Orders>();
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["ASMDB"]. ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT Order_ID, salesman_Status, Ship_ID, Employee_ID, Cus_ID, Is_Done, Quantity, Cost, Order_Date FROM Orders WHERE (Employee_ID IS NULL OR Employee_ID = 0) AND (salesman_Status IS NULL OR salesman_Status <> 'Item Prepared') AND Is_Done = 0";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Orders order = new Orders
                                {
                                    Order_ID = reader.GetInt32(0),
                                    salesman_Status = reader.IsDBNull(1) ? null : reader.GetString(1),
                                    Ship_ID = reader.IsDBNull(2) ? 0 : reader.GetInt32(2),
                                    Employee_ID = reader.IsDBNull(3) ? 0 : reader.GetInt32(3),
                                    Cus_ID = reader.IsDBNull(4) ? 0 : reader.GetInt32(4),
                                    Is_Done = reader.GetBoolean(5),
                                    Quantity = reader.IsDBNull(6) ? 1 : reader.GetInt32(6),
                                    Cost = reader.IsDBNull(7) ? 0 : reader.GetDecimal(7),
                                    Order_Date = reader.IsDBNull(8) ? "" : reader.GetDateTime(8).ToString("yyyy-MM-dd")
                                };
                                orders.Add(order);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error retrieving pending orders for acceptance: " + ex.Message);
                }
            }
            return orders;
        }

        /// <summary>
        /// Retrieves a list of order products for a given order ID.
        /// </summary>
        /// <param name="orderId">Order ID.</param>
        /// <returns>A list of order products for the specified order.</returns>
        public List<OrderProducts> GetOrderProducts(int orderId)
        {
            List<OrderProducts> orderProducts = new List<OrderProducts>();
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["ASMDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT Order_ID, Prod_ID FROM Order_Products WHERE Order_ID = @Order_ID";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Order_ID", orderId);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                orderProducts.Add(new OrderProducts
                                {
                                    Order_ID = reader.GetInt32(0),
                                    Prod_ID = reader.GetInt32(1)
                                });
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error fetching order products: " + ex.Message);
                }
            }
            return orderProducts;
        }

        /// <summary>
        /// Retrieves an order by its ID.
        /// </summary>
        /// <param name="orderId">Order ID.</param>
        /// <returns>The order with the specified ID, or null if not found.</returns>
        public Orders GetOrderById(int orderId)
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["ASMDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT Order_ID, salesman_Status, Ship_ID, Employee_ID, Cus_ID, Is_Done, Quantity, Cost, Order_Date FROM Orders WHERE Order_ID = @Order_ID";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Order_ID", orderId);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new Orders
                                {
                                    Order_ID = reader.GetInt32(0),
                                    salesman_Status = reader.IsDBNull(1) ? null : reader.GetString(1),
                                    Ship_ID = reader.IsDBNull(2) ? 0 : reader.GetInt32(2),
                                    Employee_ID = reader.IsDBNull(3) ? 0 : reader.GetInt32(3),
                                    Cus_ID = reader.IsDBNull(4) ? 0 : reader.GetInt32(4),
                                    Is_Done = reader.GetBoolean(5),
                                    Quantity = reader.IsDBNull(6) ? 1 : reader.GetInt32(6),
                                    Cost = reader.IsDBNull(7) ? 0 : reader.GetDecimal(7),
                                    Order_Date = reader.IsDBNull(8) ? "" : reader.GetDateTime(8).ToString("yyyy-MM-dd")
                                };
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error fetching order by ID: " + ex.Message);
                }
            }
            return null;
        }

        /// <summary>
        /// Marks an order as done.
        /// </summary>
        /// <param name="orderId">Order ID.</param>
        /// <returns>True if successful, false otherwise.</returns>
        public bool MarkOrderAsDone(int orderId)
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["ASMDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    string query = "UPDATE Orders SET Is_Done = 1 WHERE Order_ID = @Order_ID";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Order_ID", orderId);
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error marking order as done: " + ex.Message);
                }
            }
            return false;
        }

        /// <summary>
        /// Retrieves the shipping and address information for an order.
        /// </summary>
        /// <param name="orderId">Order ID.</param>
        /// <returns>A tuple containing the current location, customer address, shipping status, and ship ID.</returns>
        public (string CurrentLocation, string CustomerAddress, string ShippingStatus, int ShipId) GetOrderShippingAndAddress(int orderId)
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["ASMDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    string query = @"
                        SELECT s.Current_Location, c.Address, s.Shipping_Status, s.Ship_ID
                        FROM Orders o
                        JOIN Shipping s ON o.Ship_ID = s.Ship_ID
                        JOIN Customer c ON o.Cus_ID = c.Cus_ID
                        WHERE o.Order_ID = @Order_ID";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Order_ID", orderId);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return (
                                    reader.GetString(0),
                                    reader.GetString(1),
                                    reader.GetString(2),
                                    reader.GetInt32(3)
                                );
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error fetching order shipping info: " + ex.Message);
                }
            }
            return (null, null, null, 0);
        }
    }
}