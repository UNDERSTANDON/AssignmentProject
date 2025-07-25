using ASMDB.Models;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace ASMDB.Connection
{
    /// <summary>
    /// Data access layer for login and user account management.
    /// </summary>
    public class DAL_Login
    {
        // Register
        public string ConnectionString = "Data Source=LAPTOP-IMAVTAVU;Initial Catalog=ASMDB;Integrated Security=True";

        /// <summary>
        /// Tests the database connection.
        /// </summary>
        /// <returns>True if connection is successful, false otherwise.</returns>
        public bool TestConnection()
        {
            string connStr = ConfigurationManager.ConnectionStrings["ASMDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Connection failed: " + ex.Message);
                    return false;
                }
            }
        }

        /// <summary>
        /// Inserts a new user account into the database.
        /// </summary>
        /// <param name="username">Username for the account.</param>
        /// <param name="passwordHash">Hashed password.</param>
        /// <returns>True if successful, false otherwise.</returns>
        public bool InsertUserAccount(string username, string passwordHash)
        {
            string connStr = ConfigurationManager.ConnectionStrings["ASMDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    string query = "INSERT INTO UserAccount (Username, PasswordHash, CreatedAt, IsActive) VALUES (@Username, @PasswordHash, @CreatedAt, @IsActive)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Username", username);
                        cmd.Parameters.AddWithValue("@PasswordHash", passwordHash);
                        cmd.Parameters.AddWithValue("@CreatedAt", DateTime.Now);
                        cmd.Parameters.AddWithValue("@IsActive", true);
                        int rows = cmd.ExecuteNonQuery();
                        return rows > 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error inserting user: " + ex.Message);
                    return false;
                }
            }
        }

        /// <summary>
        /// Inserts a new customer into the database.
        /// </summary>
        /// <param name="name">Customer name.</param>
        /// <param name="phone">Customer phone number.</param>
        /// <param name="address">Customer address.</param>
        /// <param name="email">Customer email.</param>
        /// <returns>The new customer ID, or -1 if insertion failed.</returns>
        public int InsertCustomer(string name, int phone, string address, string email)
        {
            string connStr = ConfigurationManager.ConnectionStrings["ASMDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    // Find max Cus_ID and increment by 1
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
                        if (rows > 0)
                            return newCusId;
                        else
                            return -1;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error inserting customer: " + ex.Message);
                    return -1;
                }
            }
        }

        /// <summary>
        /// Inserts a user account and links it to a customer.
        /// </summary>
        /// <param name="username">Username for the account.</param>
        /// <param name="passwordHash">Hashed password.</param>
        /// <param name="cusId">Customer ID to link.</param>
        /// <returns>True if successful, false otherwise.</returns>
        public bool InsertUserAccountWithCustomer(string username, string passwordHash, int cusId)
        {
            string connStr = ConfigurationManager.ConnectionStrings["ASMDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    string query = "INSERT INTO UserAccount (Username, PasswordHash, Cus_ID, CreatedAt, IsActive) VALUES (@Username, @PasswordHash, @Cus_ID, @CreatedAt, @IsActive)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Username", username);
                        cmd.Parameters.AddWithValue("@PasswordHash", passwordHash);
                        cmd.Parameters.AddWithValue("@Cus_ID", cusId);
                        cmd.Parameters.AddWithValue("@CreatedAt", DateTime.Now);
                        cmd.Parameters.AddWithValue("@IsActive", true);
                        int rows = cmd.ExecuteNonQuery();
                        return rows > 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error inserting user: " + ex.Message);
                    return false;
                }
            }
        }

        // Login
        public UserAccounts GetUserAccount(string username, string passwordHash)
        {
            string connStr = ConfigurationManager.ConnectionStrings["ASMDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT UserAccount_ID, Username, PasswordHash, Employee_ID, Cus_ID, CreatedAt, IsActive FROM UserAccount WHERE Username = @Username AND PasswordHash = @PasswordHash";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Username", username);
                        cmd.Parameters.AddWithValue("@PasswordHash", passwordHash);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new UserAccounts
                                {
                                    UserAccount_ID = reader.GetInt32(0),
                                    Username = reader.GetString(1),
                                    PasswordHash = reader.GetString(2),
                                    Employee_ID = reader.IsDBNull(3) ? (int?)null : reader.GetInt32(3),
                                    Cus_ID = reader.IsDBNull(4) ? (int?)null : reader.GetInt32(4),
                                    CreatedAt = reader.GetDateTime(5),
                                    IsActive = reader.GetBoolean(6)
                                };
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error fetching user: " + ex.Message);
                }
            }
            return null;
        }

        public Employees GetEmployeeById(int employeeId)
        {
            string connStr = ConfigurationManager.ConnectionStrings["ASMDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT Employee_ID, Employee_Name, Position, Role_ID FROM Employee WHERE Employee_ID = @Employee_ID";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Employee_ID", employeeId);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new Employees
                                {
                                    Employee_ID = reader.GetInt32(0),
                                    Employee_Name = reader.GetString(1),
                                    Position = reader.GetString(2),
                                    Role_ID = reader.GetInt32(3)
                                };
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error fetching employee: " + ex.Message);
                }
            }
            return null;
        }

        // Reset Password
        public bool UpdateEmployeePasswordAndActivate(int employeeId, string passwordHash)
        {
            string connStr = ConfigurationManager.ConnectionStrings["ASMDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    string query = "UPDATE UserAccount SET PasswordHash = @PasswordHash, IsActive = 1 WHERE Employee_ID = @Employee_ID";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@PasswordHash", passwordHash);
                        cmd.Parameters.AddWithValue("@Employee_ID", employeeId);
                        int rows = cmd.ExecuteNonQuery();
                        return rows > 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error updating password: " + ex.Message);
                    return false;
                }
            }
        }

        public static string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                byte[] hash = sha256.ComputeHash(bytes);
                StringBuilder sb = new StringBuilder();
                foreach (byte b in hash)
                    sb.Append(b.ToString("x2"));
                return sb.ToString();
            }
        }
    }
}
