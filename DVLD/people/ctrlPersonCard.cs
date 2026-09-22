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

namespace DVLD.people
{
    public partial class ctrlPersonCard : UserControl
    {
        private clsBLSPeople _Person;
        private int _PersonID = -1;

        public int PersonID => _PersonID;

        public ctrlPersonCard()
        {
            InitializeComponent();
        }

        public void LoadPersonInfo(int PersonID)
        {
            _Person = clsBLSPeople.FindByID(PersonID);
            if (_Person == null)
            {
                ResetPersonInfo();
                MessageBox.Show("No Person with PersonID = " + PersonID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _FillPersonInfo();
        }

        public void LoadPersonInfo(string NationalNo)
        {
            _Person = clsBLSPeople.FindByNationalNo(NationalNo);
            if (_Person == null)
            {
                ResetPersonInfo();
                MessageBox.Show("No Person with National No. = " + NationalNo, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _FillPersonInfo();
        }

        private void _FillPersonInfo()
        {
            _PersonID = _Person.ID;
            lblPersonID.Text = _Person.ID.ToString();
            lblNationalNo.Text = _Person.NationalNo;
            lblFullName.Text = _Person.FirstName + " " + _Person.SecondName + " " + _Person.ThirdName; 
            lblGendor.Text = _Person.Gendor == 0 ? "Male" : "Female";
            lblEmail.Text = _Person.Email;
            lblPhone.Text = _Person.Phone;
            lblDateOfBirth.Text = _Person.DateOfBirth.ToShortDateString();
            lblCountry.Text = clsBLCountry.FindByCountryID(_Person.NationalityCountryID).CountryName;
            lblAddress.Text = _Person.Address;

            if (!string.IsNullOrEmpty(_Person.ImagePath) && System.IO.File.Exists(_Person.ImagePath))
                pbPersonImage.ImageLocation = _Person.ImagePath;
            else
                pbPersonImage.Image = _Person.Gendor == 0 ? Properties.Resources.Male_512 : Properties.Resources.Male_512;

            
            llEditPersonInfo.Enabled = true;
        }

        public void ResetPersonInfo()
        {
            _PersonID = -1;
            lblPersonID.Text = "[???]";
            lblNationalNo.Text = "[???]";
            lblFullName.Text = "[???]";
            lblGendor.Text = "[???]";
            lblEmail.Text = "[???]";
            lblPhone.Text = "[???]";
            lblDateOfBirth.Text = "[???]";
            lblCountry.Text = "[???]";
            lblAddress.Text = "[???]";
            pbPersonImage.Image = Properties.Resources.Male_512;
            llEditPersonInfo.Enabled = false;
        }

        private void llEditPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmAddEditPerson frm = new frmAddEditPerson(_PersonID);
            frm.DataBack += (s, personID) => LoadPersonInfo(personID);
            frm.ShowDialog();
        }

        private void llEditPersonInfo_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmAddEditPerson frm = new frmAddEditPerson(_PersonID);
            frm.DataBack += (s, personID) => LoadPersonInfo(personID);
            frm.ShowDialog();
        }
    }
}