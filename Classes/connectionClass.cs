using System;
using System.Configuration;
using System.Data.SqlClient;

namespace OnlineBrandingSystem.Classes
{
    public class ConnectionClass
    {
        private static SqlConnection conn;
        private static SqlCommand command;

        static ConnectionClass()
        {
            // Connecting to Database
            string connectionString = ConfigurationManager.ConnectionStrings["OnlineBrandingSystemConnection"].ToString();
            conn = new SqlConnection(connectionString);
            command = new SqlCommand("", conn);
        }

        public static void AddBrand(BrandClass brand)
        {
            try
            {
                conn.Open();


                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding brand: {ex.Message}");
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
