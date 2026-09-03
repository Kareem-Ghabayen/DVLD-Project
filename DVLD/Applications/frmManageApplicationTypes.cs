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

namespace DVLD.Applications
{
    public partial class frmManageApplicationTypes : Form
    {
        private DataTable _dtApplicationTypes;
        private void _RefreshApplicationTypesList()
        {
            _dtApplicationTypes = clsBLApplicationType.GetAllApplicationTypes();
            dgvApplicationTypes.DataSource = _dtApplicationTypes;
            lblRecordsCount.Text = dgvApplicationTypes.Rows.Count.ToString();

     
            }
        
        public frmManageApplicationTypes()
        {
            InitializeComponent();
        }

        private void frmManageApplicationTypes_Load(object sender, EventArgs e)
        {
            _RefreshApplicationTypesList();
        }

        private void editApplicationTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int applicationTypeID = (int)dgvApplicationTypes.CurrentRow.Cells[0].Value;

            frmUpdateApplicationType frm = new frmUpdateApplicationType(applicationTypeID);
            frm.ShowDialog();

            _RefreshApplicationTypesList();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
