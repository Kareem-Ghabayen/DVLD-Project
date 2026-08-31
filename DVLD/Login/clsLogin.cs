using BuisnessLayer;
using BusinessLayer; // تأكد من استدعاء البزنس لير الخاص بك
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Login
{
    internal class clsLogin
    {
        public static clsBLUser AuthenticateUser(string username, string password)
        {
                      
            clsBLUser user = clsBLUser.Login(username, password);

            return user;
        }

        // 2. ميثود حفظ بيانات الدخول (إذا اختار المستخدم "تذكرني")
        public static void RememberUsernameAndPassword(string username, string password)
        {
            try
            {
                string keyPath = @"HKEY_CURRENT_USER\SOFTWARE\DVLD";
                string valueNameUserName = "UserName";
                string valueNamePassword = "Password";

                // حفظ القيم في الـ Registry
                Registry.SetValue(keyPath, valueNameUserName, username, RegistryValueKind.String);
                Registry.SetValue(keyPath, valueNamePassword, password, RegistryValueKind.String);
            }
            catch (Exception ex)
            {
            }
        }

        // 3. ميثود جلب البيانات المحفوظة عند فتح الشاشة لو كانت مفعلة
        public static void  GetStoredCredential(ref string username, ref string password)
        {
            try
            {
                string keyPath = @"HKEY_CURRENT_USER\SOFTWARE\DVLD";
                string valueNameUserName = "UserName";
                string valueNamePassword = "Password";

                username = Registry.GetValue(keyPath, valueNameUserName, null) as string ?? "";
                password = Registry.GetValue(keyPath, valueNamePassword, null) as string ?? "";
            }
            catch (Exception ex)
            {
                username = "";
                password = "";
                MessageBox.Show("صار خطأ في الحفظ يا هندسة: " + ex.Message);
            }
        }
    }
}