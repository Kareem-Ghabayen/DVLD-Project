using BuisnessLayer;
using DataAccessLayer;
using System;
using System.Data;

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
        public int RetestTestAppointmentID { set; get; }

        public clsBLTestAppointment()
        {
            this.TestAppointmentID = -1;
            this.TestTypeID = -1;
            this.LocalDrivingLicenseApplicationID = -1;
            this.AppointmentDate = DateTime.Now;
            this.PaidFees = 0;
            this.CreatedByUserID = -1;
            this.IsLocked = false;
            this.RetestTestAppointmentID = -1;
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
            this.RetestTestAppointmentID = retestTestAppointmentID;
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
                this.AppointmentDate, this.PaidFees, this.CreatedByUserID, this.RetestTestAppointmentID);

            return (this.TestAppointmentID != -1);
        }

        private bool _UpdateTestAppointment()
        {
            return clsDALTestAppointment.UpdateTestAppointment(
                this.TestAppointmentID, this.TestTypeID, this.LocalDrivingLicenseApplicationID,
                this.AppointmentDate, this.PaidFees, this.CreatedByUserID, this.IsLocked, this.RetestTestAppointmentID);
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

        public static bool ScheduleNewTestAppointment(int localDrivingLicenseApplicationID, int testTypeID)
        {
            if (clsBLTest.DoesPassTestType(localDrivingLicenseApplicationID, testTypeID))
            {
                return false;
            }

            if (clsBLTestAppointment.IsThereAnActiveAppointment(localDrivingLicenseApplicationID, testTypeID))
            {
                return false;
            }
            clsBLTestAppointment appointment = new clsBLTestAppointment();

            appointment.TestTypeID = testTypeID;
            appointment.LocalDrivingLicenseApplicationID = localDrivingLicenseApplicationID;

            appointment.AppointmentDate = DateTime.Now; // (طبعاً التاريخ بتجيبه من الشاشة أو الكنترول حسب ما بختاره المستخدم)

            appointment.PaidFees = (decimal)clsBLTestType.Find(testTypeID).TestTypeFees;
            appointment.CreatedByUserID = clsGlobal.CurrentUser.UserID; // المستخدم الحالي الحقيقي
                                                                        //
                                                                        // appointment.IsLocked = false; // الموعد جديد لسه ما تقفل

            // 3. استدعاء ميثود الحفظ من كلاس المواعيد (المنفذ الفني)
            if (appointment.Save())
            {
                return true;
            }

            return false;
        }
    }
}