using BusinessLayer;
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
    public partial class frmListTestTypes : Form
    {
        private DataTable _dtTestTypes;

        public frmListTestTypes()
        {
            InitializeComponent();
        }
        private void _RefreshTestTypesList()
        {
            _dtTestTypes = clsBLTestType.GetAllTestTypes();
            dgvTestTypes.DataSource = _dtTestTypes;
            lblRecordsCount.Text = dgvTestTypes.Rows.Count.ToString();
\
        }
        private void frmListTestTypes_Load(object sender, EventArgs e)
        {
            _RefreshTestTypesList();
        }

        private void editTestTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int testTypeID = (int)dgvTestTypes.CurrentRow.Cells[0].Value;

            frmUpdateTestType frm = new frmUpdateTestType(testTypeID);
            frm.ShowDialog();

            _RefreshTestTypesList();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
