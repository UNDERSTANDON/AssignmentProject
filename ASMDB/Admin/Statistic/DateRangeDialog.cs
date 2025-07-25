using System;
using System.Windows.Forms;

namespace ASMDB.Admin.Statistic
{
    public class DateRangeDialog : Form
    {
        public DateTime? StartDate => dtpStart.Value.Date;
        public DateTime? EndDate => dtpEnd.Value.Date;

        private DateTimePicker dtpStart;
        private DateTimePicker dtpEnd;
        private Button btnOK;
        private Button btnCancel;

        public DateRangeDialog()
        {
            this.Text = "Select Date Range";
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Width = 300;
            this.Height = 180;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            dtpStart = new DateTimePicker { Left = 30, Top = 20, Width = 220, Format = DateTimePickerFormat.Short };
            dtpEnd = new DateTimePicker { Left = 30, Top = 60, Width = 220, Format = DateTimePickerFormat.Short };
            btnOK = new Button { Text = "OK", Left = 50, Width = 80, Top = 100, DialogResult = DialogResult.OK };
            btnCancel = new Button { Text = "Cancel", Left = 150, Width = 80, Top = 100, DialogResult = DialogResult.Cancel };

            this.Controls.Add(dtpStart);
            this.Controls.Add(dtpEnd);
            this.Controls.Add(btnOK);
            this.Controls.Add(btnCancel);

            this.AcceptButton = btnOK;
            this.CancelButton = btnCancel;
        }
    }
}
