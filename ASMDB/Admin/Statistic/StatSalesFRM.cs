using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ASMDB.Connection;

namespace ASMDB.Admin.Statistic
{
    public partial class StatSalesFRM : Form
    {
        public StatSalesFRM()
        {
            InitializeComponent();
        }

        private DAL_Statistics dalStatistics = new DAL_Statistics();
        private bool ascending = false;
        private DateTime? customStartDate = null;
        private DateTime? customEndDate = null;

        private void LoadGridView()
        {
            string sortBy = cbSearchType.SelectedItem?.ToString() ?? "TotalPrice";
            DateTime? startDate = customStartDate;
            DateTime? endDate = customEndDate;
            // Optionally, parse cbSearchPast for date range if not using custom
            var table = dalStatistics.GetProductSalesStatistics(startDate, endDate, sortBy, ascending);
            dgvStat.Rows.Clear();
            foreach (DataRow row in table.Rows)
            {
                dgvStat.Rows.Add(
                    row["Product_ID"],
                    row["Product_Name"],
                    row["Price"],
                    row["TotalSell"],
                    row.Table.Columns.Contains("TotalSell") ? row["TotalSell"] : null,
                    row["TotalPrice"]
                );
            }
        }

        private void ReloadStatistics()
        {
            LoadGridView();
        }

        private void BtnASCDESC_Click(object sender, EventArgs e)
        {
            ascending = !ascending;
            BtnASCDESC.Text = ascending ? "ASC" : "DESC";
            if (BtnASCDESC.Text == "ASC")
            {
                // Change Text to "DESC"
                BtnASCDESC.Text = "DESC";
            }
            else
            {
                // Change Text to "ASC"
                BtnASCDESC.Text = "ASC";
            }
            ReloadStatistics();
        }

        private void cbSearchType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ReloadStatistics();
        }

        private void StatSalesFRM_Load(object sender, EventArgs e)
        {
            cbSearchType.SelectedIndexChanged += cbSearchType_SelectedIndexChanged;
            ReloadStatistics();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void StatSalesFRM_Load_1(object sender, EventArgs e)
        {
            LoadGridView();
            cbSearchType.SelectedIndex = 0; // Default to ID
            cbSearchPast.SelectedIndex = 0; // Default to the Last week
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            ReloadStatistics();
        }

        private void BtnChooseDate_Click(object sender, EventArgs e)
        {
            using (var dlg = new DateRangeDialog())
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    customStartDate = dlg.StartDate;
                    customEndDate = dlg.EndDate;
                    ReloadStatistics();
                }
            }
        }
    }
}
