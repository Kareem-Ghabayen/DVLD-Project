using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLayer
{
    public class clsBLUser
    {
        public enum enMode { AddNew = 0, UpdateNew = 1 }
        public enMode Mode = enMode.AddNew;

        public int UserID { get; set; }
        public int PersonID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }

        public clsBLUser()
        {
            this.UserID = -1;
            this.PersonID = -1;
            this.UserName = "";
            this.Password = "";
            this.IsActive = false;

            Mode = enMode.AddNew;
        }

        private clsBLUser(int UserID, int PersonID, string UserName, string Password, bool IsActive)
        {
            this.UserID = UserID;
            this.PersonID = PersonID;
            this.UserName = UserName;
            this.Password = Password;
            this.IsActive = IsActive;

            Mode = enMode.UpdateNew;
        }

        private bool _AddNewUser()
        {
            this.UserID = clsDALUser.AddNewUser(this.PersonID, this.UserName, this.Password, this.IsActive);

            return this.UserID != -1;
        }

        private bool _UpdateUser()
        {
            return clsDALUser.UpdateUser(this.UserID, this.PersonID, this.UserName, this.Password, this.IsActive);
        }

        public static DataTable GetAllUsers()
        {
            return clsDALUser.GetAllUsers();
        }
        public static clsBLUser FindByUserID(int UserID)
        {
            int PersonID = -1;
            string UserName = "", Password = "";
            bool IsActive = false;

            if (clsDALUser.GetUserInfoByUserID(UserID, ref PersonID, ref UserName, ref Password, ref IsActive))
            {
                return new clsBLUser(UserID, PersonID, UserName, Password, IsActive);
            }
            else
            {
                return null;
            }
        }
        public static clsBLUser FindByPersonID(int PersonID)
        {
            int UserID = -1;
            string UserName = "", Password = "";
            bool IsActive = false;

            if (clsDALUser.GetUserInfoByPersonID(PersonID, ref UserID, ref UserName, ref Password, ref IsActive))
            {
                return new clsBLUser(UserID, PersonID, UserName, Password, IsActive);
            }
            else
            {
                return null;
            }
        }
        public static clsBLUser FindByUsernameAndPassword(string Username, string Password)
        {
            int UserID = -1;
            int PersonID = -1;
            bool IsActive = false;

            if (clsDALUser.GetUserInfoByUsernameAndPassword(Username, Password, ref UserID, ref PersonID, ref IsActive))
            {

                return new clsBLUser(UserID, PersonID, Username, Password, IsActive);
            }
            else
            {

                return null;
            }
        }
        public static clsBLUser Login(string Username, string Password)
        {
            clsBLUser user = FindByUsernameAndPassword(Username, Password);

            if (user == null)
            {
                return null;
            }

            if (!user.IsActive)
            {
                return null;
            }

            return user;
        }
        public static bool DeleteUser(int UserID)
        {
            return clsDALUser.DeleteUser(UserID);
        }

        public static bool IsUserExist(int UserID)
        {
            return clsDALUser.IsUserExist(UserID);
        }

        public static bool IsUserExist(string Username)
        {
            return clsDALUser.IsUserExist(Username);
        }

        public static bool IsUserExistForPersonID(int PersonID)
        {
            return clsDALUser.IsUserExistForPersonID(PersonID);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    // التأكد من عدم تكرار اسم المستخدم أو أن الشخص لديه حساب مسبقاً
                    if (IsUserExist(this.UserName) || IsUserExistForPersonID(this.PersonID))
                    {
                        return false;
                    }

                    if (_AddNewUser())
                    {
                        Mode = enMode.UpdateNew;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.UpdateNew:
                    return _UpdateUser();
            }

            return false;
        }
        public bool DeactivateAccount()
        {
            this.IsActive = false;
            return this.Save();
        }

        public bool ActivateAccount()
        {
            this.IsActive = true;
            return this.Save();
        }
    }
}