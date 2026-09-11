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
    public partial class ctrlScheduledTest : UserControl
    {
        public enum enMode { AddNew = 1, Update = 2 };
        private enMode _Mode = enMode.AddNew;

        private clsBLTestType.enTestType _TestTypeID = clsBLTestType.enTestType.VisionTest;
        private int _LocalDrivingLicenseApplicationID = -1;
        private int _TestAppointmentID = -1;

        private clsBLTestAppointment _TestAppointment;
        private clsBLLocalDrivingLicenseApplication _LocalDrivingLicenseApplication;

        // الإضافة: خاصية رقم طلب الإعادة الممرر من الفورم
        public int RetakeTestApplicationID { get; set; } = -1;

        public clsBLTestType.enTestType TestTypeID => _TestTypeID;
        public int TestAppointmentID => _TestAppointmentID;
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
                if (_LocalDrivingLicenseApplication.TotalTrialsPerTest(_TestTypeID) > 0)
                {
                    // نغير القيمة عن -1 لتجهيز الإشارة لحالة الإعادة
                    RetakeTestApplicationID = 0;
                }
                else
                {
                    RetakeTestApplicationID = -1;
                }
                }
            else
            {
                if (!_LoadAppointmentData())
                    return;
            }

            // فحص قيود المواعيد النشطة والمقفلة
            _HandleActiveAndLockedAppointments();
        }

        // وظيفتها تفعيل فحص الشروط ادا كان موعد جديد + ادا كان تحديث والموعد مغلق يسكرو عليه
        private void _HandleActiveAndLockedAppointments()
        {
            // 1. فحص النجاح المسبق باستخدام clsBLTest
            if (_Mode == enMode.AddNew && clsBLTest.DoesPassTestType(_LocalDrivingLicenseApplicationID, (int)_TestTypeID))
            {
                lblUserMessage.Text = "Person already passed this test, appointment cannot be scheduled.";
                lblUserMessage.Visible = true;
                dtpAppointmentDate.Enabled = false;
                IsAppointmentValidForSave = false;
                return;
            }
            if (_Mode == enMode.AddNew && clsBLTestAppointment.IsThereAnActiveAppointment(_LocalDrivingLicenseApplicationID, (int)_TestTypeID))
            {
                lblUserMessage.Text = "Person already has an active appointment for this test";
                lblUserMessage.Visible = true;
                dtpAppointmentDate.Enabled = false;
                IsAppointmentValidForSave = false;
                return;
            }

            if (_Mode == enMode.Update && _TestAppointment.IsLocked)
            {
                lblUserMessage.Text = "Person already sat for the test, appointment locked.";
                lblUserMessage.Visible = true;
                dtpAppointmentDate.Enabled = false;
                IsAppointmentValidForSave = false;
                return;
            }

            lblUserMessage.Visible = false;
            dtpAppointmentDate.Enabled = true;
            IsAppointmentValidForSave = true;
        }

        private bool _LoadAppointmentData()
        {
            _TestAppointment = clsBLTestAppointment.Find(_TestAppointmentID);

            if (_TestAppointment == null)
            {
                MessageBox.Show("Error: No Appointment with ID = " + _TestAppointmentID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                IsAppointmentValidForSave = false;
                return false;
            }

            lblFees.Text = _TestAppointment.PaidFees.ToString();

            if (DateTime.Compare(DateTime.Now, _TestAppointment.AppointmentDate) < 0)
                dtpAppointmentDate.MinDate = DateTime.Now;
            else
                dtpAppointmentDate.MinDate = _TestAppointment.AppointmentDate;

            dtpAppointmentDate.Value = _TestAppointment.AppointmentDate;
            return true;
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

        public bool Save()
        {
            // 1. فحص شروط الحفظ الخاصة بالواجهة
            if (!IsAppointmentValidForSave)
            {
                MessageBox.Show("DEBUG: فشل الفحص المبدئي IsAppointmentValidForSave (قد يكون هناك موعد نشط أو غير مستوفٍ للشروط).",
                                "Trace Step 1", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (_Mode == enMode.AddNew)
            {
                // 2. حالة الحجز للمرة الأولى
                if (RetakeTestApplicationID == -1)
                {
                    if (clsBLTestAppointment.ScheduleNewTestAppointment(
                        _LocalDrivingLicenseApplicationID,
                        (int)_TestTypeID,
                        dtpAppointmentDate.Value))
                    {
                        _Mode = enMode.Update;
                        return true;
                    }

                    MessageBox.Show("DEBUG: فشلت الميثود ScheduleNewTestAppointment داخل الـ BLL! تحقق من قيود قاعدة البيانات (Foreign Keys) أو قيم null.",
                                    "Trace Step 2A - New Test Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                // 3. حالة إعادة الاختبار
                else
                {
                    _TestAppointment = clsBLTestAppointment.ScheduleRetakeTest(
                        _LocalDrivingLicenseApplicationID,
                        (int)_TestTypeID,
                        dtpAppointmentDate.Value);

                    if (_TestAppointment != null)
                    {
                        _Mode = enMode.Update;
                        _TestAppointmentID = _TestAppointment.TestAppointmentID;
                        RetakeTestApplicationID = _TestAppointment.RetakeTestApplicationID;
                        return true;
                    }

                    MessageBox.Show("DEBUG: فشلت الميثود ScheduleRetakeTest وأرجعت null! تحقق من شرط الرسوب أو إنشاء طلب الإعادة داخل البزنس.",
                                    "Trace Step 2B - Retake Test Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            else // 4. حالة التعديل Update
            {
                _TestAppointment.AppointmentDate = dtpAppointmentDate.Value;

                if (_TestAppointment.Save())
                {
                    return true;
                }

                MessageBox.Show("DEBUG: فشلت عملية التعديل _TestAppointment.Save() أثناء التحديث في قاعدة البيانات.",
                                "Trace Step 3 - Update Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
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

        private void gbTestType_Enter(object sender, EventArgs e)
        {
             
        }
    }
}