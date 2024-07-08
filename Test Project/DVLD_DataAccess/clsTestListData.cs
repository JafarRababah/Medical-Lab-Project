using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DVLD_DataAccess.clsCountryData;
using System.Net;
using System.Security.Policy;

namespace DVLD_DataAccess
{
    public class clsTestListData
    {

        public static bool GetTestListInfoByID(int TestListID,
            ref string TestListTitle, ref string TestDescription, ref float TestFees)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM TestList WHERE TestListID = @TestListID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TestListID", TestListID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {

                    // The record was found
                    isFound = true;

                    TestListTitle = (string)reader["TestListTitle"];
                    TestDescription = (string)reader["TestListDescription"];
                    TestFees = Convert.ToSingle(reader["TestListFees"]);

                }
                else
                {
                    // The record was not found
                    isFound = false;
                }

                reader.Close();


            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

        public static DataTable GetAllTestLists()
        {

            DataTable dt = new DataTable();
            dt.Clear();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM TestList order by TestListID";

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
                // Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return dt;

        }

        public static int AddNewTestList(string Title, string Description, float Fees)
        {
            int TestListID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Insert Into TestList (TestListTitle,TestListDescription,TestListFees)
                            Values (@TestListTitle,@TestListDescription,@ApplicationFees)
                            where TestListID = @TestListID;
                            SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TestListTitle", Title);
            command.Parameters.AddWithValue("@TestListDescription", Description);
            command.Parameters.AddWithValue("@ApplicationFees", Fees);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    TestListID = insertedID;
                }
            }

            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);

            }

            finally
            {
                connection.Close();
            }


            return TestListID;

        }

        public static bool UpdateTestList(int TestListID, string Title, string Description, float Fees)
        {

            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Update  TestLists  
                            set TestListTitle = @TestListTitle,
                                TestListDescription=@TestListDescription,
                                TestListFees = @TestListFees
                                where TestListID = @TestListID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TestListID", TestListID);
            command.Parameters.AddWithValue("@TestListTitle", Title);
            command.Parameters.AddWithValue("@TestListDescription", Description);
            command.Parameters.AddWithValue("@TestListFees", Fees);

            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                return false;
            }

            finally
            {
                connection.Close();
            }

            return (rowsAffected > 0);
        }



    }
}
