using System;
using System.Configuration;
using System.Data.SqlClient;

namespace OnlineBrandingSystem.DAL
{
    public class PageRateDAL
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["OnlineBrandingSystemConnection"].ConnectionString;

        #region Count Page Rating (Insert Update Page Rating)
        public bool CountPageRate(int brandId, int rating)
        {
            bool isSuccess = false;
            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                // Writing T-SQL to Check if the page already exists or not
                string selectQuery = $"SELECT COUNT (*) FROM tbl_Page_Rate WHERE brand_id = {brandId}";
                SqlCommand selectCommand = new SqlCommand(selectQuery, connection);
                connection.Open();
                int count = (int)selectCommand.ExecuteScalar();

                if (count == 0)
                {
                    // Brand Does Not Exist (INSERT)
                    string insertQuery = $"INSERT INTO tbl_Page_Rate (brand_id, total_rating, no_of_rating) VALUES ({brandId}, {rating}, 1)";
                    SqlCommand insertCommand = new SqlCommand(insertQuery, connection);

                    // Execute Query
                    int rows = insertCommand.ExecuteNonQuery();

                    if (rows > 0)
                    {
                        // Successfully Inserted
                        isSuccess = true;
                    }
                    else
                    {
                        // Failed to Insert
                        isSuccess = false;
                    }
                }
                else
                {
                    // Brand Exists (Update)
                    string updateQuery = $"UPDATE tbl_Page_Rate SET total_rating = total_rating + {rating}, no_of_rating = no_of_rating + 1 WHERE brand_id = {brandId}";
                    SqlCommand updateCommand = new SqlCommand(updateQuery, connection);

                    // Execute Query
                    int rows = updateCommand.ExecuteNonQuery();

                    if (rows > 0)
                    {
                        // Successfully Updated
                        isSuccess = true;
                    }
                    else
                    {
                        // Failed to Update
                        isSuccess = false;
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle the exception, log or throw if necessary
            }
            finally
            {
                // Close Connection
                connection.Close();
            }
            return isSuccess;
        }
        #endregion
    }
}
