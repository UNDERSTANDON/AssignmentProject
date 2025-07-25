using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ASMDB.Models;

namespace ASMDB.Connection
{
    /// <summary>
    /// Data access layer for retrieving sales and employee statistics.
    /// </summary>
    public class DAL_Statistics
    {
        /// <summary>
        /// Gets sales and profit statistics by product, with optional date range and sorting.
        /// </summary>
        /// <param name="startDate">The start date for filtering statistics (nullable).</param>
        /// <param name="endDate">The end date for filtering statistics (nullable).</param>
        /// <param name="sortBy">The column to sort by (default: TotalPrice).</param>
        /// <param name="ascending">Whether to sort ascending (default: false).</param>
        /// <returns>A DataTable containing product sales statistics.</returns>
        public DataTable GetProductSalesStatistics(DateTime? startDate, DateTime? endDate, string sortBy = "TotalPrice", bool ascending = false)
        {
            DataTable table = new DataTable();
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["ASMDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string orderBy = sortBy;
                if (orderBy == "ID") orderBy = "Product_ID";
                if (orderBy == "Name") orderBy = "Product_Name";
                if (orderBy == "Sold") orderBy = "TotalSell";
                if (orderBy == "Highest Profit") orderBy = "TotalPrice";
                string ascDesc = ascending ? "ASC" : "DESC";
                string sql = @"SELECT
                                p.Prod_ID AS Product_ID,
                                p.Prod_Name AS Product_Name,
                                SUM(o.Quantity) AS TotalSell,
                                p.Prod_Price AS Price,
                                SUM(o.Quantity * p.Prod_Price) AS TotalPrice
                            FROM Orders o
                            JOIN Order_Products op ON o.Order_ID = op.Order_ID
                            JOIN Products p ON op.Prod_ID = p.Prod_ID
                            WHERE (@startDate IS NULL OR o.Order_Date >= @startDate)
                              AND (@endDate IS NULL OR o.Order_Date <= @endDate)
                            GROUP BY p.Prod_ID, p.Prod_Name, p.Prod_Price
                            ORDER BY " + orderBy + " " + ascDesc;
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@startDate", (object)startDate ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@endDate", (object)endDate ?? DBNull.Value);
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(table);
                    }
                }
            }
            return table;
        }

        /// <summary>
        /// Gets employee sales statistics with search and time filtering.
        /// </summary>
        /// <param name="searchText">Text to search for.</param>
        /// <param name="searchType">Type of search (ID, Name, Sold, etc.).</param>
        /// <param name="timeFilter">Time filter for statistics.</param>
        /// <param name="sortOrder">Sort order for results.</param>
        /// <returns>List of EmployeeStatistics objects.</returns>
        public List<EmployeeStatistics> GetEmployeeSalesStatistics(string searchText, string searchType, string timeFilter, string sortOrder)
        {
            List<EmployeeStatistics> employeeStats = new List<EmployeeStatistics>();
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["ASMDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                
                // Build WHERE clause for search
                string searchWhere = "";
                if (!string.IsNullOrEmpty(searchText))
                {
                    switch (searchType)
                    {
                        case "ID":
                            searchWhere = "AND e.Employee_ID LIKE @searchText";
                            break;
                        case "Name":
                            searchWhere = "AND e.Employee_Name LIKE @searchText";
                            break;
                        case "Sold":
                            searchWhere = "AND (SoldWeek LIKE @searchText OR SoldMonth LIKE @searchText OR SoldYear LIKE @searchText OR SoldAllTime LIKE @searchText)";
                            break;
                        default:
                            searchWhere = "AND (e.Employee_ID LIKE @searchText OR e.Employee_Name LIKE @searchText)";
                            break;
                    }
                }

                // Build ORDER BY clause
                string orderBy = "Employee_ID";
                switch (searchType)
                {
                    case "ID":
                        orderBy = "Employee_ID";
                        break;
                    case "Name":
                        orderBy = "Employee_Name";
                        break;
                    case "Sold":
                        orderBy = "SoldAllTime";
                        break;
                    default:
                        orderBy = "Employee_ID";
                        break;
                }

                // Build time filter WHERE clause
                string timeFilterWhere = "";
                switch (timeFilter)
                {
                    case "Last Week":
                        timeFilterWhere = "AND o.Order_Date >= DATEADD(week, -1, GETDATE())";
                        break;
                    case "Last Month":
                        timeFilterWhere = "AND o.Order_Date >= DATEADD(month, -1, GETDATE())";
                        break;
                    case "Last Year":
                        timeFilterWhere = "AND o.Order_Date >= DATEADD(year, -1, GETDATE())";
                        break;
                    case "All Time":
                    default:
                        timeFilterWhere = ""; // No filter for all time
                        break;
                }

                string sql = $@"
                    WITH EmployeeSales AS (
                        SELECT 
                            e.Employee_ID,
                            e.Employee_Name,
                            ISNULL(SUM(CASE 
                                WHEN o.Order_Date >= DATEADD(week, -1, GETDATE()) THEN o.Quantity 
                                ELSE 0 
                            END), 0) AS SoldWeek,
                            ISNULL(SUM(CASE 
                                WHEN o.Order_Date >= DATEADD(month, -1, GETDATE()) THEN o.Quantity 
                                ELSE 0 
                            END), 0) AS SoldMonth,
                            ISNULL(SUM(CASE 
                                WHEN o.Order_Date >= DATEADD(year, -1, GETDATE()) THEN o.Quantity 
                                ELSE 0 
                            END), 0) AS SoldYear,
                            ISNULL(SUM(o.Quantity), 0) AS SoldAllTime
                        FROM Employee e
                        LEFT JOIN Orders o ON e.Employee_ID = o.Employee_ID
                        WHERE 1=1 {searchWhere} {timeFilterWhere}
                        GROUP BY e.Employee_ID, e.Employee_Name
                    )
                    SELECT 
                        Employee_ID,
                        Employee_Name,
                        SoldWeek,
                        SoldMonth,
                        SoldYear,
                        SoldAllTime
                    FROM EmployeeSales
                    ORDER BY {orderBy} {sortOrder}";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    if (!string.IsNullOrEmpty(searchText))
                    {
                        cmd.Parameters.AddWithValue("@searchText", "%" + searchText + "%");
                    }

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            EmployeeStatistics stat = new EmployeeStatistics
                            {
                                Employee_ID = Convert.ToInt32(reader["Employee_ID"]),
                                Employee_Name = reader["Employee_Name"].ToString(),
                                SoldWeek = Convert.ToInt32(reader["SoldWeek"]),
                                SoldMonth = Convert.ToInt32(reader["SoldMonth"]),
                                SoldYear = Convert.ToInt32(reader["SoldYear"]),
                                SoldAllTime = Convert.ToInt32(reader["SoldAllTime"])
                            };
                            employeeStats.Add(stat);
                        }
                    }
                }
            }
            return employeeStats;
        }
    }
}