using BuisnessLayer;
using DataAccessLayer; // أو DVLD_DataAccess حسب مساحة الأسماء عندك
using DVLD_DataAccess;
using System;
using System.Data;

namespace BusinessLayer
{
    public class clsBLDetainedLicense
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int DetainID { set; get; }
        public int LicenseID { set; get; }
        public DateTime DetainDate { set; get; }
        public decimal FineFees { set; get; }
        public int CreatedByUserID { set; get; }
        public bool IsReleased { set; get; }
        public DateTime ReleaseDate { set; get; }
        public int ReleasedByUserID { set; get; }
        public int ReleaseApplicationID { set; get; }

        public clsBLDetainedLicense()
        {
            this.DetainID = -1;
            this.LicenseID = -1;
            this.DetainDate = DateTime.Now;
            this.FineFees = 0;
            this.CreatedByUserID = -1;
            this.IsReleased = false;
            this.ReleaseDate = DateTime.MinValue;
            this.ReleasedByUserID = -1;
            this.ReleaseApplicationID = -1;
            Mode = enMode.AddNew;
        }

        private clsBLDetainedLicense(int detainID, int licenseID, DateTime detainDate, decimal fineFees,
            int createdByUserID, bool isReleased, DateTime releaseDate, int releasedByUserID, int releaseApplicationID)
        {
            this.DetainID = detainID;
            this.LicenseID = licenseID;
            this.DetainDate = detainDate;
            this.FineFees = fineFees;
            this.CreatedByUserID = createdByUserID;
            this.IsReleased = isReleased;
            this.ReleaseDate = releaseDate;
            this.ReleasedByUserID = releasedByUserID;
            this.ReleaseApplicationID = releaseApplicationID;
            Mode = enMode.Update;
        }

        public static clsBLDetainedLicense Find(int detainID)
        {
            int licenseID = -1;
            DateTime detainDate = DateTime.MinValue;
            decimal fineFees = 0;
            int createdByUserID = -1;
            bool isReleased = false;
            DateTime releaseDate = DateTime.MinValue;
            int releasedByUserID = -1;
            int releaseApplicationID = -1;

            if (clsDALDetainedLicenses.GetDetainedLicenseInfoByID(detainID, ref licenseID, ref detainDate,
                ref fineFees, ref createdByUserID, ref isReleased, ref releaseDate, ref releasedByUserID, ref releaseApplicationID))
            {
                return new clsBLDetainedLicense(detainID, licenseID, detainDate, fineFees,
                    createdByUserID, isReleased, releaseDate, releasedByUserID, releaseApplicationID);
            }
            else
            {
                return null;
            }
        }

        public static DataTable GetAllDetainedLicenses()
        {
            return clsDALDetainedLicenses.GetAllDetainedLicenses();
        }

        private bool _AddNewDetainedLicense()
        {
            this.DetainID = clsDALDetainedLicenses.DetainLicense(
                this.LicenseID,
                this.DetainDate,
                this.FineFees,
                this.CreatedByUserID
            );

            return (this.DetainID != -1);
        }

        private bool _UpdateDetainedLicense()
        {
            return clsDALDetainedLicenses.UpdateDetainedLicense(
                this.DetainID,
                this.LicenseID,
                this.DetainDate,
                this.FineFees,
                this.CreatedByUserID
            );
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (!clsBLLicense.IsLicenseExist(this.LicenseID))
                    {
                        return false;
                    }

                    if (clsBLDetainedLicense.IsLicenseDetained(this.LicenseID))
                    {
                        return false;
                    }
                    if (_AddNewDetainedLicense())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    return false;

                case enMode.Update:
                    return _UpdateDetainedLicense();
            }

            return false;
        }

        public static bool ReleaseDetainedLicense(int detainID, int releasedByUserID, int releaseApplicationID)
        {
            return clsDALDetainedLicenses.ReleaseDetainedLicense(detainID, releasedByUserID, releaseApplicationID);
        }

        public bool Release(int releasedByUserID, int releaseApplicationID)
        {
            return clsDALDetainedLicenses.ReleaseDetainedLicense(this.DetainID, releasedByUserID, releaseApplicationID);
        }

        public static bool IsLicenseDetained(int licenseID)
        {
            return clsDALDetainedLicenses.IsLicenseDetained(licenseID);
        }

        public static int ReleaseLicense(int licenseID)
        {
            clsBLDetainedLicense detainedLicense = clsBLDetainedLicense.FindByLicenseID(licenseID);

            if (detainedLicense == null || detainedLicense.IsReleased)
            {
                return -1;
            }

            clsBLLicense license = clsBLLicense.FindByLicenseID(detainedLicense.LicenseID);
            if (license == null || license.DriverInfo == null)
            {
                return -1;
            }

            clsBLApplicationType appType = clsBLApplicationType.Find((int)clsBLApplication.enApplicationType.ReleaseDetainedDrivingLicense);
            if (appType == null)
            {
                return -1;
            }

            clsBLApplication application = new clsBLApplication();
            application.ApplicantPersonID = license.DriverInfo.PersonID;
            application.ApplicationDate = DateTime.Now;
            application.ApplicationTypeID = (int)clsBLApplication.enApplicationType.ReleaseDetainedDrivingLicense;
            application.ApplicationStatus = 1;
            application.LastStatusDate = DateTime.Now;

            // الجمع بنوع decimal دون إلغاء الكسور العشرية
            application.PaidFees = appType.ApplicationFees + Convert.ToSingle(detainedLicense.FineFees);
            application.CreatedByUserID = clsGlobal.CurrentUser.UserID;

            if (!application.Save())
            {
                return -1;
            }

            bool isReleased = detainedLicense.Release(clsGlobal.CurrentUser.UserID, application.ApplicationID);

            if (!isReleased)
            {
                return -1;
            }

            application.ApplicationStatus = 3; // Completed
            if (!application.Save())
            {
                return -1;
            }

            return application.ApplicationID;
        }
        public static clsBLDetainedLicense FindByLicenseID(int licenseID)
        {
            int detainID = -1;
            DateTime detainDate = DateTime.MinValue;
            decimal fineFees = 0;
            int createdByUserID = -1;
            bool isReleased = false;
            DateTime releaseDate = DateTime.MinValue;
            int releasedByUserID = -1;
            int releaseApplicationID = -1;

            if (clsDALDetainedLicenses.GetDetainedLicenseInfoByLicenseID(licenseID, ref detainID, ref detainDate,
                ref fineFees, ref createdByUserID, ref isReleased, ref releaseDate, ref releasedByUserID, ref releaseApplicationID))
            {
                return new clsBLDetainedLicense(detainID, licenseID, detainDate, fineFees,
                    createdByUserID, isReleased, releaseDate, releasedByUserID, releaseApplicationID);
            }
            else
            {
                return null;
            }
        }
    }
}