using BusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.International
{
    public partial class ctrlDriverInternationalLicenseInfo : UserControl
    {
        private int _InternationalLicenseID = -1;
        private clsBLInternationalLicense _InternationalLicense;

        public int InternationalLicenseID
        {
            get { return _InternationalLicenseID; }
        }
        public ctrlDriverInternationalLicenseInfo()
        {
            InitializeComponent();
        }
        public void LoadInfo(int InternationalLicenseID)
        {
            _InternationalLicenseID = InternationalLicenseID;
            _InternationalLicense = clsBLInternationalLicense.Find(_InternationalLicenseID);

            if (_InternationalLicense == null)
            {
                MessageBox.Show("Could not find International License ID = " + InternationalLicenseID.ToString(),
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _ResetInternationalLicenseInfo();
                return;
            }

            // تعبئة البيانات في اللابلز
            lblInternationalLicenseID.Text = _InternationalLicense.InternationalLicenseID.ToString();
            lblApplicationID.Text = _InternationalLicense.ApplicationID.ToString();
            lblLocalLicenseID.Text = _InternationalLicense.IssuedUsingLocalLicenseID.ToString();
            lblIsActive.Text = _InternationalLicense.IsActive ? "Yes" : "No";

            // جلب بيانات السائق والشخص
            lblName.Text = _InternationalLicense.DriverInfo.PersonInfo.FullName;
            lblNationalNo.Text = _InternationalLicense.DriverInfo.PersonInfo.NationalNo;
            lblGendor.Text = _InternationalLicense.DriverInfo.PersonInfo.Gendor == 0 ? "Male" : "Female";
            lblDateOfBirth.Text = _InternationalLicense.DriverInfo.PersonInfo.DateOfBirth.ToShortDateString();
            lblDriverID.Text = _InternationalLicense.DriverID.ToString();
            lblIssueDate.Text = _InternationalLicense.IssueDate.ToShortDateString();
            lblExpirationDate.Text = _InternationalLicense.ExpirationDate.ToShortDateString();

            _LoadPersonImage();
        }

        private void _LoadPersonImage()
        {
            if (_InternationalLicense.DriverInfo.PersonInfo.Gendor == 0)
                pbPersonImage.Image = Properties.Resources.Male_512;
            else
                pbPersonImage.Image = Properties.Resources.Cars_48;

            string imagePath = _InternationalLicense.DriverInfo.PersonInfo.ImagePath;
            if (imagePath != "" && System.IO.File.Exists(imagePath))
            {
                pbPersonImage.ImageLocation = imagePath;
            }
        }

        private void _ResetInternationalLicenseInfo()
        {
            lblInternationalLicenseID.Text = "[???]";
            lblApplicationID.Text = "[???]";
            lblLocalLicenseID.Text = "[???]";
            lblIsActive.Text = "[???]";
            lblName.Text = "[???]";
            lblNationalNo.Text = "[???]";
            lblGendor.Text = "[???]";
            lblDateOfBirth.Text = "[???]";
            lblDriverID.Text = "[???]";
            lblIssueDate.Text = "[???]";
            lblExpirationDate.Text = "[???]";
            pbPersonImage.Image = Properties.Resources.Male_512;
        }
        private void ctrlDriverInternationalLicenseInfo_Load(object sender, EventArgs e)
        {

        }
    }
}
