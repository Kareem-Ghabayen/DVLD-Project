using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLayer
{
    public class clsBLCountry
    {
        public int CountryID { get; set; }
        public string CountryName { get; set; }

        private clsBLCountry(int CountryID, string CountryName)
        {
            this.CountryID = CountryID;
            this.CountryName = CountryName;
        }

        public static clsBLCountry FindByCountryID(int countryID)
        {
            string CountryName = "";

            if (clsDALCountry.GetCountryInfoByID(countryID, ref CountryName))
                return new clsBLCountry(countryID, CountryName);

            else
                return null;
        }

        public static clsBLCountry FindByCountryName(string CountryName)
        {
            int CountryID = 0;

            if (clsDALCountry.GetCountryInfoByName(CountryName, ref CountryID))
                return new clsBLCountry(CountryID, CountryName);

            else
                return null;
        }

        public static DataTable GetAllCountries()
        {
            return clsDALCountry.GetAllCountries();
        }

    }
}