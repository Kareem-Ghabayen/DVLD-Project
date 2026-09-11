using BuisnessLayer;
using DataAccessLayer;
using System;
using System.Data;

namespace BusinessLayer
{
    public class clsBLTest
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int TestID { set; get; }
        public int TestAppointmentID { set; get; }
        public bool TestResult { set; get; }
        public string Notes { set; get; }
        public int CreatedByUserID { set; get; }

        public clsBLTest()
        {
            this.TestID = -1;
            this.TestAppointmentID = -1;
            this.TestResult = false;
            this.Notes = "";
            this.CreatedByUserID = -1;
            Mode = enMode.AddNew;
        }

        private clsBLTest(int testID, int testAppointmentID, bool testResult, string notes, int createdByUserID)
        {
            this.TestID = testID;
            this.TestAppointmentID = testAppointmentID;
            this.TestResult = testResult;
            this.Notes = notes;
            this.CreatedByUserID = createdByUserID;
            Mode = enMode.Update;
        }

        public static clsBLTest Find(int testID)
        {
            int testAppointmentID = -1;
            bool testResult = false;
            string notes = "";
            int createdByUserID = -1;

            if (clsDALTest.GetTestInfoByTestID(testID, ref testAppointmentID, ref testResult, ref notes, ref createdByUserID))
            {
                return new clsBLTest(testID, testAppointmentID, testResult, notes, createdByUserID);
            }
            else
            {
                return null;
            }
        }

        public static clsBLTest FindByTestAppointmentID(int testAppointmentID)
        {
            int testID = -1;
            bool testResult = false;
            string notes = "";
            int createdByUserID = -1;

            if (clsDALTest.GetTestInfoByTestAppointmentID(testAppointmentID, ref testID, ref testResult, ref notes, ref createdByUserID))
            {
                return new clsBLTest(testID, testAppointmentID, testResult, notes, createdByUserID);
            }
            else
            {
                return null;
            }
        }

        public static DataTable GetAllTests()
        {
            return clsDALTest.GetAllTests();
        }

        private bool _AddNewTest()
        {
            this.TestID = clsDALTest.AddNewTest(
                this.TestAppointmentID,
                this.TestResult,
                this.Notes,
                this.CreatedByUserID
            );

            return (this.TestID != -1);
        }

        private bool _UpdateTest()
        {
            return clsDALTest.UpdateTest(
                this.TestID,
                this.TestAppointmentID,
                this.TestResult,
                this.Notes,
                this.CreatedByUserID
            );
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewTest())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    return false;

                case enMode.Update:
                    return _UpdateTest();
            }

            return false;
        }

        public static byte GetPassedTestCount(int localDrivingLicenseApplicationID)
        {
            return clsDALTest.GetPassedTestCount(localDrivingLicenseApplicationID);
        }

        public static bool DoesPassTestType(int localDrivingLicenseApplicationID, int testTypeID)
        {
            return clsDALTest.DoesPassTestType(localDrivingLicenseApplicationID, testTypeID);
        }


        public static bool TakeTest(int testAppointmentID, bool testResult, string notes)
        {
            if (FindByTestAppointmentID(testAppointmentID)!=null) 
            {
                return false;
            }
            clsBLTest test = new clsBLTest();
            test.TestAppointmentID = testAppointmentID;
            test.TestResult = testResult; // true = Pass, false = Fail
            test.Notes = notes;
            test.CreatedByUserID = clsGlobal.CurrentUser.UserID; // جبناه من الكلاس العالمي مباشرة

            if (test.Save()) // حفظ النتيجة في جدول Tests
            {
                // 2. بعد ما حفظنا النتيجة، لازم نقفل الموعد الأصلي عشان يصير مغلق وما ينحجز مرة ثانية
                clsBLTestAppointment appointment = clsBLTestAppointment.Find(testAppointmentID);
                if (appointment != null)
                {
                    appointment.IsLocked = true;
                    appointment.Save(); // تحديث حالة الموعد ليصبح مقفل
                }

                return true;
            }

            return false;
        }
        public static bool DoesFailTestType(int localDrivingLicenseApplicationID, int testTypeID)
        {
            // استدعاء الداتا لاير بتمرير رقم طلب الرخصة المحلي
            return clsDALTest.DoesFailTestType(localDrivingLicenseApplicationID, testTypeID);
        }
    }
}