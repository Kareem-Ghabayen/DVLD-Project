using BuisnessLayer;
using DVLD.people;
using System;
using System.Data;
using System.Windows.Forms;

namespace DVLD
{
    public partial class frmListDrivers : Form
    {
        private DataTable _dtDrivers;

        public frmListDrivers()
        {
            InitializeComponent();
        }

        private void _RefreshDriversList()
        {
            _dtDrivers = clsBLDriver.GetAllDrivers();
            dgvDrivers.DataSource = _dtDrivers;
            lblRecordsCount.Text = dgvDrivers.Rows.Count.ToString();

            //if (dgvDrivers.Rows.Count > 0)
            //{
            //    dgvDrivers.Columns[0].HeaderText = "Driver ID";
            //    dgvDrivers.Columns[0].Width = 120;

            //    dgvDrivers.Columns[1].HeaderText = "Person ID";
            //    dgvDrivers.Columns[1].Width = 120;

            //    dgvDrivers.Columns[2].HeaderText = "National No";
            //    dgvDrivers.Columns[2].Width = 140;

            //    dgvDrivers.Columns[3].HeaderText = "Full Name";
            //    dgvDrivers.Columns[3].Width = 280;

            //    dgvDrivers.Columns[4].HeaderText = "Date";
            //    dgvDrivers.Columns[4].Width = 180;

            //    dgvDrivers.Columns[5].HeaderText = "Active Licenses";
            //    dgvDrivers.Columns[5].Width = 140;
            //}
        }










        private void frmListDrivers_Load_1(object sender, EventArgs e)
        {
            cbFilterBy.SelectedIndex = 0;
            _RefreshDriversList();
        }

        private void txtFilterValue_TextChanged_1(object sender, EventArgs e)
        {
            string filterColumn = "";

            switch (cbFilterBy.Text)
            {
                case "Driver ID":
                    filterColumn = "DriverID";
                    break;
                case "Person ID":
                    filterColumn = "PersonID";
                    break;
                case "National No":
                    filterColumn = "NationalNo";
                    break;
                case "Full Name":
                    filterColumn = "FullName";
                    break;
                default:
                    filterColumn = "None";
                    break;
            }

            if (txtFilterValue.Text.Trim() == "" || filterColumn == "None")
            {
                _dtDrivers.DefaultView.RowFilter = "";
                lblRecordsCount.Text = dgvDrivers.Rows.Count.ToString();
                return;
            }

            // تصفية الأرقام مقال النصوص
            if (filterColumn == "DriverID" || filterColumn == "PersonID")
                _dtDrivers.DefaultView.RowFilter = string.Format("[{0}] = {1}", filterColumn, txtFilterValue.Text.Trim());// '   يتم التعويض بالتوالي حتى لا يحدث اي لبس  مع الداتا جريد فيو
            else
                _dtDrivers.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", filterColumn, txtFilterValue.Text.Trim());

            lblRecordsCount.Text = dgvDrivers.Rows.Count.ToString();
        }

        private void btnClose_Click_1(object sender, EventArgs e)
        {
            this.Close();

        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //int personID = (int)dgvDrivers.CurrentRow.Cells["PersonID"].Value;
            //frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(personID);
            //frm.ShowDialog();
        }

        private void addNewPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int personID = (int)dgvDrivers.CurrentRow.Cells["PersonID"].Value;
            frmShowPersonInfo frm = new frmShowPersonInfo(personID);
            frm.ShowDialog();
            _RefreshDriversList();
        }

        private void txtFilterValue_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            // السماح بالأرقام فقط للحقول الرقمية
            if (cbFilterBy.Text == "Driver ID" || cbFilterBy.Text == "Person ID")
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }
        }

        private void cbFilterBy_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            txtFilterValue.Visible = (cbFilterBy.Text != "None");

            if (txtFilterValue.Visible)
            {
                txtFilterValue.Text = "";
                txtFilterValue.Focus();
            }
             //  هاد مهمة عشان لما تغير قيمة الفلتر يرجع كل اشي للديفولت 
            if (_dtDrivers != null)
                _dtDrivers.DefaultView.RowFilter = "";

            lblRecordsCount.Text = dgvDrivers.Rows.Count.ToString();
        }
    }
}
