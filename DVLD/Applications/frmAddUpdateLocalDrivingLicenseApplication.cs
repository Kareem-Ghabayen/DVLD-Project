using BuisnessLayer;
using DVLD_BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Windows.Forms;

namespace DVLD.Applications
{
    public partial class frmAddUpdateLocalDrivingLicenseApplication : Form
    {
        public delegate void DataBackEventHandler(object sender, int LocalDrivingLicenseApplicationID);
        public event DataBackEventHandler DataBack;
        public enum Mode { AddNew = 0, Update = 1 };
        private Mode _Mode;

        private int _LocalDrivingLicenseApplicationID = -1;
        private clsBLLocalDrivingLicenseApplication _LocalDrivingLicenseApplication;

        public frmAddUpdateLocalDrivingLicenseApplication()
        {
            InitializeComponent();
            _Mode = Mode.AddNew;
        }

        public frmAddUpdateLocalDrivingLicenseApplication(int LocalDrivingLicenseApplicationID)
        {
            InitializeComponent();
            _Mode = Mode.Update;
            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
        }
        private void _FillLicenseClassesInComobox()
        {
            
    DataTable dtLicenseClasses = clsBLLicenseClass.GetAllLicenseClasses();

            cbLicenseClasses.DataSource = dtLicenseClasses;

            cbLicenseClasses.DisplayMember = "ClassName";

            cbLicenseClasses.ValueMember = "LicenseClassID";
        }
        private void _ResetDefaultValues()
        {
            _FillLicenseClassesInComobox();

            if (_Mode == Mode.AddNew)
            {
                this.Text = "New Local Driving License Application";
                lblTitle2.Text = "New Local Driving License Application";
                _LocalDrivingLicenseApplication = new clsBLLocalDrivingLicenseApplication();
                tpApplicationInfo.Enabled = false;
                btnSave.Enabled = false;
                ctrlPersonCardWithFilter1.FilterFocus();
            
            }
            else
            {
                this.Text = "Update Local Driving License Application";
                lblTitle2.Text = "Update Local Driving License Application";
                tpApplicationInfo.Enabled = true;
                btnSave.Enabled = true;
            }
            lblApplicationDate.Text = DateTime.Now.ToShortDateString();
            cbLicenseClasses.SelectedIndex = 2;
            lblFees.Text = clsBLApplicationType.Find((int)clsBLApplication.enApplicationType.NewDrivingLicense).ApplicationFees.ToString();
            lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName;
        }
        private void chkIsActive_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel1_Click(object sender, EventArgs e)
        {

        }
        private void _LoadData()
        {
            _LocalDrivingLicenseApplication = clsBLLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID(_LocalDrivingLicenseApplicationID);

            if (_LocalDrivingLicenseApplication == null)
            {
                MessageBox.Show("No Application with ID = " + _LocalDrivingLicenseApplicationID, "No Application Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();
                return;
            }

            lblLocalDrivingLicenseApplicationID.Text = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();

            cbLicenseClasses.SelectedValue = _LocalDrivingLicenseApplication.LicenseClassID;

            lblApplicationDate.Text = _LocalDrivingLicenseApplication.BaseApplicationInfo.ApplicationDate.ToShortDateString();
            lblFees.Text = _LocalDrivingLicenseApplication.BaseApplicationInfo.PaidFees.ToString();

            clsBLUser createdByUser = clsBLUser.FindByUserID(_LocalDrivingLicenseApplication.BaseApplicationInfo.CreatedByUserID);
            lblCreatedByUser.Text = (createdByUser != null) ? createdByUser.UserName : "N/A";

            ctrlPersonCardWithFilter1.LoadPersonInfo(_LocalDrivingLicenseApplication.BaseApplicationInfo.ApplicantPersonID);
            ctrlPersonCardWithFilter1.FilterEnabled = false;
        }
        private void frmAddUpdateLocalDrivingLicenseApplication_Load(object sender, EventArgs e)
        {
            _ResetDefaultValues();

            if (_Mode == Mode.Update)
            {
                _LoadData();
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (_Mode == Mode.Update)
            {
                btnSave.Enabled = true;
                tpApplicationInfo.Enabled = true;
                tcApplicationInfo.SelectedTab = tcApplicationInfo.TabPages["tpApplicationInfo"];
                return;
            }

            // في حالة الإضافة: التثبت من اختيار شخص أولاً
            if (ctrlPersonCardWithFilter1.PersonID != -1)
            {
                tpApplicationInfo.Enabled = true;
                btnSave.Enabled = true;
                tcApplicationInfo.SelectedTab = tcApplicationInfo.TabPages["tpApplicationInfo"];
            }
            else
            {
                MessageBox.Show("Please Select a Person", "Select a Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ctrlPersonCardWithFilter1.FilterFocus();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int personID = ctrlPersonCardWithFilter1.PersonID;

            if (personID == -1)
            {
                MessageBox.Show("Please select a person first!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int licenseClassID = Convert.ToInt32(cbLicenseClasses.SelectedValue);

            if (_Mode == Mode.AddNew)
            {
                if (clsBLApplication.IsThereAnActiveApplicationInSameLicenses(personID, (int)clsBLApplicationType.enApplicationType.NewDrivingLicense, licenseClassID))
                {
                    MessageBox.Show("Selected person already has an active application for this license class. Choose another class.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (clsBLLicense.IsLicenseExistByPersonIDAndLicenseClass(personID, licenseClassID))
                {
                    MessageBox.Show("Selected person already has a license for this class. Choose another class.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                _LocalDrivingLicenseApplication = clsBLLocalDrivingLicenseApplication.AddNewLocalDrivingLicenseApplication(personID, licenseClassID);

                if (_LocalDrivingLicenseApplication != null)
                {
                    lblLocalDrivingLicenseApplicationID.Text = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();
                    _Mode = Mode.Update;
                    lblTitle2.Text = "Update Local Driving License Application";
                    this.Text = "Update Local Driving License Application";

                    MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DataBack?.Invoke(this, _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID);
                }
                else
                {
                    MessageBox.Show("Error: Application could not be saved. Make sure person has no active application or existing license for this class.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {


                if (_LocalDrivingLicenseApplication.LicenseClassID != licenseClassID)
                {
                    if (clsBLApplication.IsThereAnActiveApplicationInSameLicenses(personID, (int)clsBLApplicationType.enApplicationType.NewDrivingLicense, licenseClassID))
                    {
                        MessageBox.Show("Selected person already has an active application for this license class. Choose another class.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (clsBLLicense.IsLicenseExistByPersonIDAndLicenseClass(personID, licenseClassID))
                    {
                        MessageBox.Show("Selected person already has a license for this class. Choose another class.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                // وضع التعديل (Update)
                _LocalDrivingLicenseApplication.LicenseClassID = licenseClassID;

                if (_LocalDrivingLicenseApplication.Save())
                {
                    MessageBox.Show("Data Updated Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Error: Data Was Not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void guna2HtmlLabel3_Click(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
