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
using System.ComponentModel;
namespace DVLD.people
{
    public partial class frmAddEditPerson : Form
    {
        public enum enMode { AddNew = 0, Update = 1 };
        private enMode _Mode;
        private int _PersonID;
        private clsBLSPeople _Person;
        public frmAddEditPerson()
        {
            InitializeComponent();
            _Mode = enMode.AddNew;
            _PersonID = -1;
        }

        public frmAddEditPerson(int PersonID)
        {
            InitializeComponent();
            _Mode = enMode.Update;
            _PersonID = PersonID;
        }
        public delegate void DataBackEventHandler(object sender, int PersonID);

        public event DataBackEventHandler DataBack;
        private void _FillCountriesInComboBox()
        {
            DataTable dtCountries = clsBLCountry.GetAllCountries();

            cbCountry.DataSource = dtCountries;
            cbCountry.DisplayMember = "CountryName"; // الاسم الظاهر للمستخدم
            cbCountry.ValueMember = "CountryID";

        }
        private void _ResetDefaultValues()
        {
            // تعبئة ComboBox الدول
            _FillCountriesInComboBox();

            if (_Mode == enMode.AddNew)
            {
                guna2HtmlLabel1.Text = "Add New Person";
                _Person = new clsBLSPeople();
            }
            else
            {
                guna2HtmlLabel1.Text = "Edit Person ID = " + _PersonID;
            }

            txtFirstName.Text = "";
            txtSecondName.Text = "";
            txtThirdName.Text = "";
            txtLastName.Text = "";
            txtNationalNo.Text = "";
            txtPhone.Text = "";
            txtEmail.Text = "";
            guna2TextBox1.Text = "";
            guna2RadioButton1.Checked = true;
            dtpDateOfBirth.Value = DateTime.Now.AddYears(-18); 
            llRemoveImage.Visible = false;
        }
        private void _LoadData()
        {
            _ResetDefaultValues();

            // إذا كنا في وضع الإضافة نكتفي بالقيم الافتراضية ونخرج
            if (_Mode == enMode.AddNew)
                return;

            // في وضع التعديل: جلب بيانات الشخص من البزنس
            _Person = clsBLSPeople.FindByID(_PersonID);

            if (_Person == null)
            {
                MessageBox.Show("No Person with ID = " + _PersonID, "Person Not Found",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            // تعبئة عناصر الشاشة بالبيانات المجلوبة
            lbPersonID.Text = "Person ID:" +_Person.ID.ToString();
            txtFirstName.Text = _Person.FirstName;
            txtSecondName.Text = _Person.SecondName;
            txtThirdName.Text = _Person.ThirdName;
            txtLastName.Text = _Person.LastName;
            txtNationalNo.Text = _Person.NationalNo;
            txtPhone.Text = _Person.Phone;
            txtEmail.Text = _Person.Email;
            guna2TextBox1.Text = _Person.Address;
            dtpDateOfBirth.Value = _Person.DateOfBirth;

            if (_Person.Gendor == 0)
                guna2RadioButton1.Checked = true;
            else
                rbFemale.Checked = true;

            // تحديد الدولة المناسبة
            cbCountry.SelectedValue = _Person.NationalityCountryID;
            if (_Person.ImagePath != "")
            {
                pbPersonImage.ImageLocation = _Person.ImagePath;
                llRemoveImage.Visible = true;
            }
        }
        private void lbNA_Click(object sender, EventArgs e)
        {

        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void frmAddEditPerson_Shown(object sender, EventArgs e)
        {

        }

        private void frmAddEditPerson_Load(object sender, EventArgs e)
        {
            _LoadData();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void llSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // تعريف وإنشاء الكائن برمجياً
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string selectedFilePath = openFileDialog1.FileName;
                pbPersonImage.Load(selectedFilePath);
                llRemoveImage.Visible = true;
            }
        }

        private void llRemoveImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pbPersonImage.ImageLocation = null;

            // إعادة الصورة الافتراضية حسب الجنس
            if (guna2RadioButton1.Checked)
                pbPersonImage.Image = Properties.Resources.Male_512;
            else
                pbPersonImage.Image = Properties.Resources.Male_512;

            llRemoveImage.Visible = false;
        }

        private void txtNationalNo_TextChanged(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtNationalNo.Text.Trim()))
    {
        e.Cancel = true;
        errorProvider1.SetError(txtNationalNo, "This field is required!");
        return;
    }
    else
    {
        errorProvider1.SetError(txtNationalNo, null);
    }

    // التحقق من عدم تكرار الرقم الوطني
    if (txtNationalNo.Text.Trim() != _Person.NationalNo && clsBLSPeople.IsPersonExistByNationalNo(txtNationalNo.Text.Trim()))
    {
        e.Cancel = true;
        errorProvider1.SetError(txtNationalNo, "National Number is used for another person!");
    }
    else
    {
        errorProvider1.SetError(txtNationalNo, null);
    }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
      
            // 1. فحص جميع عناصر Validation في الشاشة
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fields are not valid! Put the mouse over the red icon(s) to see the error",
                                "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 2. تعبئة البيانات في كائن _Person
            _Person.FirstName = txtFirstName.Text.Trim();
            _Person.SecondName = txtSecondName.Text.Trim();
            _Person.ThirdName = txtThirdName.Text.Trim();
            _Person.LastName = txtLastName.Text.Trim();
            _Person.NationalNo = txtNationalNo.Text.Trim();
            _Person.Phone = txtPhone.Text.Trim();
            _Person.Email = txtEmail.Text.Trim();
            _Person.Address = guna2TextBox1.Text.Trim();
            _Person.DateOfBirth = dtpDateOfBirth.Value;
            _Person.Gendor = guna2RadioButton1.Checked ? (byte)0 : (byte)1;
            _Person.NationalityCountryID = (int)cbCountry.SelectedValue;

            if (pbPersonImage.ImageLocation != null)
                _Person.ImagePath = pbPersonImage.ImageLocation;
            else
                _Person.ImagePath = "";

            // 3. الحفظ في قاعدة البيانات
            if (_Person.Save())
            {
                guna2HtmlLabel1.Text = "Edit Person ID = " + _Person.ID;
                lbPersonID.Text = "Person ID:" + _Person.ID.ToString();
                _Mode = enMode.Update; // التحويل لوضع التعديل فوراً بعد الإضافة

                MessageBox.Show("Data Saved Successfully.", "Saved",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                DataBack?.Invoke(this, _Person.ID);
            }
            else
            {
                MessageBox.Show("Error: Data Was not Saved Successfully.", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
    }

