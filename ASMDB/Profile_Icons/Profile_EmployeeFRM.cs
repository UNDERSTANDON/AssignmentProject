using ASMDB.Models;
using Connection;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace ASMDB.Profile_Icons
{
    public partial class Profile_EmployeeFRM : Form
    {
        private readonly int employeeId;
        private DAL_EmployeeProfile dalProfile;
        private int? selectedImageId = null;
        private string selectedImagePath = null;
        private string EmployeeImagePath = @"C:\ASMDB\Employee_Image";

        public Profile_EmployeeFRM(int employeeId)
        {
            InitializeComponent();
            this.employeeId = employeeId;
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["ASMDBConnectionString"].ConnectionString;
            dalProfile = new DAL_EmployeeProfile(connStr);
            this.Load += Profile_EmployeeFRM_Load;
            btnChangeImage.Click += BtnChangeImage_Click;
            btnSave.Click += BtnSave_Click;
            btnCancel.Click += (s, e) => this.Close();
        }

        private void Profile_EmployeeFRM_Load(object sender, EventArgs e)
        {
            LoadProfile();
        }

        private void LoadProfile()
        {
            var (employee, imageId, imagePath) = dalProfile.GetEmployeeProfile(employeeId);
            if (employee == null)
            {
                MessageBox.Show("Employee not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
            txtEmployeeName.Text = employee.Employee_Name;
            txtPosition.Text = employee.Position;
            lblRole.Text = employee.Role_ID.ToString();
            if (Controls.ContainsKey("txtPhone"))
                Controls["txtPhone"].Text = employee.Phone;
            if (Controls.ContainsKey("txtAddress"))
                Controls["txtAddress"].Text = employee.Address;
            if (Controls.ContainsKey("txtEmail"))
                Controls["txtEmail"].Text = employee.Email;
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

        private string CopyImageToEmployeeImgFolder(string sourcePath, int employeeId)
        {
            string ext = Path.GetExtension(sourcePath);
            string destDir = EmployeeImagePath;
            if (!Directory.Exists(destDir))
                Directory.CreateDirectory(destDir);
            string destFileName = $"{employeeId}{ext}";
            string destPath = Path.Combine(destDir, destFileName);

            if (picProfile.Image != null && selectedImagePath != null &&
                Path.GetFullPath(selectedImagePath) == Path.GetFullPath(destPath))
            {
                picProfile.Image.Dispose();
                picProfile.Image = null;
            }

            if (!Path.GetFullPath(sourcePath).Equals(Path.GetFullPath(destPath), StringComparison.OrdinalIgnoreCase))
            {
                File.Copy(sourcePath, destPath, true);
            }

            using (var stream = new FileStream(destPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (var img = Image.FromStream(stream))
            {
                picProfile.Image = new Bitmap(img);
            }

            return destPath;
        }

        private void BtnChangeImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files|*.png;*.jpg;*.jpeg;*.bmp";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    selectedImagePath = CopyImageToEmployeeImgFolder(ofd.FileName, employeeId);
                    selectedImageId = employeeId;
                }
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            var employee = new Employees
            {
                Employee_ID = employeeId,
                Employee_Name = txtEmployeeName.Text.Trim(),
                Position = txtPosition.Text.Trim(),
                Phone = Controls.ContainsKey("txtPhone") ? Controls["txtPhone"].Text : null,
                Address = Controls.ContainsKey("txtAddress") ? Controls["txtAddress"].Text : null,
                Email = Controls.ContainsKey("txtEmail") ? Controls["txtEmail"].Text : null,
                Role_ID = int.Parse(lblRole.Text)
            };
            bool infoOk = dalProfile.UpdateEmployeeInfo(employee);
            bool imgOk = true;
            if (selectedImageId.HasValue && !string.IsNullOrEmpty(selectedImagePath))
            {
                imgOk = dalProfile.SetEmployeeProfileImage(employeeId, employeeId, selectedImagePath);
            }
            if (infoOk && imgOk)
                MessageBox.Show("Profile updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Failed to update profile.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
