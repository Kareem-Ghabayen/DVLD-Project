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

namespace DVLD.International
{
    public partial class ctrlDriverLicenseInfoWithFilter : UserControl
    {
        public delegate void LicenseSelected(int LicenseID);

        // 2. متغير من نوع الدليجيت تشترك فيه الشاشات الخارجية
        public event LicenseSelected OnLicenseSelected;

        // خصائص الكنترول
        private bool _FilterEnabled = true;
        public bool FilterEnabled
        {
            get { return _FilterEnabled; }
            set
            {
                _FilterEnabled = value;
                gbFilter.Enabled = _FilterEnabled;
            }
        }

        public int LicenseID => ctrlDriverLicenseInfo1.LicenseID;
        public clsBLLicense SelectedLicenseInfo => ctrlDriverLicenseInfo1.SelectedLicenseInfo;

        public ctrlDriverLicenseInfoWithFilter()
        {
            InitializeComponent();
        }
        // 3. دالة شحن رقم رخصة برمجياً (للاستخدام اختياري مستقبلاً)
        public void LoadLicenseInfo(int LicenseID)
        {
            txtLicenseID.Text = LicenseID.ToString();
            FindNow();
        }
        private void FindNow()
        {
            if (string.IsNullOrEmpty(txtLicenseID.Text.Trim()))
            {
                MessageBox.Show("Please enter a License ID!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int licenseID = int.Parse(txtLicenseID.Text.Trim());
            ctrlDriverLicenseInfo1.LoadInfo(licenseID);

            if (ctrlDriverLicenseInfo1.LicenseID != -1)
            {
                OnLicenseSelected?.Invoke(ctrlDriverLicenseInfo1.LicenseID);
            }
            else
            {
                OnLicenseSelected?.Invoke(-1); // تنبيه الشاشة الرئيسية لتطفي الزر!
            }
        }
        public void FilterFocus()
        {
            txtLicenseID.Focus();
        }
        private void gbFilter_Click(object sender, EventArgs e)
        {

        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            FindNow();
        }

        private void txtLicenseID_KeyPress(object sender, KeyPressEventArgs e)
        {
            {
                // منع إدخال غير الأرقام
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);

                // التنفيذ عند الضغط على Enter
                if (e.KeyChar == (char)13)
                {
                    btnFind.PerformClick();
                }
            }
        }
    }
}




