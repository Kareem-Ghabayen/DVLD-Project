using BusinessLayer;
using DataAccessLayer;
using DVLD_BLL;
using DVLD_DataAccess;
using System;
using System.Data;
using static BuisnessLayer.clsBLApplication;

namespace BuisnessLayer 
{
    public class clsBLLicense
    {
        public enum enMode { AddNew = 0, Update = 1 }
        public enMode Mode = enMode.AddNew;
        public enum enIssueReason { FirstTime = 1, Renew = 2, DamagedReplacement = 3, LostReplacement = 4 };

        public int LicenseID { get; set; }
        public int ApplicationID { get; set; }
        public int DriverID { get; set; }
        public int LicenseClass { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Notes { get; set; }
        public decimal PaidFees { get; set; }
        public bool IsActive { get; set; }
        public byte IssueReason { get; set; }
        public int CreatedByUserID { get; set; }

        public clsBLDriver DriverInfo { get; set; }


        public clsBLLicenseClass LicenseClassInfo => clsBLLicenseClass.Find(this.LicenseClass);

        public string IssueReasonText
        {
            get
            {
                switch ((enIssueReason)this.IssueReason)
                {
                    case enIssueReason.FirstTime:
                        return "First Time";
                    case enIssueReason.Renew:
                        return "Renew";
                    case enIssueReason.DamagedReplacement:
                        return "Replacement for Damaged";
                    case enIssueReason.LostReplacement:
                        return "Replacement for Lost";
                    default:
                        return "First Time";
                }
            }
        }

        public bool IsDetained => clsBLDetainedLicense.IsLicenseDetained(this.LicenseID);


        public clsBLLicense()
        {
            this.LicenseID = -1;
            this.ApplicationID = -1;
            this.DriverID = -1;
            this.LicenseClass = -1;
            this.IssueDate = DateTime.Now;
            this.ExpirationDate = DateTime.Now;
            this.Notes = "";
            this.PaidFees = 0;
            this.IsActive = true;
            this.IssueReason = 0;
            this.CreatedByUserID = -1;

            Mode = enMode.AddNew;
        }

        private clsBLLicense(int licenseID, int applicationID, int driverID, int licenseClass, DateTime issueDate, DateTime expirationDate, string notes, decimal paidFees, bool isActive, byte issueReason, int createdByUserID)
        {
            this.LicenseID = licenseID;
            this.ApplicationID = applicationID;
            this.DriverID = driverID;
            this.LicenseClass = licenseClass;
            this.IssueDate = issueDate;
            this.ExpirationDate = expirationDate;
            this.Notes = notes;
            this.PaidFees = paidFees;
            this.IsActive = isActive;
            this.IssueReason = issueReason;
            this.CreatedByUserID = createdByUserID;
            this.DriverInfo = clsBLDriver.FindByDriverID(this.DriverID);
            Mode = enMode.Update;
        }

        public static clsBLLicense FindByLicenseID(int LicenseID)
        {
            int applicationID = -1, driverID = -1, licenseClass = -1, createdByUserID = -1;
            DateTime issueDate = DateTime.Now, expirationDate = DateTime.Now;
            string notes = "";
            decimal paidFees = 0;
            bool isActive = false;
            byte issueReason = 0;

            bool isFound = clsDALLicenses.GetLicenseInfoByLicenseID(
                LicenseID, ref applicationID, ref driverID, ref licenseClass,
                ref issueDate, ref expirationDate, ref notes, ref paidFees,
                ref isActive, ref issueReason, ref createdByUserID
            );

            if (isFound)
            {
                return new clsBLLicense(LicenseID, applicationID, driverID, licenseClass,
                    issueDate, expirationDate, notes, paidFees, isActive, issueReason, createdByUserID);
            }
            else
            {
                return null;
            }
        }

        public static DataTable GetAllLicenses()
        {
            return clsDALLicenses.GetAllLicenses();
        }

        public static DataTable GetDriverLicenses(int DriverID)
        {
            return clsDALLicenses.GetDriverLicenses(DriverID);
        }

        private bool _AddNewLicense()
        {
            this.LicenseID = clsDALLicenses.AddNewLicense(
                this.ApplicationID, this.DriverID, this.LicenseClass,
                this.IssueDate, this.ExpirationDate, this.Notes,
                this.PaidFees, this.IsActive, this.IssueReason, this.CreatedByUserID
            );

            return (this.LicenseID != -1);
        }

        private bool _UpdateLicense()
        {
            return clsDALLicenses.UpdateLicense(
                this.LicenseID, this.ApplicationID, this.DriverID, this.LicenseClass,
                this.IssueDate, this.ExpirationDate, this.Notes,
                this.PaidFees, this.IsActive, this.IssueReason, this.CreatedByUserID
            );
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewLicense())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    return false;

                case enMode.Update:
                    return _UpdateLicense();
            }

            return false;
        }

        public static int GetActiveLicenseIDByPersonID(int PersonID, int LicenseClassID)
        {
            return clsDALLicenses.GetActiveLicenseIDByPersonID(PersonID, LicenseClassID);
        }

        public bool IsLicenseActiveByLicenseID()
        {
            return clsDALLicenses.IsLicenseActiveByLicenseID(LicenseID);
        }

        public static bool IsLicenseExistByPersonIDAndLicenseClass(int PersonID, int LicenseClass)
        {
            return clsDALLicenses.IsLicenseExistByPersonIDAndLicenseClass(PersonID, LicenseClass);
        }

