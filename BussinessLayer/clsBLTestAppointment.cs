using BuisnessLayer;
using DataAccessLayer;
using DVLD_BLL;
using System;
using System.Data;
using static BuisnessLayer.clsBLApplication;

namespace BusinessLayer
{
    public class clsBLTestAppointment
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int TestAppointmentID { set; get; }
        public int TestTypeID { set; get; }
        public int LocalDrivingLicenseApplicationID { set; get; }
        public DateTime AppointmentDate { set; get; }
        public decimal PaidFees { set; get; }
        public int CreatedByUserID { set; get; }
        public bool IsLocked { set; get; }
        public int RetakeTestApplicationID { set; get; }

        public clsBLTestAppointment()
        {
            this.TestAppointmentID = -1;
            this.TestTypeID = -1;
            this.LocalDrivingLicenseApplicationID = -1;
            this.AppointmentDate = DateTime.Now;
            this.PaidFees = 0;
            this.CreatedByUserID = -1;
            this.IsLocked = false;
            this.RetakeTestApplicationID = -1;
            Mode = enMode.AddNew;
        }

        private clsBLTestAppointment(int testAppointmentID, int testTypeID, int localDrivingLicenseApplicationID,
            DateTime appointmentDate, decimal paidFees, int createdByUserID, bool isLocked, int retestTestAppointmentID)
        {
            this.TestAppointmentID = testAppointmentID;
            this.TestTypeID = testTypeID;
            this.LocalDrivingLicenseApplicationID = localDrivingLicenseApplicationID;
            this.AppointmentDate = appointmentDate;
            this.PaidFees = paidFees;
            this.CreatedByUserID = createdByUserID;
            this.IsLocked = isLocked;
            this.RetakeTestApplicationID = retestTestAppointmentID;
            Mode = enMode.Update;
        }

        public static clsBLTestAppointment Find(int testAppointmentID)
        {
            int testTypeID = -1;
            int localDrivingLicenseApplicationID = -1;
            DateTime appointmentDate = DateTime.Now;
            decimal paidFees = 0;
            int createdByUserID = -1;
            bool isLocked = false;
            int retestTestAppointmentID = -1;

            if (clsDALTestAppointment.GetTestAppointmentByID(testAppointmentID, ref testTypeID, ref localDrivingLicenseApplicationID,
                ref appointmentDate, ref paidFees, ref createdByUserID, ref isLocked, ref retestTestAppointmentID))
            {
                return new clsBLTestAppointment(testAppointmentID, testTypeID, localDrivingLicenseApplicationID,
                    appointmentDate, paidFees, createdByUserID, isLocked, retestTestAppointmentID);
            }
            else
            {
                return null;
            }
        }

        public static DataTable GetAllTestAppointments()
        {
            return clsDALTestAppointment.GetAllTestAppointments();
        }

        public static DataTable GetApplicationTestAppointments(int localDrivingLicenseApplicationID)
        {
            return clsDALTestAppointment.GetApplicationTestAppointments(localDrivingLicenseApplicationID);
        }

        private bool _AddNewTestAppointment()
        {
            this.TestAppointmentID = clsDALTestAppointment.AddNewTestAppointment(
                this.TestTypeID, this.LocalDrivingLicenseApplicationID,
                this.AppointmentDate, this.PaidFees, this.CreatedByUserID, this.RetakeTestApplicationID);

            return (this.TestAppointmentID != -1);
        }

        private bool _UpdateTestAppointment()
        {
            return clsDALTestAppointment.UpdateTestAppointment(
                this.TestAppointmentID, this.TestTypeID, this.LocalDrivingLicenseApplicationID,
                this.AppointmentDate, this.PaidFees, this.CreatedByUserID, this.IsLocked, this.RetakeTestApplicationID);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewTestAppointment())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    return false;

