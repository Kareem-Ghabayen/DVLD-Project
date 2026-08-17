using System;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    public class clsDALTestTypes
    {
        public static DataTable GetAllTestTypes()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(Connection.ConnectionString);
            string query = "SELECT * FROM TestTypes";

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

        public static bool UpdateTestTypePrice(int TestTypeID, float NewPrice)
        {
            bool isUpdated = false;
            SqlConnection connection = new SqlConnection(Connection.ConnectionString);
            string query = @"UPDATE TestTypes SET TestTypeFees = @NewPrice WHERE TestTypeID = @TestTypeID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            command.Parameters.AddWithValue("@NewPrice", NewPrice);

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

        public static bool GetTestTypeByID(int TestTypeID, ref string Title, ref string Description, ref float Fees)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(Connection.ConnectionString);
            string query = "SELECT * FROM TestTypes WHERE TestTypeID = @TestTypeID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                    Title = (string)reader["TestTypeTitle"];
                    Description = (string)reader["TestTypeDescription"];
                    Fees = Convert.ToSingle(reader["TestTypeFees"]);
                }
                reader.Close();
            }
            catch { }
            finally { connection.Close(); }

            return isFound;
        }
    }
}