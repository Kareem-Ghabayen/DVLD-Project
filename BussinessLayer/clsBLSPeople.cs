


﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;

namespace BuisnessLayer
{
    public class clsBLSPeople
    {

        public enum enMode { AddNew = 0, UpdateNew = 1 }
        public enMode Mode = enMode.AddNew;

        public int ID { get; set; }
        public string NationalNo { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public byte Gendor { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int NationalityCountryID { get; set; }
        public string ImagePath { get; set; }

        public clsBLSPeople()
        {
            this.ID = -1;
            this.NationalNo = "";
            this.FirstName = "";
            this.SecondName = "";
            this.ThirdName = "";
            this.LastName = "";
            this.DateOfBirth = DateTime.Now;
            this.Gendor = 0;
            this.Address = "";
            this.Phone = "";
            this.Email = "";
            this.NationalityCountryID = -1;
            this.ImagePath = "";

            Mode = enMode.AddNew;
        }

        private clsBLSPeople(int ID, string NationalNo, string FirstName, string SecondName, string ThirdName,
                            string LastName, DateTime DateOfBirth, byte Gendor, string Address, string Phone,
                            string Email, int NationalityCountryID, string ImagePath)
        {
            this.ID = ID;
            this.NationalNo = NationalNo;
            this.FirstName = FirstName;
            this.SecondName = SecondName;
            this.ThirdName = ThirdName;
            this.LastName = LastName;
            this.DateOfBirth = DateOfBirth;
            this.Gendor = Gendor;
            this.Address = Address;
            this.Phone = Phone;
            this.Email = Email;
            this.NationalityCountryID = NationalityCountryID;
            this.ImagePath = ImagePath;

            Mode = enMode.UpdateNew;
        }

        private bool _AddNew()
        {
            this.ID = clsDALPerson.AddNewPerson(this.NationalNo, this.FirstName, this.SecondName, this.ThirdName,
                this.LastName, this.DateOfBirth, this.Gendor, this.Address, this.Phone,
                this.Email, this.NationalityCountryID, this.ImagePath);

            return this.ID != -1;

        }

        private bool _Update()
        {
            return clsDALPerson.UpdatePerson(this.ID, this.NationalNo, this.FirstName, this.SecondName, this.ThirdName,
                this.LastName, this.DateOfBirth, this.Gendor, this.Address, this.Phone,
                this.Email, this.NationalityCountryID, this.ImagePath);
        }

        public static clsBLSPeople FindByID(int ID)
        {
            string NationalNo = "", FirstName = "", SecondName = "", ThirdName = "", LastName = "";
            string Address = "", Phone = "", Email = "", ImagePath = "";
            DateTime DateOfBirth = DateTime.Now;
            byte Gendor = 0;
            int NationalityCountryID = -1;

            if (clsDALPerson.GetPersonInfoByID(ID, ref NationalNo, ref FirstName, ref SecondName,
                ref ThirdName, ref LastName, ref DateOfBirth, ref Gendor, ref Address, ref Phone,
                ref Email, ref NationalityCountryID, ref ImagePath))
            {
                return new clsBLSPeople(ID, NationalNo, FirstName, SecondName, ThirdName, LastName,
                                        DateOfBirth, Gendor, Address, Phone, Email, NationalityCountryID, ImagePath);
            }
            else
            {
                return null;
            }
        }

        public static clsBLSPeople FindByNationalNo(string NationalNo)
        {
            string FirstName = "", SecondName = "", ThirdName = "", LastName = "";
            string Address = "", Phone = "", Email = "", ImagePath = "";
            DateTime DateOfBirth = DateTime.Now;
            byte Gendor = 0;
            int NationalityCountryID = -1;
            int ID = -1;
            if (clsDALPerson.GetPersonInfoByNationalNo(ref ID,NationalNo,  ref FirstName, ref SecondName,
                ref ThirdName, ref LastName, ref DateOfBirth, ref Gendor, ref Address, ref Phone,
                ref Email, ref NationalityCountryID, ref ImagePath))
            {
                return new clsBLSPeople(ID, NationalNo, FirstName, SecondName, ThirdName, LastName,
                                        DateOfBirth, Gendor, Address, Phone, Email, NationalityCountryID, ImagePath);
            }
            else
            {
                return null;
            }
        }

        public static DataTable GetAllPeople()
        {
            return clsDALPerson.GetAllPeople();
        }

        public static bool IsPersonLinked(int ID)
        {
            return clsDALPerson.IsPersonLinked(ID);
        }
        public static bool DeletePerson(int ID)
        {
            if (IsPersonLinked(ID))
            {
                return false;
            }
            return clsDALPerson.DeletePerson(ID);
        }

        public static bool IsPersonExistByID(int ID)
        {
            return clsDALPerson.IsPersonExistByID(ID);
        }

        public static bool IsPersonExistByNationalNo(string NationalNo)
        {
            return clsDALPerson.IsPersonExistsByNationalNo(NationalNo);
        }


        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (IsPersonExistByNationalNo(this.NationalNo))
                    {
                        return false;
                    }

                    if (_AddNew())
                    {
                        Mode = enMode.UpdateNew;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.UpdateNew:
                    return _Update();
            }
            return false;
        }
        public string FullName
        {
            get
            {
                return $"{FirstName} {SecondName} {ThirdName} {LastName}".Trim();
            }
        }
    }


}