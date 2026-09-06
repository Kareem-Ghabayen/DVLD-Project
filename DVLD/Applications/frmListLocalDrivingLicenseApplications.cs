using DVLD.people;
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
    public partial class frmListLocalDrivingLicenseApplications : Form
    {
        public frmListLocalDrivingLicenseApplications()
        {
            InitializeComponent();
        }
        private static DataTable _dtAllApplications;

        private void _RefreshLocalDrivingLicenseApplicationsList()
        {
            _dtAllApplications = clsBLLocalDrivingLicenseApplication.GetAllLocalDrivingLicenseApplications();
            dgvLocalDrivingLicenseApplications.DataSource = _dtAllApplications;
            lblRecordsCount.Text = dgvLocalDrivingLicenseApplications.Rows.Count.ToString();
        }
        private void frmListLocalDrivingLicenseApplications_Load(object sender, EventArgs e)
        {
            _RefreshLocalDrivingLicenseApplicationsList();

            // ضبط الفلترة على None افتراضياً
            if (cbFilterBy.Items.Count > 0)
                cbFilterBy.SelectedIndex = 0;

            txtFilterValue.Visible = false;
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_dtAllApplications == null) return;

            txtFilterValue.Text = "";

            if (cbFilterBy.Text == "None")
            {
                txtFilterValue.Visible = false;
                _dtAllApplications.DefaultView.RowFilter = "";
                lblRecordsCount.Text = dgvLocalDrivingLicenseApplications.Rows.Count.ToString();
            }
            else
            {
                txtFilterValue.Visible = true;
                txtFilterValue.Focus();
            }

        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string filterColumn = "";

            // ربط النص المعروض في ComboBox باسم العمود الحقيقي في الفيو
            switch (cbFilterBy.Text)
            {
                case "L.D.L.AppID":
                    filterColumn = "LocalDrivingLicenseApplicationID";
                    break;

                case "National No":
                    filterColumn = "NationalNo";
                    break;

                case "Full Name":
                    filterColumn = "FullName";
                    break;

                case "Status":
                    filterColumn = "Status";
                    break;

                default:
                    filterColumn = "None";
                    break;
            }

            // إذا تم مسح النص أو اخترنا None نلغي الفلترة
            if (txtFilterValue.Text.Trim() == "" || filterColumn == "None")
            {
                _dtAllApplications.DefaultView.RowFilter = "";
                lblRecordsCount.Text = dgvLocalDrivingLicenseApplications.Rows.Count.ToString();
                return;
            }

            // تطبيق الفلترة حسب نوع العمود (رقمي أم نصي)
            if (filterColumn == "LocalDrivingLicenseApplicationID")
            {
                // الفلترة الرقمية بـ =
                if (int.TryParse(txtFilterValue.Text.Trim(), out int appID))
                {
                    _dtAllApplications.DefaultView.RowFilter = string.Format("[{0}] = {1}", filterColumn, appID);
                }
                else
                {
                    _dtAllApplications.DefaultView.RowFilter = "";
                }
            }
            else
            {
                // الفلترة النصية بـ LIKE
                _dtAllApplications.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", filterColumn, txtFilterValue.Text.Trim());
            }

            // تحديث العداد بناءً على النتيجة المفوترة
            lblRecordsCount.Text = dgvLocalDrivingLicenseApplications.Rows.Count.ToString();
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.Text == "L.D.L.AppID")
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }
        }

        private void btnAddNewApplication_Click(object sender, EventArgs e)
        {
            frmAddUpdateLocalDrivingLicenseApplication frm = new frmAddUpdateLocalDrivingLicenseApplication();
            frm.ShowDialog();

            // إعادة تحديث القائمة فور إغلاق شاشة الإضافة لتظهر البيانات الجديدة
            _RefreshLocalDrivingLicenseApplicationsList();
        }

        private void scheduleVisionTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int localDrivingLicenseApplicationID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;

            frmListTestAppointments frm = new frmListTestAppointments(localDrivingLicenseApplicationID, frmListTestAppointments.enTestType.VisionTest);
            frm.ShowDialog();

            _RefreshLocalDrivingLicenseApplicationsList();
        }

        private void cmsApplications_Opening(object sender, CancelEventArgs e)
        {
            int localDrivingLicenseApplicationID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;
            clsBLLocalDrivingLicenseApplication localApplication = clsBLLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID(localDrivingLicenseApplicationID);

            if (localApplication == null)
                return;

            int passedTests = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[5].Value;
            string status = (string)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[6].Value;

            // تمكين أو تعطيل خيارات التعديل والحذف والإلغاء
            bool isNew = (status == "New");
            editApplicationToolStripMenuItem.Enabled = isNew;
            deleteApplicationToolStripMenuItem.Enabled = isNew;
            cancelApplicationToolStripMenuItem.Enabled = isNew;

            // خيارات جدولة الاختبارات
            scheduleTestsToolStripMenuItem.Enabled = isNew && (passedTests < 3);
            if (scheduleTestsToolStripMenuItem.Enabled)
            {
                scheduleVisionTestToolStripMenuItem.Enabled = (passedTests == 0);
                scheduleWrittenTestToolStripMenuItem.Enabled = (passedTests == 1);
                scheduleStreetTestToolStripMenuItem.Enabled = (passedTests == 2);
            }

            // إصدار الرخصة لأول مرة
            issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = (passedTests == 3 && isNew);

            // عرض الرخصة (متاحة فقط إذا كانت الحالة Completed)
            showLicenseToolStripMenuItem.Enabled = (status == "Completed");
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LocalDrivingLicenseApplicationID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;

            frmShowLocalDrivingLicenseApplicationInfo frm = new frmShowLocalDrivingLicenseApplicationInfo(LocalDrivingLicenseApplicationID);
            frm.ShowDialog();
        }

        private void editApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int localDrivingLicenseApplicationID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;
            frmAddUpdateLocalDrivingLicenseApplication frm = new frmAddUpdateLocalDrivingLicenseApplication(localDrivingLicenseApplicationID);
            frm.ShowDialog();
            _RefreshLocalDrivingLicenseApplicationsList();
        }

        private void deleteApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {

            //    if (MessageBox.Show("Are you sure you want to delete this application?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            //        return;

            //    int localDrivingLicenseApplicationID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;

            //    if (clsBLLocalDrivingLicenseApplication.DeleteLocalDrivingLicenseApplication(localDrivingLicenseApplicationID))
            //    {
            //        MessageBox.Show("Application Deleted Successfully.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        _RefreshLocalDrivingLicenseApplicationsList();
            //    }
            //    else
            //    {
            //        MessageBox.Show("Could not delete application.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    }
            //}
        }

        private void cancelApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to cancel this application?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            int localDrivingLicenseApplicationID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;
            clsBLLocalDrivingLicenseApplication localApp = clsBLLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID(localDrivingLicenseApplicationID);

            if (localApp != null && localApp.BaseApplicationInfo.Cancel())
            {
                MessageBox.Show("Application Cancelled Successfully.", "Cancelled", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _RefreshLocalDrivingLicenseApplicationsList();
            }
            else
            {
                MessageBox.Show("Could not cancel application.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void scheduleWrittenTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int localDrivingLicenseApplicationID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;

            frmListTestAppointments frm = new frmListTestAppointments(localDrivingLicenseApplicationID, frmListTestAppointments.enTestType.WrittenTest);
            frm.ShowDialog();

            _RefreshLocalDrivingLicenseApplicationsList();
        }

        private void scheduleStreetTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int localDrivingLicenseApplicationID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;

            frmListTestAppointments frm = new frmListTestAppointments(localDrivingLicenseApplicationID, frmListTestAppointments.enTestType.StreetTest);
            frm.ShowDialog();

            _RefreshLocalDrivingLicenseApplicationsList();
        }
        private void _ScheduleTest(int testTypeID)
        {
            //int localDrivingLicenseApplicationID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;
            //frmListTestAppointments frm = new frmListTestAppointments(localDrivingLicenseApplicationID, testTypeID);
            //frm.ShowDialog();
            //_RefreshLocalDrivingLicenseApplicationsList();
        }

        private void issueDrivingLicenseFirstTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //int localDrivingLicenseApplicationID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;
            //frmIssueDriverLicenseFirstTime frm = new frmIssueDriverLicenseFirstTime(localDrivingLicenseApplicationID);
            //frm.ShowDialog();
            //_RefreshLocalDrivingLicenseApplicationsList();
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //string nationalNo = (string)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[2].Value;
            //clsBLPerson person = clsBLPerson.Find(nationalNo);

            //if (person != null)
            //{
            //    frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(person.PersonID);
            //    frm.ShowDialog();
            //}
        }

        private void showLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //int localDrivingLicenseApplicationID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;

            //// جلب كائن الطلب للوصول لرقم الرخصة المرتبطة به
            //clsBLLocalDrivingLicenseApplication localApp = clsBLLocalDrivingLicenseApplication.FindByLocalDrivingLicenseAppID(localDrivingLicenseApplicationID);

            //if (localApp != null)
            //{
            //    // جلب رقم الرخصة النشطة لهذا الطلب المحلي
            //    int licenseID = localApp.GetActiveLicenseID();

            //    if (licenseID != -1)
            //    {
            //        frmShowLicenseInfo frm = new frmShowLicenseInfo(licenseID);
            //        frm.ShowDialog();
            //    }
            //    else
            //    {
            //        MessageBox.Show("No License Found for this application!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    }
            //}
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}