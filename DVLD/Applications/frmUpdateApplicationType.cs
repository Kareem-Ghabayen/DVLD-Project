using BuisnessLayer;
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
    public partial class frmUpdateApplicationType : Form
    {
        private int _ApplicationTypeID = -1;
        private clsBLApplicationType _ApplicationType;
        public frmUpdateApplicationType(int applicationTypeID)
        {
            InitializeComponent();
            _ApplicationTypeID = applicationTypeID;
        }
        private void _LoadData()
        {
            _ApplicationType = clsBLApplicationType.Find(_ApplicationTypeID);

            if (_ApplicationType == null)
            {
                MessageBox.Show("No Application Type with ID = " + _ApplicationTypeID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();
                return;
            }

            lblApplicationTypeID.Text = _ApplicationType.ApplicationTypeID.ToString();
            txtTitle.Text = _ApplicationType.ApplicationTypeTitle;
            txtFees.Text = _ApplicationType.ApplicationFees.ToString();
        }
        private void frmUpdateApplicationType_Load(object sender, EventArgs e)
        {
            _LoadData();
        }

        private void txtTitle_Validating(object sender, CancelEventArgs e)
        {
           
                if (string.IsNullOrEmpty(txtTitle.Text.Trim()))
                {
                    e.Cancel = true;
                    errorProvider1.SetError(txtTitle, "Title cannot be empty!");
                }
                else
                {
                    e.Cancel = false;
                    errorProvider1.SetError(txtTitle, null);
                }
            }

        private void txtFees_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFees.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFees, "Fees cannot be empty!");
                return;
            }

            if (!decimal.TryParse(txtFees.Text.Trim(), out _))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFees, "Invalid number format!");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtFees, null);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fields are not valid, put the mouse over the red icon(s)", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _ApplicationType.ApplicationTypeTitle = txtTitle.Text.Trim();
            _ApplicationType.ApplicationFees = Convert.ToSingle(txtFees.Text.Trim());
            if (_ApplicationType.Save())
            {
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Error: Data Was Not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
