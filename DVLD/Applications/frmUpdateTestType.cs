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
    public partial class frmUpdateTestType : Form
    {
        private int _TestTypeID = -1;
        private clsBLTestType _TestType;

        public frmUpdateTestType(int testTypeID)
        {
            InitializeComponent();
            _TestTypeID = testTypeID;
        }
        private void _LoadData()
        {
            _TestType = clsBLTestType.Find(_TestTypeID);

            if (_TestType == null)
            {
                MessageBox.Show("No Test Type with ID = " + _TestTypeID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();
                return;
            }

            lblTestTypeID.Text = _TestType.TestTypeID.ToString();
            txtTitle.Text = _TestType.TestTypeTitle;
            txtDescription.Text = _TestType.TestTypeDescription;
            txtFees.Text = _TestType.TestTypeFees.ToString();
        }
        private void frmUpdateTestType_Load(object sender, EventArgs e)
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

        private void txtDescription_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtDescription.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtDescription, "Description cannot be empty!");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtDescription, null);
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

            if (!float.TryParse(txtFees.Text.Trim(), out _))
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

            _TestType.TestTypeTitle = txtTitle.Text.Trim();
            _TestType.TestTypeDescription = txtDescription.Text.Trim();
            _TestType.TestTypeFees = Convert.ToSingle(txtFees.Text.Trim());

            if (_TestType.Save())
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
