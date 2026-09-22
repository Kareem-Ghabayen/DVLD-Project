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
using System.Windows.Forms;

namespace DVLD.people
{
    public partial class frmManagePeople : Form
    {
        private DataTable _dtAllPeople;
        public frmManagePeople()
        {
            InitializeComponent();
        }
        private void _RefreshPeopleList()
        {
            _dtAllPeople = clsPerson.GetInitialData();

            dgvPeople.DataSource = _dtAllPeople;

            lblRecordsCount.Text = dgvPeople.Rows.Count.ToString();
        }
        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_dtAllPeople == null) return;
            if (cbFilterBy.Text == "None")
            {
                txtFilterValue.Visible = false;
                txtFilterValue.Text = "";
                _dtAllPeople.DefaultView.RowFilter = "";
                lblRecordsCount.Text = $"# Records: {dgvPeople.Rows.Count}";

            }
            else
            {
                txtFilterValue.Visible = true;
                txtFilterValue.Text = "";
                txtFilterValue.Focus();
            }
        }
        private void _Load()
        {
            cbFilterBy.SelectedIndex = 0;
            _dtAllPeople = clsPerson.GetInitialData();
            dgvPeople.DataSource = _dtAllPeople;
            lblRecordsCount.Text = $"# Records: {dgvPeople.Rows.Count}";
            _RefreshPeopleList();
        }


        private void frmManagePeople_Load(object sender, EventArgs e)
        {
            _Load();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            frmAddEditPerson frm = new frmAddEditPerson();
            frm.DataBack += Frm_DataBack;
            frm.Owner = this;

            frm.StartPosition = FormStartPosition.CenterParent;

            frm.ShowDialog(this);
            _RefreshPeopleList();
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string filterExpr = clsPerson.BuildFilterExpression(cbFilterBy.Text, txtFilterValue.Text);

            _dtAllPeople.DefaultView.RowFilter = filterExpr;

            lblRecordsCount.Text = $"# Records: {dgvPeople.Rows.Count}";
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int personID = (int)dgvPeople.CurrentRow.Cells["PersonID"].Value;

            frmAddEditPerson frm = new frmAddEditPerson(personID);
            frm.ShowDialog(Form.ActiveForm);
            _RefreshPeopleList();
        }
        private void Frm_DataBack(object sender, int PersonID)
        {
            _RefreshPeopleList();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvPeople.CurrentRow == null) return;

            int personID = (int)dgvPeople.CurrentRow.Cells["PersonID"].Value;

            if (MessageBox.Show($"Are you sure you want to delete Person [{personID}]?",
                                "Confirm Delete",
                                MessageBoxButtons.OKCancel,
                                MessageBoxIcon.Question) == DialogResult.OK)
            {
                if (clsBLSPeople.DeletePerson(personID))
                {
                    MessageBox.Show("Person Deleted Successfully.",
                                    "Successful",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);

                    _RefreshPeopleList(); 

                }
                else
                {
                    MessageBox.Show("Person was not deleted because it has data linked to it in the system.",
                                    "Error",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                }
            }
        }

        private void addNewPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddEditPerson frm = new frmAddEditPerson();
            frm.DataBack += Frm_DataBack;
            frm.Owner = this;

            frm.StartPosition = FormStartPosition.CenterParent;

            frm.ShowDialog(this);
            _RefreshPeopleList();
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvPeople.CurrentRow == null) return;

            int personID = (int)dgvPeople.CurrentRow.Cells["PersonID"].Value;

            frmShowPersonInfo frm = new frmShowPersonInfo(personID);
            frm.ShowDialog();

            _RefreshPeopleList();
        }
    }
}
