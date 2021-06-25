using PharmacyManagementSystem.Gateway;
using PharmacyManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PharmacyManagementSystem.BLL
{
    public class PurchaseManager
    {
        PurchaseGateway aPurchase = new PurchaseGateway();

        public List<Purchase> GetPurchaseByCostEntry() 
        {
            return aPurchase.GetPurchaseByCostEntry(); 
        }
        public List<Purchase> GetAllPurchase()
        {
            return aPurchase.GetAllPurchase();
        }
        public List<Purchase> GetPurchaseInfoByCostType() 
        {
            return aPurchase.GetPurchaseInfoByCostType();
        }
        public bool SavePurchase(Purchase purchase)
        {
            bool res = aPurchase.SavePurchase(purchase);
            return res;
        }
        public List<Purchase> GetDetails(string startDate, string endDate)
        {
            return aPurchase.GetDetails(startDate, endDate);
        }
    }
}