using System;
using System.ComponentModel;
using System.Windows.Forms;
using BuisnessLayer; 

namespace DVLD.User
{
    public partial class frmChangePassword : Form
    {
        private int _UserID = -1;
        private clsBLUser _User;

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


            ctrlUserCard1.LoadUserInfo(_UserID);
        }

  

  



        private void btnSave_Click(object sender, EventArgs e)
        {

            errorProvider1.Clear();

            if (string.IsNullOrEmpty(txtCurrentPassword.Text.Trim()))
            {
                errorProvider1.SetError(txtCurrentPassword, "Current Password cannot be empty!");
                txtCurrentPassword.Focus();
                return;
            }

            if (_User.Password != txtCurrentPassword.Text.Trim())
            {
                errorProvider1.SetError(txtCurrentPassword, "Current Password is wrong!");
                txtCurrentPassword.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtNewPassword.Text.Trim()))
            {
                errorProvider1.SetError(txtNewPassword, "New Password cannot be empty!");
                txtNewPassword.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtConfirmPassword.Text.Trim()))
            {
                errorProvider1.SetError(txtConfirmPassword, "Confirm Password cannot be empty!");
                txtConfirmPassword.Focus();
                return;
            }

            if (txtNewPassword.Text.Trim() != txtConfirmPassword.Text.Trim())
            {
                errorProvider1.SetError(txtConfirmPassword, "Password Confirmation does not match New Password!");
                txtConfirmPassword.Focus();
                return;
            }

            _User.Password = txtNewPassword.Text.Trim();

            if (_User.Save())
            {
                MessageBox.Show("Password Changed Successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnSave.Enabled = false; 
        
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
                errorProvider1.SetError(txtCurrentPassword, "Current Password cannot be empty!");
            }
            else if (_User.Password != txtCurrentPassword.Text.Trim())
            {
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
                errorProvider1.SetError(txtConfirmPassword, "Confirm Password cannot be empty!");
            }
            else if (txtConfirmPassword.Text.Trim() != txtNewPassword.Text.Trim())
            {
                errorProvider1.SetError(txtConfirmPassword, "Password Confirmation does not match New Password!");
            }
            else
            {
                errorProvider1.SetError(txtConfirmPassword, null);
            }
        }
    }
}