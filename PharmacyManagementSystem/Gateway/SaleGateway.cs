using PharmacyManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;

namespace PharmacyManagementSystem.Gateway
{
    public class SaleGateway: Gateway
    {
        public List<Sale> GetDetails(string startDate, string endDate)
        {
            List<Sale> saleList = new List<Sale>();
            SqlCommand com = new SqlCommand("pha_SearchSaleDetails", Connection);
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
            saleList = (from DataRow dr in dt.Rows

                        select new Sale()
                        {
                            SaleId = Convert.ToInt32(dr["SaleId"]),
                            BillNo = Convert.ToString(dr["BillNo"]),
                            PurchaseId = Convert.ToInt32(dr["PurchaseId"]),
                            ProductId = Convert.ToInt32(dr["ProductId"]),
                            ProductName = Convert.ToString(dr["ProductName"]),
                            Quantity = Convert.ToInt32(dr["Quantity"]),
                            UnitPrice = Convert.ToDecimal(dr["UnitPrice"]),
                            Amount = Convert.ToDecimal(dr["Amount"]),
                            Discount = Convert.ToDecimal(dr["Discount"]),
                            DiscountType = Convert.ToString(dr["DiscountType"]),
                            SaleFor = Convert.ToString(dr["SaleFor"]),
                            CustomerName = Convert.ToString(dr["CustomerName"]),
                            CustomerContact = Convert.ToString(dr["CustomerContact"]),
                            SaleDate = Convert.ToDateTime(dr["SaleDate"]),
                            IsActive = Convert.ToBoolean(dr["IsActive"])

                        }).ToList();


            return saleList;
        }
        public List<Sale> GetPurchaseForSaleEdit(int purchaseId, int saleId) 
        {

            List<Sale> purchaseList = new List<Sale>();
            SqlCommand com = new SqlCommand("pha_GetPurchaseForSaleEdit", Connection);
            com.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(com);
            com.Parameters.AddWithValue("@PurchaseId", purchaseId);
            com.Parameters.AddWithValue("@SaleId", saleId);
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

                            select new Sale()
                            {
                                SaleId = Convert.ToInt32(dr["SaleId"]),
                                BillNo = Convert.ToString(dr["BillNo"]),
                                PurchaseId = Convert.ToInt32(dr["PurchaseId"]),
                                ProductId = Convert.ToInt32(dr["ProductId"]),
                                ProductName = Convert.ToString(dr["ProductName"]),
                                Quantity = Convert.ToInt32(dr["Quantity"]),
                                UnitPrice = Convert.ToDecimal(dr["UnitPrice"]),
                                Amount = Convert.ToDecimal(dr["Amount"]),
                                Discount = Convert.ToDecimal(dr["Discount"]),
                                DiscountType = Convert.ToString(dr["DiscountType"]),
                                SaleFor = Convert.ToString(dr["SaleFor"]),
                                CustomerName = Convert.ToString(dr["CustomerName"]),
                                CustomerContact = Convert.ToString(dr["CustomerContact"]),
                                SaleDate = Convert.ToDateTime(dr["SaleDate"]),
                                IsActive = Convert.ToBoolean(dr["IsActive"])

                            }).ToList();


