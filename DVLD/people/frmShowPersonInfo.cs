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
    public partial class frmShowPersonInfo : Form
    {
        // تعديل الـ Constructor ليستلم PersonID
        public frmShowPersonInfo(int PersonID)
        {
            InitializeComponent();

            // هنا نمرر الرقم مباشرة للكنترول الموجود داخل الشاشة
            ctrlPersonCard1.LoadPersonInfo(PersonID);
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();       
        }
    }
}
