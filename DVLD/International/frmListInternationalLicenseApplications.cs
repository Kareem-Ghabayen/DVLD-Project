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

namespace DVLD.International
{
    public partial class frmListInternationalLicenseApplications : Form
    {
        private DataTable _dtInternationalLicenses;
        public frmListInternationalLicenseApplications()
        {
            InitializeComponent();
        }
        private void _RefreshInternationalLicensesList()
        {
            _dtInternationalLicenses = clsBLInternationalLicense.GetAllInternationalLicenses();
            dgvInternationalLicenses.DataSource = _dtInternationalLicenses;
            lblRecordsCount.Text = dgvInternationalLicenses.Rows.Count.ToString();


        }

        private void frmListInternationalLicenseApplications_Load(object sender, EventArgs e)
        {

        }

        private void frmListInternationalLicenseApplications_Load_1(object sender, EventArgs e)
        {
            _RefreshInternationalLicensesList();
            cbFilterBy.SelectedIndex = 0;
            txtFilterValue.Visible = false;
            cbIsActive.Visible = false;
        }

        private void cbFilterBy_SelectedValueChanged(object sender, EventArgs e)
        {
            if (_dtInternationalLicenses == null)
                return;
            if (cbFilterBy.Text == "Is Active")
            {
                txtFilterValue.Visible = false;
                cbIsActive.Visible = true;
                cbIsActive.Focus();
                cbIsActive.SelectedIndex = 0;
            }
            else
            {
                txtFilterValue.Visible = (cbFilterBy.Text != "None");
                cbIsActive.Visible = false;

                if (cbFilterBy.Text == "None")
                {
                    txtFilterValue.Text = "";
                }
                else
                {
                    txtFilterValue.Text = "";
                    txtFilterValue.Focus();
                }
                _dtInternationalLicenses.DefaultView.RowFilter = "";
                lblRecordsCount.Text = dgvInternationalLicenses.Rows.Count.ToString();
            }
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string filterColumn = "";

            switch (cbFilterBy.Text)
            {
                case "International License ID":
                    filterColumn = "InternationalLicenseID";
                    break;

                case "Application ID":
                    filterColumn = "ApplicationID";
                    break;

                case "Driver ID":
                    filterColumn = "DriverID";
                    break;

                case "Local License ID":
                    filterColumn = "IssuedUsingLocalLicenseID";
                    break;

                default:
                    filterColumn = "None";
                    break;
            }

            if (txtFilterValue.Text.Trim() == "" || filterColumn == "None")
            {
                _dtInternationalLicenses.DefaultView.RowFilter = "";
            }
            else
            {
                _dtInternationalLicenses.DefaultView.RowFilter = string.Format("[{0}] = {1}", filterColumn, txtFilterValue.Text.Trim());
            }

            lblRecordsCount.Text = dgvInternationalLicenses.Rows.Count.ToString();
        }

        private void cbIsActive_SelectedIndexChanged(object sender, EventArgs e)
        {
            string filterValue = cbIsActive.Text;

            switch (filterValue)
            {
                case "All":
                    _dtInternationalLicenses.DefaultView.RowFilter = "";
                    break;

                case "Yes":
                    _dtInternationalLicenses.DefaultView.RowFilter = "[IsActive] = true";
                    break;

                case "No":
                    _dtInternationalLicenses.DefaultView.RowFilter = "[IsActive] = false";
                    break;
            }

            lblRecordsCount.Text = dgvInternationalLicenses.Rows.Count.ToString();
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar);
        }

        private void btnAddNewApplication_Click(object sender, EventArgs e)
        {
            frmNewInternationalLicenseApplication frm = new frmNewInternationalLicenseApplication();
            frm.ShowDialog();
            _RefreshInternationalLicensesList();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void showPersonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int driverID = (int)dgvInternationalLicenses.CurrentRow.Cells[2].Value;
            clsBLDriver driver = clsBLDriver.FindByDriverID(driverID);

            if (driver != null)
            {
                frmShowPersonInfo frm = new frmShowPersonInfo(driver.PersonID);
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Driver not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void showLicenseDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int internationalLicenseID = (int)dgvInternationalLicenses.CurrentRow.Cells[0].Value;
            frmShowInternationalLicenseInfo frm = new frmShowInternationalLicenseInfo(internationalLicenseID);
            frm.ShowDialog();
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int driverID = (int)dgvInternationalLicenses.CurrentRow.Cells[2].Value;
            clsBLDriver driver = clsBLDriver.FindByDriverID(driverID);

            if (driver != null)
            {
                frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(driver.PersonID);
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Driver not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        } 
    
    
    }
}
