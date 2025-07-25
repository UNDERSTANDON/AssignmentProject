using ASMDB.Connection;
using ASMDB.Models;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ASMDB.Order
{
    public partial class ShippingInfoDialog : Form
    {
        private int? _cusId = null;
        public ShippingInfoDialog() : this(null) { }
        public ShippingInfoDialog(int? cusId)
        {
            InitializeComponent();
            dgvShippingInfo.AutoGenerateColumns = false;
            _cusId = cusId;
            LoadData();
        }

        private void LoadData()
        {
            DAL_Shipping dal = new DAL_Shipping();
            List<Shipping> shipments;
            if (_cusId.HasValue)
                shipments = dal.GetPendingShippingInfoByCustomer(_cusId.Value);
            else
                shipments = dal.GetPendingShippingInfo();
            dgvShippingInfo.Rows.Clear();
            foreach (var s in shipments)
            {
                dgvShippingInfo.Rows.Add(
                    s.Ship_ID,
                    s.Estimate_Date.ToShortDateString(),
                    s.Ship_Type,
                    s.Shipping_Cost,
                    s.Current_Location,
                    s.Shipping_Status
                );
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
