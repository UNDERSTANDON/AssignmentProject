using System;
using System.Windows.Forms;

namespace ASMDB.Admin
{
    public partial class CustomerDetailsDialog : Form
    {
        private string customerImagePath;

        public string CustomerName => txtName.Text;
        public string Phone => txtPhone.Text;
        public string Address => txtAddress.Text;
        public string Email => txtEmail.Text;
        public string CustomerImagePath => customerImagePath;

        public CustomerDetailsDialog(string name, string phone, string address, string email, string imagePath = null)
        {
            InitializeComponent();
            txtName.Text = name;
            txtPhone.Text = phone;
            txtAddress.Text = address;
            txtEmail.Text = email;
            btnSelectImage.Click += BtnSelectImage_Click;
            if (!string.IsNullOrEmpty(imagePath) && System.IO.File.Exists(imagePath))
            {
                pictureBoxCustomer.Image = System.Drawing.Image.FromFile(imagePath);
                customerImagePath = imagePath;
            }
        }

        public CustomerDetailsDialog() : this("", "", "", "", null) { }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text) || string.IsNullOrWhiteSpace(txtPhone.Text))
            {
                MessageBox.Show("Name and Phone are required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void BtnSelectImage_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog())
            {
                ofd.Title = "Select Customer Image";
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string destDir = @"C:\ASMDB\Customer_Image";
                        if (!System.IO.Directory.Exists(destDir))
                            System.IO.Directory.CreateDirectory(destDir);
                        string fileName = System.IO.Path.GetFileName(ofd.FileName);
                        string destPath = System.IO.Path.Combine(destDir, fileName);
                        int count = 1;
                        string nameOnly = System.IO.Path.GetFileNameWithoutExtension(fileName);
                        string ext = System.IO.Path.GetExtension(fileName);
                        while (System.IO.File.Exists(destPath))
                        {
                            destPath = System.IO.Path.Combine(destDir, $"{nameOnly}_{count}{ext}");
                            count++;
                        }
                        System.IO.File.Copy(ofd.FileName, destPath);
                        pictureBoxCustomer.Image = System.Drawing.Image.FromFile(destPath);
                        customerImagePath = destPath;
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