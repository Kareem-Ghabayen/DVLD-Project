using BuisnessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Applications.Replacement
{
    public partial class frmReplaceLostOrDamagedLicenseApplication : Form
    {
        private int _NewLicenseID = -1;

        private clsBLLicense.enIssueReason _GetIssueReason()
        {
            return rbDamagedLicense.Checked ?
                clsBLLicense.enIssueReason.DamagedReplacement :
                clsBLLicense.enIssueReason.LostReplacement;
        }

        private clsBLApplication.enApplicationType _GetApplicationType()
        {
            return rbDamagedLicense.Checked ?
                clsBLApplication.enApplicationType.ReplacementForDamaged :
                clsBLApplication.enApplicationType.ReplacementForLost;
        }
        public frmReplaceLostOrDamagedLicenseApplication()
        {
            InitializeComponent();
        }

        private void frmReplaceLostOrDamagedLicenseApplication_Load(object sender, EventArgs e)
        {
            rbDamagedLicense.Parent = gbReplacementFor;
            rbLostLicense.Parent = gbReplacementFor;
            ctrlDriverLicenseInfoWithFilter1.OnLicenseSelected += ctrlDriverLicenseInfoWithFilter1_OnLicenseSelected;
            lblApplicationDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName;

            rbDamagedLicense.Checked = true;
        }

        private void rbDamagedLicense_CheckedChanged(object sender, EventArgs e)
        {

            lblTitle.Text = "Replacement for Damaged License";
            this.Text = lblTitle.Text;
            lblApplicationFees.Text = clsBLApplicationType.Find((int)_GetApplicationType()).ApplicationFees.ToString();
        }

        private void rbLostLicense_CheckedChanged(object sender, EventArgs e)
        {
            lblTitle.Text = "Replacement for Lost License";
            this.Text = lblTitle.Text;
            lblApplicationFees.Text = clsBLApplicationType.Find((int)_GetApplicationType()).ApplicationFees.ToString();
        }
        private void ctrlDriverLicenseInfoWithFilter1_OnLicenseSelected(int obj)
        {
            int selectedLicenseID = obj;
            lblOldLicenseID.Text = selectedLicenseID.ToString();

            if (selectedLicenseID == -1)
            {
                btnIssueReplacement.Enabled = false;
                llShowLicensesHistory.Enabled = false;
                return;
            }

            llShowLicensesHistory.Enabled = true;

            if (!ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.IsActive)
            {
                MessageBox.Show("Selected License is NOT Active, choose an active license.", "Not allowed",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnIssueReplacement.Enabled = false;
                return;
            }

            btnIssueReplacement.Enabled = true;
        }

        private void btnIssueReplacement_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to Issue a Replacement for the license?", "Confirm",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            int selectedLicenseID = ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.LicenseID;
            clsBLLicense newLicense = null;

            if (rbDamagedLicense.Checked)
            {
                newLicense = clsBLApplication.ReplaceDamagedDrivingLicense(selectedLicenseID);
            }
            else
            {
                newLicense = clsBLApplication.ReplaceLostDrivingLicense(selectedLicenseID);
            }

            if (newLicense == null)
            {
                MessageBox.Show("Failed to Issue Replacement License!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _NewLicenseID = newLicense.LicenseID;

            lblLRApplicationID.Text = newLicense.ApplicationID.ToString();
            lblReplacedLicenseID.Text = _NewLicenseID.ToString();

            MessageBox.Show($"Replacement License Issued Successfully with ID = {_NewLicenseID}", "License Issued",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            btnIssueReplacement.Enabled = false;
            gbReplacementFor.Enabled = false;
            ctrlDriverLicenseInfoWithFilter1.FilterEnabled = false;
            llShowLicensesInfo.Enabled = true;
        }

        private void llShowLicensesHistory_Click(object sender, EventArgs e)
        {
            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(
                ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.DriverInfo.PersonID);
            frm.ShowDialog();
        }

        private void llShowLicensesInfo_Click(object sender, EventArgs e)
        {
            frmShowLicenseInfo frm = new frmShowLicenseInfo(_NewLicenseID);
            frm.ShowDialog();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
