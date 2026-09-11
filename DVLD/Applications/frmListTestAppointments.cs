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
    public partial class frmListTestAppointments : Form
    {
        public enum enTestType { VisionTest = 1, WrittenTest = 2, StreetTest = 3 };

        private enTestType _TestType = enTestType.VisionTest;
        private int _LocalDrivingLicenseApplicationID = -1;
        private DataTable _dtLicenseTestAppointments;

        public frmListTestAppointments(int LocalDrivingLicenseApplicationID, enTestType TestType)
        {
            InitializeComponent();
            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            _TestType = TestType;
        }

        private void _LoadTestTypeTitleAndImage()
        {
            switch (_TestType)
            {
                case enTestType.VisionTest:
                    lblTitle.Text = "Vision Test Appointments";
                    pbTestTypeImage.Image = Properties.Resources.Vision_512; // اضبط اسم الصورة لديك
                    break;

                case enTestType.WrittenTest:
                    lblTitle.Text = "Written Test Appointments";
                    pbTestTypeImage.Image = Properties.Resources.Schedule_Test_512;
                    break;

                case enTestType.StreetTest:
                    lblTitle.Text = "Street Test Appointments";
                    pbTestTypeImage.Image = Properties.Resources.Schedule_Test_512;
                    break;
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ctrlLocalDrivingLicenseApplicationInfo1_Load(object sender, EventArgs e)
        {

        }
        private void _RefreshAppointmentsList()
        {
            _dtLicenseTestAppointments = clsBLTestAppointment.GetApplicationTestAppointmentsPerTestType(_LocalDrivingLicenseApplicationID, (int)_TestType);

            dgvAppointments.DataSource = _dtLicenseTestAppointments;
            lblRecordsCount.Text = dgvAppointments.Rows.Count.ToString();

        }
        private void frmListTestAppointments_Load(object sender, EventArgs e)
        {
            _LoadTestTypeTitleAndImage();

            ctrlLocalDrivingLicenseApplicationInfo1.LoadApplicationInfoByLocalDrivingAppID(_LocalDrivingLicenseApplicationID);

            _RefreshAppointmentsList();
        }

        private void btnAddNewAppointment_Click(object sender, EventArgs e)
        {
            int localDrivingLicenseApplicationID = _LocalDrivingLicenseApplicationID;
            clsBLTestType.enTestType testTypeID = (clsBLTestType.enTestType)_TestType; // VisionTest, WrittenTest, or StreetTest

            frmScheduleTest frm = new frmScheduleTest(localDrivingLicenseApplicationID, testTypeID);
            frm.ShowDialog();

            _RefreshAppointmentsList();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int appointmentID = (int)dgvAppointments.CurrentRow.Cells["TestAppointmentID"].Value;
            int localDrivingLicenseApplicationID = _LocalDrivingLicenseApplicationID;
            clsBLTestType.enTestType testTypeID = (clsBLTestType.enTestType)_TestType;

            frmScheduleTest frm = new frmScheduleTest(localDrivingLicenseApplicationID, testTypeID, appointmentID);
            frm.ShowDialog();

            _RefreshAppointmentsList();
        }

        private void takeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            {
                int testAppointmentID = (int)dgvAppointments.CurrentRow.Cells["TestAppointmentID"].Value;

  
                clsBLTestType.enTestType testTypeID = (clsBLTestType.enTestType)_TestType;
                frmTakeTest frm = new frmTakeTest(_LocalDrivingLicenseApplicationID, testTypeID, testAppointmentID);

                // 3. عرض الشاشة كـ Dialog
                frm.ShowDialog();

                // 4. إعادة تحميل الجدول لتحديث القائمة وإظهار الموعد المقفول (IsLocked = true)
                _RefreshAppointmentsList();
            }
        }

        private void btnClose_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}