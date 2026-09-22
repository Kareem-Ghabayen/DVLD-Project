using DVLD.people;
using DVLD_BLL;
using System;
using System.Windows.Forms;

namespace DVLD.Applications.Controls
{
    public partial class ctrlLocalDrivingLicenseApplicationInfo : UserControl
    {
        private int _LocalDrivingLicenseApplicationID = -1;
        private clsBLLocalDrivingLicenseApplication _LocalDrivingLicenseApplication;
        private int _ApplicationID = -1;
        private int _LicenseID = -1;
        public int LocalDrivingLicenseApplicationID
        {
            get { return _LocalDrivingLicenseApplicationID; }
        }

        public clsBLLocalDrivingLicenseApplication SelectedLocalDrivingLicenseAppInfo
        {
            get { return _LocalDrivingLicenseApplication; }
        }
        public void ResetLocalDrivingLicenseApplicationInfo()
        {
            _LocalDrivingLicenseApplicationID = -1;
            _ApplicationID = -1;
            _LocalDrivingLicenseApplication = null;
            lblLocalDrivingLicenseAppID.Text = "[???]";
            lblAppliedForLicense.Text = "[???]";
            lblPassedTests.Text = "0/3";
            lblApplicationID.Text = "[???]";
            lblStatus.Text = "[???]";
            lblFees.Text = "[???]";
            lblType.Text = "[???]";
            lblApplicant.Text = "[???]";
            lblDate.Text = "[???]";
            lblStatusDate.Text = "[???]";
            lblCreatedBy.Text = "[???]";
            llViewPersonInfo.Enabled = false;
            llShowLicenseInfo.Enabled = false;
        }
        private void _FillLocalDrivingLicenseApplicationInfo()
        {
            _LicenseID = _LocalDrivingLicenseApplication.GetActiveLicenseID();
            _LocalDrivingLicenseApplicationID = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID;
            _ApplicationID = _LocalDrivingLicenseApplication.ApplicationID;

            lblLocalDrivingLicenseAppID.Text = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();
            lblAppliedForLicense.Text = _LocalDrivingLicenseApplication.LicenseClassInfo.ClassName;
            lblPassedTests.Text = _LocalDrivingLicenseApplication.GetPassedTestCount().ToString() + "/3";

            lblApplicationID.Text = _LocalDrivingLicenseApplication.ApplicationID.ToString();
            lblStatus.Text = _LocalDrivingLicenseApplication.BaseApplicationInfo.StatusText;
            lblFees.Text = _LocalDrivingLicenseApplication.BaseApplicationInfo.PaidFees.ToString();
            lblType.Text = _LocalDrivingLicenseApplication.BaseApplicationInfo.ApplicationTypeInfo.ApplicationTypeTitle;
            var person = _LocalDrivingLicenseApplication.BaseApplicationInfo.ApplicantPersonInfo;
            lblApplicant.Text = person.FirstName + " " + person.SecondName + " " + person.ThirdName + " " + person.LastName; lblDate.Text = _LocalDrivingLicenseApplication.BaseApplicationInfo.ApplicationDate.ToShortDateString();
            lblStatusDate.Text = _LocalDrivingLicenseApplication.BaseApplicationInfo.LastStatusDate.ToShortDateString();
            lblCreatedBy.Text = _LocalDrivingLicenseApplication.BaseApplicationInfo.CreatedByUserInfo.UserName;

            llViewPersonInfo.Enabled = true;
            llShowLicenseInfo.Enabled = (_LicenseID != -1);

        }
        public void LoadApplicationInfoByLocalDrivingAppID(int LocalDrivingLicenseApplicationID)
        {
            _LocalDrivingLicenseApplication = clsBLLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID(LocalDrivingLicenseApplicationID);

            if (_LocalDrivingLicenseApplication == null)
            {
                ResetLocalDrivingLicenseApplicationInfo();
                MessageBox.Show("No Application with ApplicationID = " + LocalDrivingLicenseApplicationID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _FillLocalDrivingLicenseApplicationInfo();
        }

        public void LoadApplicationInfoByApplicationID(int ApplicationID)
        {
            _LocalDrivingLicenseApplication = clsBLLocalDrivingLicenseApplication.FindByApplicationID(ApplicationID);

            if (_LocalDrivingLicenseApplication == null)
            {
                ResetLocalDrivingLicenseApplicationInfo();
                MessageBox.Show("No Application with ApplicationID = " + ApplicationID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _FillLocalDrivingLicenseApplicationInfo();
        }
        public ctrlLocalDrivingLicenseApplicationInfo()
        {
            InitializeComponent();
        }

        private void guna2HtmlLabel1_Click(object sender, EventArgs e)
        {
            int LicenseID = _LocalDrivingLicenseApplication.GetActiveLicenseID();


            if (LicenseID != -1)
            {
                frmShowLicenseInfo frm = new frmShowLicenseInfo(LicenseID);
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("No license found for this application!", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gbLocalDrivingLicenseApplicationInfo_Click(object sender, EventArgs e)
        {

        }

        private void llViewPersonInfo_Click(object sender, EventArgs e)
        {
            frmShowPersonInfo frm = new frmShowPersonInfo(_LocalDrivingLicenseApplication.BaseApplicationInfo.ApplicantPersonID);
            frm.ShowDialog();

            LoadApplicationInfoByLocalDrivingAppID(_LocalDrivingLicenseApplicationID);
        }

        private void gbApplicationBasicInfo_Click(object sender, EventArgs e)
        {

        }
    }
}