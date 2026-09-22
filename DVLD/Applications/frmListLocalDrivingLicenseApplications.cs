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

            if (txtFilterValue.Text.Trim() == "" || filterColumn == "None")
            {
                _dtAllApplications.DefaultView.RowFilter = "";
                lblRecordsCount.Text = dgvLocalDrivingLicenseApplications.Rows.Count.ToString();
                return;
            }

            if (filterColumn == "LocalDrivingLicenseApplicationID")
            {
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
                _dtAllApplications.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", filterColumn, txtFilterValue.Text.Trim());
            }

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

            bool isNew = (status == "New");
            editApplicationToolStripMenuItem.Enabled = isNew;
            deleteApplicationToolStripMenuItem.Enabled = isNew;
            cancelApplicationToolStripMenuItem.Enabled = isNew;

            scheduleTestsToolStripMenuItem.Enabled = isNew && (passedTests < 3);
            if (scheduleTestsToolStripMenuItem.Enabled)
            {
                scheduleVisionTestToolStripMenuItem.Enabled = (passedTests == 0);
                scheduleWrittenTestToolStripMenuItem.Enabled = (passedTests == 1);
                scheduleStreetTestToolStripMenuItem.Enabled = (passedTests == 2);
            }

            issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = (passedTests == 3 && isNew);

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

            if (MessageBox.Show("Are you sure you want to delete this application?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            int localDrivingLicenseApplicationID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;

            if (clsBLLocalDrivingLicenseApplication.DeleteLocalDrivingLicenseApplication(localDrivingLicenseApplicationID))
            {
                MessageBox.Show("Application Deleted Successfully.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _RefreshLocalDrivingLicenseApplicationsList();
            }
            else
            {
                MessageBox.Show("Could not delete application.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
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
            if (dgvLocalDrivingLicenseApplications.CurrentRow == null)
                return;

            // 2. جلب رقم الطلب المحلي من السطر المحدد
            int localDrivingLicenseApplicationID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells["LocalDrivingLicenseApplicationID"].Value;

            // 3. فتح شاشة إصدار الرخصة لأول مرة وتمرير الرقم
            frmIssueDriverLicenseFirstTime frm = new frmIssueDriverLicenseFirstTime(localDrivingLicenseApplicationID);
            frm.ShowDialog();

            // 4. إعادة تحميل الجدول لتحديث حالة الطلب فور إغلاق الشاشة
            frmListLocalDrivingLicenseApplications_Load(null, null);
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 1. جلب رقم الطلب المحلي من السطر المحدد في الجدول
            int LocalDrivingLicenseApplicationID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;

            // 2. البحث عن بيانات الطلب للحصول على رقم الشخص (ApplicantPersonID)
            clsBLLocalDrivingLicenseApplication LocalDrivingLicenseApplication =
                clsBLLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID(LocalDrivingLicenseApplicationID);

            if (LocalDrivingLicenseApplication != null)
            {
                // 3. تمرير رقم الشخص للكونستركتور وفتح الشاشة
                frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(LocalDrivingLicenseApplication.BaseApplicationInfo.ApplicantPersonID);
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("No Application Found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void showLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int localDrivingLicenseApplicationID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells["LocalDrivingLicenseApplicationID"].Value;

            int licenseID = clsBLLocalDrivingLicenseApplication
                                .FindByLocalDrivingLicenseApplicationID(localDrivingLicenseApplicationID)
                                .GetActiveLicenseID();
            frmShowLicenseInfo frm = new frmShowLicenseInfo(licenseID);

            frm.ShowDialog();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}