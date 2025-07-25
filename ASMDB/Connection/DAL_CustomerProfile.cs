using System;
using System.Data.SqlClient;
using System.IO;

namespace Connection
{
    public class DAL_CustomerProfile
    {
        private string connStr;

        public DAL_CustomerProfile(string connectionString)
        {
            connStr = connectionString;
        }

        public bool DeleteCustomerAccount(int cusId)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                // Delete profile image if exists
                string deleteImageQuery = "DELETE FROM CustomersImage WHERE Cus_ID = @Cus_ID";
                using (SqlCommand cmdImg = new SqlCommand(deleteImageQuery, conn))
                {
                    cmdImg.Parameters.AddWithValue("@Cus_ID", cusId);
                    cmdImg.ExecuteNonQuery(); // ignore result, image may not exist
                }
                // Delete user account(s) for this customer
                string deleteUserAccountQuery = "DELETE FROM UserAccount WHERE Cus_ID = @Cus_ID";
                using (SqlCommand cmdUA = new SqlCommand(deleteUserAccountQuery, conn))
                {
                    cmdUA.Parameters.AddWithValue("@Cus_ID", cusId);
                    cmdUA.ExecuteNonQuery(); // ignore result, may not exist
                }
                // Delete customer
                string deleteCustomerQuery = "DELETE FROM Customer WHERE Cus_ID = @Cus_ID";
                using (SqlCommand cmdCus = new SqlCommand(deleteCustomerQuery, conn))
                {
                    cmdCus.Parameters.AddWithValue("@Cus_ID", cusId);
                    return cmdCus.ExecuteNonQuery() > 0;
                }
            }
        }

        // Returns (ASMDB.Models.Customers, int? imageId, string imagePath) tuple for a given customer ID
        public (ASMDB.Models.Customers, int?, string) GetCustomerProfile(int cusId)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                // Get customer info
                string customerQuery = "SELECT Cus_ID, Cus_Name, Phone, Address, Email FROM Customer WHERE Cus_ID = @Cus_ID";
                using (SqlCommand cmd = new SqlCommand(customerQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@Cus_ID", cusId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (!reader.Read())
                            return (null, null, null);
                        var customer = new ASMDB.Models.Customers
                        {
                            Cus_ID = reader.GetInt32(0),
                            Cus_Name = reader.GetString(1),
                            Phone = reader.GetString(2),
                            Address = reader.GetString(3),
                            Email = reader.GetString(4)
                        };
                        reader.Close();
                        // Get image id and path
                        string imageQuery = "SELECT Image_ID, Image_Path FROM CustomersImage WHERE Cus_ID = @Cus_ID";
                        using (SqlCommand imgCmd = new SqlCommand(imageQuery, conn))
                        {
                            imgCmd.Parameters.AddWithValue("@Cus_ID", cusId);
                            using (SqlDataReader imgReader = imgCmd.ExecuteReader())
                            {
                                if (imgReader.Read())
                                {
                                    int? imageId = imgReader["Image_ID"] != DBNull.Value ? (int?)Convert.ToInt32(imgReader["Image_ID"]) : null;
                                    string imagePath = imgReader["Image_Path"] != DBNull.Value ? imgReader["Image_Path"].ToString() : null;
                                    return (customer, imageId, imagePath);
                                }
                                else
                                {
                                    return (customer, null, null);
                                }
                            }
                        }
                    }
                }
            }
        }

        // Updates customer info in the Customer table
        public bool UpdateCustomerInfo(ASMDB.Models.Customers customer)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string updateQuery = "UPDATE Customer SET Cus_Name = @Cus_Name, Phone = @Phone, Address = @Address, Email = @Email WHERE Cus_ID = @Cus_ID";
                using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@Cus_ID", customer.Cus_ID);
                    cmd.Parameters.AddWithValue("@Cus_Name", customer.Cus_Name);
                    cmd.Parameters.AddWithValue("@Phone", customer.Phone);
                    cmd.Parameters.AddWithValue("@Address", customer.Address);
                    cmd.Parameters.AddWithValue("@Email", customer.Email);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        // Sets or updates the profile image id and path for a customer in CustomersImage table
        public bool SetCustomerProfileImage(int cusId, int imageId, string imagePath)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                bool fileExists = File.Exists(imagePath);
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
                        string updateQuery = "UPDATE CustomersImage SET Image_ID = @Image_ID, Image_Path = @Image_Path, Is_Existed = @Is_Existed WHERE Cus_ID = @Cus_ID";
                        using (SqlCommand updateCmd = new SqlCommand(updateQuery, conn))
                        {
                            updateCmd.Parameters.AddWithValue("@Image_ID", imageId);
                            updateCmd.Parameters.AddWithValue("@Image_Path", (object)pathToStore ?? DBNull.Value);
                            updateCmd.Parameters.AddWithValue("@Is_Existed", isExisted);
                            updateCmd.Parameters.AddWithValue("@Cus_ID", cusId);
                            return updateCmd.ExecuteNonQuery() > 0;
                        }
                    }
                    else
                    {
                        // Insert
                        string insertQuery = "INSERT INTO CustomersImage (Cus_ID, Image_ID, Image_Path, Is_Existed) VALUES (@Cus_ID, @Image_ID, @Image_Path, @Is_Existed)";
                        using (SqlCommand insertCmd = new SqlCommand(insertQuery, conn))
                        {
                            insertCmd.Parameters.AddWithValue("@Cus_ID", cusId);
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