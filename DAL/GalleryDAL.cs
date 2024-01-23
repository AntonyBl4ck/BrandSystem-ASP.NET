using OnlineBrandingSystem.Classes;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;

namespace OnlineBrandingSystem.DAL
{
    public class UniqueGalleryDAL
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["OnlineBrandingSystemConnection"].ConnectionString;

        #region Add Image
        public bool InsertImage(Gallery gallery)
        {
            bool isSuccessful = false;
            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                string query = "INSERT INTO ######### (brand_id, image_name, image_title) VALUES (@brand_id, @image_name, @image_title)";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@brand_id", gallery.brand_id);
                command.Parameters.AddWithValue("@image_name", gallery.image_name);
                command.Parameters.AddWithValue("@image_title", gallery.image_title);

                connection.Open();

                int rowsAffected = command.ExecuteNonQuery();

                isSuccessful = rowsAffected > 0;
            }
            catch (Exception ex)
            {
                
            }
            finally
            {
                connection.Close();
            }

            return isSuccessful;
        }
        #endregion

        #region Show all Images in Gallery
        public static List<Gallery> RetrieveImages(int brandId)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            List<Gallery> imageList = new List<Gallery>();

            try
            {
                string query = $"SELECT * FROM #### WHERE brand_id={brandId}";
                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Gallery gallery = new Gallery
                    {
                        image_id = Convert.ToInt32(reader[""]),
                        brand_id = Convert.ToInt32(reader[""]),
                        image_name = Convert.ToString(reader[""]),
                        image_title = Convert.ToString(reader[""])
                    };
                    imageList.Add(gallery);
                }
            }
            catch (Exception ex)
            {
                
            }
            finally
            {
                connection.Close();
            }

            return imageList;
        }
        #endregion
    }
}