                case enMode.Update:
                    return _UpdateTestAppointment();
            }

            return false;
        }

        public static bool DeleteTestAppointment(int testAppointmentID)
        {
            return clsDALTestAppointment.DeleteTestAppointment(testAppointmentID);
        }
        public static bool IsThereAnActiveAppointment(int localDrivingLicenseApplicationID, int testTypeID)
        {
            return clsDALTestAppointment.IsThereAnActiveAppointment(localDrivingLicenseApplicationID, testTypeID);
        }

        public static bool ScheduleNewTestAppointment(int localDrivingLicenseApplicationID, int testTypeID, DateTime appointmentDate)
        {
            try
            {
                // 1. فحص هل اجتاز الاختبار سابقاً
                if (clsBLTest.DoesPassTestType(localDrivingLicenseApplicationID, testTypeID))
                {
                    Console.WriteLine("تتبع 1: فشل بسبب أن المتقدم اجتاز هذا الاختبار سابقاً!");
                    return false;
                }

                // 2. فحص هل يوجد موعد نشط حالياً
                if (clsBLTestAppointment.IsThereAnActiveAppointment(localDrivingLicenseApplicationID, testTypeID))
                {
                    Console.WriteLine("تتبع 2: فشل بسبب وجود موعد نشط ومحجوز حالياً لهذا الفحص!");
                    return false;
                }

                // 3. فحص وجود الطلب المحلي وعدد المحاولات السابقة
                var localApp = clsBLLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID(localDrivingLicenseApplicationID);
                if (localApp == null)
                {
                    Console.WriteLine("تتبع 3أ: فشل بسبب عدم العثور على طلب الرخصة المحلي (localApp == null)!");
                    return false;
                }

                byte totalTrials = localApp.TotalTrialsPerTest((clsBLTestType.enTestType)testTypeID);
                if (totalTrials > 0)
                {
                    Console.WriteLine($"تتبع 3ب: فشل بسبب وجود محاولات رسوب سابقة ({totalTrials})! المفروض يتوجه لمسار الإعادة وليس جديد.");
                    return false;
                }

                // 4. فحص جلب رسوم نوع الاختبار
                var testType = clsBLTestType.Find(testTypeID);
                if (testType == null)
                {
                    Console.WriteLine($"تتبع 4: فشل بسبب عدم العثور على نوع الاختبار رقم ({testTypeID}) في جدول TestTypes!");
                    return false;
                }

                // 5. فحص تسجيل دخول المستخدم الحالي
                if (clsGlobal.CurrentUser == null)
                {
                    Console.WriteLine("تتبع 5: فشل بسبب أن clsGlobal.CurrentUser يساوي null!");
                    return false;
                }

                // 6. إنشـاء وتعبئة الكائن
                clsBLTestAppointment appointment = new clsBLTestAppointment();
                appointment.LocalDrivingLicenseApplicationID = localDrivingLicenseApplicationID;
                appointment.TestTypeID = testTypeID;
                appointment.AppointmentDate = appointmentDate;
                appointment.PaidFees = (decimal)testType.TestTypeFees;
                appointment.CreatedByUserID = clsGlobal.CurrentUser.UserID;
                appointment.RetakeTestApplicationID = -1;

                // 7. محاولة الحفظ في قاعدة البيانات
                if (!appointment.Save())
                {
                    Console.WriteLine("تتبع 6: فشل داخل appointment.Save()! (تأكد من الـ DAL وقيم DBNull.Value للـ RetakeTestApplicationID)");
                    return false;
                }

                Console.WriteLine("تتبع نجاح: تم حفظ الموعد الجديد بنجاح!");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("استثناء خطأ (Exception): " + ex.Message);
                return false;
            }
        }

        public static clsBLTestAppointment ScheduleRetakeTest(int localDrivingLicenseApplicationID, int testTypeID, DateTime appointmentDate)
        {
            // 1. التحقق من الرسوب في أحدث اختبار
            if (!clsBLTest.DoesFailTestType(localDrivingLicenseApplicationID, testTypeID))
            {
                Console.WriteLine("DEBUG: الشخص ليس راسباً في هذا الاختبار!");
                return null;
            }

            // 2. التحقق من عدم وجود موعد نشط حالياً
            if (clsBLTestAppointment.IsThereAnActiveAppointment(localDrivingLicenseApplicationID, testTypeID))
            {
                Console.WriteLine("DEBUG: يوجد موعد نشط بالفعل لهذا الاختبار!");
                return null;
            }

            // 3. جلب بيانات طلب الرخصة المحلي للحصول على رقم الشخص
            clsBLLocalDrivingLicenseApplication localApp = clsBLLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID(localDrivingLicenseApplicationID);
            if (localApp == null)
            {
                Console.WriteLine("DEBUG: لم يتم العثور على طلب الرخصة المحلي!");
                return null;
            }

            // 4. إنشاء طلب إعادة الاختبار وحفظه في جدول Applications (بدلاً من البحث عن طلب سابق)
            clsBLApplication retakeApplication = new clsBLApplication();
            retakeApplication.ApplicantPersonID = localApp.BaseApplicationInfo.ApplicantPersonID;
            retakeApplication.ApplicationDate = DateTime.Now;
            retakeApplication.ApplicationTypeID = (int)clsBLApplication.enApplicationType.RetakeTest;
            retakeApplication.ApplicationStatus = (int)clsBLApplication.enStatus.Completed;
            retakeApplication.LastStatusDate = DateTime.Now;
            retakeApplication.PaidFees = clsBLApplicationType.Find((int)clsBLApplication.enApplicationType.RetakeTest).ApplicationFees;
            retakeApplication.CreatedByUserID = clsGlobal.CurrentUser.UserID;

            if (!retakeApplication.Save())
            {
                Console.WriteLine("DEBUG: فشل حفظ طلب الإعادة الجديد في جدول Applications!");
                return null;
            }

            // 5. إنشاء موعد الاختبار وربطه برقم طلب الإعادة الجديد المتولد تلقائياً
            clsBLTestAppointment appointment = new clsBLTestAppointment();
            appointment.TestTypeID = testTypeID;
            appointment.LocalDrivingLicenseApplicationID = localDrivingLicenseApplicationID;
            appointment.AppointmentDate = appointmentDate;
            appointment.PaidFees = (decimal)clsBLTestType.Find(testTypeID).TestTypeFees;
            appointment.CreatedByUserID = clsGlobal.CurrentUser.UserID;
            appointment.IsLocked = false;

            // هان نقطة الربط الجوهرية: إسناد الـ ID الجديد للطلب الذي أنشأناه لتوّنا
            appointment.RetakeTestApplicationID = retakeApplication.ApplicationID;

            // 6. حفظ الموعد في جدول TestAppointments
            if (!appointment.Save())
            {
                Console.WriteLine("DEBUG: فشل حفظ الموعد في قاعدة البيانات!");
                return null;
            }

            Console.WriteLine("DEBUG: تم حفظ طلب الإعادة والموعد بنجاح!");
            return appointment;
        }
        public static int GetActiveRetakeTestApplicationID(int localDrivingLicenseApplicationID, int testTypeID)
        {
            return clsDALTestAppointment.GetActiveRetakeTestApplicationID(localDrivingLicenseApplicationID, testTypeID);
        }
        public static DataTable GetApplicationTestAppointmentsPerTestType(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            return clsDALTestAppointment.GetApplicationTestAppointmentsPerTestType(LocalDrivingLicenseApplicationID, TestTypeID);
        }
    }
}