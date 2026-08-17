using System;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    public class clsDALTestAppointment
    {
        public static bool GetTestAppointmentByID(int TestAppointmentID, ref int TestTypeID,
            ref int LocalDrivingLicenseApplicationID, ref DateTime AppointmentDate,
            ref decimal PaidFees, ref int CreatedByUserID, ref bool IsLocked, ref int RetestTestAppointmentID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(Connection.ConnectionString);
            string query = "SELECT * FROM TestAppointments WHERE TestAppointmentID = @TestAppointmentID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                    TestTypeID = (int)reader["TestTypeID"];
                    LocalDrivingLicenseApplicationID = (int)reader["LocalDrivingLicenseApplicationID"];
                    AppointmentDate = (DateTime)reader["AppointmentDate"];
                    PaidFees = (decimal)reader["PaidFees"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                    IsLocked = (bool)reader["IsLocked"];

                    if (reader["RetestTestAppointmentID"] != DBNull.Value)
                        RetestTestAppointmentID = (int)reader["RetestTestAppointmentID"];
                    else
                        RetestTestAppointmentID = -1;
                }
                reader.Close();
            }
            catch { }
            finally { connection.Close(); }

            return isFound;
        }

        public static DataTable GetApplicationTestAppointments(int LocalDrivingLicenseApplicationID)
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(Connection.ConnectionString);
            string query = "SELECT * FROM TestAppointments_View WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);

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

        public static DataTable GetAllTestAppointments()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(Connection.ConnectionString);
            string query = "SELECT * FROM TestAppointments_View";

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

        public static int AddNewTestAppointment(int TestTypeID, int LocalDrivingLicenseApplicationID,
            DateTime AppointmentDate, decimal PaidFees, int CreatedByUserID, int RetestTestAppointmentID)
        {
            int TestAppointmentID = -1;
            SqlConnection connection = new SqlConnection(Connection.ConnectionString);

            string query = @"INSERT INTO TestAppointments (TestTypeID, LocalDrivingLicenseApplicationID, AppointmentDate, PaidFees, CreatedByUserID, IsLocked, RetestTestAppointmentID)
                             VALUES (@TestTypeID, @LocalDrivingLicenseApplicationID, @AppointmentDate, @PaidFees, @CreatedByUserID, 0, @RetestTestAppointmentID);
                             SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            if (RetestTestAppointmentID == -1)
                command.Parameters.AddWithValue("@RetestTestAppointmentID", DBNull.Value);
            else
                command.Parameters.AddWithValue("@RetestTestAppointmentID", RetestTestAppointmentID);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                    TestAppointmentID = insertedID;
            }
            catch { }
            finally { connection.Close(); }

            return TestAppointmentID;
        }

=        public static bool UpdateTestAppointment(int TestAppointmentID, int TestTypeID,
            int LocalDrivingLicenseApplicationID, DateTime AppointmentDate, decimal PaidFees,
            int CreatedByUserID, bool IsLocked, int RetestTestAppointmentID)
        {
            bool isUpdated = false;
            SqlConnection connection = new SqlConnection(Connection.ConnectionString);

            string query = @"UPDATE TestAppointments SET 
                        TestTypeID = @TestTypeID,
                        LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID,
                        AppointmentDate = @AppointmentDate,
                        PaidFees = @PaidFees,
                        CreatedByUserID = @CreatedByUserID,
                        IsLocked = @IsLocked,
                        RetestTestAppointmentID = @RetestTestAppointmentID
                      WHERE TestAppointmentID = @TestAppointmentID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@IsLocked", IsLocked);

            if (RetestTestAppointmentID == -1)
                command.Parameters.AddWithValue("@RetestTestAppointmentID", DBNull.Value);
            else
                command.Parameters.AddWithValue("@RetestTestAppointmentID", RetestTestAppointmentID);

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

        public static bool DeleteTestAppointment(int TestAppointmentID)
        {
            bool isDeleted = false;
            SqlConnection connection = new SqlConnection(Connection.ConnectionString);
            string query = "DELETE FROM TestAppointments WHERE TestAppointmentID = @TestAppointmentID AND IsLocked = 0";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);

            try
            {
                connection.Open();
                int rows = command.ExecuteNonQuery();
                if (rows > 0) isDeleted = true;
            }
            catch { }
            finally { connection.Close(); }

            return isDeleted;
        }
    }
}