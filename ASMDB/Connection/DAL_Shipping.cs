using ASMDB.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ASMDB.Connection
{
    /// <summary>
    /// Data access layer for shipping information and records.
    /// </summary>
    public class DAL_Shipping
    {
        /// <summary>
        /// Gets shipping information for a given shipping ID.
        /// </summary>
        /// <param name="shipId">The shipping ID.</param>
        /// <returns>A Shipping object with shipping details, or null if not found.</returns>
        public Shipping GetShippingInfo(int shipId)
        {
            string connStr = ConfigurationManager.ConnectionStrings["ASMDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT Ship_ID, Estimate_Date, Ship_Type, Shipping_Cost, Current_Location, Shipping_Status FROM Shipping WHERE Ship_ID = @Ship_ID";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Ship_ID", shipId);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new Shipping
                                {
                                    Ship_ID = reader.GetInt32(0),
                                    Estimate_Date = reader.GetDateTime(1),
                                    Ship_Type = reader.GetString(2),
                                    Shipping_Cost = reader.GetDecimal(3),
                                    Current_Location = reader.GetString(4),
                                    Shipping_Status = reader.GetString(5)
                                };
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error fetching shipping info: " + ex.Message);
                }
            }
            return null;
        }

        /// <summary>
        /// Creates a new shipping record with the specified type and cost.
        /// </summary>
        /// <param name="shipType">Type of shipping (e.g., Express, Standard).</param>
        /// <param name="shippingCost">Cost of shipping.</param>
        /// <returns>The new shipping ID, or -1 if creation failed.</returns>
        public int CreateShippingRecord(string shipType, decimal shippingCost)
        {
            string connStr = ConfigurationManager.ConnectionStrings["ASMDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    // Find max Ship_ID and increment by 1
                    int newShipId = 1;
                    string getMaxIdQuery = "SELECT ISNULL(MAX(Ship_ID), 100) + 1 FROM Shipping";
                    using (SqlCommand getMaxIdCmd = new SqlCommand(getMaxIdQuery, conn))
                    {
                        newShipId = (int)getMaxIdCmd.ExecuteScalar();
                    }

                    // Calculate estimate date based on ship type
                    DateTime estimateDate = DateTime.Now.AddDays(shipType == "Express" ? 2 : 5);

                    string query = "INSERT INTO Shipping (Ship_ID, Estimate_Date, Ship_Type, Shipping_Cost, Current_Location, Shipping_Status) VALUES (@Ship_ID, @Estimate_Date, @Ship_Type, @Shipping_Cost, @Current_Location, @Shipping_Status)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Ship_ID", newShipId);
                        cmd.Parameters.AddWithValue("@Estimate_Date", estimateDate);
                        cmd.Parameters.AddWithValue("@Ship_Type", shipType);
                        cmd.Parameters.AddWithValue("@Shipping_Cost", shippingCost);
                        cmd.Parameters.AddWithValue("@Current_Location", "Warehouse");
                        cmd.Parameters.AddWithValue("@Shipping_Status", "Processing");

                        int result = cmd.ExecuteNonQuery();
                        if (result > 0)
                        {
                            return newShipId;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error creating shipping record: " + ex.Message);
                }
            }
            return -1;
        }

        /// <summary>
        /// Gets predefined shipping rates for Standard and Express delivery.
        /// </summary>
        /// <returns>A dictionary of shipping rate information.</returns>
        public Dictionary<string, object> GetShippingRates()
        {
            var rates = new Dictionary<string, object>();
            rates["Standard"] = new { Cost = 5.99m, Days = 5, Description = "Standard delivery within 5 business days" };
            rates["Express"] = new { Cost = 12.99m, Days = 2, Description = "Express delivery within 2 business days" };
            return rates;
        }

        /// <summary>
        /// Gets a list of shipping records for a specific customer.
        /// </summary>
        /// <param name="customerId">The customer ID.</param>
        /// <returns>A list of Shipping objects associated with the customer.</returns>
        public List<Shipping> GetCustomerOrders(int customerId)
        {
            List<Shipping> shipments = new List<Shipping>();
            string connStr = ConfigurationManager.ConnectionStrings["ASMDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    string query = @"SELECT s.Ship_ID, s.Estimate_Date, s.Ship_Type, s.Shipping_Cost, s.Current_Location, s.Shipping_Status 
                                   FROM Shipping s 
                                   JOIN Orders o ON s.Ship_ID = o.Ship_ID 
                                   WHERE o.Cus_ID = @Cus_ID";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Cus_ID", customerId);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                shipments.Add(new Shipping
                                {
                                    Ship_ID = reader.GetInt32(0),
                                    Estimate_Date = reader.GetDateTime(1),
                                    Ship_Type = reader.GetString(2),
                                    Shipping_Cost = reader.GetDecimal(3),
                                    Current_Location = reader.GetString(4),
                                    Shipping_Status = reader.GetString(5)
                                });
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error fetching customer orders: " + ex.Message);
                }
            }
            return shipments;
        }

        /// <summary>
        /// Gets a list of completed shipments (where status is 'Delivered').
        /// </summary>
        /// <returns>A list of completed Shipping objects.</returns>
        public List<Shipping> GetCompletedShipments()
        {
            List<Shipping> completedShipments = new List<Shipping>();
            string connStr = ConfigurationManager.ConnectionStrings["ASMDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT Ship_ID, Estimate_Date, Ship_Type, Shipping_Cost, Current_Location, Shipping_Status FROM Shipping WHERE Shipping_Status = 'Delivered'";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                completedShipments.Add(new Shipping
                                {
                                    Ship_ID = reader.GetInt32(0),
                                    Estimate_Date = reader.GetDateTime(1),
                                    Ship_Type = reader.GetString(2),
                                    Shipping_Cost = reader.GetDecimal(3),
                                    Current_Location = reader.GetString(4),
                                    Shipping_Status = reader.GetString(5)
                                });
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error retrieving completed shipments: " + ex.Message);
                }
            }
            return completedShipments;
        }

        /// <summary>
        /// Gets a list of pending shipments (where Is_Done = 0).
        /// </summary>
        /// <returns>A list of pending Shipping objects.</returns>
        public List<Shipping> GetPendingShippingInfo()
        {
            List<Shipping> shipments = new List<Shipping>();
            string connStr = ConfigurationManager.ConnectionStrings["ASMDB"]. ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    string query = @"SELECT s.Ship_ID, s.Estimate_Date, s.Ship_Type, s.Shipping_Cost, s.Current_Location, s.Shipping_Status
                                     FROM Shipping s
                                     JOIN Orders o ON s.Ship_ID = o.Ship_ID
                                     WHERE o.Is_Done = 0";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                shipments.Add(new Shipping
                                {
                                    Ship_ID = reader.GetInt32(0),
                                    Estimate_Date = reader.GetDateTime(1),
                                    Ship_Type = reader.GetString(2),
                                    Shipping_Cost = reader.GetDecimal(3),
                                    Current_Location = reader.GetString(4),
                                    Shipping_Status = reader.GetString(5)
                                });
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error fetching pending shipping info: " + ex.Message);
                }
            }
            return shipments;
        }

        /// <summary>
        /// Gets a list of pending shipments for a specific customer (where Is_Done = 0).
        /// </summary>
        /// <param name="cusId">The customer ID.</param>
        /// <returns>A list of pending Shipping objects for the customer.</returns>
        public List<Shipping> GetPendingShippingInfoByCustomer(int cusId)
        {
            List<Shipping> shipments = new List<Shipping>();
            string connStr = ConfigurationManager.ConnectionStrings["ASMDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    string query = @"SELECT s.Ship_ID, s.Estimate_Date, s.Ship_Type, s.Shipping_Cost, s.Current_Location, s.Shipping_Status
                                     FROM Shipping s
                                     JOIN Orders o ON s.Ship_ID = o.Ship_ID
                                     WHERE o.Is_Done = 0 AND o.Cus_ID = @Cus_ID";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Cus_ID", cusId);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                shipments.Add(new Shipping
                                {
                                    Ship_ID = reader.GetInt32(0),
                                    Estimate_Date = reader.GetDateTime(1),
                                    Ship_Type = reader.GetString(2),
                                    Shipping_Cost = reader.GetDecimal(3),
                                    Current_Location = reader.GetString(4),
                                    Shipping_Status = reader.GetString(5)
                                });
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error fetching pending shipping info for customer: " + ex.Message);
                }
            }
            return shipments;
        }

        /// <summary>
        /// Updates the shipping status and location for a given shipping ID.
        /// </summary>
        /// <param name="shipId">The shipping ID.</param>
        /// <param name="shippingStatus">The new shipping status.</param>
        /// <param name="location">The new current location.</param>
        /// <returns>True if the update was successful, otherwise false.</returns>
        public bool UpdateShippingStatusAndLocation(int shipId, string shippingStatus, string location)
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["ASMDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    string query = "UPDATE Shipping SET Shipping_Status = @shippingStatus, Current_Location = @location WHERE Ship_ID = @shipId";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@shippingStatus", shippingStatus);
                        cmd.Parameters.AddWithValue("@location", location);
                        cmd.Parameters.AddWithValue("@shipId", shipId);
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error updating shipping status: " + ex.Message);
                }
            }
            return false;
        }

        /// <summary>
        /// Gets a shipping record by its ID (alias for GetShippingInfo).
        /// </summary>
        /// <param name="shipId">The shipping ID.</param>
        /// <returns>A Shipping object with shipping details, or null if not found.</returns>
        public Shipping GetShippingById(int shipId)
        {
            return GetShippingInfo(shipId);
        }
    }
}