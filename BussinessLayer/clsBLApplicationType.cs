using System;
using System.Data;
using DataAccessLayer;

namespace BuisnessLayer
{
    public class clsBLApplicationType
    {
        public enum enMode { Update = 1 }
        public enMode Mode = enMode.Update;

        public int ApplicationTypeID { set; get; }
        public string ApplicationTypeTitle { set; get; }
        public float ApplicationFees { set; get; }

        private clsBLApplicationType(int ApplicationTypeID, string ApplicationTypeTitle, float ApplicationFees)
        {
            this.ApplicationTypeID = ApplicationTypeID;
            this.ApplicationTypeTitle = ApplicationTypeTitle;
            this.ApplicationFees = ApplicationFees;
            Mode = enMode.Update;
        }

        private bool _UpdateApplicationType()
        {
            return clsDALApplicationType.UpdateApplicationFees(this.ApplicationTypeID, this.ApplicationFees);
        }

        public static DataTable GetAllApplicationTypes()
        {
            return clsDALApplicationType.GetAllApplicationTypes();
        }

        public static clsBLApplicationType Find(int ApplicationTypeID)
        {
            string ApplicationTypeTitle = "";
            float ApplicationFees = 0;

            if (clsDALApplicationType.GetApplicationTypeInfoByID(ApplicationTypeID, ref ApplicationTypeTitle, ref ApplicationFees))
            {
                return new clsBLApplicationType(ApplicationTypeID, ApplicationTypeTitle, ApplicationFees);
            }
            else
            {
                return null;
            }
        }

        public bool Save()
        {
            return _UpdateApplicationType();
        }
    }
}