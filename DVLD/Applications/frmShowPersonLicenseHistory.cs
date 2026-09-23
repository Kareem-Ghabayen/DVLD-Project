using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Applications
{
    public partial class frmShowPersonLicenseHistory : Form
    {
        private int _PersonID = -1;

        public frmShowPersonLicenseHistory()
        {
            InitializeComponent();
        }

        public frmShowPersonLicenseHistory(int PersonID)
        {
            InitializeComponent();
            _PersonID = PersonID;
        }
        private void lblRecordsCount_Click(object sender, EventArgs e)
        {

        }

        private void frmShowPersonLicenseHistory_Load(object sender, EventArgs e)
        {
            if (_PersonID != -1)
            {
                ctrlPersonCardWithFilter1.LoadPersonInfo(_PersonID);
                ctrlPersonCardWithFilter1.FilterEnabled = false;

            }
            else
            {
                ctrlPersonCardWithFilter1.FilterEnabled = true;
                ctrlPersonCardWithFilter1.FilterFocus();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ctrlPersonCardWithFilter1_Load(object sender, EventArgs e)
        {

        }

        private void ctrlPersonCardWithFilter1_OnPersonSelected(int PersonID)
        {
            _PersonID = PersonID;

            if (_PersonID == -1)
            {
                B.Clear();
            }
            else
            {
                B.LoadInfoByPersonID(_PersonID);
            }
        }

        private void ctrlPersonCardWithFilter1_OnPersonSelected_1(int PersonID)
        {
            _PersonID = PersonID;

            if (_PersonID == -1)
            {
                B.Clear();
            }
            else
            {
                B.LoadInfoByPersonID(_PersonID);
            }
        }
    }
}
