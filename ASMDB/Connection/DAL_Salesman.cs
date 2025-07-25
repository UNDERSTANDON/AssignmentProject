using ASMDB.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ASMDB.Connection
{
    /// <summary>
    /// Data access layer for salesman product queries and searches.
    /// </summary>
    public class DAL_Salesman
    {
        /// <summary>
        /// Gets all products with their category information.
        /// </summary>
        /// <returns>List of ProductWithCategory objects.</returns>
        public List<ProductWithCategory> GetAllProductsWithCategory()
        {
            List<ProductWithCategory> products = new List<ProductWithCategory>();
            string connStr = ConfigurationManager.ConnectionStrings["ASMDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    string query = @"SELECT p.Prod_ID, p.Prod_Name, p.Prod_Price, p.Category_ID, c.Category_Name 
                                   FROM Products p 
                                   JOIN Category c ON p.Category_ID = c.Category_ID 
                                   ORDER BY p.Prod_ID";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                products.Add(new ProductWithCategory
                                {
                                    Prod_ID = reader.GetInt32(0),
                                    Prod_Name = reader.GetString(1),
                                    Prod_Price = reader.GetDecimal(2),
                                    Category_ID = reader.GetInt32(3),
                                    Category_Name = reader.GetString(4),
                                    Status = "Visible", // Default status
                                    MarkDown = "No"     // Default markdown
                                });
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error fetching products: " + ex.Message);
                }
            }
            return products;
        }

        /// <summary>
        /// Searches products by term and type (ID or Name).
        /// </summary>
        /// <param name="searchTerm">Search term (ID or Name).</param>
        /// <param name="searchType">Type of search (ID or Name).</param>
        /// <returns>List of ProductWithCategory objects matching the search.</returns>
        public List<ProductWithCategory> SearchProducts(string searchTerm, string searchType)
        {
            List<ProductWithCategory> products = new List<ProductWithCategory>();
            string connStr = ConfigurationManager.ConnectionStrings["ASMDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    string query;
                    if (searchType == "ID")
                    {
                        query = @"SELECT p.Prod_ID, p.Prod_Name, p.Prod_Price, p.Category_ID, c.Category_Name 
                                   FROM Products p 
                                   JOIN Category c ON p.Category_ID = c.Category_ID 
                                   WHERE p.Prod_ID = @SearchID
                                   ORDER BY p.Prod_ID";
                    }
                    else
                    {
                        query = @"SELECT p.Prod_ID, p.Prod_Name, p.Prod_Price, p.Category_ID, c.Category_Name 
                                   FROM Products p 
                                   JOIN Category c ON p.Category_ID = c.Category_ID 
                                   WHERE p.Prod_Name LIKE @SearchTerm OR c.Category_Name LIKE @SearchTerm
                                   ORDER BY p.Prod_ID";
                    }
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        if (searchType == "ID" && int.TryParse(searchTerm, out int id))
                        {
                            cmd.Parameters.AddWithValue("@SearchID", id);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@SearchTerm", "%" + searchTerm + "%");
                        }
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                products.Add(new ProductWithCategory
                                {
                                    Prod_ID = reader.GetInt32(0),
                                    Prod_Name = reader.GetString(1),
                                    Prod_Price = reader.GetDecimal(2),
                                    Category_ID = reader.GetInt32(3),
                                    Category_Name = reader.GetString(4),
                                    Status = "Visible", // Default status
                                    MarkDown = "No"     // Default markdown
                                });
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error searching products: " + ex.Message);
                }
            }
            return products;
        }

        /// <summary>
        /// Updates a product's details.
        /// </summary>
        /// <param name="prodId">Product ID.</param>
        /// <param name="prodName">Product name.</param>
        /// <param name="prodPrice">Product price.</param>
        /// <param name="categoryId">Category ID.</param>
        /// <param name="imagePath">Optional image path.</param>
        /// <returns>True if the product was updated successfully, otherwise false.</returns>
        public bool UpdateProduct(int prodId, string prodName, decimal prodPrice, int categoryId, string imagePath = null)
        {
            string connStr = ConfigurationManager.ConnectionStrings["ASMDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    string query = "UPDATE Products SET Prod_Name = @Prod_Name, Prod_Price = @Prod_Price, Category_ID = @Category_ID WHERE Prod_ID = @Prod_ID";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Prod_ID", prodId);
                        cmd.Parameters.AddWithValue("@Prod_Name", prodName);
                        cmd.Parameters.AddWithValue("@Prod_Price", prodPrice);
                        cmd.Parameters.AddWithValue("@Category_ID", categoryId);
                        int result = cmd.ExecuteNonQuery();
                        if (result > 0 && !string.IsNullOrEmpty(imagePath))
                        {
                            SetProductImage(conn, prodId, imagePath);
                        }
                        return result > 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error updating product: " + ex.Message);
                }
            }
            return false;
        }

        private void SetProductImage(SqlConnection conn, int prodId, string imagePath)
        {
            bool fileExists = System.IO.File.Exists(imagePath);
            int isExisted = fileExists ? 1 : 0;
            string pathToStore = fileExists ? imagePath.Replace("\\", "/") : null;
            // Check if entry exists
            string checkQuery = "SELECT COUNT(*) FROM ProductsImage WHERE Prod_ID = @Prod_ID";
            using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
            {
                checkCmd.Parameters.AddWithValue("@Prod_ID", prodId);
                int count = (int)checkCmd.ExecuteScalar();
                if (count > 0)
                {
                    // Update
                    string updateQuery = "UPDATE ProductsImage SET Image_Path = @Image_Path, Is_Existed = @Is_Existed WHERE Prod_ID = @Prod_ID";
                    using (SqlCommand updateCmd = new SqlCommand(updateQuery, conn))
                    {
                        updateCmd.Parameters.AddWithValue("@Image_Path", (object)pathToStore ?? System.DBNull.Value);
                        updateCmd.Parameters.AddWithValue("@Is_Existed", isExisted);
                        updateCmd.Parameters.AddWithValue("@Prod_ID", prodId);
                        updateCmd.ExecuteNonQuery();
                    }
                }
                else
                {
                    // Insert
                    string insertQuery = "INSERT INTO ProductsImage (Image_ID, Image_Path, Is_Existed, Prod_ID) VALUES (@Image_ID, @Image_Path, @Is_Existed, @Prod_ID)";
                    int newImageId = 1;
                    string getMaxIdQuery = "SELECT ISNULL(MAX(Image_ID), 0) + 1 FROM ProductsImage";
                    using (SqlCommand getMaxIdCmd = new SqlCommand(getMaxIdQuery, conn))
                    {
                        newImageId = (int)getMaxIdCmd.ExecuteScalar();
                    }
                    using (SqlCommand insertCmd = new SqlCommand(insertQuery, conn))
                    {
                        insertCmd.Parameters.AddWithValue("@Image_ID", newImageId);
                        insertCmd.Parameters.AddWithValue("@Image_Path", (object)pathToStore ?? System.DBNull.Value);
                        insertCmd.Parameters.AddWithValue("@Is_Existed", isExisted);
                        insertCmd.Parameters.AddWithValue("@Prod_ID", prodId);
                        insertCmd.ExecuteNonQuery();
                    }
                }
            }
        }

        /// <summary>
        /// Gets all categories.
        /// </summary>
        /// <returns>List of Category objects.</returns>
        public List<Category> GetAllCategories()
        {
            List<Category> categories = new List<Category>();
            string connStr = ConfigurationManager.ConnectionStrings["ASMDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT Category_ID, Category_Name FROM Category ORDER BY Category_Name";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                categories.Add(new Category
                                {
                                    Category_ID = reader.GetInt32(0),
                                    Category_Name = reader.GetString(1)
                                });
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error fetching categories: " + ex.Message);
                }
            }
            return categories;
        }

        /// <summary>
        /// Gets the image path of a product.
        /// </summary>
        /// <param name="prodId">Product ID.</param>
        /// <returns>Image path as a string, or null if not found.</returns>
        public string GetProductImagePath(int prodId)
        {
            string connStr = ConfigurationManager.ConnectionStrings["ASMDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string query = "SELECT Image_Path FROM ProductsImage WHERE Prod_ID = @Prod_ID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Prod_ID", prodId);
                    var result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                        return result.ToString();
                }
            }
            return null;
        }
    }

    public class ProductWithCategory
    {
        public int Prod_ID { get; set; }
        public string Prod_Name { get; set; }
        public decimal Prod_Price { get; set; }
        public int Category_ID { get; set; }
        public string Category_Name { get; set; }
        public string Status { get; set; }
        public string MarkDown { get; set; }
    }
}