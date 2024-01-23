using OnilneBrandingSystem.Classes;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace OnilneBrandingSystem.DAL
{
    public class UniqueBrandDAL
    {
        private static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["OnlineBrandingSystemConnection"].ConnectionString;

        #region Add Brand
        public bool AddBrand(BrandClass brand)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    string insertSql = "INSERT INTO tbl_Brands (brand_name, email, password, contact, description, location, keywords, category, addedDate, image) " +
                                       "VALUES (@brand_name, @email, @password, @contact, @description, @location, @keywords, @category, @addedDate, @image)";

                    using (SqlCommand insertCommand = new SqlCommand(insertSql, connection))
                    {
                        insertCommand.Parameters.AddWithValue("@brand_name", brand.BrandName);
                        insertCommand.Parameters.AddWithValue("@email", brand.Email);
                        insertCommand.Parameters.AddWithValue("@password", brand.Password);
                        insertCommand.Parameters.AddWithValue("@contact", brand.Contact);
                        insertCommand.Parameters.AddWithValue("@description", brand.Description);
                        insertCommand.Parameters.AddWithValue("@location", brand.Location);
                        insertCommand.Parameters.AddWithValue("@keywords", brand.Keywords);
                        insertCommand.Parameters.AddWithValue("@category", brand.Category);
                        insertCommand.Parameters.AddWithValue("@image", brand.Image);
                        insertCommand.Parameters.AddWithValue("@addedDate", brand.AddedDate);

                        connection.Open();

                        int rowsAffected = insertCommand.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error adding brand: {ex.Message}");
                    return false;
                }
            }
        }
        #endregion

        #region Get Brands By Keywords
        public static List<BrandClass> GetBrandsByKeywords(string keywords)
        {
            List<BrandClass> brands = new List<BrandClass>();

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    string selectSql = $"SELECT * FROM tbl_Brands WHERE brand_name LIKE '%{keywords}%' OR location LIKE '%{keywords}%'";

                    connection.Open();

                    using (SqlCommand selectCommand = new SqlCommand(selectSql, connection))
                    using (SqlDataReader reader = selectCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            BrandClass brand = new BrandClass
                            {
                                BrandId = Convert.ToInt32(reader["brand_id"]),
                                BrandName = Convert.ToString(reader["brand_name"]),
                                AddedDate = Convert.ToDateTime(reader["addedDate"]),
                                Image = Convert.ToString(reader["image"])
                            };
                            brands.Add(brand);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error getting brands by keywords: {ex.Message}");
                }
            }

            return brands;
        }

        private static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["OnlineBrandingSystemConnection"].ConnectionString;

        #region Get Latest Brand (By Added Date)
        public static List<BrandClass> GetNewBrands()
        {
            List<BrandClass> brands = new List<BrandClass>();

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    string sql = "SELECT * FROM tbl_Brands ORDER BY addedDate DESC";

                    connection.Open();

                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            BrandClass brand = ReadBrandFromReader(reader);
                            brands.Add(brand);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error getting new brands: {ex.Message}");
                }
            }

            return brands;
        }
        #endregion

        #region Get Brand Details By ID
        public static List<BrandClass> GetBrandDetails(int id)
        {
            List<BrandClass> brands = new List<BrandClass>();

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    string sql = $"SELECT * FROM tbl_Brands WHERE brand_id = {id}";

                    connection.Open();

                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            BrandClass brand = ReadBrandFromReader(reader);
                            brands.Add(brand);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error getting brand details by ID: {ex.Message}");
                }
            }

            return brands;
        }
        #endregion

        #region Get Brands by Category
        public static List<BrandClass> GetBrandsByCategory(string category)
        {
            List<BrandClass> brands = new List<BrandClass>();

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    string sql = $"SELECT * FROM tbl_Brands WHERE category = '{category}'";

                    connection.Open();

                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            BrandClass brand = ReadBrandFromReader(reader);
                            brands.Add(brand);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error getting brands by category: {ex.Message}");
                }
            }

            return brands;
        }
        #endregion

        #region Get Brand Details By Email
        public static List<BrandClass> GetBrandDetailsByEmail(string email)
        {
            List<BrandClass> brands = new List<BrandClass>();

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    string sql = $"SELECT * FROM tbl_Brands WHERE email = '{email}'";

                    connection.Open();

                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            BrandClass brand = ReadBrandFromReader(reader);
                            brands.Add(brand);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error getting brand details by email: {ex.Message}");
                }
            }

            return brands;
        }
        #endregion

        #region Get Brands with Maximum Hits
        public static List<BrandClass> GetBrandsWithMaxHits()
        {
            List<BrandClass> brands = new List<BrandClass>();
            List<PageViewClass> pages = PageViewDAL.GetBrandsIdWithMaxHit();

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    connection.Open();

                    foreach (PageViewClass page in pages)
                    {
                        int brandId = page.BrandId;
                        string sql = $"SELECT * FROM tbl_Brands WHERE brand_id = {brandId}";

                        using (SqlCommand cmd = new SqlCommand(sql, connection))
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                BrandClass brand = ReadBrandFromReader(reader);
                                brands.Add(brand);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error getting brands with maximum hits: {ex.Message}");
                }
            }

            return brands;
        }
        #endregion

        private static BrandClass ReadBrandFromReader(SqlDataReader reader)
        {
            return new BrandClass
            {
                BrandId = Convert.ToInt32(reader["brand_id"]),
                BrandName = Convert.ToString(reader["brand_name"]),
                Email = Convert.ToString(reader["email"]),
                Password = Convert.ToString(reader["password"]),
                Contact = Convert.ToString(reader["contact"]),
                Description = Convert.ToString(reader["description"]),
                Location = Convert.ToString(reader["location"]),
                Keywords = Convert.ToString(reader["keywords"]),
                Category = Convert.ToString(reader["category"]),
                Image = Convert.ToString(reader["image"]),
                AddedDate = Convert.ToDateTime(reader["addedDate"])
            };
        }
    }
}