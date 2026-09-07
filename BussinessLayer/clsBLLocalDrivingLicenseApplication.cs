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
        // خصائص الكلاس (Properties)
        public int LocalDrivingLicenseApplicationID { get; set; }
        public int ApplicationID { get; set; }
        public int LicenseClassID { get; set; }

        // كلاس الأب (Composition للـ Application الأساسي)
        public clsBLApplication BaseApplicationInfo { get; set; }

        // Composition لفئة الرخصة
        public clsBLLicenseClass LicenseClassInfo { get; set; }

        // Constructor افتراضي
        public clsBLLocalDrivingLicenseApplication()
        {
            this.LocalDrivingLicenseApplicationID = -1;
            this.ApplicationID = -1;
            this.LicenseClassID = -1;
            this.LicenseClassInfo = null;
        }

        // Constructor خاص بالبحث
        private clsBLLocalDrivingLicenseApplication(int localDrivingLicenseApplicationID, int applicationID, int licenseClassID)
        {
            this.LocalDrivingLicenseApplicationID = localDrivingLicenseApplicationID;
            this.ApplicationID = applicationID;
            this.LicenseClassID = licenseClassID;
            this.BaseApplicationInfo = clsBLApplication.Find(applicationID);
            this.LicenseClassInfo = clsBLLicenseClass.Find(licenseClassID);
        }

        // 1. ميثود البحث باستخدام ID الطلب المحلي
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

        //// 2. ميثود البحث باستخدام ID الطلب الأساسي
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

        // 3. ميثود الحفظ (Save)
        public bool Save()
        {
            switch (LocalDrivingLicenseApplicationID)
            {
                case -1:
                    return _AddNewLocalDrivingLicenseApplication();
                default:
                    int oldClassID = FindByLocalDrivingLicenseApplicationID(this.LocalDrivingLicenseApplicationID).LicenseClassID;

                    // 2. إذا غير المستخدم الفئة من الشاشة، نفحص الفئة الجديدة
                    if (this.LicenseClassID != oldClassID)
                    {
                        if (clsBLLicense.IsLicenseExistByPersonIDAndLicenseClass(BaseApplicationInfo.ApplicantPersonID, this.LicenseClassID) ||
                            clsBLApplication.IsThereAnActiveApplicationInSameLicenses(BaseApplicationInfo.ApplicantPersonID, (int)clsBLApplicationType.enApplicationType.NewDrivingLicense, this.LicenseClassID))
                        {
                            return false; // يرفض التعديل لو الفئة الجديدة مكررة
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

        // 4. فحص عدد الاختبارات التي اجتازها المتقدم
        public int GetPassedTestCount()
        {
            return clsBLTest.GetPassedTestCount(this.LocalDrivingLicenseApplicationID);
        }

        // 5. ميثود إضافة طلب رخصة قيادة جديد باستخدام رقم الهوية (NationalNo)
        public static clsBLLocalDrivingLicenseApplication AddNewLocalDrivingLicenseApplication(int applicantPersonID, int licenseClassID)
        {
            // 1. البحث عن الشخص باستخدام رقم الهوية (NationalNo) بدلاً من الـ ID
            clsBLSPeople person = clsBLSPeople.FindByID(applicantPersonID);
            if (person == null)
            {
                return null; // الشخص غير مسجل بالنظام أصلاً
            }

            // 2. إنشاء الطلب الأساسي (Application)
            clsBLApplication application = new clsBLApplication();

            application.ApplicantPersonID = person.ID; // أخذنا الـ ID الحقيقي من بيانات الشخص الذي وجدناه
            application.ApplicationDate = DateTime.Now;
            application.ApplicationTypeID = (int)enApplicationType.NewDrivingLicense;
            application.ApplicationStatus = 1; // New
            application.LastStatusDate = DateTime.Now;

            clsBLApplicationType appType = clsBLApplicationType.Find((int)enApplicationType.NewDrivingLicense);
            if (appType == null) return null;

            application.PaidFees = appType.ApplicationFees;
            application.CreatedByUserID = clsGlobal.CurrentUser.UserID;

            // 3. التحقق من الشروط والقواعد (عدم وجود طلب فعال لنفس الفئة أو رخصة سابقة)
            if (application.PaidFees != appType.ApplicationFees ||
                IsThereAnActiveApplicationInSameLicenses(application.ApplicantPersonID, application.ApplicationTypeID, licenseClassID) ||
                clsBLLicense.IsLicenseExistByPersonIDAndLicenseClass(application.ApplicantPersonID, licenseClassID))
            {
                return null;
            }

            // 4. حفظ الطلب الأساسي
            if (!application.Save())
            {
                return null;
            }

            // 5. إنشاء وتعبئة الطلب المحلي (Local Driving License Application)
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
    }
}