            return purchaseList;

        }
        public List<Sale> GetAllSale() 
        {

            List<Sale> saleList = new List<Sale>();
            SqlCommand com = new SqlCommand("pha_GetAllSale", Connection);
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

            saleList = (from DataRow dr in dt.Rows

                             select new Sale()
                             {
                                 SaleId = Convert.ToInt32(dr["SaleId"]),
                                 BillNo = Convert.ToString(dr["BillNo"]),
                                 PurchaseId = Convert.ToInt32(dr["PurchaseId"]),
                                 ProductId = Convert.ToInt32(dr["ProductId"]),
                                 ProductName = Convert.ToString(dr["ProductName"]),
                                 Quantity = Convert.ToInt32(dr["Quantity"]),
                                 UnitPrice = Convert.ToDecimal(dr["UnitPrice"]),
                                 Amount = Convert.ToDecimal(dr["Amount"]),
                                 Discount = Convert.ToDecimal(dr["Discount"]),
                                 DiscountType = Convert.ToString(dr["DiscountType"]),
                                 SaleFor = Convert.ToString(dr["SaleFor"]),
                                 CustomerName = Convert.ToString(dr["CustomerName"]),
                                 CustomerContact = Convert.ToString(dr["CustomerContact"]),
                                 SaleDate = Convert.ToDateTime(dr["SaleDate"]),
                                 IsActive = Convert.ToBoolean(dr["IsActive"])

                             }).ToList();


            return saleList;
        }
        public List<Sale> GetLastSale()
        {

            List<Sale> saleList = new List<Sale>();
            SqlCommand com = new SqlCommand("pha_GetLastSale", Connection);
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

            saleList = (from DataRow dr in dt.Rows

                        select new Sale()
                        {
                            SaleId = Convert.ToInt32(dr["SaleId"]),
                            BillNo = Convert.ToString(dr["BillNo"]),
                            PurchaseId = Convert.ToInt32(dr["PurchaseId"]),
                            ProductId = Convert.ToInt32(dr["ProductId"]),
                            ProductName = Convert.ToString(dr["ProductName"]),
                            Quantity = Convert.ToInt32(dr["Quantity"]),
                            UnitPrice = Convert.ToDecimal(dr["UnitPrice"]),
                            Amount = Convert.ToDecimal(dr["Amount"]),
                            Discount = Convert.ToDecimal(dr["Discount"]),
                            DiscountType = Convert.ToString(dr["DiscountType"]),
                            SaleFor = Convert.ToString(dr["SaleFor"]),
                            CustomerName = Convert.ToString(dr["CustomerName"]),
                            CustomerContact = Convert.ToString(dr["CustomerContact"]),
                            SaleDate = Convert.ToDateTime(dr["SaleDate"]),
                            IsActive = Convert.ToBoolean(dr["IsActive"])

                        }).ToList();


            return saleList;
        }
        
        public string GetBillNo(string saleDate)
        {
            SqlCommand com = new SqlCommand("pha_GetBillNoBySaleDate", Connection);
            com.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(com);
            com.Parameters.AddWithValue("@SaleDate", saleDate);
            string billNo;
            try
            {
                Connection.Open();
                billNo = com.ExecuteScalar().ToString();
            }
            finally
            {
                Connection.Close();
            }
            return billNo;
        }
        public bool DeleteSale(int saleId)
        {
            SqlCommand com = new SqlCommand("pha_DeleteSale", Connection);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@SaleId", saleId);

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
        public bool SaveSale(Sale sale)
        {
            SqlCommand com = new SqlCommand("pha_SaveSale", Connection);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@SaleId", sale.SaleId);
            com.Parameters.AddWithValue("@BillNo", sale.BillNo);
            com.Parameters.AddWithValue("@PurchaseId", sale.PurchaseId);
            com.Parameters.AddWithValue("@Quantity", sale.Quantity);
            com.Parameters.AddWithValue("@UnitPrice", sale.UnitPrice);
            com.Parameters.AddWithValue("@Amount", sale.Amount);
            com.Parameters.AddWithValue("@Discount", sale.Discount);
            com.Parameters.AddWithValue("@DiscountType", sale.DiscountType);
            com.Parameters.AddWithValue("@SaleFor", sale.SaleFor);
            com.Parameters.AddWithValue("@CustomerName", sale.CustomerName);
            com.Parameters.AddWithValue("@CustomerContact", sale.CustomerContact);
            com.Parameters.AddWithValue("@SaleDate", sale.SaleDate);
            com.Parameters.AddWithValue("@IsActive", sale.IsActive);

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