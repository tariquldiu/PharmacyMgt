using PharmacyManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PharmacyManagementSystem.Gateway
{
    public class PurchaseGateway : Gateway
    {
        public List<Purchase> GetDetails(string startDate, string endDate)
        {
            List<Purchase> purchaseList = new List<Purchase>();
            SqlCommand com = new SqlCommand("pha_SearchPurchaseDetails", Connection);
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
            purchaseList = (from DataRow dr in dt.Rows

                            select new Purchase()
                            {
                                PurchaseId = Convert.ToInt32(dr["PurchaseId"]),
                                ProductId = Convert.ToInt32(dr["ProductId"]),
                                ProductName = Convert.ToString(dr["ProductName"]),
                                WarehouseId = Convert.ToInt32(dr["WarehouseId"]),
                                WarehouseName = Convert.ToString(dr["WarehouseName"]),
                                GroupName = Convert.ToString(dr["GroupName"]),
                                BrandName = Convert.ToString(dr["BrandName"]),
                                TypeName = Convert.ToString(dr["TypeName"]),
                                UnitPrice = Convert.ToDecimal(dr["UnitPrice"]),
                                Quantity = Convert.ToInt32(dr["Quantity"]),
                                TotalQuantity = Convert.ToInt32(dr["TotalQuantity"]),
                                Measurement = Convert.ToString(dr["Measurement"]),
                                Description = Convert.ToString(dr["Description"]),
                                PurchaseDate = Convert.ToDateTime(dr["PurchaseDate"]),
                                ManufacturingDate = Convert.ToDateTime(dr["ManufacturingDate"]),
                                ExpireDate = Convert.ToDateTime(dr["ExpireDate"]),
                                IsStock = Convert.ToBoolean(dr["IsStock"])

                            }).ToList();


            return purchaseList;
        }
        public List<Purchase> GetAllPurchase()
        {

            List<Purchase> purchaseList = new List<Purchase>();
            SqlCommand com = new SqlCommand("pha_GetAllPurchase", Connection);
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

            purchaseList = (from DataRow dr in dt.Rows

                            select new Purchase()
                            {
                                PurchaseId = Convert.ToInt32(dr["PurchaseId"]),
                                ProductId = Convert.ToInt32(dr["ProductId"]),
                                ProductName = Convert.ToString(dr["ProductName"]),
                                WarehouseId = Convert.ToInt32(dr["WarehouseId"]),
                                WarehouseName = Convert.ToString(dr["WarehouseName"]),
                                GroupName = Convert.ToString(dr["GroupName"]),
                                BrandName = Convert.ToString(dr["BrandName"]),
                                TypeName = Convert.ToString(dr["TypeName"]),
                                UnitPrice = Convert.ToDecimal(dr["UnitPrice"]),
                                Quantity = Convert.ToInt32(dr["Quantity"]),
                                TotalQuantity = Convert.ToInt32(dr["TotalQuantity"]),
                                Measurement = Convert.ToString(dr["Measurement"]),
                                Description = Convert.ToString(dr["Description"]),
                                PurchaseDate = Convert.ToDateTime(dr["PurchaseDate"]),
                                ManufacturingDate = Convert.ToDateTime(dr["ManufacturingDate"]),
                                ExpireDate = Convert.ToDateTime(dr["ExpireDate"]),
                                IsStock = Convert.ToBoolean(dr["IsStock"])

                            }).ToList();


            return purchaseList; 

        }
        public List<Purchase> GetPurchaseByCostEntry() 
        {

            List<Purchase> purchaseList = new List<Purchase>();
            SqlCommand com = new SqlCommand("pha_GetPurchaseByCostEntry", Connection);
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

            purchaseList = (from DataRow dr in dt.Rows

                            select new Purchase()
                            {
                                PurchaseId = Convert.ToInt32(dr["PurchaseId"]),
                                ProductId = Convert.ToInt32(dr["ProductId"]),
                                ProductName = Convert.ToString(dr["ProductName"]),
                                WarehouseId = Convert.ToInt32(dr["WarehouseId"]),
                                WarehouseName = Convert.ToString(dr["WarehouseName"]),
                                GroupName = Convert.ToString(dr["GroupName"]),
                                BrandName = Convert.ToString(dr["BrandName"]),
                                TypeName = Convert.ToString(dr["TypeName"]),
                                UnitPrice = Convert.ToDecimal(dr["UnitPrice"]),
                                Quantity = Convert.ToInt32(dr["Quantity"]),
                                Measurement = Convert.ToString(dr["Measurement"]),
                                Description = Convert.ToString(dr["Description"]),
                                PurchaseDate = Convert.ToDateTime(dr["PurchaseDate"]),
                                ManufacturingDate = Convert.ToDateTime(dr["ManufacturingDate"]),
                                ExpireDate = Convert.ToDateTime(dr["ExpireDate"]),
                                IsStock = Convert.ToBoolean(dr["IsStock"])

                            }).ToList();


            return purchaseList;

        }
        public List<Purchase> GetPurchaseInfoByCostType() 
        {

            List<Purchase> purchaseList = new List<Purchase>();
            SqlCommand com = new SqlCommand("pha_GetPurchaseInfoByCostType", Connection);
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
            
            purchaseList = (from DataRow dr in dt.Rows

                            select new Purchase()
                            {
                                PurchaseId = Convert.ToInt32(dr["PurchaseId"]),
                                ProductId = Convert.ToInt32(dr["ProductId"]),
                                ProductName = Convert.ToString(dr["ProductName"]),
                                WarehouseId = Convert.ToInt32(dr["WarehouseId"]),
                                UnitPrice = Convert.ToDecimal(dr["UnitPrice"]),
                                Quantity = Convert.ToInt32(dr["Quantity"]),
                                Measurement = Convert.ToString(dr["Measurement"]),
                                Description = Convert.ToString(dr["Description"]),
                                PurchaseDate = Convert.ToDateTime(dr["PurchaseDate"]),
                                ManufacturingDate = Convert.ToDateTime(dr["ManufacturingDate"]),
                                ExpireDate = Convert.ToDateTime(dr["ExpireDate"]),
                                IsStock = Convert.ToBoolean(dr["IsStock"])

                            }).ToList();


            return purchaseList;

        }
        public bool SavePurchase(Purchase purchase)
        { 
            SqlCommand com = new SqlCommand("pha_SavePurchase", Connection);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@PurchaseId", purchase.PurchaseId);
            com.Parameters.AddWithValue("@ProductId", purchase.ProductId);
            com.Parameters.AddWithValue("@WarehouseId", purchase.WarehouseId);
            com.Parameters.AddWithValue("@Measurement", purchase.Measurement);
            com.Parameters.AddWithValue("@Description", purchase.Description);
            com.Parameters.AddWithValue("@IsStock", purchase.IsStock);
            com.Parameters.AddWithValue("@PurchaseDate", purchase.PurchaseDate);
            com.Parameters.AddWithValue("@ManufacturingDate", purchase.ManufacturingDate);
            com.Parameters.AddWithValue("@ExpireDate", purchase.ExpireDate);
            com.Parameters.AddWithValue("@UnitPrice", purchase.UnitPrice);
            com.Parameters.AddWithValue("@Quantity", purchase.Quantity);
            com.Parameters.AddWithValue("@IsActive", purchase.IsActive);
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