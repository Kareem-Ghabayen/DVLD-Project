using System;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    public class clsDALTest
    {
        public static bool GetTestInfoByTestID(int TestID, ref int TestAppointmentID,
            ref bool TestResult, ref string Notes, ref int CreatedByUserID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(Connection.ConnectionString);
            string query = "SELECT * FROM Tests WHERE TestID = @TestID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestID", TestID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                    TestAppointmentID = (int)reader["TestAppointmentID"];
                    TestResult = (bool)reader["TestResult"];

                    if (reader["Notes"] != DBNull.Value)
                        Notes = (string)reader["Notes"];
                    else
                        Notes = "";

                    CreatedByUserID = (int)reader["CreatedByUserID"];
                }
                reader.Close();
            }
            catch { }
            finally { connection.Close(); }

            return isFound;
        }

        public static bool GetTestInfoByTestAppointmentID(int TestAppointmentID, ref int TestID,
            ref bool TestResult, ref string Notes, ref int CreatedByUserID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(Connection.ConnectionString);
            string query = "SELECT * FROM Tests WHERE TestAppointmentID = @TestAppointmentID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                    TestID = (int)reader["TestID"];
                    TestResult = (bool)reader["TestResult"];

                    if (reader["Notes"] != DBNull.Value)
                        Notes = (string)reader["Notes"];
                    else
                        Notes = "";

                    CreatedByUserID = (int)reader["CreatedByUserID"];
                }
                reader.Close();
            }
            catch { }
            finally { connection.Close(); }

            return isFound;
        }

        public static DataTable GetAllTests()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(Connection.ConnectionString);
            string query = "SELECT * FROM Tests";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows) dt.Load(reader);
                reader.Close();
            }
            catch { }
            finally { connection.Close(); }

            return dt;
        }

        public static int AddNewTest(int TestAppointmentID, bool TestResult, string Notes, int CreatedByUserID)
        {
            int TestID = -1;
            SqlConnection connection = new SqlConnection(Connection.ConnectionString);

            string query = @"INSERT INTO Tests (TestAppointmentID, TestResult, Notes, CreatedByUserID)
                             VALUES (@TestAppointmentID, @TestResult, @Notes, @CreatedByUserID);
                             SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            command.Parameters.AddWithValue("@TestResult", TestResult);

            if (string.IsNullOrEmpty(Notes))
                command.Parameters.AddWithValue("@Notes", DBNull.Value);
            else
                command.Parameters.AddWithValue("@Notes", Notes);

            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    TestID = insertedID;
                }
            }
            catch { }
            finally { connection.Close(); }

            return TestID;
        }

        public static bool UpdateTest(int TestID, int TestAppointmentID, bool TestResult, string Notes, int CreatedByUserID)
        {
            bool isUpdated = false;
            SqlConnection connection = new SqlConnection(Connection.ConnectionString);

            string query = @"UPDATE Tests SET 
                                TestAppointmentID = @TestAppointmentID,
                                TestResult = @TestResult,
                                Notes = @Notes,
                                CreatedByUserID = @CreatedByUserID
                             WHERE TestID = @TestID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestID", TestID);
            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            command.Parameters.AddWithValue("@TestResult", TestResult);

            if (string.IsNullOrEmpty(Notes))
                command.Parameters.AddWithValue("@Notes", DBNull.Value);
            else
                command.Parameters.AddWithValue("@Notes", Notes);

            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {
                connection.Open();
                int rows = command.ExecuteNonQuery();
                if (rows > 0) isUpdated = true;
            }
            catch { }
            finally { connection.Close(); }

            return isUpdated;
        }
        public static byte GetPassedTestCount(int LocalDrivingLicenseApplicationID)
        {
            byte PassedTestCount = 0;
            SqlConnection connection = new SqlConnection(Connection.ConnectionString);

            string query = @"SELECT COUNT(TestID) 
                             FROM Tests INNER JOIN TestAppointments 
                             ON Tests.TestAppointmentID = TestAppointments.TestAppointmentID
                             WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID 
                               AND TestResult = 1";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null && byte.TryParse(result.ToString(), out byte count))
                {
                    PassedTestCount = count;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }

            return PassedTestCount;
        }

        public static bool DoesPassTestType(int localDrivingLicenseApplicationID, int testTypeID)
        {
            bool isPassed = false;
            SqlConnection connection = new SqlConnection(Connection.ConnectionString);

            string query = @"SELECT TOP 1 Found = 1 
                     FROM Tests 
                     INNER JOIN TestAppointments 
                     ON Tests.TestAppointmentID = TestAppointments.TestAppointmentID
                     WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID 
                       AND TestTypeID = @TestTypeID 
                       AND TestResult = 1";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", localDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@TestTypeID", testTypeID);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null)
                {
                    isPassed = true;
                }
            }
            catch (Exception ex)
            {
                // Log exception if needed
                isPassed = false;
            }
            finally
            {
                connection.Close();
            }

            return isPassed;
        }
        public static bool DoesFailTestType(int localDrivingLicenseApplicationID, int testTypeID)
        {
            bool isFailed = false;

            // استعلام مباشر بجدول المواعيد بدون الحاجة لـ JOINs زائدة مع جدول الأشخاص
            string query = @"SELECT TOP 1 Tests.TestResult 
                     FROM Tests 
                     INNER JOIN TestAppointments ON Tests.TestAppointmentID = TestAppointments.TestAppointmentID
                     WHERE TestAppointments.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID 
                       AND TestAppointments.TestTypeID = @TestTypeID
                     ORDER BY Tests.TestID DESC";

            using (SqlConnection connection = new SqlConnection(Connection.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", localDrivingLicenseApplicationID);
                    command.Parameters.AddWithValue("@TestTypeID", testTypeID);

                    try
                    {
                        connection.Open();
                        object result = command.ExecuteScalar();

                        if (result != null && bool.TryParse(result.ToString(), out bool testResult))
                        {
                            // لو النتيجة false (راسب) ترجع isFailed = true
                            isFailed = !testResult;
                        }
                    }
                    catch (Exception ex)
                    {
                        // Handling Exception
                    }
                }
            }

            return isFailed;
        }
    }
}