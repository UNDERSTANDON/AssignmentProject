using ASMDB.Connection;
using ASMDB.Models;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ASMDB.Employee
{
    public partial class SalesmanUpdateDialog : Form
    {
        private DAL_Salesman dal_Salesman;

        public string ProdName { get; private set; }
        public decimal ProdPrice { get; private set; }
        public int Category_ID { get; private set; }
        public string ProductImagePath { get; private set; }

        public SalesmanUpdateDialog(int prodId, string prodName, decimal prodPrice, string category, string status, string markDown, DAL_Salesman dal, string imagePath = null)
        {
            dal_Salesman = dal;
            InitializeComponent();
            this.Text = $"Update Product #{prodId}";
            txtName.Text = prodName;
            txtPrice.Text = prodPrice.ToString();
            LoadCategories(category);
            btnOK.Click += BtnOK_Click;
            btnCancel.Click += (s, e) => this.Close();
            btnSelectImage.Click += BtnSelectImage_Click;
            if (!string.IsNullOrEmpty(imagePath) && System.IO.File.Exists(imagePath))
            {
                pictureBoxProduct.Image = Image.FromFile(imagePath);
                ProductImagePath = imagePath;
            }
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text) || !decimal.TryParse(txtPrice.Text, out decimal price))
            {
                MessageBox.Show("Please enter valid product name and price.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.DialogResult = DialogResult.None;
                return;
            }
            if (cmbCategory.SelectedItem == null)
            {
                MessageBox.Show("Please select a category.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.DialogResult = DialogResult.None;
                return;
            }
            ProdName = txtName.Text;
            ProdPrice = price;
            Category_ID = ((Category)cmbCategory.SelectedItem).Category_ID;
            this.Close();
        }

        private void LoadCategories(string currentCategory)
        {
            try
            {
                var categories = dal_Salesman.GetAllCategories();
                cmbCategory.Items.AddRange(categories.ToArray());
                cmbCategory.DisplayMember = "Category_Name";
                for (int i = 0; i < cmbCategory.Items.Count; i++)
                {
                    var cat = (Category)cmbCategory.Items[i];
                    if (cat.Category_Name == currentCategory)
                    {
                        cmbCategory.SelectedIndex = i;
                        break;
                    }
                }
                if (cmbCategory.SelectedIndex == -1 && cmbCategory.Items.Count > 0)
                {
                    cmbCategory.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading categories: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSelectImage_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog())
            {
                ofd.Title = "Select Product Image";
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string destDir = @"C:\ASMDB\Product_Image";
                        if (!System.IO.Directory.Exists(destDir))
                            System.IO.Directory.CreateDirectory(destDir);
                        string fileName = System.IO.Path.GetFileName(ofd.FileName);
                        string destPath = System.IO.Path.Combine(destDir, fileName);
                        // If file exists, generate a unique name
                        int count = 1;
                        string nameOnly = System.IO.Path.GetFileNameWithoutExtension(fileName);
                        string ext = System.IO.Path.GetExtension(fileName);
                        while (System.IO.File.Exists(destPath))
                        {
                            destPath = System.IO.Path.Combine(destDir, $"{nameOnly}_{count}{ext}");
                            count++;
                        }
                        System.IO.File.Copy(ofd.FileName, destPath);
                        pictureBoxProduct.Image = Image.FromFile(destPath);
                        ProductImagePath = destPath;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Failed to copy image: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}