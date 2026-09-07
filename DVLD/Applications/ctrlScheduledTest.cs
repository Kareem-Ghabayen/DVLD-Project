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
    public partial class ctrlScheduledTest : UserControl
    {
        public enum enMode { AddNew = 1, Update = 2 };
        private enMode _Mode = enMode.AddNew;

        private clsBLTestType.enTestType _TestTypeID = clsBLTestType.enTestType.VisionTest;
        private int _LocalDrivingLicenseApplicationID = -1;
        private int _TestAppointmentID = -1;

        private clsBLTestAppointment _TestAppointment;
        private clsBLLocalDrivingLicenseApplication _LocalDrivingLicenseApplication;

        public clsBLTestType.enTestType TestTypeID => _TestTypeID;
        public int TestAppointmentID => _TestAppointmentID;
        public int TestID => _TestAppointment?.TestAppointmentID ?? -1;
        public bool IsAppointmentValidForSave { get; private set; } = true;
        public float TestFees => !string.IsNullOrEmpty(lblFees.Text) ? Convert.ToSingle(lblFees.Text) : 0;

        public DateTime SelectedDate
        {
            get => dtpAppointmentDate.Value;
            set => dtpAppointmentDate.Value = value;
        }

        public ctrlScheduledTest()
        {
            InitializeComponent();
        }
        public void LoadInfo(int LocalDrivingLicenseApplicationID, clsBLTestType.enTestType TestTypeID, int AppointmentID = -1)
        {
            _TestTypeID = TestTypeID;
            _SetTestTypeUI();

            _Mode = (AppointmentID == -1) ? enMode.AddNew : enMode.Update;
            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            _TestAppointmentID = AppointmentID;

            _LocalDrivingLicenseApplication = clsBLLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID(_LocalDrivingLicenseApplicationID);

            if (_LocalDrivingLicenseApplication == null)
            {
                MessageBox.Show("Error: No Application found with ID = " + _LocalDrivingLicenseApplicationID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                IsAppointmentValidForSave = false;
                return;
            }

            // تعبئة البيانات الأساسية للطلب
            lblLocalDrivingLicenseAppID.Text = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();
            lblDrivingClass.Text = _LocalDrivingLicenseApplication.LicenseClassInfo.ClassName;
            var person = _LocalDrivingLicenseApplication.BaseApplicationInfo.ApplicantPersonInfo;
            lblFullName.Text = $"{person.FirstName} {person.SecondName} {person.ThirdName} {person.LastName}";
            lblTrial.Text = _LocalDrivingLicenseApplication.TotalTrialsPerTest(_TestTypeID).ToString();

            // ضبط حالة الموعد (جديد أم تعديل)
            if (_Mode == enMode.AddNew)
            {
                lblFees.Text = clsBLTestType.Find((int)_TestTypeID).TestTypeFees.ToString();
                dtpAppointmentDate.MinDate = DateTime.Now;
                dtpAppointmentDate.Value = DateTime.Now;
                _TestAppointment = new clsBLTestAppointment();
            }
            else
            {
                if (!_LoadAppointmentData())
                    return;
            }

            // فحص قيود المواعيد النشطة والمقفلة
            _HandleActiveAndLockedAppointments();
        }
        private void _SetTestTypeUI()
        {
            switch (_TestTypeID)
            {
                case clsBLTestType.enTestType.VisionTest:
                    gbTestType.Text = "Vision Test";
                    lblTitle.Text = "Vision Test";
                    // pbTestTypeImage.Image = Properties.Resources.Vision_512;
                    break;

                case clsBLTestType.enTestType.WrittenTest:
                    gbTestType.Text = "Written Test";
                    lblTitle.Text = "Written Test";
                    // pbTestTypeImage.Image = Properties.Resources.Written_512;
                    break;

                case clsBLTestType.enTestType.StreetTest:
                    gbTestType.Text = "Street Test";
                    lblTitle.Text = "Street Test";
                    // pbTestTypeImage.Image = Properties.Resources.Street_512;
                    break;
            }
        }
        private void dtpAppointmentDate_ValueChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel15_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel7_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {

        }
    }
}
