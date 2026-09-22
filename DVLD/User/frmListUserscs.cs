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
    public partial class frmListUserscs : Form
    {
        private static DataTable _dtAllUsers;
        public frmListUserscs()
        {
            InitializeComponent();
        }
        private void _RefreshUsersList()
        {
            _dtAllUsers = clsBLUser.GetAllUsers(); 
            dgvUsers.DataSource = _dtAllUsers;

            lblRecordsCount.Text = dgvUsers.Rows.Count.ToString();

        }
        private void frmListUserscs_Load(object sender, EventArgs e)
        {
            cbFilterBy.SelectedIndex = 0; 
            _RefreshUsersList();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilterBy.Text == "Is Active")
            {
                txtFilterValue.Visible = false;
                cbIsActive.Visible = true;
                cbIsActive.Focus();
                cbIsActive.SelectedIndex = 0; 
            }
            else
            {
                txtFilterValue.Visible = (cbFilterBy.Text != "None");
                cbIsActive.Visible = false;

                txtFilterValue.Text = "";
                txtFilterValue.Focus();
            }
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";

            switch (cbFilterBy.Text)
            {
                case "User ID":
                    FilterColumn = "UserID";
                    break;
                case "Person ID":
                    FilterColumn = "PersonID";
                    break;
                case "UserName":
                    FilterColumn = "UserName";
                    break;
                case "Full Name":
                    FilterColumn = "FullName";
                    break;
                default:
                    FilterColumn = "None";
                    break;
            }

            if (txtFilterValue.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtAllUsers.DefaultView.RowFilter = "";
                lblRecordsCount.Text = dgvUsers.Rows.Count.ToString();
                return;
            }

            if (FilterColumn == "UserID" || FilterColumn == "PersonID")
            {
                if (int.TryParse(txtFilterValue.Text.Trim(), out int result))
                    _dtAllUsers.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, result);
                else
                    _dtAllUsers.DefaultView.RowFilter = "1 = 0"; 
            }
            else
            {
                _dtAllUsers.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFilterValue.Text.Trim());
            }

            lblRecordsCount.Text = dgvUsers.Rows.Count.ToString();
        }

        private void cbIsActive_SelectedIndexChanged(object sender, EventArgs e)
        {
            string FilterValue = cbIsActive.Text;

            switch (FilterValue)
            {
                case "All":
                    _dtAllUsers.DefaultView.RowFilter = "";
                    break;
                case "Yes":
                    _dtAllUsers.DefaultView.RowFilter = "[IsActive] = true OR [IsActive] = 1";
                    break;
                case "No":
                    _dtAllUsers.DefaultView.RowFilter = "[IsActive] = false OR [IsActive] = 0";
                    break;
            }

            lblRecordsCount.Text = dgvUsers.Rows.Count.ToString();
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            frmAddUpdateUser frm = new frmAddUpdateUser();
            frm.DataBack += _DataBackEventHandler;

            frm.ShowDialog();
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int userID = (int)dgvUsers.CurrentRow.Cells["UserID"].Value;
            frmUserInfo frm = new frmUserInfo(userID);
            frm.ShowDialog();
        }

        private void addNewPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdateUser frm = new frmAddUpdateUser();
            frm.DataBack += _DataBackEventHandler;

            frm.ShowDialog();

        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int userID = (int)dgvUsers.CurrentRow.Cells["UserID"].Value;
            frmAddUpdateUser frm = new frmAddUpdateUser(userID); 
            frm.ShowDialog();
            _RefreshUsersList();
        }

        private void sendEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int userID = (int)dgvUsers.CurrentRow.Cells["UserID"].Value;
            frmChangePassword frm = new frmChangePassword(userID);
            frm.ShowDialog();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int userID = (int)dgvUsers.CurrentRow.Cells["UserID"].Value;

            if (MessageBox.Show("Are you sure you want to delete User [" + userID + "]?", "Confirm Delete",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                if (clsBLUser.DeleteUser(userID))
                {
                    MessageBox.Show("User Deleted Successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _RefreshUsersList();
                }
                else
                {
                    MessageBox.Show("User was not deleted because it has data linked to it.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmListUserscs_Load_1(object sender, EventArgs e)
        {
            cbFilterBy.SelectedIndex = 0;
            _RefreshUsersList();
        }

        private void cmsUser_Opening(object sender, CancelEventArgs e)
        {

        }
        private void _DataBackEventHandler(object sender, int UserID)
        {
            _RefreshUsersList();
        }
    }
}
