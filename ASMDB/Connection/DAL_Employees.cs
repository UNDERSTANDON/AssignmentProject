using ASMDB.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ASMDB.Connection
{
    /// <summary>
    /// Data access layer for employee queries and management.
    /// </summary>
    public class DAL_Employees
    {
        /// <summary>
        /// Gets all employees from the database.
        /// </summary>
        /// <returns>List of Employees objects.</returns>
        public List<Employees> GetAllEmployees()
        {
            List<Employees> employees = new List<Employees>();
            string connStr = ConfigurationManager.ConnectionStrings["ASMDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT Employee_ID, Employee_Name, Position, Phone, Address, Email, Role_ID FROM Employee";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                employees.Add(new Employees
                                {
                                    Employee_ID = reader.GetInt32(0),
                                    Employee_Name = reader.GetString(1),
                                    Position = reader.GetString(2),
                                    Phone = reader.IsDBNull(3) ? null : reader.GetString(3),
                                    Address = reader.IsDBNull(4) ? null : reader.GetString(4),
                                    Email = reader.IsDBNull(5) ? null : reader.GetString(5),
                                    Role_ID = reader.GetInt32(6)
                                });
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error fetching employees: " + ex.Message);
                }
            }
            return employees;
        }

        /// <summary>
        /// Searches employees by term and type (ID or Name).
        /// </summary>
        /// <param name="term">Search term (ID or Name).</param>
        /// <param name="type">Type of search (ID or Name).</param>
        /// <returns>List of Employees objects matching the search.</returns>
        public List<Employees> SearchEmployees(string term, string type)
        {
            List<Employees> employees = new List<Employees>();
            string connStr = ConfigurationManager.ConnectionStrings["ASMDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    string query = type == "ID"
                        ? "SELECT Employee_ID, Employee_Name, Position, Phone, Address, Email, Role_ID FROM Employee WHERE Employee_ID = @term"
                        : "SELECT Employee_ID, Employee_Name, Position, Phone, Address, Email, Role_ID FROM Employee WHERE Employee_Name LIKE @term";
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
                                employees.Add(new Employees
                                {
                                    Employee_ID = reader.GetInt32(0),
                                    Employee_Name = reader.GetString(1),
                                    Position = reader.GetString(2),
                                    Phone = reader.IsDBNull(3) ? null : reader.GetString(3),
                                    Address = reader.IsDBNull(4) ? null : reader.GetString(4),
                                    Email = reader.IsDBNull(5) ? null : reader.GetString(5),
                                    Role_ID = reader.GetInt32(6)
                                });
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error searching employees: " + ex.Message);
                }
            }
            return employees;
        }

        /// <summary>
        /// Adds a new employee to the database.
        /// </summary>
        /// <param name="name">Employee name.</param>
        /// <param name="position">Employee position.</param>
        /// <param name="phone">Employee phone number.</param>
        /// <param name="address">Employee address.</param>
        /// <param name="email">Employee email.</param>
        /// <param name="roleId">Role ID.</param>
        /// <param name="username">Username for the account.</param>
        /// <param name="passwordHash">Hashed password.</param>
        /// <returns>True if successful, false otherwise.</returns>
        public bool AddEmployee(string name, string position, string phone, string address, string email, int roleId, string username, string passwordHash)
        {
            string connStr = ConfigurationManager.ConnectionStrings["ASMDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    int newEmpId = 1;
                    string getMaxIdQuery = "SELECT ISNULL(MAX(Employee_ID), 0) + 1 FROM Employee";
                    using (SqlCommand getMaxIdCmd = new SqlCommand(getMaxIdQuery, conn))
                    {
                        newEmpId = (int)getMaxIdCmd.ExecuteScalar();
                    }
                    string empQuery = "INSERT INTO Employee (Employee_ID, Employee_Name, Position, Phone, Address, Email, Role_ID) VALUES (@Employee_ID, @Employee_Name, @Position, @Phone, @Address, @Email, @Role_ID)";
                    using (SqlCommand empCmd = new SqlCommand(empQuery, conn))
                    {
                        empCmd.Parameters.AddWithValue("@Employee_ID", newEmpId);
                        empCmd.Parameters.AddWithValue("@Employee_Name", name);
                        empCmd.Parameters.AddWithValue("@Position", position);
                        empCmd.Parameters.AddWithValue("@Phone", (object)phone ?? DBNull.Value);
                        empCmd.Parameters.AddWithValue("@Address", (object)address ?? DBNull.Value);
                        empCmd.Parameters.AddWithValue("@Email", (object)email ?? DBNull.Value);
                        empCmd.Parameters.AddWithValue("@Role_ID", roleId);
                        empCmd.ExecuteNonQuery();
                    }
                    string accQuery = "INSERT INTO UserAccount (Username, PasswordHash, Employee_ID, CreatedAt, IsActive) VALUES (@Username, @PasswordHash, @Employee_ID, @CreatedAt, @IsActive)";
                    using (SqlCommand accCmd = new SqlCommand(accQuery, conn))
                    {
                        accCmd.Parameters.AddWithValue("@Username", username);
                        accCmd.Parameters.AddWithValue("@PasswordHash", passwordHash);
                        accCmd.Parameters.AddWithValue("@Employee_ID", newEmpId);
                        accCmd.Parameters.AddWithValue("@CreatedAt", DateTime.Now);
                        accCmd.Parameters.AddWithValue("@IsActive", false);
                        accCmd.ExecuteNonQuery();
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error adding employee: " + ex.Message);
                    return false;
                }
            }
        }

        /// <summary>
        /// Updates an existing employee's information.
        /// </summary>
        /// <param name="employeeId">Employee ID.</param>
        /// <param name="name">Employee name.</param>
        /// <param name="position">Employee position.</param>
        /// <param name="phone">Employee phone number.</param>
        /// <param name="address">Employee address.</param>
        /// <param name="email">Employee email.</param>
        /// <param name="roleId">Role ID.</param>
        /// <param name="imagePath">Path to employee's profile image (optional).</param>
        /// <returns>True if successful, false otherwise.</returns>
        public bool UpdateEmployee(int employeeId, string name, string position, string phone, string address, string email, int roleId, string imagePath = null)
        {
            string connStr = ConfigurationManager.ConnectionStrings["ASMDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    string query = "UPDATE Employee SET Employee_Name = @Employee_Name, Position = @Position, Phone = @Phone, Address = @Address, Email = @Email, Role_ID = @Role_ID WHERE Employee_ID = @Employee_ID";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Employee_ID", employeeId);
                        cmd.Parameters.AddWithValue("@Employee_Name", name);
                        cmd.Parameters.AddWithValue("@Position", position);
                        cmd.Parameters.AddWithValue("@Phone", (object)phone ?? System.DBNull.Value);
                        cmd.Parameters.AddWithValue("@Address", (object)address ?? System.DBNull.Value);
                        cmd.Parameters.AddWithValue("@Email", (object)email ?? System.DBNull.Value);
                        cmd.Parameters.AddWithValue("@Role_ID", roleId);
                        int rows = cmd.ExecuteNonQuery();
                        if (rows > 0 && !string.IsNullOrEmpty(imagePath))
                        {
                            SetEmployeeProfileImage(conn, employeeId, imagePath);
                        }
                        return rows > 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error updating employee: " + ex.Message);
                    return false;
                }
            }
        }

        private void SetEmployeeProfileImage(SqlConnection conn, int employeeId, string imagePath)
        {
            bool fileExists = System.IO.File.Exists(imagePath);
            int isExisted = fileExists ? 1 : 0;
            string pathToStore = fileExists ? imagePath.Replace("\\", "/") : null;
            // Check if entry exists
            string checkQuery = "SELECT COUNT(*) FROM EmployeesImage WHERE Employee_ID = @Employee_ID";
            using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
            {
                checkCmd.Parameters.AddWithValue("@Employee_ID", employeeId);
                int count = (int)checkCmd.ExecuteScalar();
                if (count > 0)
                {
                    // Update
                    string updateQuery = "UPDATE EmployeesImage SET Image_Path = @Image_Path, Is_Existed = @Is_Existed WHERE Employee_ID = @Employee_ID";
                    using (SqlCommand updateCmd = new SqlCommand(updateQuery, conn))
                    {
                        updateCmd.Parameters.AddWithValue("@Image_Path", (object)pathToStore ?? System.DBNull.Value);
                        updateCmd.Parameters.AddWithValue("@Is_Existed", isExisted);
                        updateCmd.Parameters.AddWithValue("@Employee_ID", employeeId);
                        updateCmd.ExecuteNonQuery();
                    }
                }
                else
                {
                    // Insert
                    string insertQuery = "INSERT INTO EmployeesImage (Image_ID, Image_Path, Is_Existed, Employee_ID) VALUES (@Image_ID, @Image_Path, @Is_Existed, @Employee_ID)";
                    int newImageId = 1;
                    string getMaxIdQuery = "SELECT ISNULL(MAX(Image_ID), 0) + 1 FROM EmployeesImage";
                    using (SqlCommand getMaxIdCmd = new SqlCommand(getMaxIdQuery, conn))
                    {
                        newImageId = (int)getMaxIdCmd.ExecuteScalar();
                    }
                    using (SqlCommand insertCmd = new SqlCommand(insertQuery, conn))
                    {
                        insertCmd.Parameters.AddWithValue("@Image_ID", newImageId);
                        insertCmd.Parameters.AddWithValue("@Image_Path", (object)pathToStore ?? System.DBNull.Value);
                        insertCmd.Parameters.AddWithValue("@Is_Existed", isExisted);
                        insertCmd.Parameters.AddWithValue("@Employee_ID", employeeId);
                        insertCmd.ExecuteNonQuery();
                    }
                }
            }
        }

        /// <summary>
        /// Deletes an employee from the database.
        /// </summary>
        /// <param name="employeeId">Employee ID.</param>
        /// <returns>True if successful, false otherwise.</returns>
        public bool DeleteEmployee(int employeeId)
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
                            string deleteOrderProductsQuery = "DELETE FROM Order_Products WHERE Order_ID IN (SELECT Order_ID FROM Orders WHERE Employee_ID = @Employee_ID)";
                            using (SqlCommand cmd = new SqlCommand(deleteOrderProductsQuery, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@Employee_ID", employeeId);
                                cmd.ExecuteNonQuery();
                            }

                            // Delete from Orders (FK constraint)
                            string deleteOrdersQuery = "DELETE FROM Orders WHERE Employee_ID = @Employee_ID";
                            using (SqlCommand cmd = new SqlCommand(deleteOrdersQuery, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@Employee_ID", employeeId);
                                cmd.ExecuteNonQuery();
                            }

                            // Delete from EmployeesImage (FK constraint)
                            string deleteImageQuery = "DELETE FROM EmployeesImage WHERE Employee_ID = @Employee_ID";
                            using (SqlCommand cmd = new SqlCommand(deleteImageQuery, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@Employee_ID", employeeId);
                                cmd.ExecuteNonQuery();
                            }

                            // Delete from UserAccount (FK constraint)
                            string deleteUserAccountQuery = "DELETE FROM UserAccount WHERE Employee_ID = @Employee_ID";
                            using (SqlCommand cmd = new SqlCommand(deleteUserAccountQuery, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@Employee_ID", employeeId);
                                cmd.ExecuteNonQuery();
                            }

                            // Finally delete from Employee
                            string deleteEmployeeQuery = "DELETE FROM Employee WHERE Employee_ID = @Employee_ID";
                            using (SqlCommand cmd = new SqlCommand(deleteEmployeeQuery, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@Employee_ID", employeeId);
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
                    MessageBox.Show("Error deleting employee: " + ex.Message);
                    return false;
                }
            }
        }
    }
}
