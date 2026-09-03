using System;
using System.ComponentModel;
using System.Windows.Forms;
using BuisnessLayer; // تأكد من اسم الـ Namespace الخاص بالبزنس لاير عندك

namespace DVLD.User
{
    public partial class frmChangePassword : Form
    {
        private int _UserID = -1;
        private clsBLUser _User;

        // المشد يستقبل UserID عند فتح الشاشة
        public frmChangePassword(int UserID)
        {
            InitializeComponent();
            _UserID = UserID;
        }

        private void frmChangePassword_Load(object sender, EventArgs e)
        {
            _User = clsBLUser.FindByUserID(_UserID);

            if (_User == null)
            {
                MessageBox.Show("No User with UserID = " + _UserID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            // تحميل بيانات المستخدم داخل الكنترول العلوي
            ctrlUserCard1.LoadUserInfo(_UserID);
        }

  

  



        private void btnSave_Click(object sender, EventArgs e)
        {

            // تصفير الأخطاء السابقة
            errorProvider1.Clear();

            // 1. التحقق من أن كلمة السر الحالية ليست فارغة
            if (string.IsNullOrEmpty(txtCurrentPassword.Text.Trim()))
            {
                errorProvider1.SetError(txtCurrentPassword, "Current Password cannot be empty!");
                txtCurrentPassword.Focus();
                return;
            }

            // 2. التحقق من أن كلمة السر الحالية صحيحة وتطابق كلمة سر المستخدم
            if (_User.Password != txtCurrentPassword.Text.Trim())
            {
                errorProvider1.SetError(txtCurrentPassword, "Current Password is wrong!");
                txtCurrentPassword.Focus();
                return;
            }

            // 3. التحقق من أن كلمة السر الجديدة ليست فارغة
            if (string.IsNullOrEmpty(txtNewPassword.Text.Trim()))
            {
                errorProvider1.SetError(txtNewPassword, "New Password cannot be empty!");
                txtNewPassword.Focus();
                return;
            }

            // 4. التحقق من أن تأكيد كلمة السر ليس فارغاً
            if (string.IsNullOrEmpty(txtConfirmPassword.Text.Trim()))
            {
                errorProvider1.SetError(txtConfirmPassword, "Confirm Password cannot be empty!");
                txtConfirmPassword.Focus();
                return;
            }

            // 5. التحقق من تطابق كلمة السر الجديدة مع التأكيد
            if (txtNewPassword.Text.Trim() != txtConfirmPassword.Text.Trim())
            {
                errorProvider1.SetError(txtConfirmPassword, "Password Confirmation does not match New Password!");
                txtConfirmPassword.Focus();
                return;
            }

            // --- إذا مرت كل التحققات بنجاح، يتم الحفظ الآن ---
            _User.Password = txtNewPassword.Text.Trim();

            if (_User.Save())
            {
                MessageBox.Show("Password Changed Successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnSave.Enabled = false; // تعطيل الزر لتفادي التكرار
            }
            else
            {
                MessageBox.Show("Password was not saved successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtCurrentPassword_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCurrentPassword.Text.Trim()))
            {
                //e.Cancel = true;
                errorProvider1.SetError(txtCurrentPassword, "Current Password cannot be empty!");
            }
            else if (_User.Password != txtCurrentPassword.Text.Trim())
            {
                //e.Cancel = true;
                errorProvider1.SetError(txtCurrentPassword, "Current Password is wrong!");
            }
            else
            {
                errorProvider1.SetError(txtCurrentPassword, null);
            }
        }

        private void txtNewPassword_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNewPassword.Text.Trim()))
            {
                //e.Cancel = true;
                errorProvider1.SetError(txtNewPassword, "New Password cannot be empty!");
            }
            else
            {
                errorProvider1.SetError(txtNewPassword, null);
            }
        }

        private void txtConfirmPassword_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtConfirmPassword.Text.Trim()))
            {
                //e.Cancel = true;
                errorProvider1.SetError(txtConfirmPassword, "Confirm Password cannot be empty!");
            }
            else if (txtConfirmPassword.Text.Trim() != txtNewPassword.Text.Trim())
            {
                //e.Cancel = true;
                errorProvider1.SetError(txtConfirmPassword, "Password Confirmation does not match New Password!");
            }
            else
            {
                errorProvider1.SetError(txtConfirmPassword, null);
            }
        }
    }
}