        public static bool IsLicenseExist(int licenseID)
        {
            return clsDALLicenses.IsLicenseExist(licenseID);
        }

        public clsBLLicense RenewLicense(string Notes, int CreatedByUserID)
        {
            clsBLApplication application = new clsBLApplication();
            application.ApplicantPersonID = this.DriverInfo.PersonID;
            application.ApplicationDate = DateTime.Now;
            application.ApplicationTypeID = (int)enApplicationType.RenewDrivingLicense;
            application.ApplicationStatus = (byte)clsBLApplication.enStatus.Completed;
            application.LastStatusDate = DateTime.Now;
            application.PaidFees = clsBLApplicationType.Find((int)enApplicationType.RenewDrivingLicense).ApplicationFees;
            application.CreatedByUserID = CreatedByUserID;

            if (!application.Save())
                return null;

            this.IsActive = false;
            if (!this.Save())
                return null;

            clsBLLicense newLicense = new clsBLLicense();
            newLicense.ApplicationID = application.ApplicationID;
            newLicense.DriverID = this.DriverID;
            newLicense.LicenseClass = this.LicenseClass;
            newLicense.IssueDate = DateTime.Now;

            byte validityLength = this.LicenseClassInfo.DefaultValidityLength;
            newLicense.ExpirationDate = DateTime.Now.AddYears(validityLength);

            newLicense.Notes = Notes;
            newLicense.PaidFees = this.LicenseClassInfo.ClassFees;
            newLicense.IsActive = true;
            newLicense.IssueReason = (byte)enIssueReason.Renew;
            newLicense.CreatedByUserID = CreatedByUserID;

            if (!newLicense.Save())
                return null;

            return newLicense;
        }

        public int IssueLicenseFirstTime(int localDrivingLicenseApplicationID, string notes = "")
        {
            clsBLLocalDrivingLicenseApplication localApp = clsBLLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID(localDrivingLicenseApplicationID);

            if (localApp == null || localApp.GetPassedTestCount() < 3)
                return -1;

            clsBLApplication baseApplication = clsBLApplication.Find(localApp.ApplicationID);
            if (baseApplication == null)
                return -1;

            int driverID = clsBLDriver.GetOrCreateDriverID(baseApplication.ApplicantPersonID);
            if (driverID == -1)
                return -1; 

            clsBLLicenseClass licenseClassInfo = clsBLLicenseClass.Find(localApp.LicenseClassID);
            if (licenseClassInfo == null)
                return -1;

            this.ApplicationID = localApp.ApplicationID;
            this.DriverID = driverID; 
            this.LicenseClass = localApp.LicenseClassID;
            this.IssueDate = DateTime.Now;
            this.ExpirationDate = DateTime.Now.AddYears(licenseClassInfo.DefaultValidityLength);
            this.Notes = notes;
            this.PaidFees = licenseClassInfo.ClassFees;
            this.IsActive = true;
            this.IssueReason = 1; 
            this.CreatedByUserID = clsGlobal.CurrentUser.UserID;

            if (this.Save())
            {
                baseApplication.SetComplete();
                return this.LicenseID;
            }

            return -1;
        }

        public clsBLLicense Replace(enIssueReason issueReason, int applicationID)
        {
            this.IsActive = false;
            if (!this.Save())
            {
                return null;
            }

            clsBLLicense newLicense = new clsBLLicense();

            newLicense.ApplicationID = applicationID;
            newLicense.DriverID = this.DriverID;
            newLicense.LicenseClass = this.LicenseClass;
            newLicense.IssueDate = DateTime.Now;

            newLicense.ExpirationDate = this.ExpirationDate;

            newLicense.Notes = this.Notes;
            newLicense.PaidFees = 0;
            newLicense.IsActive = true;
            newLicense.IssueReason = (byte)issueReason;
            newLicense.CreatedByUserID = clsGlobal.CurrentUser.UserID;

            if (!newLicense.Save())
            {
                return null;
            }

            return newLicense;
        }

        public static clsBLLicense GetActiveClass3LicenseByNationalNo(string nationalNo)
        {
            if (string.IsNullOrEmpty(nationalNo))
            {
                throw new Exception("Debugging: 'nationalNo' passed to BLL is NULL or Empty!");
            }
            int licenseID = -1;
            int applicationID = -1;
            int driverID = -1;
            int licenseClass = -1;
            DateTime issueDate = DateTime.MinValue;
            DateTime expirationDate = DateTime.MinValue;
            string notes = "";
            decimal paidFees = 0;
            bool isActive = false;
            byte issueReason = 0;
            int createdByUserID = -1;

            if (clsDALLicenses.GetActiveClass3LicenseInfoByNationalNo(nationalNo,
                ref licenseID, ref applicationID, ref driverID, ref licenseClass,
                ref issueDate, ref expirationDate, ref notes, ref paidFees,
                ref isActive, ref issueReason, ref createdByUserID))
            {
                return new clsBLLicense(licenseID, applicationID, driverID, licenseClass,
                    issueDate, expirationDate, notes, paidFees, isActive, issueReason, createdByUserID);
            }
            else
            {
                return null;
            }

        }
        public static int GetActiveLicenseIDByApplicationID(int ApplicationID)
        {
            return clsDALLicenses.GetActiveLicenseIDByApplicationID(ApplicationID);
        }
        public bool IsLicenseExpired()
        {
            return (this.ExpirationDate < DateTime.Now);
        }
    }
}