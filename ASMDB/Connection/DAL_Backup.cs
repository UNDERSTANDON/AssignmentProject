using System;
using System.Data.SqlClient;

namespace Connection
{
    /// <summary>
    /// Data access layer for creating backups of the ASM database.
    /// </summary>
    public class DAL_Backup
    {
        /// <summary>
        /// The connection string to the SQL Server database.
        /// </summary>
        private string connStr;

        /// <summary>
        /// Initializes a new instance of the DAL_Backup class with the specified connection string.
        /// </summary>
        /// <param name="connectionString">The connection string to the SQL Server database.</param>
        public DAL_Backup(string connectionString)
        {
            connStr = connectionString;
        }

        /// <summary>
        /// Creates a backup of the ASM database and saves it to the C:/ASMDB/Backup directory with a timestamped filename.
        /// </summary>
        public void CreateBackup()
        {
            string dir = "C:/ASMDB/Backup";
            string fileName = $"{DateTime.Now:yyyyMMdd_HHmmss}_backup.bak";
            string fullPath = System.IO.Path.Combine(dir, fileName);

            if (!System.IO.Directory.Exists(dir))
            {
                System.IO.Directory.CreateDirectory(dir);
            }

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    string backupQuery = $"BACKUP DATABASE [ASM] TO DISK = '{fullPath}' WITH FORMAT, INIT, SKIP, NOREWIND, NOUNLOAD, STATS = 10";
                    using (SqlCommand cmd = new SqlCommand(backupQuery, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error creating backup: " + ex.Message);
                }
            }
        }
    }
}
