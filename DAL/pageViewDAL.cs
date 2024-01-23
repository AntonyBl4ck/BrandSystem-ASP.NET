using OnilneBrandingSystem.Classes;
using System;
using System.Configuration;
using System.Data.SqlClient;

namespace OnilneBrandingSystem.DAL
{
    public class PageViewDAL
    {
        private static string myConnStr = ConfigurationManager.ConnectionStrings["OnlineBrandingSystemConnection"].ConnectionString;

        #region Count Page Views (Insert Update Page Views)
        public bool CountPageViews(int brandId)
        {
            bool isSuccess = false;
            SqlConnection connection = new SqlConnection(myConnStr);

            try
            {
                string selectSql = $"SELECT COUNT (*) FROM ###### WHERE brand_id = {brandId}";
                SqlCommand selectCmd = new SqlCommand(selectSql, connection);
                connection.Open();
                int count = (int)selectCmd.ExecuteScalar();

                if (count == 0)
                {
                    string insertSql = $"INSERT INTO ###### (brand_id, hits) VALUES ({brandId}, 1)";
                    SqlCommand insertCmd = new SqlCommand(insertSql, connection);

                    int rows = insertCmd.ExecuteNonQuery();

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
                    string updateSql = $"UPDATE ##### SET hits = hits + 1 WHERE brand_id = {brandId}";
                    SqlCommand updateCmd = new SqlCommand(updateSql, connection);

                    int rows = updateCmd.ExecuteNonQuery();

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

        #region Get All Brands with Max Page Views
        public static List<PageViewClass> GetBrandsIdWithMaxHit()
        {
            SqlConnection connection = new SqlConnection(myConnStr);
            List<PageViewClass> pages = new List<PageViewClass>();

            try
            {
                string sql = "SELECT * FROM #### ORDER BY hits DESC";
                connection.Open();
                SqlCommand cmd = new SqlCommand(sql, connection);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    PageViewClass page = new PageViewClass
                    {
                        PageViewId = Convert.ToInt32(reader["page_view_id"]),
                        BrandId = Convert.ToInt32(reader["brand_id"]),
                        Hits = Convert.ToInt32(reader["hits"])
                    };
                    pages.Add(page);
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }

            return pages;
        }
        #endregion
    }
}
