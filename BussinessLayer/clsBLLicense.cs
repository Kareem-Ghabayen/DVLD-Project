using DataAccessLayer;
using DVLD_BLL;
using DVLD_DataAccess;
using System;
using System.Data;
using static BuisnessLayer.clsBLApplication;

namespace BuisnessLayer // أو حسب اسم الـ Namespace عندك
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
            this.DriverInfo= clsBLDriver.FindByDriverID(this.DriverID);
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
        //public static clsBLLicense FindByLicenseID(int licenseID)
        //{
        //    // هاي الدالة بترجع لك كائن كامل بيحتوي على كل بيانات الرخصة (الفئة، تاريخ الانتهاء، الحالة، الـ DriverID، إلخ)
        //    return clsDALLicenses.FindByLicenseID(licenseID);
        //}
        public static DataTable GetDriverLicenses(int DriverID)
        {
            return clsDALLicenses.GetDriverLicenses(DriverID);
        }

        //  هان طبعا فيه هيتم شروط كثيرة على طريقة الاضافة يجب تجاوزها 
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

        public  bool IsLicenseActiveByLicenseID( )
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

        public static clsBLLicense RenewLicense(int oldLicenseID)
        {
            clsBLLicense oldLicense = clsBLLicense.FindByLicenseID(oldLicenseID);
            if (oldLicense == null)
            {
                return null;
            }
            
            if (oldLicense.IsLicenseActiveByLicenseID())
            {
                return null;
            }
                        if (clsBLApplication.IsThereAnActiveApplicationInSameLicenses(oldLicense.DriverInfo.PersonID, (int)enApplicationType.RenewDrivingLicense, oldLicense.LicenseClass))
            {
                return null; 
            }


            clsBLApplication application = new clsBLApplication();

            application.ApplicantPersonID = oldLicense.DriverInfo.PersonID;
            application.ApplicationDate = DateTime.Now;
            application.ApplicationTypeID = (int)enApplicationType.RenewDrivingLicense;
            application.ApplicationStatus = 1; 
            application.LastStatusDate = DateTime.Now;
            application.PaidFees = clsBLApplicationType.Find((int)enApplicationType.RenewDrivingLicense).ApplicationFees;
            application.CreatedByUserID = clsGlobal.CurrentUser.UserID;

            float requiredFees = clsBLApplicationType.Find(application.ApplicationTypeID).ApplicationFees;
            if (application.PaidFees != requiredFees)
            {
                return null;
            }


            //  هان لازم احط شرط يربط نتيجة فحص النظر طيب 

            if (!application.Save())
            {
                return null;
            }

            oldLicense.IsActive = false;
            if (!oldLicense.Save())
            {
                return null;
            }

            clsBLLicense newLicense = new clsBLLicense();

            newLicense.ApplicationID = application.ApplicationID;
            newLicense.DriverID = oldLicense.DriverID;
            newLicense.LicenseClass = oldLicense.LicenseClass;
            newLicense.IssueDate = DateTime.Now;

            byte validityLength = clsBLLicenseClass.Find(oldLicense.LicenseClass).DefaultValidityLength;
            newLicense.ExpirationDate = DateTime.Now.AddYears(validityLength);

            newLicense.Notes = "";
            newLicense.PaidFees = clsBLLicenseClass.Find(oldLicense.LicenseClass).ClassFees;
            newLicense.IsActive = true;
            newLicense.IssueReason = (int)enIssueReason.Renew;
            newLicense.CreatedByUserID = clsGlobal.CurrentUser.UserID;

            if (!newLicense.Save())
            {
                return null;
            }

            return newLicense;
        }
        public int IssueLicenseFirstTime(int localDrivingLicenseApplicationID, string notes = "")
        {
            // 1. جلب الطلب المحلي وفحص هل اجتاز الـ 3 اختبارات
            clsBLLocalDrivingLicenseApplication localApp = clsBLLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID(localDrivingLicenseApplicationID);

            if (localApp == null || localApp.GetPassedTestCount() < 3)
                return -1; // يا إما الطلب مش موجود أو ما خلص الفحوصات

            // 2. جلب الطلب الأساسي (Application) عشان نقدر نطلع منها رقم الشخص (ApplicantPersonID)
            clsBLApplication baseApplication = clsBLApplication.Find(localApp.ApplicationID);
            if (baseApplication == null)
                return -1;

            // 3. جلب أو إنشاء رقم السائق (DriverID) تلقائياً باستخدام رقم الشخص
            int driverID = clsBLDriver.GetOrCreateDriverID(baseApplication.ApplicantPersonID);
            if (driverID == -1)
                return -1; // لو فشل في جلب أو إنشاء السائق
            clsBLLicenseClass licenseClassInfo = clsBLLicenseClass.Find(localApp.LicenseClassID);
            if (licenseClassInfo == null)
                return -1;
            // 4. تعبئة بيانات الرخصة
            this.ApplicationID = localApp.ApplicationID;
            this.DriverID = driverID; // رقم السائق الصحيح
            this.LicenseClass = localApp.LicenseClassID;
            this.IssueDate = DateTime.Now;
            this.ExpirationDate = DateTime.Now.AddYears(licenseClassInfo.DefaultValidityLength); // جبناها من كلاس الفئة صح
                                                                                                 // this.Notes = notes; // لو فاضية بتضل فاضية
            this.PaidFees = licenseClassInfo.ClassFees;
            this.IsActive = true;
            this.IssueReason = 1; // First Time
            this.CreatedByUserID = clsGlobal.CurrentUser.UserID; // جبناه من الكلاس العالمي مباشرة

            // 5. الحفظ وتحديث حالة الطلب
            if (this.Save())
            {
                baseApplication.SetComplete();
                return this.LicenseID;
            }

            return -1;
        }

        public clsBLLicense Replace(enIssueReason issueReason, int applicationID)
        {
            // 1. إلغاء تفعيل الرخصة القديمة الحالية
            this.IsActive = false;
            this.Save(); // أو ميثود خاصة بإلغاء التفعيل

            // 2. إنشاء رخصة جديدة كبديل للرخصة الحالية
            clsBLLicense newLicense = new clsBLLicense();

            newLicense.ApplicationID = applicationID;
            newLicense.DriverID = this.DriverID;
            newLicense.LicenseClass = this.LicenseClass;
            newLicense.IssueDate = DateTime.Now;

            // حساب تاريخ انتهاء الرخصة بناءً على فئة الرخصة (License Class Default Validity Length)
            clsBLLicenseClass licenseClassInfo = clsBLLicenseClass.Find(this.LicenseClass);
            if (licenseClassInfo != null)
            {
                newLicense.ExpirationDate = this.ExpirationDate;
            }
            else
            {
                newLicense.ExpirationDate = DateTime.Now.AddYears(10); // قيمة افتراضية مثلاً
            }

            newLicense.PaidFees = licenseClassInfo != null ? licenseClassInfo.ClassFees : 0;
            newLicense.IsActive = true;
            newLicense.IssueReason = (byte)issueReason; // هنا رح تكون Lost (مثلاً 3 أو حسب إعدادات السيستم)
            newLicense.CreatedByUserID = clsGlobal.CurrentUser.UserID;

            // 3. حفظ الرخصة الجديدة في الداتابيز
            if (!newLicense.Save())
            {
                return null;
            }

            return newLicense; // إرجاع أوبجيكت الرخصة الجديدة الناجحة
        }
        public static clsBLLicense GetActiveClass3LicenseByNationalNo(string nationalNo)
        {
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

            // استدعاء داتابيز
            if (clsDALLicenses.GetActiveClass3LicenseInfoByNationalNo(nationalNo,
                ref licenseID, ref applicationID, ref driverID, ref licenseClass,
                ref issueDate, ref expirationDate, ref notes, ref paidFees,
                ref isActive, ref issueReason, ref createdByUserID))
            {
                // إرجاع كائن رخصة جديد جاهز بالبيانات
                return new clsBLLicense(licenseID, applicationID, driverID, licenseClass,
                    issueDate, expirationDate, notes, paidFees, isActive, issueReason, createdByUserID);
            }
            else
            {
                return null;
            }
        }
    }
}