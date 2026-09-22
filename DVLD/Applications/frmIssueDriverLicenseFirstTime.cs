using BuisnessLayer;
using DVLD_BLL;
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
    public partial class frmIssueDriverLicenseFirstTime : Form
    {
        private int _LocalDrivingLicenseApplicationID = -1;
        private clsBLLocalDrivingLicenseApplication _LocalDrivingLicenseApplication;
        public frmIssueDriverLicenseFirstTime(int LocalDrivingLicenseApplicationID)
        {
            InitializeComponent();
            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmIssueDriverLicenseFirstTime_Load(object sender, EventArgs e)
        {
            ctrlLocalDrivingLicenseApplicationInfo1.LoadApplicationInfoByLocalDrivingAppID(_LocalDrivingLicenseApplicationID);

            _LocalDrivingLicenseApplication = clsBLLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID(_LocalDrivingLicenseApplicationID);

            if (_LocalDrivingLicenseApplication == null)
            {
                MessageBox.Show("No Application Found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            if (_LocalDrivingLicenseApplication.GetPassedTestCount() < 3)
            {
                MessageBox.Show("Person Should Pass All Tests First!", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnIssueLicense.Enabled = false;
                return;
            }

            int activeLicenseID = _LocalDrivingLicenseApplication.GetActiveLicenseID();
            if (activeLicenseID != -1)
            {
                MessageBox.Show("Person Already Has a License For This Application!", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnIssueLicense.Enabled = false;
            }
        }

        private void btnIssueLicense_Click(object sender, EventArgs e)
        {
            clsBLLicense newLicense = new clsBLLicense();
            int licenseID = newLicense.IssueLicenseFirstTime(_LocalDrivingLicenseApplicationID, txtNotes.Text.Trim());

            if (licenseID != -1)
            {
                MessageBox.Show($"License Issued Successfully with ID = {licenseID}", "Succeeded", MessageBoxButtons.OK, MessageBoxIcon.Information);

                btnIssueLicense.Enabled = false;
                txtNotes.Enabled = false;

                frmShowLicenseInfo frm = new frmShowLicenseInfo(licenseID);
                frm.ShowDialog();

                this.Close();
            }
            else
            {
                MessageBox.Show("License Could Not Be Issued!", "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
