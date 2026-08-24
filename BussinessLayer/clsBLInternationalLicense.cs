using DataAccessLayer; // أو DVLD_DataAccess حسب مساحة الأسماء عندك
using DVLD_DataAccess;
using System;
using System.Data;

namespace BusinessLayer
{
    public class clsBLInternationalLicense
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int InternationalLicenseID { set; get; }
        public int ApplicationID { set; get; }
        public int DriverID { set; get; }
        public int IssuedUsingLocalLicenseID { set; get; }
        public DateTime IssueDate { set; get; }
        public DateTime ExpirationDate { set; get; }
        public bool IsActive { set; get; }
        public int CreatedByUserID { set; get; }

        public clsBLInternationalLicense()
        {
            this.InternationalLicenseID = -1;
            this.ApplicationID = -1;
            this.DriverID = -1;
            this.IssuedUsingLocalLicenseID = -1;
            this.IssueDate = DateTime.Now;
            this.ExpirationDate = DateTime.Now;
            this.IsActive = true;
            this.CreatedByUserID = -1;
            Mode = enMode.AddNew;
        }

        private clsBLInternationalLicense(int internationalLicenseID, int applicationID, int driverID,
            int issuedUsingLocalLicenseID, DateTime issueDate, DateTime expirationDate, bool isActive, int createdByUserID)
        {
            this.InternationalLicenseID = internationalLicenseID;
            this.ApplicationID = applicationID;
            this.DriverID = driverID;
            this.IssuedUsingLocalLicenseID = issuedUsingLocalLicenseID;
            this.IssueDate = issueDate;
            this.ExpirationDate = expirationDate;
            this.IsActive = isActive;
            this.CreatedByUserID = createdByUserID;
            Mode = enMode.Update;
        }

        public static clsBLInternationalLicense Find(int internationalLicenseID)
        {
            int applicationID = -1;
            int driverID = -1;
            int issuedUsingLocalLicenseID = -1;
            DateTime issueDate = DateTime.Now;
            DateTime expirationDate = DateTime.Now;
            bool isActive = true;
            int createdByUserID = -1;

            if (clsDALInternationalLicenses.GetInternationalLicenseInfoByID(internationalLicenseID,
                ref applicationID, ref driverID, ref issuedUsingLocalLicenseID,
                ref issueDate, ref expirationDate, ref isActive, ref createdByUserID))
            {
                return new clsBLInternationalLicense(internationalLicenseID, applicationID, driverID,
                    issuedUsingLocalLicenseID, issueDate, expirationDate, isActive, createdByUserID);
            }
            else
            {
                return null;
            }
        }

        public static DataTable GetAllInternationalLicenses()
        {
            return clsDALInternationalLicenses.GetAllInternationalLicenses();
        }

        public static DataTable GetDriverInternationalLicenses(int driverID)
        {
            return clsDALInternationalLicenses.GetDriverInternationalLicenses(driverID);
        }

        private bool _AddNewInternationalLicense()
        {
            this.InternationalLicenseID = clsDALInternationalLicenses.AddNewInternationalLicense(
                this.ApplicationID,
                this.DriverID,
                this.IssuedUsingLocalLicenseID,
                this.IssueDate,
                this.ExpirationDate,
                this.IsActive,
                this.CreatedByUserID
            );

            return (this.InternationalLicenseID != -1);
        }

        private bool _UpdateInternationalLicense()
        {
            return clsDALInternationalLicenses.UpdateInternationalLicense(
                this.InternationalLicenseID,
                this.ApplicationID,
                this.DriverID,
                this.IssuedUsingLocalLicenseID,
                this.IssueDate,
                this.ExpirationDate,
                this.IsActive,
                this.CreatedByUserID
            );
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewInternationalLicense())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    return false;

                case enMode.Update:
                    return _UpdateInternationalLicense();
            }

            return false;
        }
    }
}