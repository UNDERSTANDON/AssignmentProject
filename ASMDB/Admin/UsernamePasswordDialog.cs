using System;
using System.Windows.Forms;

namespace ASMDB.Admin
{
    public partial class UsernamePasswordDialog : Form
    {
        public string Username => txtUsername.Text;
        public string Password => txtPassword.Text;

        public UsernamePasswordDialog()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
