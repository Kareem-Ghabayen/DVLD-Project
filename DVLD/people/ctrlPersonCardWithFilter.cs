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
    public partial class ctrlPersonCardWithFilter : UserControl
    {
        public void FilterFocus()
        {
            txtFilterValue.Focus();
        }
        public event Action<int> OnPersonSelected;

        public int PersonID
        {
            get { return ctrlPersonCard1.PersonID; }
        }

        public bool FilterEnabled
        {
            get { return gbFilter.Enabled; }
            set { gbFilter.Enabled = value; }
        }
        public ctrlPersonCardWithFilter()
        {
            InitializeComponent();
        }

        private void ctrlPersonCardWithFilter_Load(object sender, EventArgs e)
        {
            cbFilterBy.SelectedIndex = 0;
            txtFilterValue.Focus();
        }

        private void _FindNow()
        {
            clsBLSPeople person = null;

            switch (cbFilterBy.Text)
            {
                case "Person ID":
                    if (int.TryParse(txtFilterValue.Text.Trim(), out int personID))
                        person = clsBLSPeople.FindByID(personID);
                    break;

                case "National No":
                    person = clsBLSPeople.FindByNationalNo(txtFilterValue.Text.Trim());
                    break;
            }

            if (person != null)
            {
                ctrlPersonCard1.LoadPersonInfo(person.ID);
            }
            else
            {
                MessageBox.Show("No Person Found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (OnPersonSelected != null)
                OnPersonSelected(ctrlPersonCard1.PersonID);
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtFilterValue.Text.Trim()))
                return;

            _FindNow();
        }





        private void DataBackEvent(object sender, int PersonID)
        {
            cbFilterBy.SelectedIndex = 1; 
            txtFilterValue.Text = PersonID.ToString();
            ctrlPersonCard1.LoadPersonInfo(PersonID);

            if (OnPersonSelected != null)
                OnPersonSelected(PersonID);
        }

        private void btnAddPerson_Click(object sender, EventArgs e)
        {
            frmAddEditPerson frm = new frmAddEditPerson();
            frm.DataBack += DataBackEvent; 
            frm.ShowDialog();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Text = "";
            txtFilterValue.Focus();
        }




        public void LoadPersonInfo(int PersonID)
        {
            cbFilterBy.SelectedIndex = 1;
            txtFilterValue.Text = PersonID.ToString();
            _FindNow();
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.Text == "Person ID")
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }

            if (e.KeyChar == (char)Keys.Enter)
            {
                btnFind.PerformClick();
            }
        }

        private void gbFilter_Click(object sender, EventArgs e)
        {

        }
    }
}
