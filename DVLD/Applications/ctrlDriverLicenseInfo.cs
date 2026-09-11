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

namespace DVLD.Applications
{
    public partial class ctrlDriverLicenseInfo : UserControl
    {
        private clsBLLicense _License;
        private int _LicenseID = -1;
        public int LicenseID => _LicenseID;
        public clsBLLicense SelectedLicenseInfo => _License;
        public void LoadInfo(int LicenseID)
        {
            _LicenseID = LicenseID;
            _License = clsBLLicense.FindByLicenseID(_LicenseID);

            if (_License == null)
            {
                MessageBox.Show($"Could not find License ID = {LicenseID}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _ResetDefaultValues();
                return;
            }
            lblClass.Text = _License.LicenseClassInfo.ClassName;
            lblName.Text = _License.DriverInfo.PersonInfo.FullName;
            lblLicenseID.Text = _License.LicenseID.ToString();
            lblNationalNo.Text = _License.DriverInfo.PersonInfo.NationalNo;
            lblGendor.Text = (_License.DriverInfo.PersonInfo.Gendor == 0) ? "Male" : "Female";
            lblIssueDate.Text = _License.IssueDate.ToString("dd/MMM/yyyy");
            lblIssueReason.Text = _License.IssueReasonText; // أو _License.IssueReason.ToString() حسب الكلاس عندك
            lblNotes.Text = string.IsNullOrWhiteSpace(_License.Notes) ? "No Notes" : _License.Notes;
            lblIsActive.Text = _License.IsActive ? "Yes" : "No";
            lblDateOfBirth.Text = _License.DriverInfo.PersonInfo.DateOfBirth.ToString("dd/MMM/yyyy");
            lblDriverID.Text = _License.DriverID.ToString();
            lblExpirationDate.Text = _License.ExpirationDate.ToString("dd/MMM/yyyy");
            lblIsDetained.Text = _License.IsDetained ? "Yes" : "No";

            _LoadPersonImage();
        }

        private void _LoadPersonImage()
        {
            if (_License.DriverInfo.PersonInfo.Gendor == 0)
                pbPersonImage.Image = Properties.Resources.Male_512;
            else
                pbPersonImage.Image = Properties.Resources.Cars_48;

            string imagePath = _License.DriverInfo.PersonInfo.ImagePath;
            if (!string.IsNullOrEmpty(imagePath) && System.IO.File.Exists(imagePath))
            {
                pbPersonImage.Load(imagePath);
            }
        }

        private void _ResetDefaultValues()
        {
            lblClass.Text = "[???]";
            lblName.Text = "[???]";
            lblLicenseID.Text = "[???]";
            lblNationalNo.Text = "[???]";
            lblGendor.Text = "[???]";
            lblIssueDate.Text = "[???]";
            lblIssueReason.Text = "[???]";
            lblNotes.Text = "[???]";
            lblIsActive.Text = "[???]";
            lblDateOfBirth.Text = "[???]";
            lblDriverID.Text = "[???]";
            lblExpirationDate.Text = "[???]";
            lblIsDetained.Text = "[???]";
            pbPersonImage.Image = Properties.Resources.Male_512;
        }
        public ctrlDriverLicenseInfo()
        {
            InitializeComponent();
        }

        private void gbDriverLicenseInfo_Click(object sender, EventArgs e)
        {

        }
    }
}
