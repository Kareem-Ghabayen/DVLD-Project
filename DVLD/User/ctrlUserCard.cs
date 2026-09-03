using BuisnessLayer;
using DVLD.people;
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
    public partial class ctrlUserCard : UserControl
    {
        private clsBLUser _User;
        private int _UserID = -1;

        public int UserID => _UserID;

        public ctrlUserCard()
        {
            InitializeComponent();
        }

        // دالة جلب بيانات المستخدم بواسطة رقم المستخدم UserID
        public void LoadUserInfo(int UserID)
        {
            _User = clsBLUser.FindByUserID(UserID);
    
            if (_User == null)
            {
                ResetUserInfo();
                MessageBox.Show("No User with UserID = " + UserID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _FillUserInfo();
        }

        // دالة جلب بيانات المستخدم بواسطة رقم الشخص PersonID
        public void LoadUserInfoByPersonID(int PersonID)
        {
            _User = clsBLUser.FindByPersonID(PersonID);

            if (_User == null)
            {
                ResetUserInfo();
                MessageBox.Show("No User with PersonID = " + PersonID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _FillUserInfo();
        }

        // تعبئة البيانات في الشاشة
        private void _FillUserInfo()
        {
            _UserID = _User.UserID;

            ctrlPersonCard1.LoadPersonInfo(_User.PersonID);
            lblUserID.Text = _User.UserID.ToString();
            lblUserName.Text = _User.UserName;
            lblIsActive.Text = _User.IsActive ? "Yes" : "No";
        }

        // تفريغ البيانات
        public void ResetUserInfo()
        {
            _UserID = -1;
            ctrlPersonCard1.ResetPersonInfo();
            lblUserID.Text = "[???]";
            lblUserName.Text = "[???]";
            lblIsActive.Text = "[???]";
        }

        private void ctrlPersonCard1_Load(object sender, EventArgs e)
        {

        }
    }
}
