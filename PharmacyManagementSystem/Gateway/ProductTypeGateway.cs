using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Data;
using PharmacyManagementSystem.Models;

namespace PharmacyManagementSystem.Gateway
{
    public class ProductTypeGateway : Gateway
    {

        public List<ProductType> GetAllProductType() 
        {

            List<ProductType> typeList = new List<ProductType>();
            SqlCommand com = new SqlCommand("pha_GetAllType", Connection);
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

            typeList = (from DataRow dr in dt.Rows

                        select new ProductType()
                        {
                            TypeId = Convert.ToInt32(dr["TypeId"]),
                            TypeName = Convert.ToString(dr["TypeName"])

                        }).ToList();


            return typeList;
        }
        public bool SaveProductType(ProductType type) 
        {
            SqlCommand com = new SqlCommand("pha_SaveProductType", Connection);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@TypeId", type.TypeId);
            com.Parameters.AddWithValue("@TypeName", type.TypeName);
            com.Parameters.AddWithValue("@Description", type.Description);
            com.Parameters.AddWithValue("@IsActive", type.IsActive);

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