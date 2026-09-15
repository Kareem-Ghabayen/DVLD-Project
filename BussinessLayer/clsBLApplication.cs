using BusinessLayer;
using DataAccessLayer;
using System;
using System.Data;
using System.Runtime.Remoting.Messaging;
using static BuisnessLayer.clsBLLicense;
using static System.Net.Mime.MediaTypeNames;

namespace BuisnessLayer
{
    public class clsBLApplication
    {
        public enum enMode { AddNew = 0, Update = 1 }
        public enMode Mode = enMode.AddNew;
        public enum enStatus { New = 1, Cancelled = 2, Completed = 3 }

        public int ApplicationID { set; get; }
        public int ApplicantPersonID { set; get; }
        public clsBLSPeople ApplicantPersonInfo { set; get; }
        public DateTime ApplicationDate { set; get; }
        public int ApplicationTypeID { set; get; }
        public clsBLApplicationType ApplicationTypeInfo { set; get; }
        public byte ApplicationStatus { set; get; }

        public string StatusText 
        {
            get 
            {
                switch (ApplicationStatus)
                {
                    case 1:
                        return "New";
                    case 2:
                        return "Cancelled";
                    case 3:
                        return "Completed";
                    default:
                        return "Unknown";
                }
            }
        }
        public DateTime LastStatusDate { set; get; }
        public float PaidFees { set; get; }
        public int CreatedByUserID { set; get; }
        public clsBLUser CreatedByUserInfo { set; get; }

        public clsBLApplication()
        {
            this.ApplicationID = -1;
            this.ApplicantPersonID = -1;
            this.ApplicationDate = DateTime.Now;
            this.ApplicationTypeID = -1;
            this.ApplicationStatus = (byte)enStatus.New;
            this.LastStatusDate = DateTime.Now;
            this.PaidFees = 0;
            this.CreatedByUserID = -1;
            Mode = enMode.AddNew;
        }

        private clsBLApplication(int ApplicationID, int ApplicantPersonID, DateTime ApplicationDate,
            int ApplicationTypeID, byte ApplicationStatus, DateTime LastStatusDate,
            float PaidFees, int CreatedByUserID)
        {
            this.ApplicationID = ApplicationID;
            this.ApplicantPersonID = ApplicantPersonID;
            this.ApplicantPersonInfo = clsBLSPeople.FindByID(ApplicantPersonID);//  هان انا حملت معلومات الشخص من خلال ال  id  
            this.ApplicationDate = ApplicationDate;
            this.ApplicationTypeID = ApplicationTypeID;
            this.ApplicationTypeInfo = clsBLApplicationType.Find(ApplicationTypeID);
            this.ApplicationStatus = ApplicationStatus;
            this.LastStatusDate = LastStatusDate;
            this.PaidFees = PaidFees;
            this.CreatedByUserID = CreatedByUserID;
            this.CreatedByUserInfo = clsBLUser.FindByUserID(CreatedByUserID);
            Mode = enMode.Update;
        }
        private bool _AddNewApplication()
        {
            this.ApplicationID = clsDALApplication.AddNewApplication(
                this.ApplicantPersonID, this.ApplicationDate,
                this.ApplicationTypeID, this.ApplicationStatus,
                this.LastStatusDate, this.PaidFees, this.CreatedByUserID
            );

            return (this.ApplicationID != -1);
        }

        private bool _UpdateApplication()
        {
            return clsDALApplication.UpdateApplication(
                this.ApplicationID, this.ApplicantPersonID, this.ApplicationDate,
                this.ApplicationTypeID, this.ApplicationStatus,
                this.LastStatusDate, this.PaidFees, this.CreatedByUserID
            );
        }

        public static clsBLApplication Find(int ApplicationID)
        {
            int ApplicantPersonID = -1;
            DateTime ApplicationDate = DateTime.Now;
            int ApplicationTypeID = -1;
            byte ApplicationStatus = 1;
            DateTime LastStatusDate = DateTime.Now;
            float PaidFees = 0;
            int CreatedByUserID = -1;

            bool IsFound = clsDALApplication.GetApplicationInfoByID(
                ApplicationID, ref ApplicantPersonID, ref ApplicationDate,
                ref ApplicationTypeID, ref ApplicationStatus, ref LastStatusDate,
                ref PaidFees, ref CreatedByUserID
            );

            if (IsFound)
            {
                return new clsBLApplication(ApplicationID, ApplicantPersonID, ApplicationDate,
                    ApplicationTypeID, ApplicationStatus, LastStatusDate, PaidFees, CreatedByUserID);
            }
            else
            {
                return null;
            }
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:

                    if (_AddNewApplication())
                    {
                        Mode = enMode.Update;

                        return true;
                    }
                    return false;

                case enMode.Update:
                    return _UpdateApplication();
            }

            return false;
        }

        public static DataTable GetAllApplications()
        {
            return clsDALApplication.GetAllApplications();
        }

        public static DataTable GetApplicationsByPersonID(int PersonID)
        {
            return clsDALApplication.GetApplicationsByPersonID(PersonID);
        }

        public static bool DeleteApplication(int ApplicationID)
        {
            if (!IsApplicationExist(ApplicationID))
                return false;
            return clsDALApplication.DeleteApplication(ApplicationID);
        }

