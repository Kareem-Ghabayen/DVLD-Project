using DataAccessLayer;
using System;
using System.Data;
using System.Data.SqlClient;

public class clsDALLocalDrivingLicenseApplication
{
    public static DataTable GetAllLocalDrivingLicenseApplications()
    {
        DataTable dt = new DataTable();
        SqlConnection connection = new SqlConnection(Connection.ConnectionString);
        string query = "SELECT * FROM LocalDrivingLicenseApplications_View";
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

    public static bool GetInfoByLocalDrivingLicenseApplicationID(int LocalDrivingLicenseApplicationID, ref int ApplicationID, ref int LicenseClassID)
    {
        bool isFound = false;
        SqlConnection connection = new SqlConnection(Connection.ConnectionString);
        string query = "SELECT * FROM LocalDrivingLicenseApplications WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID";
        SqlCommand command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);

        try
        {
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                isFound = true;
                ApplicationID = (int)reader["ApplicationID"];
                LicenseClassID = (int)reader["LicenseClassID"];
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

    public static int AddNewLocalDrivingLicenseApplication(int ApplicationID, int LicenseClassID)
    {
        int LocalDrivingLicenseApplicationID = -1;
        SqlConnection connection = new SqlConnection(Connection.ConnectionString);
        string query = @"INSERT INTO LocalDrivingLicenseApplications (ApplicationID, LicenseClassID)
                         VALUES (@ApplicationID, @LicenseClassID);
                         SELECT SCOPE_IDENTITY();";

        SqlCommand command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
        command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

        try
        {
            connection.Open();
            object result = command.ExecuteScalar();
            if (result != null && int.TryParse(result.ToString(), out int insertedID))
            {
                LocalDrivingLicenseApplicationID = insertedID;
            }
        }
        catch (Exception ex)
        {
        }
        finally
        {
            connection.Close();
        }

        return LocalDrivingLicenseApplicationID;
    }

    public static bool UpdateLocalDrivingLicenseApplication(int LocalDrivingLicenseApplicationID, int ApplicationID, int LicenseClassID)
    {
        bool isUpdated = false;
        SqlConnection connection = new SqlConnection(Connection.ConnectionString);
        string query = @"UPDATE LocalDrivingLicenseApplications 
                         SET ApplicationID = @ApplicationID, 
                             LicenseClassID = @LicenseClassID
                         WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID";

        SqlCommand command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
        command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
        command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

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

    public static bool DeleteLocalDrivingLicenseApplication(int LocalDrivingLicenseApplicationID)
    {
        bool isDeleted = false;
        SqlConnection connection = new SqlConnection(Connection.ConnectionString);
        string query = "DELETE FROM LocalDrivingLicenseApplications WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID";

        SqlCommand command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);

        try
        {
            connection.Open();
            int rowsAffected = command.ExecuteNonQuery();
            if (rowsAffected > 0)
            {
                isDeleted = true;
            }
        }
        catch (Exception ex)
        {
            isDeleted = false;
        }
        finally
        {
            connection.Close();
        }

        return isDeleted;
    }
    public static bool GetFullInfoByApplicationID(int ApplicationID, ref int ApplicantPersonID, ref DateTime ApplicationDate,
        ref int ApplicationTypeID, ref byte ApplicationStatus, ref DateTime LastStatusDate, ref decimal PaidFees,
        ref int CreatedByUserID, ref int LocalDrivingLicenseApplicationID, ref int LicenseClassID)
    {
        bool isFound = false;
        SqlConnection connection = new SqlConnection(Connection.ConnectionString);

        string query = "SELECT * FROM LocalDrivingLicenseFullApplications_View WHERE ApplicationID = @ApplicationID";

        SqlCommand command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

        try
        {
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                isFound = true;

                ApplicantPersonID = (int)reader["ApplicantPersonID"];
                ApplicationDate = (DateTime)reader["ApplicationDate"];
                ApplicationTypeID = (int)reader["ApplicationTypeID"];
                ApplicationStatus = (byte)reader["ApplicationStatus"];
                LastStatusDate = (DateTime)reader["LastStatusDate"];
                PaidFees = (decimal)reader["PaidFees"];
                CreatedByUserID = (int)reader["CreatedByUserID"];
                LocalDrivingLicenseApplicationID = (int)reader["LocalDrivingLicenseApplicationID"];
                LicenseClassID = (int)reader["LicenseClassID"];
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
    public static bool GetInfoByApplicationID(int ApplicationID, ref int LocalDrivingLicenseApplicationID, ref int LicenseClassID)
    {
        bool isFound = false;

        SqlConnection connection = new SqlConnection(Connection.ConnectionString);

        string query = "SELECT * FROM LocalDrivingLicenseApplications WHERE ApplicationID = @ApplicationID;";

        SqlCommand command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

        try
        {
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                isFound = true;

                LocalDrivingLicenseApplicationID = (int)reader["LocalDrivingLicenseApplicationID"];
                LicenseClassID = (int)reader["LicenseClassID"];
            }

            reader.Close();
        }
        catch (Exception ex)
        {
            // isFound = false;
        }
        finally
        {
            connection.Close();
        }

        return isFound;
    }
    //public static bool IsLicenseApplicationForPersonIDExist(int PersonID, int LicenseClassID)
    //{
    //    bool isExist = false;
    //    SqlConnection connection = new SqlConnection(Connection.ConnectionString);

    //    string query = @"SELECT Found = 1 
    //                 FROM LocalDrivingLicenseApplications_View 
    //                 WHERE ApplicantPersonID = @PersonID 
    //                   AND LicenseClassID = @LicenseClassID 
    //                   AND ApplicationStatus = 1"; 

    //    SqlCommand command = new SqlCommand(query, connection);
    //    command.Parameters.AddWithValue("@PersonID", PersonID);
    //    command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

    //    try
    //    {
    //        connection.Open();
    //        object result = command.ExecuteScalar();
    //        if (result != null)
    //        {
    //            isExist = true;
    //        }
    //    }
    //    catch { }
    //    finally { connection.Close(); }

    //    return isExist;
    //}
    public static byte GetTotalTrialsPerTest(int LocalDrivingLicenseApplicationID, int TestTypeID)
    {
        byte TotalTrials = 0;

        using (SqlConnection connection = new SqlConnection(Connection.ConnectionString))
        {
            string query = @"SELECT TotalTrialsPerTest = COUNT(TestAppointments.TestAppointmentID)
                        FROM TestAppointments INNER JOIN
                             Tests ON TestAppointments.TestAppointmentID = Tests.TestAppointmentID
                        WHERE (TestAppointments.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID) 
                          AND (TestAppointments.TestTypeID = @TestTypeID)";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
                command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

                try
                {
                    connection.Open();

                    object result = command.ExecuteScalar();

                    if (result != null && byte.TryParse(result.ToString(), out byte value))
                    {
                        TotalTrials = value;
                    }
                }
                catch (Exception ex)
                {
                    // يمكن إضافة تسجيل للخطأ هنا عند الحاجة
                }
            }
        }

        return TotalTrials;
    }
}