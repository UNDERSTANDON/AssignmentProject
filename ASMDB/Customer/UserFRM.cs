using ASMDB.Connection;
using ASMDB.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ASMDB.Customer
{
    public partial class UserFRM : Form
    {
        private List<CartItem> cartItems = new List<CartItem>();
        private Panel cartPanel;
        private FlowLayoutPanel cartItemsPanel;
        private bool isCartVisible = false;
        private DAL_Products dalProducts;
        private ToolTip toolTip;
        private int cusId;

        public UserFRM(int cusId)
        {
            InitializeComponent();
            this.cusId = cusId;
            dalProducts = new DAL_Products();
            InitializeToolTips();
            picShippingIcon.BringToFront();
            btnPastOrders.BringToFront();
            LoadItems();
            InitializeCartPanel();
            picShippingIcon.Click += PicShippingIcon_Click;
            btnPastOrders.Click += BtnPastOrders_Click;
            btnSearch.Click += BtnSearch_Click;
            txtSearch.KeyPress += TxtSearch_KeyPress;
        }

        private void InitializeToolTips()
        {
            toolTip = new ToolTip();
            toolTip.AutoPopDelay = 2000;
            toolTip.InitialDelay = 500;
            toolTip.ReshowDelay = 100;
            toolTip.ShowAlways = true;

            // Add tooltips to the icons
            toolTip.SetToolTip(picCustomerProfile, "User Profile");
            toolTip.SetToolTip(picCartIcon, "Shopping Cart");
            toolTip.SetToolTip(picShippingIcon, "Shipping Information");
        }

        private void InitializeCartPanel()
        {
            // Create cart panel
            cartPanel = new Panel
            {
                Width = 300,
                Height = 400,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Visible = false,
                Location = new Point(this.Width - 320, 50)
            };

            // Create cart items panel
            cartItemsPanel = new FlowLayoutPanel
            {
                Width = 280,
                Height = 300,
                AutoScroll = true,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                Location = new Point(10, 10)
            };

            // Create buy button
            var buyButton = new Button
            {
                Text = "Buy Now",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                BackColor = Color.Red,
                ForeColor = Color.White,
                Width = 280,
                Height = 40,
                Location = new Point(10, 320),
                FlatStyle = FlatStyle.Flat
            };
            buyButton.FlatAppearance.BorderSize = 0;
            buyButton.Click += BuyButton_Click;

            // Add controls to cart panel
            cartPanel.Controls.Add(cartItemsPanel);
            cartPanel.Controls.Add(buyButton);
            this.Controls.Add(cartPanel);
        }

        private void BuyButton_Click(object sender, EventArgs e)
        {
            if (cartItems.Count == 0)
            {
                MessageBox.Show("Your cart is empty!", "Cart Empty", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            var orderForm = new Order.OrderFRM(cartItems.Select(ci => new Order.OrderItem
            {
                Prod_ID = ci.Prod_ID,
                Prod_Name = ci.Prod_Name,
                Price = ci.Price,
                Quantity = ci.Quantity
            }).ToList(), cusId);
            ToggleCart();
            this.Hide();
            orderForm.ShowDialog();
            this.Show();
        }

        private void ToggleCart()
        {
            isCartVisible = !isCartVisible;
            cartPanel.Visible = isCartVisible;
            if (isCartVisible)
                cartPanel.BringToFront();
            UpdateCartDisplay();
        }

        private void UpdateCartDisplay()
        {
            cartItemsPanel.Controls.Clear();
            foreach (var item in cartItems)
            {
                var itemPanel = new Panel
                {
                    Width = 260,
                    Height = 60,
                    Margin = new Padding(0, 0, 0, 10),
                    BackColor = Color.WhiteSmoke
                };
                var lblName = new Label
                {
                    Text = item.Name,
                    Font = new Font("Segoe UI", 10, FontStyle.Bold),
                    Location = new Point(10, 5),
                    Width = 120
                };
                var lblPrice = new Label
                {
                    Text = $"${item.Price:F2}",
                    Font = new Font("Segoe UI", 10),
                    Location = new Point(140, 5),
                    ForeColor = Color.DodgerBlue
                };
                var lblQuantity = new Label
                {
                    Text = $"Qty: {item.Quantity}",
                    Font = new Font("Segoe UI", 10),
                    Location = new Point(140, 25),
                    Width = 60
                };
                var btnRemove = new Button
                {
                    Text = "\u00d7",
                    Font = new Font("Segoe UI", 12, FontStyle.Bold),
                    ForeColor = Color.Red,
                    Location = new Point(220, 15),
                    Size = new Size(30, 30),
                    FlatStyle = FlatStyle.Flat
                };
                btnRemove.FlatAppearance.BorderSize = 0;
                btnRemove.Click += (s, e) => RemoveFromCart(item);
                itemPanel.Controls.AddRange(new Control[] { lblName, lblPrice, lblQuantity, btnRemove });
                cartItemsPanel.Controls.Add(itemPanel);
            }
        }

        private void RemoveFromCart(CartItem item)
        {
            cartItems.Remove(item);
            UpdateCartDisplay();
            UpdateCartCount();
        }

        private void UpdateCartCount()
        {
            lblCartCount.Text = cartItems.Count > 0 ? cartItems.Count.ToString() : "";
        }

        private void LoadItems()
        {
            LoadItemsWithFilter("");
        }

        private void LoadItemsWithFilter(string searchTerm)
        {
            try
            {
                // Clear existing items
                itemListPanel.Controls.Clear();

                // Fetch products from database
                List<Products> products = dalProducts.GetAllProducts();

                if (products.Count == 0)
                {
                    MessageBox.Show("No products found in the database.", "No Products", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Filter products based on search term
                var filteredProducts = products;
                if (!string.IsNullOrWhiteSpace(searchTerm))
                {
                    filteredProducts = products.Where(p =>
                        p.Prod_Name.ToLower().Contains(searchTerm.ToLower())).ToList();
                }

                if (filteredProducts.Count == 0)
                {
                    var lblNoResults = new Label
                    {
                        Text = $"No products found matching '{searchTerm}'",
                        Font = new Font("Segoe UI", 12, FontStyle.Italic),
                        ForeColor = Color.Gray,
                        AutoSize = true,
                        Location = new Point(20, 20)
                    };
                    itemListPanel.Controls.Add(lblNoResults);
                    return;
                }

                foreach (var product in filteredProducts)
                {
                    var card = CreateProductCard(product);
                    itemListPanel.Controls.Add(card);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading products: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Panel CreateProductCard(Products product)
        {
            var card = new Panel
            {
                Width = 160,
                Height = 220,
                Margin = new Padding(12),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Cursor = Cursors.Hand
            };

            // Get product image info
            var (isExisted, imagePath) = dalProducts.GetProductImageInfo(product.Prod_ID);
            Image productImage = SystemIcons.Information.ToBitmap();
            if (isExisted && !string.IsNullOrEmpty(imagePath) && System.IO.File.Exists(imagePath))
            {
                try
                {
                    productImage = Image.FromFile(imagePath);
                }
                catch
                {
                    productImage = SystemIcons.Information.ToBitmap();
                }
            }

            var pic = new PictureBox
            {
                Image = productImage,
                SizeMode = PictureBoxSizeMode.StretchImage,
                Width = 120,
                Height = 100,
                Top = 12,
                Left = 20
            };

            var lblName = new Label
            {
                Text = product.Prod_Name,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                AutoSize = false,
                Width = 150,
                Height = 40, // Approx. two lines for this font size
                Top = pic.Bottom + 5,
                Left = 5,
                TextAlign = ContentAlignment.MiddleCenter,
                AutoEllipsis = true
            };

            // Add lblName to card first so we can use its Bottom property
            card.Controls.Add(pic);
            card.Controls.Add(lblName);

            var lblPrice = new Label
            {
                Text = $"${product.Prod_Price:F2}",
                Font = new Font("Segoe UI", 11, FontStyle.Regular),
                ForeColor = Color.DodgerBlue,
                AutoSize = false,
                Width = 150,
                Height = 24,
                Left = 5,
                TextAlign = ContentAlignment.MiddleCenter
            };
            // Dynamically position lblPrice below lblName
            lblPrice.Top = lblName.Bottom + 5;

            card.Controls.Add(lblPrice);

            // Store product data for click events
            card.Tag = product;

            card.Click += (s, e) => ShowItemDetail(product.Prod_Name, product.Prod_Price, productImage);
            pic.Click += (s, e) => ShowItemDetail(product.Prod_Name, product.Prod_Price, productImage);
            lblName.Click += (s, e) => ShowItemDetail(product.Prod_Name, product.Prod_Price, productImage);
            lblPrice.Click += (s, e) => ShowItemDetail(product.Prod_Name, product.Prod_Price, productImage);

            return card;
        }

        private void ShowItemDetail(string name, decimal price, Image img)
        {
            using (var dlg = new ItemDetailDialog(name, price, img, AddToCart))
            {
                dlg.ShowDialog();
            }
        }

        private void AddToCart(string name, decimal price, int quantity)
        {
            var products = dalProducts.GetAllProducts();
            var product = products.FirstOrDefault(p => p.Prod_Name == name);
            if (product != null)
            {
                var existing = cartItems.FirstOrDefault(ci => ci.Prod_ID == product.Prod_ID);
                if (existing != null)
                {
                    existing.Quantity += quantity;
                }
                else
                {
                    cartItems.Add(new CartItem
                    {
                        Prod_ID = product.Prod_ID,
                        Prod_Name = product.Prod_Name,
                        Name = name,
                        Price = price,
                        Quantity = quantity
                    });
                }
                UpdateCartDisplay();
                UpdateCartCount();
                MessageBox.Show($"Added {quantity} x {name} to cart!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Product not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void picCartIcon_Click(object sender, EventArgs e)
        {
            if (cartItems.Count == 0)
            {
                MessageBox.Show("Your cart is empty!", "Cart Empty", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            ToggleCart();
        }

        private void PicShippingIcon_Click(object sender, EventArgs e)
        {
            Order.ShippingFRM.ShowForCustomerOrders(cusId);
            this.Hide();
            this.Show();
        }

        private void BtnPastOrders_Click(object sender, EventArgs e)
        {
            var dlg = new Order.PastOrderDialog(cusId);
            dlg.ShowDialog();
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            string searchTerm = txtSearch.Text.Trim();
            LoadItemsWithFilter(searchTerm);
        }

        private void TxtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow search on Enter key
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true; // Prevent the beep sound
                BtnSearch_Click(sender, e);
            }
        }

        private void UserFRM_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
            Application.Exit(); // Ensure application exits when user form is closed
        }

        private void picCustomerProfile_Click(object sender, EventArgs e)
        {
            var profileForm = new Profile_Icons.Profile_CustomerFRM(cusId);
            profileForm.ShowDialog();
        }

        public void ClearCart()
        {
            cartItems.Clear();
            UpdateCartDisplay();
            UpdateCartCount();
        }
    }

    public class CartItem
    {
        public int Prod_ID { get; set; }
        public string Prod_Name { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
