using System;
using System.Data;
using DataAccessLayer;

namespace BuisnessLayer
{
    public class clsBLDriver
    {
        public enum enMode { AddNew = 0, Update = 1 }
        public enMode Mode = enMode.AddNew;

        public int DriverID { set; get; }
        public int PersonID { set; get; }
        public int CreatedByUserID { set; get; }
        public DateTime CreatedDate { set; get; }

   
        public clsBLSPeople PersonInfo { set; get; }

        public clsBLDriver()
        {
            this.DriverID = -1;
            this.PersonID = -1;
            this.CreatedByUserID = -1;
            this.CreatedDate = DateTime.MinValue;
            this.PersonInfo = null;

            Mode = enMode.AddNew;
        }

        private clsBLDriver(int DriverID, int PersonID, int CreatedByUserID, DateTime CreatedDate)
        {
            this.DriverID = DriverID;
            this.PersonID = PersonID;
            this.CreatedByUserID = CreatedByUserID;
            this.CreatedDate = CreatedDate;
            
            this.PersonInfo = clsBLSPeople.FindByID(this.PersonID);

            Mode = enMode.Update;
        }

        public static clsBLDriver FindByDriverID(int DriverID)
        {
            int PersonID = -1, CreatedByUserID = -1;
            DateTime CreatedDate = DateTime.MinValue;

            if (clsDALDriver.GetDriverInfoByDriverID(DriverID, ref PersonID, ref CreatedByUserID, ref CreatedDate))
            {
                return new clsBLDriver(DriverID, PersonID, CreatedByUserID, CreatedDate);
            }
            else
            {
                return null;
            }
        }

        public static clsBLDriver FindByPersonID(int PersonID)
        {
            int DriverID = -1, CreatedByUserID = -1;
            DateTime CreatedDate = DateTime.MinValue;

            if (clsDALDriver.GetDriverInfoByPersonID(PersonID, ref DriverID, ref CreatedByUserID, ref CreatedDate))
            {
                return new clsBLDriver(DriverID, PersonID, CreatedByUserID, CreatedDate);
            }
            else
            {
                return null;
            }
        }

        public static clsBLDriver FindByNationalNo(string NationalNo)
        {
            int DriverID = -1, PersonID = -1, CreatedByUserID = -1;
            DateTime CreatedDate = DateTime.MinValue;

            if (clsDALDriver.GetDriverInfoByNationalNo(NationalNo, ref DriverID, ref PersonID, ref CreatedByUserID, ref CreatedDate))
            {
                return new clsBLDriver(DriverID, PersonID, CreatedByUserID, CreatedDate);
            }
            else
            {
                return null;
            }
        }

        public static DataTable GetAllDrivers()
        {
            return clsDALDriver.GetAllDrivers();
        }

        private bool _AddNewDriver()
        {
            this.DriverID = clsDALDriver.AddNewDriver(this.PersonID, this.CreatedByUserID);
            return (this.DriverID != -1);
        }

        private bool _UpdateDriver()
        {
            return clsDALDriver.UpdateDriver(this.DriverID, this.PersonID, this.CreatedByUserID, CreatedDate);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (IsDriverExistByPersonID(this.PersonID))
                    {
                        return false;
                    }

                    if (_AddNewDriver())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdateDriver();
            }

            return false;
        }

        public static bool DeleteDriver(int DriverID)
        {
            return clsDALDriver.DeleteDriver(DriverID);
        }

        public static bool IsDriverExist(int DriverID)
        {
            return clsDALDriver.IsDriverExist(DriverID);
        }

        public static bool IsDriverExistByPersonID(int PersonID)
        {
            return clsDALDriver.IsDriverExistByPersonID(PersonID);
        }

        public static int GetOrCreateDriverID(int personID)
        {
            clsBLDriver driver = clsBLDriver.FindByPersonID(personID);

            if (driver != null)
            {
                return driver.DriverID;
            }

            clsBLDriver newDriver = new clsBLDriver();
            newDriver.PersonID = personID;
            newDriver.CreatedByUserID = clsGlobal.CurrentUser.UserID;

            if (newDriver.Save())
            {
                return newDriver.DriverID;
            }

            return -1;
        }
    }
}