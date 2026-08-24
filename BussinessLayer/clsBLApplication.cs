using DataAccessLayer;
using System;
using System.Data;
using System.Runtime.Remoting.Messaging;
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
            return clsDALApplication.UpdateStatus(this.ApplicationID, (byte)enStatus.Cancelled);
        }

        public bool SetComplete()
        {
            return clsDALApplication.UpdateStatus(this.ApplicationID, (byte)enStatus.Completed);
        }
        // استعمال داخلي 
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

        //  استعمال داخلي  + عليها مراجعة
        public static bool IsThereAnActiveApplicationInSameLicenses(int applicantPersonID, int applicationTypeID, int licenseClassID)
        {
            return clsDALApplication.IsThereAnActiveApplicationInSameLicenses(applicantPersonID, applicationTypeID, licenseClassID);
        }



        //  استعمال داخل 
 private bool _CheckApplicationRules(int licenseClassID)
        {
            float requiredFees = clsBLApplicationType.Find(this.ApplicationTypeID).ApplicationFees;

            //if (this.ApplicationTypeID != (int)enApplicationType.NewDrivingLicense)
            //{
            //    if (clsBLApplication.DoesPersonHaveActiveApplication(this.ApplicantPersonID, this.ApplicationTypeID))
            //    {
            //        return false; 
            //    }
            //}

            switch (this.ApplicationTypeID)
    {
        case (int)enApplicationType.NewDrivingLicense: 
            if (this.PaidFees != requiredFees || IsThereAnActiveApplicationInSameLicenses(this.ApplicantPersonID, this.ApplicationTypeID, licenseClassID)|| clsBLLicense.IsLicenseExistByPersonIDAndLicenseClass(this.ApplicantPersonID, licenseClassID))
                return false;

            break;

        case (int)enApplicationType.RenewDrivingLicense:
                    if (!clsBLLicense.IsLicenseExist(oldLicenseID))
                    {
                        return false;
                    }
                    if (this.PaidFees != requiredFees || IsThereAnActiveApplicationInSameLicenses(this.ApplicantPersonID, this.ApplicationTypeID, licenseClassID))
                return false;
            break;

        case (int)enApplicationType.ReplacementForLost:
            // بدل فاقد: الشخص لازم يكون عنده رخصة أصلاً عشان نطلعلو بدل فاقد، وفحص رسوم البدل الفاقد
            // if (!clsBLLicense.DoesPersonHaveLicenseByClass(this.ApplicantPersonID, licenseClassID)) return false;
            // if (this.PaidFees != ExpectedLostFees) return false;
            break;

        case (int)enApplicationType.ReplacementForDamaged:
            // بدل تالف: مشابه للفاقد، لازم تكون الرخصة موجودة والرسوم مدفوعة
            // if (!clsBLLicense.DoesPersonHaveLicenseByClass(this.ApplicantPersonID, licenseClassID)) return false;
            // if (this.PaidFees != ExpectedDamagedFees) return false;
            break;

        case (int)enApplicationType.ReleaseDetainedDrivingLicense:
            // الإفراج عن رخصة محجوزة: 
            // الشرط الأساسي: هل هذه الرخصة محجوزة أصلاً؟ (لو مش محجوزة ما بيزبط يقدم طلب إفراج!)
            // if (!clsBLDetainedLicense.IsLicenseDetained(licenseID)) return false;
            break;

        case (int)enApplicationType.NewInternationalLicense:
            // رخصة دولية: 
            // شرط أساسي: عشان يطلع رخصة دولية، لازم يكون عنده رخصة "محليّة" سارية المفعول أولاً!
            // if (!clsBLLicense.DoesPersonHaveActiveLocalLicense(this.ApplicantPersonID)) return false;
            break;

        case (int)enApplicationType.RetakeTest:
            // إعادة امتحان: (عادة بيتم فحص إذا رسب في الامتحان السابق لهذه الفئة ليُسمح له بإعادة الاختبار)
            break;

        default:
            // لأي نوع طلب غير متوقع
            break;
    }

    return true; 
}


    }
}