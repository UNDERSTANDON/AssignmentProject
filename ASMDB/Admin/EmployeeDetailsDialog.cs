using System;
using System.Windows.Forms;

namespace ASMDB.Admin
{
    public partial class EmployeeDetailsDialog : Form
    {
        private string employeeImagePath;

        public string EmployeeName => txtName.Text;
        public string Position => txtPosition.Text;
        public string Phone => txtPhone.Text;
        public string Address => txtAddress.Text;
        public string Email => txtEmail.Text;
        public string Role => cmbRole.SelectedItem?.ToString() ?? "";
        public string EmployeeImagePath => employeeImagePath;

        public EmployeeDetailsDialog(string name = "", string position = "", string phone = "", string address = "", string email = "", string role = "", string imagePath = null)
        {
            InitializeComponent();
            txtName.Text = name;
            txtPosition.Text = position;
            txtPhone.Text = phone;
            txtAddress.Text = address;
            txtEmail.Text = email;
            cmbRole.Items.AddRange(new object[] { "Admin", "Sales", "Warehouse" });
            if (!string.IsNullOrEmpty(role))
                cmbRole.SelectedItem = role;
            btnSelectImage.Click += BtnSelectImage_Click;
            if (!string.IsNullOrEmpty(imagePath) && System.IO.File.Exists(imagePath))
            {
                pictureBoxEmployee.Image = System.Drawing.Image.FromFile(imagePath);
                employeeImagePath = imagePath;
            }
        }

        public EmployeeDetailsDialog() : this("", "", "", "", "", "", null) { }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text) || string.IsNullOrWhiteSpace(txtPosition.Text) || string.IsNullOrWhiteSpace(txtPhone.Text) || string.IsNullOrWhiteSpace(txtAddress.Text) || string.IsNullOrWhiteSpace(txtEmail.Text) || cmbRole.SelectedIndex == -1)
            {
                MessageBox.Show("All fields are required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                ofd.Title = "Select Employee Image";
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string destDir = @"C:\ASMDB\Employee_Image";
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
                        pictureBoxEmployee.Image = System.Drawing.Image.FromFile(destPath);
                        employeeImagePath = destPath;
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