        public static bool IsApplicationExist(int ApplicationID)
        {
            return clsDALApplication.IsApplicationExist(ApplicationID);
        }
        public bool Cancel()
        {
            if (this.ApplicationStatus != (byte)enStatus.New)
            {
                return false; 
            }
            if (clsDALApplication.UpdateStatus(this.ApplicationID, (byte)enStatus.Cancelled))
            {
                this.ApplicationStatus = (byte)enStatus.Cancelled;
                this.LastStatusDate = DateTime.Now;
                return true;
            }

            return false;
        }

        public bool SetComplete()
        {
            return clsDALApplication.UpdateStatus(this.ApplicationID, (byte)enStatus.Completed);
        }
        public static bool DoesPersonHaveActiveApplication(int PersonID, int ApplicationTypeID)
        {
            return clsDALApplication.DoesPersonHaveActiveApplication(PersonID, ApplicationTypeID);
        }




        public enum enApplicationType
        {
            NewDrivingLicense = 1,
            RenewDrivingLicense = 2,
            ReplacementForLost = 3,
            ReplacementForDamaged = 4,
            ReleaseDetainedDrivingLicense = 5,
            NewInternationalLicense = 6,
            RetakeTest = 7
        }

        public static bool IsThereAnActiveApplicationInSameLicenses(int ApplicantPersonID, int ApplicationTypeID, int LicenseClassID)
        {
            return clsDALApplication.IsThereAnActiveApplicationInSameLicenses(ApplicantPersonID, ApplicationTypeID, LicenseClassID);
        }


        public static int CreateRetakeTestApplication(string nationalNo, int testTypeID)
        {
            int personID = clsBLSPeople.FindByNationalNo(nationalNo)?.ID ?? -1;
            if (personID == -1)
            {
                return -1; 
            }

            if (!clsBLTest.DoesFailTestType(personID, testTypeID))
            {
                return -1; 
            }

            clsBLApplication retakeApplication = new clsBLApplication();
            retakeApplication.ApplicantPersonID = personID;
            retakeApplication.ApplicationDate = DateTime.Now;
            retakeApplication.ApplicationTypeID = (int)enApplicationType.RetakeTest;
            retakeApplication.ApplicationStatus = 1;
            retakeApplication.LastStatusDate = DateTime.Now;

            retakeApplication.PaidFees = clsBLApplicationType.Find((int)enApplicationType.RetakeTest)?.ApplicationFees ?? 0;

            retakeApplication.CreatedByUserID = clsGlobal.CurrentUser.UserID;

            if (retakeApplication.Save())
            {
                return retakeApplication.ApplicationID;
            }

            return -1;
        }
        public static clsBLLicense ReplaceLostDrivingLicense(int licenseID)
        {
            clsBLLicense oldLicense = clsBLLicense.FindByLicenseID(licenseID);
            if (oldLicense == null || !oldLicense.IsActive)
            {
                return null;
            }

            clsBLApplication replacementApplication = new clsBLApplication();
            replacementApplication.ApplicantPersonID = oldLicense.DriverInfo.PersonID;
            replacementApplication.ApplicationDate = DateTime.Now;
            replacementApplication.ApplicationTypeID = (int)enApplicationType.ReplacementForLost; // تم التعديل هنا
            replacementApplication.ApplicationStatus = 3; // تعيين الحالة مباشرة كـ Completed قبل الحفظ
            replacementApplication.LastStatusDate = DateTime.Now;

            clsBLApplicationType appType = clsBLApplicationType.Find((int)enApplicationType.ReplacementForLost);
            replacementApplication.PaidFees = (appType != null) ? appType.ApplicationFees : 20;
            replacementApplication.CreatedByUserID = clsGlobal.CurrentUser.UserID;

            if (!replacementApplication.Save())
            {
                return null;
            }

            return oldLicense.Replace(enIssueReason.LostReplacement, replacementApplication.ApplicationID);
        }

        public static clsBLLicense ReplaceDamagedDrivingLicense(int licenseID)
        {
            clsBLLicense oldLicense = clsBLLicense.FindByLicenseID(licenseID);
            if (oldLicense == null || !oldLicense.IsActive)
            {
                return null;
            }

            clsBLApplication replacementApplication = new clsBLApplication();
            replacementApplication.ApplicantPersonID = oldLicense.DriverInfo.PersonID;
            replacementApplication.ApplicationDate = DateTime.Now;
            replacementApplication.ApplicationTypeID = (int)enApplicationType.ReplacementForDamaged;
            replacementApplication.ApplicationStatus = 3; // تعيين الحالة مباشرة كـ Completed قبل الحفظ
            replacementApplication.LastStatusDate = DateTime.Now;

            clsBLApplicationType appType = clsBLApplicationType.Find((int)enApplicationType.ReplacementForDamaged);
            replacementApplication.PaidFees = (appType != null) ? appType.ApplicationFees : 20;
            replacementApplication.CreatedByUserID = clsGlobal.CurrentUser.UserID;

            if (!replacementApplication.Save())
            {
                return null;
            }

            return oldLicense.Replace(enIssueReason.DamagedReplacement, replacementApplication.ApplicationID);
        }
    }
}