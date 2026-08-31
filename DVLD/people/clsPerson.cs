using BuisnessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD.people
{
    public class clsPerson
    {
        public static DataTable GetInitialData()
        {
            return clsBLSPeople.GetAllPeople();
        }

        public static string BuildFilterExpression(string selectedFilter, string filterValue)
        {
            if (string.IsNullOrWhiteSpace(filterValue) || selectedFilter == "None")
                return "";

            string columnName = "";
            switch (selectedFilter)
            {
                case "Person ID": columnName = "PersonID"; break;
                case "National No": columnName = "NationalNo"; break;
                case "First Name": columnName = "FirstName"; break;
                case "Second Name": columnName = "SecondName"; break;
                case "Third Name": columnName = "ThirdName"; break;
                case "Last Name": columnName = "LastName"; break;
                case "Nationality": columnName = "Nationality"; break;
                case "Phone": columnName = "Phone"; break;
                case "Email": columnName = "Email"; break;
                default: return "";
            }

            if (columnName == "PersonID")
            {
                if (int.TryParse(filterValue.Trim(), out int id))
                    return $"[PersonID] = {id}";
                else
                    return "[PersonID] = -1"; 
            }

            return $"[{columnName}] LIKE '{filterValue.Trim()}%'";
        }

    }
}
