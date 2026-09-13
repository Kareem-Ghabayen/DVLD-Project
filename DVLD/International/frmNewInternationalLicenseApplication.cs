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
            llShowLicensesHistory.IsSelectionEnabled = false;
            llShowLicensesInfo.IsSelectionEnabled = false;
            llShowLicensesHistory.Cursor = Cursors.Hand;
            llShowLicensesInfo.Cursor = Cursors.Hand;
            ctrlDriverLicenseInfoWithFilter1.OnLicenseSelected += ctrlDriverLicenseInfoWithFilter1_OnLicenseSelected;
            lblApplicationDate.Text = DateTime.Now.ToShortDateString();
            lblIssueDate.Text = DateTime.Now.ToShortDateString();
            lblExpirationDate.Text = DateTime.Now.AddYears(1).ToShortDateString();
            lblFees.Text = clsBLApplicationType.Find((int)clsBLApplicationType.enApplicationType.NewInternationalLicense).ApplicationFees.ToString();
            lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName;
        }
   
private void ctrlDriverLicenseInfoWithFilter1_OnLicenseSelected(int LocalLicenseID)
        {
            int selectedLicenseID = ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.LicenseID;

            lblLocalLicenseID.Text = selectedLicenseID.ToString();
            llShowLicensesHistory.Enabled = (selectedLicenseID != -1);

            if (selectedLicenseID == -1)
            {
                return;
            }

            // 1. الفحص الأول: التأكد من أن فئة الرخصة هي الفئة الثالثة (Class 3)
            if (ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.LicenseClass != 3)
            {
                MessageBox.Show("Selected License should be Class 3, please select another license.", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnIssue.Enabled = false;
                return;
            }

            // 2. الفحص الثاني: البحث عن رخصة دولية نشطة سابقة لنفس السائق
            clsBLInternationalLicense activeInternationalLicense =
                clsBLInternationalLicense.GetActiveInternationalLicenseByDriverID(ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.DriverID);

            if (activeInternationalLicense != null)
            {
                MessageBox.Show($"Person already has an active international license with ID = {activeInternationalLicense.InternationalLicenseID}",
                                "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);

                // تخزين الرقم وجلب البيانات لملء اللابلز بالأسفل
                _InternationalLicenseID = activeInternationalLicense.InternationalLicenseID;
                lblInternationalLicenseID.Text = activeInternationalLicense.InternationalLicenseID.ToString();
                lblApplicationID.Text = activeInternationalLicense.ApplicationID.ToString();

                btnIssue.Enabled = false;
                llShowLicensesInfo.Enabled = true; // تفعيل الرابط لفتح شاشة التفاصيل
                return;
            }

            // 3. في حال عدم وجود رخصة دولية نشطة (جاهز للإصدار الجديد)
            btnIssue.Enabled = true;
            llShowLicensesInfo.Enabled = false;
            lblInternationalLicenseID.Text = "[???]";
            lblApplicationID.Text = "[???]";
            _InternationalLicenseID = -1;
        
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
            frmShowInternationalLicenseInfo frm = new frmShowInternationalLicenseInfo(_InternationalLicenseID);
            frm.ShowDialog();
        }

        private void frmNewInternationalLicenseApplication_Shown(object sender, EventArgs e)
        {
            ctrlDriverLicenseInfoWithFilter1.FilterFocus();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
