using BuisnessLayer;
using BusinessLayer;
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
    public partial class frmScheduleTest : Form
    {
        private int _LocalDrivingLicenseApplicationID = -1;
        private clsBLTestType.enTestType _TestTypeID = clsBLTestType.enTestType.VisionTest;
        private int _AppointmentID = -1;
        private int _RetakeTestApplicationID = -1;

        public frmScheduleTest(int LocalDrivingLicenseApplicationID, clsBLTestType.enTestType TestTypeID)
        {
            InitializeComponent();

            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            _TestTypeID = TestTypeID;
            _AppointmentID = -1;
        }

        public frmScheduleTest(int LocalDrivingLicenseApplicationID, clsBLTestType.enTestType TestTypeID, int AppointmentID)
        {
            InitializeComponent();

            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            _TestTypeID = TestTypeID;
            _AppointmentID = AppointmentID;
        }

        private void gbTestType_Click(object sender, EventArgs e)
        {

        }

        private void frmScheduleTest_Load(object sender, EventArgs e)
        {
            ctrlScheduledTest1.LoadInfo(_LocalDrivingLicenseApplicationID, _TestTypeID, _AppointmentID);
            _SetupRetakeTestUI();
            btnSave.Enabled = ctrlScheduledTest1.IsAppointmentValidForSave;
        }
        private void _SetupRetakeTestUI()
        {
            var localApp = clsBLLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID(_LocalDrivingLicenseApplicationID);

            if (localApp == null)
                return;

            byte totalTrials = localApp.TotalTrialsPerTest(_TestTypeID);

            if (_AppointmentID == -1 && totalTrials > 0)
            {
                gbRetakeTestInfo.Enabled = true;

                float retakeAppFees = clsBLApplicationType.Find((int)clsBLApplication.enApplicationType.RetakeTest).ApplicationFees;

                lblRAppFees.Text = retakeAppFees.ToString();
                lblTotalFees.Text = (ctrlScheduledTest1.TestFees + retakeAppFees).ToString();
                lblRTestAppID.Text = "N/A";
            }
            // حالة تعديل موعد قائم (Update Mode)
            else if (_AppointmentID != -1)
            {
                var appointment = clsBLTestAppointment.Find(_AppointmentID);

                if (appointment != null && appointment.RetakeTestApplicationID != -1)
                {
                    gbRetakeTestInfo.Enabled = true;
                    var retakeApp = clsBLApplication.Find(appointment.RetakeTestApplicationID);

                    lblRAppFees.Text = retakeApp != null ? retakeApp.PaidFees.ToString() : "0";
                    lblTotalFees.Text = (ctrlScheduledTest1.TestFees + (retakeApp != null ? retakeApp.PaidFees : 0)).ToString();
                    lblRTestAppID.Text = appointment.RetakeTestApplicationID.ToString();
                }
                else
                {
                    _DisableRetakeTestUI();
                }
            }
            else
            {
                _DisableRetakeTestUI();
            }
        }
        private void _DisableRetakeTestUI()
        {
            gbRetakeTestInfo.Enabled = false;
            lblRAppFees.Text = "0";
            gbRetakeTestInfo.Text = ctrlScheduledTest1.TestFees.ToString();
            lblRTestAppID.Text = "N/A";
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ctrlScheduledTest1.Save())
            {
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (ctrlScheduledTest1.RetakeTestApplicationID != -1)
                {
                    lblRTestAppID.Text = ctrlScheduledTest1.RetakeTestApplicationID.ToString();
                }

                btnSave.Enabled = false;
            }
            else
            {
                MessageBox.Show("Error: Data was not saved successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
