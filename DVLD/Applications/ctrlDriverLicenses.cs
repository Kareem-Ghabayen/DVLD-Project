using BuisnessLayer;
using BusinessLayer;
using DVLD.International;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Applications
{
    public partial class ctrlDriverLicenses : UserControl
    {
        private int _DriverID = -1;
        private clsBLDriver _Driver;
        private DataTable _dtDriverLocalLicensesHistory;
        private DataTable _dtDriverInternationalLicensesHistory;

        public int DriverID => _DriverID;
        public ctrlDriverLicenses()
        {
            InitializeComponent();
        }

        private void _LoadLocalLicenses()
        {
            _dtDriverLocalLicensesHistory = clsBLLicense.GetDriverLicenses(_DriverID);
            dgvLocalLicensesHistory.DataSource = _dtDriverLocalLicensesHistory;
            lblLocalLicensesRecords.Text = dgvLocalLicensesHistory.Rows.Count.ToString();


        }
        private void _LoadInternationalLicenses()
        {
            _dtDriverInternationalLicensesHistory = clsBLInternationalLicense.GetDriverInternationalLicenses(_DriverID);
            dgvInternationalLicensesHistory.DataSource = _dtDriverInternationalLicensesHistory;
            lblInternationalLicensesRecords.Text = dgvInternationalLicensesHistory.Rows.Count.ToString();

 
        }
        public void LoadInfo(int DriverID)
        {
            _DriverID = DriverID;
            _Driver = clsBLDriver.FindByDriverID(_DriverID);

            if (_Driver == null)
            {
                MessageBox.Show("No Driver with ID = " + _DriverID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Clear();
                return;
            }

            _LoadLocalLicenses();
            _LoadInternationalLicenses();
        }

        public void LoadInfoByPersonID(int PersonID)
        {
            _Driver = clsBLDriver.FindByPersonID(PersonID);

            if (_Driver == null)
            {
                MessageBox.Show("No Driver Found for Person ID = " + PersonID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Clear();
                return;
            }
            _DriverID = _Driver.DriverID;
            _LoadLocalLicenses();
            _LoadInternationalLicenses();
        }

        public void Clear()
        {
            _dtDriverLocalLicensesHistory?.Clear();
            _dtDriverInternationalLicensesHistory?.Clear();
            lblLocalLicensesRecords.Text = "0";
            lblInternationalLicensesRecords.Text = "0";

        }

        private void lblLocalLicensesRecords_Click(object sender, EventArgs e)
        {

        }

        private void gbDriverLicenses_Click(object sender, EventArgs e)
        {

        }

        private void showPersonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvLocalLicensesHistory.CurrentRow == null)
                return;

            int licenseID = (int)dgvLocalLicensesHistory.CurrentRow.Cells["LicenseID"].Value;

            frmShowLicenseInfo frm = new frmShowLicenseInfo(licenseID);
            frm.ShowDialog();
        }

        private void showInternationalLicenseInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvInternationalLicensesHistory.CurrentRow == null)
                return;

            int internationalLicenseID = (int)dgvInternationalLicensesHistory.CurrentRow.Cells["InternationalLicenseID"].Value;

            frmShowInternationalLicenseInfo frm = new frmShowInternationalLicenseInfo(internationalLicenseID);
            frm.ShowDialog();
        }
    }
}
