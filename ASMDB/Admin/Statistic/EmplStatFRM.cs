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
    public partial class EmplStatFRM : Form
    {
        private DAL_Statistics dalStatistics;
        private bool isAscending = true;

        public EmplStatFRM()
        {
            InitializeComponent();
            dalStatistics = new DAL_Statistics();
            InitializeForm();
        }

        private void InitializeForm()
        {
            // Set default values
            cbSearchType.SelectedIndex = 0;
            cbSearchPast.SelectedIndex = 3; // All Time
            BtnASCDESC.Text = "ASC";

            // Wire up event handlers
            BtnRefresh.Click += BtnRefresh_Click;
            btnBack.Click += btnBack_Click;
            BtnASCDESC.Click += BtnASCDESC_Click;
            cbSearchType.SelectedIndexChanged += cbSearchType_SelectedIndexChanged;
            cbSearchPast.SelectedIndexChanged += cbSearchPast_SelectedIndexChanged;
            txtSearch.TextChanged += txtSearch_TextChanged;

            // Load initial data
            LoadGridView();
        }

        private void LoadGridView()
        {
            try
            {
                dgvStat.Rows.Clear();

                string searchText = txtSearch.Text.Trim();
                string searchType = cbSearchType.SelectedItem?.ToString() ?? "ID";
                string timeFilter = cbSearchPast.SelectedItem?.ToString() ?? "All Time";
                string sortOrder = isAscending ? "ASC" : "DESC";

                var employeeStats = dalStatistics.GetEmployeeSalesStatistics(searchText, searchType, timeFilter, sortOrder);

                foreach (var stat in employeeStats)
                {
                    dgvStat.Rows.Add(
                        stat.Employee_ID,
                        stat.Employee_Name,
                        stat.SoldWeek,
                        stat.SoldMonth,
                        stat.SoldYear,
                        stat.SoldAllTime
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading employee statistics: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            LoadGridView();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnASCDESC_Click(object sender, EventArgs e)
        {
            isAscending = !isAscending;
            BtnASCDESC.Text = isAscending ? "ASC" : "DESC";
            LoadGridView();
        }

        private void cbSearchType_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadGridView();
        }

        private void cbSearchPast_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadGridView();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadGridView();
        }
    }
}
