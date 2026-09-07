using System;
using System.Data;
using DataAccessLayer; // تأكد من اسم الـ namespace عندك

namespace BusinessLayer
{
    public class clsBLTestType
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.Update;
        public enum enTestType
        {
            VisionTest = 1,
            WrittenTest = 2,
            StreetTest = 3
        };
        public int TestTypeID { set; get; }
        public string TestTypeTitle { set; get; }
        public string TestTypeDescription { set; get; }
        public float TestTypeFees { set; get; }

        public clsBLTestType()
        {
            this.TestTypeID = -1;
            this.TestTypeTitle = "";
            this.TestTypeDescription = "";
            this.TestTypeFees = 0;
            Mode = enMode.AddNew;
        }

        private clsBLTestType(int testTypeID, string testTypeTitle, string testTypeDescription, float testTypeFees)
        {
            this.TestTypeID = testTypeID;
            this.TestTypeTitle = testTypeTitle;
            this.TestTypeDescription = testTypeDescription;
            this.TestTypeFees = testTypeFees;
            Mode = enMode.Update;
        }

        public static clsBLTestType Find(int testTypeID)
        {
            string title = "";
            string description = "";
            float fees = 0;

            if (clsDALTestTypes.GetTestTypeByID(testTypeID, ref title, ref description, ref fees))
            {
                return new clsBLTestType(testTypeID, title, description, fees);
            }
            else
            {
                return null;
            }
        }

        public static DataTable GetAllTestTypes()
        {
            return clsDALTestTypes.GetAllTestTypes();
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    // أنواع الاختبارات عادة لا تُضاف من التطبيق بل من الـ DB، لكن لو لزم...
                    Mode = enMode.Update;
                    return true;

                case enMode.Update:
                    return _UpdateTestType();
            }

            return false;
        }

        private bool _UpdateTestType()
        {
            return clsDALTestTypes.UpdateTestTypePrice(this.TestTypeID, this.TestTypeFees);
        }
    }
}