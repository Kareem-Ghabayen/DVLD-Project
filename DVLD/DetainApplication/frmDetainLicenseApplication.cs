using BuisnessLayer;
using BusinessLayer;
using DVLD.Applications;
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
    public partial class frmDetainLicenseApplication : Form
    {
        private int _DetainID = -1;
        private int _SelectedLicenseID = -1;
        public frmDetainLicenseApplication()
        {
            InitializeComponent();
        }

        private void frmDetainLicenseApplication_Load(object sender, EventArgs e)
        {
            ctrlDriverLicenseInfoWithFilter1.OnLicenseSelected += ctrlDriverLicenseInfoWithFilter1_OnLicenseSelected;
            lblDetainDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName;
        }
        private void ctrlDriverLicenseInfoWithFilter1_OnLicenseSelected(int obj)
        {
            _SelectedLicenseID = obj;
            lblLicenseID.Text = _SelectedLicenseID.ToString();

            if (_SelectedLicenseID == -1)
            {
                btnDetain.Enabled = false;
                llShowLicensesHistory.Enabled = false;
                return;
            }

            llShowLicensesHistory.Enabled = true;

            // 1. التحقق من أن الرخصة فعالة
            if (!ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.IsActive)
            {
                MessageBox.Show("Selected License is NOT Active, choose an active license.", "Not allowed",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnDetain.Enabled = false;
                return;
            }

            // 2. التحقق مما إذا كانت الرخصة محجوزة بالفعل
            if (clsBLDetainedLicense.IsLicenseDetained(_SelectedLicenseID))
            {
                MessageBox.Show("Selected License is already detained, choose another one.", "Not allowed",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnDetain.Enabled = false;
                return;
            }

            txtFineFees.Focus();
            btnDetain.Enabled = true;
        }

        private void llShowLicensesHistory_Click(object sender, EventArgs e)
        {
            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(
                ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.DriverInfo.PersonID);
            frm.ShowDialog();
        }

        private void llShowLicensesInfo_Click(object sender, EventArgs e)
        {
            frmShowLicenseInfo frm = new frmShowLicenseInfo(_SelectedLicenseID);
            frm.ShowDialog();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //  هاد الميثود ما استغلتها هان بدها شغل في ال  bll 
        private void btnDetain_Click(object sender, EventArgs e)
        {
            // التحقق من صحة المدخل الرقمي للغرامة
            if (string.IsNullOrEmpty(txtFineFees.Text.Trim()) || !decimal.TryParse(txtFineFees.Text.Trim(), out decimal fineFees))
            {
                MessageBox.Show("Please enter a valid Fine Fee!", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtFineFees.Focus();
                return;
            }

            if (MessageBox.Show("Are you sure you want to detain this license?", "Confirm", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            clsBLDetainedLicense detainedLicense = new clsBLDetainedLicense();

            detainedLicense.LicenseID = _SelectedLicenseID;
            detainedLicense.DetainDate = DateTime.Now;
            detainedLicense.FineFees = Convert.ToDecimal(txtFineFees.Text.Trim());
            detainedLicense.CreatedByUserID = clsGlobal.CurrentUser.UserID;
            detainedLicense.IsReleased = false;

            if (!detainedLicense.Save())
            {
                MessageBox.Show("Failed to Detain License!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _DetainID = detainedLicense.DetainID;
            lblDetainID.Text = _DetainID.ToString();

            MessageBox.Show($"License Detained Successfully with ID = {_DetainID}", "License Detained", 
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            btnDetain.Enabled = false;
            ctrlDriverLicenseInfoWithFilter1.FilterEnabled = false;
            txtFineFees.Enabled = false;
            llShowLicensesInfo.Enabled = true;

            // تحديث الكنترول لتقلب Is Detained تلقائياً إلى Yes
            ctrlDriverLicenseInfoWithFilter1.OnLicenseSelected -= ctrlDriverLicenseInfoWithFilter1_OnLicenseSelected;

            // 2. تحديث الكنترول لتقلب Is Detained إلى Yes
            ctrlDriverLicenseInfoWithFilter1.LoadLicenseInfo(_SelectedLicenseID);

            // 3. إعادة ربط الحدث من جديد للعمليات القادمة
            ctrlDriverLicenseInfoWithFilter1.OnLicenseSelected += ctrlDriverLicenseInfoWithFilter1_OnLicenseSelected;
        }
    }
}
