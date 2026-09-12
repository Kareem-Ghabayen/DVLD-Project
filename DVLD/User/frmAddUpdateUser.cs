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

namespace DVLD.User
{
    public partial class frmAddUpdateUser : Form
    {
        public delegate void DataBackEventHandler(object sender, int UserID);
        public event DataBackEventHandler DataBack;
        public enum enMode { AddNew = 0, Update = 1 }
        private enMode _Mode;

        private int _UserID = -1;
        private clsBLUser _User;
        public frmAddUpdateUser()
        {
            InitializeComponent();
            _Mode = enMode.AddNew;
        }

        public frmAddUpdateUser(int UserID)
        {
            InitializeComponent();
            _UserID = UserID;
            _Mode = enMode.Update;
        }
        //  تاهيل الازرار حسب المود تستدعى في ال  load  
        private void _ResetDefualtValues()
        {
            if (_Mode == enMode.AddNew)
            {
                lblTitle.Text = "Add New User";
                this.Text = "Add New User";
                _User = new clsBLUser();
                tpLoginInfo.Enabled = false; 
                btnSave.Enabled = false;
            }
            else
            {
                lblTitle.Text = "Update User";
                this.Text = "Update User";
                tpLoginInfo.Enabled = true;
                btnSave.Enabled = true;
            }

            lblUserIDValue.Text = "[????]";
            txtUserName.Text = "";
            txtPassword.Text = "";
            txtConfirmPassword.Text = "";
            chkIsActive.Checked = true;
        }
        //  ميثود تحميل بيانات الواجهة عند تشغيلها  
        private void _LoadData()
        {
            _User = clsBLUser.FindByUserID(_UserID);

            if (_User == null)
            {
                MessageBox.Show("No User with ID = " + _UserID, "User Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();
                return;
            }

            lblUserIDValue.Text = _User.UserID.ToString();
            txtUserName.Text = _User.UserName;
            txtPassword.Text = _User.Password;
            txtConfirmPassword.Text = _User.Password;
            chkIsActive.Checked = _User.IsActive;

            // تحميل بيانات الشخص في الكنترول وتجميد البحث عند التعديل
            ctrlPersonCardWithFilter1.LoadPersonInfo(_User.PersonID);
            ctrlPersonCardWithFilter1.FilterEnabled = false;
        }

        private void frmAddUpdateUser_Load(object sender, EventArgs e)
        {
                 _ResetDefualtValues();

            if (_Mode == enMode.Update)
                _LoadData();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (_Mode == enMode.Update)
            {
                btnSave.Enabled = true;
                tpLoginInfo.Enabled = true;
                tcUser.SelectedTab = tpLoginInfo;
                return;
            }

            if (ctrlPersonCardWithFilter1.PersonID != -1)
            {
                if (clsBLUser.FindByPersonID(ctrlPersonCardWithFilter1.PersonID)!=null)
                {
                    MessageBox.Show("Selected Person already has a user, choose another person.", "Select Another Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ctrlPersonCardWithFilter1.FilterFocus();
                }
                else
                {
                    btnSave.Enabled = true;
                    tpLoginInfo.Enabled = true;
                    tcUser.SelectedTab = tpLoginInfo;
                }
            }
            else
            {
                MessageBox.Show("Please Select a Person", "Select a Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ctrlPersonCardWithFilter1.FilterFocus();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fields are not valid!, put the mouse over the red icon(s) to see the error", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _User.PersonID = ctrlPersonCardWithFilter1.PersonID;
            _User.UserName = txtUserName.Text.Trim();
            _User.Password = txtPassword.Text.Trim();
            _User.IsActive = chkIsActive.Checked;

            if (_User.Save())
            {
                lblUserIDValue.Text = _User.UserID.ToString();
                _Mode = enMode.Update;
                lblTitle.Text = "Update User";
                this.Text = "Update User";
                DataBack?.Invoke(this, _User.UserID);
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtUserName_Validating(object sender, CancelEventArgs e)
        {
                if (string.IsNullOrEmpty(txtUserName.Text.Trim()))
    {
        e.Cancel = true;
        errorProvider1.SetError(txtUserName, "Username cannot be blank");
        return;
    }
    else
    {
        errorProvider1.SetError(txtUserName, null);
    }

    if (_Mode == enMode.AddNew)
    {
        if (clsBLUser.IsUserExist(txtUserName.Text.Trim()))
        {
            e.Cancel = true;
            errorProvider1.SetError(txtUserName, "username is used by another user");
        }
        else
        {
            errorProvider1.SetError(txtUserName, null);
        }
    }
    else
    {
        if (_User.UserName != txtUserName.Text.Trim() && clsBLUser.IsUserExist(txtUserName.Text.Trim()))
        {
            e.Cancel = true;
            errorProvider1.SetError(txtUserName, "username is used by another user");
        }
        else
        {
            errorProvider1.SetError(txtUserName, null);
        }
    }
        }

        private void txtPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtPassword.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtPassword, "Password cannot be blank");
            }
            else
            {
                errorProvider1.SetError(txtPassword, null);
            }
        }

        private void txtConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if (txtConfirmPassword.Text.Trim() != txtPassword.Text.Trim())
            {
                e.Cancel = true;
                errorProvider1.SetError(txtConfirmPassword, "Password Confirmation does not match Password!");
            }
            else
            {
                errorProvider1.SetError(txtConfirmPassword, null);
            }
        }

        private void ctrlPersonCardWithFilter1_Load(object sender, EventArgs e)
        {

        }
    }
}






