using PharmacyManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PharmacyManagementSystem.Gateway
{
    public class CategoryGateway: Gateway
    {
        public List<Category> GetAllCategory() 
        {
             
            List<Category> categoryList = new List<Category>();
            SqlCommand com = new SqlCommand("pha_GetAllCategory", Connection);
            com.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
           
            try
            {
                Connection.Open();
                da.Fill(dt);

            }
            finally
            {
                Connection.Close();
            }
            categoryList = (from DataRow dr in dt.Rows

                            select new Category()
                            {
                                CategoryId = Convert.ToInt32(dr["CategoryId"]),
                                CategoryName = Convert.ToString(dr["CategoryName"])

                            }).ToList();
            return categoryList;
        }
        public bool SaveCategory(Category category)
        {
            SqlCommand com = new SqlCommand("pha_SaveCategory", Connection);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@CategoryId", category.CategoryId);
            com.Parameters.AddWithValue("@CategoryName", category.CategoryName);
            com.Parameters.AddWithValue("@Description", category.Description);
            com.Parameters.AddWithValue("@IsActive", category.IsActive);

            int i;
            try
            {
                Connection.Open();
                i = com.ExecuteNonQuery();
            }
            finally
            {
                Connection.Close();
            }
            if (i >= 1)
            {

                return true;

            }
            else
            {

                return false;
            }


        }
    }
}