using System;
using System.Windows.Forms;

namespace ASMDB
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Initialize and test database connection at program start
            var dal = new Connection.DAL_Login();
            bool connected = dal.TestConnection();
            while (!connected)
            {
                DialogResult result = MessageBox.Show(
                    "Database connection failed. Please check your connection settings.",
                    "Connection Error",
                    MessageBoxButtons.RetryCancel,
                    MessageBoxIcon.Error);
                if (result == DialogResult.Retry)
                {
                    connected = dal.TestConnection();
                }
                else // Cancel
                {
                    Application.Exit();
                    return;
                }
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Login.LoginFRM());
        }
    }
}
