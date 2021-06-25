using PharmacyManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PharmacyManagementSystem.Gateway
{
    public class CostGateway: Gateway
    {
        public List<Cost> GetDetails(string startDate, string endDate)
        {
            List<Cost> costList = new List<Cost>();
            SqlCommand com = new SqlCommand("pha_SearchCostDetails", Connection);
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
            costList = (from DataRow dr in dt.Rows

                        select new Cost()
                        {
                            CostId = Convert.ToInt32(dr["CostId"]),
                            CostType = Convert.ToString(dr["CostType"]),
                            PurchaseId = Convert.ToInt32(dr["PurchaseId"]),
                            CostDescription = Convert.ToString(dr["CostDescription"]),
                            CostDate = Convert.ToDateTime(dr["CostDate"]),
                            Amount = Convert.ToDecimal(dr["Amount"])

                        }).ToList();


            return costList;
        }
        public List<Cost> GetAllCost() 
        {

            List<Cost> costList = new List<Cost>();
            SqlCommand com = new SqlCommand("pha_GetAllCost", Connection);
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

            costList = (from DataRow dr in dt.Rows

                             select new Cost()
                             {
                                 CostId = Convert.ToInt32(dr["CostId"]),
                                 CostType = Convert.ToString(dr["CostType"]),
                                 PurchaseId = Convert.ToInt32(dr["PurchaseId"]),
                                 CostDescription = Convert.ToString(dr["CostDescription"]),
                                 CostDate = Convert.ToDateTime(dr["CostDate"]),
                                 Amount = Convert.ToDecimal(dr["Amount"])

                             }).ToList();


            return costList;
        }
        public bool SaveCost(Cost cost) 
        { 
            SqlCommand com = new SqlCommand("pha_SaveCost", Connection);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@CostId", cost.CostId);
            com.Parameters.AddWithValue("@PurchaseId", cost.PurchaseId);
            com.Parameters.AddWithValue("@CostType", cost.CostType);
            com.Parameters.AddWithValue("@CostDescription", cost.CostDescription);
            com.Parameters.AddWithValue("@CostDate", cost.CostDate);
            com.Parameters.AddWithValue("@Amount", cost.Amount);
            com.Parameters.AddWithValue("@IsActive", cost.IsActive);

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