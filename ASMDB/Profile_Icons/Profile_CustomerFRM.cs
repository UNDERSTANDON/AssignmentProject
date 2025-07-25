using ASMDB.Models;
using Connection;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace ASMDB.Profile_Icons
{
    public partial class Profile_CustomerFRM : Form
    {
        private readonly int cusId;
        private DAL_CustomerProfile dalProfile;
        private int? selectedImageId = null;
        private string selectedImagePath = null;
        private string CustomerImagePath = @"C:\ASMDB\Customer_Image";

        public Profile_CustomerFRM(int cusId)
        {
            InitializeComponent();
            this.cusId = cusId;
            // Provide a connection string for DAL_CustomerProfile
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["ASMDBConnectionString"].ConnectionString;
            dalProfile = new DAL_CustomerProfile(connStr);
            this.Load += Profile_CustomerFRM_Load;
            btnChangeImage.Click += BtnChangeImage_Click;
            btnSave.Click += BtnSave_Click;
            btnDeleteAccount.Click += BtnDeleteAccount_Click;
            btnCancel.Click += (s, e) => this.Close();
        }

        private void Profile_CustomerFRM_Load(object sender, EventArgs e)
        {
            LoadProfile();
        }

        private void LoadProfile()
        {
            var (customer, imageId, imagePath) = dalProfile.GetCustomerProfile(cusId);
            if (customer == null)
            {
                MessageBox.Show("Customer not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
            txtName.Text = customer.Cus_Name;
            txtPhone.Text = customer.Phone.ToString();
            txtAddress.Text = customer.Address;
            txtEmail.Text = customer.Email;
            selectedImageId = imageId;
            LoadProfileImage(imagePath);
        }

        private void LoadProfileImage(string imagePath)
        {
            if (!string.IsNullOrEmpty(imagePath) && File.Exists(imagePath))
            {
                using (var stream = new FileStream(imagePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                using (var img = Image.FromStream(stream))
                {
                    picProfile.Image = new Bitmap(img);
                }
            }
            else
            {
                picProfile.Image = null;
            }
        }

        // Helper to copy image to storage and return the relative destination path
        private string CopyImageToCustomerImgFolder(string sourcePath, int cusId)
        {
            string ext = Path.GetExtension(sourcePath);
            string destDir = CustomerImagePath;
            if (!Directory.Exists(destDir))
                Directory.CreateDirectory(destDir);
            string destFileName = $"{cusId}{ext}";
            string destPath = Path.Combine(destDir, destFileName);

            // Only dispose if the PictureBox is displaying the destination image
            if (picProfile.Image != null && selectedImagePath != null &&
                Path.GetFullPath(selectedImagePath) == Path.GetFullPath(destPath))
            {
                picProfile.Image.Dispose();
                picProfile.Image = null;
            }

            // Do not copy if source and destination are the same file
            if (!Path.GetFullPath(sourcePath).Equals(Path.GetFullPath(destPath), StringComparison.OrdinalIgnoreCase))
            {
                File.Copy(sourcePath, destPath, true);
            }

            // After copying, reload the image into the PictureBox using the Bitmap copy pattern
            using (var stream = new FileStream(destPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (var img = Image.FromStream(stream))
            {
                picProfile.Image = new Bitmap(img);
            }

            // Return the absolute path for storage
            return destPath;
        }

        private void BtnChangeImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files|*.png;*.jpg;*.jpeg;*.bmp";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    selectedImagePath = CopyImageToCustomerImgFolder(ofd.FileName, cusId);
                    selectedImageId = cusId; // Use Cus_ID as the image ID
                }
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtPhone.Text.Trim(), out int phone))
            {
                MessageBox.Show("Invalid phone number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var customer = new Customers
            {
                Cus_ID = cusId,
                Cus_Name = txtName.Text.Trim(),
                Phone = phone.ToString(),
                Address = txtAddress.Text.Trim(),
                Email = txtEmail.Text.Trim()
            };
            bool infoOk = dalProfile.UpdateCustomerInfo(customer);
            bool imgOk = true;
            if (selectedImageId.HasValue && !string.IsNullOrEmpty(selectedImagePath))
            {
                // Always copy the image on save to ensure it exists in the destination
                string relativeDestPath = CopyImageToCustomerImgFolder(selectedImagePath, cusId);
                imgOk = dalProfile.SetCustomerProfileImage(cusId, cusId, relativeDestPath);
            }
            if (infoOk && imgOk)
                MessageBox.Show("Profile updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Failed to update profile.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void BtnDeleteAccount_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete your account? This cannot be undone.", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                if (dalProfile.DeleteCustomerAccount(cusId))
                {
                    MessageBox.Show("Account deleted.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Failed to delete account.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnLogOut_Click_1(object sender, EventArgs e)
        {
            foreach (Form frm in Application.OpenForms)
            {
                if (!(frm is ASMDB.Login.LoginFRM))
                    frm.Hide();
            }
            var loginForm = new ASMDB.Login.LoginFRM();
            loginForm.Show();
            this.Close();
        }
    }
}
