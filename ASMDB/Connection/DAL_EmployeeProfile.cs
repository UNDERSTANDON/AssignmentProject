using ASMDB.Models;
using System;
using System.Data.SqlClient;

namespace Connection
{
    /// <summary>
    /// Data access layer for employee profile management.
    /// </summary>
    public class DAL_EmployeeProfile
    {
        private string connStr;

        /// <summary>
        /// Initializes a new instance of the DAL_EmployeeProfile class with the specified connection string.
        /// </summary>
        /// <param name="connectionString">The connection string to the SQL Server database.</param>
        public DAL_EmployeeProfile(string connectionString)
        {
            connStr = connectionString;
        }

        /// <summary>
        /// Gets the employee profile, image ID, and image path for a given employee ID.
        /// </summary>
        /// <param name="employeeId">The employee ID.</param>
        /// <returns>Tuple of Employees object, image ID, and image path.</returns>
        public (Employees, int?, string) GetEmployeeProfile(int employeeId)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                // Get employee info
                string employeeQuery = "SELECT Employee_ID, Employee_Name, Position, Phone, Address, Email, Role_ID FROM Employee WHERE Employee_ID = @Employee_ID";
                using (SqlCommand cmd = new SqlCommand(employeeQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@Employee_ID", employeeId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (!reader.Read())
                            return (null, null, null);
                        var employee = new Employees
                        {
                            Employee_ID = reader.IsDBNull(0) ? 0 : Convert.ToInt32(reader.GetValue(0)),
                            Employee_Name = reader.IsDBNull(1) ? null : reader.GetValue(1).ToString(),
                            Position = reader.IsDBNull(2) ? null : reader.GetValue(2).ToString(),
                            Phone = reader.IsDBNull(3) ? null : reader.GetValue(3).ToString(),
                            Address = reader.IsDBNull(4) ? null : reader.GetValue(4).ToString(),
                            Email = reader.IsDBNull(5) ? null : reader.GetValue(5).ToString(),
                            Role_ID = reader.IsDBNull(6) ? 0 : Convert.ToInt32(reader.GetValue(6))
                        };
                        reader.Close();
                        // Get image id and path
                        string imageQuery = "SELECT Image_ID, Image_Path FROM EmployeesImage WHERE Employee_ID = @Employee_ID";
                        using (SqlCommand imgCmd = new SqlCommand(imageQuery, conn))
                        {
                            imgCmd.Parameters.AddWithValue("@Employee_ID", employeeId);
                            using (SqlDataReader imgReader = imgCmd.ExecuteReader())
                            {
                                if (imgReader.Read())
                                {
                                    int? imageId = imgReader["Image_ID"] != DBNull.Value ? (int?)Convert.ToInt32(imgReader["Image_ID"]) : null;
                                    string imagePath = imgReader["Image_Path"] != DBNull.Value ? imgReader["Image_Path"].ToString() : null;
                                    return (employee, imageId, imagePath);
                                }
                                else
                                {
                                    return (employee, null, null);
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Updates employee information in the Employee table.
        /// </summary>
        /// <param name="employee">The Employees object with updated info.</param>
        /// <returns>True if update was successful, false otherwise.</returns>
        public bool UpdateEmployeeInfo(Employees employee)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string updateQuery = "UPDATE Employee SET Employee_Name = @Employee_Name, Position = @Position, Phone = @Phone, Address = @Address, Email = @Email, Role_ID = @Role_ID WHERE Employee_ID = @Employee_ID";
                using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@Employee_ID", employee.Employee_ID);
                    cmd.Parameters.AddWithValue("@Employee_Name", employee.Employee_Name);
                    cmd.Parameters.AddWithValue("@Position", employee.Position);
                    cmd.Parameters.AddWithValue("@Phone", (object)employee.Phone ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Address", (object)employee.Address ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Email", (object)employee.Email ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Role_ID", employee.Role_ID);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        /// <summary>
        /// Sets or updates the profile image ID and path for an employee in the EmployeesImage table.
        /// </summary>
        /// <param name="employeeId">The employee ID.</param>
        /// <param name="imageId">The image ID.</param>
        /// <param name="imagePath">The image path.</param>
        /// <returns>True if update was successful, false otherwise.</returns>
        public bool SetEmployeeProfileImage(int employeeId, int imageId, string imagePath)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
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
                        string updateQuery = "UPDATE EmployeesImage SET Image_ID = @Image_ID, Image_Path = @Image_Path, Is_Existed = @Is_Existed WHERE Employee_ID = @Employee_ID";
                        using (SqlCommand updateCmd = new SqlCommand(updateQuery, conn))
                        {
                            updateCmd.Parameters.AddWithValue("@Image_ID", imageId);
                            updateCmd.Parameters.AddWithValue("@Image_Path", (object)pathToStore ?? DBNull.Value);
                            updateCmd.Parameters.AddWithValue("@Is_Existed", isExisted);
                            updateCmd.Parameters.AddWithValue("@Employee_ID", employeeId);
                            return updateCmd.ExecuteNonQuery() > 0;
                        }
                    }
                    else
                    {
                        // Insert
                        string insertQuery = "INSERT INTO EmployeesImage (Employee_ID, Image_ID, Image_Path, Is_Existed) VALUES (@Employee_ID, @Image_ID, @Image_Path, @Is_Existed)";
                        using (SqlCommand insertCmd = new SqlCommand(insertQuery, conn))
                        {
                            insertCmd.Parameters.AddWithValue("@Employee_ID", employeeId);
                            insertCmd.Parameters.AddWithValue("@Image_ID", imageId);
                            insertCmd.Parameters.AddWithValue("@Image_Path", (object)pathToStore ?? DBNull.Value);
                            insertCmd.Parameters.AddWithValue("@Is_Existed", isExisted);
                            return insertCmd.ExecuteNonQuery() > 0;
                        }
                    }
                }
            }
        }
    }
}