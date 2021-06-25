using PharmacyManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PharmacyManagementSystem.Gateway
{
    public class BrandGateway: Gateway
    {
        public List<Brand> GetAllBrand()
        {

            List<Brand> brandList = new List<Brand>();
            SqlCommand com = new SqlCommand("pha_GetAllBrand", Connection);
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

            brandList = (from DataRow dr in dt.Rows

                        select new Brand()
                        {
                            BrandId = Convert.ToInt32(dr["BrandId"]),
                            BrandName = Convert.ToString(dr["BrandName"])

                        }).ToList();


            return brandList;
        }
        public bool SaveBrand(Brand brand)
        {
            SqlCommand com = new SqlCommand("pha_SaveBrand", Connection);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@BrandId", brand.BrandId);
            com.Parameters.AddWithValue("@BrandName", brand.BrandName);
            com.Parameters.AddWithValue("@Description", brand.Description);
            com.Parameters.AddWithValue("@IsActive", brand.IsActive);

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