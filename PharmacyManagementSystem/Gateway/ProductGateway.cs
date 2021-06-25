using PharmacyManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PharmacyManagementSystem.Gateway
{
    public class ProductGateway : Gateway
    {
        public List<Product> GetDetails(string startDate, string endDate)
        {
            List<Product> productList = new List<Product>();
            SqlCommand com = new SqlCommand("pha_SearchProductDetails", Connection);
            com.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(com);
            com.Parameters.AddWithValue("@StartDate", startDate);
            com.Parameters.AddWithValue("@EndDate", endDate);
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
            productList = (from DataRow dr in dt.Rows

                        select new Product()
                        {
                            ProductId = Convert.ToInt32(dr["ProductId"]),
                            ProductName = Convert.ToString(dr["ProductName"]),
                            BrandId = Convert.ToInt32(dr["BrandId"]),
                            BrandName = Convert.ToString(dr["BrandName"]),
                            GroupId = Convert.ToInt32(dr["GroupId"]),
                            GroupName = Convert.ToString(dr["GroupName"]),
                            CategoryId = Convert.ToInt32(dr["CategoryId"]),
                            CategoryName = Convert.ToString(dr["CategoryName"]),
                            TypeId = Convert.ToInt32(dr["TypeId"]),
                            TypeName = Convert.ToString(dr["TypeName"]),
                            AddedDate = Convert.ToDateTime(dr["AddedDate"]),
                            IsActive = Convert.ToBoolean(dr["IsActive"])


                        }).ToList();


            return productList;
        }
        public List<Product> GetAllProduct()
        {

            List<Product> productList = new List<Product>();
            SqlCommand com = new SqlCommand("pha_GetAllProduct", Connection);
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

            productList = (from DataRow dr in dt.Rows

                           select new Product()
                           {
                               ProductId = Convert.ToInt32(dr["ProductId"]),
                               ProductName = Convert.ToString(dr["ProductName"]),
                               BrandId = Convert.ToInt32(dr["BrandId"]),
                               BrandName = Convert.ToString(dr["BrandName"]),
                               GroupId = Convert.ToInt32(dr["GroupId"]),
                               GroupName = Convert.ToString(dr["GroupName"]),
                               CategoryId = Convert.ToInt32(dr["CategoryId"]),
                               CategoryName = Convert.ToString(dr["CategoryName"]),
                               TypeId = Convert.ToInt32(dr["TypeId"]),
                               TypeName = Convert.ToString(dr["TypeName"]),
                               AddedDate = Convert.ToDateTime(dr["AddedDate"]),
                               IsActive = Convert.ToBoolean(dr["IsActive"])

                           }).ToList();


            return productList;
        }
        public bool SaveProduct(Product product)
        {
            SqlCommand com = new SqlCommand("pha_SaveProduct", Connection);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@ProductId", product.ProductId);
            com.Parameters.AddWithValue("@GroupId", product.GroupId);
            com.Parameters.AddWithValue("@BrandId", product.BrandId);
            com.Parameters.AddWithValue("@CategoryId", product.CategoryId);
            com.Parameters.AddWithValue("@TypeId", product.TypeId);
            com.Parameters.AddWithValue("@ProductName", product.ProductName);
            com.Parameters.AddWithValue("@AddedDate", product.AddedDate);
            com.Parameters.AddWithValue("@IsActive", product.IsActive);

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