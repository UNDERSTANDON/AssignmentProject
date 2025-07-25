using ASMDB.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ASMDB.Connection
{
    /// <summary>
    /// Data access layer for customer queries and management.
    /// </summary>
    public class DAL_Customers
    {
        /// <summary>
        /// Gets all customers from the database.
        /// </summary>
        /// <returns>List of Customers objects.</returns>
        public List<Customers> GetAllCustomers()
        {
            List<Customers> customers = new List<Customers>();
            string connStr = ConfigurationManager.ConnectionStrings["ASMDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT Cus_ID, Cus_Name, Phone, Address, Email FROM Customer";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                customers.Add(new Customers
                                {
                                    Cus_ID = reader.GetInt32(0),
                                    Cus_Name = reader.GetString(1),
                                    Phone = reader.GetString(2),
                                    Address = reader.GetString(3),
                                    Email = reader.GetString(4)
                                });
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error fetching customers: " + ex.Message);
                }
            }
            return customers;
        }

        /// <summary>
        /// Searches customers by term and type (ID or Name).
        /// </summary>
        /// <param name="term">Search term (ID or Name).</param>
        /// <param name="type">Type of search (ID or Name).</param>
        /// <returns>List of Customers objects matching the search.</returns>
        public List<Customers> SearchCustomers(string term, string type)
        {
            List<Customers> customers = new List<Customers>();
            string connStr = ConfigurationManager.ConnectionStrings["ASMDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    string query = type == "ID"
                        ? "SELECT Cus_ID, Cus_Name, Phone, Address, Email FROM Customer WHERE Cus_ID = @term"
                        : "SELECT Cus_ID, Cus_Name, Phone, Address, Email FROM Customer WHERE Cus_Name LIKE @term";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        if (type == "ID")
                            cmd.Parameters.AddWithValue("@term", int.TryParse(term, out int id) ? id : -1);
                        else
                            cmd.Parameters.AddWithValue("@term", "%" + term + "%");
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                customers.Add(new Customers
                                {
                                    Cus_ID = reader.GetInt32(0),
                                    Cus_Name = reader.GetString(1),
                                    Phone = reader.GetString(2),
                                    Address = reader.GetString(3),
                                    Email = reader.GetString(4)
                                });
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error searching customers: " + ex.Message);
                }
            }
            return customers;
        }

        /// <summary>
        /// Adds a new customer to the database.
        /// </summary>
        /// <param name="name">Customer name.</param>
        /// <param name="phone">Customer phone number.</param>
        /// <param name="address">Customer address.</param>
        /// <param name="email">Customer email.</param>
        /// <returns>True if successful, false otherwise.</returns>
        public bool AddCustomer(string name, int phone, string address, string email)
        {
            string connStr = ConfigurationManager.ConnectionStrings["ASMDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    int newCusId = 1;
                    string getMaxIdQuery = "SELECT ISNULL(MAX(Cus_ID), 100) + 1 FROM Customer";
                    using (SqlCommand getMaxIdCmd = new SqlCommand(getMaxIdQuery, conn))
                    {
                        newCusId = (int)getMaxIdCmd.ExecuteScalar();
                    }
                    string query = "INSERT INTO Customer (Cus_ID, Cus_Name, Phone, Address, Email) VALUES (@Cus_ID, @Cus_Name, @Phone, @Address, @Email)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Cus_ID", newCusId);
                        cmd.Parameters.AddWithValue("@Cus_Name", name);
                        cmd.Parameters.AddWithValue("@Phone", phone);
                        cmd.Parameters.AddWithValue("@Address", address);
                        cmd.Parameters.AddWithValue("@Email", email);
                        int rows = cmd.ExecuteNonQuery();
                        return rows > 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error adding customer: " + ex.Message);
                    return false;
                }
            }
        }

        /// <summary>
        /// Updates an existing customer in the database.
        /// </summary>
        /// <param name="cusId">Customer ID.</param>
        /// <param name="name">Customer name.</param>
        /// <param name="phone">Customer phone number.</param>
        /// <param name="address">Customer address.</param>
        /// <param name="email">Customer email.</param>
        /// <param name="imagePath">Path to customer profile image (optional).</param>
        /// <returns>True if successful, false otherwise.</returns>
        public bool UpdateCustomer(int cusId, string name, int phone, string address, string email, string imagePath = null)
        {
            string connStr = ConfigurationManager.ConnectionStrings["ASMDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    string query = "UPDATE Customer SET Cus_Name = @Cus_Name, Phone = @Phone, Address = @Address, Email = @Email WHERE Cus_ID = @Cus_ID";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Cus_ID", cusId);
                        cmd.Parameters.AddWithValue("@Cus_Name", name);
                        cmd.Parameters.AddWithValue("@Phone", phone);
                        cmd.Parameters.AddWithValue("@Address", address);
                        cmd.Parameters.AddWithValue("@Email", email);
                        int rows = cmd.ExecuteNonQuery();
                        if (rows > 0 && !string.IsNullOrEmpty(imagePath))
                        {
                            SetCustomerProfileImage(conn, cusId, imagePath);
                        }
                        return rows > 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error updating customer: " + ex.Message);
                    return false;
                }
            }
        }

        private void SetCustomerProfileImage(SqlConnection conn, int cusId, string imagePath)
        {
            bool fileExists = System.IO.File.Exists(imagePath);
            int isExisted = fileExists ? 1 : 0;
            string pathToStore = fileExists ? imagePath.Replace("\\", "/") : null;
            // Check if entry exists
            string checkQuery = "SELECT COUNT(*) FROM CustomersImage WHERE Cus_ID = @Cus_ID";
            using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
            {
                checkCmd.Parameters.AddWithValue("@Cus_ID", cusId);
                int count = (int)checkCmd.ExecuteScalar();
                if (count > 0)
                {
                    // Update
                    string updateQuery = "UPDATE CustomersImage SET Image_Path = @Image_Path, Is_Existed = @Is_Existed WHERE Cus_ID = @Cus_ID";
                    using (SqlCommand updateCmd = new SqlCommand(updateQuery, conn))
                    {
                        updateCmd.Parameters.AddWithValue("@Image_Path", (object)pathToStore ?? System.DBNull.Value);
                        updateCmd.Parameters.AddWithValue("@Is_Existed", isExisted);
                        updateCmd.Parameters.AddWithValue("@Cus_ID", cusId);
                        updateCmd.ExecuteNonQuery();
                    }
                }
                else
                {
                    // Insert
                    string insertQuery = "INSERT INTO CustomersImage (Image_ID, Image_Path, Is_Existed, Cus_ID) VALUES (@Image_ID, @Image_Path, @Is_Existed, @Cus_ID)";
                    int newImageId = 1;
                    string getMaxIdQuery = "SELECT ISNULL(MAX(Image_ID), 0) + 1 FROM CustomersImage";
                    using (SqlCommand getMaxIdCmd = new SqlCommand(getMaxIdQuery, conn))
                    {
                        newImageId = (int)getMaxIdCmd.ExecuteScalar();
                    }
                    using (SqlCommand insertCmd = new SqlCommand(insertQuery, conn))
                    {
                        insertCmd.Parameters.AddWithValue("@Image_ID", newImageId);
                        insertCmd.Parameters.AddWithValue("@Image_Path", (object)pathToStore ?? System.DBNull.Value);
                        insertCmd.Parameters.AddWithValue("@Is_Existed", isExisted);
                        insertCmd.Parameters.AddWithValue("@Cus_ID", cusId);
                        insertCmd.ExecuteNonQuery();
                    }
                }
            }
        }

        /// <summary>
        /// Deletes a customer and related data from the database.
        /// </summary>
        /// <param name="cusId">Customer ID.</param>
        /// <returns>True if successful, false otherwise.</returns>
        public bool DeleteCustomer(int cusId)
        {
            string connStr = ConfigurationManager.ConnectionStrings["ASMDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    using (SqlTransaction transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            // Delete from Order_Products first (FK constraint)
                            string deleteOrderProductsQuery = "DELETE FROM Order_Products WHERE Order_ID IN (SELECT Order_ID FROM Orders WHERE Cus_ID = @Cus_ID)";
                            using (SqlCommand cmd = new SqlCommand(deleteOrderProductsQuery, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@Cus_ID", cusId);
                                cmd.ExecuteNonQuery();
                            }

                            // Delete from Orders (FK constraint)
                            string deleteOrdersQuery = "DELETE FROM Orders WHERE Cus_ID = @Cus_ID";
                            using (SqlCommand cmd = new SqlCommand(deleteOrdersQuery, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@Cus_ID", cusId);
                                cmd.ExecuteNonQuery();
                            }

                            // Delete from CustomersImage (FK constraint)
                            string deleteImageQuery = "DELETE FROM CustomersImage WHERE Cus_ID = @Cus_ID";
                            using (SqlCommand cmd = new SqlCommand(deleteImageQuery, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@Cus_ID", cusId);
                                cmd.ExecuteNonQuery();
                            }

                            // Delete from UserAccount (FK constraint)
                            string deleteUserAccountQuery = "DELETE FROM UserAccount WHERE Cus_ID = @Cus_ID";
                            using (SqlCommand cmd = new SqlCommand(deleteUserAccountQuery, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@Cus_ID", cusId);
                                cmd.ExecuteNonQuery();
                            }

                            // Finally delete from Customer
                            string deleteCustomerQuery = "DELETE FROM Customer WHERE Cus_ID = @Cus_ID";
                            using (SqlCommand cmd = new SqlCommand(deleteCustomerQuery, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@Cus_ID", cusId);
                                int rows = cmd.ExecuteNonQuery();
                                if (rows > 0)
                                {
                                    transaction.Commit();
                                    return true;
                                }
                                else
                                {
                                    transaction.Rollback();
                                    return false;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            throw ex;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deleting customer: " + ex.Message);
                    return false;
                }
            }
        }

        /// <summary>
        /// Gets a customer by ID.
        /// </summary>
        /// <param name="cusId">Customer ID.</param>
        /// <returns>Customers object if found, null otherwise.</returns>
        public Customers GetCustomerById(int cusId)
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["ASMDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT Cus_ID, Cus_Name, Phone, Address, Email FROM Customer WHERE Cus_ID = @Cus_ID";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Cus_ID", cusId);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new Customers
                                {
                                    Cus_ID = reader.GetInt32(0),
                                    Cus_Name = reader.GetString(1),
                                    Phone = reader.GetString(2),
                                    Address = reader.GetString(3),
                                    Email = reader.GetString(4)
                                };
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error fetching customer by ID: " + ex.Message);
                }
            }
            return null;
        }
    }
}