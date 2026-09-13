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

namespace DVLD.International
{
    public partial class frmNewInternationalLicenseApplication : Form
    {
        private int _InternationalLicenseID = -1;

        public frmNewInternationalLicenseApplication()
        {
            InitializeComponent();
        }

        private void frmNewInternationalLicenseApplication_Load(object sender, EventArgs e)
        {
            ctrlDriverLicenseInfoWithFilter1.OnLicenseSelected += ctrlDriverLicenseInfoWithFilter1_OnLicenseSelected;
            lblApplicationDate.Text = DateTime.Now.ToShortDateString();
            lblIssueDate.Text = DateTime.Now.ToShortDateString();
            lblExpirationDate.Text = DateTime.Now.AddYears(1).ToShortDateString();
            lblFees.Text = clsBLApplicationType.Find((int)clsBLApplicationType.enApplicationType.NewInternationalLicense).ApplicationFees.ToString();
            lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName;
        }
        private void ctrlDriverLicenseInfoWithFilter1_OnLicenseSelected(int LocalLicenseID)
        {
            if (LocalLicenseID == -1)
            {
                btnIssue.Enabled = false;
                return;
            }

            lblLocalLicenseID.Text = LocalLicenseID.ToString();
            llShowLicensesHistory.Enabled = true;

            // الشرط 1: يجب أن تكون الرخصة من الفئة 3 (Ordinary driving license)
            if (ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.LicenseClass != 3)
            {
                MessageBox.Show("License class must be Ordinary License Class 3!", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnIssue.Enabled = false;
                return;
            }

            // الشرط 2: هل الرخصة فاعلة؟
            if (!ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.IsActive)
            {
                MessageBox.Show("Selected License is not Active!", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnIssue.Enabled = false;
                return;
            }

            // الشرط 3: هل تمتلك السائق رخصة دولية سارية مسبقاً؟
            // 1. استقبال الكائن مباشرة
            clsBLInternationalLicense ActiveInternationalLicense = clsBLInternationalLicense.GetActiveInternationalLicenseByDriverID(ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.DriverID);

            // 2. الفحص إذا كان الكائن موجوداً (أي يمتلك رخصة دولية فاعلة)
            if (ActiveInternationalLicense != null)
            {
                int ActiveInternationalLicenseID = ActiveInternationalLicense.InternationalLicenseID;

                MessageBox.Show($"Person already has an active international license with ID = {ActiveInternationalLicenseID}", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblInternationalLicenseID.Text = ActiveInternationalLicenseID.ToString();
                llShowLicensesInfo.Enabled = true;
                btnIssue.Enabled = false;
                return;
            }

            // تفعيل زر الإصدار إذا اجتازت كل الشروط
            btnIssue.Enabled = true;
        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to issue International License?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            // 1. جلب رقم الهوية (NationalNo) للرخصة الحالية المختارة
            string nationalNo = ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.DriverInfo.PersonInfo.NationalNo;

            // 2. استدعاء ميثود الإصدار الخاصة بك مباشرة من طبقة البزنس
            int internationalLicenseID = clsBLInternationalLicense.IssueInternationalLicenseByNationalNo(nationalNo);

            // 3. التحقق من نجاح العملية
            if (internationalLicenseID != -1)
            {
                _InternationalLicenseID = internationalLicenseID;

                // جلب كائن الرخصة الدولية الصادرة لعرض رقم الطلب (ApplicationID)
                clsBLInternationalLicense internationalLicense = clsBLInternationalLicense.Find(internationalLicenseID);

                if (internationalLicense != null)
                {
                    lblApplicationID.Text = internationalLicense.ApplicationID.ToString();
                    lblInternationalLicenseID.Text = internationalLicense.InternationalLicenseID.ToString();
                }

                MessageBox.Show($"International License Issued Successfully with ID = {internationalLicenseID}", "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // تعطيل/تفعيل عناصر الواجهة بعد الإصدار
                btnIssue.Enabled = false;
                ctrlDriverLicenseInfoWithFilter1.FilterEnabled = false;
                llShowLicensesInfo.Enabled = true;
            }
            else
            {
                MessageBox.Show("Failed to Issue International License!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void llShowLicensesHistory_Click(object sender, EventArgs e)
        {
            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.DriverInfo.PersonID);
            frm.ShowDialog();
        }

        private void llShowLicensesInfo_Click(object sender, EventArgs e)
        {
            //frmShowInternationalLicenseInfo frm = new frmShowInternationalLicenseInfo(_InternationalLicenseID);
            //frm.ShowDialog();
        }

        private void frmNewInternationalLicenseApplication_Shown(object sender, EventArgs e)
        {
            ctrlDriverLicenseInfoWithFilter1.FilterFocus();
        }
    }
}
