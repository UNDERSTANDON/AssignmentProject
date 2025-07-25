using ASMDB.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ASMDB.Connection
{
    /// <summary>
    /// Data access layer for warehouse product queries and searches.
    /// </summary>
    public class DAL_Warehouse
    {
        /// <summary>
        /// Gets all products with their quantity information.
        /// </summary>
        /// <returns>List of ProductWithQuantity objects.</returns>
        public List<ProductWithQuantity> GetAllProductsWithQuantity()
        {
            List<ProductWithQuantity> products = new List<ProductWithQuantity>();
            string connStr = ConfigurationManager.ConnectionStrings["ASMDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    string query = @"SELECT p.Prod_ID, p.Prod_Name, p.Prod_Price, p.Category_ID, c.Category_Name, p.Prod_Quantity 
                                   FROM Products p 
                                   JOIN Category c ON p.Category_ID = c.Category_ID 
                                   ORDER BY p.Prod_ID";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                products.Add(new ProductWithQuantity
                                {
                                    Prod_ID = reader.GetInt32(0),
                                    Prod_Name = reader.GetString(1),
                                    Prod_Price = reader.GetDecimal(2),
                                    Category_ID = reader.GetInt32(3),
                                    Category_Name = reader.GetString(4),
                                    Quantity = reader.GetInt32(5)
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
        /// <returns>List of ProductWithQuantity objects matching the search.</returns>
        public List<ProductWithQuantity> SearchProducts(string searchTerm, string searchType)
        {
            List<ProductWithQuantity> products = new List<ProductWithQuantity>();
            string connStr = ConfigurationManager.ConnectionStrings["ASMDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    string query;
                    if (searchType == "ID")
                    {
                        query = @"SELECT p.Prod_ID, p.Prod_Name, p.Prod_Price, p.Category_ID, c.Category_Name, p.Prod_Quantity 
                                   FROM Products p 
                                   JOIN Category c ON p.Category_ID = c.Category_ID 
                                   WHERE p.Prod_ID = @SearchID
                                   ORDER BY p.Prod_ID";
                    }
                    else
                    {
                        query = @"SELECT p.Prod_ID, p.Prod_Name, p.Prod_Price, p.Category_ID, c.Category_Name, p.Prod_Quantity 
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
                                products.Add(new ProductWithQuantity
                                {
                                    Prod_ID = reader.GetInt32(0),
                                    Prod_Name = reader.GetString(1),
                                    Prod_Price = reader.GetDecimal(2),
                                    Category_ID = reader.GetInt32(3),
                                    Category_Name = reader.GetString(4),
                                    Quantity = reader.GetInt32(5)
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
        /// Adds a new product to the database.
        /// </summary>
        /// <param name="prodName">Name of the product.</param>
        /// <param name="prodPrice">Price of the product.</param>
        /// <param name="categoryId">Category ID of the product.</param>
        /// <param name="quantity">Quantity of the product.</param>
        /// <returns>Newly created product ID, or -1 if failed.</returns>
        public int AddProduct(string prodName, decimal prodPrice, int categoryId, int quantity)
        {
            string connStr = ConfigurationManager.ConnectionStrings["ASMDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    // Find max Prod_ID and increment by 1
                    int newProdId = 1;
                    string getMaxIdQuery = "SELECT ISNULL(MAX(Prod_ID), 0) + 1 FROM Products";
                    using (SqlCommand getMaxIdCmd = new SqlCommand(getMaxIdQuery, conn))
                    {
                        newProdId = (int)getMaxIdCmd.ExecuteScalar();
                    }

                    string query = "INSERT INTO Products (Prod_ID, Prod_Name, Prod_Price, Category_ID, Prod_Quantity) VALUES (@Prod_ID, @Prod_Name, @Prod_Price, @Category_ID, @Prod_Quantity)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Prod_ID", newProdId);
                        cmd.Parameters.AddWithValue("@Prod_Name", prodName);
                        cmd.Parameters.AddWithValue("@Prod_Price", prodPrice);
                        cmd.Parameters.AddWithValue("@Category_ID", categoryId);
                        cmd.Parameters.AddWithValue("@Prod_Quantity", quantity);

                        int result = cmd.ExecuteNonQuery();
                        if (result > 0)
                        {
                            return newProdId;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error adding product: " + ex.Message);
                }
            }
            return -1;
        }

        /// <summary>
        /// Updates an existing product in the database.
        /// </summary>
        /// <param name="prodId">ID of the product to update.</param>
        /// <param name="prodName">New name of the product.</param>
        /// <param name="prodPrice">New price of the product.</param>
        /// <param name="categoryId">New category ID of the product.</param>
        /// <param name="quantity">New quantity of the product.</param>
        /// <returns>True if the product was updated successfully, otherwise false.</returns>
        public bool UpdateProduct(int prodId, string prodName, decimal prodPrice, int categoryId, int quantity)
        {
            string connStr = ConfigurationManager.ConnectionStrings["ASMDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    string query = "UPDATE Products SET Prod_Name = @Prod_Name, Prod_Price = @Prod_Price, Category_ID = @Category_ID, Prod_Quantity = @Prod_Quantity WHERE Prod_ID = @Prod_ID";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Prod_ID", prodId);
                        cmd.Parameters.AddWithValue("@Prod_Name", prodName);
                        cmd.Parameters.AddWithValue("@Prod_Price", prodPrice);
                        cmd.Parameters.AddWithValue("@Category_ID", categoryId);
                        cmd.Parameters.AddWithValue("@Prod_Quantity", quantity);

                        int result = cmd.ExecuteNonQuery();
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

        /// <summary>
        /// Deletes a product from the database.
        /// </summary>
        /// <param name="prodId">ID of the product to delete.</param>
        /// <returns>True if the product was deleted successfully, otherwise false.</returns>
        public bool DeleteProduct(int prodId)
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
                            string deleteOrderProductsQuery = "DELETE FROM Order_Products WHERE Prod_ID = @Prod_ID";
                            using (SqlCommand cmd = new SqlCommand(deleteOrderProductsQuery, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@Prod_ID", prodId);
                                cmd.ExecuteNonQuery();
                            }

                            // Delete from ProductsImage (FK constraint)
                            string deleteImageQuery = "DELETE FROM ProductsImage WHERE Prod_ID = @Prod_ID";
                            using (SqlCommand cmd = new SqlCommand(deleteImageQuery, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@Prod_ID", prodId);
                                cmd.ExecuteNonQuery();
                            }

                            // Finally delete from Products
                            string deleteProductQuery = "DELETE FROM Products WHERE Prod_ID = @Prod_ID";
                            using (SqlCommand cmd = new SqlCommand(deleteProductQuery, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@Prod_ID", prodId);
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
                    MessageBox.Show("Error deleting product: " + ex.Message);
                }
            }
            return false;
        }

        /// <summary>
        /// Gets all categories from the database.
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
    }

    public class ProductWithQuantity
    {
        public int Prod_ID { get; set; }
        public string Prod_Name { get; set; }
        public decimal Prod_Price { get; set; }
        public int Category_ID { get; set; }
        public string Category_Name { get; set; }
        public int Quantity { get; set; }
    }
}