using ASMDB.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ASMDB.Connection
{
    /// <summary>
    /// Data access layer for product and category queries.
    /// </summary>
    public class DAL_Products
    {
        /// <summary>
        /// Gets all products with their category information.
        /// </summary>
        /// <returns>List of Products objects.</returns>
        public List<Products> GetAllProducts()
        {
            List<Products> products = new List<Products>();
            string connStr = ConfigurationManager.ConnectionStrings["ASMDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT p.Prod_ID, p.Prod_Name, p.Prod_Price, p.Category_ID, c.Category_Name FROM Products p JOIN Category c ON p.Category_ID = c.Category_ID";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                products.Add(new Products
                                {
                                    Prod_ID = reader.GetInt32(0),
                                    Prod_Name = reader.GetString(1),
                                    Prod_Price = reader.GetDecimal(2),
                                    Category_ID = reader.GetInt32(3)
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
        /// Gets products by category ID.
        /// </summary>
        /// <param name="categoryId">The category ID.</param>
        /// <returns>List of Products objects in the specified category.</returns>
        public List<Products> GetProductsByCategory(int categoryId)
        {
            List<Products> products = new List<Products>();
            string connStr = ConfigurationManager.ConnectionStrings["ASMDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT p.Prod_ID, p.Prod_Name, p.Prod_Price, p.Category_ID FROM Products p WHERE p.Category_ID = @Category_ID";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Category_ID", categoryId);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                products.Add(new Products
                                {
                                    Prod_ID = reader.GetInt32(0),
                                    Prod_Name = reader.GetString(1),
                                    Prod_Price = reader.GetDecimal(2),
                                    Category_ID = reader.GetInt32(3)
                                });
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error fetching products by category: " + ex.Message);
                }
            }
            return products;
        }

        /// <summary>
        /// Gets all product categories.
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
                    string query = "SELECT Category_ID, Category_Name FROM Category";
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
        /// Gets a product by its ID.
        /// </summary>
        /// <param name="prodId">The product ID.</param>
        /// <returns>Products object with the specified ID, or null if not found.</returns>
        public Products GetProductById(int prodId)
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["ASMDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT Prod_ID, Prod_Name, Prod_Price, Category_ID FROM Products WHERE Prod_ID = @Prod_ID";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Prod_ID", prodId);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new Products
                                {
                                    Prod_ID = reader.GetInt32(0),
                                    Prod_Name = reader.GetString(1),
                                    Prod_Price = reader.GetDecimal(2),
                                    Category_ID = reader.GetInt32(3)
                                };
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error fetching product by ID: " + ex.Message);
                }
            }
            return null;
        }

        /// <summary>
        /// Gets the image existence and path for a product.
        /// </summary>
        /// <param name="prodId">The product ID.</param>
        /// <returns>Tuple with existence flag and image path string.</returns>
        public (bool IsExisted, string ImagePath) GetProductImageInfo(int prodId)
        {
            string connStr = ConfigurationManager.ConnectionStrings["ASMDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string query = "SELECT Is_Existed, Image_Path FROM ProductsImage WHERE Prod_ID = @Prod_ID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Prod_ID", prodId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            bool isExisted = reader.GetBoolean(0);
                            string imagePath = reader.IsDBNull(1) ? null : reader.GetString(1);
                            return (isExisted, imagePath);
                        }
                    }
                }
            }
            return (false, null);
        }
    }
}