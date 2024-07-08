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
using static System.Net.Mime.MediaTypeNames;
using System.Reflection;

namespace DVLD_DataAccess
{
    public class clsPatientTestData
    {

        public static bool GetTestInfoByID(int TestID,

            ref int TestListID, ref int PatientID, ref string Result)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM PatientTests WHERE TestID = @TestID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TestID", TestID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {

                    // The record was found
                    isFound = true;

                    TestID = (int)reader["TestID"];
                    TestListID = (int)reader["TestListID"];


                    Result = (string)reader["Result"];
                    PatientID = (int)reader["PatientID"];
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





        public static DataTable GetAllTests(int LocalDrivingLicenseApplicationID)
        {

            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

           
            string query = @"select DISTINCT * from PatientTestView where (LocalDrivingLicenseApplicationID=@LocalDrivingLicenseApplicationID)";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
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
                 Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return dt;

        }

        public static bool AddNewTest(int ID,int TestListID,
             int PatientID)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Insert Into PatientTests (ID,TestListID, PatientID)
                            Values (@ID,@TestListID,@PatientID)";                         
                                

            SqlCommand command = new SqlCommand(query, connection);
            
                command.Parameters.AddWithValue("@ID", ID);
                command.Parameters.AddWithValue("@TestListID", TestListID);


                command.Parameters.AddWithValue("@PatientID", PatientID);
            

            try
            {
                connection.Open();

                rowsAffected = command.ExecuteNonQuery();
            }

            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }

            finally
            {
                connection.Close();
            }


            return (rowsAffected > 0); 

        }

        public static bool UpdateTest(int ID, int TestListID,
             int PatientID, string Result)
        {
            
            int rowsAffected = 0;
            
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Update  PatientTests  
                            set 
                                Result=@Result
                                
                                where ID = @ID and TestListID=@TestListID";

            SqlCommand command = new SqlCommand(query, connection);
            
                command.Parameters.AddWithValue("@ID", ID);
                command.Parameters.AddWithValue("@TestListID", TestListID);
                command.Parameters.AddWithValue("@Result", Result);
              //  command.Parameters.AddWithValue("@PatientID", PatientID);
            
            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }

            finally
            {
                connection.Close();
            }

            return (rowsAffected > 0);
        }

        public static byte GetPassedTestCount(int SelectedTest)
        {
            byte PassedTestCount = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT PassedTestCount = count(TestID)
                         FROM PatientTests";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@SelectedTest", SelectedTest);


            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && byte.TryParse(result.ToString(), out byte ptCount))
                {
                    PassedTestCount = ptCount;
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);

            }

            finally
            {
                connection.Close();
            }

            return PassedTestCount;



        }

    }
}
