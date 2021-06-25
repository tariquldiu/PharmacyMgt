using PharmacyManagementSystem.Gateway;
using PharmacyManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PharmacyManagementSystem.BLL
{
    public class CostManager
    {
        CostGateway aCost = new CostGateway();

        public List<Cost> GetAllCost()
        { 
            return aCost.GetAllCost();
        }
        public bool SaveCost(Cost cost)
        {
            bool res = aCost.SaveCost(cost);
            return res;
        }
        public List<Cost> GetDetails(string startDate, string endDate)
        {
            return aCost.GetDetails(startDate, endDate);
        }
    }
}