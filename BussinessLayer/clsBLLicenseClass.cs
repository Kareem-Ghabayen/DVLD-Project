using System;
using System.Data;
using DataAccessLayer; 

namespace BuisnessLayer
{
    public class clsBLLicenseClass
    {
        public enum enMode { AddNew = 0, Update = 1 }
        public enMode Mode = enMode.AddNew;

        public int LicenseClassID { get; set; }
        public string ClassName { get; set; }
        public string ClassDescription { get; set; }
        public byte MinimumAllowedAge { get; set; }
        public byte DefaultValidityLength { get; set; }
        public decimal ClassFees { get; set; } 

        public clsBLLicenseClass()
        {
            this.LicenseClassID = -1;
            this.ClassName = "";
            this.ClassDescription = "";
            this.MinimumAllowedAge = 18;
            this.DefaultValidityLength = 10;
            this.ClassFees = 0;

            Mode = enMode.AddNew;
        }

        private clsBLLicenseClass(int licenseClassID, string className, string classDescription,
            byte minimumAllowedAge, byte defaultValidityLength, float classFees)
        {
            this.LicenseClassID = licenseClassID;
            this.ClassName = className;
            this.ClassDescription = classDescription;
            this.MinimumAllowedAge = minimumAllowedAge;
            this.DefaultValidityLength = defaultValidityLength;
            this.ClassFees = (decimal)classFees; 

            Mode = enMode.Update;
        }

        public static clsBLLicenseClass Find(int licenseClassID)
        {
            string className = "", classDescription = "";
            byte minimumAllowedAge = 0, defaultValidityLength = 0;
            float classFees = 0;

            bool isFound = clsDALLicenseClasses.GetLicenseClassInfoByID(licenseClassID, ref className,
                ref classDescription, ref minimumAllowedAge, ref defaultValidityLength, ref classFees);

            if (isFound)
            {
                return new clsBLLicenseClass(licenseClassID, className, classDescription,
                    minimumAllowedAge, defaultValidityLength, classFees);
            }
            else
            {
                return null;
            }
        }

        public static clsBLLicenseClass Find(string className)
        {
            int licenseClassID = -1;
            string classDescription = "";
            byte minimumAllowedAge = 0, defaultValidityLength = 0;
            float classFees = 0;

            bool isFound = clsDALLicenseClasses.GetLicenseClassInfoByClassName(className, ref licenseClassID,
                ref classDescription, ref minimumAllowedAge, ref defaultValidityLength, ref classFees);

            if (isFound)
            {
                return new clsBLLicenseClass(licenseClassID, className, classDescription,
                    minimumAllowedAge, defaultValidityLength, classFees);
            }
            else
            {
                return null;
            }
        }

        public static DataTable GetAllLicenseClasses()
        {
            return clsDALLicenseClasses.GetAllLicenseClasses();
        }

        private bool _AddNewLicenseClass()
        {
            this.LicenseClassID = clsDALLicenseClasses.AddNewLicenseClass(this.ClassName, this.ClassDescription,
                this.MinimumAllowedAge, this.DefaultValidityLength, (float)this.ClassFees);

            return (this.LicenseClassID != -1);
        }

        private bool _UpdateLicenseClass()
        {
            return clsDALLicenseClasses.UpdateLicenseClass(this.LicenseClassID, this.ClassName,
                this.ClassDescription, this.MinimumAllowedAge, this.DefaultValidityLength, (float)this.ClassFees);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewLicenseClass())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdateLicenseClass();
            }

            return false;
        }
    }
}