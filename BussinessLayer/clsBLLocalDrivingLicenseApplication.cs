using BuisnessLayer;
using BusinessLayer;
using DataAccessLayer;
using DVLD_DataAccess; // أو حسب اسم مساحة الأسماء عندك في الـ DAL
using System;
using System.Data;
using static BuisnessLayer.clsBLApplication;

namespace DVLD_BLL
{
    public class clsBLLocalDrivingLicenseApplication
    {
        public int LocalDrivingLicenseApplicationID { get; set; }
        public int ApplicationID { get; set; }
        public int LicenseClassID { get; set; }

        public clsBLApplication BaseApplicationInfo { get; set; }

        public clsBLLicenseClass LicenseClassInfo { get; set; }

        public clsBLLocalDrivingLicenseApplication()
        {
            this.LocalDrivingLicenseApplicationID = -1;
            this.ApplicationID = -1;
            this.LicenseClassID = -1;
            this.LicenseClassInfo = null;
        }

        private clsBLLocalDrivingLicenseApplication(int localDrivingLicenseApplicationID, int applicationID, int licenseClassID)
        {
            this.LocalDrivingLicenseApplicationID = localDrivingLicenseApplicationID;
            this.ApplicationID = applicationID;
            this.LicenseClassID = licenseClassID;
            this.BaseApplicationInfo = clsBLApplication.Find(applicationID);
            this.LicenseClassInfo = clsBLLicenseClass.Find(licenseClassID);
        }

        public static clsBLLocalDrivingLicenseApplication FindByLocalDrivingLicenseApplicationID(int localDrivingLicenseApplicationID)
        {
            int applicationID = -1;
            int licenseClassID = -1;

            bool isFound = clsDALLocalDrivingLicenseApplication.GetInfoByLocalDrivingLicenseApplicationID(
                localDrivingLicenseApplicationID, ref applicationID, ref licenseClassID);

            if (isFound)
            {
                return new clsBLLocalDrivingLicenseApplication(localDrivingLicenseApplicationID, applicationID, licenseClassID);
            }

            return null;
        }

        public static clsBLLocalDrivingLicenseApplication FindByApplicationID(int applicationID)
        {
            int localDrivingLicenseApplicationID = -1;
            int licenseClassID = -1;

            bool isFound = clsDALLocalDrivingLicenseApplication.GetInfoByApplicationID(
                applicationID, ref localDrivingLicenseApplicationID, ref licenseClassID);

            if (isFound)
            {
                return new clsBLLocalDrivingLicenseApplication(localDrivingLicenseApplicationID, applicationID, licenseClassID);
            }

            return null;
        }

        public bool Save()
        {
            switch (LocalDrivingLicenseApplicationID)
            {
                case -1:
                    return _AddNewLocalDrivingLicenseApplication();
                default:
                    int oldClassID = FindByLocalDrivingLicenseApplicationID(this.LocalDrivingLicenseApplicationID).LicenseClassID;

                    if (this.LicenseClassID != oldClassID)
                    {
                        if (clsBLLicense.IsLicenseExistByPersonIDAndLicenseClass(BaseApplicationInfo.ApplicantPersonID, this.LicenseClassID) ||
                            clsBLApplication.IsThereAnActiveApplicationInSameLicenses(BaseApplicationInfo.ApplicantPersonID, (int)clsBLApplicationType.enApplicationType.NewDrivingLicense, this.LicenseClassID))
                        {
                            return false; 
                        }
                    }

                    return _UpdateLocalDrivingLicenseApplication();
            }
        }

        private bool _AddNewLocalDrivingLicenseApplication()
        {
            this.LocalDrivingLicenseApplicationID = clsDALLocalDrivingLicenseApplication.AddNewLocalDrivingLicenseApplication(
                this.ApplicationID, this.LicenseClassID);

            return (this.LocalDrivingLicenseApplicationID != -1);
        }

        private bool _UpdateLocalDrivingLicenseApplication()
        {
            return clsDALLocalDrivingLicenseApplication.UpdateLocalDrivingLicenseApplication(
                this.LocalDrivingLicenseApplicationID, this.ApplicationID, this.LicenseClassID);
        }

        public int GetPassedTestCount()
        {
            return clsBLTest.GetPassedTestCount(this.LocalDrivingLicenseApplicationID);
        }

        public static clsBLLocalDrivingLicenseApplication AddNewLocalDrivingLicenseApplication(int applicantPersonID, int licenseClassID)
        {
            clsBLSPeople person = clsBLSPeople.FindByID(applicantPersonID);
            if (person == null)
            {
                return null; 
            }

            clsBLApplication application = new clsBLApplication();

            application.ApplicantPersonID = person.ID; 
            application.ApplicationDate = DateTime.Now;
            application.ApplicationTypeID = (int)enApplicationType.NewDrivingLicense;
            application.ApplicationStatus = 1; 
            application.LastStatusDate = DateTime.Now;

            clsBLApplicationType appType = clsBLApplicationType.Find((int)enApplicationType.NewDrivingLicense);
            if (appType == null) return null;

            application.PaidFees = appType.ApplicationFees;
            application.CreatedByUserID = clsGlobal.CurrentUser.UserID;

            if (application.PaidFees != appType.ApplicationFees ||
                IsThereAnActiveApplicationInSameLicenses(application.ApplicantPersonID, application.ApplicationTypeID, licenseClassID) ||
                clsBLLicense.IsLicenseExistByPersonIDAndLicenseClass(application.ApplicantPersonID, licenseClassID))
            {
                return null;
            }

            if (!application.Save())
            {
                return null;
            }

            clsBLLocalDrivingLicenseApplication localApp = new clsBLLocalDrivingLicenseApplication();

            localApp.ApplicationID = application.ApplicationID;
            localApp.LicenseClassID = licenseClassID;

            if (!localApp.Save())
            {
                return null;
            }

            return localApp;
        }
        public static DataTable GetAllLocalDrivingLicenseApplications()
        {
            return clsDALLocalDrivingLicenseApplication.GetAllLocalDrivingLicenseApplications();
        }
        public byte TotalTrialsPerTest(clsBLTestType.enTestType TestTypeID)
        {
            return clsDALLocalDrivingLicenseApplication.GetTotalTrialsPerTest(this.LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }
        public int GetActiveLicenseID()
        {
            return clsBLLicense.GetActiveLicenseIDByApplicationID(this.ApplicationID);
        }
    }
}