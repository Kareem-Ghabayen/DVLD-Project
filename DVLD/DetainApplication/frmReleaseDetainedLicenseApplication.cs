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
    public partial class frmReleaseDetainedLicenseApplication : Form
    {
        private int _SelectedLicenseID = -1;
        private clsBLDetainedLicense _DetainedLicense = null;
        public frmReleaseDetainedLicenseApplication()
        {
            InitializeComponent();
        }
        public frmReleaseDetainedLicenseApplication(int licenseID)
        {
            InitializeComponent();
            _SelectedLicenseID = licenseID;
            ctrlDriverLicenseInfoWithFilter1.LoadLicenseInfo(_SelectedLicenseID);
            ctrlDriverLicenseInfoWithFilter1.FilterEnabled = false;
        }
        private void frmReleaseDetainedLicenseApplication_Load(object sender, EventArgs e)
        {
            // ربط الحدث بالدالة
            ctrlDriverLicenseInfoWithFilter1.OnLicenseSelected += ctrlDriverLicenseInfoWithFilter1_OnLicenseSelected;

            lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName;
            lblApplicationFees.Text = clsBLApplicationType.Find((int)clsBLApplication.enApplicationType.ReleaseDetainedDrivingLicense).ApplicationFees.ToString();
        }
        private void ctrlDriverLicenseInfoWithFilter1_OnLicenseSelected(int obj)
        {
            _SelectedLicenseID = obj;
            lblLicenseID.Text = _SelectedLicenseID.ToString();

            if (_SelectedLicenseID == -1)
            {
                btnRelease.Enabled = false;
                llShowLicensesHistory.Enabled = false;
                return;
            }

            llShowLicensesHistory.Enabled = true;

            // 1. التحقق مما إذا كانت الرخصة محجوزة فعلاً
            if (!clsBLDetainedLicense.IsLicenseDetained(_SelectedLicenseID))
            {
                MessageBox.Show("Selected License is NOT detained, choose another one.", "Not allowed",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                btnRelease.Enabled = false;
                _ResetDetainInfo();
                return;
            }

            // 2. قراءة بيانات الحجز من جدول DetainedLicenses
            _DetainedLicense = clsBLDetainedLicense.FindByLicenseID(_SelectedLicenseID);

            if (_DetainedLicense == null)
            {
                MessageBox.Show("No detained license details found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnRelease.Enabled = false;
                return;
            }

            // 3. عرض بيانات الحجز والمبالغ المالية
            lblDetainID.Text = _DetainedLicense.DetainID.ToString();
            lblDetainDate.Text = _DetainedLicense.DetainDate.ToString("dd/MM/yyyy");
            lblFineFees.Text = _DetainedLicense.FineFees.ToString("N2");

            decimal applicationFees = Convert.ToDecimal(lblApplicationFees.Text);
            lblTotalFees.Text = (_DetainedLicense.FineFees + applicationFees).ToString("N2");

            btnRelease.Enabled = true;
        }

        private void _ResetDetainInfo()
        {
            lblDetainID.Text = "[???]";
            lblDetainDate.Text = "[??/??/????]";
            lblFineFees.Text = "[$$$$]";
            lblTotalFees.Text = "[$$$$]";
            lblApplicationID.Text = "[???]";
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

        private void btnRelease_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to release this detained license?", "Confirm",
        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            // 1. استدعاء ميثود فك الحجز وإنشاء الطلب من BLL
            int applicationID = clsBLDetainedLicense.ReleaseLicense(_SelectedLicenseID);

            if (applicationID == -1)
            {
                MessageBox.Show("Failed to Release the Detained License!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 2. عرض رقم الطلب وتأكيد النجاح
            lblApplicationID.Text = applicationID.ToString();

            MessageBox.Show($"Detained License Released Successfully with Application ID = {applicationID}",
                "License Released", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // 3. تعطيل وتفعيل عناصر الشاشة المناسبة
            btnRelease.Enabled = false;
            ctrlDriverLicenseInfoWithFilter1.FilterEnabled = false;
            llShowLicensesInfo.Enabled = true;

            // 4. تحديث حالة Is Detained في الكنترول مع فك الحدث مؤقتاً لتفادي ظهور رسالة التنبيه
            ctrlDriverLicenseInfoWithFilter1.OnLicenseSelected -= ctrlDriverLicenseInfoWithFilter1_OnLicenseSelected;
            ctrlDriverLicenseInfoWithFilter1.LoadLicenseInfo(_SelectedLicenseID);
            ctrlDriverLicenseInfoWithFilter1.OnLicenseSelected += ctrlDriverLicenseInfoWithFilter1_OnLicenseSelected;
        }
    }
}
