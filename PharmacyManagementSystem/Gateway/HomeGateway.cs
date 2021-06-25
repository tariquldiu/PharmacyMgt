using PharmacyManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PharmacyManagementSystem.Gateway
{
    public class HomeGateway : Gateway
    {

        public List<Home> GetDetails(string startDate, string endDate)
        {
            List<Home> homeList = new List<Home>();
            SqlCommand com = new SqlCommand("pha_GetHomeDetails", Connection);
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
            homeList = (from DataRow dr in dt.Rows

                        select new Home()
                        {
                            MedicineCost = Convert.ToDecimal(dr["MedicineCost"]),
                            OthersCost = Convert.ToDecimal(dr["OthersCost"]),
                            TotalSale = Convert.ToDecimal(dr["TotalSale"]),
                            TotalProfit = Convert.ToDecimal(dr["TotalProfit"])


                        }).ToList();


            return homeList;
        }
    }
}