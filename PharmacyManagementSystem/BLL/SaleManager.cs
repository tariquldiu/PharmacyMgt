using PharmacyManagementSystem.Gateway;
using PharmacyManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PharmacyManagementSystem.BLL
{
    public class SaleManager
    {
        SaleGateway aSale = new SaleGateway(); 
        public List<Sale> GetAllSale() 
        {
            return aSale.GetAllSale(); 
        }
        public List<Sale> GetLastSale()
        {
            return aSale.GetLastSale(); 
        }
        public List<Sale> GetPurchaseForSaleEdit(int purchaseId, int saleId)
        {
            return aSale.GetPurchaseForSaleEdit(purchaseId, saleId);
        }
        public bool DeleteSale(int saleId)
        {
            return aSale.DeleteSale(saleId);
        }
        public string GetBillNo(string saleDate)
        {
            return aSale.GetBillNo(saleDate);
        }
        public bool SaveSale(Sale sale)
        {
            bool res = aSale.SaveSale(sale);
            return res;
        }
        public List<Sale> GetDetails(string startDate, string endDate)
        {
            return aSale.GetDetails(startDate, endDate);
        }
    }
}