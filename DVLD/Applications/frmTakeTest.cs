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

namespace DVLD.Applications
{
    public partial class frmTakeTest : Form
    {
private int _LocalDrivingLicenseApplicationID;
        private clsBLTestType.enTestType _TestTypeID;
        private int _TestAppointmentID;

        public frmTakeTest(int LocalDrivingLicenseApplicationID, clsBLTestType.enTestType TestTypeID, int TestAppointmentID)
        {
            InitializeComponent();
            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            _TestTypeID = TestTypeID;
            _TestAppointmentID = TestAppointmentID;
        }

        private void frmTakeTest_Load(object sender, EventArgs e)
        {
            // 1. تحميل البيانات في الكنترول
            ctrlScheduledTest1.LoadInfo(_LocalDrivingLicenseApplicationID, _TestTypeID, _TestAppointmentID);

            // 2. التحقق مما إذا كان الاختبار قد تم إجراؤه سابقاً
            clsBLTest test = clsBLTest.FindByTestAppointmentID(_TestAppointmentID);

            if (test != null)
            {
                if (test.TestResult)
                    rbPass.Checked = true;
                else
                    rbFail.Checked = true;

                txtNotes.Text = test.Notes;

                // قفل الواجهة لأن الاختبار أُجري سابقاً
                btnSave.Enabled = false;
                rbPass.Enabled = false;
                rbFail.Enabled = false;
                txtNotes.Enabled = false;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to save this test result? After saving you cannot change it.",
                "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
            {
                return;
            }

            // استدعاء ميثود الـ BLL المباشرة
            if (clsBLTest.TakeTest(_TestAppointmentID, rbPass.Checked, txtNotes.Text.Trim()))
            {
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

                btnSave.Enabled = false;
                rbPass.Enabled = false;
                rbFail.Enabled = false;
                txtNotes.Enabled = false;
            }
            else
            {
                MessageBox.Show("Error: Data Was not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
