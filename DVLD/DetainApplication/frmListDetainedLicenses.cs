using BuisnessLayer;
using BusinessLayer;
using DVLD.Applications;
using DVLD.people;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.DetainApplication
{
    public partial class frmListDetainedLicenses : Form
    {
        private static DataTable _dtAllDetainedLicenses;
        public frmListDetainedLicenses()
        {
            InitializeComponent();
        }

        private void cmsDetainedLicenses_Opening(object sender, CancelEventArgs e)
        {
            // 1. التأكد من تحديد صف داخل الجدول
            if (dgvDetainedLicenses.CurrentRow == null)
            {
                e.Cancel = true;
                return;
            }

            // 2. جلب قيمة IsReleased للصف الحالي
            bool isReleased = Convert.ToBoolean(dgvDetainedLicenses.CurrentRow.Cells["IsReleased"].Value);

            // 3. إذا كانت مفرّج عنها أصلاً (IsReleased = true) يتم تعطيل الخيار، والعكس صحيح
            releaseDetainedLicenseToolStripMenuItem.Enabled = !isReleased;
        }
        private void _RefreshDetainedLicensesList()
        {
            _dtAllDetainedLicenses = clsBLDetainedLicense.GetAllDetainedLicenses();
            dgvDetainedLicenses.DataSource = _dtAllDetainedLicenses;
            lblRecords.Text = dgvDetainedLicenses.Rows.Count.ToString();

  
        }

        private void frmListDetainedLicenses_Load(object sender, EventArgs e)
        {

            cbFilterBy.SelectedIndex = 0;
            _RefreshDetainedLicensesList();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
     
            if (cbFilterBy.Text == "Is Released")
            {
                txtFilterValue.Visible = false;
                cbIsReleased.Visible = true;
                cbIsReleased.Focus();
                cbIsReleased.SelectedIndex = 0;
            }
            else
            {
                txtFilterValue.Visible = (cbFilterBy.Text != "None");
                cbIsReleased.Visible = false;

                txtFilterValue.Text = "";
                txtFilterValue.Focus();
            }
            if (_dtAllDetainedLicenses == null)
                return;
            _dtAllDetainedLicenses.DefaultView.RowFilter = "";
            lblRecords.Text = dgvDetainedLicenses.Rows.Count.ToString();
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string filterColumn = "";

            switch (cbFilterBy.Text)
            {
                case "Detain ID":
                    filterColumn = "DetainID";
                    break;
                case "Is Released":
                    filterColumn = "IsReleased";
                    break;
                case "National No":
                    filterColumn = "NationalNo";
                    break;
                case "Full Name":
                    filterColumn = "FullName";
                    break;
                case "Release App.ID":
                    filterColumn = "ReleaseApplicationID";
                    break;
                default:
                    filterColumn = "None";
                    break;
            }

            if (txtFilterValue.Text.Trim() == "" || filterColumn == "None")
            {
                _dtAllDetainedLicenses.DefaultView.RowFilter = "";
                lblRecords.Text = dgvDetainedLicenses.Rows.Count.ToString();
                return;
            }

            if (filterColumn == "DetainID" || filterColumn == "ReleaseApplicationID")
                _dtAllDetainedLicenses.DefaultView.RowFilter = string.Format("[{0}] = {1}", filterColumn, txtFilterValue.Text.Trim());
            else
                _dtAllDetainedLicenses.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", filterColumn, txtFilterValue.Text.Trim());

            lblRecords.Text = dgvDetainedLicenses.Rows.Count.ToString();
        }

        private void cbIsReleased_SelectedIndexChanged(object sender, EventArgs e)
        {
            string filterValue = cbIsReleased.Text;

            switch (filterValue)
            {
                case "All":
                    _dtAllDetainedLicenses.DefaultView.RowFilter = "";
                    break;
                case "Yes":
                    _dtAllDetainedLicenses.DefaultView.RowFilter = "[IsReleased] = true";
                    break;
                case "No":
                    _dtAllDetainedLicenses.DefaultView.RowFilter = "[IsReleased] = false";
                    break;
            }

            lblRecords.Text = dgvDetainedLicenses.Rows.Count.ToString();
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.Text == "Detain ID" || cbFilterBy.Text == "Release App.ID")
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }
        }

        private void showPersonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string nationalNo = (string)dgvDetainedLicenses.CurrentRow.Cells["NationalNo"].Value;
            clsBLSPeople person = clsBLSPeople.FindByNationalNo(nationalNo);

            frmShowPersonInfo frm = new frmShowPersonInfo(person.ID);
            frm.ShowDialog();
        }

        private void showLicenseDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int licenseID = (int)dgvDetainedLicenses.CurrentRow.Cells["LicenseID"].Value;
            frmShowLicenseInfo frm = new frmShowLicenseInfo(licenseID);
            frm.ShowDialog();
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string nationalNo = (string)dgvDetainedLicenses.CurrentRow.Cells["NationalNo"].Value;
            clsBLSPeople person = clsBLSPeople.FindByNationalNo(nationalNo);

            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(person.ID);
            frm.ShowDialog();
        }

        private void releaseDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int licenseID = (int)dgvDetainedLicenses.CurrentRow.Cells["LicenseID"].Value;

            frmReleaseDetainedLicenseApplication frm = new frmReleaseDetainedLicenseApplication(licenseID);
            frm.ShowDialog();

            _RefreshDetainedLicensesList();
        }

        private void btnDetainLicense_Click(object sender, EventArgs e)
        {
            frmDetainLicenseApplication frm = new frmDetainLicenseApplication();
            frm.ShowDialog();

            _RefreshDetainedLicensesList();
        }

        private void btnReleaseDetainedLicense_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedLicenseApplication frm = new frmReleaseDetainedLicenseApplication();
            frm.ShowDialog();

            _RefreshDetainedLicensesList();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
