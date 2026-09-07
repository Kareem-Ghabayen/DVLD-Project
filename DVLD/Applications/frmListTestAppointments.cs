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
            //    clsBLLocalDrivingLicenseApplication localDrivingLicenseApplication =
            //clsBLLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID(_LocalDrivingLicenseApplicationID);

            //    if (localDrivingLicenseApplication.IsThereAnActiveScheduledTest((clsBLTestType.enTestType)_TestType))
            //    {
            //        MessageBox.Show("Person already has an active appointment for this test, You cannot add a new appointment", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        return;
            //    }

            //    clsBLTest lastTest = localDrivingLicenseApplication.GetLastTestPerTestType((clsBLTestType.enTestType)_TestType);

            //    if (lastTest != null && lastTest.TestResult == true)
            //    {
            //        MessageBox.Show("This person already passed this test before. You can only retake failed tests.", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        return;
            //    }

            //    frmScheduleTest frm = new frmScheduleTest(_LocalDrivingLicenseApplicationID, (clsBLTestType.enTestType)_TestType);
            //    frm.ShowDialog();

            //    _RefreshAppointmentsList();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            {
                //int appointmentID = (int)dgvAppointments.CurrentRow.Cells[0].Value;

                //// فتح شاشة الجدولة وتمرير ID الموعد للتعديل عليه
                //frmScheduleTest frm = new frmScheduleTest(_LocalDrivingLicenseApplicationID, (clsBLTestType.enTestType)_TestType, appointmentID);
                //frm.ShowDialog();

                //// إعادة تحديث القائمة بعد التعديل
                //_RefreshAppointmentsList();
            }
        }

        private void takeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            {
                //int appointmentID = (int)dgvAppointments.CurrentRow.Cells[0].Value;

                //// فتح شاشة إجراء الاختبار وتمرير ID الموعد
                //frmTakeTest frm = new frmTakeTest(appointmentID, (clsBLTestType.enTestType)_TestType);
                //frm.ShowDialog();

                //// إعادة تحديث القائمة لإغلاق الموعد (IsLocked = true) إذا تم تقديم الاختبار
                //_RefreshAppointmentsList();
            }
        }

        private void btnClose_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}