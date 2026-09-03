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
            _dtAllUsers = clsBLUser.GetAllUsers(); // افترضنا أن الدالة ترجع DataTable يحتوي الأعمدة الموضحة بالصورة
            dgvUsers.DataSource = _dtAllUsers;

            lblRecordsCount.Text = dgvUsers.Rows.Count.ToString();

            if (dgvUsers.Rows.Count > 0)
            {
                //// تحسين عرض الأوردة والشكل إن أردت
                //dgvUsers.Columns["UserID"].HeaderText = "User ID";
                //dgvUsers.Columns["UserID"].Width = 110;

                //dgvUsers.Columns["PersonID"].HeaderText = "Person ID";
                //dgvUsers.Columns["PersonID"].Width = 110;

                //dgvUsers.Columns["FullName"].HeaderText = "Full Name";
                //dgvUsers.Columns["FullName"].Width = 320;

                //dgvUsers.Columns["UserName"].HeaderText = "UserName";
                //dgvUsers.Columns["UserName"].Width = 180;

                //dgvUsers.Columns["IsActive"].HeaderText = "Is Active";
                //dgvUsers.Columns["IsActive"].Width = 100;
            }
        }
        private void frmListUserscs_Load(object sender, EventArgs e)
        {
            cbFilterBy.SelectedIndex = 0; // ضبط الافتراضي على None
            _RefreshUsersList();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilterBy.Text == "Is Active")
            {
                txtFilterValue.Visible = false;
                cbIsActive.Visible = true;
                cbIsActive.Focus();
                cbIsActive.SelectedIndex = 0; // 0 = All
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

            // تحديد اسم العمود البرمجي حسب اختيار المستخدم
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

            // إذا كان البحث فارغاً أو اختيار الفلتر None
            if (txtFilterValue.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtAllUsers.DefaultView.RowFilter = "";
                lblRecordsCount.Text = dgvUsers.Rows.Count.ToString();
                return;
            }

            // فلترة الأرقام تختلف عن النصوص في SQL Filter Expression
            if (FilterColumn == "UserID" || FilterColumn == "PersonID")
            {
                // التأكد من أن المدخل رقم لمنع الأخطاء
                if (int.TryParse(txtFilterValue.Text.Trim(), out int result))
                    _dtAllUsers.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, result);
                else
                    _dtAllUsers.DefaultView.RowFilter = "1 = 0"; // لا يرجع شيء إذا أدخل حروف في حقل رقمي
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
            frmAddUpdateUser frm = new frmAddUpdateUser(); // فتح الشاشة في مود الإضافة (-1)
            frm.ShowDialog();
            frm.DataBack += _DataBackEventHandler;
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
            frm.ShowDialog();
            frm.DataBack += _DataBackEventHandler;
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int userID = (int)dgvUsers.CurrentRow.Cells["UserID"].Value;
            frmAddUpdateUser frm = new frmAddUpdateUser(userID); // فتح الشاشة في مود التعديل
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
            cbFilterBy.SelectedIndex = 0; // ضبط الافتراضي على None
            _RefreshUsersList();
        }

        private void cmsUser_Opening(object sender, CancelEventArgs e)
        {

        }
        private void _DataBackEventHandler(object sender, int UserID)
        {
            // أعد تحميل البيانات في DataGridView أو قم بفلترتها حسب UserID الجديد
            _RefreshUsersList();
        }
    }
}
