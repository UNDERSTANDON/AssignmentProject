using System;
using System.Drawing;
using System.Windows.Forms;

namespace ASMDB.Customer
{
    public partial class ItemDetailDialog : Form
    {
        private Action<string, decimal, int> addToCartCallback;

        public ItemDetailDialog(string name, decimal price, Image img, Action<string, decimal, int> addToCart)
        {
            InitializeComponent();
            addToCartCallback = addToCart;
            lblName.Text = name;
            lblPrice.Text = $"Price: ${price:F2}";
            picProduct.Image = img;
            btnAddToCart.Click += btnAddToCart_Click;
        }

        private void btnAddToCart_Click(object sender, EventArgs e)
        {
            addToCartCallback(lblName.Text, decimal.Parse(lblPrice.Text.Replace("Price: $", "")), (int)numQuantity.Value);
            this.Close();
        }
    }
}