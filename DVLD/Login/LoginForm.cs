using BuisnessLayer;
using DVLD.MainScreen;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace DVLD.Login

{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            string userName = "", password = "";

            clsLogin.GetStoredCredential(ref userName, ref password);

            if (!string.IsNullOrEmpty(userName))
            {
                tbUserName.Text = userName;
                tbPassword.Text = password;
                chkRememberMe.Checked = true;
            }

            tbUserName.Focus();
        }



        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();

            if (string.IsNullOrWhiteSpace(tbUserName.Text))
            {
                errorProvider1.SetError(tbUserName, "This field is required!");
                tbUserName.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(tbPassword.Text))
            {
                errorProvider1.SetError(tbPassword, "This field is required!");
                tbPassword.Focus();
                return;
            }

            clsBLUser user = clsLogin.AuthenticateUser(tbUserName.Text, tbPassword.Text);
            if (user != null)
            {

                if (chkRememberMe.Checked)
                {
                    clsLogin. RememberUsernameAndPassword(tbUserName.Text, tbPassword.Text);
                }
                else
                {
                    clsLogin. RememberUsernameAndPassword("", "");
                }
                clsGlobal.CurrentUser = user;
                this.Hide();
                MainForm frm = new MainForm(this);
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("اسم المستخدم أو كلمة المرور غير صحيحة!", "خطأ في تسجيل الدخول", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tbUserName.Focus();
                tbUserName.SelectAll();
            }
        }

        private void LoginForm_FormClosed(object sender, FormClosedEventArgs e)
        {
  
            Application.Exit(); // هذا بينهي كل التطبيق والباك جراوند من الفيجوال ستوديو فوراً
        }
    }
}