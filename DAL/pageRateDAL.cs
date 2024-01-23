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
                string selectQuery = $"SELECT COUNT (*) FROM #### WHERE brand_id = {brandId}";
                SqlCommand selectCommand = new SqlCommand(selectQuery, connection);
                connection.Open();
                int count = (int)selectCommand.ExecuteScalar();

                if (count == 0)
                {
                    string insertQuery = $"INSERT INTO #### (brand_id, total_rating, no_of_rating) VALUES ({brandId}, {rating}, 1)";
                    SqlCommand insertCommand = new SqlCommand(insertQuery, connection);

                    int rows = insertCommand.ExecuteNonQuery();

                    if (rows > 0)
                    {
                        isSuccess = true;
                    }
                    else
                    {
                        isSuccess = false;
                    }
                }
                else
                {
                    string updateQuery = $"UPDATE tbl_Page_Rate SET total_rating = total_rating + {rating}, no_of_rating = no_of_rating + 1 WHERE brand_id = {brandId}";
                    SqlCommand updateCommand = new SqlCommand(updateQuery, connection);

                    int rows = updateCommand.ExecuteNonQuery();

                    if (rows > 0)
                    {
                        isSuccess = true;
                    }
                    else
                    {                    
                        isSuccess = false;
                    }
                }
            }
            catch (Exception ex)
            {
                
            }
            finally
            {
                connection.Close();
            }
            return isSuccess;
        }
        #endregion
    }
}
