using DataAccessLayer;
using System;
using System.Data;
using System.Data.SqlClient;

namespace DVLD_DataAccess
{
    public class clsDALLicenses
    {

        public static bool IsLicenseExist(int licenseID)
        {
            bool isFound = false;

            // افترض أن عندك كلاس الاتصال مع قاعدة البيانات ConnectionString
            using (SqlConnection connection = new SqlConnection(Connection.ConnectionString))
            {
                string query = "SELECT Found=1 FROM Licenses WHERE LicenseID = @LicenseID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@LicenseID", licenseID);

                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            isFound = reader.HasRows;
                        }
                    }
                    catch (Exception ex)
                    {
                        // Log exception if needed
                        isFound = false;
                    }
                }
            }

            return isFound;
        }
        //  هان عشان تفحص ادا الشخص معاه رخصة من نوع نمعين ولا لا 
        public static bool IsLicenseExistByPersonIDAndLicenseClass(int PersonID, int LicenseClass)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(Connection.ConnectionString);

            string query = @"SELECT TOP 1 1 
                     FROM Licenses 
                     INNER JOIN Drivers ON Licenses.DriverID = Drivers.DriverID
                     WHERE Drivers.PersonID = @PersonID 
                       AND Licenses.LicenseClass = @LicenseClass";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@LicenseClass", LicenseClass);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null)
                {
                    isFound = true;
                }
            }
            catch (Exception ex)
            {
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }
        public static bool GetLicenseInfoByLicenseID(int LicenseID, ref int ApplicationID, ref int DriverID, ref int LicenseClass, ref DateTime IssueDate, ref DateTime ExpirationDate, ref string Notes, ref decimal PaidFees, ref bool IsActive, ref byte IssueReason, ref int CreatedByUserID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(Connection.ConnectionString);

            string query = "SELECT * FROM Licenses WHERE LicenseID = @LicenseID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LicenseID", LicenseID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;

                    ApplicationID = (int)reader["ApplicationID"];
                    DriverID = (int)reader["DriverID"];
                    LicenseClass = (int)reader["LicenseClass"];
                    IssueDate = (DateTime)reader["IssueDate"];
                    ExpirationDate = (DateTime)reader["ExpirationDate"];

                    if (reader["Notes"] != DBNull.Value)
                        Notes = (string)reader["Notes"];
                    else
                        Notes = "";

                    PaidFees = Convert.ToDecimal(reader["PaidFees"]);
                    IsActive = (bool)reader["IsActive"];
                    IssueReason = (byte)reader["IssueReason"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                }
                else
                {
                    isFound = false;
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

        public static DataTable GetAllLicenses()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(Connection.ConnectionString);

            string query = @"SELECT 
                                Licenses.LicenseID,
                                Licenses.ApplicationID,
                                Drivers.PersonID,
                                People.FirstName + ' ' + People.SecondName + ' ' + People.ThirdName + ' ' + People.LastName AS FullName,
                                LicenseClasses.ClassName,
                                Licenses.IssueDate,
                                Licenses.ExpirationDate,
                                Licenses.IsActive
                             FROM Licenses
                             INNER JOIN Drivers ON Licenses.DriverID = Drivers.DriverID
                             INNER JOIN People ON Drivers.PersonID = People.PersonID
                             INNER JOIN LicenseClasses ON Licenses.LicenseClass = LicenseClasses.LicenseClassID
                             ORDER BY Licenses.LicenseID DESC";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    dt.Load(reader);
                }

                reader.Close();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                connection.Close();
            }

            return dt;
        }
        public static DataTable GetDriverLicenses(int DriverID)
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(Connection.ConnectionString);

            string query = "SELECT * FROM Licenses WHERE DriverID = @DriverID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@DriverID", DriverID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    dt.Load(reader);
                }

                reader.Close();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                connection.Close();
            }

            return dt;
        }

        public static int AddNewLicense(int ApplicationID, int DriverID, int LicenseClass, DateTime IssueDate, DateTime ExpirationDate, string Notes, decimal PaidFees, bool IsActive, byte IssueReason, int CreatedByUserID)
        {
            int licenseID = -1;

            SqlConnection connection = new SqlConnection(Connection.ConnectionString);

            string query = @"INSERT INTO Licenses 
                             (ApplicationID, DriverID, LicenseClass, IssueDate, ExpirationDate, Notes, PaidFees, IsActive, IssueReason, CreatedByUserID)
                             VALUES 
                             (@ApplicationID, @DriverID, @LicenseClass, @IssueDate, @ExpirationDate, @Notes, @PaidFees, @IsActive, @IssueReason, @CreatedByUserID);
                             SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@DriverID", DriverID);
            command.Parameters.AddWithValue("@LicenseClass", LicenseClass);
            command.Parameters.AddWithValue("@IssueDate", IssueDate);
            command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);

            if (Notes != "" && Notes != null)
                command.Parameters.AddWithValue("@Notes", Notes);
            else
                command.Parameters.AddWithValue("@Notes", DBNull.Value);

            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@IsActive", IsActive);
            command.Parameters.AddWithValue("@IssueReason", IssueReason);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    licenseID = insertedID;
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                connection.Close();
            }

            return licenseID;
        }

        public static bool UpdateLicense(int LicenseID, int ApplicationID, int DriverID, int LicenseClass, DateTime IssueDate, DateTime ExpirationDate, string Notes, decimal PaidFees, bool IsActive, byte IssueReason, int CreatedByUserID)
        {
            bool isUpdated = false;

            SqlConnection connection = new SqlConnection(Connection.ConnectionString);

            string query = @"UPDATE Licenses 
                             SET ApplicationID = @ApplicationID,
                                 DriverID = @DriverID,
                                 LicenseClass = @LicenseClass,
                                 IssueDate = @IssueDate,
                                 ExpirationDate = @ExpirationDate,
                                 Notes = @Notes,
                                 PaidFees = @PaidFees,
                                 IsActive = @IsActive,
                                 IssueReason = @IssueReason,
                                 CreatedByUserID = @CreatedByUserID
                             WHERE LicenseID = @LicenseID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LicenseID", LicenseID);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@DriverID", DriverID);
            command.Parameters.AddWithValue("@LicenseClass", LicenseClass);
            command.Parameters.AddWithValue("@IssueDate", IssueDate);
            command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);

            if (Notes != "" && Notes != null)
                command.Parameters.AddWithValue("@Notes", Notes);
            else
                command.Parameters.AddWithValue("@Notes", DBNull.Value);

            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@IsActive", IsActive);
            command.Parameters.AddWithValue("@IssueReason", IssueReason);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    isUpdated = true;
                }
            }
            catch (Exception ex)
            {
                isUpdated = false;
            }
            finally
            {
                connection.Close();
            }

            return isUpdated;
        }

        public static int GetActiveLicenseIDByPersonID(int PersonID, int LicenseClass)
        {
            int licenseID = -1;

            SqlConnection connection = new SqlConnection(Connection.ConnectionString);

            string query = @"SELECT Licenses.LicenseID 
                             FROM Licenses 
                             INNER JOIN Drivers ON Licenses.DriverID = Drivers.DriverID
                             WHERE Drivers.PersonID = @PersonID 
                               AND Licenses.LicenseClass = @LicenseClass 
                               AND Licenses.IsActive = 1";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@LicenseClass", LicenseClass);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    licenseID = insertedID;
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                connection.Close();
            }

            return licenseID;
        }
        public static bool IsLicenseActiveByLicenseID(int LicenseID)
        {
            bool isActive = false;

            SqlConnection connection = new SqlConnection(Connection.ConnectionString);

            string query = "SELECT IsActive FROM Licenses WHERE LicenseID = @LicenseID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LicenseID", LicenseID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && bool.TryParse(result.ToString(), out bool activeResult))
                {
                    isActive = activeResult;
                }
            }
            catch (Exception ex)
            {
                isActive = false;
            }
            finally
            {
                connection.Close();
            }

            return isActive;
        }

        public static bool GetDriverInfoByDriverID(int DriverID, ref int PersonID, ref int CreatedByUserID, ref DateTime CreatedDate, ref string FullName, ref string NationalNo)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(Connection.ConnectionString);

            string query = @"SELECT 
                                Drivers.DriverID,
                                Drivers.PersonID,
                                Drivers.CreatedByUserID,
                                Drivers.CreatedDate,
                                People.FirstName + ' ' + People.SecondName + ' ' + People.ThirdName + ' ' + People.LastName AS FullName,
                                People.NationalNo
                             FROM Drivers
                             INNER JOIN People ON Drivers.PersonID = People.PersonID
                             WHERE Drivers.DriverID = @DriverID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@DriverID", DriverID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;

                    PersonID = (int)reader["PersonID"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                    CreatedDate = (DateTime)reader["CreatedDate"];
                    FullName = reader["FullName"].ToString();
                    NationalNo = reader["NationalNo"].ToString();
                }
                else
                {
                    isFound = false;
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }
    }
}